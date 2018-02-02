using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{

    Transform playerTransform; // プレイヤーの座標
    GameObject playerObj; // プレイヤーのゲームオブジェクトを格納
    Vector3 offset = new Vector3(0.0f, 1.5f, 0.0f);
    float speed = 1.0f;
    private GameObject camObj;
    private Camera cam;
    public string uniquePlayerInfo;
    public string cameraName;

    // Use this for initialization
    void Start()
    {
        playerTransform = GameObject.Find(uniquePlayerInfo).transform; // プレイヤーの座標を取得し格納
        playerObj = GameObject.Find(uniquePlayerInfo); // プレイヤーのゲームオブジェクトを参照
        transform.position = playerTransform.position + offset; // 弾の発射座標をプレイヤー自身に
        camObj = GameObject.Find(cameraName);
        cam = camObj.GetComponent<Camera>();
        transform.forward = cam.transform.forward; // 発射方向は常に画面の中央部分に
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        if (Time.frameCount % 60 == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
