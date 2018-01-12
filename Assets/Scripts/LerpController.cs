using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LerpController {
    public float Duration;
    public float Amount;

    private float offset = 0.0f;

    public float Offset()
    {
        return offset;
    }

    public IEnumerator Cycle()
    {
        float t = 0.0f;
        while(t < Duration)
        {
            offset = Mathf.Lerp(0.0f, Amount, t / Duration);
            t += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        t = 0.0f;
        while (t < Duration)
        {
            offset = Mathf.Lerp(Amount, 0.0f, t / Duration);
            t += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        offset = 0.0f;
    }
}
