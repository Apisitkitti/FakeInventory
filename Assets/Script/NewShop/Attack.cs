using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rov.InventorySystem
{
  public class Attack : CategoryAb
  {
    [Header("Category")]
    [SerializeField] GameObject AttackItem; 
    [SerializeField] int categoryIndexOfAttackItem = 0;
    [SerializeField] ShopPresenter shopPresenter;
    
    public override void IndexOfCategory(GameObject Category) 
    {
      if(Category.name == AttackItem.name)
      {
        shopPresenter.currentCategoryIndex = categoryIndexOfAttackItem;
        shopPresenter.RefreshUI();
      }
    }
  }
}   
