using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    DiceRoll DiceRoller;
    // Start is called before the first frame update
    void Start()
    {
        DiceRoller = GameObject.FindObjectOfType<DiceRoll>();
        theStateManager = GameObject.FindObjectOfType<StateManager>();
    }

    public Tile StartingTile;
    Tile currentTile;

    public int playerID;

    public int amountOfCoins = 0;
    public int amountOfStars = 0;

    StateManager theStateManager;
    // Update is called once per frame
    void Update()
    {
       
    }

    void OnMouseUp()
    {
        Debug.Log("HI");
        
        if (theStateManager.IsDoneRolling == false || theStateManager.IsDoneClicking == true || theStateManager.currentPlayerID != playerID)
        {
            //je kan nog niet bewegen (nog geen roll of al geklikt)
            Debug.Log(playerID);
            return;
        }

        int spacesToMove = DiceRoller.DiceValue;
        Tile finalTile = currentTile;

        for (int i = 0; i < spacesToMove; i++)
        {
            if (finalTile == null)
            {
                finalTile = StartingTile.NextTiles[0];
            }
            else
            {
                /*if (finalTile.NextTiles == null || finalTile.NextTiles.Length == 0)
                {
                    Debug.Log("Good job!");
                    Destroy(gameObject);
                }
                else */
                if (finalTile.NextTiles.Length > 0)
                {
                    //keuze van speler moet nog geïmplementeerd worden
                    finalTile = finalTile.NextTiles[0];
                }
                else
                {
                    finalTile = finalTile.NextTiles[0];
                }
            }

            if (finalTile.tileTypeID == 3)
            {
                theStateManager.IsCollectingStar = true;
                this.transform.position = finalTile.transform.position;
                if (amountOfCoins > 10)
                {
                    amountOfStars += 1;
                    Debug.Log("Your Got A Star! Amount of Stars: " + amountOfStars);
                }
                else
                {
                    Debug.Log("Not Enough Coins! Amount of Stars: " + amountOfStars);
                }
                theStateManager.IsCollectingStar = false;
            }
        }
        
        if (finalTile == null)
        {
            return;
        }

        //teleporteer player naar finalTile
        if (theStateManager.IsCollectingStar == false)
        {
            this.transform.position = finalTile.transform.position;
            currentTile = finalTile;

            switch (currentTile.tileTypeID)
            {
                //Nothing
                case 0:
                    Debug.Log("You have this amount of coins: " + amountOfCoins);
                    break;
                //+3 coins
                case 1:
                    amountOfCoins += 3;
                    Debug.Log("You have this amount of coins: " + amountOfCoins);
                    break;
                //-3 coins
                case 2:
                    amountOfCoins -= 3;
                    if (amountOfCoins < 0)
                    {
                        amountOfCoins = 0;
                    }
                    Debug.Log("You have this amount of coins: " + amountOfCoins);
                    break;
            }

            theStateManager.IsDoneClicking = true;
        }
    }
}
