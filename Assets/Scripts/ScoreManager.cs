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
            GameManager.Instance.scorePoints(value);
            gameObject.SetActive(false);
        }
    }
}
