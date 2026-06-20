using GLTFast.Schema;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class Crown : MonoBehaviour
{
    [SerializeField] private LayerMask PlayerLayer;
    private bool isTrueOne = false;
    private bool tried = false;

    public void setAsTrue()
    {
        this.isTrueOne = true;
    }

    void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnRespawn += Respawn;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((PlayerLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            if (isTrueOne)
            {
                GameManager.Instance.ScorePoints(200);
                GameManager.Instance.ChangeScene("MenuFinDeJeu");
            } else
            {
                tried = true;
                GameManager.Instance.TriggerRespawn();
            }
        }
    }

    void Respawn()
    {
        if (tried)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnRespawn -= Respawn;
        }
    }
}
