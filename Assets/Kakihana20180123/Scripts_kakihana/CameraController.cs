using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {

    private const float Y_ANGLE_MIN = -89.0f;
    private const float Y_ANGLE_MAX = 89.0f;

    public Transform target;
    public Vector3 offset;
    private Vector3 lookAt;

    private float distance = 10.0f;
    private float distance_Min = 1.0f;
    private float distance_Max = 2.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;

    private float XSensitivity = 2.0f;
    private float YSensitivity = 2.0f;
    private float XKeySensitivity = 2.0f;
    // Use this for initialization
    void Start () {
        offset = new Vector3(0.0f,1.5f,2.0f);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Q))
        {
            currentX += -XKeySensitivity;
        }
        if (Input.GetKey(KeyCode.E))
        {
            currentX += XKeySensitivity;
        }
        if (Input.GetAxis("Mouse X") >= 0 || Input.GetAxis("Mouse Y") >= 0)
        {
            currentX += Input.GetAxis("Mouse X") * XSensitivity;
            currentY += Input.GetAxis("Mouse Y") * YSensitivity;
            currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
        }
        distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel"), distance_Min, distance_Max);
    }

    void LateUpdate()
    {
        if (target != null)  //targetが指定されるまでのエラー回避
        {
            lookAt = target.position + offset;  //注視座標はtarget位置+offsetの座標

            //カメラ旋回処理
            Vector3 dir = new Vector3(0, 0, -distance);
            Quaternion rotation = Quaternion.Euler(-currentY, currentX, 0);

            transform.position = lookAt + rotation * dir;   //カメラの位置を変更
            transform.LookAt(lookAt);   //カメラをLookAtの方向に向けさせる
        }

    }

}
