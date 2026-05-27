using StarterAssets;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event Action OnRespawn;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject transitionItem;
    [SerializeField] private TextMeshProUGUI scoreField;

    private ThirdPersonController playerController;
    private StarterAssetsInputs playerInputs;
    private Animator animator;
    private float score = 0;

    public float Score { 
        get => score; 
        private set
        {
            score = value;
            updateScore();
        }
    }

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

    public void TriggerRespawn(bool ignoreScoreReset = false)
    {
        StartCoroutine(RespawnRoutine(ignoreScoreReset));
    }


    private IEnumerator RespawnRoutine(bool ignoreScoreReset)
    {
        playerController.enabled = false;
        playerInputs.move = Vector2.zero;

        yield return new WaitForSeconds(1.5f);

        animator.SetBool(TRANSITION_FLAG, true);

        yield return new WaitForSeconds(1.25f);

        OnRespawn?.Invoke();

        if (!ignoreScoreReset)
        {
            this.Score /= 2;
        }
        animator.SetBool(TRANSITION_FLAG, false);
        playerController.enabled = true;
    }

    public void scorePoints(int score)
    {
        this.Score += score;
    }

    private void updateScore()
    {
        scoreField.text = $"Score : {score:0.##}";
    }
}