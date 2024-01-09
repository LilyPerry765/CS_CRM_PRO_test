using CRM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Transactions;
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
    /// Interaction logic for AssignCabinetInputToBuchtForm.xaml
    /// </summary>
    public partial class AssignCabinetInputToBuchtForm : Local.PopupWindow
    {
        #region constructor
        private int _cabinetID = 0;
        private byte _typeCabinetInput = 0;
        Cabinet cabinet;

        public AssignCabinetInputToBuchtForm()
        {
            InitializeComponent();
        }

        public AssignCabinetInputToBuchtForm(int cabinetID , byte typeCabinetInput):this()
        {
            this._cabinetID = cabinetID;
            this._typeCabinetInput = typeCabinetInput;
            this.Initialize();
        }
        #endregion

        #region Methode Load
        private void Initialize()
        {

                cabinet = new Cabinet();
                cabinet = Data.CabinetDB.GetCabinetByID(_cabinetID);

               if (_typeCabinetInput == (byte)DB.TypeCabinetInputToBucht.Assign)
                {
                 ToCabinetInputComboBox.ItemsSource= FromCabinetInputComboBox.ItemsSource = Data.CabinetInputDB.GetCabinetInputFreeByCabinetID(this._cabinetID);
            
                 CableComboBox.ItemsSource = Data.CableDB.GetCablesByCenterID(cabinet.CenterID);
                }
                else if (_typeCabinetInput == (byte)DB.TypeCabinetInputToBucht.Leave)
                {
                   ToCabinetInputComboBox.ItemsSource = FromCabinetInputComboBox.ItemsSource = Data.CabinetInputDB.GetCabinetInputConnectedByCabinetID(this._cabinetID);
                   CableGroupBox.Visibility = Visibility.Collapsed;
                   this.Title = "آزاد سازی از کابل";
                }
               else if (_typeCabinetInput == (byte)DB.TypeCabinetInputToBucht.Delete)
               {
                   ToCabinetInputComboBox.ItemsSource = FromCabinetInputComboBox.ItemsSource = Data.CabinetInputDB.GetCabinetInputFreeByCabinetID(this._cabinetID);
                   CableGroupBox.Visibility = Visibility.Collapsed;
                   this.Title = "آزاد سازی از کابل";
               }

          

        }
        private void PopupWindow_Loaded_1(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #region Method save
        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            try
            {

                using (TransactionScope ts = new TransactionScope())
                {

                    if (_typeCabinetInput == (byte)DB.TypeCabinetInputToBucht.Assign)
                    {
                        if (FromCabinetInputComboBox.SelectedValue != null && ToCabinetInputComboBox.SelectedValue != null && FromCablePairComboBox.SelectedValue != null && ToCablePairComboBox.SelectedValue != null)
                        {
                            if (CabinetDB.AssignFreeCabinetInputToFreeCable((long?)FromCabinetInputComboBox.SelectedValue ?? 0, (long?)ToCabinetInputComboBox.SelectedValue ?? 0, (long?)FromCablePairComboBox.SelectedValue ?? 0, (long?)ToCablePairComboBox.SelectedValue ?? 0))
                            {
                                ShowSuccessMessage("ذخیره انجام شد");

                            }
                        }
                        else
                        {
                            MessageBox.Show("لطفا همه فیلد های مورد نیاز را پر کنید");
                        }

                    }
                    else if (_typeCabinetInput == (byte)DB.TypeCabinetInputToBucht.Leave)
                    {
                        if (FromCabinetInputComboBox.SelectedValue != null && ToCabinetInputComboBox.SelectedValue != null)
                        {
                            if (CabinetDB.LeaveCabinetInputFromBucht((long?)FromCabinetInputComboBox.SelectedValue ?? 0, (long?)ToCabinetInputComboBox.SelectedValue ?? 0))
                            {
                                ShowSuccessMessage("آزاد سازی انجام شد");
                            }
                        }
                        else
                        {
                            MessageBox.Show("لطفا همه فیلد های مورد نیاز را پر کنید");
                        }
                    }
                    else if (_typeCabinetInput == (byte)DB.TypeCabinetInputToBucht.Delete)
                    {
                        if (FromCabinetInputComboBox.SelectedValue != null && ToCabinetInputComboBox.SelectedValue != null)
                        {
                            List<CabinetInput> cabinetInputs = Data.CabinetInputDB.GetCabinetInputFromIDToID((long)FromCabinetInputComboBox.SelectedValue, (long)ToCabinetInputComboBox.SelectedValue, true);

                            List<CablePair> cablePair = CablePairDB.GetCablePairByCabinetInputs(cabinetInputs.Select(t => t.ID).ToList());

                            if (cablePair.Count != 0)
                                throw new Exception("امکان حذف مرکزی متصل به کابل نمی باشد");

                            DB.DeleteAll<CabinetInput>(cabinetInputs.Select(t => t.ID).ToList());

                            Cabinet cabinet = CabinetDB.GetCabinetByID(this._cabinetID);
                            cabinet.ToInputNo = CabinetDB.GetCabinetInputCount(this._cabinetID);
                            cabinet.Detach();
                            DB.Save(cabinet);

                        }
                    }

                    ts.Complete();
                }
            }
            catch(Exception ex)
            {
                if (ex.Message.Contains("Cannot insert duplicate key row in object"))
                    ShowErrorMessage("مقادیر وارد شده در پایگاه داده وجود دارد", ex);
                else
                ShowErrorMessage("خطا در ذخیره",ex);
            }
        }
        #endregion

        #region event
        private void CableComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (CableComboBox.SelectedValue != null)
            {
                ToCablePairComboBox.ItemsSource =  FromCablePairComboBox.ItemsSource = Data.CablePairDB.GetCalePairConnectedToBuchtByCableID((long)CableComboBox.SelectedValue);
            }
        }

        private void FromCablePairComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (FromCablePairComboBox.SelectedValue != null)
            {
                FromConnection.Text = Data.CablePairDB.GetConnectionInfoByCablePairID((long)FromCablePairComboBox.SelectedValue);
            }
           

        }

        private void ToCablePairComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (ToCablePairComboBox.SelectedValue != null)
            {
                ToConnection.Text = Data.CablePairDB.GetConnectionInfoByCablePairID((long)ToCablePairComboBox.SelectedValue);
            }

        }
        #endregion




    }
}
