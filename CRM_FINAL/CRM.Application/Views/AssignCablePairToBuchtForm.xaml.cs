using CRM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for AssignCablePairToBuchtForm.xaml
    /// </summary>
    public partial class AssignCablePairToBuchtForm : Local.PopupWindow
    {
        private long _cableID = 0;
        byte _typeCablePairToBucht = 0;
        Cable cable = new Cable();
        public AssignCablePairToBuchtForm()
        {
            InitializeComponent();
        }

        public AssignCablePairToBuchtForm(long cableID, byte typeCablePairToBucht)
            : this()
        {
            this._cableID = cableID;
            this._typeCablePairToBucht = typeCablePairToBucht;
            Initialize();
        }

        private void Initialize()
        {
           cable = Data.CableDB.GetCableByID(_cableID);

           if (_typeCablePairToBucht == (byte)DB.TypeCablePairToBucht.Assign)
           {
               Connection.CenterID = cable.CenterID;
               ToCablePair.ItemsSource = FromCablePair.ItemsSource = Data.CablePairDB.GetFreeCablePairCheckableByCableID(_cableID);
           }
           else if (_typeCablePairToBucht == (byte)DB.TypeCablePairToBucht.Leave)
           {
                 ToCablePair.ItemsSource =  FromCablePair.ItemsSource = Data.CablePairDB.GetConnectCablePairCheckableByCableID(_cableID);
                  GroupBoxBucht.Visibility = Visibility.Collapsed;
                  this.Title = "آزاد سازی از بوخت";
           }
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            try
            {

                if (_typeCablePairToBucht == (byte)DB.TypeCablePairToBucht.Assign)
                {


                    if (CableDB.AssignFreeCablePairToFreeBuchtForm(((long?)FromCablePair.SelectedValue) ?? 0, ((long?)ToCablePair.SelectedValue) ?? 0, (int?)Connection.ConnectionRowComboBox.SelectedValue ?? 0, ((long?)Connection.FromBuchtID) ?? 0, ((long?)Connection.ToBuchtID) ?? 0))
                    {
                        ShowSuccessMessage("ذخیره انجام شد");
                    }

             
                }
                else if (_typeCablePairToBucht == (byte)DB.TypeCablePairToBucht.Leave)
                {

                        if (CableDB.LeaveCablePairFromBuchtForm(((long?)FromCablePair.SelectedValue )?? 0,((long?)ToCablePair.SelectedValue ?? 0)))
                        {
                            ShowSuccessMessage("آزاد سازی انجام شد");
                        }
                }
            }
            catch(Exception ex)
            {
                if (ex.Message.Contains("Cannot insert duplicate key row in object"))
                    ShowErrorMessage("مقادیر وارد شده در پایگاه داده وجود دارد", ex);
                else
                    ShowErrorMessage("خطا در ذخیره بوخت", ex);
            }

        }
    }
}
