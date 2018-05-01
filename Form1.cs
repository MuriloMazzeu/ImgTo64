using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImgTo64
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnImage_Click(object sender, EventArgs e)
        {
            if(fileDialog.ShowDialog(this) == DialogResult.OK)
            {
                txtImagePath.Text = fileDialog.FileName;
                using (var image = Image.FromFile(fileDialog.FileName))
                {
                    using (var m = new MemoryStream())
                    {
                        var format = image.RawFormat;

                        image.Save(m, format);
                        var imageBytes = m.ToArray();
                        var base64String = Convert.ToBase64String(imageBytes);                        
                        var codec = ImageCodecInfo.GetImageDecoders().First(c => c.FormatID == format.Guid);

                        txtResult.Text = $"data:{codec.MimeType};base64,{base64String}";
                    }
                }
            }
        }

        private void txtResult_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtResult.Text);
            txtResult.SelectAll();
        }
    }
}
