using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardWalk : MonoBehaviour
{

    [SerializeField] private float MovementSpeed = 1f;
    [SerializeField] private float TurningSpeed = 3f;
    [SerializeField] private Transform WayPointsParent;
    [SerializeField] private LayerMask WpLayer;
    [SerializeField] private LayerMask PlayerLayer;
    [SerializeField] private bool isLoop = false;

    private const string AWARE_FLAG = "IsAware";
    private const string WARNING_FLAG = "IsWarning";

    private bool IsMoving;
    private bool PlayerInArea = false;
    private List<GameObject> Wps = new List<GameObject>();
    private int nextWp = 0;
    private bool WpReached;

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

        IsMoving = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((WpLayer.value & (1 << other.gameObject.layer)) > 0) && !WpReached)
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

        if ((PlayerLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            if (IsMoving)
            {
                Animator animator = GetComponent<Animator>();
                animator.SetBool(AWARE_FLAG, true);
                IsMoving = false;

                StartCoroutine(WarningRoutine());
            }

            PlayerInArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (((WpLayer.value & (1 << other.gameObject.layer)) > 0) && WpReached)
        {
            WpReached = false;
        }

        if ((PlayerLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            PlayerInArea = false;
        }
    }

    private IEnumerator WarningRoutine()
    {
        yield return new WaitForSeconds(3f);

        Animator animator = GetComponent<Animator>();

        if (PlayerInArea)
        {
            animator.SetBool(WARNING_FLAG, true);
        }
        else
        {
            animator.SetBool(AWARE_FLAG, false);
            IsMoving = true;
        }


    }
}