using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Enterprise;

namespace CRM.Application.UserControls
{
    public partial class Pager : UserControl
    {
        #region Properties

        public int PageSize { get; set; }

        public int TotalRecords
        {
            get { return int.Parse(TotalRecordsTextBox.Text.Replace(",", "")); }
            set
            {
                TotalRecordsTextBox.Text = value.ToString("#,0");
                TotalPagesTextBox.Text = ((TotalRecords % PageSize == 0) ? TotalRecords / PageSize : (TotalRecords / PageSize) + 1).ToString();
            }
        }

        public int CurrentPage
        {
            get
            {
                int res;
                if (int.TryParse(CurrentPageNumberTextBox.Text, out res))
                    return int.Parse(CurrentPageNumberTextBox.Text);
                else
                {
                    CurrentPageNumberTextBox.Text = "1";
                    return 1;
                }
            }
            set
            {
                if (TotalPages == 0)
                    CurrentPageNumberTextBox.Text = "0";

                else if (value > TotalPages)
                    CurrentPageNumberTextBox.Text = TotalPages.ToString();

                else if (value < 1)
                    CurrentPageNumberTextBox.Text = "1";

                else
                    CurrentPageNumberTextBox.Text = value.ToString();
            }
        }

        public int TotalPages
        {
            get { return int.Parse(TotalPagesTextBox.Text); }
        }

        public int StartRowIndex
        {
            get
            {
                if (CurrentPage == 0)
                    return 0;
                else
                    return (CurrentPage - 1) * PageSize;
            }
        }

        #endregion

        #region Constructors

        public Pager()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        public delegate void PageChange(int pageNumber);
        public event PageChange PageChanged;

        public void OnPageChanged(int pageNumber)
        {
            if (PageChanged != null) PageChanged(pageNumber);
        }

        #endregion

        #region Event Handlers

        private void ChangePage(object sender, System.Windows.RoutedEventArgs e)
        {
            int val = Convert.ToInt32((sender as MenuItem).CommandParameter);
            CurrentPage = CurrentPage + val;
        }

        private void CurrentPageNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            OnPageChanged(CurrentPage);
        }

        #endregion
    }
}