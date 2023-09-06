using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class HieTest 
{
    private const int WIDTH = 16;
    private const int OFFSET= 10;


    //Unityエディタ起動時やコンパイル時によびだし
    [InitializeOnLoadMethod]
    private static void Initalize() {
        //ヒエラルキーで文字が描画されている範囲をコールバックとして取得する
        EditorApplication.hierarchyWindowItemOnGUI += OnGUI;
    }

    private static void OnGUI(int instanceID, Rect selectionRect)
    {
        var go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
        if (go == null)
        {
            return;
        }

        var pos = selectionRect;
        pos.x = pos.xMax - OFFSET;
        pos.width = WIDTH;

        bool active = GUI.Toggle(pos, go.activeSelf, string.Empty);
        if (active == go.activeSelf) {
            return;
        }

        //アクティブ変更時にRecordObjectとSetActiveを行う(シーンの切り替え等でも設定が保存されている)
        Undo.RecordObject(go, $"{(active ? "Activate" : "Deactivate")} GameObject '{go.name}'");
        go.SetActive(active);
        EditorUtility.SetDirty(go);
    }

}
