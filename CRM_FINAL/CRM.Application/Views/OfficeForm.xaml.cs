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
	public partial class OfficeForm : Local.PopupWindow
	{
		#region Properties

		private int _ID = 0;

		#endregion

		#region Constructors

		public OfficeForm()
		{
			InitializeComponent();
			Initialize();
		}

		public OfficeForm(int id)
			: this()
		{
			_ID = id;
		}

		#endregion

		#region Methods

		private void Initialize()
		{
			CityComboBox.ItemsSource = Data.CityDB.GetCitiesCheckable();
		}

		private void LoadData()
		{
			Office office = new Office();

			if (_ID == 0)
				SaveButton.Content = "ذخیره";
			else
			{
				office = Data.OfficeDB.GetOfficeByID(_ID);
				SaveButton.Content = "بروز رسانی";
			}

			this.DataContext = office;
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
				Office office = this.DataContext as Office;

				office.Detach();
				Save(office);

				ShowSuccessMessage("ذخیره دفتر خدماتی انجام شد");

				this.DialogResult = true;
			}
			catch (Exception ex)
			{
                if (ex.Message.Contains("Cannot insert duplicate key row in object"))
                    ShowErrorMessage("مقادیر وارد شده در پایگاه داده وجود دارد", ex);
                else
			     	ShowErrorMessage("خطا در ذخیره دفتر خدماتی", ex);
			}
		}

		#endregion
	}
}
