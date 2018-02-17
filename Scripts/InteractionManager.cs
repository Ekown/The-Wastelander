using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TeamUtility.IO;

public class InteractionManager : MonoBehaviour {

    Event keyEvent;
    public GameObject panel;
    public TextMeshProUGUI interactText;

    private bool waitingKey = false;
    private IEnumerator coroutine;
    public Animator panelPopupAnim;
    int popupHash = Animator.StringToHash("isPopup");
    int popoutHash = Animator.StringToHash("isPopOut");

    // Use this for initialization
    void Start () {
        //panelPopupAnim = GetComponent<Animator>();
	}
	
	void OnTriggerEnter2D(Collider2D collider)
    {
        // If the collider is an interactable
        if(collider.gameObject.CompareTag("Interactable"))
        {
            Debug.Log("Collided with an interactable");

            if(collider.gameObject.name.Contains("PlayerMov"))
            {

                // Enables the interact text
                if (interactText != null)
                    interactText.gameObject.SetActive(true);

                waitingKey = true;

                coroutine = WaitForKey();
                StartCoroutine(coroutine);
                
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        // Disables the interact text
        if (interactText != null)
            interactText.gameObject.SetActive(false);

        if (waitingKey == true)
        {
            StopCoroutine(coroutine);
            waitingKey = false;

            Debug.Log("Stopped coroutines");
        }
    }

    private IEnumerator WaitForKey()
    {
        // The coroutine will only work if it is waiting for a key
        if(waitingKey)
        {
            Debug.Log("Waiting..");

            // Wait for input
            while (!InputManager.GetKeyDown(InputManager.GetAxisConfiguration("Default", "Interact").positive))
            {
                if (waitingKey)
                    yield return null;
                else
                    yield break;
            }

            // Activate the panel and text within
            panel.gameObject.SetActive(true); 

            yield return StartCoroutine(PanelPause());
            
            yield return null;

            yield return StartCoroutine(WaitForKey());
            
        }
        
    }

    private IEnumerator PanelPause()
    {
        yield return null;

        Debug.Log("Pause");

        Time.timeScale = 0f;

        // Wait for input
        while (!InputManager.GetKeyDown(InputManager.GetAxisConfiguration("Default", "Interact").positive))
        {
            yield return null;
        }

        Time.timeScale = 1f;

        // Deactivate the panel and text within
        panelPopupAnim.SetTrigger(popoutHash);

        yield return new WaitForSeconds(1f);

        panel.gameObject.SetActive(false);

        Debug.Log("Un-Pause");
    }


}
