using UnityEngine.UI;
using UnityEngine;

public class PunGridSquareV2 : MonoBehaviour
{
    public TextMesh debuggerText;
    public SpriteRenderer hoveredImage, occupiedImage;
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
        if(newDiceType < SpritePooling.instance.diceSprites.Count){
            diceType = newDiceType;
            hoveredImage.sprite = SpritePooling.instance.diceSprites[diceType];
            occupiedImage.sprite = hoveredImage.sprite;
            debuggerText.text = diceType.ToString();
        } else {
            UnoccupySquare();
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(!_occupied && collider.tag == "Player"){
            _hovered = true;
            SetDiceType(collider.GetComponent<PunShapeSquareV2>().diceType);
            hoveredImage.gameObject.SetActive(true);
            GameEvents.OnHoverGridSquare(_i, _j);

            isRoot = collider.GetComponent<PunShapeSquareV2>().isRoot;
        }
    }

    void OnTriggerExit2D(Collider2D collider){
        if(!_occupied && collider.tag == "Player"){
            _hovered = false;
            _lastDiceType = diceType;
            diceType = 0;
            hoveredImage.gameObject.SetActive(false);
            GameEvents.OnUnhoverGridSquare();
            debuggerText.text = diceType.ToString();
        }
    }
}
