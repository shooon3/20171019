using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankManager : MonoBehaviour {

    Bank bank;
    public GameObject[] bankObjects;

    const int BANKID_SIZE = 5;
    public int[] bankID;
    public int[] bankIdEathMoney = new int[BANKID_SIZE];
    public int[] bankMoney = { 1000, 3000, 5000, 2000, 1000, 3000, 1000 };
    public int[] bankHighMoney = { 10000, 15000, 12000, 20000, 20000, 30000, 50000 };
    private int[] bankDefence = { 5, 6, 7, 10, 12, 15, 20 };

    public static bool raidFlg = false;

    // Use this for initialization
    void Start()
    {
        bankObjects = GameObject.FindGameObjectsWithTag("Bank");
        int i = bankObjects.Length;
        bankID = new int[i];
        bankCheck();
        for (int j = 0; j < bankID.Length; j++)
        {
           bank = GameObject.Find("bankObj").GetComponent<Bank>();
           bankID[j] = int.Parse(bank.thisBankId);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public int PostMoney(int id)
    {
      int money;
      money = bankMoney[Random.Range(0, 6)];
      return money;
    }

    public int PostDefence(int def)
    {
      def = bankDefence[Random.Range(0, 6)];
      return def;
    }

    void bankCheck()
    {

    }

    public void RaidCheck(int id)
    {
        foreach (int i in bankID)
        {
            if (i == id && bank.attacked ==false)
            {
                bank.raid = true;
            }
            else
            {
                Debug.Log("既に襲撃されています");
            }
        }
    }

    public int Raid(int money)
    {
        int getMoney;
        getMoney = money;
        bank.attacked = true;
        Debug.Log("Raid関数呼び出し");
        return getMoney;
    }

    }