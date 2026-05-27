using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class GemMaterial : MonoBehaviour
{
    [SerializeField] private Material[] materials;

    void Start()
    {
        if (materials == null || materials.Length == 0)
        {
            Debug.LogWarning($"Aucun Material assigné sur '{name}'. Assignez-les dans l'inspecteur ou utilisez l'éditeur pour remplir depuis Assets/Materials.");
        }
        else
        {
            var renderer = GetComponentInChildren<Renderer>();
            Material material = materials[UnityEngine.Random.Range(0, materials.Length)];
            renderer.material = material;

            Light light = GetComponentInChildren<Light>();
            light.color = material.GetColor("_BaseColor");
        }
    }
}
