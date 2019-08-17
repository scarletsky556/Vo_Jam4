using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalResultManager : MonoBehaviour
{
    public UnityEngine.UI.Text Score;
    public UnityEngine.UI.Text Num;
    string plusText;
    public void DataSet(int c)
    {
        Num.text = c.ToString();
        Score.text = (c * 10).ToString();
        if(c>=10)
        {
            plusText = "あなたはまさしくボイロ聖徳太子です！";
        }
        else if(c>=6)
        {
            plusText = "琴葉姉妹が鬼門！";
        }
        else if(c>=3)
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
                + "text=" + "ボイロ聖徳太子で"+Score.text+"点を取りました！"+plusText
                //+ "&url=" + linkUrl
                + "&hashtags=" + hashtags;

#if UNITY_EDITOR
            Application.OpenURL(url);
#else
            Application.OpenURL(url);
#endif
    }
}
