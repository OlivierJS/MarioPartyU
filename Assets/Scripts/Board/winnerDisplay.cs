using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class winnerDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        theStateManager = GameObject.FindObjectOfType<StateManager>();
        myText = GetComponent<Text>();
    }

    public Player[] allPlayers;

    StateManager theStateManager;
    Text myText;

    string[] numberWords = { "One", "Two" };

    string decideWinner()
    {
        string winnerMessage = "";

        int currentHighStars = 0;
        int currentHighCoins = 0;
        int currentPlayerID = 0;

        for (int i = 0; i < allPlayers.Length; i++)
        {
            if (allPlayers[i].amountOfStars > currentHighStars)
            {
                currentHighStars = allPlayers[i].amountOfStars;
                currentHighCoins = allPlayers[i].amountOfCoins;
                currentPlayerID = allPlayers[i].playerID;
            }
            else if (allPlayers[i].amountOfStars == currentHighStars && allPlayers[i].amountOfCoins > currentHighCoins)
            {
                currentHighCoins = allPlayers[i].amountOfCoins;
                currentPlayerID = allPlayers[i].playerID;
            }
            else if (allPlayers[i].amountOfStars == currentHighStars && allPlayers[i].amountOfCoins == currentHighCoins)
            {
                currentPlayerID = 42;
            }
        }

        switch (currentPlayerID)
        {
            case 0:
                winnerMessage = "Player One!";
                break;
            case 1:
                winnerMessage = "Player Two!";
                break;
            default:
                winnerMessage = "No-one!";
                break;
        }

        return winnerMessage;
    }

    // Update is called once per frame
    void Update()
    {
        if (theStateManager.gameFinished == false)
        {
            myText.text = "";
        }
        else
        {
            myText.text = "The Winner is:\n" + decideWinner();
        }
    }
}
