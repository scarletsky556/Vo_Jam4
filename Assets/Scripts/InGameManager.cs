using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{

    public QuizManager quizManager;
    // Start is called before the first frame update
    void Start()
    {
        quizManager.Initialized();
        quizManager.QuizCreate(3, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
