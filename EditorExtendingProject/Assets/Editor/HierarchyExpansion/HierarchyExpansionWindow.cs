using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using UnityEditor;
using System.Dynamic;

public class HierarchyExpansionWindow : EditorWindow
{
    public VisualTreeAsset uiDocument = default;
    [MenuItem("Tool/HierarchyExpansion")]
    public static void CreateWindow()
    {
        HierarchyExpansionWindow hierarchyExpansionWindow = CreateInstance<HierarchyExpansionWindow>();
        hierarchyExpansionWindow.Show();




    }
    private void OnEnable()
    {
        if (uiDocument!=default)
        {
            uiDocument.CloneTree(rootVisualElement);
            
        }
    }


}
