
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GamepadInput;

public class CameraController : MonoBehaviour
{

    private const float Y_ANGLE_MIN = -89.0f;
    private const float Y_ANGLE_MAX = 89.0f;

    //private const float X_ANGLE_MIN = -180.0f;
    //private const float X_ANGLE_MAX = 180.0f;

    public RectTransform targetnam;
    public Transform target;
    public PlayerController player;
    public Vector3 offset;
    private Vector3 lookAt;

    private float OffsetX = 1f;
    private float OffsetY = 2.5f;
    private float OffsetZ = 1f;


    public GamePad.Index padID;

    private float distance = 0.01f;
    private float distance_Min = 0.01f;
    private float distance_Max = 0.01f;
   [SerializeField] private float currentX = 0.0f;
    private float currentY = 0.0f;

    private float XSensitivity = 2.0f;
    private float YSensitivity = 2.0f;
    private float XKeySensitivity = 2.0f;
    // Use this for initialization
    void Start () {
        offset = new Vector3(0.0f, OffsetY, OffsetZ);
    }
	
	// Update is called once per frame
	void Update ()
    {
        var Pad = GamePad.GetState(padID, false);
        float RightStick = Pad.rightStickAxis.x;
        if (Pad.LeftShoulder||RightStick<-0.5f)
        {
            currentX += -XKeySensitivity;
        }
        if (Pad.RightShoulder || RightStick > 0.5f)
        {
            currentX += XKeySensitivity;
        }
        //if (currentX >= X_ANGLE_MAX)
        //{
        //    currentX = X_ANGLE_MAX;
        //}
        //else if (currentX <= X_ANGLE_MIN)
        //{
        //    currentX = X_ANGLE_MIN;
        //}
        //offset = new Vector3(player.charMove.x,OffsetY,player.charMove.z);
        if (/*currentX >= 90 &&*/ player.charMove.x > 0)
        {
            offset = new Vector3(OffsetX, OffsetY, 0);
        }
        if (/*currentX <= -90 &&*/ player.charMove.x < 0)
        {
            offset = new Vector3(-OffsetX, OffsetY, 0);
        }
        if (/*currentX >= 90 && player.charMove.z > 0)
        {
            offset = new Vector3(0, OffsetY, OffsetZ);
        }
        if (/*currentX <= -90 && */player.charMove.z < 0)
        {
            offset = new Vector3(0, OffsetY, -OffsetZ);
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
