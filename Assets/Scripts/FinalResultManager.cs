using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalResultManager : MonoBehaviour
{
    public UnityEngine.UI.Text Score;
    public UnityEngine.UI.Text Num;
    public void DataSet(int c,int w)
    {
        Num.text = c.ToString();
        Score.text = (c * 10).ToString();
    }
}
