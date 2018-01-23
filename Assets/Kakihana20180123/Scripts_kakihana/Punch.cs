using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour {

    Transform playerTransform; // プレイヤーの座標
    GameObject playerObj;//プレイヤーのオブジェクト


    // Use this for initialization
    void Start()
    {
        playerTransform = GameObject.Find("PlayerController").transform; // プレイヤーの座標を取得し格納
        playerObj = GameObject.Find("PlayerController"); // プレイヤーのゲームオブジェクトを参照
        transform.position = playerTransform.position; // 弾の発射座標をプレイヤー自身に
        transform.forward = Camera.main.transform.forward; // 発射方向は常に画面の中央部分に
        Debug.Log("素手攻撃開始");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransform.position + transform.forward;
        if (Time.frameCount % 60 == 0)
        {
            //Destroy(this.gameObject);
            Debug.Log("素手攻撃終了");
        }
    }
}
