using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    public UnityEngine.UI.Text CorrectText;
    public UnityEngine.UI.Text WrongText;
    public void Result(int Correct,int Wrong)
    {
        CorrectText.text = Correct + "正解";
        WrongText.text = Wrong + "不正解";
    }
}
