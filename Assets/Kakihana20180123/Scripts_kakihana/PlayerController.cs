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
    CharactorStatus charactorstatus; // 各種キャラクターデータを参照するクラス
    GUIManager guimanager;
    GameManagement gm;

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
    [SerializeField] private float runSpeed; // ダッシュ時のスピード
    [SerializeField] private bool dashFlg; // ダッシュしているか
    private float originSpeed = 0.0f;
   // [SerializeField] private MouseManager mousemanager; // マウス移動のデータ


    [SerializeField] public int energy = 100; // スタンガン発射に必要なエネルギー
    [SerializeField] public int maxEnergy = 100;
    [SerializeField] private float bulletTimeInterval = 0.0f; // スタンガン発射間隔
    [SerializeField] private float punchTimeInterval = 0.0f; // 素手攻撃の間隔
    [SerializeField] public float rotSpeed = 3.0f;

    [SerializeField] private Camera playerCam; // カメラオブジェクトを格納
    const string statusName = "Status"; // ステータスオブジェクトの名前
    private GameObject statusObj; // ステータスオブジェクトを格納する変数
    public GameObject bullet; // 弾のプレハブ
    public GameObject punch; // 素手攻撃用のプレハブ
    public Action actionType = Action.NONE;

    public Vector3 charMove = new Vector3(0.0f, 0.0f, 0.0f); // キャラクター移動量
    public Vector3 targetDirection = new Vector3(0.0f, 0.0f, 0.0f); // キャラクターの方向
    private Vector3 originCameraPos; // 原点となるカメラ座標
    private Vector2 charInput; // キャラクターキー入力用のベクトル
    const float gravity = 9.81f; // 重力
    float num;
    bool isJump; // ジャンプしているか
    bool actionFlg = false; // 何か行動をしているか
    public bool falling = false; // ステージ外に居るか

    // Use this for initialization
    void Start () {
        if (myPhotonView.isMine)
        {
            cc = GetComponent<CharacterController>(); // キャラクターコントローラーコンポーネントを取得
            // ステータスオブジェクトの名前を参照し格納、CharactorStatusコンポーネントを取得
            statusObj = GameObject.Find(statusName);
            charactorstatus = statusObj.GetComponent<CharactorStatus>();
            guimanager = statusObj.GetComponent<GUIManager>();
            CharactorSetup();
            playerCam = Camera.main; // カメラの情報を格納
            originCameraPos = playerCam.transform.localPosition; // 原点カメラの座標をキャラクターの座標に
        originSpeed = charactorSpeed;
            //  mousemanager.Init(transform, playerCam.transform); // キャラクターとカメラの位置をmousemanagerに送信

        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!myPhotonView.isMine)
        {
            return;
        }
        MoveUpdate();
        RotationUpdate();

        cc.Move(charMove * Time.deltaTime);
        if (actionType != Action.NONE && Input.GetKey(KeyCode.E) == true)
        {
            IsAction(actionType);
            Debug.Log("アクション呼び出し");
        }

        if (Input.GetButtonDown("Fire1") && playerType == 1)
        {
            GuardianAttack(); // ガーディアン攻撃用メソッド
        }
        else if (Input.GetButtonDown("Fire1") && playerType == 2)
        {
            MisdeedAttack(); // ミスディード攻撃用メソッド
        }

        if (falling == true)
        {
            Transform respawnObj = GameObject.Find("Respawn").GetComponent<Transform>();
            this.transform.position = respawnObj.transform.position;
            falling = false;
        }

        if (charactorHp <= 0 && playerType == 1)
        {
            Stun();
        }
        if (charactorHp <= 0 && playerType == 2)
        {
            Death();
        }

        bulletTimeInterval -= Time.deltaTime;
        punchTimeInterval -= Time.deltaTime;
        Vector3 velocity = cc.velocity;
        myPhotonTransView.SetSynchronizedValues(velocity, 0);
        DebugTest();
    }

    void CharactorSetup() // キャラクター情報を取得するメソッド
    {
        // playerTypeの値により各パラメータを取得
        charactorHp = charactorstatus.postPlayerHp(playerType);
        charactorMaxHp = charactorstatus.postPlayerMaxHp(playerType);
        charactorAtk = charactorstatus.postPlayerAtk(playerType);
        charactorSpeed = charactorstatus.postPlayerSpeed(playerType);
        charactorMoney = charactorstatus.postPlayerMoney(playerType);
        this.gameObject.tag = charactorstatus.postPlayertTag(playerType);

    }

    void MoveUpdate()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        float maxSpeed = 10.0f;

        Vector3 forward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1.0f, 0.0f, 1.0f));
        Vector3 right = Camera.main.transform.right;

        targetDirection = horizontal * right + vertical * forward;

        if (charactorSpeed >= maxSpeed)
        {
            charactorSpeed = maxSpeed;
        }

        if (cc.isGrounded)
        {
            charMove = targetDirection * charactorSpeed;
            if (Input.GetKey(KeyCode.LeftShift)==true)
            {
                charactorSpeed = (charactorSpeed * runSpeed);
            }
            else
            {
                charactorSpeed = originSpeed;
            }
            if (Input.GetButton("Jump"))
            {
                charMove.y = jumpSpeed;
            }
            else
            {
                float tempY = charMove.y;
                charMove = Vector3.Scale(targetDirection, new Vector3(1.0f, 0.0f, 1.0f).normalized);
                charMove *= charactorSpeed;
                charMove.y = tempY - gravity * Time.deltaTime;
            }
        }
    }

    void RotationUpdate()
    {
        Vector3 rotationDirection = charMove;
        rotationDirection.y = 0;
        if (rotationDirection.sqrMagnitude > 0.01f)
        {
            float rot = rotSpeed * Time.deltaTime;
            Vector3 newDirection = Vector3.Slerp(transform.forward, rotationDirection, rot);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }

    void GuardianAttack() // ガーディアン攻撃メソッド
    {
        if (punchTimeInterval <= 0)
        {
            GameObject.Instantiate(punch);
            punchTimeInterval = 0.5f;
        }

    }

    void MisdeedAttack() // ミスディード攻撃メソッド
    {
        if (bulletTimeInterval <= 0 && energy > 0)
        {
            GameObject.Instantiate(bullet); // 弾を発射
            energy -= 20;
            bulletTimeInterval = 0.75f;
        }
    }

    void IsAction(Action type)
    {
        switch (actionType)
        {
            case Action.BANKRAID:
                Bank.raidFlg = true;
                actionType = Action.NONE;
                break;
            case Action.ENERGYCHARGE:
                Charge.chargeFlg = true;
                actionType = Action.NONE;
                break;
        }
    }

    public void GetMoney(int money)
    {
        charactorMoney += money;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Bank")
        {
            actionType = Action.BANKRAID;
        }

        if (col.gameObject.tag == "Charge")
        {
            actionType = Action.ENERGYCHARGE;
        }

        if (col.gameObject.tag == "Under")
        {
            falling = true;
        }

    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Bank" && actionType == Action.BANKRAID)
        {
            actionType = Action.NONE;
        }
        if (col.gameObject.tag == "Charge" && actionType == Action.ENERGYCHARGE)
        {
            actionType = Action.NONE;
        }
    }

    void Stun()
    {
        guimanager.LogShow(
            (int)GUIManager.SenderList.SYSTEM, 0,
            (int)GUIManager.SenderList.SYSTEM, 4);
    }

    void Death()
    {

    }

    private void DebugTest() // デバッグ用メソッド
    {
    }


}
