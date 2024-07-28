using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MyGridSquareSoul : MonoBehaviour
{
    public Image soul;
    Vector2 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        soul.gameObject.SetActive(false);
        startPosition = GetComponent<RectTransform>().localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateSoul(int diceType){
        soul.gameObject.SetActive(true);
        soul.sprite = SpritePooling.instance.diceSprites[diceType];
    }

    public void MoveTo(Vector2 target){
        //MyDebug.Log("move to {0} {1}", target.x, target.y);
        GetComponent<RectTransform>().DOLocalMove(target, .4f, false).SetUpdate(true).OnComplete(() => 
        {
            soul.gameObject.SetActive(false);
            GetComponent<RectTransform>().localPosition = startPosition;
        });
    }
}
