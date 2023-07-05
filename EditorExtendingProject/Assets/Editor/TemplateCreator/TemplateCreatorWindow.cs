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
        text.Add(new Label("�����������X�N���v�g�e���v���[�g������͂��ĉ�����"));
        text.Add(new Label("�����������X�N���v�g�e���v���[�g������͂��ĉ������Q"));
        text.Add(new TextField());
        text.Add(new Label("�����\�z�ł�"));
        text.Add(new Label("����������.cs"));
        visualElement.Add(text);
    }
    private void OnEnable()
    {
        Intialize();
    }
}
