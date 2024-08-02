using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PunShapeSquare : MonoBehaviour
{
    public int diceType;
    public Image _image;
    public TMP_Text isRootText;
    public bool isRoot = false;
    public List<DiceProbRange> diceProbRange;
    PhotonView _view;

    void Awake()
    {
        _image = GetComponent<Image>();
        _view = GetComponent<PhotonView>();
    }
    
    void OnEnable(){
        // GetRandomDiceType(SingleScoreManager.instance.score);
        GetRandomDiceType(16);
    }

    void GetRandomDiceType(int score){
        // diceType = Random.Range(1, SpritePooling.instance.diceSprites.Count);
        if(_view.IsMine) {
            GetComponent<PhotonView>().RPC("SetDiceType", RpcTarget.All, 
                Random.Range(1, SpritePooling.instance.diceSprites.Count));
        }
    }

    [PunRPC] public void SetDiceType(int i){
        diceType = i;
        try
        {
            //Debug.Log(diceType + " " + SpritePooling.instance.diceSprites.Count);
            _image.sprite = SpritePooling.instance.diceSprites[diceType];
            // Debug.Log(SpritePooling.instance.diceSprites[(int) DiceType].name);
            transform.parent.GetComponent<PunShape>().SetRootValueForShapeSquaresInChildren();
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
