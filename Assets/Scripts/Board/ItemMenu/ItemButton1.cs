using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton1 : MonoBehaviour
{
    public Player currentPlayer;
    StateManager theStateManager;
    public GameObject ItemMenu;

    public Sprite ItemImageDoubleDice;
    public Sprite ItemImageTripleDice;
    public Sprite ItemImageMiniDice;
    public Sprite ItemImageGoldenPipe;
    public Sprite ItemImageEmpty;

    // Start is called before the first frame update
    void Start()
    {
        theStateManager = GameObject.FindObjectOfType<StateManager>();
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
                theStateManager.amountOfDice = 1;
            break;
            case 1:
                // 2 keer dobbelen (2 dice laten verschijnen?)
                theStateManager.amountOfDice = 2;
            break;
            case 2:
                //3 keer dobbelen (3 dice laten verschijnen?)
                theStateManager.amountOfDice = 3;
            break;
            case 3:
                // je kan alleen 1-3 krijgen
                for (int i = 0; i < theStateManager.DiceRollers.Length; i++)
                {
                    theStateManager.DiceRollers[i].maxDiceValue = 3;
                }
                theStateManager.amountOfDice = 1;
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
                        currentPlayer.targetposition = currentPlayer.currentTile.transform.position;
                        break;
                    }
                }
                theStateManager.amountOfDice = 1;
            break;
        }
        currentPlayer.itemsInventory[0] = 0;
    }

    public void FinishItem()
    {
        theStateManager.isDoneUsingItem = true;
        ItemMenu.SetActive(false);
        for (int i = 0; i < theStateManager.DiceRollers.Length; i++)
        {
            theStateManager.DiceRollers[i].stopRandom = false;
        }
    }
}
