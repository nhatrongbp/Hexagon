using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SingleScoreManager : MonoBehaviour
{
    public static SingleScoreManager instance;
    public TMP_Text scoreText, bestScoreText;
    public int score, bestScore;
    public float duration = .5f;
    int countFPS = 30;
    Coroutine _countingCO;
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
        // PlayerPrefs.SetInt("bestScore", 69);
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
        if(_countingCO != null) {
            StopCoroutine(_countingCO);
            scoreText.text = (score-i).ToString();
        }
        _countingCO = StartCoroutine(CountTo(score - i, score));
        //scoreText.text = score.ToString();
        MyDebug.Log("add {0}, score: {1}", i, score);
    }

    IEnumerator CountTo(int prevVal, int newVal){
        WaitForSeconds wait = new WaitForSeconds(1f/countFPS);
        int stepAmount;
        if(newVal - prevVal < 0){
            stepAmount = Mathf.FloorToInt((newVal - prevVal) / (countFPS * duration));
        } else {
            stepAmount = Mathf.CeilToInt((newVal - prevVal) / (countFPS * duration));
        }
        if(prevVal < newVal){
            while(prevVal < newVal){
                prevVal += stepAmount;
                if(prevVal > newVal) prevVal = newVal;
                scoreText.text = prevVal.ToString();
                yield return wait;
            }
        } else{
            while(prevVal > newVal){
                prevVal += stepAmount;
                if(prevVal < newVal) prevVal = newVal;
                scoreText.text = prevVal.ToString();
                yield return wait;
            }
        }
        scoreText.text = score.ToString();
    }

    public bool UpdateBestScore(){
        if(score > bestScore) PlayerPrefs.SetInt("bestScore", score);
        return score > bestScore;
    }
}
