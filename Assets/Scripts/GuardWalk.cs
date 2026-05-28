using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardWalk : MonoBehaviour
{

    [SerializeField] private float MovementSpeed = 1f;
    [SerializeField] private float TurningSpeed = 5f;
    [SerializeField] private Transform WayPointsParent;
    [SerializeField] private LayerMask WpLayer;
    [SerializeField] private bool isLoop = false;
    [SerializeField] private int delta = 0;

    [SerializeField] private LayerMask PlayerLayer;
    [SerializeField] private LayerMask ObstacleLayer;
    [SerializeField] private float fov = 45f;
    [SerializeField] private float viewDistance = 10f;

    private const string AWARE_FLAG = "IsAware";
    private const string WARNING_FLAG = "IsWarning";

    private bool IsMoving;
    private bool PlayerInArea = false;
    private List<GameObject> Wps = new List<GameObject>();
    private int nextWp = 0;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        foreach (Transform wp in WayPointsParent.transform)
        {
            Wps.Add(wp.transform.gameObject);
        }

        Respawn();
        StartCoroutine(PatrolNow());

        StartCoroutine(FieldOfViewCheck());

        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnRespawn += Respawn;
        }
    }

    void Respawn()
    {
        transform.position = Wps[delta].transform.position;
        IsMoving = true;
        animator.SetBool(AWARE_FLAG, false);
        animator.SetBool(WARNING_FLAG, false);
        nextWp = delta + 1;

        Vector3 lookPos = Wps[nextWp].transform.position - transform.position;
        lookPos.y = 0;

        transform.rotation = Quaternion.LookRotation(lookPos);
    }

    void FixedUpdate()
    {
        if (IsMoving)
        {
            Vector3 lookPos = Wps[nextWp].transform.position - transform.position;
            lookPos.y = 0;

            if (lookPos.sqrMagnitude > 0.01f)
            {
                Quaternion rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * TurningSpeed);
            }

            transform.position += transform.forward * Time.deltaTime * MovementSpeed;
        }
    }

    private IEnumerator PatrolNow()
    {
        yield return new WaitForSeconds(0.1f);

        IsMoving = true;
    }

    private IEnumerator FieldOfViewCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            DetectPlayer();
        }
    }

    private void DetectPlayer()
    {
        bool isCurrentlySeeingPlayer = false;

        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewDistance, PlayerLayer);

        foreach (Collider target in targetsInViewRadius)
        {
            Vector3 dirToTarget = (target.transform.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dirToTarget) < fov / 2f)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

                if (!Physics.Raycast(transform.position, dirToTarget, distanceToTarget, ObstacleLayer))
                {
                    isCurrentlySeeingPlayer = true;
                    break;
                }
            }
        }

        if (isCurrentlySeeingPlayer && !PlayerInArea)
        {
            PlayerInArea = true;
            if (IsMoving)
            {
                animator.SetBool(AWARE_FLAG, true);
                IsMoving = false;
                StartCoroutine(WarningRoutine());
            }
        }
        else if (!isCurrentlySeeingPlayer && PlayerInArea)
        {
            PlayerInArea = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((WpLayer.value & (1 << other.gameObject.layer)) > 0))
        {
            if (other.gameObject == Wps[nextWp])
            {
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
    }

    private IEnumerator WarningRoutine()
    {
        yield return new WaitForSeconds(2f);

        if (PlayerInArea)
        {
            animator.SetBool(WARNING_FLAG, true);
            GameManager.Instance.TriggerRespawn();
        }
        else
        {
            animator.SetBool(AWARE_FLAG, false);
            IsMoving = true;
        }
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnRespawn -= Respawn;
        }
    }
}