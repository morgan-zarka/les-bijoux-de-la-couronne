using GLTFast.Schema;
using NUnit.Framework;
using UnityEngine;

public class CrownsManager : MonoBehaviour
{
    [SerializeField] private GameObject[] crowns;
    void Start()
    {
        int goodCrown = UnityEngine.Random.Range(0, crowns.Length - 1);

        crowns[goodCrown].GetComponent<Crown>().setAsTrue();
    }
}
