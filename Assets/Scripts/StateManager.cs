using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Verander alleen van 1 voor testing. Voor een normale game zou deze variable aan het begin altijd gelijk moeten zijn aan 1.
        currentTurn = 1;
    }

    public int currentTurn;
    public int numberOfPlayer = 2;
    public int currentPlayerID = 0;

    public int maxTurns = 10;

    public bool isDoneUsingItem = false;
    public bool IsDoneRolling = false;
    public bool IsDoneClicking = false;
    public bool IsCollectingStar = false;
    public bool gameFinished = false;

    public void NewTurn()
    {
        isDoneUsingItem = false;
        IsDoneRolling = false;
        IsDoneClicking = false;

        currentTurn += 1;

        currentPlayerID = (currentPlayerID + 1) % numberOfPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDoneRolling == true && IsDoneClicking == true && isDoneUsingItem == false)
        {
            if (currentTurn < maxTurns)
            {
                NewTurn();
                return;
            }
            else
            {
                gameFinished = true;
                return;
            }
        }
    }

}
