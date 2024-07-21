using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGrid : MonoBehaviour
{
    [SerializeField] GameObject gridSquare;
    public int columns = 7, rows = 7;
    // public float squareGap = .1f;
    // public float everySquareOffset = 0;

    float cellSize;
    Vector2 _startPos = new Vector2(0, 0);
    // Vector2 _offset = new Vector2(0, 0);
    List<GameObject> _gridSquares = new List<GameObject>();

    // Start is called before the first frame update
    void Awake()
    {
        cellSize = gridSquare.GetComponent<RectTransform>().rect.width;
        // _startPos.x = -cellSize * (columns / 2f - 1) - cellSize;
        // _startPos.y = cellSize * (rows / 2f - 1) + cellSize / 2;
        _startPos.x = -.9f * cellSize * (columns / 2f - 1) - .9f * cellSize;
        _startPos.y = 1.015f * cellSize * (rows / 2f - 1) + 1.015f * cellSize / 2;
        SpawnGridSquare();
    }

    void SpawnGridSquare()
    {
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                if (OutOfBounds(i, j)) continue;
                _gridSquares.Add(Instantiate(gridSquare, transform) as GameObject);
                // _gridSquares[_gridSquares.Count - 1].transform.SetParent(this.transform);
                // _gridSquares[_gridSquares.Count - 1].transform.localScale = new Vector3(1, 1, 1);

                var posXOffset = i * cellSize * .9f;
                var posYOffset = j * cellSize * 1.015f;
                if (j % 2 == 1) posXOffset += .9f * cellSize / 2;
                posYOffset += (rows / 2 - j) * .234f * cellSize;
                // _gridSquares[_gridSquares.Count - 1].GetComponent<RectTransform>().anchoredPosition 
                //     = new Vector2(_startPos.x + posXOffset, _startPos.y - posYOffset);
                _gridSquares[_gridSquares.Count - 1].GetComponent<RectTransform>().localPosition
                    = new Vector3(_startPos.x + posXOffset, _startPos.y - posYOffset, 0);
                _gridSquares[_gridSquares.Count - 1].GetComponent<MyGridSquare>().SetDebuggerText(i, j);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    bool OutOfBounds(int x, int y)
    {
        int diff = Mathf.Abs(y - rows / 2);
        float limitLeft = diff / 2f - .5f;
        int limitRight = columns - diff / 2;
        if (x <= limitLeft || limitRight <= x) return true;
        return false;
    }
}
