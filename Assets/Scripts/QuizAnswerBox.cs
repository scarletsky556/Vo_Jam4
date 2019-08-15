using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuizAnswerBox : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public QuizChoicePrefab ChoicePrefab;

    public InGameManager manager;

    public string NowAnswer;

    [HideInInspector]
    public int Id;

    [HideInInspector]
    public int AnswerId;

    public Image BoxImage;

    public GameObject Correct;
    public GameObject Miss;

    public void AnswerSet(int id)
    {
        Correct.SetActive(false);
        Miss.SetActive(false);
        ChoicePrefab.gameObject.SetActive(false);
        AnswerId = id;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (pointerEventData.pointerDrag == null) return;
        QuizChoice droppedAnswer = pointerEventData.pointerDrag.GetComponent<QuizChoice>();
        //ChoicePrefab.Itemtext.text = droppedAnswer.Itemtext.text;
        BoxImage.color = Color.gray;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (pointerEventData.pointerDrag == null) return;

        if(NowAnswer=="")
        {
            ChoicePrefab.Itemtext.text = "";
        }
        else
        {
            ChoicePrefab.Itemtext.text = NowAnswer;
        }
        BoxImage.color = Color.white;
    }
    public void OnDrop(PointerEventData pointerEventData)
    {
        QuizChoice droppedAnswer = pointerEventData.pointerDrag.GetComponent<QuizChoice>();
        ChoicePrefab.gameObject.SetActive(true);
        ChoicePrefab.Itemtext.text = droppedAnswer.Itemtext.text;
        NowAnswer = droppedAnswer.Itemtext.text;
        BoxImage.color = Color.white;
        Id = droppedAnswer.id;

        manager.AnswerCheck();
    }

    public void CoMiSet(bool IsCorrect)
    {
        if (IsCorrect)
            Correct.SetActive(true);
        else
            Miss.SetActive(true);
    }
}