using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TeamUtility.IO;
using System.Text;

public class KeyBindingsController : MonoBehaviour {

    [Header("Panels")]
    public Transform movementPanel;
    public Transform generalPanel;

    [Header("Sliders")]
    public Slider _slider;

    private string temp;
    Event keyEvent;
    TextMeshProUGUI buttonText;
    KeyCode newKey;

    bool waitingForKey;

    // Axis Configuration for Horizontal Axis (Walk Left and Walk Right)
    AxisConfiguration horizontalAxisConfig;

    // Axis Configuration for Interact Axis (Interact)
    AxisConfiguration interactAxisConfig;

    // Axis Configuration for Pause Menu Axis (Pause)
    AxisConfiguration pauseAxisConfig;

    // Axis Configuration for Inventory Axis (Inventory)
    AxisConfiguration inventoryAxisConfig;

    // Axis Configuration for Notebook Axis (Notebook)
    AxisConfiguration notebookAxisConfig;

    // Use this for initialization
    void Start () {

        try
        {
            InputManager.Load();
        }
        catch (System.Exception e)
        {
            print(e);
        }

        //// Find the Movement Panel's Transform
        //movementPanel = transform.Find("MovementPanel");

        //// Find the General Panel's Transform
        //generalPanel = transform.Find("GeneralPanel");

        horizontalAxisConfig = InputManager.GetAxisConfiguration("Default", "Horizontal");
        interactAxisConfig = InputManager.GetAxisConfiguration("Default", "Interact");
        pauseAxisConfig = InputManager.GetAxisConfiguration("Default", "Pause");
        inventoryAxisConfig = InputManager.GetAxisConfiguration("Default", "Inventory");
        notebookAxisConfig = InputManager.GetAxisConfiguration("Default", "Notebook");

        // Set the WaitingForKey to false
        waitingForKey = false;

        InitializeAxisConfig();

        //	The axis config needs to be reinitialized because loading can invalidate
        //	the input configurations
        InputManager.Instance.Loaded += InitializeAxisConfig;
    }

    private void OnDestroy()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.Loaded -= InitializeAxisConfig;
        }
    }

    private void InitializeAxisConfig()
    {
        // Traverse the Movement Panel for the Buttons and change the buttons' text to the PlayerPrefs.InputManager value
        for (int i = 0; i < movementPanel.childCount; i++)
        {
            if (i >= 7)
            {
                switch (movementPanel.GetChild(i).name)
                {
                    case "Walk Left Button":
                        movementPanel.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = horizontalAxisConfig.negative.ToString();
                        break;

                    case "Walk Right Button":
                        movementPanel.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = horizontalAxisConfig.positive.ToString();
                        break;

                    //case "Up Button":
                    //    movementPanel.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = InputManager.GM.Up.ToString();
                    //    break;

                    //case "Down Button":
                    //    movementPanel.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = InputManager.GM.Down.ToString();
                    //    break;

                    //case "Sprint Button":
                    //    movementPanel.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = InputManager.GM.Sprint.ToString();
                    //    break;

                    case "Interact Button":
                        movementPanel.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = interactAxisConfig.positive.ToString();
                        break;
                }

            }
        }

        // Traverse the General Panel for the Buttons and change the buttons' text to the PlayerPrefs.InputManager value
        for (int i = 0; i < generalPanel.childCount; i++)
        {
            if (i >= 3)
            {
                switch (generalPanel.GetChild(i).name)
                {
                    case "Pause Button":
                        generalPanel.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = pauseAxisConfig.positive.ToString();
                        break;

                    case "Inventory Button":
                        generalPanel.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = inventoryAxisConfig.positive.ToString();
                        break;

                    case "Open Notebook Button":
                        generalPanel.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = notebookAxisConfig.positive.ToString();
                        break;
                }
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnGUI()
    {
        // Get the current event 
        keyEvent = Event.current;

        // If the current event is a keypress and the waitingForkey is true
        if(keyEvent.isKey && waitingForKey)
        {
            // Store the current pressed key to the newKey variable
            newKey = keyEvent.keyCode;

            // Set the waitingForKey to false again
            waitingForKey = false;
        }
    }

    public void StartAssignment(string keyName)
    {
        if (!waitingForKey)
            StartCoroutine(AssignKey(keyName));
    }

    public void SendText(TextMeshProUGUI text)
    {
        buttonText = text;
    }

    // An infinite loop that waits for a single pressed key
    IEnumerator WaitForKey()
    {
        while (!keyEvent.isKey)
        {
            yield return null;
        }
    }

    
    public IEnumerator AssignKey(string keyName)
    {
        waitingForKey = true;

        yield return WaitForKey();

        switch (keyName)
        {
            case "Left":
                horizontalAxisConfig.negative = newKey;
                print(horizontalAxisConfig.negative);

                buttonText.text = horizontalAxisConfig.negative.ToString();
                break;

            case "Right":
                horizontalAxisConfig.positive = newKey;
                buttonText.text = horizontalAxisConfig.positive.ToString();
                break;

            //case "Up":
            //    InputManager.GM.Up = newKey;
            //    buttonText.text = InputManager.GM.Up.ToString();
            //    PlayerPrefs.SetString("upKey", InputManager.GM.Up.ToString());
            //    break;

            //case "Down":
            //    InputManager.GM.Down = newKey;
            //    buttonText.text = InputManager.GM.Down.ToString();
            //    PlayerPrefs.SetString("downKey", InputManager.GM.Down.ToString());
            //    break;

            case "Interact":
                interactAxisConfig.positive = newKey;
                buttonText.text = interactAxisConfig.positive.ToString();
                break;

            //case "Sprint":
            //    InputManager.GM.Sprint = newKey;
            //    buttonText.text = InputManager.GM.Sprint.ToString();
            //    PlayerPrefs.SetString("sprintKey", InputManager.GM.Sprint.ToString());
            //    break;

            case "Pause":
                pauseAxisConfig.positive = newKey;
                buttonText.text = pauseAxisConfig.positive.ToString();
                break;

            case "Inventory":
                inventoryAxisConfig.positive = newKey;
                buttonText.text = inventoryAxisConfig.positive.ToString();
                break;
    
            case "Notebook":
                notebookAxisConfig.positive = newKey;
                buttonText.text = notebookAxisConfig.positive.ToString();
                break;
        }

        try
        {
            InputManager.Save();
        }
        catch(System.Exception e)
        {
            print(e);
        }

        yield return null;
    }

}
