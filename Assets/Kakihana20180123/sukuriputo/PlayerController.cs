using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;

public class PlayerController_2 : MonoBehaviour {
    /*プレイヤースクリプト　2.0*/
    public enum Action // プレイヤーの行動
    {
        NONE = 0, // 通常状態（何もしていない）
        BANKRAID = 1, // 銀行襲撃
        CONVENIRAID = 2, // コンビニ襲撃
        TAKEITEM = 3, // アイテム取得
        ENERGYCHARGE = 4, // エネルギー充電
        RESCUE = 5, // 気絶したプレイヤーを復活
        RESCUEWAIT = 6 // 復活待ち
    }
    public enum PLAYERTYPE // 自分の所属
    {
        GUARDIAN = 1, // ガーディアン
        MISDEED = 2 // ミスディード
    }

    public Animator animetor; // キャラクターのアニメーター
    public CharacterController cc; // キャラクターコントローラー
    CharactorStatus charactorstatus; // 各種キャラクターデータを参照するクラス
    GUIManager guimanager; // GUIクラスのオブジェクト
    GameManagement gm; // マスタークラスのオブジェクト
	public GamePad.Index padID;

	public string playerName; // プレイヤー名
    public int playerId; // プレイヤーID
    public int orderNum; // ゲームマスタークラスが識別するための番号
    //public PhotonView myPhotonView; // 自分のPhotonview
    //public PhotonTransformView myPhotonTransView; // 自分のPhotonTransformView

    [SerializeField] private int playerType = (int)PLAYERTYPE.MISDEED;// プレイヤーの属性 1でガーディアン、2でミスディードS
    [SerializeField] public int charactorHp = 0; // プレイヤーのHP
    [SerializeField] private int charactorMaxHp = 0; // 最大HP
    [SerializeField] private int charactorAtk = 0; // 攻撃力
    [SerializeField] private float charactorSpeed = 0.0f; // 移動速度
    [SerializeField] private float charactorMoney; // 所持金
    [SerializeField] private float jumpSpeed = 10.0f; // ジャンプ移動量
    [SerializeField] private float runSpeed; // ダッシュ時のスピード
    [SerializeField] private bool dashFlg; // ダッシュしているか
    private float originSpeed = 0.0f; // 元のキャラクタースピード

    [SerializeField] public int energy = 100; // スタンガン発射に必要なエネルギー
    [SerializeField] public int maxEnergy = 100; // 最大エネルギー
    [SerializeField] private float bulletTimeInterval = 0.0f; // スタンガン発射間隔
    [SerializeField] private float punchTimeInterval = 0.0f; // 素手攻撃の間隔
    [SerializeField] public float rotSpeed = 3.0f; // 旋回速度
    public float rescueTime = 0.0f;
    public float rescueTimeLimit = 7.0f;
    public float actionTime = 0.0f;
    public float actionTimeLimit = 3.0f;

	public Camera playerCam; // カメラオブジェクトを格納
    const string statusName = "Status"; // ステータスオブジェクトの名前
    private GameObject statusObj; // ステータスオブジェクトを格納する変数
    public GameObject bullet; // 弾のプレハブ
    public GameObject punch; // 素手攻撃用のプレハブ
    public GameObject rescueObj; // 自分の救助用当たり判定
    public GameObject beRescueObj; // 救助するプレイヤーのオブジェクト
    public PlayerController rescuePlayerScript; // 救助するプレイヤーのデータ
    public Action actionType = Action.NONE; // 行動を保存する変数

    public Vector3 charMove = new Vector3(0.0f, 0.0f, 0.0f); // キャラクター移動量
    public Vector3 targetDirection = new Vector3(0.0f, 0.0f, 0.0f); // キャラクターの方向
    const float gravity = 9.81f; // 重力
    bool isJump; // ジャンプしているか
    bool actionFlg = false; // 何か行動をしているか
    public bool falling = false; // ステージ外に居るか
    public bool isStun = false;
    public bool rescue = false;

    // Use this for initialization
    void Start () {
        //if (myPhotonView.isMine)// 自分のキャラクターであれば実行
        //{
            cc = GetComponent<CharacterController>(); // キャラクターコントローラーコンポーネントを取得
            // ステータスオブジェクトの名前を参照し格納、CharactorStatusコンポーネントを取得
            statusObj = GameObject.Find(statusName);
            charactorstatus = statusObj.GetComponent<CharactorStatus>();
            guimanager = statusObj.GetComponent<GUIManager>();
            rescueObj.SetActive(false);
            CharactorSetup();// キャラクターステータスの初期設定
            //playerCam = Camera.main; // カメラの情報を格納
            originSpeed = charactorSpeed; // 元スピードは最初に取得したスピードに
        //}
    }
	
