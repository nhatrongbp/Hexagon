using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SpawnerV2 : MonoBehaviour
{
    public GameObject applePrefab;
    // Start is called before the first frame update
    void Start()
    {
        if(PhotonNetwork.IsMasterClient){
            PhotonNetwork.Instantiate(applePrefab.name, Vector2.zero, Quaternion.identity, 0);
        }
        else{
            PhotonNetwork.Instantiate(applePrefab.name, Vector2.zero, Quaternion.identity, 0);
            Camera.main.transform.Rotate(0.0f, 0.0f, 180.0f, Space.Self);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
