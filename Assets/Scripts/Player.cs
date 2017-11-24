using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    /* プレイヤークラス */
    /* StandardAssetsのFPSControllerを基に作成*/
    /* カメラの向きに合わせて移動する*/
    [SerializeField] private int playerGroup = 0; // プレイヤーの属性 0で市民（NPC）1で市民（プレイヤー）2でテロリスト（プレイヤー）
    [SerializeField] private int charactorHp = 0; // 自キャラのHP
    [SerializeField] private int charactorMaxHp = 0; // 自キャラの最大HP
    [SerializeField] private int charactorAtk = 0; // 自キャラの攻撃力
    [SerializeField] private float charactorSpeed = 0.0f; // 自キャラの移動速度
    [SerializeField] private float jumpSpeed = 10.0f; // ジャンプ移動量
    [SerializeField] private float runSpeed = 0.0f; // ダッシュ時の倍率
    [SerializeField] private bool dashFlg; // ダッシュしているか

    [SerializeField] private MouseManager mousemanager; // マウス移動のデータ
    [SerializeField] private Curve headCurve; // カメラの上下移動のデータ
    [SerializeField] private LerpController lerpcontroller; // カメラの回転部分のデータ

    [SerializeField] public float energy = 100.0f; // スタンガン発射に必要なエネルギー
    [SerializeField] public float maxEnergy = 100.0f;
    [SerializeField] private float bulletTimeInterval = 0.0f; // スタンガン発射間隔
    [SerializeField] private float punchTimeInterval = 0.0f;

    [SerializeField] private Camera playerCam; // カメラ移動はmousemanager LerpController このクラスで行うため、現在は使用していない

    const string statusName = "Status"; // ステータスオブジェクトの名前
    private GameObject statusObj; // ステータスオブジェクトを格納する変数
    public GameObject bullet; // 弾のプレハブ
    public GameObject fire; // 攻撃用のプレハブ（何に使うかは未定）
    public GameObject punch; // 素手攻撃用のプレハブ
    public GameObject cameraObj;

    public Text energyText;

    CollisionFlags charCollFlg; // キャラクター衝突フラグ

    CharactorStatus charactorstatus; // 各種キャラクターデータを参照するクラス
    CharacterController cc; // キャラクターコントローラーを格納する
    GUIManager guimanager;

    public Vector3 charMove = new Vector3(0.0f, 0.0f, 0.0f); // キャラクター移動量
    private Vector3 mouseclickPos = new Vector3(0.0f, 0.0f, 0.0f); // マウスクリック時の座標
    public Vector3 charDirection = new Vector3(0.0f, 0.0f, 0.0f); // キャラクターの方向
    private Vector3 originCameraPos; // 原点となるカメラ座標
    private Vector2 charInput; // キャラクターキー入力用のベクトル
    const float gravity = 9.81f; // 重力
    bool isJump; // ジャンプしているか

    CamManager cm;

    // Use this for initialization
    void Start() {
        cc = GetComponent<CharacterController>(); // キャラクターコントローラーコンポーネントを取得
        // ステータスオブジェクトの名前を参照し格納、CharactorStatusコンポーネントを取得
        statusObj = GameObject.Find(statusName);
        charactorstatus = statusObj.GetComponent<CharactorStatus>();
        guimanager = statusObj.GetComponent<GUIManager>();

        playerCam = Camera.main; // カメラの情報を格納
        originCameraPos = playerCam.transform.localPosition; // 原点カメラの座標をキャラクターの座標に

        energyText.text = "Energy："; // 残りエネルギー確認用

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

        if (Input.GetButtonDown("Fire1") && playerGroup ==1)
        {
            PlayerAttack(); // 市民の攻撃用メソッド
        }else if(Input.GetButtonDown("Fire1")&& playerGroup == 2)
        {
            mouseclickPos = Input.mousePosition;
            mouseclickPos.z = 5.0f;
            terroristAttack(); // テロリスト攻撃用メソッド
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

    void setcitizenStatus() // 市民（NPC）データ取得メソッド、各種データを取得し変数に格納
    {

    }
    void setplayerStatus() // 市民（プレイヤー）用データ取得メソッド
    {
        charactorHp = charactorstatus.postplayerHp();
        charactorMaxHp = charactorstatus.postplayerMaxHp();
        charactorAtk = charactorstatus.postplayerAtk();
        charactorSpeed = charactorstatus.postplayerSpeed();
        runSpeed = charactorSpeed * 2;
    }
    void setterroristStatus() // テロリスト（プレイヤー）用データ取得メソッド
    {
        charactorHp = charactorstatus.postterroristHp();
        charactorMaxHp = charactorstatus.postterroristMaxHp();
        charactorAtk = charactorstatus.postterroristAtk();
        charactorSpeed = charactorstatus.postterroristSpeed();
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
            //iTween.ValueTo(guimanager.gameObject, iTween.Hash(
            //    "from", energy,
            //    "to", energy - 20,
            //    "time", 1.0f,
            //    "easetype", iTween.EaseType.linear,
            //    "onupdate", "DownEnergy",
            //    "onupdatetarget", guimanager.gameObject
            //    ));
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

    private void DebugTest() // デバッグ用メソッド
    {
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Bank" && playerGroup ==2)
        {

        }
    }

}

//        if (cc.isGrounded)
//        {
//            charMove = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));// キー入力から方向を取得
//            charMove = transform.TransformDirection(charMove); // 移動方向をキャラクター移動量に設定
//            if (Input.GetKey(KeyCode.LeftShift))
//            {
//                // 左シフトキーでダッシュ
//                charMove *= charactorSpeed* runSpeed;
//            }
//            if (Input.GetButton("Jump"))
//            {
//                // スペースキーでジャンプ
//                charMove.y = jumpSpeed;
//            }
//        }
//        charMove.y -= gravity* Time.deltaTime; // 重力を計算
//cc.Move(charMove* Time.deltaTime); // キャラクター移動
//charDirection = transform.localEulerAngles;