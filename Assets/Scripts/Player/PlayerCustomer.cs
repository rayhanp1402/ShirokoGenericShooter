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
            if (isNearShop)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    shopHandler.ShowShop();
                }
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Shop")
            {
                Debug.Log("Near Shop");
                isNearShop = true;
                shopHandler = other.gameObject.GetComponent<ShopHandler>();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Shop")
            {
                Debug.Log("Exit Shop");
                isNearShop = false;
                shopHandler.HideShop();
                shopHandler = null;
            }
        }
    }
}
