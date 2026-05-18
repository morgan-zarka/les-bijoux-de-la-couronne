using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public Renderer playerRenderer;
    public Color newColor = Color.red;

    public void Equip()
    {
        playerRenderer.material.color = newColor;
    }
}