using System.Collections;
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
        //StartCoroutine(BestScoreEffect());
        // newBestScoreText.transform.DOShakePosition(1f, new Vector3(8f,0,0), 10, 0).SetEase(Ease.Linear).SetLoops(-1);
        // bestScoreText.transform.DOShakePosition(1.5f, new Vector3(8f,0,0), 10, 0).SetEase(Ease.Linear).SetLoops(-1);
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
            newBestScoreText.GetComponent<RectTransform>().DOShapeCircle(Vector2.zero, 2f, 2f);
            StartCoroutine(BestScoreEffect());
        }
        else {
            newBestScoreText.text = "your score";
        }
        bestScoreText.text = SingleScoreManager.instance.score.ToString();
    }

    IEnumerator BestScoreEffect(){
        while (true)
        {
            newBestScoreText.transform.DOScale(1.25f, 1f).OnComplete(() => {
                newBestScoreText.transform.DOScale(1f, 1f);
            });
            yield return new WaitForSeconds(2f);
        }
        
    }

    public void LoadScene(string str){
        SceneManager.LoadScene(str);
    }
}
