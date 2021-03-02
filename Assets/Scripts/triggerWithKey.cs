using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class triggerWithKey : MonoBehaviour
{
    public KeyCode key1;
    public KeyCode key2;
    StateManager theStateManager;

    void Start()
    {
        theStateManager = GameObject.FindObjectOfType<StateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (theStateManager.currentPlayerID == 0)
        {
            if (Input.GetKeyDown(key1))
            {
                GetComponent<Button>().onClick.Invoke();
            }
        }

        else
        {
            if (Input.GetKeyDown(key2))
            {
                GetComponent<Button>().onClick.Invoke();
            }
        }
    }
}
