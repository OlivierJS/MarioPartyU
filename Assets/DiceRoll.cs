﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int DiceValue;

    public void RollDice()
    {
        DiceValue = Random.Range(1, 7);
        Debug.Log(DiceValue);
    }
}
