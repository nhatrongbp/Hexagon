using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;

public class PunShapeV2 : MonoBehaviour
{
    PunShapeSquareV2[] _shapeSquares;
    BoxCollider2D[] _shapeSquaresColliders;
    PhotonView _view;
    Camera _cam;
    void OnEnable(){
        // GameEvents.OnTurnChanged += (b) => ReceiveTurnChanged(b);
        foreach (var item in _shapeSquares)
        {
            item.enabled = true;
        }
    }
    void OnDisable(){
        // GameEvents.OnTurnChanged -= (b) => ReceiveTurnChanged(b);
        foreach (var item in _shapeSquares)
        {
            item.enabled = false;
        }
    }
    // void ReceiveTurnChanged(bool isMasterClient){
    //     MyDebug.Log("ReceiveTurnChanged: {0}", isMasterClient);
    //     if(_view.Owner.IsMasterClient == isMasterClient){
    //         foreach (var item in _shapeSquares)
    //         {
    //             item.GetComponent<SpriteRenderer>().color = Color.white;
    //         }
    //     } else{
    //         foreach (var item in _shapeSquares)
    //         {
    //             item.GetComponent<SpriteRenderer>().color = new Color(
    //                 255, 255, 255, 100
    //             );
    //         }
    //     }
    // }
    void Awake(){
        _shapeSquares = GetComponentsInChildren<PunShapeSquareV2>();
        _shapeSquaresColliders = GetComponentsInChildren<BoxCollider2D>();
        _view = GetComponent<PhotonView>();
        _cam = Camera.main;
        ResetPosition();
    }
    void Start(){
        if(!_view.IsMine)
        _shapeSquaresColliders[0].enabled = false;
        // foreach (var item in _shapeSquares)
        // {
        //     item.enabled = false;
        // }
    }
    public void ResetPosition(){
        transform.localPosition = Vector2.zero;
    }

    public void OnMouseUp()  {
        //ResetPosition();
        Invoke("SendOnShapeDropped", .2f);
        //GameEvents.OnShapeDropped(_shapeSquares.Length);
        //_view.RPC("ShowSquareColliders", RpcTarget.All, false);
    }

    void SendOnShapeDropped(){
        PunEvents.SendOnShapeDroppedEvent(PhotonNetwork.LocalPlayer.ActorNumber, _shapeSquares.Length);
        
    }

    public void OnMouseDown(){
        _view.RPC("ShowSquareColliders", RpcTarget.All, true);
    }

    public void OnMouseDrag()   {
        Vector2 mousePosition = _cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.Translate(mousePosition);  
    }

    [PunRPC] void ShowSquareColliders(bool b){
        for (int i = 1; i < _shapeSquaresColliders.Length; ++i)
        {
            _shapeSquaresColliders[i].enabled = b;
        }
    }

    public void SetRootValueForShapeSquaresInChildren(){
        // if(_shapeSquares.Length > 1 && _shapeSquares[0].diceType == _shapeSquares[1].diceType){
        //     _shapeSquares[0].SetRoot(true);
        //     _shapeSquares[1].SetRoot(false);
        // }
        // else {
        //     foreach (var item in _shapeSquares)
        //     {
        //         item.SetRoot(false);
        //     }
        // }
    }
}
