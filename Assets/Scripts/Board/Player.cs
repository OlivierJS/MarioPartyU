using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        theStateManager = GameObject.FindObjectOfType<StateManager>();
        theGlobalDataManager = GameObject.FindObjectOfType<GlobalDataManager>();
        allTiles = GameObject.FindObjectsOfType<Tile>();

        switch (playerID)
        {
            case 0:
                //currentTile = theGlobalDataManager.P1currentTile;
                currentTileID = theGlobalDataManager.P1currentTileID;
                amountOfCoins = theGlobalDataManager.P1amountOfCoins;
                amountOfStars = theGlobalDataManager.P1amountOfStars;
                itemsInventory = theGlobalDataManager.P1itemsInventory;
                break;
            case 1:
                //currentTile = theGlobalDataManager.P2currentTile;
                currentTileID = theGlobalDataManager.P2currentTileID;
                amountOfCoins = theGlobalDataManager.P2amountOfCoins;
                amountOfStars = theGlobalDataManager.P2amountOfStars;
                itemsInventory = theGlobalDataManager.P2itemsInventory;
                break;
        }

        for (int i = 0; i < allTiles.Length; i++)
        {
            if (currentTileID == allTiles[i].tileID)
            {
                currentTile = allTiles[i];
            }
        }

        this.transform.position = currentTile.transform.position;
    }

    public Tile StartingTile;
    public Tile currentTile;
    Tile[] allTiles;
    int currentTileID;

    public int playerID;

    public int amountOfCoins = 0;
    public int amountOfStars = 0;
    public int[] itemsInventory = { 0, 0, 0 };
    public int DiceTotal;
    int value1;
    int value2;
    int value3;

    StateManager theStateManager;
    GlobalDataManager theGlobalDataManager;
    public GameObject StarMenu;
    public GameObject ItemMenu;
    public GameObject ShopMenu;


    // Update is called once per frame
    void Update()
    {   
        if (theStateManager.isDoneUsingItem == false)
        {
            ItemUsage();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && theStateManager.isDoneUsingItem == true)
        {
            //Debug.Log("Rolled");
            Roll();
            MoveCheck();

            if (amountOfCoins <= 0)
            {
                amountOfCoins = 0;
            }

            SavePlayer();
        }


        //Debug.Log("Player:" + playerID + ", " + amountOfCoins);
    }

    public void PlayerMovement()
    {
        Tile finalTile = currentTile;
 
        for (int i = 0; i < theStateManager.PlayersList[theStateManager.currentPlayerID].DiceTotal; i++)
        {
            if (finalTile == null)
            {
                finalTile = StartingTile.NextTiles[0];
            }
            else
            {
                if (finalTile.NextTiles.Length > 1)
                {
                    //keuze van speler moet nog geïmplementeerd worden
                    finalTile = finalTile.NextTiles[0];
                }
                else
                {
                    finalTile = finalTile.NextTiles[0];
                }
            }
            if (finalTile.tileTypeID == 3 && theStateManager.IsDoneCollecting == false)
            {
                StarCollection(finalTile);
                Debug.Log("You landed on a star tile!");
                break;
            }
            if (finalTile.tileTypeID == 5 && theStateManager.IsDoneShopping == false)
            {
                if (itemsInventory[0] == 0 || itemsInventory[1] == 0 || itemsInventory[2] == 0)
                {
                    Shopping(finalTile);
                    Debug.Log("You landed on a shopping tile!");
                    break;
                }
                else 
                {
                    DiceTotal += 1;
                }
            }
        }
        
        if (finalTile == null)
        {
            return;
        }

        //teleporteer player naar finalTile, tenzij hij bezig is met star collection
        if (finalTile.tileTypeID != 3 && finalTile.tileTypeID != 5)
        {
            currentTileID = finalTile.tileID;
            this.transform.position = finalTile.transform.position;
            //DiceTotal = 0;
            currentTile = finalTile;
            if (currentTile != null)
            {
                currentTile.TileEffects(currentTile, this);
            }

            theStateManager.IsDoneClicking = true;
        }
    }

    void MoveCheck()
    {
        if (theStateManager.canMove == true)
        {
            //Debug.Log("Can Move");
            if (theStateManager.IsDoneRolling == false || theStateManager.IsDoneClicking == true || theStateManager.currentPlayerID != playerID)
            {
                //je kan nog niet bewegen (nog geen roll of al geklikt)
                //Debug.Log(playerID);
                return;
            }

            if (theStateManager.IsCollectingStar == false && theStateManager.IsCurrentlyShopping == false)
            {
                PlayerMovement();                   
                //hier is currentTile gebruikt omdat de functie importeren van tile.cs niet lukte                   
            }

            if (theStateManager.IsCollectingStar == true || theStateManager.IsCurrentlyShopping == true)
            {
                 return;
            }
        }
    }

    void Roll()
    {
            if(theStateManager.IsDoneRolling == false)
            {
 
                for (int i = 0; i < theStateManager.DiceRollers.Length; i++)
                {
                    theStateManager.DiceRollers[i].RollDice();
                }
                value1 = theStateManager.DiceRollers[0].DiceValue;
                value2 = theStateManager.DiceRollers[1].DiceValue;
                value3 = theStateManager.DiceRollers[2].DiceValue;

                //Debug.Log(AllDice[i].DiceValue);
                switch(theStateManager.amountOfDice)
                {
                    case 1:
                        theStateManager.PlayersList[theStateManager.currentPlayerID].DiceTotal += value1;
                    break;
                    case 2:
                        theStateManager.PlayersList[theStateManager.currentPlayerID].DiceTotal = value1 +  value2;
                    break;
                    case 3:
                        theStateManager.PlayersList[theStateManager.currentPlayerID].DiceTotal = value1 + value2 + value3;
                    break;
                }
 
                theStateManager.IsDoneRolling = true;
            }       
    }


    void ItemUsage()
    {
        if(itemsInventory[0] == 0 && itemsInventory[1] == 0 && itemsInventory[2] == 0)
        {
            theStateManager.isDoneUsingItem = true;
            for (int i = 0; i < theStateManager.DiceRollers.Length; i++)
            {
                theStateManager.DiceRollers[i].stopRandom = false;
            }
        }
        else
        {
            ItemMenu.SetActive(true);
        }
    }



    void StarCollection(Tile tile)
    {
            if (tile.tileTypeID == 3)
            {
                theStateManager.IsCollectingStar = true;
                this.transform.position = tile.transform.position;
                StarMenu.SetActive(true);
                theStateManager.canMove = false;               
                theStateManager.IsCollectingStar = false;
                theStateManager.IsDoneClicking = false;
                theStateManager.IsDoneCollecting = true;
                currentTile = currentTile.NextTiles[0];               
            }
    }

    void Shopping(Tile tile)
    {
            if (tile.tileTypeID == 5)
            {
                theStateManager.IsCurrentlyShopping = true;
                this.transform.position = tile.transform.position;
                ShopMenu.SetActive(true);
                theStateManager.canMove = false;
                theStateManager.IsCurrentlyShopping = false;
                theStateManager.IsDoneClicking = false;
                theStateManager.IsDoneShopping = true;
                currentTile = currentTile.NextTiles[0];
            }
    }

    public void SavePlayer()
    {
        switch(playerID)
        {
            case 0:
                //theGlobalDataManager.P1currentTile = currentTile;
                theGlobalDataManager.P1currentTileID = currentTileID;
                theGlobalDataManager.P1amountOfCoins = amountOfCoins;
                theGlobalDataManager.P1amountOfStars = amountOfStars;
                theGlobalDataManager.P1itemsInventory = itemsInventory;
                break;
            case 1:
                //theGlobalDataManager.P2currentTile = currentTile;
                theGlobalDataManager.P2currentTileID = currentTileID;
                theGlobalDataManager.P2amountOfCoins = amountOfCoins;
                theGlobalDataManager.P2amountOfStars = amountOfStars;
                theGlobalDataManager.P2itemsInventory = itemsInventory;
                break;
        }
    }
}
