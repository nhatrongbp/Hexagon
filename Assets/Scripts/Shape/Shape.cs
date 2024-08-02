using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shape : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public Vector2 offset = new Vector2(0, 700);
    public int dirID;

    RectTransform _tranform;
    Canvas _canvas;
    ShapeSquare[] _shapeSquares;
    bool _isSwapping;

    void Awake(){
        _tranform = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        _shapeSquares = GetComponentsInChildren<ShapeSquare>();
        _isSwapping = false;
        //ResetPosition();
    }
    public void ResetPosition(){
        _tranform.anchorMin = new Vector2(.5f, .5f);
        _tranform.anchorMax = new Vector2(.5f, .5f);
        _tranform.pivot = new Vector2(.5f, .5f);
        _tranform.localPosition = new Vector2(0, 0);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _isSwapping = false;
        //Debug.Log("ScreenPoint: " + eventData.position.x + " " + eventData.position.y);
        _tranform.anchorMin = new Vector2(0, 0);
        _tranform.anchorMax = new Vector2(0, 0);
        _tranform.pivot = new Vector2(0, 0);
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform,
            eventData.position, Camera.main, out pos);
        //Debug.Log("ScreenPoint: " + pos.x + " " + pos.y);
        _tranform.localPosition = pos + offset;
        //Debug.Log("local point in rectangle: " + _tranform.localPosition.x + " " + _tranform.localPosition.y);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameEvents.OnShapeDropped(_shapeSquares.Length);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isSwapping = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(_isSwapping && _shapeSquares.Length > 1) {
            Debug.Log("swap shape");
            Vector2 vector2 = _shapeSquares[0].GetComponent<RectTransform>().localPosition;
            _shapeSquares[0].GetComponent<RectTransform>().localPosition = _shapeSquares[1].GetComponent<RectTransform>().localPosition;
            _shapeSquares[1].GetComponent<RectTransform>().localPosition = vector2;

            Array.Reverse(_shapeSquares);
            // for (int i = 0; i < _shapeSquares.Length; i++)
            // {
            //     _shapeSquares[i].debuggerText.text = i.ToString();
            // }
        }
    }

    public void SetRootValueForShapeSquaresInChildren(){
        if(_shapeSquares.Length > 1 && _shapeSquares[0].diceType == _shapeSquares[1].diceType){
            _shapeSquares[0].SetRoot(true);
            _shapeSquares[1].SetRoot(false);
        }
        else {
            foreach (var item in _shapeSquares)
            {
                item.SetRoot(false);
            }
        }
    }
}
