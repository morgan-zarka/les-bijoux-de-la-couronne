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

            GemMaterial gem = GetComponent<GemMaterial>();

            if (gem != null && gem.IsEndTrigger())
            {
                // Todo
            }
        }
    }

    public void SetInt(string keyName, int value)
    {
        PlayerPrefs.SetInt(keyName, value);
    }

    public int GetInt(string keyName)
    {
        return PlayerPrefs.GetInt(keyName);
    }

    public void Save(){
        PlayerPrefs.Save();
    }

}
