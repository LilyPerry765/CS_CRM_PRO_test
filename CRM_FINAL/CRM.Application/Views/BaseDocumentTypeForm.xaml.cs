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
using CRM.Data;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for BaseDocumentTypeForm.xaml
    /// </summary>
    public partial class BaseDocumentTypeForm : Local.PopupWindow
    {
      
        public int _id;

        public DocumentType docType { get; set; }

        public BaseDocumentTypeForm()
        {
            InitializeComponent();
          
            Initialize();
        }

        private void Initialize()
        {
            docType = new DocumentType();       
         
            this.DataContext = docType;
        }

        private void Savebtn_Click(object sender, RoutedEventArgs e)
        {
        
            DB.Save(docType);
            _id = 0;
            Load();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void Load()
        {
            if (_id == 0)
            {
                Initialize();
                Title = "ثبت انواع مدارک پایه";
            }
            else
            {
                docType = DB.GetEntitybyID<DocumentType>(_id);
                this.DataContext = docType;
            }
        }
    }
}
