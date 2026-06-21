using System.Collections;
using UnityEngine;
using TMPro;

public class DamierManager : MonoBehaviour
{
    [Header("Réglages")]
    public int maxErreurs = 3;
    public int maxTentatives = 3;

    [Header("Temps de mémorisation")]
    private float[] tempsParNiveau = { 5f, 4f, 2f, 0f };
    private int niveauActuel = 0;

    private bool carteVue = false;

    [Header("Références")]
    public GameObject panneauNotification;
    public TextMeshProUGUI texteNotification;
    public GameObject panneauEchec;
    public TextMeshProUGUI texteEssais;

    private int erreursActuelles = 0;
    private int nombreTentatives = 0;
    private bool enAttenteRecommencer = false;

    [Header("Sortie")]
    public GameObject gemmeSortie;
    public int totalCasesChemin = 28;
    private int bonnesCasesTraversees = 0;

    void Start()
    {
        if (texteEssais != null)
            texteEssais.text = "Tentatives : 0 / " + maxTentatives;
    }

    void Update()
    {
        if (enAttenteRecommencer && UnityEngine.InputSystem.Keyboard.current.eKey.wasPressedThisFrame)
        {
            RecommencerSalle();
        }

        if (enAttenteRecommencer && UnityEngine.InputSystem.Keyboard.current.dKey.wasPressedThisFrame)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        }
    }

    public float GetTempsActuel()
    {
        if (niveauActuel < tempsParNiveau.Length)
            return tempsParNiveau[niveauActuel];
        return 0f;
    }

    public void CarteVue()
    {
        carteVue = true;
        niveauActuel++;
    }

    public void SignalerErreur(GameObject joueur)
    {
        erreursActuelles++;

        if (erreursActuelles >= maxErreurs)
        {
            erreursActuelles = 0;
            nombreTentatives++;

            if (texteEssais != null)
                texteEssais.text = "Tentatives : " + nombreTentatives + " / " + maxTentatives;

            if (nombreTentatives >= maxTentatives)
            {
                ProposerRecommencer();
            }
            else
            {
                GameManager.Instance.TriggerRespawn();
            }
        }
    }

    public void SignalerBonneCase()
    {
        bonnesCasesTraversees++;
    }

    public bool EstDebloque()
    {
        return carteVue;
    }

    public void AfficherMessageBloque()
    {
        if (panneauNotification != null && !panneauNotification.activeSelf)
        {
            texteNotification.text = "Examinez d'abord la carte ! Allez vers la vitrine ";
            panneauNotification.SetActive(true);
            StartCoroutine(CacherMessage());
        }
    }

    private IEnumerator CacherMessage()
    {
        yield return new WaitForSeconds(2f);
        panneauNotification.SetActive(false);
    }

    public void ProposerRecommencer()
    {
        if (panneauNotification != null) panneauNotification.SetActive(false);
        if (panneauEchec != null)
        {
            panneauEchec.SetActive(true);
            enAttenteRecommencer = true;
        }
    }

    public void RecommencerSalle()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void DamierComplete()
    {
        if (bonnesCasesTraversees >= totalCasesChemin)
        {
            if (gemmeSortie != null)
                gemmeSortie.SetActive(true);
        }
        else
        {
            AfficherMessageBloque();
        }
    }
}