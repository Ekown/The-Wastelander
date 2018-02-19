using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Panel", menuName = "Panel")]
public class Panel : ScriptableObject
{
    public string panelName;

    [TextArea]
    public string panelDescription;
}
