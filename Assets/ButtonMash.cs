using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMash : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        stateTex = GetComponent<RawImage>();
        theMashgameManager = GameObject.FindObjectOfType<MashgameManager>();
    }

    public KeyCode key;
    RawImage stateTex;
    public Texture2D[] statesTex;
    MashgameManager theMashgameManager;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(key))
        {
            if(theMashgameManager.P1Stop == false)
            {
                theMashgameManager.amountOfPressesP1 += 1;
                stateTex.texture = statesTex[1];
            }
            if(theMashgameManager.P2Stop == false)
            {
                theMashgameManager.amountOfPressesP2 += 1;
                stateTex.texture = statesTex[1];
            }
         }

        else
        {
            stateTex.texture = statesTex[0];
        }        
    }
}
