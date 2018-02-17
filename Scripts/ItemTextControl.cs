using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ItemTextControl : MonoBehaviour 
{

	public Transform popuptext; //Text object
	public static string textstatus = "off";
    public GameObject emil;

    //void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (textstatus == "off")
    //    {
    //        if (gameObject.name == "Tin-Can")
    //        {
    //            popuptext.GetComponent<TextMesh>().text = "Tin Can\n Aluminum Cans are recyclable";
    //        }

    //        else if (gameObject.name == "Box")
    //        {
    //            popuptext.GetComponent<TextMesh>().text = "Box\n Boxes are recyclable but depends on\n the its condition";
    //        }

    //        else if (gameObject.name == "Instruction-Sign-PlayerMov")
    //        {
    //            popuptext.GetComponent<TextMesh>().text = "Player Movement\nUse your arrow keys to move left, right\n up and down";
    //        }

    //        else if (gameObject.name == "Instruction-Sign-HoverItems")
    //        {
    //            popuptext.GetComponent<TextMesh>().text = "Items\nYou can view the item descriptions or info\nby hovering your mouse on it";
    //        }

    //        else if (gameObject.name == "Instruction-Sign-SaveLoad")
    //        {
    //            popuptext.GetComponent<TextMesh>().text = "Save and Load\nYou can choose to save or\n load your progress through the \n in-game menu";
    //        }

    //        else if (gameObject.name == "Instruction-Sign-GameMech")
    //        {
    //            popuptext.GetComponent<TextMesh>().text = "Game Mechanics\nAcquire Solid Waste Management\n knowledge to solve puzzles!";
    //        }

    //        textstatus = "on";
    //        Instantiate(popuptext, new Vector3(transform.position.x - 4.7f, transform.position.y + 7f, transform.position.z), popuptext.rotation);
    //    }
    //}

    //void OnTriggerExit2D()
    //{
    //    if (textstatus == "on")
    //    {
    //        textstatus = "off";
    //    }
    //}

}
