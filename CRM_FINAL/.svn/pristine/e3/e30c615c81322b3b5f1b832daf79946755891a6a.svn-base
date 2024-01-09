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
using Enterprise;
using System.Reflection;

namespace CRM.Application.Local
{
    public class ExtendedTabWindowBase : System.Windows.Controls.UserControl
    {
        #region Properties

        private static ExtendedStatusBar _TabWindowStatusBar;
        public int _PageSize = 10;

        public Boolean _SearchExpander = true;

        #endregion

        #region Constructors

        public ExtendedTabWindowBase()
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
            if (this.TimeConsumingBackgroundWorker != null)
            {
                this.TimeConsumingBackgroundWorker = new BackgroundWorker();
            }

            if (DB.CurrentUser.ID != 0)
            {
                UIElement container = this.Content as UIElement;

                if (DB.CurrentUser.UserConfig != null && DB.CurrentUser.UserConfig.PageSize != 0 && DB.CurrentUser.UserConfig.PageSize != null)
                    _PageSize = DB.CurrentUser.UserConfig.PageSize;

                if (DB.CurrentUser.UserConfig != null && DB.CurrentUser.UserConfig.CloseSearchExpander != null)
                    _SearchExpander = !DB.CurrentUser.UserConfig.CloseSearchExpander;

                if (Helper.FindVisualChildren<ExtendedStatusBar>(container).Count() == 0)
                {
                    if (container is DockPanel)
                    {
                        _TabWindowStatusBar = new ExtendedStatusBar();
                        DockPanel.SetDock(_TabWindowStatusBar, Dock.Bottom);
                        (container as DockPanel).Children.Insert(0, _TabWindowStatusBar);
                    }
                }
                else
                {
                    _TabWindowStatusBar = Helper.FindVisualChildren<ExtendedStatusBar>(container).First() as ExtendedStatusBar;
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

        #region TimeConsumingTask/BackgroundWorker EventHandlers and Properties

        private BackgroundWorker TimeConsumingBackgroundWorker
        {
            get;
            set;
        }

        public void RunTimeConsumingOperation(TimeConsumingOperation timeConsumingOperationForExecute)
        {
            using (this.TimeConsumingBackgroundWorker = new BackgroundWorker())
            {
                this.TimeConsumingBackgroundWorker.DoWork += TimeConsumingBackgroundWorker_DoWork;
                this.TimeConsumingBackgroundWorker.RunWorkerCompleted += TimeConsumingBackgroundWorker_RunWorkerCompleted;
                if (!this.TimeConsumingBackgroundWorker.IsBusy)
                {
                    this.TimeConsumingBackgroundWorker.RunWorkerAsync(timeConsumingOperationForExecute);
                }
            }
        }

        private void TimeConsumingBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string completionMessage = string.Format("{0} is called in {1}", MethodBase.GetCurrentMethod().Name, this.GetType().Name);

            TimeConsumingResult timeConsumingResult = e.Result as TimeConsumingResult;
            Action afterOperationAction = (e.Result as TimeConsumingResult).AfterOperationAction;

            if (timeConsumingResult.Error != null)
            {
                Exception occuredException = timeConsumingResult.Error;
                Logger.WriteInfo(completionMessage + " and has error.");
                Logger.Write(occuredException, "{0} in {1} has error.", MethodBase.GetCurrentMethod().Name, this.GetType().Name);
                MessageBox.Show(".اجرای عملیات با خطا مواجه شد", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                afterOperationAction.Invoke();
                return;
            }
            else
            {
                Logger.WriteInfo(completionMessage + " without error.");
                if (afterOperationAction != null)
                {
                    afterOperationAction.Invoke();
                }
            }
        }

        private void TimeConsumingBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string typeName = this.GetType().Name;
            Logger.WriteInfo("{0} is called in {1}", MethodBase.GetCurrentMethod().Name, typeName);
            TimeConsumingResult result = new TimeConsumingResult();
            TimeConsumingOperation actions = e.Argument as TimeConsumingOperation;

            try
            {
                if (actions.DuringOperationAction != null)
                {
                    this.Dispatcher.BeginInvoke(new System.Windows.Forms.MethodInvoker(actions.DuringOperationAction));
                }

                actions.MainOperationAction.Invoke();
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "{0} in {1} has error ", MethodBase.GetCurrentMethod().Name, typeName);
                result.Error = ex;
            }
            finally
            {
                if (actions.AfterOperationAction != null)
                {
                    result.AfterOperationAction = actions.AfterOperationAction;
                }
                e.Result = result;
            }
        }

        #endregion
    }
}