using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuizAnswerBox : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public QuizChoicePrefab ChoicePrefab;
    private Sprite nowSprite;

    string NowAnswer;

    public Image BoxImage;

    void Start()
    {
        nowSprite = null;
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
        ChoicePrefab.Itemtext.text = droppedAnswer.Itemtext.text;
        NowAnswer = droppedAnswer.Itemtext.text;
        BoxImage.color = Color.white;
    }
}