using System.Collections;
using UnityEngine;
using TMPro;
using StarterAssets;

public class CarteAffichage : MonoBehaviour
{
    [Header("Références")]
    public GameObject panneauCarte;
    public TextMeshProUGUI texteCompteur;
    public DamierManager damierManager;

    private float tempsRestant;

    private void Start()
    {
        panneauCarte.SetActive(false);
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
        if (ctrl != null) ctrl.enabled = false;

        if (tempsRestant <= 0)
        {
            if (texteCompteur != null)
                texteCompteur.text = "Plus de carte disponible";
            yield return new WaitForSeconds(1.5f);
            damierManager.CarteVue();
            if (ctrl != null) ctrl.enabled = true;
            yield break;
        }

        panneauCarte.SetActive(true);

        while (tempsRestant > 0)
        {
            if (texteCompteur != null)
                texteCompteur.text = Mathf.CeilToInt(tempsRestant) + "s";
            tempsRestant -= Time.deltaTime;
            yield return null;
        }

        panneauCarte.SetActive(false);
        damierManager.CarteVue();

        if (ctrl != null) ctrl.enabled = true;
    }
}