using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Playables;
public class InGameManager : MonoBehaviour
{
    class DiffcultSetting
    {
        public int AnswerNum;
        public int WrongNum;
        public string[] Character;
        public List<Vector2> AnswerPos;
    }
    List<DiffcultSetting> difficult = new List<DiffcultSetting>();

    public PlayableDirector introTL;

    public PlayableDirector AnswerTL;

    public UnityEngine.UI.Button AnswerButton;

    public ResultManager result;

    public GameSceneManager sceneManager;

    public QuizChoice[] QuizText;

    public QuizAnswerBox[] AnswerBox;

    public QuizManager quizManager;

    public Animator[] character;

    public AudioSource[] voiceSource;

    public GameObject QFukidashi;

    public FinalResultManager FinalResult;

    // Start is called before the first frame update
    public void Init()
    {
        quizManager.Initialized();
        
        difficult.Add(new DiffcultSetting
        {
            //Easy
            AnswerNum = 4,
            WrongNum = 1,
            Character = new string[] { "yukari", "maki", "aoi", "akane" },
            AnswerPos = new List<Vector2>(new Vector2[] {new Vector2(-112,0),new Vector2(-56,0),new Vector2(0,0),new Vector2(56,0),new Vector2(112,0) })
            
        });
        difficult.Add(new DiffcultSetting
        {
            //Normal
            AnswerNum = 4,
            WrongNum = 1,
            Character = new string[] {"yukari","maki","aoi","akane"}
        });
        difficult.Add(new DiffcultSetting
        {
            //Hard
            AnswerNum = 6,
            WrongNum = 2,
            Character = new string[] { "yukari", "maki", "aoi", "akane", "akari", "kiritan" }
        });

        
    }

    public GameObject Round;
    public UnityEngine.UI.Text RoundText;

    public void GameStart(int round)
    {
        
        StartCoroutine(GameStartCo(round));
    }

    IEnumerator GameStartCo(int round)
    {
        Round.SetActive(true);
        NowRound = round;
        RoundText.text = round.ToString();
        //初期化
        AnswerButton.gameObject.SetActive(false);
        for (int i = 0; i < AnswerBox.Length; i++)
        {
            AnswerBox[i].gameObject.SetActive(false);
        }
        for(int i=0;i<QuizText.Length;i++)
        {
            QuizText[i].gameObject.SetActive(false);
        }
        QFukidashi.SetActive(false);
        yield return 0;

        var quiz = quizManager.QuizCreate(difficult[0].AnswerNum, difficult[0].WrongNum);
        var pos = difficult[0].AnswerPos.OrderBy(i => System.Guid.NewGuid()).ToList();
        for (int i = 0; i < difficult[0].Character.Length; i++)
        {

            QuizText[i].DataSet(quiz[i].name, i);
            QuizText[i].transform.localPosition = new Vector3(pos[i].x, pos[i].y, 0);
            AnswerBox[i].AnswerSet(i,quiz[i].name);
            voiceSource[i].clip = Resources.Load<AudioClip>("Sounds/QuizVoice/" + quiz[i].systemName + "_" + difficult[0].Character[i]);
            Debug.Log(quiz[i].systemName + "_" + difficult[0].Character[i]);
        }
        for (int i = difficult[0].Character.Length; i < quiz.Length; i++)
        {
            QuizText[i].DataSet(quiz[i].name, i);
            QuizText[i].transform.localPosition = new Vector3(pos[i].x, pos[i].y, 0);
            Debug.Log("Wrong Answer:" + quiz[i].systemName);
        }

        if(round==1)
        {
            introTL.Play();

            yield return 0;

            //イントロ再生
            while (introTL.state == PlayState.Playing)
            {

                yield return 0;
            }
        }
        
        
        //音声再生
        float maxClip=0;
        for(int i=0;i<voiceSource.Length;i++)
        {
            var l = voiceSource[i].clip.length;
            if (maxClip < l)
                maxClip = l;

            character[i].SetTrigger("correct");
            voiceSource[i].Play();
        }
        QFukidashi.SetActive(true);
        yield return new WaitForSeconds(maxClip+0.5f);

        QFukidashi.SetActive(false);
        for(int i=0;i<character.Length;i++)
        {
            character[i].SetTrigger("idle");
        }

        //回答モード再生
        AnswerTL.Play();


        for (int i = 0; i < QuizText.Length; i++)
        {
            QuizText[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < AnswerBox.Length; i++)
        {
            AnswerBox[i].gameObject.SetActive(true);
        }


    }

    public void IntroSkip()
    {
        introTL.time = 23.6f;
    }

    public void AnswerCheck()
    {
        for(int i=0;i<AnswerBox.Length;i++)
        {
            if (AnswerBox[i].NowAnswer == "")
                return;
        }
        AnswerButton.gameObject.SetActive(true);
    }

    int NowRound=0;

    public void Answer()
    {
        for (int i = 0; i < QuizText.Length; i++)
        {
            QuizText[i].gameObject.SetActive(false);
        }
        AnswerButton.gameObject.SetActive(false);
        int c=0,w = 0;
        for (int i = 0; i < AnswerBox.Length; i++)
        {
            if (AnswerBox[i].Id == AnswerBox[i].AnswerId)
            {
                c++;
                AnswerBox[i].CoMiSet(true);
            }
            else
            {
                w++;
                AnswerBox[i].CoMiSet(false);
            }
               
        }
        AnswerTL.Stop();
        AnswerTL.time = 0;
        sceneManager.ToResult();
        result.ResulStart(c,NowRound);
        //FinalResult.DataSet(c, w);
    }

    public void AnswerReset()
    {
        for (int i = 0; i < AnswerBox.Length; i++)
        {
            AnswerBox[i].gameObject.SetActive(false);
        }
    }
    
}
