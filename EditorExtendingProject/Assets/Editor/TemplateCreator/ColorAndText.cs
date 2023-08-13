using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace ExampleElements // 名前空間は定義しておいた方がいい
{
    public class ColorAndText : VisualElement // VisualElementを継承
    {
        
        // ファクトリとして使われるクラス
        public new class UxmlFactory : UxmlFactory<ColorAndText> { }
        public Color Color
        {
            get => _colorField.value;
            set
            {
                _colorField.value = value;
                _label.text = _colorField.value.ToString();
            }
        }

        private readonly Label _label;
        private readonly ColorField _colorField;

        public ColorAndText()
        {
            // レイアウトを調整（横並び&上下中央に）
            style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row);
            style.alignItems = new StyleEnum<Align>(Align.Center);

            // ColorFieldを子要素として追加
            _colorField = new ColorField();
            SetMargin(_colorField, 0); // 子要素のMarginはUSSで変更されないようにスクリプトで定義
            SetPadding(_colorField, 0); // 子要素のPaddingはUSSで変更されないようにスクリプトで定義
            _colorField.style.width = new StyleLength(new Length(50, LengthUnit.Percent));
            Add(_colorField);

            // Labelを子要素として追加
            _label = new Label();
            SetMargin(_label, 0);
            SetPadding(_label, 0);
            _label.style.width = new StyleLength(new Length(50, LengthUnit.Percent));
            _label.text = _colorField.value.ToString();
            Add(_label);

            _colorField.RegisterValueChangedCallback(x => _label.text = x.newValue.ToString());
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

