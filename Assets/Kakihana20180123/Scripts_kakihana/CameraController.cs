
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GamepadInput;

public class CameraController : MonoBehaviour
{

    private const float Y_ANGLE_MIN = -89.0f;
    private const float Y_ANGLE_MAX = 89.0f;

    private const float X_ANGLE_MIN = -45.0f;
    private const float X_ANGLE_MAX = 45.0f;

    public RectTransform targetnam;
    public Transform target;
    public PlayerController player;
    public Vector3 offset;
    private Vector3 lookAt;

    public GamePad.Index padID;

    private float distance = 0.01f;
    private float distance_Min = 0.01f;
    private float distance_Max = 0.01f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;

    private float XSensitivity = 2.0f;
    private float YSensitivity = 2.0f;
    private float XKeySensitivity = 2.0f;
    // Use this for initialization
    void Start () {
        offset = new Vector3(0.0f, 1.8f, 0.0f);
    }
	
	// Update is called once per frame
	void Update ()
    {
        var Pad = GamePad.GetState(padID, false);
       //   if (Pad.LeftShoulder)
        if (Pad.RightShoulder)
        {
            currentX += XKeySensitivity;
        }
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
