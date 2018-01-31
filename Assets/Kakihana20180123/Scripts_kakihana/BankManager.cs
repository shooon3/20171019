using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankManager : MonoBehaviour {

    /*銀行の管理メソッド*/

    Bank bank; // 銀行ごとのクラス
    GUIManager guimanager;
    public GameObject[] bankObjects; // 銀行ごとのオブジェクト

    const int BANKID_SIZE = 5; // 銀行オブジェクトの最大数
    public int[] bankID; // 銀行ごとのID
    public int[] bankIdEathMoney = new int[BANKID_SIZE]; // IDごとの所持金数
    public int[] bankMoney = { 1000, 3000, 5000, 2000, 1000, 3000, 1000 }; // 所持金
    private int[] bankDefence = { 5, 6, 7, 10, 12, 15, 20 };

    public static bool raidFlg = false; // 襲撃されたか

    float showTimeLimit = 10.0f;
   public float timeCount = 0f;

   public bool showFlg = true;

    // Use this for initialization
    void Start()
    {
        bankObjects = GameObject.FindGameObjectsWithTag("Bank"); // タグを元にオブジェクトを格納
        guimanager = GameObject.Find("Status").GetComponent<GUIManager>();
        int i = bankObjects.Length; // Bankタグのオブジェクトの最大数を取得
        bankID = new int[i]; // IDの大きさをタグのオブジェクトの最大数に
        for (int j = 0; j < bankID.Length; j++)
        {
           //銀行のオブジェクトの数だけBankクラスのコンポーネントを取得し、銀行ごとにIDをつける
           bank = GameObject.Find("bankObj").GetComponent<Bank>();
           bankID[j] = int.Parse(bank.thisBankId);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;
        if (timeCount >= showTimeLimit)
        {
            showFlg = true;
            timeCount = 0;
        }
    }

    public int PostMoney(int id) // 銀行のお金振り分けメソッド
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

    public void RaidCheck(int id) // 襲撃確認メソッド
    {
        foreach (int i in bankID) // 全銀行のIDを取り出す
        {
            if (i == id && bank.attacked ==false) // IDが一致していてかつ一度も襲撃されていなかったら
            {
                bank.raid = true; // 銀行を襲撃する
            }
            else if(showFlg == true)
            {
                // 一度でも襲撃されていたら襲撃しない
                guimanager.LogShow((int)GUIManager.SenderList.SYSTEM, 0, (int)GUIManager.SenderList.SYSTEM, 3);
                showFlg = false;
            }
        }
    }

    public int Raid(int money)
    {
        int getMoney;
        getMoney = money;
        bank.attacked = true;
        return getMoney;
    }

    }