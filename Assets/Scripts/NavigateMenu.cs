using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NavigateMenu : MonoBehaviour
{
    public NavigateMenu PreviousButton;
    public NavigateMenu NextButton;
    public KeyCode previous;
    public KeyCode next;
    public bool IsSelected;

    // Update is called once per frame
    void Update()
    {
        if(IsSelected == true)
        {
            if(Input.GetKeyDown(previous))
            {
                EventSystem.current.SetSelectedGameObject(PreviousButton.gameObject);
                StartCoroutine(Waiting(PreviousButton));
            }

            if(Input.GetKeyDown(next))
            {
                EventSystem.current.SetSelectedGameObject(NextButton.gameObject);
                StartCoroutine(Waiting(NextButton));
            }
        }
    }

    IEnumerator Waiting(NavigateMenu Button)
    {
        IsSelected = false;
        yield return new WaitForSeconds(0.2f);
        Button.IsSelected = true;
    }
}

