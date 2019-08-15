using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class QuizManager : MonoBehaviour
{
    public TextAsset WordCsv;
    List<WordGroupe> words = new List<WordGroupe>();

    //CSVの読み込み、問題グループの生成
    public void Initialized()
    {
        string txt = WordCsv.text;
        string[] wordlist = txt.Split('\n');
        for(int i=0;i<wordlist.Length;i++)
        {
            words.Add(new WordGroupe());
            var gtxt = wordlist[i].Split(',');
            words[i].difficult = int.Parse(gtxt[0]);
            words[i].wordList = new List<Word>();
            for (int x=1,j=0;x<gtxt.Length; x++,j++)
            {
                words[i].wordList.Add(new Word());
                words[i].wordList[j].name = gtxt[x];
                words[i].wordList[j].systemName = gtxt[x + 1];
                x++;
            }
        }
    }

    public Word[] QuizCreate(int AnswerNum,int WrongNum,int Type=0)
    {
        List<WordGroupe> quizList;
        if(Type==0)
        {
            quizList = words;
        }
        else
        {
            quizList = words.Where(x => x.difficult == Type).ToList();
        }
        if (quizList == null || quizList.Count == 0)
            return null;
        int q = Random.Range(0, quizList.Count);
        q = 0;
        var quiz = quizList[q].wordList.OrderBy(i => System.Guid.NewGuid()).Take(AnswerNum + WrongNum).ToArray();
        return quiz;
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