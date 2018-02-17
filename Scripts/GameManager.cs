using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using TeamUtility.IO;

public class GameManager : MonoBehaviour
{

    public Button continueButton;
    public Button newButton;
    public Button loadButton;
    //public TextMeshProUGUI loadingText;

    private string currentSceneName;

    void Start()
    {
        // Create a temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        currentSceneName = currentScene.name; 
    }

    void Update()
    {
        //loadingText.color = new Color();
    }

    private SaveController CreateSaveGameObject()
    {
        SaveController save = new SaveController();

        save.newGame = false;

        return save;
    }

    public void SaveGame()
    {
        SaveController save = CreateSaveGameObject();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();

        Debug.Log("Game Saved at " + Application.persistentDataPath + "/gamesave.save");
    }

    public void LoadGame()
    {

        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            // Enable the submenu for "Continue Game" if the current scene is the "Main Menu"
            if (currentSceneName == "MainMenu")
            {
                continueButton.gameObject.SetActive(true);
                newButton.gameObject.SetActive(true);
                loadButton.gameObject.SetActive(true);
            }

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            SaveController save = (SaveController)bf.Deserialize(file);
            file.Close();

            Debug.Log("Game Loaded from " + Application.persistentDataPath + "/gamesave.save");
        }
        else
        {
            // Jumps right into creating a new gamesave data in the "Main Menu" scene
            if (currentSceneName == "MainMenu")
            {
                continueButton.gameObject.SetActive(true);
                newButton.gameObject.SetActive(true);
                loadButton.gameObject.SetActive(true);

                continueButton.interactable = false;
                loadButton.interactable = false;

            }

            Debug.Log("No save data in !" + Application.persistentDataPath);
        }
    }

    public void NewGame()
    {
        //if (!PlayerPrefs.HasKey("NextScene"))
        //{
        //    PlayerPrefs.SetString("NextScene", "TutorialLevel");
        //}

        //SceneManager.LoadScene("LoadingScreen");

        LoadingScreenController.LoadScene(2);
    }
}