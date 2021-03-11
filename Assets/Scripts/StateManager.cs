using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        theGlobalDataManager = GameObject.FindObjectOfType<GlobalDataManager>();

        //Verander alleen van 1 voor testing. Voor een normale game zou deze variable aan het begin altijd gelijk moeten zijn aan 1.
        currentTurn = theGlobalDataManager.currentTurn;
        canMove = true;
        amountOfDice = 1;
    }

    GlobalDataManager theGlobalDataManager;

    public int currentTurn;
    public int numberOfPlayer = 2;
    public int currentPlayerID = 0;
    public int amountOfDice;

    public int maxTurns = 10;

    public bool isDoneUsingItem = false;
    public bool IsDoneRolling = false;
    public bool IsDoneClicking = false;
    public bool IsCollectingStar = false;
    public bool IsDoneCollecting = false;
    public bool IsCurrentlyShopping = false;
    public bool IsDoneShopping = false;
    public bool gameFinished = false;
    public bool canMove = true;

    public Player[] PlayersList;
    public GameObject diePrefab;
    public DiceRoll[] DiceRollers;
    public GameObject Dice1;
    public GameObject Dice2;
    public GameObject Dice3;
    public GameObject ShopMenu;
    public GameObject StarMenu;

    public void NewTurn()
    {
        isDoneUsingItem = false;
        IsDoneRolling = false;
        IsDoneClicking = false;
        IsDoneCollecting = false;
        IsDoneShopping = false;
        canMove = true;  
        for (int i = 0; i < DiceRollers.Length; i++)
        {
            DiceRollers[i].maxDiceValue = 6;
        }
        currentTurn += 1;
        theGlobalDataManager.currentTurn = currentTurn;
        PlayersList[currentPlayerID].DiceTotal = 0;

        currentPlayerID = (currentPlayerID + 1) % numberOfPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDoneRolling == true && IsDoneClicking == true && isDoneUsingItem == true)
        {
            if (currentTurn < maxTurns)
            {
                NewTurn();
                return;
            }
            else
            {
                gameFinished = true;
                return;
            }
        }    
             
        switch(amountOfDice)
        {
            case 1:
                Dice1.SetActive(true);
                Dice2.SetActive(false);
                Dice3.SetActive(false);
            break;
            case 2:
                Dice1.SetActive(true);
                Dice2.SetActive(true);
                Dice3.SetActive(false);
            break;
            case 3:
                Dice1.SetActive(true);
                Dice2.SetActive(true);
                Dice3.SetActive(true);
            break;
        }      
    }

    public void Cancel()
    {
        amountOfDice = 1;
    }
    
    public void CanMove()
    {
        Debug.Log("Can now move");
        canMove = true;
        ShopMenu.SetActive(false);
        StarMenu.SetActive(false);
    }

}
