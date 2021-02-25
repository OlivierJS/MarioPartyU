using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUIPlayerInfo : MonoBehaviour
{
    public Player InfoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        myText = GetComponent<Text>();
    }

    Text myText;

    string[] numberWords = { "One", "Two" };

    // Update is called once per frame
    void Update()
    {
        myText.text = "Player " + numberWords[InfoPlayer.playerID] + "\nCoins: " + InfoPlayer.amountOfCoins + "\nStars: " + InfoPlayer.amountOfStars;
    }
}
