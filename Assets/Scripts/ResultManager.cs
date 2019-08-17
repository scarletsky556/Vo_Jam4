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

    int correct=0;

    int round;

    public void DataReset()
    {
        correct = 0;
        round = 0;
    }

    public void ResulStart(int c,int round)
    {
        correct = c;
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
            NextTL.Play();
            yield return 0;
            while (NextTL.state == PlayState.Playing)
            {

                yield return 0;
            }
            round++;

            gameManager.GameStart(round);
        }
        
    }
}