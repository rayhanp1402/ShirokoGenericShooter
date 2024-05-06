using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Nightmare
{
    public class ShopHandler : MonoBehaviour
    {
        
        public float openTime = 5.0f;

        private Canvas shopCanvas;
        private float timer = 0.0f;
        private bool isShopOpen;

        private TextMeshPro ShopText;

        // Start is called before the first frame update
        void Start()
        {
            shopCanvas = GetComponentInChildren<Canvas>();
            shopCanvas.gameObject.SetActive(false);
            isShopOpen = true;
            ShopText = GetComponentInChildren<TextMeshPro>();
        }

        // Update is called once per frame
        void Update()
        {
            if (isShopOpen) {
                if (Input.GetKeyDown(KeyCode.Escape) && shopCanvas.gameObject.activeSelf)
                {
                    HideShop();
                }
                if (timer < openTime)
                {
                    timer += Time.deltaTime;
                    ShopText.text = "Open time: " + (openTime - timer).ToString("F0") + "s";
                    if (timer >= openTime)
                    {
                        CloseShop();
                    }
                }
            }

        }

        // void OnTriggerEnter(Collider other)
        // {
        //     if (other.gameObject.tag == "Player")
        //     {
        //         Debug.Log("Player near shop");
        //     }
        // }

        // void OnTriggerExit(Collider other)
        // {
        //     if (other.gameObject.tag == "Player")
        //     {
        //         Debug.Log("Player left shop");
        //     }
        // }

        public void ShowShop()
        {
            shopCanvas.gameObject.SetActive(true);
            EventManager.TriggerEvent("Pause", true);
        }

        public void HideShop()
        {
            shopCanvas.gameObject.SetActive(false);
            EventManager.TriggerEvent("Pause", false);
        }

        public void CloseShop()
        {
            HideShop();
            Collider collider = GetComponent<Collider>();
            collider.enabled = false;
            FadeOutDestroy shopKeeper = transform.Find("Shopkeeper").GetComponent<FadeOutDestroy>();
            shopKeeper.StartFadeOut();
            isShopOpen = false;
            ShopText.text = "Shop Closed";
        }

    }
}