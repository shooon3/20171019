using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveni : MonoBehaviour {

    /*銀行ごとのクラス*/

    PlayerController player;

    BankManager bankmanager; // 銀行管理クラス
    GUIManager guimanager;
    GameObject bankmanagerObj; // 銀行管理クラスのオブジェクト

    public GameObject thisConveniObj; // 自分の銀行のオブジェクト

    public GameObject raidPlayerObj; // 襲撃したプレイヤーのオブジェクト

    public string thisConveniId; // 自分のID
    [SerializeField] private int haveMoney; // 所持金
    public bool attacked = false; // 襲撃されたか
    public bool raid = false; // 襲撃フラグ
    public static bool raidFlg = false; // 襲撃フラグ（銀行管理クラス用
    [SerializeField] private int getMoney; // 奪われる金額

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
