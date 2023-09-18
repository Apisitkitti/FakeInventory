using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Rov.InventorySystem
{
  public class Magic : CategoryAb
  {
    [Header("Category")]
    [SerializeField] GameObject MagicItem; 
    [SerializeField] int categoryIndexOfMagicItem = 0;
    [SerializeField] ShopPresenter shopPresenter;
    
    public override void IndexOfCategory(GameObject Category) 
    {
      if(Category.name == MagicItem.name)
      {
        shopPresenter.currentCategoryIndex = categoryIndexOfMagicItem;
        shopPresenter.RefreshUI();
      }
    }
  }
}
