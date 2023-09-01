using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rov.InventorySystem
{
    public class UIItem : MonoBehaviour
    {
        [SerializeField] TMP_Text itemNameText;
        [SerializeField] TMP_Text PriceText;
        [SerializeField] Image IconImage;

        public void SetData(UIItem_Data data)
        {
            IconImage.sprite = data.itemData.icon;
            itemNameText.text = data.itemData.displayName;
            PriceText.text = "X " +  data.itemData.price;
        }
    }

    //Create a DTO class that hold information of the item and also tell is the item is selected.
    public class UIItem_Data
    {
        public ItemData itemData;

        public UIItem_Data(ItemData itemData)
        {
            this.itemData = itemData;
        }
    }
}