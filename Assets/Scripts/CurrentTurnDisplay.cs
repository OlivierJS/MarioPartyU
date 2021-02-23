using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentTurnDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        theStateManager = GameObject.FindObjectOfType<StateManager>();
        myText = GetComponent<Text>();
    }

    StateManager theStateManager;
    Text myText;

    // Update is called once per frame
    void Update()
    {
        if (theStateManager.gameFinished == false)
        {
            myText.text = "Turn: " + theStateManager.currentTurn + "/" + theStateManager.maxTurns;
        }
        else 
        {
            myText.text = "Game Finished";
        }
    }
}
