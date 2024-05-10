using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Nightmare
{
    public class WarningManager : MonoBehaviour
    {
        Animator anim;

        private UnityEvent listener;

        void Awake ()
        {
            anim = GetComponent <Animator> ();
            EventManager.StartListening("ShopWarning", ShowShopWarning);
        }

        void OnDestroy()
        {
            EventManager.StopListening("ShopWarning", ShowShopWarning);
        }

        void ShowShopWarning()
        {
            anim.SetTrigger("ShopWarning");
        }
    }
}