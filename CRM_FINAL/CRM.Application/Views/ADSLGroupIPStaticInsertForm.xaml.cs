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
    public partial class ADSLGroupIPStaticInsertForm : Local.PopupWindow
    { 
        #region Properties

        private long _ID = 0;

        #endregion

        #region Constructors

        public ADSLGroupIPStaticInsertForm()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            IPTypeComboBox.ItemsSource = DB.GetAllEntity<ADSLIPType>();
            CustomerGroupComboBox.ItemsSource = ADSLCustomerGroupDB.GetADSLCustomerGroupCheckable();
            StatusComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ADSLIPStatus));
        }

        private void LoadData()
        {
            ADSLGroupIP aDSLGroupIP = new ADSLGroupIP();

            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                aDSLGroupIP = Data.ADSLIPDB.GetADSLGroupIPById(_ID);
                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = aDSLGroupIP;
        }

        private void GenerateGroupIP(long fromIP0, long fromIP1, long fromIP2, long fromIP3, long endIP0, long endIP1, long endIP2, long endIP3, int count2, int count4, int count8, int count16, int count32, int count64)
        {
        }

        #endregion

        #region Event Handlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            try
            {
                ADSLGroupIP aDSLGroupIP = this.DataContext as ADSLGroupIP;

                int fromIP0 = 0;
                int endIP0 = 0;

                int fromIP1 = 0;
                int endIP1 = 0;

                int fromIP2 = 0;
                int endIP2 = 0;

                int fromIP3 = 0;
                int endIP3 = 0;

                int length2 = 0;
                int length4 = 0;
                int length8 = 0;
                int length16 = 0;
                int length32 = 0;
                int length64 = 0;


                if (!string.IsNullOrEmpty(FromIPTextBox.Text))
                {
                    fromIP0 = Convert.ToInt32(FromIPTextBox.Text.Split('.')[0]);
                    fromIP1 = Convert.ToInt32(FromIPTextBox.Text.Split('.')[1]);
                    fromIP2 = Convert.ToInt32(FromIPTextBox.Text.Split('.')[2]);
                    fromIP3 = Convert.ToInt32(FromIPTextBox.Text.Split('.')[3]);

                    if (!string.IsNullOrEmpty(ToIPTextBox.Text))
                    {
                        endIP0 = Convert.ToInt32(ToIPTextBox.Text.Split('.')[0]);
                        endIP1 = Convert.ToInt32(ToIPTextBox.Text.Split('.')[1]);
                        endIP2 = Convert.ToInt32(ToIPTextBox.Text.Split('.')[2]);
                        endIP3 = Convert.ToInt32(ToIPTextBox.Text.Split('.')[3]);

                        if (!string.IsNullOrWhiteSpace(Block2TextBox.Text))
                            length2 = Convert.ToInt32(Block2TextBox.Text);

                        if (!string.IsNullOrWhiteSpace(Block4TextBox.Text))
                            length4 = Convert.ToInt32(Block4TextBox.Text);

                        if (!string.IsNullOrWhiteSpace(Block8TextBox.Text))
                            length8 = Convert.ToInt32(Block8TextBox.Text);

                        if (!string.IsNullOrWhiteSpace(Block16TextBox.Text))
                            length16 = Convert.ToInt32(Block16TextBox.Text);

                        if (!string.IsNullOrWhiteSpace(Block32TextBox.Text))
                            length32 = Convert.ToInt32(Block32TextBox.Text);

                        if (!string.IsNullOrWhiteSpace(Block64TextBox.Text))
                            length64 = Convert.ToInt32(Block64TextBox.Text);

                        string virtualRange = VirtualRangeTextBox.Text;

                        int i1 = 0;
                        for (int i = fromIP0; i <= endIP0; i++)
                        {
                            i1 = i1 + 1;

                            if (i != endIP0)
                            {
                                if (i1 != 1)
                                {
                                    fromIP1 = 1;
                                    fromIP0 = fromIP0 + 1;
                                }
                                endIP1 = 255;
                            }
                            else
                            {
                                if (i1 != 1)
                                {
                                    fromIP1 = 1;
                                    fromIP0 = fromIP0 + 1;
                                }
                                endIP1 = Convert.ToInt32(ToIPTextBox.Text.Split('.')[1]);
                            }

                            int j1 = 0;
                            for (int j = fromIP1; j <= endIP1; j++)
                            {
                                j1 = j1 + 1;

                                if (j != endIP1)
                                {
                                    if (j1 != 1)
                                    {
                                        fromIP2 = 1;
                                        fromIP1 = fromIP1 + 1;
                                    }
                                    endIP2 = 255;
                                }
                                else
                                {
                                    if (j1 != 1)
                                    {
                                        fromIP2 = 1;
                                        fromIP1 = fromIP1 + 1;
                                    }
                                    endIP2 = Convert.ToInt32(ToIPTextBox.Text.Split('.')[2]);
                                }

                                int k1 = 0;

                                for (int k = fromIP2; k <= endIP2; k++)
                                {
                                    k1 = k1 + 1;

                                    if (k != endIP2)
                                    {
                                        if (k1 != 1)
                                        {
                                            fromIP3 = 1;
                                            fromIP2 = fromIP2 + 1;
                                        }
                                        endIP3 = 254;
                                    }
                                    else
                                    {
                                        if (k1 != 1)
                                        {
                                            fromIP3 = 1;
                                            fromIP2 = fromIP2 + 1;
                                        }
                                        endIP3 = (Convert.ToInt32(ToIPTextBox.Text.Split('.')[3]) == 255) ? 254 : Convert.ToInt32(ToIPTextBox.Text.Split('.')[3]);
                                    }

                                    while (length2 > 0 && fromIP3 + 1 <= endIP3)
                                    {
                                        aDSLGroupIP.ID = 0;
                                        aDSLGroupIP.VirtualRange = virtualRange;
                                        aDSLGroupIP.StartRange = fromIP0 + "." + fromIP1 + "." + fromIP2 + "." + fromIP3.ToString() + "/31";
                                        aDSLGroupIP.BlockCount = 2;

                                        aDSLGroupIP.Detach();
                                        Save(aDSLGroupIP);

                                        fromIP3 = fromIP3 + 2;
                                        virtualRange = virtualRange.Split('.')[0] + "." + virtualRange.Split('.')[1] + "." + virtualRange.Split('.')[2] + "." + (Convert.ToInt32(virtualRange.Split('.')[3]) + 1).ToString();
                                        length2 = length2 - 1;
                                    }

                                    while (length4 > 0 && fromIP3 + 3 <= endIP3)
                                    {
                                        aDSLGroupIP.ID = 0;
                                        aDSLGroupIP.VirtualRange = virtualRange;
                                        aDSLGroupIP.StartRange = fromIP0 + "." + fromIP1 + "." + fromIP2 + "." + fromIP3.ToString() + "/30";
                                        aDSLGroupIP.BlockCount = 4;

                                        aDSLGroupIP.Detach();
                                        Save(aDSLGroupIP);

                                        fromIP3 = fromIP3 + 4;
                                        virtualRange = virtualRange.Split('.')[0] + "." + virtualRange.Split('.')[1] + "." + virtualRange.Split('.')[2] + "." + (Convert.ToInt32(virtualRange.Split('.')[3]) + 1).ToString();
                                        length4 = length4 - 1;
                                    }

                                    while (length8 > 0 && fromIP3 + 7 <= endIP3)
                                    {
                                        aDSLGroupIP.ID = 0;
                                        aDSLGroupIP.VirtualRange = virtualRange;
                                        aDSLGroupIP.StartRange = fromIP0 + "." + fromIP1 + "." + fromIP2 + "." + fromIP3.ToString() + "/29";
                                        aDSLGroupIP.BlockCount = 8;

                                        aDSLGroupIP.Detach();
                                        Save(aDSLGroupIP);

                                        fromIP3 = fromIP3 + 8;
                                        virtualRange = virtualRange.Split('.')[0] + "." + virtualRange.Split('.')[1] + "." + virtualRange.Split('.')[2] + "." + (Convert.ToInt32(virtualRange.Split('.')[3]) + 1).ToString();
                                        length8 = length8 - 1;
                                    }

                                    while (length16 > 0 && fromIP3 + 15 <= endIP3)
                                    {
                                        aDSLGroupIP.ID = 0;
                                        aDSLGroupIP.VirtualRange = virtualRange;
                                        aDSLGroupIP.StartRange = fromIP0 + "." + fromIP1 + "." + fromIP2 + "." + fromIP3.ToString() + "/28";
                                        aDSLGroupIP.BlockCount = 16;

                                        aDSLGroupIP.Detach();
                                        Save(aDSLGroupIP);

                                        fromIP3 = fromIP3 + 16;
                                        virtualRange = virtualRange.Split('.')[0] + "." + virtualRange.Split('.')[1] + "." + virtualRange.Split('.')[2] + "." + (Convert.ToInt32(virtualRange.Split('.')[3]) + 1).ToString();
                                        length16 = length16 - 1;
                                    }

                                    while (length32 > 0 && fromIP3 + 31 <= endIP3)
                                    {
                                        aDSLGroupIP.ID = 0;
                                        aDSLGroupIP.VirtualRange = virtualRange;
                                        aDSLGroupIP.StartRange = fromIP0 + "." + fromIP1 + "." + fromIP2 + "." + fromIP3.ToString() + "/27";
                                        aDSLGroupIP.BlockCount = 32;

                                        aDSLGroupIP.Detach();
                                        Save(aDSLGroupIP);

                                        fromIP3 = fromIP3 + 32;
                                        virtualRange = virtualRange.Split('.')[0] + "." + virtualRange.Split('.')[1] + "." + virtualRange.Split('.')[2] + "." + (Convert.ToInt32(virtualRange.Split('.')[3]) + 1).ToString();
                                        length32 = length32 - 1;
                                    }

                                    while (length64 > 0 && fromIP3 + 63 <= endIP3)
                                    {
                                        aDSLGroupIP.ID = 0;
                                        aDSLGroupIP.VirtualRange = virtualRange;
                                        aDSLGroupIP.StartRange = fromIP0 + "." + fromIP1 + "." + fromIP2 + "." + fromIP3.ToString() + "/26";
                                        aDSLGroupIP.BlockCount = 64;

                                        aDSLGroupIP.Detach();
                                        Save(aDSLGroupIP);

                                        fromIP3 = fromIP3 + 64;
                                        virtualRange = virtualRange.Split('.')[0] + "." + virtualRange.Split('.')[1] + "." + virtualRange.Split('.')[2] + "." + (Convert.ToInt32(virtualRange.Split('.')[3]) + 1).ToString();
                                        length64 = length64 - 1;
                                    }

                                    if (length64 == 0)
                                        break;
                                }

                                if (length64 == 0)
                                    break;
                            }

                            if (length64 == 0)
                                break;
                        }
                    }
                }
                else
                    throw new Exception("لطفا شماره IP را وارد نمایید !");

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره پورت ، " + ex.Message, ex);
            }
        }

        private void PortNo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            if (Char.IsNumber(c) || char.Equals(c, '.'))
                e.Handled = false;
            else
                e.Handled = true;

            base.OnPreviewTextInput(e);
        }


        #endregion
    }
}
