using UnityEngine;

public class PlayerCheckpoint : MonoBehaviour
{
    [SerializeField] private Vector3 respawnPoint;
    [SerializeField] private LayerMask CpLayer;

    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Respawn();

        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnRespawn += Respawn;
        }
    }

    void Respawn()
    {
        characterController.enabled = false;
        transform.position = respawnPoint;
        characterController.enabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if ((CpLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            respawnPoint = other.transform.position;
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
