using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedManager : MonoBehaviour
{
    public static TurnBasedManager instance;
    public float duration = 7;
    bool _currentTurn;  //true is host, false is client

    void Awake(){
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        _currentTurn = true;
        //StartCoroutine(TurnBasedCO());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TurnBasedCO(){
        while(true){
            yield return new WaitForSeconds(duration);
            _currentTurn = !_currentTurn;
            GameEvents.OnTurnChanged(_currentTurn);
        }
    }
}
