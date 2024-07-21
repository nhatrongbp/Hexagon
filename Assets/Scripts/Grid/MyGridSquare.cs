using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class MyGridSquare : MonoBehaviour
{
    public TMP_Text debuggerText;
    // public Image normalImage;
    // public List<Sprite> normalImages;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDebuggerText(int i, int j){
        debuggerText.text = i.ToString() + ", " + j.ToString();
    }

    // public void SetImage(bool setFirstImage){
    //     normalImage.sprite = setFirstImage ? normalImages[0] : normalImages[1];
    // }
}
