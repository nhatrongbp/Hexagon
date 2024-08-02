using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SingleScoreManager : MonoBehaviour
{
    public static SingleScoreManager instance;
    public TMP_Text scoreText, bestScoreText;
    public int score, bestScore;
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
        if(PlayerPrefs.HasKey("bestScore")) bestScore = PlayerPrefs.GetInt("bestScore");
        else bestScore = -1;
        bestScoreText.text = bestScore >= 0 ? bestScore.ToString() : "N/A";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int i){
        score += i;
        scoreText.text = score.ToString();
    }

    public bool UpdateBestScore(){
        if(score > bestScore) PlayerPrefs.SetInt("bestScore", score);
        return score > bestScore;
    }
}
