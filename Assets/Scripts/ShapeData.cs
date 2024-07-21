using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu] [System.Serializable]
public class ShapeData : ScriptableObject
{
    [System.Serializable]
    public class Row{
        public bool[] column;
        int _size = 7;
        public Row(){}
        public Row(int size){
            _size = size;
            CreateRow();
        }
        public void CreateRow(){
            column = new bool[_size];
            ClearRow();
        }
        public void ClearRow(){
            for (int i = 0; i < _size; i++)
            {
                column[i] = false;
            }
        }
    }

    public int columns = 0;
    public int rows = 0;
    public Row[] board;
    public void Clear(){
        for (int i = 0; i < rows; i++)
        {
            board[i].ClearRow();
        }
    }
    public void CreateNewBoard(){
        board = new Row[rows];
        for (int i = 0; i < rows; i++)
        {
            board[i] = new Row(columns);
        }
    }
}
