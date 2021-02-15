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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int DiceValue;

    public bool IsDoneRolling = false;

    public Texture2D[] DiceTexture;

    public void NewTurn()
    {
        IsDoneRolling = false;
    }
    public void RollDice()
    {
        DiceValue = Random.Range(1, 7);
        Debug.Log(DiceValue);
        // met een animatie zouden we eerst moeten wachten op het einde van de animatie
        IsDoneRolling = true;

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
