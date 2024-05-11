using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nightmare
{
    public class PlayerCustomer : MonoBehaviour
    {
        bool isNearShop = false;
        ShopHandler shopHandler;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isNearShop && shopHandler)
                {
                    shopHandler.ShowShop();
                } else {
                    EventManager.TriggerEvent("ShopWarning");
                
                }
            }
        }

        public void setNearShop(bool value)
        {
            isNearShop = value;
            shopHandler = FindObjectOfType<ShopHandler>();
        }
    }
}
