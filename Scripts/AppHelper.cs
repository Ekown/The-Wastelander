using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class AppHelper
{
     #if UNITY_WEBPLAYER
     public static string webplayerQuitURL = "http://google.com";
     #endif
     public static void Quit()
     {
        //if (PlayerPrefs.HasKey("NextScene"))
        //{
        //    PlayerPrefs.DeleteKey("NextScene");
        //}

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
         #elif UNITY_WEBPLAYER
         Application.OpenURL(webplayerQuitURL);
         #else
         Application.Quit();
         #endif
     }
}
