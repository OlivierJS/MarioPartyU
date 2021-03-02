using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton2 : MonoBehaviour
{
    public Player currentPlayer;
    StateManager theStateManager;

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

        switch (currentPlayer.itemsInventory[1])
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
        currentPlayer.itemsInventory[1] = 0;
    }
}
