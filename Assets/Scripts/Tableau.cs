using UnityEngine;
using TMPro;

public class Tableau : MonoBehaviour
{
    [Header("Références")]
    public CarteAffichage carteAffichage;
    public TextMeshPro texteInterrogation;

    private bool dejaDeclenche = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !dejaDeclenche)
        {
            dejaDeclenche = true;
            if (texteInterrogation != null) texteInterrogation.text = "MAP";
            carteAffichage.AfficherCarte();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dejaDeclenche = false;
        }
    }
}