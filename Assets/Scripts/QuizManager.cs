using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    public TextAsset WordCsv;
    List<WordGroupe> words = new List<WordGroupe>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}

public class WordGroupe
{
    public int difficult;
    public List<Word> wordList;
}

public class Word
{
    public string name;
    public string systemName;
}