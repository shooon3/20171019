using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    Transform playerTransform1, playerTransform2, playerTransform3, playerTransform4; // プレイヤーの座標
    GameObject playerObj1, playerObj2, playerObj3, playerObj4; // プレイヤーのゲームオブジェクトを格納

    private PlayerController1 playerInfo1;
    private PlayerController2 playerInfo2;
    private PlayerController3 playerInfo3;
    private PlayerController4 playerInfo4;

    float shootSpeed = 10.0f; // 弾のスピード
    Vector3 bulletDirection = new Vector3(0.0f, 0.0f, 0.0f); // 弾の移動量

	// Use this for initialization
	void Start () {
        playerTransform1 = GameObject.Find("Player1").transform; // プレイヤーの座標を取得し格納
        playerTransform2 = GameObject.Find("Player2").transform; // プレイヤーの座標を取得し格納
        playerTransform3 = GameObject.Find("Player3").transform; // プレイヤーの座標を取得し格納
        playerTransform4 = GameObject.Find("Player4").transform; // プレイヤーの座標を取得し格納

        playerObj1 = GameObject.Find("Player1"); // プレイヤーのゲームオブジェクトを参照
        playerObj2 = GameObject.Find("Player2");
        playerObj3 = GameObject.Find("Player3");
        playerObj4 = GameObject.Find("Player4");
        playerInfo1 = playerObj1.GetComponent<PlayerController1>(); // プレイヤー情報クラスのコンポーネントを取得
        transform.position = playerTransform1.position; // 弾の発射座標をプレイヤー自身に
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
