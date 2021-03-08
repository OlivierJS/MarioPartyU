using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRoll : MonoBehaviour
{
    Renderer m_Renderer;
    // Start is called before the first frame update
    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
        theStateManager = GameObject.FindObjectOfType<StateManager>();
        maxDiceValue = 6;

    }

    // Update is called once per frame
    void Update()
    {
        if (theStateManager.IsDoneRolling == false && stopRandom == false)
        {
            switch (Random.Range(1, 7))
            {
                case 1:
                    m_Renderer.material.SetTexture("_MainTex", DiceTexture[0]);
                break;

                case 2:
                    m_Renderer.material.SetTexture("_MainTex", DiceTexture[1]);
                break;

                case 3:
                    m_Renderer.material.SetTexture("_MainTex", DiceTexture[2]);
                break;

                case 4:
                    m_Renderer.material.SetTexture("_MainTex", DiceTexture[3]);
                break;

                case 5:
                    m_Renderer.material.SetTexture("_MainTex", DiceTexture[4]);
                break;

                case 6:
                    m_Renderer.material.SetTexture("_MainTex", DiceTexture[5]);
                break;
            }
        }
    }

    StateManager theStateManager;
    public int DiceValue;
    public Texture2D[] DiceTexture;
    public int maxDiceValue;
    public bool stopRandom;

    public void RollDice()
    {
        if (theStateManager.IsDoneRolling == true)
        {
            //Al gerold
            return; 
        }


        DiceValue = Random.Range(1, maxDiceValue + 1);
        stopRandom = true;
        // met een animatie zouden we eerst moeten wachten op het einde van de animatie

        switch (DiceValue)
        {
            case 1:
                m_Renderer.material.SetTexture("_MainTex", DiceTexture[0]);
            break;

            case 2:
                m_Renderer.material.SetTexture("_MainTex", DiceTexture[1]);
            break;

            case 3:
                m_Renderer.material.SetTexture("_MainTex", DiceTexture[2]);
            break;

            case 4:
                m_Renderer.material.SetTexture("_MainTex", DiceTexture[3]);
            break;

            case 5:
                m_Renderer.material.SetTexture("_MainTex", DiceTexture[4]);
            break;

            case 6:
                m_Renderer.material.SetTexture("_MainTex", DiceTexture[5]);
            break;
        }
    }
}
