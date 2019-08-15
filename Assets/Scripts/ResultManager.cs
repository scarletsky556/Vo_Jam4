using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ResultManager : MonoBehaviour
{
    public PlayableDirector ResultTL;

    public GameSceneManager sceneManager;

    public void ResulStart()
    {
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
        sceneManager.ToFinalResult();
    }
}