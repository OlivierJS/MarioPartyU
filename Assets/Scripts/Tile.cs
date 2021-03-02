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
    Player player;
    Player otherplayer;


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

        public void TileEffects(Tile tile, Player player)
        {
                switch (tile.tileTypeID)
                {
                    //Nothing
                    case 0:
                        
                    break;
                    //+3 coins
                    case 1:
                        player.amountOfCoins += 3;
                        
                    break;
                    //-3 coins
                    case 2:
                        player.amountOfCoins -= 3;
                        
                    break;
                    //Lucky space: random keuze tussen +3-5 coins aan iemand, -3-5 coins, een item, of coins wisselen met andere speler
                    case 4:
                        /*int[] temp_list = new int[theStateManager.numberOfPlayer];
                        for (int i = 0; i < theStateManager.numberOfPlayer; i++)
                        {
                            temp_list[i] = i;
                        }
                        
                        int a = UnityEngine.Random.Range(0, temp_list.Length);
                        int p1 = temp_list[a];
                        RemoveAt(ref temp_list, a);
                        int b = UnityEngine.Random.Range(0, temp_list.Length);
                        int p2 = temp_list[b];*/

                        LuckySpace();
                        
                    break;
                }
        }

        public void LuckySpace()
        {
        int effect = UnityEngine.Random.Range(1, 5);
            player = theStateManager.PlayersList[theStateManager.currentPlayerID];
            otherplayer = theStateManager.PlayersList[(theStateManager.currentPlayerID + 1) % theStateManager.numberOfPlayer];
            
;           switch (effect)
            {
                case 1:
                    player.amountOfCoins += UnityEngine.Random.Range(3, 6);
                break;
                case 2:
                    otherplayer.amountOfCoins -= UnityEngine.Random.Range(3, 6);
                break;
                case 3:
                    Debug.Log(player);
                    GetItem();
                break;
                case 4:
                    int temp_coin;
                    int temp_coin2;
                    temp_coin = player.amountOfCoins;
                    temp_coin2 = otherplayer.amountOfCoins;
                    player.amountOfCoins = temp_coin2; 
                    otherplayer.amountOfCoins = temp_coin;
                break;
            }
        }

        public void GetItem()
        {
            for (int i = 0; i < player.itemsInventory.Length; i++)
            {
                if (player.itemsInventory[i] == 0)
                {
                    player.itemsInventory[i] = UnityEngine.Random.Range(1, 5);
                    Debug.Log("You got an item!");
                    break;
                }
                else
                {
                Debug.Log("Inventory Full!");
            }
            }
        }
        // Update is called once per frame
        void Update()
        {
        
        }
       /* public static void RemoveAt<T>(ref T[] arr, int index)
        {
            // replace the element at index with the last element
            arr[index] = arr[arr.Length - 1];
            // finally, let's decrement Array's size by one
            Array.Resize(ref arr, arr.Length - 1);       
        }
       */                
}
