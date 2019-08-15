using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    public GameObject title;

    public InGameManager inGame;

    public ResultManager result;

    public FinalResultManager final;

    public void Start()
    {
        inGame.Init();
    }

    public void ToGame()
    {
        title.SetActive(false);
        inGame.gameObject.SetActive(true);
        inGame.GameStart(0);
    }

    public void ToResult()
    {
        result.gameObject.SetActive(true);
    }

    public void ToFinalResult()
    {
        inGame.gameObject.SetActive(false);
        result.gameObject.SetActive(false);
        final.gameObject.SetActive(true);
    }
}
