using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamManager : MonoBehaviour {
    /* プレイヤーカメラ関連のクラス */
    /* 今後移動方向に応じてカメラの向きを変えたい*/

    public float mouseX, mouseY; // マウス移動量
    public Vector3 CameraMove = new Vector3(0, 0, 0); // カメラ移動量
    float mouseSpeed = 10.0f; // カメラ移動スピード

    private GameObject target; // カメラ回転を基にするターゲット
    Player player; // Playerクラスより位置情報を格納

	// Use this for initialization
	void Start () {
        // Playerの情報を取得
        target = GameObject.Find("Player1");
        player = target.GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        mouseX = Input.GetAxis("Mouse X"); // マウスカーソルより移動量を取得
        mouseY = Input.GetAxis("Mouse Y");

        if (mouseX != 0)// マウスカーソルが動いていたら
        {
            if (mouseX > 0) // マウスカーソルが右に向いていたら
            {
                CameraMove = new Vector3(0, mouseSpeed, 0); // カメラ移動量をプラスに
            }
            else if (mouseX < 0) // マウスカーソルが左に向いていたら
            {
                CameraMove = new Vector3(0, -mouseSpeed, 0); // カメラ移動量をマイナスに
            }
            // カメラ移動
            transform.Rotate(CameraMove * Time.deltaTime);
        }
        else
        {
            // マウス移動量が0になるとカメラの回転がリセットされる
            CameraMove = Vector3.zero;
            transform.rotation = Quaternion.Euler(CameraMove);
        }
        if (Time.frameCount % 20 ==0)
        {
            // デバッグ用
            Debug.Log(mouseX);
            Debug.Log(mouseY);
        }
		
	}
}
