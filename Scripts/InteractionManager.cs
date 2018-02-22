using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TeamUtility.IO;
using System;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour {

    Event keyEvent;
    public GameObject panel;
    public RawImage panelImage;
    public TextMeshProUGUI interactText;

    // Bool variables
    private bool waitingKey = false;
    private bool updatedText = false;
    private bool isIntro = true;

    private IEnumerator coroutine;
    private Collider2D itemCollided;
    private string interactKey;

    // Animation variables
    public Animator panelPopupAnim;
    int popupHash = Animator.StringToHash("isPopup");
    int popoutHash = Animator.StringToHash("isPopOut");
    int showPanelImage = Animator.StringToHash("showImage");
    int hidePanelImage = Animator.StringToHash("hideImage");

    // Use this for initialization
    void Start () {
        //panelPopupAnim = GetComponent<Animator>();

        //StartCoroutine(Wait());

        //if (waitingKey == false)
        //{
        //    waitingKey = true;

        //    coroutine = WaitForKey();

        //    StartCoroutine(coroutine);
        //}

        isIntro = false;
    }
	
	void OnTriggerEnter2D(Collider2D collider)
    {
        // If the collider is an interactable
        if(collider.gameObject.CompareTag("Interactable"))
        {
            //if(collider.gameObject.name.Contains("PlayerMov"))
            //{
            

                // Enables and dynamically changes the interact text
                if (interactText != null)
                {
                    // If the interact key is set to NULL
                    if(interactKey == null)
                    {
                        interactKey = InputManager.GetAxisConfiguration("Default", "Interact").positive.ToString();
                    }

                    interactText.gameObject.SetActive(true);
                    interactText.gameObject.GetComponent<TextMeshProUGUI>().text = "Press " + interactKey + " to interact";
                }
                    
                if(waitingKey == false)
                {

                    waitingKey = true;

                    coroutine = WaitForKey();

                    itemCollided = collider;

                Debug.Log("Collided with " + itemCollided.gameObject.name);

                StartCoroutine(coroutine);
                }
      
            //}
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        // Disables the interact text
        if (interactText != null)
            interactText.gameObject.SetActive(false);          

        if (waitingKey == true)
        {
            itemCollided = null;
            updatedText = false;

            StopCoroutine(coroutine);
            waitingKey = false;

            if (panelImage.gameObject.activeSelf == true)
            {
                panelImage.gameObject.GetComponent<RawImage>().texture = null;
                panelImage.gameObject.SetActive(true);
            }

            Debug.Log("Stopped coroutines");
        }
    }

    private IEnumerator WaitForKey()
    {
        // The coroutine will only work if it is waiting for a key
        if (waitingKey)
        {
            //if (isIntro == false)
            //{
                interactText.gameObject.GetComponent<TextMeshProUGUI>().text = "Press " + interactKey + " to interact";

                Debug.Log("Waiting..");

                // Wait for input
                while (!InputManager.GetKeyDown(InputManager.GetAxisConfiguration("Default", "Interact").positive))
                {
                    if (waitingKey)
                        yield return null;
                    else
                        yield break;
                }
            //}
            //else
            //{
                //interactText.gameObject.GetComponent<TextMeshProUGUI>().text = "";

                //StartCoroutine(Wait());
            //}

            // Activate the panel and text within
            panel.gameObject.SetActive(true);
            
            if(itemCollided.gameObject.name == "Instruction-Sign-SaveLoad")
            {
                panelPopupAnim.SetTrigger(showPanelImage);
            }
            else
            {
                panelPopupAnim.SetTrigger(popupHash);
            }

            // Update the panel text
            UpdatePanelText();
            
            yield return StartCoroutine(PanelPause());
            
            yield return null;

            yield return StartCoroutine(WaitForKey());
            
        }
        
    }

    private IEnumerator PanelPause()
    {
        yield return null;

        interactText.gameObject.GetComponent<TextMeshProUGUI>().text = "Press " + interactKey + " to continue";

        Debug.Log("Pause");

        Time.timeScale = 0f;

        // Wait for input
        while (!InputManager.GetKeyDown(InputManager.GetAxisConfiguration("Default", "Interact").positive))
        {
            yield return null;
        }

        Time.timeScale = 1f;

        // Deactivate the panel and text within
        //panelPopupAnim.SetTrigger(popoutHash);

        //panel.gameObject.GetComponent<TextMeshProUGUI>().text = "";

        if (itemCollided.gameObject.name == "Instruction-Sign-SaveLoad")
        {
            panelPopupAnim.SetTrigger(hidePanelImage);
        }
        else
        {
            panelPopupAnim.SetTrigger(popoutHash);
        }

        yield return new WaitForSeconds(1f);

        panel.gameObject.SetActive(false);

        Debug.Log("Un-Pause");
    }

    private void UpdatePanelText()
    {
        if (updatedText == false)
        {
            string panelText;

            // Get the panel description from the interactable            
            panelText = itemCollided.gameObject.GetComponent<PanelContainer>().panel.panelDescription;

            Debug.Log("Item has description of :" + panelText);

            // If panel description has '<walk left button>' keyword
            if (panelText.IndexOf("<walk left button>") != -1)
            {
                //try
                //{
                //    string walkLeftButton = InputManager.PlayerOneConfiguration.axes[0].positive.ToString();
                //    string walkRightButton = InputManager.GetAxisConfiguration("Default", "Horizontal").positive.ToString();
                //}
                //catch(Exception e)
                //{
                //    if(InputManager == null)
                //    {

                //    }
                //}

                string walkLeftButton = InputManager.PlayerOneConfiguration.axes[0].positive.ToString();
                string walkRightButton = InputManager.PlayerOneConfiguration.axes[0].negative.ToString();



                panelText = panelText.Replace("<walk left button>", "[" + walkLeftButton + "]");
                panelText = panelText.Replace("<walk right button>", "[" + walkRightButton + "]");
            }
            else if(panelText.IndexOf("<interact>") != -1)
            {
                //Debug.Log("There is interact in the description");

                string interactButton = InputManager.PlayerOneConfiguration.axes[1].positive.ToString();
                string pauseButton = InputManager.PlayerOneConfiguration.axes[2].positive.ToString();
                string notebookButton = InputManager.PlayerOneConfiguration.axes[3].positive.ToString();

                panelText = panelText.Replace("<interact>", "[" + interactButton + "]");
                panelText = panelText.Replace("<pause>", "[" + pauseButton + "]");
                panelText = panelText.Replace("<notebook>", "[" + notebookButton + "]");
            }
            else if(panelText.IndexOf("autosave feature") != -1)
            {
                ActivatePanelImage();
            }

            // Replace the PanelText's text component with the Panel Description of the Interactable
            try
            {            
                // If the panel's text component is not empty or null
                //if(! String.IsNullOrEmpty(panel.gameObject.GetComponentInChildren<TextMeshProUGUI>().text))
                //{
                //    Debug.Log("Panel text changed into: " + panelText);
                //    panel.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "";
                //}

                panel.gameObject.GetComponentInChildren<TextMeshProUGUI>(true).text = panelText;
            }
            catch(NullReferenceException e)
            {
                // Start of Debugging

                if(panel == null)
                {
                    Debug.LogWarning("Panel reference is set to null!");
                }
                else if(panel.gameObject == null)
                {
                    Debug.LogWarning("Panel GameObject reference is set to null!");
                }
                else if(panel.gameObject.GetComponentInChildren<TextMeshProUGUI>() == null)
                {
                    Debug.LogWarning("PanelText's TextMeshProUGUI component reference is set to null!");
                }
                else if(panelText == null)
                {
                    Debug.LogWarning("Panel description component from Interactable Gameobject is set to null!");
                }
                
                // End of Debugging
            }

            isIntro = false;

            updatedText = true;

        }
    }

    private IEnumerator Wait(int seconds)
    {
        Debug.Log("Waiting for " + seconds);
        yield return new WaitForSeconds(10f);
    }

    private void ActivatePanelImage()
    {
        Texture2D saveTexture = Resources.Load("InitialSprites/SaveIcon/compact-disc") as Texture2D;

        Debug.Log(saveTexture);

        //panelImage.gameObject.SetActive(true);

        panelImage.gameObject.GetComponent<RawImage>().texture = saveTexture;
    }
}
