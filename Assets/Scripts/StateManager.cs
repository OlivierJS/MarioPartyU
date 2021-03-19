using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        theGlobalDataManager = GameObject.FindObjectOfType<GlobalDataManager>();
        PlayersList[currentPlayerID].ItemUsage();
        //Verander alleen van 1 voor testing. Voor een normale game zou deze variable aan het begin altijd gelijk moeten zijn aan 1.
        currentTurn = theGlobalDataManager.currentTurn;
        canMove = true;
        stopWaiting = true;
        amountOfDice = 1;
    }

    GlobalDataManager theGlobalDataManager;

    public int currentTurn;
    public int numberOfPlayer = 2;
    public int currentPlayerID = 0;
    public int amountOfDice;
    float timeRemaining;
    int gameNo;
    int MenuOption;

    public int maxTurns = 10;

    public bool isDoneUsingItem = false;
    public bool IsDoneRolling = false;
    public bool IsDoneClicking = false;
    public bool IsDoneAnimating = false;
    public bool IsCollectingStar = false;
    public bool IsDoneCollecting = false;
    public bool IsCurrentlyShopping = false;
    public bool IsDoneShopping = false;
    public bool gameFinished = false;
    public bool canMove = true;
    public bool goToMinigame = false;
    bool stopWaiting;

    public Player[] PlayersList;
    public GameObject diePrefab;
    public DiceRoll[] DiceRollers;
    public GameObject Dice1;
    public GameObject Dice2;
    public GameObject Dice3;
    public GameObject ShopMenu;
    public GameObject StarMenu;
    public GameObject MinigameSelecter;
    public Text MinigameText;

    public void NewTurn()
    {
        stopWaiting = true;
        isDoneUsingItem = false;
        IsDoneRolling = false;
        IsDoneClicking = false;
        IsDoneAnimating = false;
        IsDoneCollecting = false;
        IsDoneShopping = false;
        canMove = true;
        MenuOption = 0;
        amountOfDice = 1;

        for (int i = 0; i < DiceRollers.Length; i++)
        {
            DiceRollers[i].maxDiceValue = 6;
        }

        if (currentTurn % 2 == 0)
        {
            goToMinigame = true;
            timeRemaining = 3;
            gameNo = Random.Range(1, 4);
        }

        currentTurn += 1;
        theGlobalDataManager.currentTurn = currentTurn;
        PlayersList[currentPlayerID].DiceTotal = 0;

        currentPlayerID = (currentPlayerID + 1) % numberOfPlayer;
        StopCoroutine(WaitforNewTurn());
    }
    
    IEnumerator WaitforNewTurn()
    {
        stopWaiting = false;
        yield return new WaitForSeconds(2);
        if(stopWaiting == false)
        {
            NewTurn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDoneRolling == true && IsDoneClicking == true && isDoneUsingItem == true && IsDoneAnimating == true)
        {
            if (currentTurn < maxTurns)
            {
                if(stopWaiting == true)
                {
                    StartCoroutine(WaitforNewTurn());
                }
                return;
            }
            else
            {
                gameFinished = true;
                return;
            }
        }
        
        if (goToMinigame == true)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                MenuOption += 1;
                MinigameSelecter.SetActive(true);
            }
            if(timeRemaining <= 0.5)
            {
                MenuOption = gameNo;
            }
            if(timeRemaining <= 0)
            {
                timeRemaining = 0;
                MinigameSelecter.SetActive(false);
                switch(gameNo)
                {
                    case 1:
                        SceneManager.LoadScene("Mashing minigame", LoadSceneMode.Single);
                    break;
                    case 2:
                        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
                    break;
                    case 3:
                        Debug.Log("F");
                    break;
                }
                goToMinigame = false;
            }

        }

        switch(MenuOption % 4)
        {
            case 1:
                MinigameText.text = "        Minigame Time!" + "\n> Mashing" + "\n   Cards" + "\n   Platforming";
            break;

            case 2:
              MinigameText.text = "        Minigame Time!" + "\n   Mashing" + "\n> Cards" + "\n   Platforming";
            break;

            case 3:
              MinigameText.text = "        Minigame Time!" + "\n   Mashing" + "\n   Cards" + "\n> Platforming";
            break;
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
