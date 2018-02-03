using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tape : MonoBehaviour
{
    RectTransform rt;

    bool tapeflg = false;

    // Use this for initialization
    void Start ()
    {
        rt = GetComponent<RectTransform>();
    }

    //Tapeの演出スクリプト
    void Update ()
    {
       if(tapeflg==true)
        {
            rt.sizeDelta = new Vector2(rt.sizeDelta.x + 500.0f, 200.0f);
            if (rt.sizeDelta.x >= 2300.0f)
            {
                rt.sizeDelta = new Vector2(2300.0f, 200.0f);
            }
        }
    }

    //tapeMasによる呼び出し
    public void Wakeflg()
    {
        tapeflg = true;
    }
}
