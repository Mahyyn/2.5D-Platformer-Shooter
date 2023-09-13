using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ChangeInputButton : MonoBehaviour
{

    //Reference to the Event System component in the scene
    EventSystem system;
    //The initial input element to be selected when the menu is opened
    public Selectable InitialInput;
    //The submit button for the form
    public Button submitButton;

    //Start is called before the first frame update
    void Start()
    {
        //Get the reference to the event system
        system = EventSystem.current;
        //Set the focus on the initial input element
        InitialInput.Select();
    }

    //Update is called once per frame
    void Update()
    {
        //Check if the tab key and the left shift key are pressed
        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
        {
            //Find the previous selectable UI element
            Selectable previous = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
            //Check if a previous selectable UI element was found
            if (previous != null)
            {
                //Set the focus on the previous selectable UI element
                previous.Select();
            }
        }
        //Check if only the tab key is pressed
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            //Find the next selectable UI element
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            //Check if a next selectable UI element was found
            if (next != null)
            {
                //Set the focus on the next selectable UI element
                next.Select();
            }
        }
        //Check if the return key is pressed
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            //lets the onClick event for the submit button know that it should happen.
            submitButton.onClick.Invoke();
            Debug.Log("Button Pressed");
        }
    }
}
