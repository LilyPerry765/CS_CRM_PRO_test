using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Application.UserControls;
using System.Windows;
using CRM.Data;
using Folder.Printing;
using System.Windows.Controls;


namespace CRM.Application.Local
{
	public class RequestFormBase : Local.PopupWindow
	{
		#region Properties

		private Data.Schema.ActionLogRequest actionLogRequest = new Data.Schema.ActionLogRequest();

		public static UserControls.Actions _Action;
		public long RequestID = 0;
        public List<long> RequestIDs { get; set; }  
		public bool IsSaveSuccess = false;
		public bool IsDeleteSuccess = false;
		public bool IsForwardSuccess = false;
        public bool IsRefundSuccess = false;
		public bool IsConfirmEndSuccess = false;
		public bool IsCancelSuccess = false;
		public bool IsConfirmSuccess = false;
		public bool IsRejectSuccess = false;
		public bool IsSaveWatingListSuccess = false;
		public bool IsExitWatingListSuccess = false;
		public bool IsSaveBlackListSuccess = false;
		public bool IsExitBlackListSuccess = false;
		public bool IsPrintSuccess = false;
        public bool IsKickedBackSuccess = false;
        
		public List<byte> ActionIDs { get; set; }

		#endregion

		#region Constructors

		public RequestFormBase()
		{
			this.Loaded += new RoutedEventHandler(RequestFormBase_Loaded);
            this.ContentRendered += new EventHandler(RequestFormBase_ContentRendered);
			Initialize();
		}





		#endregion

		#region Methods

		private void Initialize()
		{
            if (DB.CurrentUser.ID == 0)
                throw new Exception("خطا در ورود کاربر");

			_Action = new UserControls.Actions();
			_Action.Name = "ActionUserControl";
			ActionIDs = Helper.GetEnumItem(typeof(DB.NewAction)).Select(t => t.ID).ToList();
		}

		private void CreateAction()
		{
			UserControls.FormTemplate _formTemp = new UserControls.FormTemplate();
			PopupWindow._FormTemplate.Footer.Children.Add(_Action);
		}

		#endregion

		#region Event Handlers

		private void RequestFormBase_Loaded(object sender, RoutedEventArgs e)
		{
             if (DB.CurrentUser.ID != 0)
            {
			CreateAction();

			if (this.Title != "")
				AddActionLog();
            }
                        else
                        {
                            this.Visibility = Visibility.Collapsed;
                        }

		}
        private void RequestFormBase_ContentRendered(object sender, EventArgs e)
        {

            CRM.Application.Codes.Print.CheckingGridColumn(this.Content, this.GetType().Name);
            //DataGridColumnConfig dataGridColumnConfig = DB.CurrentUser.DataGridColumnConfig.Where(t => t.FormName == this.GetType().Name).SingleOrDefault();
            //if (dataGridColumnConfig != null)
            //{
            //    DataGrid dataGrid = Helper.GetLogicalChildCollection<DataGrid>(container).Where(t => t.Name == dataGridColumnConfig.DataGridName).Take(1).SingleOrDefault();
            //    if (dataGrid != null)
            //    {
            //        CRM.Data.Schema.DataGridColumns columns = LogSchemaUtility.Deserialize<CRM.Data.Schema.DataGridColumns>(dataGridColumnConfig.SelectedColumns.ToString());
            //        if (columns.Columns.Count() > 0)
            //        {

            //            System.Windows.Controls.Primitives.DataGridColumnHeadersPresenter presenter = null;

            //            System.Windows.Controls.Control sv = dataGrid.Template.FindName("DG_ScrollViewer", dataGrid) as System.Windows.Controls.Control;
            //            if (sv != null)
            //            {
            //                presenter = sv.Template.FindName("PART_ColumnHeadersPresenter", sv) as System.Windows.Controls.Primitives.DataGridColumnHeadersPresenter;

            //                if (presenter != null)
            //                {
            //                    System.Windows.Controls.Primitives.DataGridColumnHeader header = null;
            //                    header = (System.Windows.Controls.Primitives.DataGridColumnHeader)presenter.ItemContainerGenerator.ContainerFromIndex(1);

            //                    CheckBox CheckBox = Helper.FindVisualChildren<CheckBox>(header).SingleOrDefault();
            //                    CheckBox.IsChecked = true;

            //                }
            //            }
            //        }
            //    }
            //}
        }


		#endregion

		#region Virtual Methods

		public void AddActionLog()
		{
			actionLogRequest.FormType = this.GetType().FullName;
			actionLogRequest.FormName = this.Title;
			ActionLogDB.AddActionLog((byte)DB.ActionLog.View, DB.CurrentUser.UserName, actionLogRequest);
		}

		public virtual bool Save()
		{
			return IsSaveSuccess;
		}

		public virtual bool Delete()
		{
			return IsSaveSuccess;
		}

		public virtual bool Confirm()
		{
			return IsConfirmSuccess;
		}

		public virtual bool Deny()
		{
			return IsRejectSuccess;
		}

		public virtual bool Print()
		{
			return IsPrintSuccess;
		}

		public virtual bool Forward()
		{
			return IsForwardSuccess;
		}

        public virtual bool Refund()
        {
            return IsRefundSuccess;
        }

		public virtual bool ConfirmEnd()
		{
			return IsConfirmEndSuccess;
		}

		public virtual bool Cancel()
		{
			return IsCancelSuccess;
		}

		public virtual bool SaveWaitingList()
		{
			return IsSaveWatingListSuccess;
		}

		public virtual bool ExitWaitingList()
		{
			return IsExitWatingListSuccess;
		}

		public virtual bool SaveBlackList()
		{
			return IsSaveBlackListSuccess;
		}

		public virtual bool ExitBlackList()
		{
			return IsExitBlackListSuccess;
		}
        public virtual bool KickedBack()
        {
            return IsKickedBackSuccess;
        }

		public void SaveAction()
		{
		}
        public virtual bool PrintFormForWiring()
        {
            return IsSaveSuccess;
        }
		#endregion
	}
}
