using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UIElements;

namespace Rov.InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        public ItemData[] Items => itemList.ToArray();
        [SerializeField] List<ItemData> itemList = new List<ItemData>();

        public ItemData[] GetItemsByType(ItemType targetType)
        {
            var resultlist = new List<ItemData>();
            foreach(var ItemData in itemList)
            {
                if(ItemData.type == targetType)
                {
                    if(ItemData.type == targetType)
                        resultlist.Add(ItemData);
                    
                } 
            }
            return resultlist.ToArray();
        }
        public void Add(ItemData itemToAdd)
        {
            
        }

        public void Remove(ItemData itemToRemove)
        {
            
        }
    }
}

    [Serializable]
    public class ItemData
    {
        public string displayName;
        public string description;
        public Sprite icon;
        public ItemType type; 
        public int count;
    }

    public enum ItemType
    {
        Physic_attack,
        Magic_Attack,
        Defense,
        Movement,
        Jungling,
        Support
    }