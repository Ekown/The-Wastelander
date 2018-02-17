using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TeamUtility.IO;

public class PauseController : MonoBehaviour {

    [SerializeField]
    private GameObject pauseMenu;
    private Ray ray;

    // Axis Configuration for Pause Menu Axis (Pause)
    AxisConfiguration pauseAxisConfig;
	
	// Update is called once per frame
	void Update ()
    {
        if(pauseAxisConfig == null)
            pauseAxisConfig = InputManager.GetAxisConfiguration("Default", "Pause");

        // Check if the player presses the pause keycode
        if (InputManager.GetKeyDown(pauseAxisConfig.positive))
        //if(InputManager.GetAxis("Pause") != 0f)
        {
            // If the pause menu is not yet active, activated it, vice versa (like a switch)
            if (pauseMenu.gameObject.activeSelf == false)
            {
                pauseMenu.gameObject.SetActive(true);

                // This pauses the time 
                Time.timeScale = 0f;
            }
            else if (pauseMenu.gameObject.activeSelf == true)
            {
                pauseMenu.gameObject.SetActive(false);

                // This unpauses the time 
                Time.timeScale = 1.0f;
            }
        }
    }

   public void Resume()
   {
        pauseMenu.gameObject.SetActive(false);

        // This unpauses the time 
        Time.timeScale = 1.0f;
    }

    public void Quit()
    {
        //PlayerPrefs.SetString("NextScene", "MainMenu");

        //SceneManager.LoadScene("LoadingScreen");

        LoadingScreenController.LoadScene(1);
    }
}
