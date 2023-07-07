using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.ServiceModel;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UIElements;
using UnityEngine.WSA;

public class TemplateCreatorWindow : EditorWindow
{
    public VisualTreeAsset uIDocument = default;
    private const string scriptTemplatesName = "ScriptTemplates";
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
    private string numbarText = default;
    private string menuText = default;
    private string defaultText = default;
    [MenuItem("Tool/GraphEditorWindow")]
    public static void CreateWindow() {
        TemplateCreatorWindow templateCreatorWindow = CreateInstance<TemplateCreatorWindow>();
        templateCreatorWindow.Show();
        //decimal= AssetDatabase.LoadAssetAtPath<Folder>(pathName);
        templateCreatorWindow.Load();
        templateCreatorWindow.Set();
        

    }
    private void Intialize() {
        //VisualElement visualElement = this.rootVisualElement;
        //var text = new Box();
        //text.Add(new Label("生成したいスクリプトテンプレート名を入力して下さい"));
        //text.Add(new Label("生成したいスクリプトテンプレート名を入力して下さい２"));
        //text.Add(new TextField());
        //text.Add(new Label("完成予想です"));
        //text.Add(new Label("＝＝＝＝＝.cs"));
        //visualElement.Add(text);
        if (uIDocument != default)
            uIDocument.CloneTree(rootVisualElement);
    }
    private void Load() {
        if (!Directory.Exists(folderPath)) { 
            Directory.CreateDirectory(folderPath);
            AssetDatabase.ImportAsset(folderPath);
            UnityEngine.Debug.Log("フォルダが未作成だったので生成しました(生成名　ScriptTemplates)");
            AssetDatabase.Refresh();
        }
    }
    private void Set() {
        var buton = this.rootVisualElement.Q<Button>("Create");
        buton.clicked += () =>
        {
            ScriptCreate();
        };
        TextUpdate();

    }
    private void TextUpdate() {
        numbarText = this.rootVisualElement.Q<TextField>("NumberText").text;
        menuText = this.rootVisualElement.Q<TextField>("MenuText").text;
        defaultText = this.rootVisualElement.Q<TextField>("DefaultText").text;
    }
    private void ScriptCreate()
    {
        TextUpdate();
        UnityEngine.Debug.Log("スクリプトを作成しました(生成名　ScriptTemplates)");
        var filepath = "Assets/ScriptTemplates/"+numbarText+"-"+ menuText + "-"+defaultText+".cs.txt";
        var assetPath=AssetDatabase.GenerateUniqueAssetPath(filepath);
        File.WriteAllText(assetPath, defaultCode);
        AssetDatabase.ImportAsset(filepath);
        AssetDatabase.Refresh();

    }
    private void OnEnable()
    {
        Intialize();
    }
}
