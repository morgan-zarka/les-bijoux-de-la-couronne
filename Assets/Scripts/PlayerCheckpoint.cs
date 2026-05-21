using UnityEngine;

public class PlayerCheckpoint : MonoBehaviour
{
    [SerializeField] private Vector3 respawnPoint;
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

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnRespawn -= Respawn;
        }
    }
}
