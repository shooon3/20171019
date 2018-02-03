using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapeMas : MonoBehaviour
{
    public Image tape1;//上から順番に
    public Image tape2;
    public Image tape3;
    public Image tape4;

    public Image DrawImage; //GameOverイラスト

    AudioSource tapeAS;//tape用SE
    AudioSource GameOverAS;//GameOver用SE

    //float WaitTime = 0.25f;//本処理用待機時間

    // Use this for initialization
    void Start ()
    {
        StartCoroutine("BarWake");

        AudioSource[] AS = GetComponents<AudioSource>();
        tapeAS = AS[0];
        GameOverAS = AS[1];
    }

    //コルーチンで順番に出現させる。
    private IEnumerator BarWake()
    {
        yield return new WaitForSeconds(0.2f);//-----|
        tapeAS.PlayOneShot(tapeAS.clip);//-----------}まとめてtape1つ分
        tape1.SendMessage("Wakeflg");//--------------|
        yield return new WaitForSeconds(0.2f);
        tapeAS.PlayOneShot(tapeAS.clip);
        tape2.SendMessage("Wakeflg");
        yield return new WaitForSeconds(0.2f);
        tapeAS.PlayOneShot(tapeAS.clip);
        tape3.SendMessage("Wakeflg");
        yield return new WaitForSeconds(0.2f);
        tapeAS.PlayOneShot(tapeAS.clip);
        tape4.SendMessage("Wakeflg");
        yield return new WaitForSeconds(0.2f);//-------------|
        GameOverAS.PlayOneShot(GameOverAS.clip);//-----------}GameOver処理部分
        DrawImage.SendMessage("TextWake");//-----------------|
    }
}

