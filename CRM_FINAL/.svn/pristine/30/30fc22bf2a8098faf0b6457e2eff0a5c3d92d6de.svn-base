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
    public class TabWindow : System.Windows.Controls.UserControl
    {
        #region Properties

        private static StatusBar _TabWindowStatusBar;
        public int _PageSize = 10;

        public Boolean _SearchExpander = true;

        #endregion

        #region Constructors

        public TabWindow()
        {
            this.Loaded += new RoutedEventHandler(TabWindowBase_Loaded);

            //ResourceDictionary defaultResourceDictionary = new ResourceDictionary();
            //defaultResourceDictionary.Source = new Uri("pack://application:,,,/CRM.Application;component/Resources/Styles/DefaultStyles.xaml");
            //base.Resources.MergedDictionaries.Add(defaultResourceDictionary);
        }

        #endregion

        #region Methods

        public static void ShowSuccessMessage(string message)
        {
            _TabWindowStatusBar.ShowSuccessMessage(message);
        }

        public static void ShowWarningMessage(string message)
        {
            _TabWindowStatusBar.ShowWarningsMessage(message);
        }

        public static void ShowErrorMessage(string message, Exception ex)
        {
            _TabWindowStatusBar.ShowErrorMessage(message, ex);
        }

        public static void HideMessage()
        {
            _TabWindowStatusBar.HideMessage();
        }
        #endregion

        #region Event Handlers

        private void TabWindowBase_Loaded(object sender, RoutedEventArgs e)
        {
            if (DB.CurrentUser.ID != 0)
            {
                UIElement container = this.Content as UIElement;

                if (DB.CurrentUser.UserConfig != null && DB.CurrentUser.UserConfig.PageSize != 0 && DB.CurrentUser.UserConfig.PageSize != null)
                    _PageSize = DB.CurrentUser.UserConfig.PageSize;

                if (DB.CurrentUser.UserConfig != null && DB.CurrentUser.UserConfig.CloseSearchExpander != null)
                    _SearchExpander = !DB.CurrentUser.UserConfig.CloseSearchExpander;

                if (Helper.FindVisualChildren<StatusBar>(container).Count() == 0)
                {
                    if (container is DockPanel)
                    {
                        _TabWindowStatusBar = new StatusBar();
                        DockPanel.SetDock(_TabWindowStatusBar, Dock.Bottom);
                        (container as DockPanel).Children.Insert(0, _TabWindowStatusBar);
                    }
                }
                else
                {
                    _TabWindowStatusBar = Helper.FindVisualChildren<StatusBar>(container).First() as StatusBar;
                }

                if (Helper.FindVisualChildren<Pager>(container).Count() != 0)
                {
                    (Helper.FindVisualChildren<Pager>(container).First() as Pager).PageSize = _PageSize;
                }

                if (Helper.FindVisualChildren<Expander>(container).Count() != 0)
                {
                    (Helper.FindVisualChildren<Expander>(container).First() as Expander).IsExpanded = _SearchExpander;
                }

                //
                CRM.Application.Codes.Print.CheckingGridColumn(this.Content, this.GetType().Name);

                //
            }
            else
            {
                this.Visibility = Visibility.Collapsed;
            }

        }

        #endregion

        #region Virtual Methods

        public virtual void Load()
        {
        }

        #endregion
    }
}