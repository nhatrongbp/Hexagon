using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SingleplayerManager : MonoBehaviour
{
    public GameObject pauseScreen;
    // Start is called before the first frame update
    void Start()
    {
        pauseScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchPauseScreen(){
        pauseScreen.SetActive(!pauseScreen.activeSelf);
    }

    public void LoadScene(string str){
        SceneManager.LoadScene(str);
    }
}
