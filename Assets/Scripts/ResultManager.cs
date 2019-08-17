using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ResultManager : MonoBehaviour
{
    public PlayableDirector ResultTL;

    public FinalResultManager finalResult;

    public InGameManager gameManager;

    public PlayableDirector NextTL;

    public GameSceneManager sceneManager;

    public GameObject RoundObj;

    int[] correct= new int[3];

    int round;

    public UnityEngine.UI.Text RoundText;

    public void DataReset()
    {
        for(int i=0;i<3;i++)
        {
            correct[i] = 0;
        }
        round = 0;
    }

    public void ResulStart(int c,int round)
    {
        correct[round-1] = c;
        this.round = round;
        StartCoroutine(ResultCo());
    }

    IEnumerator ResultCo()
    {
        ResultTL.Play();

        yield return 0;

        //イントロ再生
        while (ResultTL.state == PlayState.Playing)
        {

            yield return 0;
        }
        if (round >= 3)
        {
            RoundObj.SetActive(false);
            finalResult.DataSet(correct);
            sceneManager.ToFinalResult();
        }
        else
        {
            gameManager.AnswerReset();
            yield return new WaitForSeconds(0.1f);
            gameManager.AnswerAnActive();
            round++;
            RoundText.text = (round).ToString();
            NextTL.Play();
            yield return 0;
            while (NextTL.state == PlayState.Playing)
            {

                yield return 0;
            }
            
            gameManager.GameStart(round);
        }
        
    }
}