using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class HieTest 
{
    //表示が重なっている場合をどうにかしたい

    //Unityエディタ起動時やコンパイル時によびだし
    [InitializeOnLoadMethod]
    private static void Initalize() {
        //ヒエラルキーで文字が描画されている範囲をコールバックとして取得する
        EditorApplication.hierarchyWindowItemOnGUI += OnTag;
    }
    /// <summary>
    /// 横にオブジェクトのタグを表示する
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
        //標示する内容の場所
        Debug.Log(selectionRect.xMax);//現在の枠のX大きさ
        selectionRect.x = selectionRect.xMax - 100;
        //大きさに合わせて表示させたい
        //高さ
        //selectionRect.width = 1;
        GUI.Label(selectionRect, tagValue);

    }

}
