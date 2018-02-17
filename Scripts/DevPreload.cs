using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DevPreload : MonoBehaviour {

    void Awake()
    {
        GameObject check = GameObject.Find("Universe");
        if (check == null)
        {
            SceneManager.LoadScene("00 - Universe");
        }
    }
}
