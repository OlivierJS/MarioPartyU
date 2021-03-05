using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        DiceRoller = GameObject.FindObjectOfType<DiceRoll>();
        theStateManager = GameObject.FindObjectOfType<StateManager>();
    }

    public Tile StartingTile;
    public Tile currentTile;

    public int playerID;

    public int amountOfCoins = 0;
    public int amountOfStars = 0;
    public int[] itemsInventory = { 0, 0, 0 };

    StateManager theStateManager;
    DiceRoll DiceRoller;
    public GameObject StarMenu;
    public GameObject ItemMenu;


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
            DiceRoller.RollDice();

            if (theStateManager.canMove == true)
            {
                //Debug.Log("Can Move");
                if (theStateManager.IsDoneRolling == false || theStateManager.IsDoneClicking == true || theStateManager.currentPlayerID != playerID)
                {
                    //je kan nog niet bewegen (nog geen roll of al geklikt)
                    //Debug.Log(playerID);
                    return;
                }

                if (theStateManager.IsCollectingStar == false)
                {
                    PlayerMovement();
                    //hier is currentTile gebruikt omdat de functie importeren van tile.cs niet lukte
                    if (currentTile != null)
                    {
                        currentTile.TileEffects(currentTile, this);
                    }
                }

                if (theStateManager.IsCollectingStar == true)
                {
                    return;
                }

                if (amountOfCoins <= 0)
                {
                    amountOfCoins = 0;
                }
            }
        }


        //Debug.Log("Player:" + playerID + ", " + amountOfCoins);
    }

    public void PlayerMovement()
    {
        Tile finalTile = currentTile;

        for (int i = 0; i < DiceRoller.DiceValue; i++)
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
        }
        
        if (finalTile == null)
        {
            return;
        }

        //teleporteer player naar finalTile, tenzij hij bezig is met star collection
        if (finalTile.tileTypeID != 3)
        {
            this.transform.position = finalTile.transform.position;
            currentTile = finalTile;

            theStateManager.IsDoneClicking = true;
        }
    }

    public void CanMove()
    {
        theStateManager.canMove = true;
        StarMenu.SetActive(false);
    }

    void ItemUsage()
    {
        ItemMenu.SetActive(true);
    }

    public void FinishItem()
    {
        theStateManager.isDoneUsingItem = true;
        ItemMenu.SetActive(false);
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
                DiceRoller.DiceValue += 1;
                
            }
    }
}
