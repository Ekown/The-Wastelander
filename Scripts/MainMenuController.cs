using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamUtility.IO;

public class MainMenuController : MonoBehaviour {

    private Ray ray;
    private GameObject exitPanel;

    // Use this for initialization
    void Start () {
        exitPanel = GameObject.Find("AreYouSurePanel");

        FindObjectOfType<AudioManager>().Play("WindAmbience");
	}

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(InputManager.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (InputManager.GetMouseButtonDown(0))
            {
                // If the player, clicks 'Yes' in the AreYouSurePanel to exit the game
                if (hit.collider.gameObject.name == "Btn_Yes")
                {
                    

                    // Exits the game application
                    Debug.Log("Exit the game");
                    AppHelper.Quit();
                }
            }
        }
    }
}
