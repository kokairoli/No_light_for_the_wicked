using UnityEngine;

namespace Inventory.Model
{
    public class EquippableItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        public string ActionName =>"Equip";

        public AudioClip audioClip { get; private set; }

        public bool PerformAction(GameObject character)
        {
            throw new System.NotImplementedException();
        }
    }
}