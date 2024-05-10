using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;
public class CoinDropManager : MonoBehaviour
{
    private static int coinValueCache;
    private static int goldBarValueCache;
    private static int diamondValueCache;

    // Start is called before the first frame update

    private static CoinDropManager coinDropManager;

    public static CoinDropManager instance
    {
        get
        {
            if (!coinDropManager)
            {
                coinDropManager = FindObjectOfType(typeof(CoinDropManager)) as CoinDropManager;

                if (!coinDropManager)
                {
                    Debug.LogError("There needs to be one active CoinDropManager script on a GameObject in your scene.");
                }
            }

            return coinDropManager;
        }
    }

    void Start()
    {
        coinValueCache = PoolManager.GetRef("Coin").GetComponent<Item>().value;
        goldBarValueCache = PoolManager.GetRef("GoldBar").GetComponent<Item>().value;
        diamondValueCache = PoolManager.GetRef("Diamond").GetComponent<Item>().value;
    }
    public static void DropMoney(Vector3 pos, int value)
    {
        int diamondCount = value / diamondValueCache;
        value -= diamondCount * diamondValueCache;
        int goldBarCount = value / goldBarValueCache;
        value -= goldBarCount * goldBarValueCache;
        int coinCount = value / coinValueCache;
        Debug.Log("Dropping " + diamondCount + " diamonds, " + goldBarCount + " gold bars, and " + coinCount + " coins.");
        for (int i = 0; i < diamondCount; i++)
        {
            Spawn("Diamond", pos);
        }
        for (int i = 0; i < goldBarCount; i++)
        {
            Spawn("GoldBar", pos);
        }
        for (int i = 0; i < coinCount; i++)
        {
            Spawn("Coin", pos);
        }
    }

    static void Spawn(string objname, Vector3 pos)
    {
        pos.y += 1;
        GameObject c = PoolManager.Pull(objname, pos, Quaternion.identity);
        Vector3 force = new Vector3(Random.Range(-1.0f, 1.0f), 1, Random.Range(-1.0f, 1.0f));
        force.Normalize();
        force *= 3;
        c.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
    }
}
