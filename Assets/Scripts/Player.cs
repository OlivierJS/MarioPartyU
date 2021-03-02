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
    public int[] itemsInventory = { 0, 0, 0 };
    

    StateManager theStateManager;
    // Update is called once per frame
    void Update()
    {
       //Debug.Log("Player:" + playerID + ", " + amountOfCoins);
    }

    void OnMouseUp()
    {
        //Debug.Log("HI");
        
        if (theStateManager.IsDoneRolling == false || theStateManager.IsDoneClicking == true || theStateManager.currentPlayerID != playerID)
        {
            //je kan nog niet bewegen (nog geen roll of al geklikt)
            //Debug.Log(playerID);
            return;
        }

        PlayerMovement();
        if (theStateManager.IsCollectingStar == false)
        {
            //hier is StartingTile gebruikt omdat de functie importeren van tile.cs niet lukte
            if (currentTile != null)
            {
                currentTile.TileEffects(currentTile, this);
            }

            
        }
    }

    public void PlayerMovement()
    {
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
            StarCollection(finalTile);
        }
        
        if (finalTile == null)
        {
            return;
        }

        //teleporteer player naar finalTile, tenzij hij bezig is met star collection
        if (theStateManager.IsCollectingStar == false)
        {
            this.transform.position = finalTile.transform.position;
            currentTile = finalTile;
            

            theStateManager.IsDoneClicking = true;
        }
    }

    void StarCollection(Tile tile)
    {
            if (tile.tileTypeID == 3)
            {
                theStateManager.IsCollectingStar = true;
                this.transform.position = tile.transform.position;
                //Coins moeten er nog afgehaald worden bij star collection, voor testen weggelaten
                if (amountOfCoins > 10)
                {
                    amountOfStars += 1;
                    amountOfCoins -= 10;
                    //Debug.Log("Your Got A Star! Amount of Stars: " + amountOfStars);
                }
                else
                {
                    //Debug.Log("Not Enough Coins! Amount of Stars: " + amountOfStars);
                }
                theStateManager.IsCollectingStar = false;
            }
    }

    //misschien deze functie in de Tile script zetten om deze overzichtelijk te houden
    /*void TileEffects(Tile tile)
    {
            switch (tile.tileTypeID)
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
    }*/

}
