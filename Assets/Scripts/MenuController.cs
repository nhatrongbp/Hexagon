using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public List<Transform> buttons;
    public GameObject settingPanel;
    List<Sequence> sequences = new List<Sequence>();
    // Start is called before the first frame update
    void Start()
    {
        settingPanel.SetActive(false);
        //Debug.Log("photon status: " + PhotonNetwork.IsConnected);
        AnimateButtons();
    }

    // Update is called once per frame
    void AnimateButtons()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].localScale = Vector2.zero;
            AnimateButton(i, .3f * i);
        }
    }
    void AnimateButton(int i, float delay){
        if(sequences.Count >= i){
            sequences.Add(DOTween.Sequence());
        } else{
            if(sequences[i].IsPlaying()){
                sequences[i].Kill(true);
            }
        }
        var seq = sequences[i];
        var button = buttons[i];
        seq.Append(button.DOScale(1, .1f));
        seq.Append(button.DOPunchScale(Vector2.one * .3f, .5f, 2, 1f).SetEase(Ease.OutCirc));
        seq.PrependInterval(delay);
    }

    public void ShowSettingPanel(){
        settingPanel.SetActive(!settingPanel.activeSelf);
    }

    public void LoadScene(string str){
        SceneManager.LoadScene(str);
    }
}
