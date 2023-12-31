using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Rov.InventorySystem
{
    public class UIShop : MonoBehaviour
    {
        // #region Category
        // [Header("Category")]
        // [SerializeField] Image categoryIconImage;
        // [SerializeField] Text categoryText;
        // #endregion

        public float fadeTime = 1f;
        #region CurrentItem
        [Header("Current Item")]
        [SerializeField] Image currentItemIconImage;
        [SerializeField] TMP_Text ItemNameText;
        [SerializeField] TMP_Text descriptionText;
        #endregion

        #region Item List
        [Header("Item List")] 
        [SerializeField] UIItem itemUIPrefab;
        [SerializeField] public List<UIItem> itemUIList = new List<UIItem>();
        #endregion

        void Start()
        {
            //Make sure to hide original blueprint of UIItem at the start.
            itemUIPrefab.gameObject.SetActive(false);
        }

        // public void SetCategory(CategoryInfo info)
        // {
        //     categoryIconImage.sprite = info.icon;
        //     categoryText.text = info.name;
        // }
        
        public void SetCurrentItemInfo(ItemData data)
        {
            ItemNameText.text = data.displayName;
            descriptionText.text = data.description;
            currentItemIconImage.sprite = data.icon;
        }
        public void SetItemList(UIItem_Data[] uiDatas)
        {
            //Clear and destroy created UIs first, before creating new ones.
            ClearAllItemUIs();
            foreach (var uiItemData in uiDatas)
            {
                
                //When creating a new UI, ALWAYS put it inside Canvas. and pass false for 'worldPositionStays'
                //This is because all UIs are always in Screen Space not World Space.
                var newItemUI = Instantiate(itemUIPrefab,itemUIPrefab.transform.parent,false);
                
                newItemUI.transform.localScale = Vector3.zero;
                newItemUI.transform.DOScale(1f, fadeTime).SetEase(Ease.OutBounce);

                //Don't forget to enable it. Because the original UIItem was disabled from Start()
                newItemUI.gameObject.SetActive(true);
                itemUIList.Add(newItemUI);
                newItemUI.SetData(uiItemData);
                newItemUI.name = uiItemData.itemData.displayName;
        
            }
            
        }
        
        //Destroy all created UIItem and then clear the list.
        public void ClearAllItemUIs()
        {
            foreach (UIItem uiItem in itemUIList)
                Destroy(uiItem.gameObject);
            
            itemUIList.Clear();
        }
    }

    [Serializable]
    public class CategoryInfo
    {
        public string name;
    }
}