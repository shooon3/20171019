using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    Transform playerTransform; // プレイヤーの座標
    GameObject playerObj; // プレイヤーのゲームオブジェクトを格納

    Player playerRot; // プレイヤーの情報を参照するクラス

    float shootSpeed = 10.0f; // 弾のスピード
    Vector3 bulletDirection = new Vector3(0.0f, 0.0f, 0.0f); // 弾の移動量

	// Use this for initialization
	void Start () {
        playerTransform = GameObject.Find("PlayerController").transform; // プレイヤーの座標を取得し格納
        playerObj = GameObject.Find("PlayerController"); // プレイヤーのゲームオブジェクトを参照
        playerRot = playerObj.GetComponent<Player>(); // プレイヤー情報クラスのコンポーネントを取得
        transform.position = playerTransform.position; // 弾の発射座標をプレイヤー自身に
        transform.forward = Camera.main.transform.forward; // 発射方向は常に画面の中央部分に
		
	}
	
	// Update is called once per frame
	void Update () {
        // 弾の移動
        transform.position += transform.forward * shootSpeed * Time.deltaTime;
        Debug.Log(this.transform.forward);
        // 原点から30マス以上離れたら削除する
        if (transform.position.magnitude >= 100.0f)
        {
            //Destroy(this.gameObject);
        }
		
	}
}
