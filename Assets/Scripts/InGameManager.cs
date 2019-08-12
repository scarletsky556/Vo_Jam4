using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    class DiffcultSetting
    {
        public int AnswerNum;
        public int WrongNum;
        public string[] Character;
    }
    List<DiffcultSetting> difficult = new List<DiffcultSetting>();
    public QuizManager quizManager;
    // Start is called before the first frame update
    void Start()
    {
        quizManager.Initialized();
        
        difficult.Add(new DiffcultSetting
        {
            //Easy
            AnswerNum = 3,
            WrongNum = 2,
            Character = new string[]{"yukari","maki","akari"}
        });
        difficult.Add(new DiffcultSetting
        {
            //Normal
            AnswerNum = 4,
            WrongNum = 2,
            Character = new string[] {"yukari","maki","aoi","akane"}
        });
        difficult.Add(new DiffcultSetting
        {
            //Hard
            AnswerNum = 6,
            WrongNum = 2,
            Character = new string[] { "yukari", "maki", "aoi", "akane", "akari", "kiritan" }
        });

        GameStart(0);
    }

    public void GameStart(int diff)
    {
        var quiz = quizManager.QuizCreate(difficult[diff].AnswerNum, difficult[diff].WrongNum);
        for (int i = 0; i < difficult[diff].Character.Length;i++)
        {
            Debug.Log(quiz[i].systemName+"_"+difficult[diff].Character[i]);
        }
        for(int i= difficult[diff].Character.Length;i<quiz.Length;i++)
        {
            Debug.Log("Wrong Answer:"+quiz[i].systemName);
        }
        
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
