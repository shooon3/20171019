using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {
    public GameObject target;
    public Vector3 offset;
    MouseManager targetMouseManager;

    [SerializeField] private float distance = 4.0f;
    [SerializeField] private float angleY = 45.0f;
    [SerializeField] private float angleX = 45.0f;
    [SerializeField] private float minDistance = 1.0f;
    [SerializeField] private float maxDistance = 7.0f;
    [SerializeField] private float minRotAngle = 5.0f;
    [SerializeField] private float maxRotAngle = 75.0f;
    [SerializeField] public float XSensitivity = 0.0f;
    [SerializeField] public float YSensitivity = 0.0f;

    // Use this for initialization
    void Start () {
        target = GameObject.Find("PlayerController");
        XSensitivity = targetMouseManager.XSensitivity;
        YSensitivity = targetMouseManager.YSensitivity;
	}
	
    void LateUpdate()
    {
        //UpdateAngle(targetMouseManager.Xrot, targetMouseManager.Yrot);
        var lookAtPos = target.transform.position + offset;
        UpdatePos(lookAtPos);
        transform.LookAt(lookAtPos);
    }

    void UpdateAngle(float x,float y)
    {
        x = angleX - x * XSensitivity;
        angleX = Mathf.Repeat(x, 360);
        y = angleY - y * YSensitivity;
        angleY = Mathf.Clamp(y, minRotAngle, maxRotAngle);
    }

    void UpdatePos(Vector3 lookPos)
    {
        var da = angleX * Mathf.Deg2Rad;
        var dp = angleY * Mathf.Deg2Rad;
        transform.position = new Vector3(
            lookPos.x + distance * Mathf.Sin(dp) * Mathf.Cos(da),
            lookPos.y + distance * Mathf.Cos(dp),
            lookPos.z + distance * Mathf.Cos(dp) * Mathf.Sin(da));
    }

}
