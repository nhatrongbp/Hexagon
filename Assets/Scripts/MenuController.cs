using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("photon status: " + PhotonNetwork.IsConnected);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(string str){
        SceneManager.LoadScene(str);
    }
}
