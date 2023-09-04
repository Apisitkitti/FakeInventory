using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Rov.InventorySystem
{
    public class Wallet : MonoBehaviour
    {
        public TMP_Text MoneyWallet;
        public int PlayerMoney = 10000; // กำหนดค่าเริ่มต้นตามที่คุณต้องการ

        private void Start()
        {
            UpdateMoneyText();
        }

        public void UpdateMoneyText()
        {
            MoneyWallet.text = "Money: " + PlayerMoney.ToString();
        }
    }
}
