using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for FormulaEditorForm.xaml
    /// </summary>
    public partial class FormulaEditorForm : Local.PopupWindow
    {
        public string Formula { get; set; }
        private string PastFormula { get; set; }
        public bool Center { get; set; }
        public bool UseOutBound { get; set; }
        public bool UseZeroBlock { get; set; }
        public bool UseCableMeter { get; set; }
        bool isSave = false;
        public FormulaEditorForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {

        }
        public FormulaEditorForm(string formula)
            : this()
        {
            Formula = formula;
            PastFormula = formula;
        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this;
        }

        private void ResultButton_Click(object sender, RoutedEventArgs e)
        {
            AddTextToFormula("\nresult = ;\n");
        }

        private void OutBoundButton_Click(object sender, RoutedEventArgs e)
        {
            AddTextToFormula(" outBoundMeter ");
        }
         private void CableMeterButton_Click(object sender, RoutedEventArgs e)
        {
            AddTextToFormula(" CableMeter ");
        }

        private void ZeroBlockButton_Click(object sender, RoutedEventArgs e)
        {
            AddTextToFormula(" ZeroBlock ");
        }

        private void FirstZeroBlockButton_Click(object sender, RoutedEventArgs e)
        {
            AddTextToFormula(" FirstZero ");
        }

        private void SecondZeroBlockButton_Click(object sender, RoutedEventArgs e)
        {
            AddTextToFormula(" SecondZero ");
        }

        private void LimitLessButton_Click(object sender, RoutedEventArgs e)
        {
            AddTextToFormula(" LimitLess ");
        }


        private void DebugButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                // متن فرمول را کامپایل می کند
                System.CodeDom.Compiler.CompilerErrorCollection compilerErrorCollection = Data.Calculate.CompiledFormula(Formula);
                if (compilerErrorCollection.Count > 0)
                {
                    Folder.MessageBox.ShowError("فرمول نوشته شده شامل " + compilerErrorCollection.Count.ToString() + " خطا می باشد.");
                }
                else
                {
                    Folder.MessageBox.ShowInfo("فرمول بدون خطا می باشد.");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در اشکال زدایی", ex);
            }

        }

        private void IfButton_Click(object sender, RoutedEventArgs e)
        {
            AddTextToFormula(" if ()\n{\nresult = ;\n}\nelse\n{\nresult = ;\n}\n ");

        }

        //private void RoundButton_Click(object sender, RoutedEventArgs e)
        //{
        //    AddTextToFormula(" Round( , ); ");
        //}
        private void AddTextToFormula(string content)
        {
            FormulaTextBox.SelectedText = content;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Formula == null) return;

                if (Formula.Contains("outBoundMeter")) UseOutBound   = true;
                if (Formula.Contains("CableMeter"))    UseCableMeter = true;
                if (Formula.Contains("ZeroBlock"))     UseZeroBlock  = true;

                System.CodeDom.Compiler.CompilerErrorCollection compilerErrorCollection = Data.Calculate.CompiledFormula(Formula);
                if (compilerErrorCollection.Count > 0)
                {
                    Folder.MessageBox.ShowError("فرمول نوشته شده شامل " + compilerErrorCollection.Count.ToString() + " خطا می باشد.");
                }
                else
                {
                    if (isSave == false)
                    {
                        isSave = true;
                        this.Close();
                    }


                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در اعمال تغییرات", ex);
            }


        }

        private void PopupWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (isSave == false)
            {
                MessageBoxResult result = Folder.MessageBox.Show("آیا می خواهید تغییرات ذخیره شود؟", "پرسش", MessageBoxImage.Question, MessageBoxButton.YesNoCancel);
                switch (result)
                {
                    case MessageBoxResult.No:
                        Formula = PastFormula;
                        FormulaTextBox.Text = PastFormula;
                        break;
                    case MessageBoxResult.Cancel:
                        e.Cancel = true;
                        break;
                    case MessageBoxResult.Yes:
                        isSave = true;
                        SaveButton_Click(null, null);
                        break;
                    default:
                        break;
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Formula = null;
            FormulaTextBox.Text = string.Empty;
        }













    }
}
