using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PunShapeSquareV2 : MonoBehaviour
{
    public int diceType = 1;
    public SpriteRenderer _spriteRenderer;
    public TextMesh isRootText;
    public bool isRoot;
    PhotonView _view;
    // Start is called before the first frame update
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void OnDisable(){
        // GetRandomDiceType(SingleScoreManager.instance.score);
        GetRandomDiceType(16);
    }

    void GetRandomDiceType(int score){
        // diceType = Random.Range(1, SpritePooling.instance.diceSprites.Count);
        if(_view.IsMine) {
            GetComponent<PhotonView>().RPC("SetDiceType", RpcTarget.All, 
                Random.Range(1, 5));
        }
    }
    [PunRPC] public void SetDiceType(int i){
        MyDebug.Log("SetDiceType: {0}", i);
        diceType = i;
        try
        {
            //Debug.Log(diceType + " " + SpritePooling.instance.diceSprites.Count);
            _spriteRenderer.sprite = SpritePooling.instance.diceSprites[diceType];
            // Debug.Log(SpritePooling.instance.diceSprites[(int) DiceType].name);
            //transform.parent.GetComponent<PunShape>().SetRootValueForShapeSquaresInChildren();
        }
        catch (System.Exception)
        {
            Debug.Log("dice image not found");
            if(_spriteRenderer == null) Debug.Log("_spriteRenderer == null");
        }
    }

    public void SetRoot(bool b){
        isRoot = b;
        isRootText.text = b ? "root" : "";
    }
}
