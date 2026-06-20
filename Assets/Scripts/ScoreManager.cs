using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private LayerMask PlayerLayer;
    [SerializeField] private int value = 50;

    private void OnTriggerEnter(Collider other)
    {
        if ((PlayerLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            GameManager.Instance.ScorePoints(value);
            gameObject.SetActive(false);

            GemMaterial gem = GetComponent<GemMaterial>();

            if (gem != null && gem.IsEndTrigger().Item1)
            {
                GameManager.Instance.ChangeScene(gem.IsEndTrigger().Item2);
            }
        }
    }
}
