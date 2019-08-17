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

    public Animator animator;

    public Text CorrectText;

    public void AnimatorReset()
    {
        animator.SetTrigger("Return");
    }

    public void AnswerSet(int id,string Ctext)
    {
        
        NowAnswer = "";
        ChoicePrefab.gameObject.SetActive(false);
        AnswerId = id;
        CorrectText.text = Ctext;
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
            animator.SetTrigger("Correct");
        else
            animator.SetTrigger("Miss");
    }
}