using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private Transform playerTransform;// プレイヤーの座標
    private GameObject playerObj;// プレイヤーのゲームオブジェクトを格納

  //  private PlayerController player;

    float shootSpeed = 10.0f; // 弾のスピード
    private Vector3 bulletDirection = new Vector3(0.0f, 0.0f, 0.0f); // 弾の移動量
    public Vector3 offset = new Vector3(0.0f, 2.0f, 0.0f);
    // Use this for initialization
    void Start () {
        playerObj = GameObject.FindGameObjectWithTag("Misdeed");
        playerTransform = playerObj.transform;
       // player = playerObj.GetComponent<PlayerController>(); // プレイヤー情報クラスのコンポーネントを取得
        transform.position = playerTransform.position + offset; // 弾の発射座標をプレイヤー自身に
        transform.forward = Camera.main.transform.forward; // 発射方向は常に画面の中央部分に
		
	}
	
	// Update is called once per frame
	void Update () {
        // 弾の移動
        transform.position += transform.forward * shootSpeed * Time.deltaTime;
        // 原点から30マス以上離れたら削除する
        if (Time.deltaTime % 60 == 0)
        {
            Destroy(this.gameObject);
        }
		
	}
}
