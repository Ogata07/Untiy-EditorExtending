using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public class TemplateCreatorWindow : EditorWindow
{
    [MenuItem("Tool/GraphEditorWindow")]
    public static void CreateWindow() {
        TemplateCreatorWindow templateCreatorWindow = CreateInstance<TemplateCreatorWindow>();
        templateCreatorWindow.Show();

    }
    private void Intialize() {
        VisualElement visualElement = this.rootVisualElement;
        var text = new Box();
        text.Add(new Label("生成したいスクリプトテンプレート名を入力して下さい"));
        text.Add(new Label("生成したいスクリプトテンプレート名を入力して下さい２"));
        text.Add(new TextField());
        text.Add(new Label("完成予想です"));
        text.Add(new Label("＝＝＝＝＝.cs"));
        visualElement.Add(text);
    }
    private void OnEnable()
    {
        Intialize();
    }
}
