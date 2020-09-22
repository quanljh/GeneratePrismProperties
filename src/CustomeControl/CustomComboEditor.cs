using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Infragistics.Controls.Editors;
using Infragistics.Controls.Editors.Primitives;

namespace GeneratePrismProperties
{
    /// <summary>
    /// このカスタム コントロールを XAML ファイルで使用するには、手順 1a または 1b の後、手順 2 に従います。
    ///
    /// 手順 1a) 現在のプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:GeneratePrismProperties"
    ///
    ///
    /// 手順 1b) 異なるプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:GeneratePrismProperties;assembly=GeneratePrismProperties"
    ///
    /// また、XAML ファイルのあるプロジェクトからこのプロジェクトへのプロジェクト参照を追加し、
    /// リビルドして、コンパイル エラーを防ぐ必要があります:
    ///
    ///     ソリューション エクスプローラーで対象のプロジェクトを右クリックし、
    ///     [参照の追加] の [プロジェクト] を選択してから、このプロジェクトを参照し、選択します。
    ///
    ///
    /// 手順 2)
    /// コントロールを XAML ファイルで使用します。
    ///
    ///     <MyNamespace:CustomComboEditor/>
    ///
    /// </summary>
    [TemplatePart(Name = "TextBoxPresenter", Type = typeof(SpecializedTextBox))]
    public class CustomComboEditor : XamComboEditor
    {
        #region Private Members

        /// <summary>
        /// Textbox of this ComboEditor
        /// </summary>
        private SpecializedTextBox _specializedTextBox;

        #endregion

        public string InputText
        {
            get => (string)GetValue(InputTextProperty);
            set => SetValue(InputTextProperty, value);
        }

        // Using a DependencyProperty as the backing store for InputText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InputTextProperty =
            DependencyProperty.Register("InputText", typeof(string), typeof(CustomComboEditor), new PropertyMetadata(default(string)));



        static CustomComboEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomComboEditor), new FrameworkPropertyMetadata(typeof(CustomComboEditor)));
        }

        /// <inheritdoc />
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _specializedTextBox = Template.FindName("TextBoxPresenter", this) as SpecializedTextBox;
        }

        public void SelectAllText()
        {
            _specializedTextBox?.Focus();
            _specializedTextBox?.SelectAll();
        }
    }
}
