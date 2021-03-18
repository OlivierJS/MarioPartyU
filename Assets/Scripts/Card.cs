using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<RawImage>();
        theCardgameManager = GameObject.FindObjectOfType<CardgameManager>();
        canSwitchState = true;
        theCardgameManager.P1canMove = true;
        theCardgameManager.P2canMove = true;
    }

    public Texture2D[] CardTextures;
    RawImage image;
    CardgameManager theCardgameManager;
    public int cardState;
    bool canSwitchState;
    bool oneChange;
    public int cardID;
    public Card NextCard;
    public Card PrevCard;


    // Update is called once per frame
    void Update()
    {
        if(canSwitchState == true)
        {
            ChangeState();
        }

        ChangeAppearance();
    }

    void ChangeState()
    {
        if(cardState == 3 && theCardgameManager.P2canMove == true)
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                theCardgameManager.P2canMove = false;
                if(PrevCard.cardState == 1)
                {
                    PrevCard.PrevCard.cardState = 3;
                }
                else
                {
                    PrevCard.cardState = 3;
                }
                cardState = 0;
            }

            if(Input.GetKeyDown(KeyCode.RightArrow))
            {         
                theCardgameManager.P2canMove = false;
                if(NextCard.cardState == 1)
                {
                    NextCard.NextCard.cardState = 3;                   
                }
                else
                {
                    NextCard.cardState = 3;      
                }
                cardState = 0;
            }

            if(Input.GetKeyDown(KeyCode.Space))
            {
                cardState = 4;
                canSwitchState = false;
                theCardgameManager.P2Done = true;
            }
        }
        
        if(cardState == 1 && theCardgameManager.P1canMove == true)
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                theCardgameManager.P1canMove = false;
                if(PrevCard.cardState == 3)
                {
                    PrevCard.PrevCard.cardState = 1;
                }
                else
                {
                    PrevCard.cardState = 1;
                }
                cardState = 0;
            }

            if(Input.GetKeyDown(KeyCode.D))
            {
                theCardgameManager.P1canMove = false;
                if(NextCard.cardState == 3)
                {
                    NextCard.NextCard.cardState = 1;
                }
                else
                {
                    NextCard.cardState = 1;
                }
                cardState = 0;
                Debug.Log("Moving to" + NextCard);
            }

            if(Input.GetKeyDown(KeyCode.Alpha0))
            {
                cardState = 2;
                canSwitchState = false;
                theCardgameManager.P1Done = true;
            }
        }        
    }

    void ChangeAppearance()
    {
        switch(cardState)
        {
            case 0:
              image.texture = CardTextures[0];
            break;

            case 1:
                image.texture = CardTextures[1];
            break;

            case 2:
                if(theCardgameManager.P1Done == true && theCardgameManager.P2Done == true)
                {
                    if(theCardgameManager.winID == cardID)
                    {
                        image.texture = CardTextures[4];
                        Debug.Log("P1 W");
                        theCardgameManager.P1Win = true;
                    }
                    else
                    {
                        image.texture = CardTextures[3];
                    }
                }

            break;  
            
            case 3:
                image.texture = CardTextures[2];
            break;

            case 4:
                if(theCardgameManager.P1Done == true && theCardgameManager.P2Done == true)
                {
                    if(theCardgameManager.winID == cardID)
                    {
                        image.texture = CardTextures[4];
                        theCardgameManager.P2Win = true;
                    }
                    else
                    {
                        image.texture = CardTextures[3];
                    }
                }                
            break;
        }
    }
}
