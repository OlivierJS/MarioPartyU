using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardgameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //winID = Random.Range(1, 5);
        winID = 1;
        theGlobalDataManager = GameObject.FindObjectOfType<GlobalDataManager>();
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

    // Update is called once per frame
    void Update()
    {
        Debug.Log(P2Win);
        if(P1Done == true && P2Done == true && theGlobalDataManager.P1amountOfCoins == oldP1coins && theGlobalDataManager.P2amountOfCoins == oldP2coins)
        {
            StartCoroutine(ReturntoBoard());
        }

        else
        {
            StopCoroutine(ReturntoBoard());
        }

        if(P1canMove == true || P2canMove == true)
        {
            StartCoroutine(CanMoveAgain());
        }
    }

    IEnumerator ReturntoBoard()
    {
        Debug.Log("Returning to board");
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
}
