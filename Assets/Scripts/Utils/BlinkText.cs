using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class BlinkText : MonoBehaviour
{
    TMP_Text _Text;
    Color nullColor = new Color(255, 255, 255, 0);
    // Start is called before the first frame update
    void Start()
    {
        _Text = GetComponent<TMP_Text>();
        StartCoroutine(BlinkCO());
    }

    // Update is called once per frame
    IEnumerator BlinkCO()
    {
        while(true){
            _Text.DOColor(nullColor, .75f).OnComplete(()=>{
                _Text.DOColor(Color.white, .75f);
            });
            yield return new WaitForSeconds(1.5f);
        }
    }
}
