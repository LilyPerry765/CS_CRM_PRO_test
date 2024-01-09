using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Imaging;
using CRM.Application.UserControls;
using System.Linq;
using System.Data.Linq;
using CRM.Data;
using Enterprise;

namespace CRM.Application.Local
{
    public partial class PopupWindow : Window
    {
        public bool IsValidationMode { get; set; }

        #region Properties

        private Data.Schema.ActionLogRequest actionLogRequest = new Data.Schema.ActionLogRequest();

        public IEnumerable<System.Windows.Controls.Control> ValidationControls = null;

        public static  UserControls.FormTemplate _FormTemplate;
        public int currentStep { get; set; }
        public int currentStat { get; set; }
        public ImageSource HeaderIcon
        {
            get
            {
                if (base.Icon == null)
                    return new BitmapImage(Helper.MakePackUri("Images/document_32x32.png"));

                return base.Icon;
            }
            set
            {
                base.Icon = value;
            }
        }

        #endregion

        #region Constructors

        public PopupWindow()
        {
            if (DB.CurrentUser.ID != 0)
            {

                base.ResizeMode = System.Windows.ResizeMode.CanResize;
                base.SizeToContent = System.Windows.SizeToContent.Height;
                base.ShowActivated = true;
                base.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                if (double.IsNaN(this.Width)) base.Width = 780;

                this.Loaded += new RoutedEventHandler(PopupWindow_Loaded);
                this.SizeChanged += new SizeChangedEventHandler(PopupWindow_SizeChanged);
                base.KeyDown += new KeyEventHandler(delegate(object sender, KeyEventArgs e) { if (e.Key == Key.Escape) this.Close(); });

                ResourceDictionary defaultResourceDictionary = new ResourceDictionary();
                defaultResourceDictionary.Source = new Uri("pack://application:,,,/CRM.Application;component/Resources/Styles/DefaultStyles.xaml");

                ResourceDictionary popupResourceDictionary = new ResourceDictionary();
                popupResourceDictionary.Source = new Uri("pack://application:,,,/CRM.Application;component/Resources/Styles/PopupWindowStyles.xaml");

                ResourceDictionary validationResourceDictionary = new ResourceDictionary();
                validationResourceDictionary.Source = new Uri("pack://application:,,,/CRM.Application;component/Codes/Validation/ValidationResourceDictionary.xaml");

                base.Resources.MergedDictionaries.Add(defaultResourceDictionary);
                base.Resources.MergedDictionaries.Add(popupResourceDictionary);
                base.Resources.MergedDictionaries.Add(validationResourceDictionary);
            }
            else
            {
                base.Close();
            }
        }

        public PopupWindow(bool isValidationMode)
            : this()
        {
            IsValidationMode = isValidationMode;
        }

        #endregion

        #region Event Handlers

        public void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Logger.WriteInfo(this.GetType().Name);
            CreateTemplate();



         


            if (ValidationControls == null)
            {
                return;
            }

            var varControl = Codes.Validation.ValidationWorking.GetValidationXml().elements.FirstOrDefault(q => q.Name == this.GetType().FullName);
            if (varControl == null)
                return;


            List<Data.RegularExpression> lstRegularExpression = null;
            using (Data.MainDataContext entity = new Data.MainDataContext())
            {
                lstRegularExpression = entity.RegularExpressions.ToList();
            }


            foreach (var textBox in ValidationControls.Where(t => varControl.Controls.Any(q => q.Name == t.Name)))
            {
                UserControl uc = Codes.Validation.Helper.FindVisualParent<UserControl>(textBox);
                if (uc != null)
                {
                    continue;
                }

                var varFetchTextBox = varControl.Controls.FirstOrDefault(q => q.Name == textBox.Name);

                Binding binding = null;

                var varExistBinding =
                    textBox.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty);

                if (varExistBinding != null)
                {
                    binding = varExistBinding.ParentBinding;

                    binding.ValidationRules.Clear();

                    Binding cloneBinding =
                        Codes.Validation.CloneBinding.Clone(binding);

                    BindingOperations.ClearBinding(textBox, TextBox.TextProperty);

                    cloneBinding.FallbackValue = "بایندینگ در نظر گرفته شده اشتباه است";

                    if (cloneBinding.UpdateSourceTrigger == UpdateSourceTrigger.Explicit)
                    {
                        cloneBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    }

                    textBox.SetBinding(TextBox.TextProperty, cloneBinding);

                    binding = cloneBinding;
                }
                else
                {
                    binding = new Binding("Text");

                    binding.RelativeSource = new RelativeSource(RelativeSourceMode.Self);

                    textBox.SetBinding(TextBox.TextProperty, binding);
                }

                if (varFetchTextBox.IsRequire == true)
                {
                    binding.ValidationRules.Add
                        (new Codes.Validation.RequireValidation()
                        {
                            ErrorMessage = "پر کردن این فیلد اجباری است"
                        });
                }

