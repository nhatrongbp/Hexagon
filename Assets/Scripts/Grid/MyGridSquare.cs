using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class MyGridSquare : MonoBehaviour
{
    public TMP_Text debuggerText;
    public Image hoveredImage, occupiedImage;
    public int diceType;
    
    public bool _hovered;              //if we are hovering this square
    public bool _occupied;             //if this square was occupied
    public int _i, _j, _lastDiceType;
    public bool isRoot;

    // Start is called before the first frame update
    void Start()
    {
        UnoccupySquare();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnoccupySquare(){
        diceType = 0;
        _hovered = false; _occupied = false;
        hoveredImage.gameObject.SetActive(false);
        occupiedImage.gameObject.SetActive(false);
        debuggerText.text = diceType.ToString();
    }

    public void OccupySquare(){
        if(diceType == 0) {
            diceType = _lastDiceType;
            debuggerText.text = diceType.ToString();
        }
        occupiedImage.sprite = hoveredImage.sprite;
        hoveredImage.gameObject.SetActive(false);
        occupiedImage.gameObject.SetActive(true);
        _hovered = true; _occupied = true;
    }

    public void SetIndex(int i, int j){
        debuggerText.text = i.ToString() + ", " + j.ToString();
        debuggerText.text = diceType.ToString();
        _i = i; _j = j;
    }

    public void SetDiceType(int newDiceType){
        if(newDiceType < DiceImagesStore.instance.diceSprites.Count){
            diceType = newDiceType;
            hoveredImage.sprite = DiceImagesStore.instance.diceSprites[diceType];
            occupiedImage.sprite = hoveredImage.sprite;
            debuggerText.text = diceType.ToString();
        } else {
            UnoccupySquare();
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(!_occupied){
            _hovered = true;
            SetDiceType(collider.GetComponent<ShapeSquare>().diceType);
            hoveredImage.gameObject.SetActive(true);
            GameEvents.OnHoverGridSquare(_i, _j);

            isRoot = collider.GetComponent<ShapeSquare>().isRoot;
        }
    }

    void OnTriggerExit2D(Collider2D collider){
        if(!_occupied){
            _hovered = false;
            _lastDiceType = diceType;
            diceType = 0;
            hoveredImage.gameObject.SetActive(false);
            GameEvents.OnUnhoverGridSquare();
            debuggerText.text = diceType.ToString();
        }
    }
}
