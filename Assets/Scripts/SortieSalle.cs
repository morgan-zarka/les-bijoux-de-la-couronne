using UnityEngine;
using UnityEngine.SceneManagement;

public class SortieSalle : MonoBehaviour
{
    [SerializeField] private LayerMask PlayerLayer;
    [SerializeField] private string sceneSuivante = "Menu";

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Quelque chose est entré : " + other.name + " (layer: " + LayerMask.LayerToName(other.gameObject.layer) + ")");

        if ((PlayerLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            SceneManager.LoadScene(sceneSuivante);
        }
    }
}