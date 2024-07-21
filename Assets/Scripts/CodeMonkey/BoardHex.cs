using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHex : MonoBehaviour
{
    [SerializeField] Transform pfSquare;
    GridHex<GridObject> GridHex;
    public int width, height;
    public class GridObject{
    }
    void Awake()
    {
        GridHex = new GridHex<GridObject>(
            width, height, .9f, Vector3.zero
            , (GridHex<GridObject> g, int x, int y) => new GridObject()
        );
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if(!OutOfBounds(x, y)){
                    Transform cell = Instantiate(pfSquare, GridHex.GetWorldPosition(x, y), Quaternion.identity);
                    cell.parent = transform;
                    cell.GetComponentInChildren<TextMesh>().text = x.ToString() + ", " + y.ToString();
                    cell.GetComponentInChildren<TextMesh>().color = Color.green;
                } 
                // else {
                //     Transform cell = Instantiate(pfSquare, GridHex.GetWorldPosition(x, y), Quaternion.identity);
                //     cell.parent = transform;
                //     cell.GetComponentInChildren<TextMesh>().text = x.ToString() + ", " + y.ToString();
                //     cell.GetComponentInChildren<TextMesh>().color = Color.red;
                // }
            }
        }
    }

    bool OutOfBounds(int x, int y){
        int diff = Mathf.Abs(y - height/2);
        float limitLeft = diff / 2f - .5f;
        int limitRight = width - diff / 2;
        if(x <= limitLeft || limitRight <= x) return true;
        return false;
    }
}
