using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    // プレイヤークラス
    [SerializeField]
    private int playerGroup = 0; // プレイヤーの属性 0で市民（NPC）1で市民（プレイヤー）2でテロリスト（プレイヤー）

    const string statusName = "Status"; // ステータスオブジェクトの名前
    private GameObject statusObj; // ステータスオブジェクトを格納する変数

    CharactorStatus charactorstatus; // 各種キャラクターデータを参照するクラス

   public int charactorHp = 0; // 自キャラのHP
   public int charactorMaxHp = 0; // 自キャラの最大HP
   public int charactorAtk = 0; // 自キャラの攻撃力
   public float charactorSpeed = 0.0f; // 自キャラの移動速度

	// Use this for initialization
	void Start () {

        // ステータスオブジェクトの名前を参照し格納、CharactorStatusコンポーネントを取得
        statusObj = GameObject.Find(statusName);
        charactorstatus = statusObj.GetComponent<CharactorStatus>();

        // playerGroupの数値によって各種キャラクターのデータを取得
        switch (playerGroup)
        {
            case 0:
                setcitizenStatus(); // 市民（NPC）用データ取得メソッドを起動
                break;
            case 1:
                setplayerStatus(); // 市民（プレイヤー）用データ取得メソッドを起動
                break;
            case 2:
                setterroristStatus(); // テロリスト（プレイヤー）用データ取得メソッドを起動
                break;
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void setcitizenStatus() // 市民（NPC）データ取得メソッド、各種データを取得し変数に格納
    {
        
    }
    void setplayerStatus() // 市民（プレイヤー）用データ取得メソッド
    {
        charactorHp = charactorstatus.postplayerHp();
        charactorMaxHp = charactorstatus.postplayerMaxHp();
        charactorAtk = charactorstatus.postplayerAtk();
        charactorSpeed = charactorstatus.postplayerSpeed();
    }
    void setterroristStatus() // テロリスト（プレイヤー）用データ取得メソッド
    {
        charactorHp = charactorstatus.postterroristHp();
        charactorMaxHp = charactorstatus.postterroristMaxHp();
        charactorAtk = charactorstatus.postterroristAtk();
        charactorSpeed = charactorstatus.postterroristSpeed();
    }
}
