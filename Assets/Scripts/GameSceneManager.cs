using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    public GameObject title;

    public GameObject DifSelect;

    public SelectedManager selected;

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
        DifSelect.SetActive(true);
    }

    public void ToSelected()
    {
        DifSelect.SetActive(false);
        selected.gameObject.SetActive(true);
        selected.SelectedStart();
    }

    public void ToInGame()
    {
        selected.gameObject.SetActive(false);
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

    public void ToTitle()
    {
        final.gameObject.SetActive(false);
        title.SetActive(true);
    }
}
