using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using OpenCover.Framework.Model;
using UnityEngine.Windows;
using System.IO;
using UnityEngine.UI;
using UnityEditor;

namespace ExampleElements
{
    public class ObjectElement : VisualElement
    {
        //ファクトリとして使われるクラス(UIToolKitで使うため)
        public new class UxmlFactory : UxmlFactory<ObjectElement> { }
        public readonly Label label;
        public readonly UnityEditor.UIElements.ObjectField objectField;


        public ObjectElement() {
            // レイアウトを調整（横並び&上下中央に）
            style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row);
            style.alignItems = new StyleEnum<Align>(Align.Center);

            label = new Label();
            SetMargin(label, 0);
            SetPadding(label, 0);
            label.style.width = new StyleLength(new Length(50, LengthUnit.Percent));
            label.text="参考にするスクリプト";
            Add(label);

            objectField = new ObjectField();
            objectField.name = "TextScript";
            objectField.objectType = typeof(UnityEngine.Object );
            SetMargin(objectField, 0);
            SetPadding(objectField, 0);
            objectField.style.width = new StyleLength(new Length(50, LengthUnit.Percent));
            Add(objectField);

        }
        private static void SetMargin(VisualElement element, float px)
        {
            element.style.marginLeft = px;
            element.style.marginTop = px;
            element.style.marginRight = px;
            element.style.marginBottom = px;
        }

        private static void SetPadding(VisualElement element, float px)
        {
            element.style.paddingLeft = px;
            element.style.paddingTop = px;
            element.style.paddingRight = px;
            element.style.paddingBottom = px;
        }
    }
}
