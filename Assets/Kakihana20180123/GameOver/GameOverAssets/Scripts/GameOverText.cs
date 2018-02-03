using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverText : MonoBehaviour {

    public Image DrawText;//GameOverイラスト

    bool fadeout;//フェードアウト用フラグ

    float alpha;

    // Use this for initialization
    void Start()
    {
        alpha = 0.0f;

        fadeout = false;

        this.transform.SetAsLastSibling();//最前列へ

        // 画像のアルファ値をセット
        DrawText.color = new Color(DrawText.color.r, DrawText.color.g, DrawText.color.b, alpha);

    }

    //tapeMasによる呼び出し
    public void TextWake()
    {
        fadeout = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeout == true)
        {
            alpha += 0.1f;
            DrawText.color = new Color(DrawText.color.r, DrawText.color.g, DrawText.color.b, alpha);
            if (alpha == 1.0f)
            {
                alpha = 1.0f;
                fadeout = false;
            }
        }
    }
}
