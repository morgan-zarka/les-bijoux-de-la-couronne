using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private EquipmentManager _equipmentManager;

    private void Start()
    {
        _equipmentManager = GetComponent<EquipmentManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item != null)
        {
            _equipmentManager.Equip();
            Destroy(other.gameObject);
        }
    }
}