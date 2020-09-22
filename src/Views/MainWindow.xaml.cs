using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TypeEditor_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!(sender is CustomComboEditor xamComboEditor))
                return;

            if (!(xamComboEditor.DataContext is PropertyModel model))
                return;

            if (!(DataContext is MainWindowViewModel vm))
                return;

            if (model.PropertyType == xamComboEditor.InputText)
                return;

            model.PropertyType = xamComboEditor.InputText;

            if (!vm.TypeList.Contains(xamComboEditor.InputText))
                vm.TypeList.Add(xamComboEditor.InputText);
        }

        private void DataGrid_OnPreparingCellForEdit(object sender, DataGridPreparingCellForEditEventArgs e)
        {
            if (e.EditingElement.FindVisualChild<TextBox>() is TextBox textBox)
            {
                textBox.Focus();
                textBox.SelectAll();
            }
        }
    }
}
