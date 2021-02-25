using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    Material myMaterial;
    Renderer m_Renderer;
    public Texture2D[] TileTexture;
    StateManager theStateManager;
    public Player[] PlayerList;
    Player player;
    Player player2;


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
            case 4: 
                m_Renderer.material.SetTexture("_MainTex", TileTexture[0]);
            break;
        }
    }

        // Start is called before the first frame update
        void Start()
        {
            m_Renderer = GetComponent<Renderer>();
            tileTypeSetup();
            theStateManager = GameObject.FindObjectOfType<StateManager>();
        }

        public Tile[] NextTiles;
        public int tileTypeID;

        public void TileEffects(Tile tile,int coins)
        {
                switch (tile.tileTypeID)
                {
                    //Nothing
                    case 0:
                        Debug.Log("You have this amount of coins: " + coins);
                    break;
                    //+3 coins
                    case 1:
                        coins += 3;
                        Debug.Log("You have this amount of coins: " + coins);
                    break;
                    //-3 coins
                    case 2:
                        coins -= 3;
                        if (coins < 0)
                        {
                            coins = 0;
                        }
                        Debug.Log("You have this amount of coins: " + coins);
                    break;
                    //Lucky space: random keuze tussen +5 coins aan iemand, -5 coins, een item, of coins wisselen met andere speler
                    case 4:
                        int[] temp_list = new int[theStateManager.numberOfPlayer];
                        for (int i = 0; i < theStateManager.numberOfPlayer; i++)
                        {
                            temp_list[i] = i;
                        }
                        
                        int a = UnityEngine.Random.Range(0, temp_list.Length);
                        int p1 = temp_list[a];
                        RemoveAt(ref temp_list, a);
                        int b = UnityEngine.Random.Range(0, temp_list.Length);
                        int p2 = temp_list[b];

                        LuckySpace(p1, p2);
                        
                    break;
                }
        }

        public void LuckySpace(int Player_ID1, int player_ID2)
        {
            //int effect = UnityEngine.Random.Range(1, 3);
            int effect = 4;
            player = PlayerList[Player_ID1];
            player2 = PlayerList[player_ID2];
            
;           switch (effect)
            {
                case 1:
                    player.amountOfCoins += UnityEngine.Random.Range(3, 6);
                break;
                case 2:
                    player.amountOfCoins -= UnityEngine.Random.Range(3, 6);
                break;
                case 3:
                    GetItem();
                break;
                case 4:
                    int temp_coin;
                    int temp_coin2;
                    temp_coin = player.amountOfCoins;
                    temp_coin2 = player2.amountOfCoins;
                    player.amountOfCoins = temp_coin2; 
                    player2.amountOfCoins = temp_coin;
                break;
            }
        }

        public void GetItem()
        {
            Debug.Log("You got an item!");
        }
        // Update is called once per frame
        void Update()
        {
        
        }
        public static void RemoveAt<T>(ref T[] arr, int index)
        {
            // replace the element at index with the last element
            arr[index] = arr[arr.Length - 1];
            // finally, let's decrement Array's size by one
            Array.Resize(ref arr, arr.Length - 1);       
        }
                       
}
