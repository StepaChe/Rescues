using UnityEngine;
using UnityEngine.UI;


namespace Rescues
{
    public class InventoryTooltip : MonoBehaviour
    {
        #region Fields

        [SerializeField] Text ItemNameText;
        [SerializeField] Text ItemDescriptionText;

        #endregion


        #region Methods

        public void ShowTooltip(ItemData item)
        {
            ItemNameText.text = item.Name;
            ItemDescriptionText.text = item.Description;
            gameObject.SetActive(true);
        }

        public void HideTooltip()
        {
            gameObject.SetActive(false);
        }

        #endregion
    }
}
