using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Rov.InventorySystem
{
    public class ShopPresenter : MonoBehaviour
    {
        #region private num
        int currentItemIndex;
        int curentCategoryIndex;

        int maxShownItemCount;
        int maxCategoryCount = 3;
        int pageSize = 6;
        #endregion

        #region SerializeField
        [SerializeField] UIShop ui;
        [SerializeField] ShopInventory inventory;
        [SerializeField] List<CategoryInfo> categoryInfoList = new List<CategoryInfo>();
        #endregion

        void Start()
        {
            RefreshUI();
        }
        void Update()
        {

        }
        [ContextMenu(nameof(RefreshUI))]
        void RefreshUI()
        {
            var currentCategoryInfo = categoryInfoList[currentItemIndex];
            // ui.SetCategory(currentCategoryInfo);

            var currentCategory = (ItemType)curentCategoryIndex;

            var itemsToDisplay = inventory.GetItemsByType(currentCategory);
            maxShownItemCount = itemsToDisplay.Length;

            if(maxShownItemCount <= 0 )
            {
                ui.ClearAllItemUIs();
                return;
            }

            var currentItem = itemsToDisplay[currentItemIndex];
            ui.SetCurrentItemInfo(currentItem);

            var uiDataList = new List<UIItem_Data>();
            var currentPageIndex = currentItemIndex/pageSize;
            var startIndexToDisplay = currentPageIndex * pageSize;
            var endIndexToDisplay = startIndexToDisplay + pageSize;

            var i =0;
            foreach(var item in itemsToDisplay)
            {
                if(i >= startIndexToDisplay && i< endIndexToDisplay)
                {
                    uiDataList.Add(new UIItem_Data(item,currentItem == item));
                }

                i++;
            }

            ui.SetItemList(uiDataList.ToArray());

        }
    }  
}

