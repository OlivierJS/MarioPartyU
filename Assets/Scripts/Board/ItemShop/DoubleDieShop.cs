using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDieShop : MonoBehaviour
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
            if (currentPlayer.itemsInventory[i] == 0 && currentPlayer.amountOfCoins >= 3)
            {
                currentPlayer.itemsInventory[i] = 1;
                currentPlayer.amountOfCoins -= 3;
                Debug.Log("You got a double die!");
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
