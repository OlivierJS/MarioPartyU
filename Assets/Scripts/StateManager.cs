using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public int numberOfPlayer = 2;
    public int currentPlayerID = 0;

    public bool IsDoneRolling = false;
    public bool IsDoneClicking = false;
    public bool IsCollectingStar = false;

    public void NewTurn()
    {
        IsDoneRolling = false;
        IsDoneClicking = false;

        currentPlayerID = (currentPlayerID + 1) % numberOfPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDoneRolling == true && IsDoneClicking == true)
        {
            NewTurn();
            return;
        }
    }

}
