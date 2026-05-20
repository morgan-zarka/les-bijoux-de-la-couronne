using Unity.Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineCamera m_vcam;
    [SerializeField] private int m_priority = 100;
    private const string PLAYER_TAG = "Player";

    private void OnTriggerEnter(Collider other)
    {
        // When the player enters the trigger, the camera will change to the new one
        if (other.CompareTag(PLAYER_TAG))
        {
            var brain = Camera.main.GetComponent<CinemachineBrain>();

            if (brain != null && brain.ActiveVirtualCamera != null)
            {
                if (brain.ActiveVirtualCamera is CinemachineCamera activeCam)
                {
                    activeCam.Priority = 0;
                }

                m_vcam.Priority = m_priority;
            }
        }
    }
}