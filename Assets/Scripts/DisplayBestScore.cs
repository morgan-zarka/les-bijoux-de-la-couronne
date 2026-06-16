using TMPro;
using UnityEngine;

public class DisplayBestScore : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = $"Meilleur score : {GameManager.Instance.GetBestScore():0.##}";
    }
}
