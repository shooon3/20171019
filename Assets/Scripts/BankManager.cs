using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankManager : MonoBehaviour {

    Bank bank;

    const int BANKID_SIZE = 5;
    public int[] bankID;
    public int[] bankMoney = { 1000, 3000, 5000, 2000, 1000, 3000, 1000 };
    public int[] bankHighMoney = { 10000, 15000, 12000, 20000, 20000, 30000, 50000 };
    private int[] bankDefence = { 5, 6, 7, 10, 12, 15, 20 };

    public static bool raidFlg = false;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < bankID.Length; i++)
        {
           bank = GameObject.FindGameObjectWithTag("Bank").GetComponent<Bank>();
           bankID[i] = int.Parse(bank.thisBankId);
        }
    }

    // Update is called once per frame
    void Update()
    {
    if (raidFlg == true)
    {

    }

    }

     int PostMoney(int money)
    {
      money = bankMoney[Random.Range(0, 6)];
      return money;
    }

    public int PostDefence(int def)
    {
      def = bankDefence[Random.Range(0, 6)];
      return def;
    }

    public int SetMoneyID(int id)
    {

    }

    }