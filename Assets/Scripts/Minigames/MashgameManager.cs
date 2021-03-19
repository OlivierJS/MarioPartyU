using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MashgameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        theGlobalDataManager = GameObject.FindObjectOfType<GlobalDataManager>();
        theTimer = GameObject.FindObjectOfType<Timer>();
        oldP1coins = theGlobalDataManager.P1amountOfCoins;
        oldP2coins = theGlobalDataManager.P2amountOfCoins;
        instructions.text = "P1 press space to begin";
        P1Stop = true;
        P2Stop = true;
        waiting = true;
    }

    Timer theTimer;
    GlobalDataManager theGlobalDataManager;
    public Text instructions;
    public bool P2Stop;
    public bool P1Stop;
    public bool P2Win;
    public bool P1Win;
    public bool waiting;
    public bool draw;
    int oldP1coins;
    int oldP2coins;
    public int amountOfPressesP1;
    public int amountOfPressesP2;

    // Update is called once per frame
    void Update()
    {
        //Game begins if P1 / P2 presses spacebar
        if(P1Stop == true && P2Stop == true)
        {
            theTimer.timeRemaining = 11;
        }

        if(Input.GetKeyUp(KeyCode.Space) && P1Stop == true && amountOfPressesP1 == 0)
        {
            P1Stop = false;
            instructions.text = "";
        }

        if(Input.GetKeyUp(KeyCode.Space) && P2Stop == true && waiting == false && amountOfPressesP2 == 0)
        {
            P2Stop = false;
            instructions.text = "";
        }


        if(theTimer.timeRemaining <= 0 && P1Stop == false)
        {
            P1Stop = true;
            StartCoroutine(NextPlayer());
        }

        if(theTimer.timeRemaining <= 0 && P2Stop == false && theGlobalDataManager.P1amountOfCoins == oldP1coins && theGlobalDataManager.P2amountOfCoins == oldP2coins)
        {
            StopCoroutine(NextPlayer());
            CompareScores();
            StartCoroutine(ReturntoBoard());
        }
        else
        {
            StopCoroutine(ReturntoBoard());
        }
    }

    void CompareScores()
    {
        if(amountOfPressesP1 > amountOfPressesP2)
        {
            P1Win = true;
        }
        if(amountOfPressesP1 < amountOfPressesP2)
        {
            P2Win = true;
        }
        if(amountOfPressesP1 == amountOfPressesP2)
        {
            draw = true;
        }
    }

    IEnumerator ReturntoBoard()
    {
        if(P1Win == true)
        {
            theGlobalDataManager.P1amountOfCoins += 10; 
            instructions.text = "Player 1 wins!";
        }
        if(P2Win == true)
        {
           theGlobalDataManager.P2amountOfCoins += 10;
           instructions.text = "Player 2 wins!";
        }
        if (draw == true)
        {
            instructions.text = "       DRAW";
        }
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Scene", LoadSceneMode.Single);
    }

    IEnumerator NextPlayer()
    {
        instructions.text = "        FINISH!";
        yield return new WaitForSeconds(2);
        waiting = false;
        instructions.text = "P2 press space\n         to begin";
        theTimer.GameIsDone = false;
        theTimer.timeRemaining = 11;
    }
}