                if (varFetchTextBox.RegularExpression != null && varFetchTextBox.RegularExpression.Trim() != string.Empty)
                {
                    var varRegularExpressionValidation = new Codes.Validation.RegularExpressionValidation();

                    varRegularExpressionValidation.RegularExperssion = varFetchTextBox.RegularExpression;

                    var varReqularExpression = lstRegularExpression.FirstOrDefault(q => q.RegularExpressinon == varFetchTextBox.RegularExpression);

                    if (varReqularExpression != null)
                    {
                        varRegularExpressionValidation.ErrorMessage = varReqularExpression.ErrorMessage;
                    }

                    binding.ValidationRules.Add(varRegularExpressionValidation);
                }
            }




        }

        private void PopupWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UIElement container = this.Content as UIElement;

            if (container != null)
            {
                Panel containerScrollViewer = Helper.FindVisualParent<Panel>(container);
                if (containerScrollViewer == null || containerScrollViewer.ActualWidth == 0) return;

                List<System.Windows.Controls.Control> controlsList = Helper.FindVisualChildren<System.Windows.Controls.Control>(container).ToList();

                foreach (System.Windows.Controls.Control control in controlsList.Where(t => t is Expander))
                {
                    control.Width = containerScrollViewer.ActualWidth - 35;
                    (control as Expander).Expanded += new RoutedEventHandler(Expander_Expanded);
                }
            }
        }

        private void Expander_Expanded(object sender, EventArgs e)
        {
            ScrollViewer containerScrollViewer = Helper.FindVisualChildren<ScrollViewer>(this.Content as UIElement).FirstOrDefault();
            Expander selectedExpandar = sender as Expander;

            //Panel stackPanel = selectedExpandar.Parent as Panel;
            Panel stackPanel = Helper.FindVisualParent<Panel>(selectedExpandar);
            int intIndexOfExpander = stackPanel.Children.IndexOf(selectedExpandar);
            double dblHeight = 0;

            for (int index = 0; index < intIndexOfExpander; index++)
            {
                Expander iteratorExpander = stackPanel.Children[index] as Expander;
                if (iteratorExpander != null)
                {
                    dblHeight += iteratorExpander.ActualHeight;
                }
            }

            containerScrollViewer.ScrollToVerticalOffset(dblHeight + (4 * intIndexOfExpander));
        }

        #endregion

        #region Methods

        private void CreateTemplate()
        {
            _FormTemplate = new UserControls.FormTemplate();
            _FormTemplate.FlowDirection = System.Windows.FlowDirection.RightToLeft;

            if (this.Content == null)
                return;

            UIElement container = this.Content as UIElement;



            this.Content = _FormTemplate;

            _FormTemplate.Main.Children.Add(container);

            List<System.Windows.Controls.Control> controlsList = Helper.FindVisualChildren<System.Windows.Controls.Control>(container).ToList();

            foreach (Button footerButton in controlsList.Where(t => t.Tag != null && t.Tag.ToString() == "Footer" && t.GetType() == typeof(Button)))
            {
                (footerButton.Parent as Panel).Children.Remove(footerButton);
                _FormTemplate.Footer.Children.Add(footerButton);
            }

            foreach (GroupBox control in controlsList.Where(t => t.GetType() == typeof(GroupBox)).ToList())
            {
                control.Style = (Style)FindResource("BlueGroupBox");
            }

            foreach (Expander control in controlsList.Where(t => t.GetType()  == typeof(Expander)).ToList())
            {
                control.Style = (Style)FindResource("BlueExpander");
            }

            ValidationControls = controlsList;
        }

        public void ResizeWindow()
        {
            try
            {
                this.SizeToContent = System.Windows.SizeToContent.Manual;
                this.Height = SystemParameters.PrimaryScreenHeight - 150;
                this.Top = 50;
            }
            catch
            {
            }
        }

        public static void ShowSuccessMessage(string message)
        {
            if (_FormTemplate != null)
                _FormTemplate.StatusBar.ShowSuccessMessage(message);
        }

        public static void ShowErrorMessage(string message, Exception ex)
        {
            _FormTemplate.StatusBar.ShowErrorMessage(message, ex);
        }

        public static void ShowWarningMessage(string message)
        {
            _FormTemplate.StatusBar.ShowWarningsMessage(message);
        }

        public static void HideMessage()
        {
            _FormTemplate.StatusBar.HideMessage();
        }

        #endregion

        #region Virtual Methods

        public void Save(object instance, bool isNew = false)
        {
            DB.Save(instance, isNew);

            actionLogRequest.FormType = this.GetType().FullName;
            actionLogRequest.FormName = this.Title;
            actionLogRequest.ObjectType = instance.GetType().Name;
            ActionLogDB.AddActionLog((byte)DB.ActionLog.Save, Folder.User.Current.Username, actionLogRequest);
        }

        #endregion
    }
}