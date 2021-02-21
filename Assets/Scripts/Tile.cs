using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    Material myMaterial;

    void tileTypeSetup()
    {
        myMaterial = GetComponent<Renderer>().material;
        switch (tileTypeID)
        {
            //Normal Tile
            case 0:
                myMaterial.color = Color.green;
                break;
            //+3 coins Tile
            case 1:
                myMaterial.color = Color.blue;
                break;
            //-3 coins Tile
            case 2:
                myMaterial.color = Color.red;
                break;
            //Star Tile
            case 3:
                myMaterial.color = Color.yellow;
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        tileTypeSetup();
    }

    public Tile[] NextTiles;
    public int tileTypeID;

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
