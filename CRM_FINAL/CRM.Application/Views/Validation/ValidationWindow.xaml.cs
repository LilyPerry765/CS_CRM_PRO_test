using System.Linq;
using System.Windows;
using System.Reflection;
using System.Collections.Generic;
using System;
using System.Windows.Markup;
using System.Windows.Controls;
using System.Windows.Data;
using System.ComponentModel;

namespace CRM.Application.Views.Validation
{
    public partial class ValidationWindow : Window
    {
        public ValidationWindow()
        {
            InitializeComponent();
        }

        List<Codes.Validation.ValidationWindowAssociate> lstValidationWindowAssociate =
            new List<Codes.Validation.ValidationWindowAssociate>();

        List<Codes.Validation.ValidationUserControlAssociate> lstValidationUserControlAssociate =
            new List<Codes.Validation.ValidationUserControlAssociate>();

        ICollectionView windowsViewSource;
        ICollectionView usercontrolViewSource;

        private void BaseWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Codes.Validation.XmlValidation validationWorking = Codes.Validation.ValidationWorking.GetValidationXml();

            Assembly currentAssembly = System.Reflection.Assembly.GetAssembly(this.GetType());

            List<Type> lstWindow = currentAssembly.GetTypes()
                .Where(q => HisParentIsInThisType(q, typeof(Window))).ToList();
            List<Type> lstUserControls = currentAssembly.GetTypes()
                .Where(q => HisParentIsInThisType(q, typeof(UserControl))).ToList();

            lstWindow.ForEach(q =>
                {
                    try
                    {
                        Window Reflectedwindow = Activator.CreateInstance(q) as Window;

                        if (Reflectedwindow.Content != null)
                        {
                            Codes.Validation.ValidationWindowAssociate validationWindowAssociate =
                                new Codes.Validation.ValidationWindowAssociate();
                            validationWindowAssociate.Title = Reflectedwindow.Title;
                            validationWindowAssociate.Type = q;
                            validationWindowAssociate.HasContent = Reflectedwindow.Content == null ? true : false;
                            validationWindowAssociate.Fullname = q.FullName;

                            if (validationWorking != null && validationWorking.elements.FirstOrDefault(p => p.Name == q.FullName) != null)
                            {
                                validationWindowAssociate.HasValidation = true;
                            }

                            lstValidationWindowAssociate.Add(validationWindowAssociate);
                        }
                    }
                    catch
                    {
                    }
                });

            lstUserControls.ForEach(q =>
                {
                    try
                    {
                        UserControl userControl = Activator.CreateInstance(q) as UserControl;

                        if (userControl.Content != null)
                        {
                            Codes.Validation.ValidationUserControlAssociate validationUserControlAssociate =
                                new Codes.Validation.ValidationUserControlAssociate();

                            validationUserControlAssociate.Type = q;
                            validationUserControlAssociate.HasContent = userControl.Content == null ? true : false;
                            validationUserControlAssociate.Fullname = q.FullName;

                            if (validationWorking != null && validationWorking.elements.FirstOrDefault(p => p.Name == q.FullName) != null)
                            {
                                validationUserControlAssociate.HasValidation = true;
                            }

                            if (userControl.Tag != null)
                            {
                                validationUserControlAssociate.Title = userControl.Tag.ToString();
                            }
                            else
                            {
                                validationUserControlAssociate.Title = userControl.GetType().Name;
                            }

                            lstValidationUserControlAssociate.Add(validationUserControlAssociate);
                        }
                    }
                    catch
                    {
                    }
                });

            windowsViewSource = CollectionViewSource.GetDefaultView(lstValidationWindowAssociate.OrderBy(q => q.Title));
            windowsViewSource.GroupDescriptions.Add(new PropertyGroupDescription("Fullname", new Codes.Validation.GroupingValidationConvertor()));

            usercontrolViewSource = CollectionViewSource.GetDefaultView(lstValidationUserControlAssociate.OrderBy(q => q.Title));
            usercontrolViewSource.GroupDescriptions.Add(new PropertyGroupDescription("Fullname", new Codes.Validation.GroupingValidationConvertor()));

