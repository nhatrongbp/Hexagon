using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject shapePoolingPrefab;
    public Transform canvasParent;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate(shapePoolingPrefab.name, Vector2.zero, Quaternion.identity, 0);
    }
}
