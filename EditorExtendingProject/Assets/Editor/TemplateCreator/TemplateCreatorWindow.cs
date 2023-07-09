using System.IO;
using UnityEditor;
using UnityEngine.UIElements;
/// <summary>
/// スクリプトテンプレートを作成してくれるクラス
/// </summary>
public class TemplateCreatorWindow : EditorWindow
{
    //拡張ウィンドウの
    public VisualTreeAsset uIDocument = default;
    //スクリプトテンプレート用txtの保存先フォルダパス
    private const string folderPath = "Assets/ScriptTemplates";
    private static readonly string defaultCode = @"
        using System;
        using System.Collections;
        using System.Collections.Generic;
        using UnityEngine;

        public class #SCRIPTNAME# : MonoBehaviour
        {
            void OnEnable()
            {

            }

            void OnDisable()
            {

            }
        }
    ";
    //スクリプトテンプレート作成時に使う各種Text
    private string numbarText = default;
    private string menuText = default;
    private string defaultText = default;
    [MenuItem("Tool/GraphEditorWindow")]
    public static void CreateWindow() {
        TemplateCreatorWindow templateCreatorWindow = CreateInstance<TemplateCreatorWindow>();
        templateCreatorWindow.Show();
        templateCreatorWindow.Load();
        templateCreatorWindow.Set();
        

    }
    /// <summary>
    /// スクリプトテンプレートを保存するフォルダを検索して無かったら作成
    /// </summary>
    private void Load() {
        if (!Directory.Exists(folderPath)) { 
            Directory.CreateDirectory(folderPath);
            AssetDatabase.ImportAsset(folderPath);
            UnityEngine.Debug.Log("フォルダが未作成だったので生成しました(生成名　ScriptTemplates)");
            AssetDatabase.Refresh();
        }
    }
    /// <summary>
    /// 拡張ウィンドウの機能追加
    /// </summary>
    private void Set() {
        var buton = this.rootVisualElement.Q<Button>("Create");
        buton.clicked += () =>
        {
            ScriptCreate();
        };
        TextUpdate();

    }
    /// <summary>
    /// スクリプトテンプレート作成に使うTextの更新
    /// </summary>
    private void TextUpdate() {
        numbarText = this.rootVisualElement.Q<TextField>("NumberText").text;
        menuText = this.rootVisualElement.Q<TextField>("MenuText").text;
        defaultText = this.rootVisualElement.Q<TextField>("DefaultText").text;
    }
    /// <summary>
    /// スクリプトテンプレート作成
    /// </summary>
    private void ScriptCreate()
    {
        TextUpdate();
        var filepath = "Assets/ScriptTemplates/"+numbarText+"-"+ menuText + "-"+defaultText+".cs.txt";
        UnityEngine.Debug.Log("スクリプトを作成しました(生成名　"+numbarText + "-" + menuText + "-" + defaultText + ".cs.txt)");
        var assetPath =AssetDatabase.GenerateUniqueAssetPath(filepath);
        File.WriteAllText(assetPath, defaultCode);
        AssetDatabase.ImportAsset(filepath);
        AssetDatabase.Refresh();

    }
    private void OnEnable()
    {
        if (uIDocument != default)
            uIDocument.CloneTree(rootVisualElement);

    }
}
