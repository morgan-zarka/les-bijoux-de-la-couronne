using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardWalk : MonoBehaviour
{

    public float MovementSpeed = 1f;
    public float TurningSpeed = 3f;
    public Transform WayPointsParent;
    public string WpTag = "WalkWp";
    public bool isLoop = false;




    bool IsMoving;
    List<GameObject> Wps = new List<GameObject>();
    int nextWp;
    bool WpReached;

    void Start()
    {
        StartCoroutine(PatrolNow());
        IsMoving = false;
        WpReached = false;
        foreach (Transform wp in WayPointsParent.transform)
        {
            Wps.Add(wp.transform.gameObject);
        }
    }

    void FixedUpdate()
    {
        if (IsMoving)
        {
            Vector3 lookPos = Wps[nextWp].transform.position - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * TurningSpeed);
            transform.position += transform.forward * Time.deltaTime * MovementSpeed;
        }
    }

    IEnumerator PatrolNow()
    {
        yield return new WaitForSeconds(0.1f);
        nextWp = FindClosestPoint();
        IsMoving = true;
    }

    public int FindClosestPoint()
    {
        int closest = 0;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        int goidx = 0;
        foreach (Transform go in WayPointsParent.transform)
        {
            Vector3 diff = go.gameObject.transform.position - position;
            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance)
            {
                closest = goidx;
                distance = curDistance;
            }
            goidx++;
        }
        return closest;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == WpTag && !WpReached)
        {

            WpReached = true;
            if (nextWp == Wps.Count - 1)
            {
                if (isLoop)
                {
                    nextWp = 0;
                }
                else
                {
                    IsMoving = false;
                }

            }
            else
            {
                nextWp++;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == WpTag && WpReached)
        {
            WpReached = false;
        }

    }
}