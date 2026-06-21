using UnityEngine;
using System.Collections;

public class Case : MonoBehaviour
{
    [HideInInspector] public int coordX;
    [HideInInspector] public int coordZ;

    private CheminCorrect cheminCorrect;
    private DamierManager damierManager;
    private Renderer rend;
    private Color couleurOriginale;
    private bool gemmePosee = false;

    [Header("Couleur du piège")]
    public Color couleurPiege = new Color(1f, 0f, 0f, 0.5f);
    public float dureeFlash = 0.4f;

    [Header("Gemme (trace de pas)")]
    public GameObject gemmePrefab;

    void Start()
    {
        cheminCorrect = FindFirstObjectByType<CheminCorrect>();
        damierManager = FindFirstObjectByType<DamierManager>();
        rend = GetComponent<Renderer>();
        couleurOriginale = rend.material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (!damierManager.EstDebloque())
        {
            damierManager.AfficherMessageBloque();
            return;
        }

        if (cheminCorrect == null) cheminCorrect = FindFirstObjectByType<CheminCorrect>();
        if (damierManager == null) damierManager = FindFirstObjectByType<DamierManager>();

        bool sure = cheminCorrect.EstSure(coordX, coordZ);

        if (!sure)
        {
            StartCoroutine(FlashRouge());
            damierManager.SignalerErreur(other.gameObject);
        }
        else if (!gemmePosee && gemmePrefab != null)
        {
            gemmePosee = true;
            GameObject gemmeInstance = Instantiate(gemmePrefab, transform);
            gemmeInstance.transform.position = transform.position + Vector3.up * 0.1f;

            Animator anim = gemmeInstance.GetComponent<Animator>();
            if (anim != null) anim.enabled = false;

            ScoreManager scoreManagerGemme = gemmeInstance.GetComponent<ScoreManager>();
            if (scoreManagerGemme != null) scoreManagerGemme.enabled = false;

            damierManager.SignalerBonneCase();
        }
    }

    private IEnumerator FlashRouge()
    {
        rend.material.color = couleurPiege;
        yield return new WaitForSeconds(dureeFlash);
        rend.material.color = couleurOriginale;
    }
}