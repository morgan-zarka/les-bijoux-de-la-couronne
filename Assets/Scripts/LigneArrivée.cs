using UnityEngine;

public class LigneArrivee : MonoBehaviour
{
    public DamierManager damierManager;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Quelque chose a touché la ligne d'arrivée : " + other.name);
        if (other.CompareTag("Player"))
        {
            Debug.Log("C'est le joueur, on appelle DamierComplete");
            damierManager.DamierComplete();
        }
    }
}