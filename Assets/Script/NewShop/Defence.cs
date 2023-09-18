using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rov.InventorySystem
{
  public class Defence : CategoryAb
  {
    [Header("Category")]
    [SerializeField] GameObject DefenceItem; 
    [SerializeField] int categoryIndexOfDefenseItem = 0;
    [SerializeField] ShopPresenter shopPresenter;
    
    public override void IndexOfCategory(GameObject Category) 
    {
      if(Category.name == DefenceItem.name)
      {
        shopPresenter.currentCategoryIndex = categoryIndexOfDefenseItem;
        shopPresenter.RefreshUI();
      }
    }
  }
}
