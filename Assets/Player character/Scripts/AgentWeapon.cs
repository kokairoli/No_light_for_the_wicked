using Inventory.Model;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AgentWeapon : MonoBehaviour
{
    [SerializeField]
    private EquippableItemSO weapon;

    [SerializeField]
    private InventorySO inventoryData;

    [SerializeField]
    private List<ItemParameter> parametersToModify, itemsCurrentState;


    public void SetWeapon(EquippableItemSO weaponItemSO, List<ItemParameter> itemState)
    {
        if (weapon != null)
        {
            inventoryData.AddItem(weapon, 1, itemsCurrentState);
        }

        this.weapon = weaponItemSO;
        this.itemsCurrentState = itemState;
        ModifyParameters();
    }

    private void ModifyParameters()
    {
        foreach (var parameter in parametersToModify)
        {
            if (itemsCurrentState.Contains(parameter))
            {
                int index = itemsCurrentState.IndexOf(parameter);
                float newValue = itemsCurrentState[index].value + parameter.value;
                itemsCurrentState[index] = new ItemParameter { itemParameter = parameter.itemParameter, value = newValue };
            }
        }
    }
}
