using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nightmare
{
    public class PlayerCustomer : MonoBehaviour
    {
        bool isNearShop = false;
        ShopHandler shopHandler;

        private void Awake()
        {
            shopHandler = FindObjectOfType<ShopHandler>();
        }

        private void Update()
        {
            if (isNearShop && shopHandler)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    shopHandler.ShowShop();
                }
            }
        }

        public void setNearShop(bool value)
        {
            isNearShop = value;
        }
    }
}
