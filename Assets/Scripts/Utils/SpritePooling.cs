using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePooling : MonoBehaviour
{
    public static SpritePooling instance;
    public List<Sprite> diceSprites;

    void Awake(){
        if(instance != null && instance != this) Destroy(gameObject);
        else {
            instance = this;
            // DontDestroyOnLoad(gameObject);
        }
    }
}
