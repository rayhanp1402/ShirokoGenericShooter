using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{

    public static int coins;
    private Text cText;

    // Start is called before the first frame update
    void Awake ()
    {
        cText = GetComponent <Text> ();
        coins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        cText.text = coins.ToString();
    }
}
