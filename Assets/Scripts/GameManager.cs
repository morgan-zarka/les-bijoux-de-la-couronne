using StarterAssets;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event Action OnRespawn;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject transitionItem;

    private ThirdPersonController playerController;
    private StarterAssetsInputs playerInputs;
    private Animator animator;

    private const string TRANSITION_FLAG = "transitionAsked";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        playerController = player.GetComponent<ThirdPersonController>();
        playerInputs = player.GetComponent<StarterAssetsInputs>();
        animator = transitionItem.GetComponent<Animator>();
    }

    public void TriggerRespawn()
    {
        StartCoroutine(RespawnRoutine());
    }


    private IEnumerator RespawnRoutine()
    {
        playerController.enabled = false;
        playerInputs.move = Vector2.zero;

        yield return new WaitForSeconds(1.5f);

        animator.SetBool(TRANSITION_FLAG, true);

        yield return new WaitForSeconds(1.25f);

        OnRespawn?.Invoke();
        animator.SetBool(TRANSITION_FLAG, false);
        playerController.enabled = true;
    }
}