            lstWindows.ItemsSource = windowsViewSource;
            lstUserCotrols.ItemsSource = usercontrolViewSource;
        }

        private bool HisParentIsInThisType(Type type, Type baseType)
        {
            if (type == this.GetType() || type == typeof(SetValidationWindow) || type == typeof(SetValidationDetailWindow))
            {
                return (false);
            }

            while (true)
            {
                if (baseType == type.BaseType || baseType == type)
                {
                    return (true);
                }

                if (type.BaseType == null)
                {
                    break;
                }
                else
                {
                    type = type.BaseType;
                }
            }

            return (false);
        }

        private void windowButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button == null || button.Tag == null)
            {
                return;
            }

            Codes.Validation.ValidationWindowAssociate validatieWindowAssociate = button.Tag as Codes.Validation.ValidationWindowAssociate;

            try
            {
                Window reflectedwindow = Activator.CreateInstance(validatieWindowAssociate.Type) as Window;
                SetValidationWindow setValidationWindow = new SetValidationWindow();
                setValidationWindow.FlowDirection = FlowDirection.RightToLeft;
                setValidationWindow.NameOfControl = validatieWindowAssociate.Fullname;
                setValidationWindow.Title = reflectedwindow.Title;
                setValidationWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                setValidationWindow.Height = 600;
                setValidationWindow.Width = 600;

                setValidationWindow.SizeToContent = System.Windows.SizeToContent.Height;

                var content = reflectedwindow.Content;
                reflectedwindow.Content = null;
                setValidationWindow.Content = content;

                setValidationWindow.ShowDialog();
            }
            catch (MissingMethodException)
            {
                MessageBox.Show("تمامی ویندوزها بایستی سازنده بدون پارامتر داشته باشد");
            }
            catch
            {
                MessageBox.Show("خطایی رخ داده است با مدیر سیستم تماس بگیرید");
            }
        }

        private void userControlButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button == null || button.Tag == null)
            {
                return;
            }

            Codes.Validation.ValidationUserControlAssociate validateUserControlAssociate = button.Tag as Codes.Validation.ValidationUserControlAssociate;

            try
            {
                UserControl userControl = Activator.CreateInstance(validateUserControlAssociate.Type) as UserControl;

                SetValidationWindow setValidationWindow = new SetValidationWindow();
                setValidationWindow.FlowDirection = FlowDirection.RightToLeft;
                setValidationWindow.Title = validateUserControlAssociate.Title;
                setValidationWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                setValidationWindow.Height = 600;
                setValidationWindow.Width = 600;
                setValidationWindow.NameOfControl = validateUserControlAssociate.Fullname;
                setValidationWindow.SizeToContent = System.Windows.SizeToContent.Height;

                var content = userControl.Content;
                userControl.Content = null;
                setValidationWindow.Content = content;

                setValidationWindow.ShowDialog();
            }
            catch (MissingMethodException)
            {
                MessageBox.Show("تمامی ویندوزها بایستی سازنده بدون پارامتر داشته باشد");
            }
            catch
            {
                MessageBox.Show("خطایی رخ داده است با مدیر سیستم تماس بگیرید");
            }
        }

        private void txtSearchWindow_TextChanged(object sender, TextChangedEventArgs e)
        {
            windowsViewSource.Filter = new Predicate<object>(WindowSearchFilter);
        }

        private void txtSearchUserControl_TextChanged(object sender, TextChangedEventArgs e)
        {
            usercontrolViewSource.Filter = new Predicate<object>(UserControlSearchFilter);
        }

        private bool WindowSearchFilter(object param)
        {
            var validationWindowAssociate = param as Codes.Validation.ValidationWindowAssociate;

            if (validationWindowAssociate.Title.ToLower().Contains(txtSearchWindow.Text.ToLower()))
            {
                return (true);
            }

            return (false);
        }

        private bool UserControlSearchFilter(object param)
        {
            var validationUserControlAssociate = param as Codes.Validation.ValidationUserControlAssociate;

            if (validationUserControlAssociate.Title.ToLower().Contains(txtSearchUserControl.Text.ToLower()))
            {
                return (true);
            }

            return (false);
        }
    }
}
