﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    DiceRoll DiceRoller;
    // Start is called before the first frame update
    void Start()
    {
        DiceRoller = GameObject.FindObjectOfType<DiceRoll>();
    }

    public Tile StartingTile;
    Tile currentTile;
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseUp()
    {
        Debug.Log("HI");
        
        if (DiceRoller.IsDoneRolling == false)
        {
            //je kan nog niet bewegen
            return;
        }

        int spacesToMove = DiceRoller.DiceValue;
        Tile finalTile = currentTile;

        for (int i = 0; i < spacesToMove; i++)
        {
            if (finalTile == null)
            {
                finalTile = StartingTile.NextTiles[0];
            }
            else
            {
                if (finalTile.NextTiles == null || finalTile.NextTiles.Length == 0)
                {
                    Debug.Log("Good job!");
                    Destroy(gameObject);
                }
                else if(finalTile.NextTiles.Length > 0)
                {
                    //keuze van speler moet nog geïmplementeerd worden
                    finalTile = finalTile.NextTiles[0];
                }
                else
                {
                    finalTile = finalTile.NextTiles[0];
                }
                Debug.Log(finalTile.transform.position);
               
            }
        }
        
        if (finalTile == null)
        {
            return;
        }

        //teleporteer player naar finalTile

        this.transform.position = finalTile.transform.position;
        currentTile = finalTile;
    }
}
