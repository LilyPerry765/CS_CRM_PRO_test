using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Collections.Generic;
using System;
using CRM.Application.UserControls;
using System.Linq;
using CRM.Data;

namespace CRM.Application.Local
{
    public class UserControlBase : System.Windows.Controls.UserControl
    {

        #region Properties && Events

        public bool _IsLoaded = false;


        #endregion

        public UserControlBase()
        {
            this.Loaded += UserControlBase_Loaded;

        }

        private void UserControlBase_Loaded(object sender, RoutedEventArgs e)
        {


            var controls = Codes.Validation.Helper.FindVisualChildren<System.Windows.Controls.Control>(this);

            if (controls == null)
            {
                return;
            }

            Codes.Validation.XmlValidation xmlValidation = Codes.Validation.ValidationWorking.GetValidationXml();

            List<Data.RegularExpression> lstRegularExpression = null;
            using (Data.MainDataContext entity = new Data.MainDataContext())
            {
                lstRegularExpression = entity.RegularExpressions.ToList();
            }

            string fullname = this.GetType().FullName;

            var ValidationControl = xmlValidation.elements.FirstOrDefault(q => q.Name == fullname);

            if (ValidationControl != null)
            {
                foreach (var control in controls.Where(c => c.Visibility == Visibility.Visible || c.IsEnabled == true))
                {


                    UserControl uc = Codes.Validation.Helper.FindVisualParent<UserControl>(control);
                    if (uc.GetType() != this.GetType())
                    {
                        continue;
                    }

                    var varFetchTextBox = ValidationControl.Controls.FirstOrDefault(q => q.Name == control.Name);

                    if (varFetchTextBox == null)
                        continue;

                    Binding binding = null;

                    var varExistBinding = control.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty);

                    if (varExistBinding != null)
                    {
                        binding = varExistBinding.ParentBinding;

                        binding.ValidationRules.Clear();

                        Binding cloneBinding =
                            Codes.Validation.CloneBinding.Clone(binding);

                        if (cloneBinding == null)
                        {
                            continue;
                        }

                        BindingOperations.ClearBinding(control, TextBox.TextProperty);

                        cloneBinding.FallbackValue = "بایندینگ در نظر گرفته شده اشتباه است";

                        if (cloneBinding.UpdateSourceTrigger == UpdateSourceTrigger.Explicit)
                        {
                            cloneBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                        }

                        control.SetBinding(TextBox.TextProperty, cloneBinding);

                        binding = cloneBinding;
                    }
                    else
                    {
                        binding = new Binding("Text");

                        binding.RelativeSource = new RelativeSource(RelativeSourceMode.Self);

                        control.SetBinding(TextBox.TextProperty, binding);
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

            Window window = Helper.FindVisualParent<Window>(this);

            if (window != null)
            {
                if (window.GetType().Name.Contains("SetValidationWindow"))
                {
                    e.Handled = true;
                }
            }
        }


    }
}