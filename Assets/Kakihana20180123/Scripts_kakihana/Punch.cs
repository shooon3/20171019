using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour {

    Transform playerTransform; // プレイヤーの座標
    GameObject playerObj; // プレイヤーのゲームオブジェクトを格納
    Vector3 offset = new Vector3(0.0f, 1.5f, 0.0f);

    // Use this for initialization
    void Start()
    {
        playerTransform = GameObject.Find("Player").transform; // プレイヤーの座標を取得し格納
        playerObj = GameObject.Find("Player"); // プレイヤーのゲームオブジェクトを参照
        transform.position = playerTransform.position + offset; // 弾の発射座標をプレイヤー自身に
        transform.forward = Camera.main.transform.forward; // 発射方向は常に画面の中央部分に
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransform.position + transform.forward + offset;
        if (Time.frameCount % 60 == 0)
        {
            Destroy(this.gameObject);
            Debug.Log("素手攻撃終了");
        }
    }
}
