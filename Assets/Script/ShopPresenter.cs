using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;
using System.IO;


namespace Rov.InventorySystem
{
    public class ShopPresenter : MonoBehaviour
    {
        int currentItemIndex;
        public int currentCategoryIndex;
        [Header("Online-Save")]
        [SerializeField] string mysave;
        [SerializeField] string loading;
      
        int maxShownItemCount;
        int maxCategoryCount = 3;
        int pageSize = 10;
       // int PlayerMoney = 10000;
        [SerializeField] Wallet wallet;
        [SerializeField] UIShop ui;
        [SerializeField] ShopInventory shop;
        [SerializeField] ShopInventory inventory;
        
        
        //This list tells the UI what name and icon to set for each category.
        [SerializeField] List<CategoryInfo> categoryInfoList = new List<CategoryInfo>();
        [SerializeField] List<CategoryAb> category;

        void Awake()
        {
          LoadScoreFromGoogleDrive();
        }
        
        public void ClickPreviousItem()
        {
            PrevItem();
        }
        public void ClickNextItem()
        {
            NextItem();
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
        public void CategoryButton(GameObject CategoryButton)
        {
          for(int i = 0; i<category.Count; i++)
          {
            category[i].IndexOfCategory(CategoryButton);
          }
          
        }
        void PrevItem()
        {
            if (currentItemIndex <= 0)
                return;
            
            currentItemIndex--;
            Debug.Log("Prev Item");
            RefreshUI();
        }

        void NextItem()
        {
            if (currentItemIndex >= maxShownItemCount -1)
                return;
            
            currentItemIndex++;
            Debug.Log("Next Item");
            RefreshUI();
        }

        [ContextMenu(nameof(RefreshUI))]
        public void RefreshUI()
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
            
            var count = 0;
            foreach (var item in itemsToDisplay)
            {
                //Select only item within start and end index and add to list.
                if (count >= startIndexToDisplay && count < endIndexToDisplay)
                {
                    uiDataList.Add(new UIItem_Data(item));
                }
              
                count++;
            }
            
            //Draw the results! Convert to array to prevent the results from being changed accidentally.
            ui.SetItemList(uiDataList.ToArray());
        }

        [ContextMenu(nameof(SaveScoreData))]
        void SaveScoreData()
        {
            if (string.IsNullOrEmpty(mysave))
            {
                Debug.Log("NO SAVE!");
                return;
            }

            var scoreJson = JsonConvert.SerializeObject(shop.itemList, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }); ;
            var dataPath = Application.dataPath;
            var targetFilePath = Path.Combine(dataPath, mysave);

            var directoryPath = Path.GetDirectoryName(targetFilePath);
            if (Directory.Exists(directoryPath) == false)
                Directory.CreateDirectory(directoryPath);

            File.WriteAllText(targetFilePath, scoreJson);
        }


        IEnumerator LoadDataRoutine(string url)
        {
            var webRequest = UnityWebRequest.Get(url);
            //Get download progress
            var progress = webRequest.downloadProgress;
            Debug.Log(progress);

            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(webRequest.error);
            }
            else
            {
                var downloadedText = webRequest.downloadHandler.text;
                shop.itemList = JsonConvert.DeserializeObject<List<ItemData>>(downloadedText);
            }
            RefreshUI();
        }
         [ContextMenu(nameof(LoadScoreFromGoogleDrive))]
        void LoadScoreFromGoogleDrive()
        {
            StartCoroutine(LoadDataRoutine(loading));
        }

        public void CheckCurrentItem()
        {
            if(currentItemIndex >=  0)
            {
                
                PurChaseItem();
            }
        }
        public void  PurChaseItem()
        {  
                // ดึงข้อมูลไอเท็มปัจจุบัน
                var currentItem = inventory.GetItemsByType((ItemType)currentCategoryIndex)[currentItemIndex];
                // ตรวจสอบว่าผู้เล่นมีเงินเพียงพอในการซื้อไอเท็ม
                if (wallet.PlayerMoney  >= currentItem.price)
                {   
                    //BuyItem
                    wallet.PlayerMoney  -= currentItem.price;
                    Debug.Log("Purchase successful!");
                    Debug.Log("MONEY: " + wallet.PlayerMoney);
                    wallet.UpdateMoneyText();
                }
                else
                {
                    Debug.Log("Not enough money to purchase!");
                    
                }
            
        }
        //Pagination 5 items / 7 items / 10 items
        public void Page5item()
        {
            pageSize = 5;
            RefreshUI();
        }
        public void Page7item()
        {
            pageSize = 7;
            RefreshUI();
        }
        public void Page10item()
        {
            pageSize = 10;
            RefreshUI();
        }
        
    }
}

