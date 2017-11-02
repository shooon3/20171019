using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    /* プレイヤークラス */
    [SerializeField]
    private int playerGroup = 0; // プレイヤーの属性 0で市民（NPC）1で市民（プレイヤー）2でテロリスト（プレイヤー）

    const string statusName = "Status"; // ステータスオブジェクトの名前
    private GameObject statusObj; // ステータスオブジェクトを格納する変数
    public GameObject bullet;
    public GameObject fire;
    public GameObject punch;

    CharactorStatus charactorstatus; // 各種キャラクターデータを参照するクラス
    CharacterController cc;

    public int charactorHp = 0; // 自キャラのHP
    public int charactorMaxHp = 0; // 自キャラの最大HP
    public int charactorAtk = 0; // 自キャラの攻撃力
    public float charactorSpeed = 0.0f; // 自キャラの移動速度

    private float energy = 100.0f;

    public float jumpSpeed = 10.0f; // ジャンプ移動量
    private float runSpeed = 2.0f; // ダッシュ時の倍率

    public Vector3 charMove = new Vector3(0.0f, 0.0f, 0.0f); // キャラクター移動量
    private Vector3 mouseclickPos = new Vector3(0.0f, 0.0f, 0.0f);  
    const float gravity = 9.81f; // 重力

    public Sprite point;
    CamManager cm;

    // Use this for initialization
    void Start() {
        cc = GetComponent<CharacterController>(); // キャラクターコントローラーコンポーネントを取得
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
    void Update() {
        if (cc.isGrounded)
        {
            charMove = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));// キー入力から方向を取得
            charMove = transform.TransformDirection(charMove); // 移動方向をキャラクター移動量に設定
            if (Input.GetKey(KeyCode.LeftShift))
            {
                // 左シフトキーでダッシュ
                charMove *= charactorSpeed * runSpeed;
            }
            if (Input.GetButton("Jump"))
            {
                // スペースキーでジャンプ
                charMove.y = jumpSpeed;
            }
        }
        charMove.y -= gravity * Time.deltaTime; // 重力を計算
        cc.Move(charMove * Time.deltaTime); // キャラクター移動

        if (Input.GetButtonDown("Fire1") && playerGroup ==1)
        {
            PlayerAttack();
        }else if(Input.GetButtonDown("Fire1")&& playerGroup == 2)
        {
            mouseclickPos = Input.mousePosition;
            mouseclickPos.z = 5.0f;
            terroristAttack();
        }

        DebugTest();
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

    void DebugTest() // デバッグ用メソッド
    {
    }

    void PlayerAttack()
    {

    }

    void terroristAttack()
    {
        GameObject.Instantiate(bullet);
    }
}
