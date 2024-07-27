using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceImagesStore : MonoBehaviour
{
    public static DiceImagesStore instance;
    public List<Sprite> diceSprites;

    void Awake(){
        if(instance != null && instance != this) Destroy(gameObject);
        else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
