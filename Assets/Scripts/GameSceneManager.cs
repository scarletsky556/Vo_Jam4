using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    public GameObject title;

    public InGameManager inGame;

    public ResultManager result;

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
        inGame.gameObject.SetActive(false);
        result.gameObject.SetActive(true);
    }
}
