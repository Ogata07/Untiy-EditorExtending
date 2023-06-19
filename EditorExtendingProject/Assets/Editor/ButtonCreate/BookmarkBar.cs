using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Toolbar = UnityEditor.Toolbar;

public  class BookmarkBar 
{
    public BookmarkSetting BookmarkSetting { get; set; }
    private static ToolbarButton[] toolbarButtonArray=new ToolbarButton[3];
    [InitializeOnLoadMethod]
    private static void InitializeOnLoad()
    {
        //�X�V���ɒǉ��̋@�\�����Ă��炤
        EditorApplication.update += OnUpdate;
    }
    private static void OnUpdate()
    {
        SetingMenu setingMenu = new SetingMenu();


        var toolbar = Toolbar.get;
        if (toolbar.windowBackend?.visualTree is not VisualElement visualTree) return; // �c�[���o�[��VisualTree���擾
        if (visualTree.Q("ToolbarZoneLeftAlign") is not { } leftZone) return; // �c�[���o�[�����̃]�[�����擾
        EditorApplication.update -= OnUpdate; // �`��͈��݂̂ł悢 

        // VisualElement�̒ǉ�
        //�����{�^��
        for (int count = 0; count < toolbarButtonArray.Count(); count++) {
            toolbarButtonArray[count] = new ToolbarButton();
            
            toolbarButtonArray[count].Add(new Label("����" + (count + 1).ToString()) {
                style = {
                fontSize = 10,
                unityFontStyleAndWeight= new StyleEnum<FontStyle>(FontStyle.Bold)
                }
            });
            toolbarButtonArray[count].Add(new Image()
            {
                image = EditorGUIUtility.IconContent("CreateAddNew").image
            });
            leftZone.Add(toolbarButtonArray[count]);
        }
        toolbarButtonArray[0].clicked += () => setingMenu.pase(1);
        toolbarButtonArray[1].clicked += () => setingMenu.pase(2);
        toolbarButtonArray[2].clicked += () => setingMenu.pase(3);
        //�ݒ�{�^��

        var sampleButton = new ToolbarButton()
        {
            // �摜�ɂ���ă{�^�����傫���Ȃ肷���Ă��܂�Ȃ��悤�ɕ��𐧌�
            style = { width = 20 } 
        };
        sampleButton.clicked += () => setingMenu.CreateMenu();
        sampleButton.Add(new Image()
        {
            image = EditorGUIUtility.IconContent("_Popup").image 
        });
        leftZone.Add(sampleButton);

    }
    private static void CreateCube() {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        
    }
}
