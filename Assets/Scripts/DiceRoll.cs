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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    StateManager theStateManager;

    public int DiceValue;

    public Texture2D[] DiceTexture;

    public void RollDice()
    {
        if (theStateManager.IsDoneRolling == true)
        {
            //Al gerold
            return;
        }

        DiceValue = Random.Range(1, 7);
        Debug.Log("You rolled: " + DiceValue);
        // met een animatie zouden we eerst moeten wachten op het einde van de animatie
        theStateManager.IsDoneRolling = true;

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
