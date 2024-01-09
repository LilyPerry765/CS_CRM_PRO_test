using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CRM.Application.Views.Validation
{
	public partial class SetValidationDetailWindow : Window
	{
		public SetValidationDetailWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
            using (Data.MainDataContext entity = new Data.MainDataContext())
			{
				comRegx.ItemsSource = entity.RegularExpressions.ToList();
			}

            this.Resources.Clear();
		}

		private void btnClose_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
		}

		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}
	}
}
