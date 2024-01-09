using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WIA;

namespace CRM.Data
{
    public class Scanner
    {

        Device oDevice;
        WIA.CommonDialog dlg;
        public Scanner()
        {
            try
            {
                dlg = new WIA.CommonDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Erroe in  Constructor./n" + ex.Message);
            }
        }
        public WIA.ImageFile Scann()
        {
            try
            {
                WIA.ImageFile image = dlg.ShowAcquireImage(WiaDeviceType.ScannerDeviceType
                                                       , WiaImageIntent.UnspecifiedIntent
                                                       , WiaImageBias.MaximizeQuality
                                                       , "{00000000-0000-0000-0000-000000000000}"
                                                       , true
                                                       , true
                                                       , false);
                return image;

            }
            catch (Exception ex)
            {
                MessageBox.Show("ارتباط با اسکنر برقرار نشد./n" + ex.Message);

                return null;
            }
        }

        public byte[] ScannWithExtension(out string extension)
        {
            try
            {

                extension = string.Empty;
                byte[] imageByte;
                ImageFile image = dlg.ShowAcquireImage(WiaDeviceType.ScannerDeviceType
                                                       , WiaImageIntent.UnspecifiedIntent
                                                       , WiaImageBias.MinimizeSize
                                                       , "{B96B3CAE-0728-11D3-9D7B-0000F81EF32E}"
                                                       , true
                                                       , true
                                                       , false);

                if (image.FormatID != "{B96B3CAE-0728-11D3-9D7B-0000F81EF32E}")
                {
                    MessageBox.Show("اسکنر فرمت jpg  را پشتیبانی نمی کند.");
                    return null;
                }

                extension = "." + image.FileExtension.Trim();
                WIA.Vector vector = image.FileData;
                imageByte =  (byte[])vector.get_BinaryData();

                return imageByte;
                    

            }
            catch (Exception ex)
            {
                MessageBox.Show(".ارتباط با اسکنر برقرار نشد");
                extension = string.Empty;
                return null;
            }
        }

    }
}
