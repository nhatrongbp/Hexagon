using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGrid : MonoBehaviour
{
    public int columns = 7, rows = 7;
    public ShapePooling shapePooling;
    [SerializeField] MyGridSquare gridSquare;
    [SerializeField] MyGridSquareSoul gridSquareSoul;
    float cellSize;
    Vector2 _startPos;
    bool[,] _visited;
    MyGridSquare[,] _gridSquares;
    MyGridSquareSoul[,] _gridSquareSouls;
    Queue<Tuple<int, int>> _hoveringSquare;
    int[,] _dx = new int[2, 7] {
        {0,  1,  0,  -1, -1, -1, 0},
        {1, 1,  1,  0,  -1, 0, 0}
    };
    int[] _dy = new int[7] { -1, 0, 1, 1, 0, -1,0 };
    List<Tuple<int, int>> _squaresUnoccupied;

    // Start is called before the first frame update
    void Awake()
    {
        cellSize = gridSquare.GetComponent<RectTransform>().rect.width;
        // _startPos.x = -cellSize * (columns / 2f - 1) - cellSize;
        // _startPos.y = cellSize * (rows / 2f - 1) + cellSize / 2;
        _startPos.x = -.9f * cellSize * (columns / 2f - 1) - .9f * cellSize;
        _startPos.y = 1.015f * cellSize * (rows / 2f - 1) + 1.015f * cellSize / 2;
        SpawnGridSquare();
        SpawnGridSquareSoul();
        _hoveringSquare = new Queue<Tuple<int, int>>();
    }

    void SpawnGridSquare()
    {
        _gridSquares = new MyGridSquare[columns, rows];
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                if (OutOfBounds(i, j)) continue;
                _gridSquares[i, j] = Instantiate(gridSquare, transform);

                var posXOffset = i * cellSize * .9f;
                var posYOffset = j * cellSize * 1.015f;
                if (j % 2 == 1) posXOffset += .9f * cellSize / 2;
                posYOffset += (rows / 2 - j) * .234f * cellSize;
                // _gridSquares[_gridSquares.Count - 1].GetComponent<RectTransform>().anchoredPosition 
                //     = new Vector2(_startPos.x + posXOffset, _startPos.y - posYOffset);
                _gridSquares[i, j].GetComponent<RectTransform>().localPosition
                    = new Vector3(_startPos.x + posXOffset, _startPos.y - posYOffset, 0);
                _gridSquares[i, j].SetIndex(i, j);
            }
        }
    }

    void SpawnGridSquareSoul(){
        _gridSquareSouls = new MyGridSquareSoul[columns, rows];
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                if (OutOfBounds(i, j)) continue;
                _gridSquareSouls[i, j] = Instantiate(gridSquareSoul, transform);
                _gridSquareSouls[i, j].GetComponent<RectTransform>().localPosition
                    = _gridSquares[i, j].GetComponent<RectTransform>().localPosition;
            }
        }
    }

    // Update is called once per frame
    void OnDisable()
    {
        GameEvents.OnShapeDropped -= CheckIfShapeCanBePlaced;
        GameEvents.OnHoverGridSquare -= (i, j) => _hoveringSquare.Enqueue(new Tuple<int, int>(i, j));
        GameEvents.OnUnhoverGridSquare -= () => _hoveringSquare.Dequeue();
    }

    void OnEnable()
    {
        GameEvents.OnShapeDropped += CheckIfShapeCanBePlaced;
        GameEvents.OnHoverGridSquare += (i, j) => _hoveringSquare.Enqueue(new Tuple<int, int>(i, j));
        GameEvents.OnUnhoverGridSquare += () => _hoveringSquare.Dequeue();
        // this.RegisterListener(EventID.OnPlaceShape, (param) => CheckIfShapeCanBePlaced((int) param));
    }

    void CheckIfShapeCanBePlaced(int shapeSize)
    {
        // MyDebug.Log("you has dragged and dropped a shape with size {0}", shapeSize);
        if (_hoveringSquare.Count == shapeSize)
        {
            // Debug.Log("yes you can place down this shape");
            foreach (var mStartSquare in _hoveringSquare)
            {
                // MyDebug.Log("{0} {1}", mStartSquare.Item1, mStartSquare.Item2);
                _gridSquares[mStartSquare.Item1, mStartSquare.Item2].OccupySquare();
            }
            shapePooling.FinishDroppingAboveShape(true);

            //bfs
            // Debug.Log(_hoveringSquare.Count);
            StartCoroutine(UnoccupyAdjacentSquares());
        }
        else
        {
            // Debug.Log("no you can not, pls try again");
            shapePooling.FinishDroppingAboveShape(false);
        }
    }

    IEnumerator UnoccupyAdjacentSquares()
    {
        bool destroyedSomeSquares = true;
        int squaresUnoccupiedCount = 0; //praise if player unoccupied from 3 to 11 squares in a row
        int multiplier = 1;
        int scoresEarned = 0;
        while (destroyedSomeSquares)
        {
            //yield return new WaitForSeconds(1f);
            destroyedSomeSquares = false;
            _visited = new bool[columns, rows];

            Tuple<int,int>[] _hoveringSquareArray = _hoveringSquare.ToArray();
            if(_hoveringSquareArray.Length > 1 && 
                _gridSquares[_hoveringSquareArray[0].Item1, _hoveringSquareArray[0].Item2].diceType > 
                _gridSquares[_hoveringSquareArray[1].Item1, _hoveringSquareArray[1].Item2].diceType){
                    Array.Reverse(_hoveringSquareArray);
                }
            if(_hoveringSquareArray.Length > 1 && 
                _gridSquares[_hoveringSquareArray[0].Item1, _hoveringSquareArray[0].Item2].diceType == 
                _gridSquares[_hoveringSquareArray[1].Item1, _hoveringSquareArray[1].Item2].diceType
                && _gridSquares[_hoveringSquareArray[1].Item1, _hoveringSquareArray[1].Item2].isRoot){
                    Array.Reverse(_hoveringSquareArray);
                }

            foreach (var mStartSquare in _hoveringSquareArray)
            {
                // if (!_visited[mStartSquare.Item1, mStartSquare.Item2])
                // {
                    bfs(mStartSquare.Item1, mStartSquare.Item2,
                            _gridSquares[mStartSquare.Item1, mStartSquare.Item2].diceType);
                    if (_squaresUnoccupied.Count > 1)
                    {
                        squaresUnoccupiedCount += _squaresUnoccupied.Count;
                        if(_gridSquares[mStartSquare.Item1, mStartSquare.Item2].diceType == 8){
                            ++squaresUnoccupiedCount;
                        }
                        yield return new WaitForSeconds(.2f);
                        destroyedSomeSquares = true;
                        MyDebug.Log("_squaresUnoccupied: {0}", _squaresUnoccupied.Count);
                        foreach (var item in _squaresUnoccupied)
                        {
                            _gridSquareSouls[item.Item1, item.Item2].ActivateSoul(_gridSquares[item.Item1, item.Item2].diceType);
                            _gridSquares[item.Item1, item.Item2].UnoccupySquare();

                            //TODO: start moving the soul to the position [mStartSquare.Item1, mStartSquare.Item2]
                            _gridSquareSouls[item.Item1, item.Item2].MoveTo(
                                _gridSquares[mStartSquare.Item1, mStartSquare.Item2].GetComponent<RectTransform>().localPosition);
                        }
                        //_squaresUnoccupied.Clear();

                        //TODO: wait the soul complete moving
                        yield return new WaitForSeconds(.3f);

                        ParticlePooling.instance.PlayParticle(
                            _gridSquares[mStartSquare.Item1, mStartSquare.Item2].diceType + 1,
                            _gridSquares[mStartSquare.Item1, mStartSquare.Item2].GetComponent<RectTransform>().position,
                            _squaresUnoccupied.Count * 
                            _gridSquares[mStartSquare.Item1, mStartSquare.Item2].diceType * multiplier
                        );
                        SingleScoreManager.instance.AddScore(_squaresUnoccupied.Count * 
                            _gridSquares[mStartSquare.Item1, mStartSquare.Item2].diceType* multiplier);
                        scoresEarned += _squaresUnoccupied.Count * 
                            _gridSquares[mStartSquare.Item1, mStartSquare.Item2].diceType* multiplier;

                        yield return new WaitForSeconds(.1f);

                        _gridSquares[mStartSquare.Item1, mStartSquare.Item2].SetDiceType(
                            _gridSquares[mStartSquare.Item1, mStartSquare.Item2].diceType + 1);
                        AudioManager.instance.PlaySound("Merge1");

                        _squaresUnoccupied.Clear();

                        ++multiplier;

                        //we bfs above point again because it can possibly be lower than next point
                        break;
                    }

                    _visited[mStartSquare.Item1, mStartSquare.Item2] = false;
                // }
            }
            if (destroyedSomeSquares)
            {
                // yield return new WaitForSeconds(1f);
                Debug.Log("keep calm, there are more squares need to be destroyed");
            }
        }
        
        if(squaresUnoccupiedCount > 2){
            ParticlePooling.instance.PlayPraise(
                squaresUnoccupiedCount+multiplier-1+scoresEarned/18);
        }
        Debug.Log("ok, now there is no square to be destroyed, player can take new turn");
        shapePooling.GenerateRandomShape();
        _hoveringSquare.Clear();
    }

    void bfs(int mStartX, int mStartY, int diceType)
    {
        //return true if can destroy some squares, else return false
        if (diceType == 0) return ;
        Queue<Tuple<int, int>> q = new Queue<Tuple<int, int>>();
        q.Enqueue(new Tuple<int, int>(mStartX, mStartY));
        _visited[mStartX, mStartY] = true;
        Tuple<int, int> u;
        int newX, newY;
        _squaresUnoccupied = new List<Tuple<int, int>>();
        while (q.Count > 0)
        {
            u = q.Dequeue();
            if (u.Item1 != mStartX || u.Item2 != mStartY)
            {
                //if this square is not the start square, then we may (or not) "unoccupy" it
                //_gridSquares[u.Item1, u.Item2].UnoccupySquare();
                _squaresUnoccupied.Add(new Tuple<int, int>(u.Item1, u.Item2));
            }
            for (int i = 0; i < 6; i++)
            {
                newX = u.Item1 + _dx[u.Item2 % 2, i];
                newY = u.Item2 + _dy[i];
                if (!OutOfBounds(newX, newY) && !_visited[newX, newY]
                    && _gridSquares[newX, newY].diceType == diceType)
                {
                    _visited[newX, newY] = true;
                    q.Enqueue(new Tuple<int, int>(newX, newY));
                }
            }
        }
    }

    public bool IsGameOver(int dirID){
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                if(!OutOfBounds(i, j) && !OutOfBounds(i + _dx[j % 2, dirID], j + _dy[dirID])
                    && !_gridSquares[i, j].occupied 
                    && !_gridSquares[i + _dx[j % 2, dirID], j + _dy[dirID]].occupied){
                        return false;
                }
            }
        }
        return true;
    }

    bool OutOfBounds(int x, int y)
    {
        if (x < 0 || x >= columns || y < 0 || y >= rows) return true;
        int diff = Mathf.Abs(y - rows / 2);
        float limitLeft = diff / 2f - .5f;
        int limitRight = columns - diff / 2;
        if (x <= limitLeft || limitRight <= x) return true;
        return false;
    }
}
