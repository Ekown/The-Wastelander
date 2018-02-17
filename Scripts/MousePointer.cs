using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TeamUtility.IO;

public class MousePointer : MonoBehaviour {

    public Texture2D defaultCursor;
    public Texture2D clickedCursor;
    public Texture2D disabledCursor;

    private Vector2 hotSpot = Vector2.zero;
    private Ray ray;

    void Start()
    {
        // Set the default mouse pointer
        Cursor.SetCursor(defaultCursor, hotSpot, CursorMode.Auto);

        //Debug.Log("Path: " + Application.persistentDataPath);
    }

    // Update is called once per frame
    void Update () {
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(InputManager.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if(InputManager.GetMouseButtonDown(0))
            {
                if (hit.collider.gameObject.tag == "ClickableOption")
                {
                    if ((hit.collider.gameObject.name == "Btn_Continue" || hit.collider.gameObject.name == "Btn_LoadGame") && !File.Exists(Application.persistentDataPath + "/gamesave.save"))
                    {
                        Cursor.SetCursor(disabledCursor, hotSpot, CursorMode.Auto);
                    }
                    else
                    {
                        //Debug.Log(hit.collider.gameObject.name);
                        Cursor.SetCursor(clickedCursor, hotSpot, CursorMode.Auto);
                    }   
                }
                else
                {
                    //Debug.Log(hit.collider.gameObject.name);
                    Cursor.SetCursor(defaultCursor, hotSpot, CursorMode.Auto);
                }
            }
            else
            {
                if((hit.collider.gameObject.name == "Btn_Continue" || hit.collider.gameObject.name == "Btn_LoadGame") && !File.Exists(Application.persistentDataPath + "/gamesave.save"))
                {
                    Cursor.SetCursor(disabledCursor, hotSpot, CursorMode.Auto);
                }
                else
                {
                    Cursor.SetCursor(defaultCursor, hotSpot, CursorMode.Auto);
                }               
                
            }
        }
        
    }
}
