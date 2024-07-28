using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SingleScoreManager : MonoBehaviour
{
    public static SingleScoreManager instance;
    public TMP_Text scoreText;
    public int score;
    // Start is called before the first frame update
    void Awake()
    {
        score = 0;
        if(instance != null && instance != this) Destroy(gameObject);
        else {
            instance = this;
            // DontDestroyOnLoad(gameObject);
        }
    }

    void Start(){
        // score = 0;
        scoreText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int i){
        score += i;
        scoreText.text = score.ToString();
    }
}
