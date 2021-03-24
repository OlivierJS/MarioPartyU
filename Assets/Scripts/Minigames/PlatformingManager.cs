using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformingManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        theGlobalDataManager = GameObject.FindObjectOfType<GlobalDataManager>();
        theTimer = GameObject.FindObjectOfType<Timer>();
        oldP1coins = theGlobalDataManager.P1amountOfCoins;
        oldP2coins = theGlobalDataManager.P2amountOfCoins;
    }

    int oldP1coins;
    int oldP2coins;
    public bool P1Win;
    public bool P2Win;
    public bool timeUp;
    Timer theTimer;
    GlobalDataManager theGlobalDataManager;

    // Update is called once per frame
    void Update()
    {
        GameEnd();
        TimeUp();
    }

    void GameEnd()
    {
        if ((P1Win == true || P2Win == true || timeUp == true) && theGlobalDataManager.P1amountOfCoins == oldP1coins && theGlobalDataManager.P2amountOfCoins == oldP2coins)
        {
            StartCoroutine(ReturntoBoard());
        }

        else
        {
            StopCoroutine(ReturntoBoard());
        }
    }

    IEnumerator ReturntoBoard()
    {
        theTimer.GameIsDone = true;
        if (P1Win == true)
        {
            theGlobalDataManager.P1amountOfCoins += 10;
            Debug.Log("Player 1 wins!");
        }
        if (P2Win == true)
        {
            theGlobalDataManager.P2amountOfCoins += 10;
            Debug.Log("Player 2 wins!");
        }
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Scene", LoadSceneMode.Single);
    }

    void TimeUp()
    {
        if (theTimer.timeRemaining <= 0)
        {
            timeUp = true;
        }
    }
}
