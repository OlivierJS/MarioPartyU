using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    Material myMaterial;
    Renderer m_Renderer;
    public Texture2D[] TileTexture;
    StateManager theStateManager;
    Player player;
    Player otherplayer;
    public GameObject LuckySpaceMenu;
    public Text MenuText;
    int MenuOption;
    bool waiting;
    int effect;
    float timeRemaining;


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
            //Lucky Tile
            case 4: 
                m_Renderer.material.SetTexture("_MainTex", TileTexture[0]);
                break;
            case 5:
                m_Renderer.material.SetTexture("_MainTex", TileTexture[1]);
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
        public Tile PrevTile;
        public int tileTypeID;
        public int tileID;

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
            waiting = true;
            timeRemaining = 5;
            effect = UnityEngine.Random.Range(1, 5);
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
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                MenuOption += 1;
                Debug.Log(timeRemaining);
                LuckySpaceMenu.SetActive(true);
            }
            if(timeRemaining <= 2)
            {
                MenuOption = effect;
            }

            if(timeRemaining <= 0 && waiting == true)
            {
                timeRemaining = 0;
                LuckySpaceMenu.SetActive(false);
                if(theStateManager.IsDoneAnimating == false)
                {
                    player = theStateManager.PlayersList[theStateManager.currentPlayerID];
                    otherplayer = theStateManager.PlayersList[(theStateManager.currentPlayerID + 1) % theStateManager.numberOfPlayer];
                    LuckySpaceEffects();
                }
                theStateManager.IsDoneAnimating = true;
                waiting = false;
                MenuOption = 0;
                
            }
            MenuScroll();
        }
    public void LuckySpaceEffects()
    {
        switch (effect)
        {
            case 1:
                player.amountOfCoins += UnityEngine.Random.Range(3, 6);
            break;
            case 2:
                otherplayer.amountOfCoins -= UnityEngine.Random.Range(3, 6);
                if (otherplayer.amountOfCoins < 0)
                {
                    otherplayer.amountOfCoins = 0;
                }
            break;
            case 3:
                int temp_coin;
                int temp_coin2;
                temp_coin = player.amountOfCoins;
                temp_coin2 = otherplayer.amountOfCoins;
                player.amountOfCoins = temp_coin2; 
                otherplayer.amountOfCoins = temp_coin;
            break;
            case 4:
                GetItem();
            break;
        }
    }

    public void MenuScroll()
    {
        switch(MenuOption % 5)
        {
            case 1:
                MenuText.text = "     Lucky Tile" + "\n> +3-5 Coins to yourself" +" \n  -3-5 Coins to enemy" + "\n  Switch Coins with enemy" + "\n  Get item";
            break;
            case 2:
                MenuText.text = "     Lucky Tile" + "\n  +3-5 Coins to yourself" +" \n> -3-5 Coins to enemy" + "\n  Switch Coins with enemy" + "\n  Get item";
            break;
            case 3:
                MenuText.text = "     Lucky Tile" + "\n  +3-5 Coins to yourself" +" \n  -3-5 Coins to enemy" + "\n> Switch Coins with enemy" + "\n  Get item";
            break;
            case 4:
                MenuText.text = "     Lucky Tile" + "\n  +3-5 Coins to yourself" +" \n  -3-5 Coins to enemy" + "\n  Switch Coins with enemy" + "\n> Get item";
            break;
            }
    }
}
