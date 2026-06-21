using StarterAssets;
using System.Collections;
using TMPro;
using UnityEngine;

public class CarteAffichage : MonoBehaviour
{
    [Header("Références")]
    public GameObject panneauCarte;
    public TextMeshProUGUI texteCompteur;
    public DamierManager damierManager;
    public GameObject fondSombre;

    private float tempsRestant;

    private void Start()
    {
        panneauCarte.SetActive(false);
        if (fondSombre != null) fondSombre.SetActive(false);
    }

    public void AfficherCarte()
    {
        tempsRestant = damierManager.GetTempsActuel();
        StartCoroutine(AffichageRoutine());
    }

    private IEnumerator AffichageRoutine()
    {
        GameObject joueur = GameObject.FindWithTag("Player");
        ThirdPersonController ctrl = joueur?.GetComponent<ThirdPersonController>();
        StarterAssetsInputs playerInputs = joueur.GetComponent<StarterAssetsInputs>();
        Animator playerAnimator = joueur.GetComponent<Animator>();

        if (ctrl != null) ctrl.enabled = false;
        if (playerInputs != null) playerInputs.move = Vector2.zero;
        if (playerAnimator != null)
        {
            playerAnimator.SetFloat("Speed", 0f);
            playerAnimator.SetFloat("MotionSpeed", 0f);
        }

        if (tempsRestant <= 0)
        {
            if (texteCompteur != null)
                texteCompteur.text = "Plus de carte disponible";
            yield return new WaitForSeconds(1.5f);
            damierManager.CarteVue();
            if (ctrl != null) ctrl.enabled = true;
            yield break;
        }

        if (fondSombre != null) fondSombre.SetActive(true);
        panneauCarte.SetActive(true);

        while (tempsRestant > 0)
        {
            if (texteCompteur != null)
                texteCompteur.text = Mathf.CeilToInt(tempsRestant) + "s";
            tempsRestant -= Time.deltaTime;
            yield return null;
        }

        panneauCarte.SetActive(false);
        if (fondSombre != null) fondSombre.SetActive(false);
        damierManager.CarteVue();

        if (ctrl != null) ctrl.enabled = true;
    }
}