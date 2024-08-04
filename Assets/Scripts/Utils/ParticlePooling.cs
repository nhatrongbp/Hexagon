using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ParticlePooling : MonoBehaviour
{
    public static ParticlePooling instance;
    public List<ParticleSystem> particles, confetties;
    public List<CanvasGroup> scoreFX, praises;
    public float offset = .5f;

    void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
        else
        {
            instance = this;
            // DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        foreach (var p in particles) p.gameObject.SetActive(false);
        foreach (var p in confetties) p.gameObject.SetActive(false);
        foreach (var s in scoreFX) s.alpha = 0;
        foreach (var s in praises) s.alpha = 0;
    }

    // Update is called once per frame
    public void PlayParticle(int i, Vector2 pos, int score)
    {
        MyDebug.Log("play particle at {0} {1}", pos.x, pos.y);
        if (i < particles.Count)
        {
            particles[i].gameObject.SetActive(true);
            particles[i].transform.position = pos;
            particles[i].Play();
            scoreFX[i].gameObject.SetActive(true);
            scoreFX[i].GetComponent<TMP_Text>().text = "+" + score.ToString();
            scoreFX[i].GetComponent<RectTransform>().position = new Vector2(pos.x, pos.y + offset);
            scoreFX[i].GetComponent<RectTransform>().localScale = Vector2.zero;
            scoreFX[i].DOFade(1f, .1f).SetEase(Ease.InOutSine);
            scoreFX[i].GetComponent<RectTransform>().DOScale(1f, .1f).OnComplete(() => {
                scoreFX[i].DOFade(0f, .95f).SetEase(Ease.InOutSine);
                scoreFX[i].GetComponent<RectTransform>().DOMoveY(pos.y + offset * 2.75f, 1.25f).OnComplete(()=>{
                    scoreFX[i].gameObject.SetActive(false);
                });
            });
        }
    }

    public void PlayPraise(int i)
    {
        if(i > 11) i = 11;
        praises[i].gameObject.SetActive(true);
        praises[i].DOComplete();
        praises[i].GetComponent<RectTransform>().DOComplete();
        praises[i].GetComponent<RectTransform>().localPosition = Vector2.zero;
        // praises[i].GetComponent<RectTransform>().DOMoveY(0 + offset * 1.5f, 1.25f).SetEase(Ease.InOutSine);
        // praises[i].DOFade(1f, .75f).SetEase(Ease.InOutSine).OnComplete(() =>
        // {
        //     praises[i].DOFade(0f, .75f).SetEase(Ease.InOutSine);
        // });
        praises[i].GetComponent<RectTransform>().localScale = Vector2.zero;
        praises[i].DOFade(1f, .2f).SetEase(Ease.InOutSine);
        praises[i].GetComponent<RectTransform>().DOScale(1f, .2f).OnComplete(() => {
            praises[i].DOFade(0f, 1.5f).SetEase(Ease.InOutSine);
            praises[i].GetComponent<RectTransform>().DOMoveY(0 + offset * 1.2f, 1.75f).OnComplete(()=>{
                praises[i].gameObject.SetActive(false);
            });
        });
        AudioManager.instance.PlaySound(i);
    }

    public void PlayConfetti(){
        AudioManager.instance.PlaySound("Victory");
        foreach (var item in confetties)
        {
            item.gameObject.SetActive(true);
            item.Play();
        }
    }
}
