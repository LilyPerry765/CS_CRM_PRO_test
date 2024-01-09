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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CRM.CodeGenerator.Data;
using System.IO;
using Microsoft.Win32;

namespace CRM.CodeGenerator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void FetchDatabaseSchema()
        {
            if (!string.IsNullOrWhiteSpace(ConnectionStringTextBox.Text) && ConnectionManager.TestConnection(ConnectionStringTextBox.Text))
            {
                DataBase.ConnectionString = ConnectionStringTextBox.Text;
                TablesComboBox.ItemsSource = DataBase.Tables;

                RegistryKey masterKey = Registry.LocalMachine.CreateSubKey("SOFTWARE\\WPFCodeGenerator\\Preferences");
                try
                {
                    masterKey.SetValue("ConnectionString", ConnectionStringTextBox.Text);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    masterKey.Close();
                }
            }
        }

        private void ConnectionStringTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FetchDatabaseSchema();
        }

        private void GenerateCode(object sender, RoutedEventArgs e)
        {
            Data.Table table = TablesComboBox.SelectedItem as Data.Table;

            ListXaml.Text = CodeGenerators.GenerateListXamlCode(table);
            ListCs.Text = CodeGenerators.GenerateListCsCode(table);
            DBCs.Text = CodeGenerators.GenerateDBCsCode(table);
            FormXaml.Text = CodeGenerators.GenerateFormXamlCode(table);
            FormCs.Text = CodeGenerators.GenerateFormCsCode(table);
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RegistryKey masterKey = Registry.LocalMachine.CreateSubKey("SOFTWARE\\WPFCodeGenerator\\Preferences");
            try
            {
                if (masterKey.GetValue("ConnectionString") == null) return;

                ConnectionStringTextBox.Text = masterKey.GetValue("ConnectionString").ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                masterKey.Close();
            }
        }
    }
}
