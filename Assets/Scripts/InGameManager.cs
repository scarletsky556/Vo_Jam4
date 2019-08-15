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

    public void GameStart(int diff)
    {
        StartCoroutine(GameStartCo(diff));
    }

    IEnumerator GameStartCo(int diff)
    {
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

        var quiz = quizManager.QuizCreate(difficult[diff].AnswerNum, difficult[diff].WrongNum);
        var pos = difficult[diff].AnswerPos.OrderBy(i => System.Guid.NewGuid()).ToList();
        for (int i = 0; i < difficult[diff].Character.Length; i++)
        {

            QuizText[i].DataSet(quiz[i].name, i);
            QuizText[i].transform.localPosition = new Vector3(pos[i].x, pos[i].y, 0);
            AnswerBox[i].AnswerId = i;
            voiceSource[i].clip = Resources.Load<AudioClip>("Sounds/QuizVoice/" + quiz[i].systemName + "_" + difficult[diff].Character[i]);
            Debug.Log(quiz[i].systemName + "_" + difficult[diff].Character[i]);
        }
        for (int i = difficult[diff].Character.Length; i < quiz.Length; i++)
        {
            QuizText[i].DataSet(quiz[i].name, i);
            QuizText[i].transform.localPosition = new Vector3(pos[i].x, pos[i].y, 0);
            Debug.Log("Wrong Answer:" + quiz[i].systemName);
        }


        introTL.Play();
        
        yield return 0;

        //イントロ再生
        while(introTL.state==PlayState.Playing)
        {
            
            yield return 0;
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

    public void AnswerCheck()
    {
        for(int i=0;i<AnswerBox.Length;i++)
        {
            if (AnswerBox[i].NowAnswer == "")
                return;
        }
        AnswerButton.gameObject.SetActive(true);
    }

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
        sceneManager.ToResult();
        result.ResulStart();
        FinalResult.DataSet(c, w);
    }
}
