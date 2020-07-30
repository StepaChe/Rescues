using UnityEngine;


namespace Rescues
{
    [CreateAssetMenu(fileName = "ItemRecipe", menuName = "Data/ItemRecipe")]
    public sealed class ItemRecipe : ScriptableObject
    {
        #region Fields

        public ItemData ItemA;
        public ItemData ItemB;
        public ItemData ItemResult;

        #endregion


        #region Methods

        public bool CanCraft(ItemData itemA, ItemData itemB)
        {
            bool canCraft = false;
            if (ItemA == itemA && ItemB == itemB)
            {
                canCraft = true;                
            }
            else if(ItemA == itemB && ItemB == itemA)
            {
                canCraft = true;
            }
            return canCraft;
        }

        public ItemData Craft(Inventory inventory)
        {
            if(ItemA.IsDestructuble) inventory.RemoveItem(ItemA);
            if(ItemB.IsDestructuble) inventory.RemoveItem(ItemB);                      
            return ItemResult;
        }

        #endregion
    }
}
