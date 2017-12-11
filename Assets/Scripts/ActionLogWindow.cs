using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ActionLogWindow : MonoBehaviour {

    public static string log = "";
    public static string oldLog = "";

    ScrollRect scrollrect;

    public Text textLog;


	// Use this for initialization
	void Start () {
        scrollrect = this.gameObject.GetComponent<ScrollRect>();
        textLog = scrollrect.content.GetComponentInChildren<Text>();		
	}
	
	// Update is called once per frame
	void Update () {
        if (scrollrect != null && log != oldLog)
        {
            textLog.text = log;
            StartCoroutine(DelayLog(5, () =>
             {
                 scrollrect.verticalNormalizedPosition = 0;
             }
            ));
        }
	}

    public static void Log(string logText)
    {
        log += (logText + "\n");
    }

    IEnumerator DelayLog(int delayFrame,UnityAction action)
    {
        for (int i = 0; i < delayFrame; i++)
        {
            yield return null;
        }
        action();
    }

}
