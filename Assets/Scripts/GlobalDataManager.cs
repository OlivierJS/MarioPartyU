using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDataManager : MonoBehaviour
{
    public static GlobalDataManager Instance;

    //public Tile P1currentTile;
    public int P1currentTileID = 0;
    public int P1amountOfCoins = 0;
    public int P1amountOfStars = 0;
    public int[] P1itemsInventory = { 0, 0, 0 };

    //public Tile P2currentTile;
    public int P2currentTileID = 0;
    public int P2amountOfCoins = 0;
    public int P2amountOfStars = 0;
    public int[] P2itemsInventory = { 0, 0, 0 };

    public int currentTurn = 0;

    //Keeps current instance alive through all scenes
    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
