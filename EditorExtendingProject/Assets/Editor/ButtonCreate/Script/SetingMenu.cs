using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Toolbar = UnityEditor.UIElements.Toolbar;
using System.Linq;
/// <summary>
///　拡張ウィンドウの設定クラス
/// </summary>
public class SetingMenu : EditorWindow
{
    public static BookmarkSetting bookmarkSetting;
    private ObjectField[] objectFields = new ObjectField[4];
    private string path = "Assets/Editor/ButtonCreate/bookmarkSetting.asset";
    /// <summary>
    /// 拡張ウィンドウの表示
    /// </summary>
    public void CreateMenu()
    {
        SetingMenu graphEditorWindow = CreateInstance<SetingMenu>();
        graphEditorWindow.Show();
    }
    private void OnEnable()
    {
        Intialize();
        Load();
        SetValue();
    }
    /// <summary>
    /// 拡張ウィンドウの要素設定
    /// </summary>
    private void Intialize() {
        VisualElement visualElement = this.rootVisualElement;
        Toolbar toolbar = new();
        visualElement.Add(toolbar);
        Box text = new();
        text.Add(new Label("生成１～３に生成させたいObjectを入れてください"));
        visualElement.Add(text);
        visualElement.Add(new Label("保存先"));
        //ObjectFieldの設定
        for (int count = 0; count < objectFields.Count(); count++)
        {
            objectFields[count] = new ObjectField();
            objectFields[count].objectType = typeof(GameObject);
            if (count == 0)
            {
                objectFields[count].objectType = typeof(BookmarkSetting);
            }
        }
        //For文で回すとIndexOverになる
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
        visualElement.Add(new Label("生成1"));
        visualElement.Add(objectFields[1]);

        visualElement.Add(new Label("生成2"));
        visualElement.Add(objectFields[2]);

        visualElement.Add(new Label("生成3"));
        visualElement.Add(objectFields[3]);


    }
    /// <summary>
    /// 生成するオブジェクトを保存する
    /// </summary>
    private void Load() {
        bookmarkSetting = AssetDatabase.LoadAssetAtPath<BookmarkSetting>(path);
        if (bookmarkSetting == null)
        { // ロードしてnullだったら存在しないので生成
            bookmarkSetting = ScriptableObject.CreateInstance<BookmarkSetting>(); 
            AssetDatabase.CreateAsset(bookmarkSetting, path);
        }


    }
    /// <summary>
    /// 保存先の値を挿入する
    /// </summary>
    private void SetValue() {
        //値が挿入されていたら更新
        if (bookmarkSetting != null)
        {
            objectFields[0].value = bookmarkSetting;
            for (int count = 0; count < bookmarkSetting.bookmarks.Count(); count++)
            {
                objectFields[count + 1].value = bookmarkSetting.bookmarks[count].saveObject;

            }
        }
    }
    /// <summary>
    /// セットされたオブジェクトを保存先と変更する
    /// </summary>
    /// <param name="object">設定したいオブジェクト</param>
    /// <param name="count">配列の順番</param>
    private void OnChange(UnityEngine.Object @object,int count)
    {
        if (@object is GameObject gameObject) {
            bookmarkSetting.bookmarks[count-1].saveObject = gameObject;
            EditorUtility.SetDirty(bookmarkSetting); 
        }

    }
    /// <summary>
    /// 配列に保存してあるオブジェクトをInstantiateする
    /// </summary>
    /// <param name="number">配列の順番</param>
    public void Create(int number)
    {
        Load();
        GameObject createObject = bookmarkSetting.bookmarks[number-1].saveObject;
        Instantiate(createObject);
    }
}
