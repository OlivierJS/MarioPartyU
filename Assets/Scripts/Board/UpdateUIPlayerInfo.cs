using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUIPlayerInfo : MonoBehaviour
{
    public Player InfoPlayer;

    public Sprite ItemImageDoubleDice;
    public Sprite ItemImageTripleDice;
    public Sprite ItemImageMiniDice;
    public Sprite ItemImageGoldenPipe;
    public Sprite ItemImageEmpty;


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

        for (int i = 0; i < 3; i++)
        {
            switch (InfoPlayer.itemsInventory[i])
            {
                case 0:
                    this.transform.GetChild(i).GetComponent<Image>().sprite = ItemImageEmpty;
                    break;
                case 1:
                    this.transform.GetChild(i).GetComponent<Image>().sprite = ItemImageDoubleDice;
                    break;
                case 2:
                    this.transform.GetChild(i).GetComponent<Image>().sprite = ItemImageTripleDice;
                    break;
                case 3:
                    this.transform.GetChild(i).GetComponent<Image>().sprite = ItemImageMiniDice;
                    break;
                case 4:
                    this.transform.GetChild(i).GetComponent<Image>().sprite = ItemImageGoldenPipe;
                    break;
                default:
                    this.transform.GetChild(i).GetComponent<Image>().sprite = ItemImageEmpty;
                    break;
            }
        }
    }
}
