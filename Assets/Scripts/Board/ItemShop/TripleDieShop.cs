using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleDieShop : MonoBehaviour
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
            if (currentPlayer.itemsInventory[i] == 0 && currentPlayer.amountOfCoins >= 7)
            {
                currentPlayer.itemsInventory[i] = 2;
                currentPlayer.amountOfCoins -= 7;
                Debug.Log("You got a triple die!");
                break;
            }
            else
            {
                //Should only happen when not enough money
                Debug.Log("Inventory Full or Not Enough Money!");
            }
        }
    }
}
