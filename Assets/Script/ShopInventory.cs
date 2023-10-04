using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UIElements;

namespace Rov.InventorySystem
{
    public class ShopInventory : MonoBehaviour
    {
        public IconData[] ItemIcon => itemIconlist.ToArray();
        [SerializeField] public List<IconData> itemIconlist = new List<IconData>();
        public ItemData[] Items => itemList.ToArray();
        [SerializeField] public List<ItemData> itemList = new List<ItemData>();

        public ItemData[] GetItemsByType(ItemType targetType)
        {
            var resultlist = new List<ItemData>();
            foreach(var ItemData in itemList)
            {
                if(ItemData.type == targetType)
                {
                  int number = 0;
                  foreach(var IconData in itemIconlist)
                  {
                    if(ItemData.displayName == IconData.displayName)
                    {
                      ItemData.icon = IconData.icon;
                    }
                    number++;
                  }
                  resultlist.Add(ItemData); 
                }
                
            }
              return resultlist.ToArray();  
           
        }
        
    }
}

    [Serializable]
    public class ItemData
    {
        public string displayName;
        public string description;
        public ItemType type; 
        public int price;
        public Sprite icon;
    }
    [Serializable]
    public class IconData
    {
       public Sprite icon;
       public string displayName;
    }

    public enum ItemType
    {
        Physic_attack,
        Magic_Attack,
        Defense
    }