using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]

// ドラッグとドロップに関するインターフェースを実装する
public class QuizChoice : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public Text Itemtext;
    [System.NonSerialized]
    public int id;

    public QuizChoicePrefab ChoiceBox;

    public void DataSet(string name,int id)
    {
        Itemtext.text = name;
        this.id = id;
    }

    private Transform canvasTran;
    private QuizChoicePrefab draggingObject;

    void Awake()
    {
        canvasTran = transform.parent.parent;
    }

    public void OnBeginDrag(PointerEventData pointerEventData)
    {
        CreateDragObject();
        var pos = Camera.main.ScreenToWorldPoint(pointerEventData.position);
        pos.z = 0;
        draggingObject.transform.position = pos;
        
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        var pos = Camera.main.ScreenToWorldPoint(pointerEventData.position);
        pos.z = 0;
        draggingObject.transform.position = pos;
    }

    public void OnEndDrag(PointerEventData pointerEventData)
    {
        gameObject.GetComponent<Image>().color = Vector4.one;
        Destroy(draggingObject.gameObject);
    }

    // ドラッグオブジェクト作成
    private void CreateDragObject()
    {
        draggingObject = Instantiate(ChoiceBox);
        draggingObject.transform.SetParent(canvasTran);
        draggingObject.transform.SetAsLastSibling();
        draggingObject.transform.localScale = Vector3.one;

        // レイキャストがブロックされないように
        CanvasGroup canvasGroup = draggingObject.gameObject.AddComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = false;

        draggingObject.Itemtext.text = Itemtext.text;

        gameObject.GetComponent<Image>().color = Vector4.one * 0.6f;
    }
}