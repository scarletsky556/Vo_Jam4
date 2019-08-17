using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalResultManager : MonoBehaviour
{
    public UnityEngine.UI.Text FullScore;
    public UnityEngine.UI.Text Q1;
    public UnityEngine.UI.Text Q2;
    public UnityEngine.UI.Text Q3;
    string plusText;
    public void DataSet(int[] c)
    {
        int sum = 0;
        Q1.text = c[0].ToString();
        sum += c[0];
        Q2.text = c[1].ToString();
        sum += c[1];
        Q3.text = c[2].ToString();
        sum += c[2];

        FullScore.text = (sum * 10).ToString();

        if(sum>=10)
        {
            plusText = "あなたはまさしくボイロ聖徳太子です！";
        }
        else if(sum>=6)
        {
            plusText = "琴葉姉妹が鬼門！";
        }
        else if(sum>=3)
        {
            plusText = "…１人くらいはわかる…？";
        }
        else
        {
            plusText = "お願いだから１人ずつ喋って！";
        }
    }
    
    //[SerializeField] string linkUrl = "http://negi-lab.blog.jp/";   // ツイートに挿入するURL
    [SerializeField] string hashtags = "ボイロ聖徳太子";        // ツイートに挿入するハッシュタグ

    public void ResultTweet()
    {
            var url = "https://twitter.com/intent/tweet?"
                + "text=" + "ボイロ聖徳太子で"+ FullScore.text+"点を取りました！"+plusText
                //+ "&url=" + linkUrl
                + "&hashtags=" + hashtags;

#if UNITY_EDITOR
            Application.OpenURL(url);
#else
            Application.OpenURL(url);
#endif
    }
}
