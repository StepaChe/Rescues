using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Rescues
{
    public sealed class Inventory : MonoBehaviour
    {
        #region Fields

        [SerializeField] List<ItemSlot> ItemSlots;
        [SerializeField] List<ItemRecipe> CraftableItemsList;
        [SerializeField] Image _draggableItem;
        [SerializeField] InventoryTooltip _inventoryTooltip;
        private ItemSlot _draggedSlot;       

        #endregion


        #region UnityMethods

        public void Awake()
        {
            if (ItemSlots != null)
            {
                for (int i = 0; i < ItemSlots.Count; i++)
                {
                    ItemSlots[i].OnBeginDragEvent += BeginDrag;
                    ItemSlots[i].OnEndDragEvent += EndDrag;
                    ItemSlots[i].OnDragEvent += Drag;
                    ItemSlots[i].OnDropEvent += Drop;
                    ItemSlots[i].OnPointerEnterEvent += ShowTooltip;
                    ItemSlots[i].OnPointerExitEvent += HideTooltip;
                }
                CustomDebug.Log(ItemSlots.Count);
            }

        }
        
        #endregion


        #region Methods

        public bool AddItem(ItemData item)
        {
            bool canAddItem = false; 
            for (int i = 0; i < ItemSlots.Count; i++)
            {
                if (ItemSlots[i].Item == null)
                {
                    ItemSlots[i].Item = item;
                    canAddItem = true;
                    break;
                }
            }
            return canAddItem;
        }

        public bool RemoveItem(ItemData item)
        {
            bool canRemoveItem = false;
            for (int i = 0; i < ItemSlots.Count; i++)
            {
                if (ItemSlots[i].Item == item)
                {
                    ItemSlots[i].Item = null;
                    canRemoveItem = true;
                    break;
                }
            }
            return canRemoveItem;
        }

        public bool Contains(ItemData item)
        {
            bool isContain = false;
            for (int i = 0; i < ItemSlots.Count; i++)
            {
                if (ItemSlots[i].Item == item)
                {
                    isContain = true;
                    break;
                }
            }
            return isContain;
        }

        public bool IsFull()
        {
            bool isFull = true;
            for (int i = 0; i < ItemSlots.Count; i++)
            {
                if (ItemSlots[i].Item == null)
                {
                    isFull = false;
                    break;
                }
            }
            return isFull;
        }

        private void BeginDrag(ItemSlot itemSlot)
        {
           if (itemSlot.Item != null)
            {
                _draggedSlot = itemSlot;
                _draggableItem.sprite = itemSlot.Item.Icon;
                _draggableItem.transform.position = Input.mousePosition;
                _draggableItem.enabled = true;
            }
        }

        private void EndDrag(ItemSlot itemSlot)
        {
           _draggedSlot = null;
            _draggableItem.enabled = false;
        }

        private void Drag(ItemSlot itemSlot)
        {
            if (_draggableItem.enabled)
            {
                _draggableItem.transform.position = Input.mousePosition;
            }
        }

        private void Drop(ItemSlot dropItemSlot)
        {
           if (_draggedSlot == null) return;

            ItemData draggedItem = _draggedSlot.Item;
            bool isSomethingCrafted = false;

            foreach (ItemRecipe itemRecipe in CraftableItemsList)
            {
                if (itemRecipe.CanCraft(_draggedSlot.Item, dropItemSlot.Item))
                {
                    if(dropItemSlot.Item.IsDestructuble == false)
                    {
                        _draggedSlot.Item = itemRecipe.Craft(this);
                        isSomethingCrafted = true;
                    }
                    else
                    {
                        dropItemSlot.Item = itemRecipe.Craft(this);
                        isSomethingCrafted = true;
                    }                  
                    break;
                }
            }

            if(isSomethingCrafted == false)
            {
                _draggedSlot.Item = dropItemSlot.Item;
                dropItemSlot.Item = draggedItem;    
            }
        }

        private void ShowTooltip(ItemSlot itemSlot)
        {
            ItemData item = itemSlot.Item as ItemData;
            if (item != null)
            {
                _inventoryTooltip.ShowTooltip(item);
            }
        }

        private void HideTooltip(ItemSlot itemSlot)
        {
            _inventoryTooltip.HideTooltip();
        }
       
        //public ItemData GetItem(ItemData value)
        //{
        //    if (Items.Contains(value))
        //    {
        //        Items.Remove(value);
        //        return value;
        //    }

        //    return null;
        //}

        #endregion
    }
}

