using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveniManager : MonoBehaviour {

    /*コンビニの管理メソッド*/

    public Conveni[] conveni; // コンビニごとのクラス
    GUIManager guimanager;
    GameManagement gm;
    public GameObject[] conveniObjects; // コンビニごとのオブジェクト
    public GameObject[] conveniUniqueObj; // コンビニを識別するためのオブジェクト

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
        gm = GameObject.Find("Master").GetComponent<GameManagement>();
/*        conveniObjects = GameObject.FindGameObjectsWithTag("Conveni");*/ // タグを元にオブジェクトを格納
        guimanager = GameObject.Find("Status").GetComponent<GUIManager>();
        ConveniDataInit();
        //int i = conveniObjects.Length; // conveniタグのオブジェクトの最大数を取得
        //conveniID = new int[i]; // IDの大きさをタグのオブジェクトの最大数に
    }

    // Update is called once per frame
    void Update()
    {
        //timeCount += Time.deltaTime;
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

    public void ConveniDataInit()
    {
        int i = 0;
        for(i = 0; i < 3; i++)
        {
            switch (i)
            {
                case 0:
                    conveniUniqueObj[i] = GameObject.Find("Coveni1");
                    break;
                case 1:
                    conveniUniqueObj[i] = GameObject.Find("Coveni2");
                    break;
                case 2:
                    conveniUniqueObj[i] = GameObject.Find("Coveni3");
                    break;
            }
            conveniObjects[i] = conveniUniqueObj[i].transform.parent.gameObject;
            conveni[i] = conveniObjects[i].GetComponent<Conveni>();
            conveniID[i] = int.Parse(conveni[i].thisConveniId);
            conveniIdEathMoney[i] = conveni[i].haveMoney;
        }
    }

    public void RaidCheck(int id) // 襲撃確認メソッド
    {
        foreach (int i in conveniID) // 全銀行のIDを取り出す
        {
            if (i == id && conveni[i].attacked == false) // IDが一致していてかつ一度も襲撃されていなかったら
            {
                conveni[i].raid = true; // 銀行を襲撃する
            }
            else if (showFlg == true)
            {
                // 一度でも襲撃されていたら襲撃しない
                guimanager.LogShow((int)GUIManager.SenderList.SYSTEM, 0, (int)GUIManager.SenderList.SYSTEM, 3);
                showFlg = false;
            }
        }
    }

    public int Raid(int money,int id)
    {
        int getMoney;
        getMoney = money;
        conveni[id].attacked = true;
        return getMoney;
    }

}
