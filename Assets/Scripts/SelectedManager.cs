using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SelectedManager : MonoBehaviour
{
    public PlayableDirector SelectedTL;

    public GameSceneManager sceneManager;

    public void SelectedStart()
    {
        StartCoroutine(SelectedStartCo());
    }

    IEnumerator SelectedStartCo()
    {
        SelectedTL.Play();

        yield return 0;

        //イントロ再生
        while (SelectedTL.state == PlayState.Playing)
        {

            yield return 0;
        }
        sceneManager.ToInGame();
    }
}
