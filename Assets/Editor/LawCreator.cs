using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class LawCreator : EditorWindow
{



    private void OnGUI()
    {
        /*Editor editor = Editor.CreateEditor(new LawNode(new Law("Washing Hands", 10, 0.1f)));
        editor.DrawDefaultInspector();*/
    }

    [MenuItem("Plague Prevention/Law Creator")]
    public static void ShowWindow()
    {
        GetWindow<LawCreator>("Law Creator");
    }

}
