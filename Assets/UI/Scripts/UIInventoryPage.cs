using UnityEngine;
using System.Collections.Generic;
using System;

namespace Inventory.UI
{
    public class UIInventoryPage : MonoBehaviour
    {
        [SerializeField]
        private UIInventoryItem itemPrefab;

        [SerializeField]
        private RectTransform contentPanel;

        [SerializeField]
        private UIInventoryDescription itemDescription;

        [SerializeField]
        private MouseFollower mouseFollower;

        List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>();


        public event Action<int> OnDescriptionRequested, OnItemActionRequested, OnStartDragging;

        public event Action<int, int> OnSwapItems;

        private int currentlyDraggedItemIndex = -1;

        public void InitializeInventoryUI(int inventorySize)
        {
            for (int i = 0; i < inventorySize; i++)
            {
                UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentPanel);
                listOfUIItems.Add(uiItem);
                uiItem.OnItemClicked += HandleItemSelction;
                uiItem.OnItemBeginDrag += HandleBeginDrag;
                uiItem.OnItemDroppedOn += HandleSwap;
                uiItem.OnItemEndDrag += HandleEndDrag;
                uiItem.OnRightMouseBtnClick += HandleShowItemActions;
            }
        }

        public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
        {
            if (listOfUIItems.Count > itemIndex)
            {
                listOfUIItems[itemIndex].SetData(itemImage, itemQuantity);
            }
        }

        private void HandleShowItemActions(UIInventoryItem item)
        {

        }

        private void HandleEndDrag(UIInventoryItem item)
        {
            ResetDraggedItem();
        }

        private void HandleSwap(UIInventoryItem item)
        {
            int index = listOfUIItems.IndexOf(item);
            if (index == -1)
            {
                return;
            }
            OnSwapItems?.Invoke(currentlyDraggedItemIndex, index);
            HandleItemSelction(item);

        }

        private void ResetDraggedItem()
        {
            mouseFollower.Toggle(false);
            currentlyDraggedItemIndex = -1;
        }

        private void HandleBeginDrag(UIInventoryItem item)
        {
            int index = listOfUIItems.IndexOf(item);
            if (index == -1)
                return;
            currentlyDraggedItemIndex = index;
            HandleItemSelction(item);
            OnStartDragging?.Invoke(index);
        }

        public void CreateDraggedItem(Sprite sprite, int quantity)
        {
            mouseFollower.Toggle(true);
            mouseFollower.SetData(sprite, quantity);
        }

        private void HandleItemSelction(UIInventoryItem item)
        {
            int index = listOfUIItems.IndexOf(item);
            if (index == -1)
                return;
            OnDescriptionRequested?.Invoke(index);
        }

        private void Awake()
        {
            Hide();
            mouseFollower.Toggle(false);
            itemDescription.ResetDescription();
        }

        public void Show()
        {
            gameObject.SetActive(true);
            itemDescription.ResetDescription();
            ResetSelection();
        }

        public void ResetSelection()
        {
            itemDescription.ResetDescription();
            DeselectAllItems();
        }

        private void DeselectAllItems()
        {
            foreach (UIInventoryItem item in listOfUIItems)
            {
                item.Deselect();
            }
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            ResetDraggedItem();
        }

        internal void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description)
        {
            itemDescription.SetDescription(itemImage, name, description);
            DeselectAllItems();
            listOfUIItems[itemIndex].Select();
        }

        internal void ResetAllItems()
        {
            foreach (var item in listOfUIItems)
            {
                item.ResetData();
                item.Deselect();
            }
        }
    }
}