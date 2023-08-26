using System.IO;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Debug= UnityEngine.Debug;
/// <summary>
/// スクリプトテンプレートを作成してくれるクラス
/// </summary>
public class TemplateCreatorWindow : EditorWindow
{
    //拡張ウィンドウ
    public VisualTreeAsset uIDocument = default;
    //スクリプトテンプレート用txtの保存先フォルダパス
    private const string FolderPath = "Assets/ScriptTemplates";
    //参考にするスクリプトがない場合に使用するデフォルトString
    private static readonly string defaultCode = 
        @"using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class #SCRIPTNAME# : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }
}
    ";
    //参考にするスクリプトがある場合に使う変数
    private static string referenceCode = null;
    private static string referenceName=null;
    private string templateScriptName = "#SCRIPTNAME#";
    //スクリプトテンプレート作成時に使う各種Text
    private string numbarText = default;
    private string menuText = default;
    private string defaultText = default;

    [MenuItem("Tool/TemplateCreator")]
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
        if (!Directory.Exists(FolderPath)) { 
            Directory.CreateDirectory(FolderPath);
            AssetDatabase.ImportAsset(FolderPath);
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

    }
    /// <summary>
    /// スクリプトテンプレート作成に使うTextの更新
    /// </summary>
    private void TextUpdate() {
        numbarText = this.rootVisualElement.Q<TextField>("NumberText").text;
        menuText = this.rootVisualElement.Q<TextField>("MenuText").text;
        defaultText = this.rootVisualElement.Q<TextField>("DefaultText").text;
        var @object=this.rootVisualElement.Q<ObjectField>("TextScript").value;
        if (@object == null)
        {
            //参考にするスクリプトがない場合はリセットする
            referenceCode = null;
            referenceName = null;
        }
        else {
            //参考にするスクリプトから必要な情報を抜き取る
            var path2 = AssetDatabase.GetAssetPath(@object);
            referenceCode = System.IO.File.ReadAllText(path2);
            referenceName = @object.name.ToString();
        }
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
        //File.WriteAllText(assetPath, defaultCode);
        if (referenceCode != null)
        {
            //参考にするスクリプトからクラス名を変更したのを作成する
            File.WriteAllText(assetPath, ClassNameChange(referenceCode));
        }
        else
        {
            //参考にするスクリプトがない場合はデフォルトを使う
            File.WriteAllText(assetPath, defaultCode);
        }
        AssetDatabase.ImportAsset(filepath);
        AssetDatabase.Refresh();

    }
    /// <summary>
    /// 参考にするスクリプトからクラス名を変更させる関数
    /// </summary>
    /// <param name="changeString">参考にするスクリプトのString文</param>
    /// <returns></returns>
    private string ClassNameChange(string changeString) {
        string returnString=changeString;
        int index = changeString.IndexOf(referenceName);
        Debug.Log(referenceName.Length);
        if (index != -1) {
            returnString = changeString.Substring(0,index)+templateScriptName+changeString.Substring(index+21);
        }
        return returnString;
    }
    /// <summary>
    /// Unityリロード時にウィンドウが残っていたら更新処理を行う
    /// </summary>
    private void OnEnable()
    {
        if (uIDocument != default)
        {
            uIDocument.CloneTree(rootVisualElement);
            this.Set();
        }
    }
}
