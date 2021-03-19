using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardgameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        winID = Random.Range(1, 5);
        theGlobalDataManager = GameObject.FindObjectOfType<GlobalDataManager>();
        theTimer = GameObject.FindObjectOfType<Timer>();
        oldP1coins = theGlobalDataManager.P1amountOfCoins;
        oldP2coins = theGlobalDataManager.P2amountOfCoins;
    }

    int oldP1coins;
    int oldP2coins;
    public int winID;
    public bool P1canMove;
    public bool P2canMove;
    public bool P1Done;
    public bool P2Done;
    public bool P1Win;
    public bool P2Win;
    GlobalDataManager theGlobalDataManager;
    public Card[] Cards;
    Timer theTimer;

    // Update is called once per frame
    void Update()
    {  
        GameEnd();
        TimeUp();

        if(P1canMove == true || P2canMove == true)
        {
            StartCoroutine(CanMoveAgain());
        }
    }

    IEnumerator ReturntoBoard()
    {
        theTimer.GameIsDone = true;
        if(P1Win == true)
        {
            theGlobalDataManager.P1amountOfCoins += 10; 
            Debug.Log("Player 1 wins!");
        }
        if(P2Win == true)
        {
           theGlobalDataManager.P2amountOfCoins += 10;
           Debug.Log("Player 2 wins!");
        }
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Scene", LoadSceneMode.Single);
    }

    IEnumerator CanMoveAgain()
    {
        yield return new WaitForSeconds(0.2f);
        P1canMove = true;
        P2canMove = true;
    }

    void GameEnd()
    {
        if(P1Done == true && P2Done == true && theGlobalDataManager.P1amountOfCoins == oldP1coins && theGlobalDataManager.P2amountOfCoins == oldP2coins)
        {
            StartCoroutine(ReturntoBoard());
        }

        else
        {
            StopCoroutine(ReturntoBoard());
        }
    }

    void TimeUp()
    {
        if(theTimer.timeRemaining <= 0)
        {
            P1Done = true;
            P2Done = true;
            for(int i = 0; i < Cards.Length; i++)
            {
                if(Cards[i].cardState == 1)
                {
                    Cards[i].cardState = 2;
                }

                if(Cards[i].cardState == 3)
                {
                    Cards[i].cardState = 4;
                }
            }
        }
    }

}
