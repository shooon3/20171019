using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamManager : MonoBehaviour {
    /* プレイヤーカメラ関連のクラス */
    /**/

    public float mouseX, mouseY,pos; // マウス移動量
    public Vector3 cameraMove = new Vector3(0.0f,0.0f,0.0f); // カメラ移動量
    public Vector3 cameraDirection = new Vector3(0.0f,0.0f,0.0f);
    float cameraSpeed = 10.0f; // カメラ移動スピード

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

        pos = Input.GetAxis("Horizontal");

        if (mouseX != 0)// マウスカーソルが動いていたら
        {
            if (mouseX > 0)
            {
                cameraMove = new Vector3(0,mouseX, 0);
            }
            if (mouseX < 0)
            {
                mouseX = -mouseX;
                cameraMove = new Vector3(0,-mouseX, 0);
            }
            // カメラ移動
            cameraDirection = transform.localEulerAngles;
            transform.Rotate((cameraMove*cameraSpeed) * Time.deltaTime);
        }
        else
        {
            // マウス移動量が0になるとカメラの回転がリセットされる
            //cameraMove = Vector3.zero;
            //transform.rotation = Quaternion.Euler(cameraMove);
        }

        if (Time.frameCount % 20 ==0)
        {
            // デバッグ用
            Debug.Log(mouseX);
            Debug.Log(mouseY);
        }
		
	}
}
