using CRM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for ModifyMUIDForm.xaml
    /// </summary>
    public partial class ModifyMUIDForm : Local.PopupWindow
    {
        PCMCardInfo _PCMCardInfo = new PCMCardInfo();
        PCM pcm = new PCM();
        public ModifyMUIDForm()
        {
            InitializeComponent();
        }

        public ModifyMUIDForm(PCMCardInfo PCMCardInfo)
            : this()
        {
            _PCMCardInfo = PCMCardInfo;
        }

        private void LoadData()
        {

        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            try
            {
                if (pcm != null)
                {
                    using (TransactionScope ts = new TransactionScope())
                    {
                        PCMRock pcmRock = PCMRockDB.GetPCMRockByNumber(_PCMCardInfo.CenterID, Convert.ToInt32(RockTextBox.Text.Trim()));

                        if (pcmRock == null)
                        {
                            pcmRock = new PCMRock();
                            pcmRock.CenterID = _PCMCardInfo.CenterID;
                            pcmRock.Number = Convert.ToInt32(RockTextBox.Text.Trim());
                            pcmRock.Detach();
                            DB.Save(pcmRock);
                        }

                        PCMShelf pCMShelf = PCMShelfDB.GetPCMShelfByNumber(pcmRock.ID, Convert.ToInt32(ShelfTextBox.Text.Trim()));

                        if (pCMShelf == null)
                        {
                            pCMShelf = new PCMShelf();
                            pCMShelf.PCMRockID = pcmRock.ID;
                            pCMShelf.Number = Convert.ToInt32(ShelfTextBox.Text.Trim());
                            pCMShelf.Detach();
                            DB.Save(pCMShelf);
                        }


                        PCM PCM = PCMDB.GetPCMByNumber(pCMShelf.ID, Convert.ToInt32(PCMTextBox.Text.Trim()));
                        if (PCM == null)
                        {
                            pcm = PCMDB.GetPCMByID(_PCMCardInfo.ID);
                            pcm.ShelfID = pCMShelf.ID;
                            pcm.Card = Convert.ToInt32(PCMTextBox.Text.Trim());
                            pcm.Detach();
                            DB.Save(pcm);
                        }

                        ts.Complete();
                        ShowSuccessMessage("ذخیره اطلاعات انجام شد");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره اطلاعات", ex);
            }
        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
