using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        theMashgameManager = GameObject.FindObjectOfType<MashgameManager>();
        displays[0].text = " V " + "\nP1" + "\n ?";
    }

    public Text[] displays;
    MashgameManager theMashgameManager;

    // Update is called once per frame
    void Update()
    {        
        if (theMashgameManager.P1Win == true || theMashgameManager.P2Win == true || theMashgameManager.draw == true)
        {
            displays[0].text = "\nP1" + "\n" + theMashgameManager.amountOfPressesP1;
            displays[1].text = "\nP2" + "\n" + theMashgameManager.amountOfPressesP2;
        }

        else if(theMashgameManager.waiting == false)
        {
            displays[1].text =" V " + "\nP2" + "\n ?";
            displays[0].text = "\nP1" + "\n ?";
        }
    }
}
