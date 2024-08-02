using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SingleplayerManager : MonoBehaviour
{
    public static SingleplayerManager instance;
    public GameObject pauseScreen, gameOverScreen;
    public RectTransform gameOverPanel;
    public TMP_Text newBestScoreText, bestScoreText;

    void Awake(){
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        pauseScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        //ShowGameOverScreen();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowPauseScreen(){
        pauseScreen.SetActive(!pauseScreen.activeSelf);
    }

    public void ShowGameOverScreen(){
        gameOverScreen.SetActive(true);
        gameOverPanel.localScale = Vector2.zero;
        gameOverPanel.DOScale(Vector2.one, .7f).SetEase(Ease.InOutSine);
        if(SingleScoreManager.instance.UpdateBestScore()){
            newBestScoreText.text = "new best score!";
        }
        else {
            newBestScoreText.text = "your score";
        }
        bestScoreText.text = SingleScoreManager.instance.score.ToString();
    }

    public void LoadScene(string str){
        SceneManager.LoadScene(str);
    }
}
