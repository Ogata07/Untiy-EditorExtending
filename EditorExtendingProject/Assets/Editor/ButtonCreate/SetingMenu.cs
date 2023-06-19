using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Toolbar = UnityEditor.UIElements.Toolbar;
using UnityEditor.PackageManager;
using PlasticGui.WorkspaceWindow.PendingChanges;
using System.Linq;
using System;
using Object = UnityEngine.Object;
using System.IO;

public class SetingMenu : EditorWindow
{
    private BookmarkBar BookmarkBar { get; set; }
    public Object @object2;
    public static BookmarkSetting bookmarkSetting;
    private ObjectField[] objectFields = new ObjectField[4];
    private void OnEnable()
    {

    }
    public void CreateMenu()
    {//BookmarkBar bookmarkBar
        SetingMenu graphEditorWindow = CreateInstance<SetingMenu>();
        graphEditorWindow.Show();
        graphEditorWindow.Intialize();
        graphEditorWindow.Load();
        graphEditorWindow.SetValue();
        //BookmarkBar=bookmarkBar;
    }
    private void Intialize() {
        VisualElement visualElement = this.rootVisualElement;
        var toolbar = new Toolbar();
        visualElement.Add(toolbar);
        var text = new Box();
        text.Add(new Label("�����P�`�R�ɐ�����������Object�����Ă�������"));
        visualElement.Add(text);
        visualElement.Add(new Label("�ۑ���"));
        //ObjectField�̐ݒ�
        for (int count = 0; count < objectFields.Count(); count++)
        {
            objectFields[count] = new ObjectField();
            objectFields[count].objectType = typeof(GameObject);
            if (count == 0)
            {
                objectFields[count].objectType = typeof(BookmarkSetting);
            }
        }
        //For���ŉ񂷂�IndexOver�ɂȂ�
        objectFields[1].RegisterCallback<ChangeEvent<string>>(events =>
        {
            OnChange(objectFields[1].value,1);
        }); objectFields[2].RegisterCallback<ChangeEvent<string>>(events =>
        {
            OnChange(objectFields[2].value,2);
        }); objectFields[3].RegisterCallback<ChangeEvent<string>>(events =>
        {
            OnChange(objectFields[3].value,3);
        });


        visualElement.Add(objectFields[0]);
        visualElement.Add(new Label("����1"));
        visualElement.Add(objectFields[1]);

        visualElement.Add(new Label("����2"));
        visualElement.Add(objectFields[2]);

        visualElement.Add(new Label("����3"));
        visualElement.Add(objectFields[3]);


    }
    private void Load() {
        var path = "Assets/bookmarkSetting.asset";
        bookmarkSetting = AssetDatabase.LoadAssetAtPath<BookmarkSetting>(path);
        if (bookmarkSetting == null)
        { // ���[�h����null�������瑶�݂��Ȃ��̂Ő���
            bookmarkSetting = ScriptableObject.CreateInstance<BookmarkSetting>(); 
            AssetDatabase.CreateAsset(bookmarkSetting, path);
        }


    }
    private void SetValue() {
        //�l���}������Ă�����X�V
        if (bookmarkSetting != null)
        {
            objectFields[0].value = bookmarkSetting;
            for (int count = 0; count < bookmarkSetting.bookmarks.Count(); count++)
            {
                objectFields[count + 1].value = bookmarkSetting.bookmarks[count].GameObject;

            }
        }
    }
    private void OnChange(UnityEngine.Object @object,int count)
    {
        if (@object is GameObject gameObject) {
            bookmarkSetting.bookmarks[count-1].GameObject = gameObject;
            EditorUtility.SetDirty(bookmarkSetting); 
        }

    }
    public void pase(int Number)
    {
        Load();
        GameObject createObject = bookmarkSetting.bookmarks[Number-1].GameObject;
        Instantiate(createObject);
    }
}
