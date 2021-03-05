using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton1 : MonoBehaviour
{
    public Player currentPlayer;
    StateManager theStateManager;
    DiceRoll DiceRoller;

    public Sprite ItemImageDoubleDice;
    public Sprite ItemImageTripleDice;
    public Sprite ItemImageMiniDice;
    public Sprite ItemImageGoldenPipe;
    public Sprite ItemImageEmpty;

    // Start is called before the first frame update
    void Start()
    {
        theStateManager = GameObject.FindObjectOfType<StateManager>();
        DiceRoller = GameObject.FindObjectOfType<DiceRoll>();
    }

    // Update is called once per frame
    void Update()
    {
        currentPlayer = theStateManager.PlayersList[theStateManager.currentPlayerID];

        switch (currentPlayer.itemsInventory[0])
        {
            case 0:
                this.transform.GetChild(0).GetComponent<Image>().sprite = ItemImageEmpty;
                break;
            case 1:
                this.transform.GetChild(0).GetComponent<Image>().sprite = ItemImageDoubleDice;
                break;
            case 2:
                this.transform.GetChild(0).GetComponent<Image>().sprite = ItemImageTripleDice;
                break;
            case 3:
                this.transform.GetChild(0).GetComponent<Image>().sprite = ItemImageMiniDice;
                break;
            case 4:
                this.transform.GetChild(0).GetComponent<Image>().sprite = ItemImageGoldenPipe;
                break;
            default:
                this.transform.GetChild(0).GetComponent<Image>().sprite = ItemImageEmpty;
                break;
        }
    }

    public void UseItem()
    {
        //TO DO: Add item functionality (could be done using a switch statement)
        switch (currentPlayer.itemsInventory[0])
        {
            case 0:
                return;
            break;
            case 1:
                // 2 keer dobbelen (2 dice laten verschijnen?)
            break;
            case 2:
                //3 keer dobbelen (3 dice laten verschijnen?)

            break;
            case 3:
                // je kan alleen 1-3 krijgen
                DiceRoller.maxDiceValue = 3;
            break;
            case 4:
                //teleporteer naar plek vlak voor star tile
                currentPlayer.currentTile = currentPlayer.StartingTile;
                for(int i = 0; i < 10000; i++)
                {
                    currentPlayer.currentTile = currentPlayer.currentTile.NextTiles[0];
                    if (currentPlayer.currentTile.tileTypeID == 3)
                    {
                        currentPlayer.currentTile = currentPlayer.currentTile.PrevTile;
                        currentPlayer.transform.position = currentPlayer.currentTile.transform.position;
                        break;
                    }
                }
            break;
        }
        currentPlayer.itemsInventory[0] = 0;
    }
}
