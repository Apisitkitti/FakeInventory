using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Rov.InventorySystem
{
  public class Swtich : MonoBehaviour
  {
    [SerializeField] GameObject Scene_Old;
    [SerializeField] GameObject Scene_New;
    [SerializeField] ShopPresenter shopPresenter;
    public void OldShop()
    {
      Scene_Old.SetActive(true);
      shopPresenter.RefreshUI();
      Scene_New.SetActive(false);
    }
    public void NewShop()
    {
      Scene_Old.SetActive(false);
      Scene_New.SetActive(true);
      shopPresenter.RefreshUI();
    }
  }
}
