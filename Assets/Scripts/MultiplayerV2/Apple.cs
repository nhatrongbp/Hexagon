using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Photon.Pun;
using UnityEngine;

public class Apple : MonoBehaviour
{
    PhotonView _view;

    void Awake()
    {
        _view = GetComponent<PhotonView>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_view.IsMine){
            if (Input.GetKey("up"))
            {
                // Move(transform.position + Vector3.up);
                PhotonView photonView = GetComponent<PhotonView>();
                photonView.RPC("Move", RpcTarget.All, transform.position + Vector3.up);
            }

            if (Input.GetKey("down"))
            {
                // Move(transform.position + Vector3.down);
                PhotonView photonView = GetComponent<PhotonView>();
                photonView.RPC("Move", RpcTarget.All, transform.position + Vector3.down);
            }
        }
    }

    [PunRPC] void Move(Vector3 dir){
        transform.DOMove(dir, .5f, false);
        // if(PhotonNetwork.IsMasterClient){
        //     PhotonView photonView = GetComponent<PhotonView>();
        //     photonView.RPC("Move", RpcTarget.Others, dir);
        // }
    }
}
