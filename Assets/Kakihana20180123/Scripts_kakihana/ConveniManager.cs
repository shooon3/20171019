using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveniManager : MonoBehaviour {

    /*コンビニの管理メソッド*/

    Conveni conveni; // 銀行ごとのクラス
    GUIManager guimanager;
    public GameObject[] conveniObjects; // 銀行ごとのオブジェクト

    const int CONVENI_SIZE = 5; // 銀行オブジェクトの最大数
    public int[] conveniID; // 銀行ごとのID
    public int[] conveniIdEathMoney = new int[CONVENI_SIZE]; // IDごとの所持金数
    public int[] conveniMoney = { 1000, 2000, 500, 1000, 2000, 1000, 500 }; // 所持金
    private int[] bankDefence = { 5, 6, 7, 10, 12, 15, 20 };

    public static bool raidFlg = false; // 襲撃されたか

    float showTimeLimit = 10.0f;
    public float timeCount = 0f;

    public bool showFlg = true;

    // Use this for initialization
    void Start()
    {
        conveniObjects = GameObject.FindGameObjectsWithTag("Conveni"); // タグを元にオブジェクトを格納
        guimanager = GameObject.Find("Status").GetComponent<GUIManager>();
        int i = conveniObjects.Length; // Bankタグのオブジェクトの最大数を取得
        conveniID = new int[i]; // IDの大きさをタグのオブジェクトの最大数に
        for (int j = 0; j < conveniID.Length; j++)
        {
            //銀行のオブジェクトの数だけBankクラスのコンポーネントを取得し、銀行ごとにIDをつける
            conveni = GameObject.Find("ConveniObj").GetComponent<Conveni>();
            conveniID[j] = int.Parse(conveni.thisConveniId);
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
        money = conveniMoney[Random.Range(0, 6)];
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
        foreach (int i in conveniID) // 全銀行のIDを取り出す
        {
            if (i == id && conveni.attacked == false) // IDが一致していてかつ一度も襲撃されていなかったら
            {
                conveni.raid = true; // 銀行を襲撃する
            }
            else if (showFlg == true)
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
        conveni.attacked = true;
        return getMoney;
    }

}
