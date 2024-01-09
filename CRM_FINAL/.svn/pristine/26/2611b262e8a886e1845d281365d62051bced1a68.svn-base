using CRM.Application.Codes;
using CRM.Application.UserControls;
using CRM.Data;
using Stimulsoft.Report.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace CRM.Application.Views
{
    public partial class UserConfigForm : Local.PopupWindow
    {
        #region Properties

        User _user { get; set; }

        CRM.Data.Schema.UserConfig userConfig { get; set; }

        public ReportSetting CurrentReportSettings { get; set; }

        #endregion

        #region Constructors

        public UserConfigForm()
        {
            this.CurrentReportSettings = new ReportSetting();
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        //TODO:rad
        private void Initialize()
        {
            userConfig = new Data.Schema.UserConfig();
            Action fillCombo = new Action(() =>
                                                {
                                                    List<EnumItem> result = new List<EnumItem>();
                                                    System.Reflection.FieldInfo[] fields = typeof(StiPageOrientation).GetFields().Where(f => f.IsLiteral).ToArray();
                                                    foreach (System.Reflection.FieldInfo fi in fields)
                                                    {
                                                        result.Add(new EnumItem { Name = fi.Name, EnumValue = Convert.ToInt32(fi.GetRawConstantValue()) });
                                                    }
                                                    PageOrientationsComboBox.ItemsSource = result;
                                                }
                                         );
            fillCombo.Invoke();
            //PageSizeComboBox.ItemsSource = Helpers.GetEnumItems(typeof(System.Drawing.Printing.PaperKind));
            for (double d = 0.1; d <= 2.5; d += 0.25)
            {
                HeaderBorderThicknessComboBox.Items.Add(d);
                TextBorderThicknessComboBox.Items.Add(d);
            }
        }

        private void LoadData()
        {
            _user = Data.UserDB.GetUserByID(DB.CurrentUser.ID);

            if (_user.Config != null)
            {
                userConfig = LogSchemaUtility.Deserialize<CRM.Data.Schema.UserConfig>(_user.Config.ToString());
            }

            if (DashboardAutoUpdateCheckBox.IsChecked == true)
            {
                DashboardUpdateTimeLabel.Visibility = Visibility.Collapsed;
                DashboardUpdateTimeTextBox.Visibility = Visibility.Collapsed;

                PupupNotificationLabel.Visibility = Visibility.Visible;
                PupupNotificationCheckBox.Visibility = Visibility.Visible;
            }
            else
            {
                DashboardUpdateTimeLabel.Visibility = Visibility.Visible;
                DashboardUpdateTimeTextBox.Visibility = Visibility.Visible;


                PupupNotificationLabel.Visibility = Visibility.Collapsed;
                PupupNotificationCheckBox.Visibility = Visibility.Collapsed;
            }

            this.DataContext = userConfig;
        }

        #endregion

        #region Event Handlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                userConfig = this.DataContext as CRM.Data.Schema.UserConfig;
                _user.Config = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.UserConfig>(userConfig, true));

                _user.Detach();
                DB.Save(_user, false);

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در دخیره تنظیمات", ex);
            }
        }

        private void PageOrientationsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PageOrientationsComboBox.SelectedValue != null)
            {
                this.CurrentReportSettings.StiPageOrientation = Convert.ToInt32(PageOrientationsComboBox.SelectedValue);
            }
        }

        private void SetBrush_Click(object sender, RoutedEventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                colorDialog.AllowFullOpen = false;
                System.Windows.Forms.DialogResult result = colorDialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    SolidColorBrush selectedBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(colorDialog.Color.A, colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B));
                    System.Windows.Controls.Button source = e.Source as System.Windows.Controls.Button;

                    if (source != null)
                    {
                        switch (source.Name)
                        {
                            case "HeaderBackgroundButton":
                                {
                                    this.CurrentReportSettings.HeaderBackground = selectedBrush;
                                    break;
                                }
                            case "HeaderForegroundButton":
                                {
                                    this.CurrentReportSettings.HeaderForeground = selectedBrush;
                                    break;
                                }
                            case "HeaderBorderBrushButton":
                                {
                                    this.CurrentReportSettings.HeaderBorderBrush = selectedBrush;
                                    break;
                                }
                            case "TextBackgroundButton":
                                {
                                    this.CurrentReportSettings.TextBackground = selectedBrush;
                                    break;
                                }
                            case "TextForegroundButton":
                                {
                                    this.CurrentReportSettings.TextForeground = selectedBrush;
                                    break;
                                }
                            case "TextBorderBrushButton":
                                {
                                    this.CurrentReportSettings.TextBorderBrush = selectedBrush;
                                    break;
                                }
                        }
                    }
                }
            }
        }

        private void SetFont_Click(object sender, RoutedEventArgs e)
        {
            PersianFontDialog fontDialog = new PersianFontDialog();
            fontDialog.Owner = this;
            bool? result = fontDialog.ShowDialog();
            if (result.Value)
            {
                System.Windows.Controls.Button source = e.Source as System.Windows.Controls.Button;
                if (source != null)
                {
                    switch (source.Name)
                    {
                        case "HeaderFontButton":
                            {
                                this.CurrentReportSettings.HeaderFont = new System.Drawing.Font(
                                                                                       fontDialog.Font.Family.Source,
                                                                                       (float)fontDialog.Font.Size,
                                                                                       ((fontDialog.Font.Weight == FontWeights.Bold) ? System.Drawing.FontStyle.Bold : System.Drawing.FontStyle.Regular) |
                                                                                       ((fontDialog.Font.Style == FontStyles.Italic) ? System.Drawing.FontStyle.Italic : System.Drawing.FontStyle.Regular) |
                                                                                       ((fontDialog.Font.TextDecorations == TextDecorations.Underline ? System.Drawing.FontStyle.Underline : System.Drawing.FontStyle.Regular))
                                                                                     );
                                this.CurrentReportSettings.HeaderFontIsBold = this.CurrentReportSettings.HeaderFont.Bold;
                                this.CurrentReportSettings.HeaderFontIsItalic = this.CurrentReportSettings.HeaderFont.Italic;
                                this.CurrentReportSettings.HeaderFontIsUnderlined = this.CurrentReportSettings.HeaderFont.Underline;
                                break;
                            }
                        case "TextFontButton":
                            {
                                this.CurrentReportSettings.TextFont = new System.Drawing.Font(
                                                                                       fontDialog.Font.Family.Source,
                                                                                       (float)fontDialog.Font.Size,
                                                                                       ((fontDialog.Font.Weight == FontWeights.Bold) ? System.Drawing.FontStyle.Bold : System.Drawing.FontStyle.Regular) |
                                                                                       ((fontDialog.Font.Style == FontStyles.Italic) ? System.Drawing.FontStyle.Italic : System.Drawing.FontStyle.Regular) |
                                                                                       ((fontDialog.Font.TextDecorations == TextDecorations.Underline ? System.Drawing.FontStyle.Underline : System.Drawing.FontStyle.Regular))
                                                                                     );
                                this.CurrentReportSettings.TextFontIsBold = this.CurrentReportSettings.TextFont.Bold;
                                this.CurrentReportSettings.TextFontIsItalic = this.CurrentReportSettings.TextFont.Italic;
                                this.CurrentReportSettings.TextFontIsUnderlined = this.CurrentReportSettings.TextFont.Underline;
                                break;
                            }
                    }
                }
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Windows.Controls.ComboBox source = e.Source as System.Windows.Controls.ComboBox;

            if (source != null)
            {
                if (source.SelectedValue == null)
                {
                    return;
                }
                double selectedValue = Convert.ToDouble(source.SelectedValue);

                switch (source.Name)
                {
                    case "HeaderBorderThicknessComboBox":
                        {
                            this.CurrentReportSettings.HeaderBorderThickness = selectedValue;
                            break;
                        }
                    case "TextBorderThicknessComboBox":
                        {
                            this.CurrentReportSettings.TextBorderThickness = selectedValue;
                            break;
                        }
                }
            }
        }

        //TODO:rad
        private void ShowReportButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //دیتای مورد نیاز برای ایجاد گزارش
                var result = SummaryInfo.GetSampleData();

                // هدر گزارش به صورت پیش فرض حاشیه داشته باشد یا بر اساس مقادر انتخابی توسط کاربر
                if (HeaderHasBorderCheckBox.IsChecked.Value)
                {
                    this.CurrentReportSettings.HeaderBorderBrush = (this.CurrentReportSettings.HeaderBorderBrush != null) ? this.CurrentReportSettings.HeaderBorderBrush : System.Windows.Media.Brushes.Black;
                    this.CurrentReportSettings.HeaderBorderThickness = (!this.CurrentReportSettings.HeaderBorderThickness.Equals(0.0)) ? this.CurrentReportSettings.HeaderBorderThickness : 1.0;
                }

                // آیتم های متنی گزارش به صورت پیش فرض حاشیه داشته باشند یا بر اساس مقادر انتخابی توسط کاربر
                if (TextHasBorderCheckBox.IsChecked.Value)
                {
                    this.CurrentReportSettings.TextBorderBrush = (this.CurrentReportSettings.TextBorderBrush != null) ? this.CurrentReportSettings.TextBorderBrush : System.Windows.Media.Brushes.Black;
                    this.CurrentReportSettings.TextBorderThickness = (!this.CurrentReportSettings.TextBorderThickness.Equals(0.0)) ? this.CurrentReportSettings.TextBorderThickness : 1.0;
                }

                //ارسال برای نمایش گزارش
                Helper.ShowSampleReport(
                                         result,
                                         this.CurrentReportSettings.HeaderFont,
                                         this.CurrentReportSettings.HeaderFontIsBold,
                                         this.CurrentReportSettings.HeaderFontIsItalic,
                                         this.CurrentReportSettings.HeaderFontIsUnderlined,
                                         this.CurrentReportSettings.HeaderBackground,
                                         this.CurrentReportSettings.HeaderForeground,
                                         this.CurrentReportSettings.HeaderBorderBrush,
                                         this.CurrentReportSettings.HeaderBorderThickness,
                                         this.CurrentReportSettings.TextFont,
                                         this.CurrentReportSettings.TextFontIsBold,
                                         this.CurrentReportSettings.TextFontIsItalic,
                                         this.CurrentReportSettings.TextFontIsUnderlined,
                                         this.CurrentReportSettings.TextForeground,
                                         this.CurrentReportSettings.TextBackground,
                                         this.CurrentReportSettings.TextBorderBrush,
                                         this.CurrentReportSettings.TextBorderThickness,
                                         this.CurrentReportSettings.TextHasWordWrap,
                                         this.CurrentReportSettings.HeaderHasWordWrap,
                                         this.CurrentReportSettings.PrintWithPreview,
                                         this.CurrentReportSettings.StiPageOrientation,
                                         this.CurrentReportSettings.ReportHasPageFooter,
                                         this.CurrentReportSettings.ReportHasTitle,
                                         this.CurrentReportSettings.ReportHasDate,
                                         this.CurrentReportSettings.ReportHasTime,
                                         this.CurrentReportSettings.ReportHasLogo,
                                         this.CurrentReportSettings.ReportSumRecordsQuantity
                                        );
            }
            catch (Exception ex)
            {
                Enterprise.Logger.Write(ex, "خطا در نمایش گزارش فرضی");
                System.Windows.MessageBox.Show("خطا در نمایش", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void HasBorder_Checked(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.CheckBox source = e.Source as System.Windows.Controls.CheckBox;
            if (source != null)
            {
                switch (source.Name)
                {
                    case "HeaderHasBorderCheckBox":
                        {
                            foreach (var child in Helper.FindVisualChildren<System.Windows.FrameworkElement>(this).Where(c => c.Tag != null))
                            {
                                if (child.Tag.ToString() == "HeaderBorder")
                                {
                                    child.Visibility = Visibility.Visible;
                                }
                            }
                            break;
                        }
                    case "TextHasBorderCheckBox":
                        {
                            foreach (var child in Helper.FindVisualChildren<System.Windows.FrameworkElement>(this).Where(c => c.Tag != null))
                            {
                                if (child.Tag.ToString() == "TextBorder")
                                {
                                    child.Visibility = Visibility.Visible;
                                }
                            }
                            break;
                        }
                }
            }
        }

        private void HasBorder_UnChecked(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.CheckBox source = e.Source as System.Windows.Controls.CheckBox;
            if (source != null)
            {
                switch (source.Name)
                {
                    case "HeaderHasBorderCheckBox":
                        {
                            foreach (var child in Helper.FindVisualChildren<System.Windows.FrameworkElement>(this).Where(c => c.Tag != null))
                            {
                                if (child.Tag.ToString() == "HeaderBorder")
                                {
                                    child.Visibility = Visibility.Collapsed;
                                }
                            }
                            break;
                        }
                    case "TextHasBorderCheckBox":
                        {
                            foreach (var child in Helper.FindVisualChildren<System.Windows.FrameworkElement>(this).Where(c => c.Tag != null))
                            {
                                if (child.Tag.ToString() == "TextBorder")
                                {
                                    child.Visibility = Visibility.Collapsed;
                                }
                            }
                            break;
                        }
                }
            }
        }

        private void HasWrodWarpCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.CheckBox source = e.Source as System.Windows.Controls.CheckBox;
            if (source != null)
            {
                switch (source.Name)
                {
                    case "HeaderHasWorWrapCheckBox":
                        {
                            this.CurrentReportSettings.HeaderHasWordWrap = true;
                            break;
                        }
                    case "TextHasWrodWarpCheckBox":
                        {
                            this.CurrentReportSettings.TextHasWordWrap = true;
                            break;
                        }
                }
            }
        }

        private void HasWrodWarpCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.CheckBox source = e.Source as System.Windows.Controls.CheckBox;
            if (source != null)
            {
                switch (source.Name)
                {
                    case "HeaderHasWorWrapCheckBox":
                        {
                            this.CurrentReportSettings.HeaderHasWordWrap = false;
                            break;
                        }
                    case "TextHasWrodWarpCheckBox":
                        {
                            this.CurrentReportSettings.TextHasWordWrap = false;
                            break;
                        }
                }
            }
        }

        private void PrintWithPreviewCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            this.CurrentReportSettings.PrintWithPreview = true;
        }

        private void PrintWithPreviewCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            this.CurrentReportSettings.PrintWithPreview = false;
        }

        private void ReportHasPageFooterCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            this.CurrentReportSettings.ReportHasPageFooter = true;
        }

        private void ReportHasPageFooterCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            this.CurrentReportSettings.ReportHasPageFooter = false;
        }

        private void ReportHasTitleCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            this.CurrentReportSettings.ReportHasTitle = true;
        }

        private void ReportHasTitleCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            this.CurrentReportSettings.ReportHasTitle = false;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            if (Char.IsNumber(c))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void DashboardAutoUpdateCheckBox_Checked(object sender, RoutedEventArgs e)
        {

            DashboardUpdateTimeLabel.Visibility = Visibility.Collapsed;
            DashboardUpdateTimeTextBox.Visibility = Visibility.Collapsed;

            PupupNotificationLabel.Visibility = Visibility.Visible;
            PupupNotificationCheckBox.Visibility = Visibility.Visible;

        }

        private void DashboardAutoUpdateCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            DashboardUpdateTimeLabel.Visibility = Visibility.Visible;
            DashboardUpdateTimeTextBox.Visibility = Visibility.Visible;


            PupupNotificationLabel.Visibility = Visibility.Collapsed;
            PupupNotificationCheckBox.Visibility = Visibility.Collapsed;
        }

        private void ReportHasDateCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            this.CurrentReportSettings.ReportHasDate = true;
        }

        private void ReportHasDateCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            this.CurrentReportSettings.ReportHasDate = false;
        }

        private void ReportHasTimeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            this.CurrentReportSettings.ReportHasTime = true;
        }

        private void ReportHasTimeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            this.CurrentReportSettings.ReportHasTime = false;
        }

        private void ReportHasLogoCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            this.CurrentReportSettings.ReportHasLogo = true;
        }

        private void ReportHasLogoCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            this.CurrentReportSettings.ReportHasLogo = false;
        }

        private void ReportSumRecordsQuantityCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            this.CurrentReportSettings.ReportSumRecordsQuantity = true;
        }

        private void ReportSumRecordsQuantityCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            this.CurrentReportSettings.ReportSumRecordsQuantity = false;
        }

        #endregion

    }
}
