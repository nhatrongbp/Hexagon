using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum DiceType{
    DiceZero, DiceOne, DiceTwo, DiceThree,
    DiceFour, DiceFive, DiceSix, DiceSeven, DiceKing
}
public class ShapeSquare : MonoBehaviour
{
    public int diceType;
    public Image _image;
    public TMP_Text isRootText;
    public bool isRoot= false;
    // Start is called before the first frame update
    void Awake()
    {
        _image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable(){
        GetRandomDiceType();
    }

    void GetRandomDiceType(){
        // diceType = Random.Range(1, DiceImagesStore.instance.diceSprites.Count);
        diceType = Random.Range(1, 4);
        try
        {
            //Debug.Log(diceType + " " + DiceImagesStore.instance.diceSprites.Count);
            _image.sprite = DiceImagesStore.instance.diceSprites[diceType];
            // Debug.Log(DiceImagesStore.instance.diceSprites[(int) DiceType].name);
            transform.parent.GetComponent<Shape>().SetRootValueForShapeSquaresInChildren();
        }
        catch (System.Exception)
        {
            Debug.Log("dice image not found");
            if(_image == null) Debug.Log("image == null");
        }
    }

    public void SetRoot(bool b){
        isRoot = b;
        isRootText.text = b ? "root" : "";
    }
}
