using CRM.Application.Local;
using CRM.Data;
using Enterprise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for AgentForm.xaml
    /// </summary>
    public partial class AgentForm : PopupWindow
    {
        #region Properties and Fields

        private int _id;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        #endregion

        #region Constructors

        public AgentForm()
        {
            InitializeComponent();
        }

        public AgentForm(CustomerFormInfo ownerCustomer)
            : this()
        {
            CustomerGroupBox.DataContext = ownerCustomer;
        }

        #endregion

        #region EventHandlers

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            try
            {
                Agent agent = this.DataContext as Agent;
                agent.Detach();
                Save(agent);

                ShowSuccessMessage("نماینده ذخیره شد");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "An error occurred {0}.{1}", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
                ShowErrorMessage("خطا در ذخیره نماینده", ex);
            }
        }

        #endregion

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            Agent agent = new Agent();

            if (this.ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                agent = AgentDB.GetAgentById(this.ID);
                SaveButton.Content = "بروز رسانی";
            }
            this.DataContext = agent;
        }

    }
}
