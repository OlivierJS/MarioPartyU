using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniDieShop : MonoBehaviour
{
    public Player currentPlayer;
    StateManager theStateManager;

    // Start is called before the first frame update
    void Start()
    {
        theStateManager = GameObject.FindObjectOfType<StateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Buyitem()
    {
        currentPlayer = theStateManager.PlayersList[theStateManager.currentPlayerID];

        for (int i = 0; i < currentPlayer.itemsInventory.Length; i++)
        {
            if (currentPlayer.itemsInventory[i] == 0 && currentPlayer.amountOfCoins >= 1)
            {
                currentPlayer.itemsInventory[i] = 3;
                currentPlayer.amountOfCoins -= 1;
                Debug.Log("You got a mini die!");
                break;
            }
            else
            {
                //Should never happen
                Debug.Log("Inventory Full!");
            }
        }
    }
}
