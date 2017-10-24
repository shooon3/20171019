using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamManager : MonoBehaviour {
    /* プレイヤーカメラ関連のクラス */

    public float mouseX, mouseY; // マウス移動量
    public Vector3 CameraMove = new Vector3(0, 0, 0); // カメラ移動量
    float mouseSpeed = 10.0f; // カメラ移動スピード

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        mouseX = Input.GetAxis("Mouse X"); // マウスカーソルより移動量を取得
        mouseY = Input.GetAxis("Mouse Y");

        if (mouseX > 0) // マウスカーソルが右に向いていたら
        {
            CameraMove = new Vector3(0, mouseSpeed, 0); // カメラ移動量をプラスに
        }else if(mouseX < 0) // マウスカーソルが左に向いていたら
        {
            CameraMove = new Vector3(0, -mouseSpeed, 0); // カメラ移動量をマイナスに
        }else if(mouseX == 0) // マウスカーソルが動いていなかったら
        {
            CameraMove = Vector3.zero; // カメラ移動量はゼロに
        }
        transform.Rotate(CameraMove * Time.deltaTime); // カメラ移動

        if (Time.frameCount % 20 ==0)
        {
            // デバッグ用
            Debug.Log(mouseX);
            Debug.Log(mouseY);
        }
		
	}
}
