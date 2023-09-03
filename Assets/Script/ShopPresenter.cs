using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Rov.InventorySystem
{
    public class ShopPresenter : MonoBehaviour
    {
        int currentItemIndex;
        int currentCategoryIndex;

        int maxShownItemCount;
        int maxCategoryCount = 3;
        int pageSize = 10;

        [SerializeField] UIShop ui;
        [SerializeField] ShopInventory inventory;
        
        //This list tells the UI what name and icon to set for each category.
        [SerializeField] List<CategoryInfo> categoryInfoList = new List<CategoryInfo>();

        void Start()
        {
            RefreshUI();
        }

        void Update()
        {
            // if (Input.GetKeyDown(KeyCode.A))
            // {
            //     PrevCategory();
            // }
            // else if (Input.GetKeyDown(KeyCode.D))
            // {
            //     NextCategory();
            // }
            if (Input.GetKeyDown(KeyCode.W))
            {
                PrevItem();
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                NextItem();
            }
        }

        public void PhysicCategory()
        {   
            currentCategoryIndex =0;
            currentItemIndex = 0;
            RefreshUI();
        }

       public void MagicCategory()
        {
            currentCategoryIndex =1;
            currentItemIndex = 0;
            RefreshUI();
        }
       public void DefenseCategory()
        {
            currentCategoryIndex=2;
            currentItemIndex = 0;
            RefreshUI();
        }


        void PrevItem()
        {
            if (currentItemIndex <= 0)
                return;

            currentItemIndex--;
            RefreshUI();
        }

        void NextItem()
        {
            if (currentItemIndex >= maxShownItemCount -1)
                return;
            
            currentItemIndex++;
            RefreshUI();
        }

        [ContextMenu(nameof(RefreshUI))]
        void RefreshUI()
        {
            var currentCategoryInfo = categoryInfoList[currentCategoryIndex];
            //ui.SetCategory(currentCategoryInfo);

            //From our int 'currentCategoryIndex', cast it into 'ItemType'.
            var currentCategory = (ItemType)currentCategoryIndex;
            
            //Get only items that matched current category.
            var itemsToDisplay = inventory.GetItemsByType(currentCategory);
            maxShownItemCount = itemsToDisplay.Length;
            
            //Clear everything and cancel if there are no items with the current category.
            if (maxShownItemCount <= 0)
            {
                ui.ClearAllItemUIs();
                return;
            }
            
            //Current item is retrieved from itemsToDisplay using 'currentItemIndex'
            var currentItem = itemsToDisplay[currentItemIndex];
            ui.SetCurrentItemInfo(currentItem);

            //This will hold list of UIItem_Data for the display of UIItem
            var uiDataList = new List<UIItem_Data>();

            //First find out what page we are in. 
            var currentPageIndex = currentItemIndex / pageSize;
            
            //Then find range of index that we want to draw.
            var startIndexToDisplay = currentPageIndex * pageSize;
            var endIndexToDisplay = startIndexToDisplay + pageSize;
            
            var i = 0;
            foreach (var item in itemsToDisplay)
            {
                //Select only item within start and end index and add to list.
                if (i >= startIndexToDisplay && i < endIndexToDisplay)
                {
                    uiDataList.Add(new UIItem_Data(item));
                }
              
                i++;
            }
            
            //Draw the results! Convert to array to prevent the results from being changed accidentally.
            ui.SetItemList(uiDataList.ToArray());
        }
    }
}

