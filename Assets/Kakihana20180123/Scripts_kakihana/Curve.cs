using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Curve{
    public float Horizontal = 0.33f;
    public float Vertical = 0.33f;
    public AnimationCurve charCurve = new AnimationCurve(
        new Keyframe(0.0f, 0.0f), new Keyframe(0.5f, 1.0f),
        new Keyframe(1.0f, 0.0f), new Keyframe(1.5f, -1.0f),
        new Keyframe(2.0f, 1.0f));


    public float hvRatio = 1.0f;

    private float cyclePosX;
    private float cyclePosY;
    private float interval;
    private Vector3 originalCameraPotiton;
    private float time;

    public void Setup(Camera camera,float baseInterval)
    {
        interval = baseInterval;
        originalCameraPotiton = camera.transform.localPosition;

        time = charCurve[charCurve.length - 1].time;
    }

    public Vector3 DoHead(float speed)
    {
        float posX = originalCameraPotiton.x + (charCurve.Evaluate(cyclePosX) * Horizontal);
        float posY = originalCameraPotiton.y + (charCurve.Evaluate(cyclePosY) * Vertical);

        cyclePosX += (speed * Time.deltaTime)/interval;
        cyclePosY += ((speed * Time.deltaTime) / interval) * Vertical;

        if (cyclePosX > time)
        {
            cyclePosX = cyclePosX - time;
        }
        if (cyclePosY > time)
        {
            cyclePosY = cyclePosY - time;
        }
        return new Vector3(posX, posY, 0.0f);
    }

}
