using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public enum Action // プレイヤーの行動
    {
        NONE = 0, // 通常状態
        BANKRAID = 1, // 銀行襲撃
        TAKEITEM = 2, // アイテム取得
        ENERGYCHARGE = 3 // エネルギー充電
    }

    public Animator animetor;
    public CharacterController cc;

    public string playerName; // プレイヤー名
    public int playerId; // プレイヤーID
    public PhotonView myPhotonView;
    public PhotonTransformView myPhotonTransView;

    [SerializeField] private int playerType = 0;// プレイヤーの属性 1で市民、2でテロリスト
    [SerializeField] private int charactorHp = 0; // プレイヤーのHP
    [SerializeField] private int charactorMaxHp = 0; // 最大HP
    [SerializeField] private int charactorAtk = 0; // 攻撃力
    [SerializeField] private float charactorSpeed = 0.0f; // 移動速度
    [SerializeField] private float charactorMoney; // 所持金
    [SerializeField] private float jumpSpeed = 10.0f; // ジャンプ移動量
    [SerializeField] private float runSpeed = 0.0f; // ダッシュ時のスピード
    [SerializeField] private bool dashFlg; // ダッシュしているか
    [SerializeField] private MouseManager mousemanager; // マウス移動のデータ


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
