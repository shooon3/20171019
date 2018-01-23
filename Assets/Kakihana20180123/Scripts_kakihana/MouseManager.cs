using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MouseManager {
    public float XSensitivity = 2f; // 水平方向のマウス感度
    public float YSensitivity = 2f; // 垂直方向のマウス感度
    public float maxNumX = 90.0f; // カメラの最小・最大回転角度
    public float minNumX = -90.0f;
    public bool smooth; // マウススムージングのフラグ
    public bool clampVerticalRotation = true;
    public float smoothTime = 5.0f; // マウススムージングのスピード
    public bool lockCursor = true; // カーソルを非表示にするかどうか

   [SerializeField] private Quaternion charaTargetRotation; // プレイヤーのクォータニオン
   [SerializeField] private Quaternion cameraTargetRotation; // カメラのクォータニオン
   [SerializeField] private Quaternion cameraYRotation;
    [SerializeField] private Quaternion rot;
    private bool isLockCursor = true; // 「今」カーソルが非表示になっているかどうか

    public void Init(Transform charactor,Transform camera)
    {
        // Playerクラスから送られたデータを格納
        charaTargetRotation = charactor.localRotation;
        cameraTargetRotation = camera.localRotation;
    }

    public void LookRotation(Transform charactor, Transform camera) // カメラ移動と回転のメソッド
    {
        float Yrot = Input.GetAxis("Mouse X") * YSensitivity; // マウスの入力と感度からカメラ回転スピードを保存
        float Xrot = Input.GetAxis("Mouse Y") * XSensitivity;

        charaTargetRotation *= Quaternion.Euler(0.0f, Yrot, 0.0f);

        cameraTargetRotation *= Quaternion.Euler(0.0f, Yrot, 0.0f);

        if (clampVerticalRotation)
        {
            cameraTargetRotation = ClampRotationAroundXAxis(cameraTargetRotation); // カメラの回転メソッド
        }

        if (smooth) // マウススムージングが有効なら
        {
            charactor.localRotation = Quaternion.Slerp(
                charactor.localRotation,
                charaTargetRotation,
                smoothTime * Time.deltaTime
                );
            camera.localRotation = Quaternion.Slerp(
                camera.localRotation,
                cameraTargetRotation,
                smoothTime * Time.deltaTime
                );
        }
        else
        {
            charactor.localRotation = charaTargetRotation;
            camera.localRotation = cameraTargetRotation;
        }

        UpdateCursorLock(); // カーソル表示切り替えメソッド

    }

    public void SetCursorLock(bool value)
    {
        lockCursor = value;
        if (!lockCursor)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void UpdateCursorLock()
    {
        if (lockCursor)
        {
            LockUpdate();
        }
    }

    private void LockUpdate()
    {
        // エスケープキーが押されたらマウスカーソルが表示される
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            isLockCursor = false; 
        }else if (Input.GetMouseButtonUp(0))
        {
            isLockCursor = true;
        }

        if (isLockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (!isLockCursor)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    Quaternion ClampRotationAroundXAxis(Quaternion q) // カメラ回転制限メソッド
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, minNumX, maxNumX);
        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);
        return q;
    }
    Quaternion ClampRotationAroundYAxis(Quaternion q) // カメラ回転制限メソッド
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleY = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.y);

        angleY = Mathf.Clamp(angleY, minNumX, maxNumX);
        q.y = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleY);
        return q;
    }
}
