using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Nightmare
{
    public class ShopHandler : MonoBehaviour
    {
        
        public float openTime = 5.0f;
        public GameObject shopItemUI;

        public ShopItem[] shopItems;

        private Canvas shopCanvas;
        private float timer = 0.0f;
        private bool isShopOpen;
        private TextMeshPro ShopText;
        private PlayerCustomer playerCustomer;
        private Transform content;

        // Start is called before the first frame update
        void Awake()
        {
            Debug.Log("ShopHandler awake");
            shopCanvas = GetComponentInChildren<Canvas>();
            shopCanvas.gameObject.SetActive(false);
            isShopOpen = true;
            ShopText = GetComponentInChildren<TextMeshPro>();
            playerCustomer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCustomer>();
            content = shopCanvas.transform.GetComponentInChildren<ScrollRect>().content;
            foreach (ShopItem item in shopItems)
            {
                GameObject shopItem = Instantiate(shopItemUI, content.GameObject().transform) as GameObject;
                shopItem.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = item.name;
                shopItem.transform.Find("Price").GetComponent<TextMeshProUGUI>().text = item.price.ToString();
                shopItem.transform.Find("ImagePanel/Image").GetComponent<UnityEngine.UI.Image>().sprite = item.sprite;
                shopItem.GetComponentInChildren<Button>().onClick.AddListener(delegate { BuyItem(item.price, item.prefab); });
            }
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

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                playerCustomer.setNearShop(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                playerCustomer.setNearShop(false);
                HideShop();
            }
        }

        private void UpdateButton()
        {
            Debug.Log("Updating button");
            for (int i = 0; i < shopItems.Length; i++)
            {
                int price = shopItems[i].price;
                Button button = content.GetChild(i).GetComponentInChildren<Button>();
                Debug.Log(button);
                if (CoinManager.coins < price)
                {
                    button.interactable = false;
                }
                else
                {
                    button.interactable = true;
                }
            }
        }

        public void ShowShop()
        {
            UpdateButton();
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
            playerCustomer.setNearShop(false);
            ShopText.text = "Shop Closed";
        }

        public void BuyItem(int price, GameObject obj)
        {
            if (CoinManager.coins >= price)
            {
                CoinManager.coins -= price;
                Vector3 pos = transform.forward * 2 + transform.position;
                Instantiate(obj, pos, Quaternion.identity);
                UpdateButton();
            }
        }
        

        private void OnDestroy()
        {
            EventManager.TriggerEvent("Pause", false);
        }


    }
}

[System.Serializable]
public class ShopItem{
    public string name;
    public int price;
    public GameObject prefab;
    public Sprite sprite;
    public ShopItem(string name, int price, GameObject prefab, Sprite sprite)
    {
        this.name = name;
        this.price = price;
        this.prefab = prefab;
        this.sprite = sprite;
    }
}

