using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class HieTest 
{
    static readonly Vector2 offset = new Vector2(18f, 0);

    // どんなフォントカラーに変更するか
    static readonly Color separatorColor = new Color(1f, 0f, 0f);
    //表示が重なっている場合をどうにかしたい

    //Unityエディタ起動時やコンパイル時によびだし
    [InitializeOnLoadMethod]
    private static void Initalize() {
        //ヒエラルキーで文字が描画されている範囲をコールバックとして取得する
        
        //EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
        EditorApplication.hierarchyWindowItemOnGUI += OnTag;
    }
    /// <summary>
    /// 横にオブジェクトのタグ名を表示する
    /// </summary>
    /// <param name="instanceID">対象のオブジェクトのID</param>
    /// <param name="selectionRect">ヒエラルキー内での対象のオブジェクトの場所</param>
    private static void OnTag(int instanceID, Rect selectionRect)
    {
        // instanceID をオブジェクト参照に変換
        var go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
        //変換できない場合はReturn
        if (go == null)
        {
            return;
        }

        // タグを取得
        string tagValue = go.tag as string;
        string layerValue = LayerMask.LayerToName( go.layer);
        string displayText = tagValue + " : " + layerValue;
        //表示する内容の場所
        //Debug.Log(selectionRect.position);//現在の枠のX大きさ
        Debug.Log(selectionRect.xMax);

        selectionRect = new Rect(selectionRect.position, selectionRect.size);
        selectionRect.x = selectionRect.xMax-(100);//selectionRect.xMax - 100;//tagValue.Length*5
        //大きさに合わせて表示させたい
        //高さ
        //selectionRect.width = 1;
        GUI.Label(selectionRect, displayText);

    }

    static void HierarchyWindowItemOnGUI(int instanceId, Rect selectionRect)
    {
        var gameObject = EditorUtility.InstanceIDToObject(instanceId) as GameObject;

        if (gameObject == null)
        {
            return;
        }

        // フォントカラーを変更したいコンポーネント
        var target = gameObject.GetComponent<Camera>();

        if (target == null)
        {
            return;
        }

        var offsetRect = new Rect(selectionRect.position, selectionRect.size);

        EditorGUI.LabelField(offsetRect, gameObject.name, new GUIStyle
        {
            normal = new GUIStyleState
            {
                textColor = separatorColor
            }
        });
    }
    static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        GameObject obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

        if (obj != null)
        {
            GUIStyle style = new GUIStyle(EditorStyles.label);
            float fontSize = style.fontSize;

            Rect labelRect = new Rect(selectionRect.x + selectionRect.width, selectionRect.y, 100f, selectionRect.height);

            EditorGUI.LabelField(labelRect, "Font Size: " + fontSize);
        }
    }
    static bool DuplicatDecision(int instanceID, Rect selectionRect) {
        return false;
    }
}
