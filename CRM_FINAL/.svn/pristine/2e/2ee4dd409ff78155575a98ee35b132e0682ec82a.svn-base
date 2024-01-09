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
using System.ComponentModel;
using CRM.Data;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for ChangePreCodeList.xaml
    /// </summary>
    public partial class ChangePreCodeList : Local.TabWindow
    {
        #region Constructor & Fields
        long oldPreCode;
        long newPreCode;
        long fromTelephonNo;
        long toTelephoneNo;
        List<ChangePreCode> ChangePreCodes;
        private BackgroundWorker _worker;
        public ChangePreCodeList()
        {
            InitializeComponent();
            Initialize();

        }

        
        #endregion

        #region Load Methods

        private void Initialize()
        {
            _worker = new BackgroundWorker();
            _worker.DoWork += new DoWorkEventHandler(_worker_DoWork);
            _worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_worker_RunWorkerCompleted);
            OldSwitchColumn.ItemsSource = Data.SwitchDB.GetSwitchCheckable();
            OldPreCodeIDColumn.ItemsSource = Data.SwitchPrecodeDB.GetSwitchPrecodeCheckable();
            NewSwitchColumn.ItemsSource = Data.SwitchDB.GetSwitchCheckable();
         
        }

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Search(null, null);
        }
        #endregion


        #region Worker
        private void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
             ChangePreCodes = Data.ChangePreCodeDB.SearchChangePreCode( oldPreCode, newPreCode, fromTelephonNo, toTelephoneNo);
        }

        private void _worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ItemsDataGrid.ItemsSource = ChangePreCodes;
        }
        #endregion

        #region events
        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                ChangePreCode item =  ItemsDataGrid.SelectedItem as ChangePreCode;
               
                try
                {

                    if (item != null)
                    {
                        Local.PopupWindow window = Local.FormSelector.Select(new List<long>{ item.ID});
                        //window.currentStep = requestInfo.StepID;
                        // window.currentStat = requestInfo.StatusID;

                        window.ShowDialog();

                        //    if (window.DialogResult == true)
                        // LoadData();
                    }
                    else
                    {
                        ChangePreCodeForm window = new ChangePreCodeForm(0);
                        window.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    //FooterStatusBar.Visibility = Visibility.Visible;
                  //  FooterStatusLine.Visibility = Visibility.Collapsed;
                    ShowErrorMessage(ex.Message, ex);
                }
            }
        }

        
        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {

            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);
           
        }

        private void Search(object sender, RoutedEventArgs e)
        {
              
              if(!long.TryParse(OldPreCodeTextBox.Text , out oldPreCode))
              {
                 oldPreCode = -1;
              }

              
              if(!long.TryParse(NewPreCodeTextBox.Text , out newPreCode))
              {
                newPreCode = -1;
              }
       
 
              if (!long.TryParse(FromTelephonNoTextBox.Text, out fromTelephonNo))
              {
                fromTelephonNo = -1;
              }

             
              if (!long.TryParse(ToTelephoneNoTextBox.Text, out toTelephoneNo))
              {
                  toTelephoneNo = -1;
              }


            if (!_worker.IsBusy)
                _worker.RunWorkerAsync();
        }
        #endregion Events

        private void NewClick(object sender, RoutedEventArgs e)
        {

            ChangePreCodeForm window = new ChangePreCodeForm((int)Data.DB.RequestType.ChangePreCode);
            window.ShowDialog();
            Search(null, null);
        }

      
    }
}
