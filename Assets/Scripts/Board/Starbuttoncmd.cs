using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starbuttoncmd : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        theStateManager = GameObject.FindObjectOfType<StateManager>();
    }

    Player player;
    StateManager theStateManager;
    public GameObject ShopMenu;
    public GameObject StarMenu;

    public void ChooseToCollect()
    {
          player = theStateManager.PlayersList[theStateManager.currentPlayerID];

          if (player.amountOfCoins >= 10)
          {
                player.amountOfStars += 1;
                player.amountOfCoins -= 10;
                Debug.Log("You Got A Star! Amount of Stars: " + player.amountOfStars);
          }
          else
          {
                Debug.Log("Not Enough Coins! Amount of Stars: " + player.amountOfStars);
          }
    }
}
