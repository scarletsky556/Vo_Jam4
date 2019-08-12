using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]

// ドラッグとドロップに関するインターフェースを実装する
public class QuizChoice : MonoBehaviour, IDragHandler
{
    private Transform canvasTran;
    private GameObject draggingObject;

    public Text text;
    [System.NonSerialized]
    public int id;


    public void DataSet(string name,int id)
    {
        text.text = name;
        this.id = id;
    }

    // ドラッグ前の位置
    private Vector2 prevPos;

    void Awake()
    {
        canvasTran = transform.parent.parent;
    }

    public RectTransform m_rectTransform = null;

    private void Reset()
    {
        m_rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData e)
    {
        m_rectTransform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        m_rectTransform.position = new Vector3(m_rectTransform.position.x, m_rectTransform.position.y, 0);
    }
}