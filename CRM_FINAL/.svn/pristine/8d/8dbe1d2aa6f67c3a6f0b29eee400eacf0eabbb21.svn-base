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
using System.Windows.Forms;
using Folder;
using System.Text.RegularExpressions;
using System.IO;
using System.Transactions;


namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for AddSwitchPortOfFileForm.xaml
    /// </summary>
    /// 
    public partial class AddSwitchPortOfFileForm : Local.PopupWindow
    {

        #region properties && Fileds
        string pathFix = string.Empty;
        string pathFloat = string.Empty;
        #endregion

        #region Constractor
        public AddSwitchPortOfFileForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Methode

        private void SelectfileFix_Click(object sender, RoutedEventArgs e)
        { 
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Text files (*.txt)|*.txt";

                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    PathfileFixTextBox.Text = pathFix = ofd.FileName;
        }

        private void SelectfileFloat_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text files (*.txt)|*.txt";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                PathfileFloatTextBox.Text = pathFloat = ofd.FileName;

        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            try
            {
                #region Fix
                if (System.IO.File.Exists(pathFix))
                {

                    int i = 0;
                    string Error = string.Empty;

                    string pattern = @"^(?<SwitchCode>\d+);(?<TelephoneNo>\d+);(?<Type>\d+);(?<PortNo>\w+)";
                    Regex reg = new Regex(pattern, RegexOptions.IgnoreCase);

                    IEnumerable<string> lines = System.IO.File.ReadLines(pathFix);

                    foreach (string line in lines)
                    {
                        string ReasonError = string.Empty;
                        bool ErrorFlag = false;
                        Data.SwitchPort switchPort = new Data.SwitchPort();
                        Data.Telephone telephoneItem = new Data.Telephone();

                        if (!reg.IsMatch(line))
                        {

                            ReasonError += "عدم مطابقت با الگو" + " ، ";
                            ErrorFlag = true;

                        }
                        else
                        {


                            Match match = reg.Match(line);

                            //////    بررسی وجود سوئیچ
                            Data.Switch switchItem = Data.DB.SearchByPropertyName<Data.Switch>("SwitchCode", Convert.ToInt64(match.Groups["SwitchCode"].Value)).SingleOrDefault();
                            if (switchItem == null)
                            {
                                ReasonError += "سوئییچ یافت نشد" + " ، ";
                                ErrorFlag = true;
                            }
                            else
                            {
                                switchPort.SwitchID = switchItem.ID;
                                
                            }
                            /////


                            ////// بررسی وجود تلفن

                            telephoneItem = Data.DB.SearchByPropertyName<Data.Telephone>("TelephoneNo", Convert.ToInt64(match.Groups["TelephoneNo"].Value)).SingleOrDefault();
                            if (telephoneItem == null)
                            {
                                ReasonError += "تلفن یافت نشد" + " ، ";
                                ErrorFlag = true;
                            }
                            else if (!telephoneItem.SwitchPortID.Equals(null))
                            {
                                ReasonError += "تلفن به یک پورت متصل است" + " ، ";
                                ErrorFlag = true;
                            }
                            else if (telephoneItem.Status != (byte)Data.DB.TelephoneStatus.Free)
                            {

                                ReasonError += "تلفن آزاد نیست" + " ، ";
                                ErrorFlag = true;
                            }


                            /////


                            ///// بررسی نوع تلفن (عادی یا همگانی بودن)
                            Data.SwitchPrecode switchPrecode = Data.SwitchPrecodeDB.GetSwitchPrecodeByTelephoneNo(Convert.ToInt64(match.Groups["TelephoneNo"].Value));
                            byte switchPrecodeType;
                            if (switchPrecode == null)
                            {
                                ReasonError += "پیش شماره یافت نشد" + " ، ";
                                ErrorFlag = true;
                            }
                            else if (!byte.TryParse(match.Groups["Type"].Value, out switchPrecodeType) || switchPrecode.PreCodeType != switchPrecodeType)
                            {
                                ReasonError += "نوع تلفن مطابقت ندارد" + " ، ";
                                ErrorFlag = true;
                            }

                            ////


                            ///// بررسی وجود پورت)
                            Data.SwitchPort switchPortItem = Data.DB.SearchByPropertyName<Data.SwitchPort>("PortNo", match.Groups["PortNo"].Value).SingleOrDefault();
                            if (switchPortItem != null)
                            {
                                ReasonError += "پورت موجود میباشد" + " ، ";
                                ErrorFlag = true;
                            }
                            else
                            {

                                switchPort.PortNo = match.Groups["PortNo"].Value;
                            }

                            ////



                            switchPort.Type = true;
                            switchPort.Status = (byte)Data.DB.SwitchPortStatus.Free;
                 


                                  

                        }

                        // اکر خطا رخ داده باشد تولید متن خطا در غیر این صورت ایجاد پورت و تغییر تلفن
                        if (ErrorFlag == true)
                        {
                            Error += (line + " // " + ReasonError).Remove((line + " // " + ReasonError).Length - 3, 3) + Environment.NewLine;
                            i++;
                        }
                        else
                        {

                            using (TransactionScope ts = new TransactionScope())
                            {

                                switchPort.Detach();
                                Data.DB.Save(switchPort, true);


                                telephoneItem.SwitchPortID = switchPort.ID;
                                telephoneItem.Detach();
                                Data.DB.Save(telephoneItem, false);

                                ts.Complete();
                            }

                        }

                    }


                    // ایجاد فایل خطا
                    if (i > 0)
                    {
                       
                        string ErrorPath = (Directory.GetParent(pathFix) + "\\Error_" + System.IO.Path.GetFileName(pathFix));
                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(ErrorPath))
                        {
                            file.Write(Error);
                        }
                       MessageBoxResult result = System.Windows.MessageBox.Show(" تعداد " + i.ToString() + " خطا در پورت های ثابت یافت شد ", "تعداد خطا", MessageBoxButton.OKCancel, MessageBoxImage.Error);

                       if (result == MessageBoxResult.OK)
                        {
                            System.Diagnostics.Process.Start(ErrorPath);
                        }
                        

                    }

                }
                #endregion

                #region Float
                if (System.IO.File.Exists(pathFloat))
                {

                    int i = 0;
                    string Error = string.Empty;

                    string patternWithoutONU = @"^(?<SwitchCode>\d+);(?<TelephoneNo>\d+);(?<Type>\d{1});(?<PortType>\w{1});(?<PortNo>\w+);(?<PortStatus>\d{1});(?<MDFHorizental>.*);$";
                    Regex regWithoutONU = new Regex(patternWithoutONU, RegexOptions.IgnoreCase);

                    string patternWithONU = @"^(?<SwitchCode>\d+);(?<TelephoneNo>\d+);(?<Type>\d{1});(?<PortType>\w{1});(?<PortNo>\w+);(?<PortStatus>\d{1});(?<MDFHorizental>.*);(?<ONU>[.\S]*)";
                    Regex regWithONU = new Regex(patternWithONU, RegexOptions.IgnoreCase);


                    IEnumerable<string> lines = System.IO.File.ReadLines(pathFloat);

                    foreach (string line in lines)
                    {

                        string ReasonError = string.Empty;
                        bool ErrorFlag = false;
                        bool UpdataTelephone = false;
                        Data.SwitchPort switchPort = new Data.SwitchPort();
                        Data.Telephone telephoneItem = new Data.Telephone();

                        if (regWithoutONU.IsMatch(line))
                        {
                            #region Match Without ONU
                            Match match = regWithoutONU.Match(line);

                            //////    بررسی وجود سوئیچ
                            Data.Switch switchItem = Data.DB.SearchByPropertyName<Data.Switch>("SwitchCode", match.Groups["SwitchCode"].Value).SingleOrDefault();
                            if (switchItem == null)
                            {
                                ReasonError += "سوئییچ یافت نشد" + " ، ";
                                ErrorFlag = true;
                            }
                            else
                            {
                                switchPort.SwitchID = switchItem.ID;

                            }
                            /////


                            // بررسی نوع پورت 

                            Data.SwitchType switchTypeItem = Data.DB.SearchByPropertyName<Data.SwitchType>("ID", switchItem.SwitchTypeID).SingleOrDefault();
                            
                            if (switchTypeItem != null &&  match.Groups["PortType"].Value == "V")
                            {
                                if (switchTypeItem.SwitchTypeValue != (byte)Data.DB.SwitchTypeCode.ONUVWire)
                                {
                                    ReasonError += "نوع پورت اونو نیست" + " ، ";
                                    ErrorFlag = true;

                                }
                            }
                            else if (switchTypeItem != null && match.Groups["PortType"].Value == "A")
                            {

                                if (switchTypeItem.SwitchTypeValue != (byte)Data.DB.SwitchTypeCode.ONUABWire)
                                {
                                    ReasonError += "نوع مسی ای بی وایر نیست" + " ، ";
                                    ErrorFlag = true;

                                }
                            }
                            else if (switchTypeItem != null && match.Groups["PortType"].Value == "Z")
                            {

                                if (switchTypeItem.SwitchTypeValue != (byte)Data.DB.SwitchTypeCode.ONUCopper)
                                {
                                    ReasonError += "نوع پورت مسی نیست" + " ، ";
                                    ErrorFlag = true;

                                }
                            }
                            else
                            {
                                ReasonError += "نوع پورت مشخص نیست" + " ، ";
                                ErrorFlag = true;
                            }



                            ////// بررسی وجود تلفن

                            telephoneItem = Data.DB.SearchByPropertyName<Data.Telephone>("TelephoneNo", match.Groups["TelephoneNo"].Value).SingleOrDefault();
                            if (telephoneItem == null)
                            {
                                ReasonError += "تلفن یافت نشد" + " ، ";
                                ErrorFlag = true;
                            }
                            else if (telephoneItem != null)
                            {
                              byte PortStatus=0;
                                // درصورت عدد بودن وضعیت پورت
                                if (byte.TryParse(match.Groups["PortStatus"].Value,out PortStatus))
	                           {

                                          // اگر وضعیت پورت تخصیص یافته بود
                                         if (PortStatus == (byte)Data.DB.PortStatusOfFile.Allocated)
                                    
                                         {
                                             // وتلفن دایر بود تلفن باید با پورت جدید مقدار بگیرد
                                             if (telephoneItem.Status == (byte)Data.DB.TelephoneStatus.Connecting)
                                             {
                                                 UpdataTelephone = true;
                                             }
                                                 // واگر تلفن دایر نباشد اعلام خطا میکند
                                             else
                                             {
                                                 ReasonError += "تلفن دایر نیست" + " ، ";
                                                 ErrorFlag = true;
                                             }
                                         }

                                }
                                else
                                {
                                ReasonError += "وضعیت پورت دارای مقدار صحیح نیست" + " ، ";
                                ErrorFlag = true;
                                }
                            }
                            /////


                            ///// بررسی نوع تلفن (عادی یا همگانی بودن)
                            Data.SwitchPrecode switchPrecode = Data.SwitchPrecodeDB.GetSwitchPrecodeByTelephoneNo(Convert.ToInt64(match.Groups["TelephoneNo"].Value));
                            byte switchPrecodeType;
                            if (switchPrecode == null)
                            {
                                ReasonError += "پیش شماره یافت نشد" + " ، ";
                                ErrorFlag = true;
                            }
                            else if (!byte.TryParse(match.Groups["Type"].Value, out switchPrecodeType) || switchPrecode.PreCodeType != switchPrecodeType)
                            {
                                ReasonError += "نوع تلفن مطابقت ندارد" + " ، ";
                                ErrorFlag = true;
                            }

                            ////


                            ///// بررسی وجود پورت)
                            Data.SwitchPort switchPortItem = Data.DB.SearchByPropertyName<Data.SwitchPort>("PortNo", match.Groups["PortNo"].Value).SingleOrDefault();
                            if (switchPortItem != null)
                            {
                                ReasonError += "پورت موجود میباشد" + " ، ";
                                ErrorFlag = true;
                            }
                            else
                            {

                                switchPort.PortNo = match.Groups["PortNo"].Value;
                                switchPort.MDFHorizentalID = match.Groups["MDFHorizental"].Value;
                            }

                            ////


                            //if (match.Groups["PortType"].Value == "V")
                            //{
                            //    ReasonError += "نوع پورت نمی تواند V5.2 باشد" + " ، ";
                            //    ErrorFlag = true;
                            //}
                            //else
                            //{
                            //   // switchPort.TypeOfPort =(byte)Data.DB.TypeOfPort.Z;
                            //}

                            #endregion

                        }
                        else if(regWithONU.IsMatch(line))
                        {

                            #region Match With ONU
                            Match match = regWithONU.Match(line);
                            //////    بررسی وجود سوئیچ
                            Data.Switch switchItem = Data.DB.SearchByPropertyName<Data.Switch>("SwitchCode", match.Groups["SwitchCode"].Value).SingleOrDefault();
                            if (switchItem == null)
                            {
                                ReasonError += "سوئییچ یافت نشد" + " ، ";
                                ErrorFlag = true;
                            }
                            else if(switchItem.FeatureONU != match.Groups["ONU"].Value.ToString())
                            {
                                ReasonError += "اونو یافت نشد" + " ، ";
                                ErrorFlag = true;
                            }
                            else

                            {
                                switchPort.SwitchID = switchItem.ID;

                            }
                            /////


                            // بررسی نوع پورت 

                            Data.SwitchType switchTypeItem = Data.DB.SearchByPropertyName<Data.SwitchType>("ID", switchItem.SwitchTypeID).SingleOrDefault();

                            if (switchTypeItem != null && match.Groups["PortType"].Value == "V")
                            {
                                if (switchTypeItem.SwitchTypeValue != (byte)Data.DB.SwitchTypeCode.ONUVWire)
                                {
                                    ReasonError += "نوع پورت اونو نیست" + " ، ";
                                    ErrorFlag = true;

                                }
                            }
                            else if (switchTypeItem != null && match.Groups["PortType"].Value == "A")
                            {

                                if (switchTypeItem.SwitchTypeValue != (byte)Data.DB.SwitchTypeCode.ONUABWire)
                                {
                                    ReasonError += "نوع مسی ای بی وایر نیست" + " ، ";
                                    ErrorFlag = true;

                                }
                            }
                            else if (switchTypeItem != null && match.Groups["PortType"].Value == "Z")
                            {

                                if (switchTypeItem.SwitchTypeValue != (byte)Data.DB.SwitchTypeCode.ONUCopper)
                                {
                                    ReasonError += "نوع پورت مسی نیست" + " ، ";
                                    ErrorFlag = true;

                                }
                            }
                            else
                            {
                                ReasonError += "نوع پورت مشخص نیست" + " ، ";
                                ErrorFlag = true;
                            }




                            ////// بررسی وجود تلفن

                            telephoneItem = Data.DB.SearchByPropertyName<Data.Telephone>("TelephoneNo", match.Groups["TelephoneNo"].Value).SingleOrDefault();
                            if (telephoneItem == null)
                            {
                                ReasonError += "تلفن یافت نشد" + " ، ";
                                ErrorFlag = true;
                            }
                            else if (telephoneItem != null)
                            {
                                byte PortStatus = 0;
                                // درصورت عدد بودن وضعیت پورت
                                if (byte.TryParse(match.Groups["PortStatus"].Value, out PortStatus))
                                {

                                    // اگر وضعیت پورت تخصیص یافته بود
                                    if (PortStatus == (byte)Data.DB.PortStatusOfFile.Allocated)
                                    {
                                        // وتلفن دایر بود تلفن باید با پورت جدید مقدار بگیرد
                                        if (telephoneItem.Status == (byte)Data.DB.TelephoneStatus.Connecting)
                                        {
                                            UpdataTelephone = true;
                                        }
                                        // واگر تلفن دایر نباشد اعلام خطا میکند
                                        else
                                        {
                                            ReasonError += "تلفن دایر نیست" + " ، ";
                                            ErrorFlag = true;
                                        }
                                    }

                                }
                                else
                                {
                                    ReasonError += "وضعیت پورت دارای مقدار صحیح نیست" + " ، ";
                                    ErrorFlag = true;
                                }
                            }
                            /////


                            ///// بررسی نوع تلفن (عادی یا همگانی بودن)
                            Data.SwitchPrecode switchPrecode = Data.SwitchPrecodeDB.GetSwitchPrecodeByTelephoneNo(Convert.ToInt64(match.Groups["TelephoneNo"].Value));
                            byte switchPrecodeType;
                            if (switchPrecode == null)
                            {
                                ReasonError += "پیش شماره یافت نشد" + " ، ";
                                ErrorFlag = true;
                            }
                            else if (!byte.TryParse(match.Groups["Type"].Value, out switchPrecodeType) || switchPrecode.PreCodeType != switchPrecodeType)
                            {
                                ReasonError += "نوع تلفن مطابقت ندارد" + " ، ";
                                ErrorFlag = true;
                            }

                            ////

                            ///// بررسی وجود پورت)
                            Data.SwitchPort switchPortItem = Data.DB.SearchByPropertyName<Data.SwitchPort>("PortNo", match.Groups["PortNo"].Value).SingleOrDefault();
                            if (switchPortItem != null)
                            {
                                ReasonError += "پورت موجود میباشد" + " ، ";
                                ErrorFlag = true;
                            }
                            else
                            {

                                switchPort.PortNo = match.Groups["PortNo"].Value;
                                switchPort.MDFHorizentalID = match.Groups["MDFHorizental"].Value;
                            }

                            ////


                            //if (match.Groups["PortType"].Value == "Z")
                            //{
                            //    ReasonError += "نوع پورت نمی تواند مسی/ ABWire باشد" + " ، ";
                            //    ErrorFlag = true;
                            //}
                            //else
                            //{
                            //   // switchPort.TypeOfPort = (byte)Data.DB.TypeOfPort.V;
                            //}


                            #endregion

                        }
                        else
                        {
                            #region Did not match any of the patterns
                            ReasonError += "عدم مطابقت با الگو" + " ، ";
                            ErrorFlag = true;
                            #endregion
                        }


                        if (ErrorFlag == true)
                        {
                            Error += (line + " // " + ReasonError).Remove((line + " // " + ReasonError).Length - 3, 3) + Environment.NewLine;
                            i++;
                        }
                        else
                        {

                            using (TransactionScope ts = new TransactionScope())
                            {
                                switchPort.Type = false;
                                switchPort.Status = (byte)Data.DB.SwitchPortStatus.Free;
                                switchPort.Detach();
                                Data.DB.Save(switchPort, true);

                                if (UpdataTelephone == true)
                                {
                                    telephoneItem.SwitchPortID = switchPort.ID;
                                    telephoneItem.Detach();
                                    Data.DB.Save(telephoneItem, false);
                                }
                                ts.Complete();
                            }

                        }


                    }


                    if (i > 0)
                    {

                        string ErrorPath = (Directory.GetParent(pathFloat) + "\\Error_" + System.IO.Path.GetFileName(pathFloat));
                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(ErrorPath))
                        {
                            file.Write(Error);
                        }
                        MessageBoxResult result = System.Windows.MessageBox.Show(" تعداد " + i.ToString() + " خطا پورت ها شناور یافت شد ", " ", MessageBoxButton.OKCancel, MessageBoxImage.Error);

                        if (result == MessageBoxResult.OK)
                        {
                            System.Diagnostics.Process.Start(ErrorPath);
                        }


                    }


                }

                #endregion
                ShowSuccessMessage("ثبت اطلاعات انجام شد");
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ثبت اطلاعات", ex);
            }
        }
        #endregion

       



    }
}
