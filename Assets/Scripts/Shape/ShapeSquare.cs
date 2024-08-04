using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct DiceProbRange
{
    public int lo, hi;
    public float[] prob;
}

public class ShapeSquare : MonoBehaviour
{
    public int diceType;
    public TMP_Text isRootText;
    public bool isRoot = false;
    public List<DiceProbRange> diceProbRange;
    Image _image;
    
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
        GetRandomDiceType(SingleScoreManager.instance.score);
    }

    void GetRandomDiceType(int score){
        // diceType = Random.Range(1, SpritePooling.instance.diceSprites.Count);
        // diceType = Random.Range(1, 4);
        foreach (var item in diceProbRange)
        {
            if ((item.lo <= score && score <= item.hi) || item.hi == -1)
            {
                //get random shape level (easy, medium, hard)
                diceType = 0;
                do diceType = Utils.GetRandomIndexWithCustomProb(item.prob);
                while(diceType == 0); //diceType = 8;
                break;
            }
        }
        try
        {
            //Debug.Log(diceType + " " + SpritePooling.instance.diceSprites.Count);
            _image.sprite = SpritePooling.instance.diceSprites[diceType];
            // Debug.Log(SpritePooling.instance.diceSprites[(int) DiceType].name);
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
