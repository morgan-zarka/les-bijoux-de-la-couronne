using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class GemMaterial : MonoBehaviour
{
    [SerializeField] private Material[] materials;
    [SerializeField] private bool isEndTrigger;

    void Start()
    {
        if (materials == null || materials.Length <= 1)
        {
            Debug.LogWarning($"Aucun Material assigné sur '{name}'. Assignez-les dans l'inspecteur ou utilisez l'éditeur pour remplir depuis Assets/Materials.");
        }
        else
        {
            var renderer = GetComponentInChildren<Renderer>();
            Material material = null;
            if (!isEndTrigger)
            {
                material = materials[UnityEngine.Random.Range(0, materials.Length - 1)];
            } else
            {
                material = materials[^1];
            }
            
            renderer.material = material;

            Light light = GetComponentInChildren<Light>();
            light.color = material.GetColor("_BaseColor");
        }
    }

    public bool IsEndTrigger()
    {
        return isEndTrigger;
    }
}
