using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.ServiceModel;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine.UIElements;
/// <summary>
/// �X�N���v�g�e���v���[�g���쐬���Ă����X�N���v�g
/// </summary>
public class TemplateCreatorWindow : EditorWindow
{
    //�g���E�B���h�E��
    public VisualTreeAsset uIDocument = default;
    //�X�N���v�g�e���v���[�g�ptxt�̕ۑ���t�H���_�p�X
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
    //�X�N���v�g�e���v���[�g�쐬���Ɏg���e��Text
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
    /// �X�N���v�g�e���v���[�g��ۑ�����t�H���_���������Ė���������쐬
    /// </summary>
    private void Load() {
        if (!Directory.Exists(folderPath)) { 
            Directory.CreateDirectory(folderPath);
            AssetDatabase.ImportAsset(folderPath);
            UnityEngine.Debug.Log("�t�H���_�����쐬�������̂Ő������܂���(�������@ScriptTemplates)");
            AssetDatabase.Refresh();
        }
    }
    /// <summary>
    /// �g���E�B���h�E�̋@�\�ǉ�
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
    /// �X�N���v�g�e���v���[�g�쐬�Ɏg��Text�̍X�V
    /// </summary>
    private void TextUpdate() {
        numbarText = this.rootVisualElement.Q<TextField>("NumberText").text;
        menuText = this.rootVisualElement.Q<TextField>("MenuText").text;
        defaultText = this.rootVisualElement.Q<TextField>("DefaultText").text;
    }
    /// <summary>
    /// �X�N���v�g�e���v���[�g�쐬
    /// </summary>
    private void ScriptCreate()
    {
        TextUpdate();
        var filepath = "Assets/ScriptTemplates/"+numbarText+"-"+ menuText + "-"+defaultText+".cs.txt";
        UnityEngine.Debug.Log("�X�N���v�g���쐬���܂���(�������@"+numbarText + "-" + menuText + "-" + defaultText + ".cs.txt)");
        var assetPath =AssetDatabase.GenerateUniqueAssetPath(filepath);
        File.WriteAllText(assetPath, defaultCode);
        AssetDatabase.ImportAsset(filepath);
        AssetDatabase.Refresh();

    }
    /// <summary>
    /// Unity��ʂł̍X�V���Ɏ��s����܂�
    /// </summary>
    private void OnEnable()
    {
        if (uIDocument != default)
            uIDocument.CloneTree(rootVisualElement);

    }
}
