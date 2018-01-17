using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    /* プレイヤークラス */
    /* StandardAssetsのFPSControllerを基に作成*/
    /* カメラの向きに合わせて移動する*/

    public enum Action
    {
        NONE = 0,
        BANKRAID = 1,
        TAKEITEM = 2,
        ENERGYCHARGE = 3
    }
    public string playerName;
    public int playerId;
    public PhotonView photonview;

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
    [SerializeField] private Curve headCurve; // カメラの上下移動のデータ
    [SerializeField] private LerpController lerpcontroller; // カメラの回転部分のデータ

    [SerializeField] public int energy = 100; // スタンガン発射に必要なエネルギー
    [SerializeField] public int maxEnergy = 100;
    [SerializeField] private float bulletTimeInterval = 0.0f; // スタンガン発射間隔
    [SerializeField] private float punchTimeInterval = 0.0f; // 素手攻撃の間隔

    [SerializeField] Camera playerCam; // カメラオブジェクトを格納

    const string statusName = "Status"; // ステータスオブジェクトの名前
    private GameObject statusObj; // ステータスオブジェクトを格納する変数
    public GameObject bullet; // 弾のプレハブ
    public GameObject fire; // 攻撃用のプレハブ（何に使うかは未定）
    public GameObject punch; // 素手攻撃用のプレハブ
    [SerializeField] GameObject nameLabel;
    public Text energyText;
    public Text raidText;
    public Text chargeText;

    CollisionFlags charCollFlg; // キャラクター衝突フラグ
    public Action actionType = Action.NONE;

    CharactorStatus charactorstatus; // 各種キャラクターデータを参照するクラス
    CharacterController cc; // キャラクターコントローラーを格納する
    GUIManager guimanager;
    GameManagement gm;

    public Vector3 charMove = new Vector3(0.0f, 0.0f, 0.0f); // キャラクター移動量
    private Vector3 mouseclickPos = new Vector3(0.0f, 0.0f, 0.0f); // マウスクリック時の座標
    public Vector3 charDirection = new Vector3(0.0f, 0.0f, 0.0f); // キャラクターの方向
    private Vector3 originCameraPos; // 原点となるカメラ座標
    private Vector2 charInput; // キャラクターキー入力用のベクトル
    const float gravity = 9.81f; // 重力
    float num;
    bool isJump; // ジャンプしているか
    bool actionFlg = false;
    public bool falling = false;

    // Use this for initialization
    void Start() {
        cc = GetComponent<CharacterController>(); // キャラクターコントローラーコンポーネントを取得
        // ステータスオブジェクトの名前を参照し格納、CharactorStatusコンポーネントを取得
        statusObj = GameObject.Find(statusName);
        charactorstatus = statusObj.GetComponent<CharactorStatus>();
        guimanager = statusObj.GetComponent<GUIManager>();
        nameLabel = Instantiate(Resources.Load("NameLabel")) as GameObject;

        CharactorSetup();
        raidText.enabled = false;
        chargeText.enabled = false;
        playerCam = Camera.main; // カメラの情報を格納
        originCameraPos = playerCam.transform.localPosition; // 原点カメラの座標をキャラクターの座標に

        energyText.text = "Energy："; // 残りエネルギー確認用
        mousemanager.Init(transform, playerCam.transform); // キャラクターとカメラの位置をmousemanagerに送信
    }

    // Update is called once per frame
    void Update() {
        RotateView(); // カメラとキャラクターの向きを更新する
        if (cc.isGrounded) // 地面に設置していたら
        {
            if (Input.GetButtonDown("Jump"))// スペースキーが押されたら
            {
                isJump = true; // ジャンプする
            }
            if (isJump&&cc.isGrounded)
            {
                StartCoroutine(lerpcontroller.Cycle()); // ジャンプするとカメラも若干上を向くように回転
                charDirection.y = jumpSpeed;
                isJump = false;
            }
        }
        else
        {
            charDirection += Physics.gravity * Time.deltaTime; // 重力によって落下する
        }
        charCollFlg = cc.Move(charDirection * Time.deltaTime);

        if (actionType != Action.NONE && Input.GetKey(KeyCode.E)==true)
        {
            IsAction(actionType);
            Debug.Log("アクション呼び出し");
        }

        

        if (Input.GetButtonDown("Fire1") && playerType ==1)
        {
            PlayerAttack(); // 市民の攻撃用メソッド
        }else if(Input.GetButtonDown("Fire1")&& playerType == 2)
        {
            mouseclickPos = Input.mousePosition;
            mouseclickPos.z = 5.0f;
            terroristAttack(); // テロリスト攻撃用メソッド
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

        energyText.text = "Energy：" + energy.ToString();
        bulletTimeInterval -= Time.deltaTime;
        punchTimeInterval -= Time.deltaTime;
        DebugTest();
    }

    void FixedUpdate()
    {
        float speed;
        GetInput(out speed); // キャラクター移動方向の取得
        charMove = transform.forward * charInput.y + transform.right * charInput.x; // キャラクター移動量を設定

        RaycastHit hit;
        Physics.SphereCast( // 地面の設置判定
            transform.position,
            cc.radius,
            Vector3.down,
            out hit,
            cc.height / 2,
            Physics.AllLayers,
            QueryTriggerInteraction.Ignore
            );
        charMove = Vector3.ProjectOnPlane(charMove, hit.normal).normalized; // 進む方向にベクトルを出力する
        charDirection.x = charMove.x * speed;
        charDirection.z = charMove.z * speed;

        CameraPotiton(speed);
        mousemanager.UpdateCursorLock();
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
        runSpeed = charactorSpeed * 2;
        
    }

    void PlayerAttack() // 市民攻撃メソッド
    {
        if (punchTimeInterval <= 0)
        {
            GameObject.Instantiate(punch);
            punchTimeInterval = 0.5f;
        }

    }

    void terroristAttack() // テロリスト攻撃メソッド
    {
        if (bulletTimeInterval <= 0 && energy > 0)
        {
            GameObject.Instantiate(bullet); // 弾を発射
            energy -= 20;
            bulletTimeInterval = 0.75f;
        }
    }

    void CameraPotiton(float speed)
    {
        Vector3 newCameraPos;
        if (cc.velocity.magnitude>0&&cc.isGrounded)
        {
            newCameraPos = playerCam.transform.localPosition;
            newCameraPos.y = playerCam.transform.localPosition.y - lerpcontroller.Offset();
        }
        else
        {
            newCameraPos = playerCam.transform.localPosition;
            newCameraPos.y = originCameraPos.y - lerpcontroller.Offset();
        }
        playerCam.transform.localPosition = newCameraPos;
    }

    private void GetInput(out float speed) // 移動量取得メソッド
    {
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        bool wasDash = dashFlg;

        if (Input.GetKey(KeyCode.LeftShift)) // 左シフトキーでダッシュ
        {
            dashFlg = true;
        }
        else
        {
            dashFlg = false;
        }
        if (dashFlg == true)
        {
            speed = runSpeed;
        }
        else
        {
            speed = charactorSpeed;
        }

        charInput = new Vector2(Horizontal, Vertical);

        if (charInput.sqrMagnitude > 1)
        {
            charInput.Normalize();
        }
        if (dashFlg !=wasDash && cc.velocity.sqrMagnitude > 0)
        {
            StopAllCoroutines();
        }
    }

    private void RotateView() // カメラ移動メソッド
    {
        mousemanager.LookRotation(transform, playerCam.transform);
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
            raidText.enabled = true;
        }

        if (col.gameObject.tag == "Charge")
        {
            actionType = Action.ENERGYCHARGE;
            chargeText.enabled = true;
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
            raidText.enabled = false;
        }
        if (col.gameObject.tag =="Charge" && actionType == Action.ENERGYCHARGE)
        {
            actionType = Action.NONE;
            chargeText.enabled = false;
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