	// Update is called once per frame
	void Update () {
		var Pad = GamePad.GetState(padID, false);
		//ゲームパッドの対応をさせる。
		//if (!myPhotonView.isMine)// 自分のキャラクターでなければ実行されない
		//{
		//    return;
		//}

		//if (actionType == Action.NONE)// 何も行動していなければ
		//{
		//    MoveUpdate(); // 移動量を決めるメソッド
		//}
		if (!isStun)
		{
			MoveUpdate(); // 移動量を決めるメソッド
			RotationUpdate(); // 向きを決めるメソッド
		}
		else
		{

		}

        cc.Move(charMove * Time.deltaTime); // キャラクター移動
        if (actionType != Action.NONE && Pad.B)
        {
            actionTime += Time.deltaTime;
            if (actionTime > actionTimeLimit)
            {
                IsAction(actionType);
                Debug.Log("アクション呼び出し");
                actionTime = 0.0f;
            }
        }
        else
        {
            actionTime = 0.0f;
        }

        if (Pad.B && playerType == (int)PLAYERTYPE.GUARDIAN)
        {
            GuardianAttack(); // ガーディアン攻撃用メソッド
        }
        else if (Pad.B && playerType == (int)PLAYERTYPE.MISDEED)
        {
            MisdeedAttack(); // ミスディード攻撃用メソッド
        }

        if (falling == true) // マップ外に落ちたら指定されている場所へワープ
        {
            Transform respawnObj = GameObject.Find("Respawn").GetComponent<Transform>();
            this.transform.position = respawnObj.transform.position;
            falling = false;
        }

        if (charactorHp <= 0 && playerType == (int)PLAYERTYPE.GUARDIAN) // ガーディアンで体力が０になったら
        {
            isStun = true; // 気絶メソッド
        }
        else
        {
            isStun = false;
        }
        if (charactorHp <= 0 && playerType == (int)PLAYERTYPE.MISDEED) // ミスディードで体力が０になったら
        {
            Arrest(); // 確保メソッド
        }

        if (isStun == true)
        {
            Stun();
        }

        bulletTimeInterval -= Time.deltaTime;
        punchTimeInterval -= Time.deltaTime;
        Vector3 velocity = cc.velocity;
        //myPhotonTransView.SetSynchronizedValues(velocity, 0);
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
		var Pad = GamePad.GetState(padID, false);
		float vertical = Pad.LeftStickAxis.y;
        float horizontal = Pad.LeftStickAxis.x;
        float maxSpeed = 10.0f; // 最大スピード
		
		// カメラの方向よりY成分を除いたベクトルを取得し、自分が移動する方向を決める
        Vector3 forward = Vector3.Scale(playerCam.transform.forward, new Vector3(1.0f, 0.0f, 1.0f));
        Vector3 right = playerCam.transform.right; // カメラの右方向の向きを取得
		
		// カメラの方向を元にキャラクターの移動方向を調整
		targetDirection = horizontal * right + vertical * forward;

		if (charactorSpeed >= maxSpeed)
        {
            charactorSpeed = maxSpeed;// 最大スピードを超えたらスピードは最大値に固定される
        }
            charMove = targetDirection * charactorSpeed;

		if (charMove.x != 0.0f || charMove.z != 0.0f)
		{
			animetor.SetBool("Run", true);
		}
		else
		{
			animetor.SetBool("Run", false);
		}

		if (Input.GetKey(KeyCode.LeftShift)==true)
            {
                charactorSpeed = (charactorSpeed * runSpeed);
            }
            else
            {
                charactorSpeed = originSpeed;
            }
            //if (Pad.X)
            //{
            //    charMove.y = jumpSpeed;
            //}
            //else
            //{
                float tempY = charMove.y;
                charMove = Vector3.Scale(targetDirection, new Vector3(1.0f, 0.0f, 1.0f).normalized);
                charMove *= charactorSpeed;
                charMove.y = tempY - gravity * Time.deltaTime;
            //}
        
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

    void IsAction(Action type)// アクションメソッド
    {
        switch (actionType)
        {
            case Action.BANKRAID: // 銀行襲撃
                Bank.raidFlg = true;
                actionType = Action.NONE;
                break;
            case Action.CONVENIRAID: // コンビニ襲撃
                Conveni.raidFlg = true;
                actionType = Action.NONE;
                break;
            case Action.ENERGYCHARGE: // スタンガン充電
                Charge.chargeFlg = true;
                actionType = Action.NONE;
                break;
            case Action.RESCUE: // 気絶しているプレイヤーを救助
                Rescue();
                break;
        }
    }

    public void GetMoney(int money)
    {
        charactorMoney += money;
    }

    void Stun() // 気絶メソッド
    {
		rescueObj.SetActive(true); // 救助用当たり判定を表示
		animetor.SetBool("Stun", true);
		actionType = Action.RESCUEWAIT; // 行動は気絶待ち状態に
		rescueTime += Time.deltaTime;
		Debug.Log(rescueTime);
		if (rescueTime > rescueTimeLimit)
		{
			isStun = false;
			charactorHp = charactorMaxHp;
			animetor.SetBool("Stun", false);
		}

		rescueObj.SetActive(true); // 救助用当たり判定を表示
        actionType = Action.RESCUEWAIT; // 行動は気絶待ち状態に
        //guimanager.LogShow(
        //    (int)GUIManager.SenderList.SYSTEM, 0,
        //    (int)GUIManager.SenderList.SYSTEM, 4); // 気絶ログを表示
        //gm.isStun[orderNum] = true; // マスタークラスに気絶状態を送信
    }
    void IsRescue() // 救助メソッド
    {
        rescuePlayerScript.Rescue(); // 救助される側は救助メソッドを起動
        actionType = Action.NONE; // 自分の行動状態を通常に
    }

    public void Rescue() // 救出メソッド
    {
        if (actionType == Action.RESCUEWAIT) // 救助待ちなら
        {
            charactorHp = charactorMaxHp; // 自分のHPを回復
            actionType = Action.NONE; // 通常状態に
            isStun = false; // 気絶を解除
            gm.isStun[orderNum] = false; // マスタークラスに気絶解除を送信
            rescueObj.SetActive(false); // 救助用当たり判定を非表示に
        }
    }

    void Arrest() // 確保メソッド
    {

    }

    void OnTriggerEnter(Collider col) // 当たったオブジェクトにより行動を起こす
    {
        if (col.gameObject.tag == "Bank" && playerType == (int)PLAYERTYPE.MISDEED)
        {
            actionType = Action.BANKRAID;
        }

        if (col.gameObject.tag == "Conveni" && playerType == (int)PLAYERTYPE.MISDEED)
        {
            actionType = Action.CONVENIRAID;
        }

        if (col.gameObject.tag == "Charge" && playerType == 2)
        {
            actionType = Action.ENERGYCHARGE;
        }

        if (col.gameObject.tag == "Rescue" && playerType == (int)PLAYERTYPE.GUARDIAN)
        {
            actionType = Action.RESCUE; 
            beRescueObj = col.gameObject;
            rescuePlayerScript = beRescueObj.GetComponent<PlayerController>();
        }

        if (col.gameObject.tag == "Bullet" && playerType == (int)PLAYERTYPE.GUARDIAN)
        {
            int damage = 80;
            charactorHp -= damage;
            Destroy(col);
        }

        if (col.gameObject.tag == "Punch" && playerType == (int)PLAYERTYPE.MISDEED)
        {
            int damage = -20;
            charactorHp -= damage;
        }

        if (col.gameObject.tag == "Under")
        {
            falling = true;
        }

    }
    void OnTriggerExit(Collider col) // 行動を起こすオブジェクトから離れたら行動状態を解除
    {
        if (col.gameObject.tag == "Bank" && actionType == Action.BANKRAID)
        {
            actionType = Action.NONE;
        }
        if (col.gameObject.tag == "Charge" && actionType == Action.ENERGYCHARGE)
        {
            actionType = Action.NONE;
        }

        if (col.gameObject.tag == "Conveni" && actionType == Action.CONVENIRAID)
        {
            actionType = Action.NONE;
        }
        if (col.gameObject.tag == "Rescue" && actionType == Action.RESCUE)
        {
            actionType = Action.NONE;
        }
    }



    private void DebugTest() // デバッグ用メソッド
    {
    }

	//void OnControllerColliderHit(ControllerColliderHit hit)
	//{
	//	if (hit.gameObject.tag == "Bullet" && playerType == (int)PLAYERTYPE.GUARDIAN)
	//	{
	//		int damage = 80;
	//		charactorHp -= damage;
	//	}
	//}




	}
