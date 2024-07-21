using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shape : MonoBehaviour, IPointerClickHandler, IPointerUpHandler, 
                    IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    public GameObject squareShapeImage;

    // [HideInInspector] 
    public ShapeData currentShapeData;
    List<GameObject> _currentShape = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        //RequestNewShape(currentShapeData);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RequestNewShape(ShapeData shapeData){
        CreateShape(shapeData);
    }

    public void CreateShape(ShapeData shapeData){
        currentShapeData = shapeData;
        var totalSquareNumber = GetNumberOfSquares(shapeData);
        while(_currentShape.Count <= totalSquareNumber){
            _currentShape.Add(Instantiate(squareShapeImage, transform) as GameObject);
        }
        foreach (var square in _currentShape)
        {
            square.gameObject.transform.position = Vector3.zero;
            square.gameObject.SetActive(false);
        }

        var squareRect = squareShapeImage.GetComponent<RectTransform>();
        var moveDistance = new Vector2(
            squareRect.rect.width * squareRect.localScale.x,
            squareRect.rect.height * squareRect.localScale.y);

        int currentIndexInList = 0;
        //set position to form final shape
        for (int row = 0; row < shapeData.rows; row++)
        {
            for (int column = 0; column < shapeData.columns; column++)
            {
                if(shapeData.board[row].column[column]){
                    _currentShape[currentIndexInList].SetActive(true);
                    _currentShape[currentIndexInList].GetComponent<RectTransform>().localPosition = new Vector2(
                        GetXPositonForShapeSquare(shapeData, column, moveDistance), 
                        GetYPositonForShapeSquare(shapeData, row, moveDistance));

                    currentIndexInList++;
                }
            }
        }
    }

    private float GetYPositonForShapeSquare(ShapeData shapeData, int row, Vector2 moveDistance){
        float shiftOnY = 0f;
        if(shapeData.rows > 1){  //vertical position calculation
            if(shapeData.rows % 2 != 0){
                var middleSquareIndex = (shapeData.rows - 1) / 2;
                var multiplier = (shapeData.rows - 1) / 2;
                if(row < middleSquareIndex){ 
                    //move it on the up
                    shiftOnY = moveDistance.y * 1;
                    shiftOnY *= multiplier;
                } else if(row > middleSquareIndex){  
                    //move it on the down
                    shiftOnY = moveDistance.y * -1;
                    shiftOnY *= multiplier;
                }
            }else{
                var middleSquareIndex2 = (shapeData.rows == 2) ? 1 : (shapeData.rows / 2);
                var middleSquareIndex1 = (shapeData.rows == 2) ? 0 : (shapeData.rows - 2);
                var multiplier = shapeData.rows / 2;
                if(row == middleSquareIndex1 || row == middleSquareIndex2){ 
                    if(row == middleSquareIndex2){
                        shiftOnY = moveDistance.y / 2 * -1;
                    }
                    if(row == middleSquareIndex1){
                        shiftOnY = (moveDistance.y / 2);
                    }
                } 
                if(row < middleSquareIndex1 && row < middleSquareIndex2){  
                    //move it on the up
                    shiftOnY = moveDistance.y * 1;
                    shiftOnY *= multiplier;
                } else if(row > middleSquareIndex1 && row > middleSquareIndex2){
                    //move it on the down
                    shiftOnY = moveDistance.x * -1;
                    shiftOnY *= multiplier;
                }
            }
        }
        return shiftOnY;
    }

    private float GetXPositonForShapeSquare(ShapeData shapeData, int column, Vector2 moveDistance){
        float shiftOnX = 0f;
        if(shapeData.columns > 1){  //vertical position calculation
            if(shapeData.columns % 2 != 0){
                var middleSquareIndex = (shapeData.columns - 1) / 2;
                var multiplier = (shapeData.columns - 1) / 2;
                if(column < middleSquareIndex){ 
                    //move it on the left
                    shiftOnX = moveDistance.x * -1;
                    shiftOnX *= multiplier;
                } else if(column > middleSquareIndex){  
                    //move it on the right
                    shiftOnX = moveDistance.x * 1;
                    shiftOnX *= multiplier;
                }
            }else{
                var middleSquareIndex2 = (shapeData.columns == 2) ? 1 : (shapeData.columns / 2);
                var middleSquareIndex1 = (shapeData.columns == 2) ? 0 : (shapeData.columns - 1);
                var multiplier = shapeData.columns / 2;
                if(column == middleSquareIndex1 || column == middleSquareIndex2){ 
                    if(column == middleSquareIndex2){
                        shiftOnX = moveDistance.x / 2;
                    }
                    if(column == middleSquareIndex1){
                        shiftOnX = (moveDistance.x / 2) * -1;
                    }
                } 
                if(column < middleSquareIndex1 && column < middleSquareIndex2){  
                    //move it on the left
                    shiftOnX = moveDistance.x * -1;
                    shiftOnX *= multiplier;
                } else if(column > middleSquareIndex1 && column > middleSquareIndex2){
                    //move it on the right
                    shiftOnX = moveDistance.x * 1;
                    shiftOnX *= multiplier;
                }
            }
        }
        return shiftOnX;
    }

    int GetNumberOfSquares(ShapeData shapeData){
        int number = 0;
        foreach (var rowData in shapeData.board)
        {
            foreach (var active in rowData.column)
            {
                if(active) number++;
            }
        }
        return number;
    }

    #region Events
    public Vector3 shapeSelectedScale;
    public Vector2 offset = new Vector2(0, 700);
    Vector3 _shapeStartScale;
    RectTransform _tranform;
    bool _shapeDraggable;
    Canvas _canvas;
    void Awake(){
        //_shapeStartScale = this.GetComponent<RectTransform>().localScale;
        _tranform = this.GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        _shapeDraggable = true;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        // throw new System.NotImplementedException();

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //this.GetComponent<RectTransform>().localScale = shapeSelectedScale;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _tranform.anchorMin = new Vector2(0, 0);
        _tranform.anchorMax = new Vector2(0, 0);
        _tranform.pivot = new Vector2(0, 0);
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform,
            eventData.position, Camera.main, out pos);
        _tranform.localPosition = pos + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //this.GetComponent<RectTransform>().localScale = _shapeStartScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }
    #endregion
}
