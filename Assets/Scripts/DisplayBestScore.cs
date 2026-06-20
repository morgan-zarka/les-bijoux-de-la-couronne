using TMPro;
using UnityEngine;

public class DisplayBestScore : MonoBehaviour
{
    void Start()
    {
        if (GameManager.Instance.isBestScore())
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = "Nouveau record !";
        }
        else {
            gameObject.GetComponent<TextMeshProUGUI>().text = $"Meilleur score : {GameManager.Instance.GetBestScore():0.##}";
        }
    }
}
