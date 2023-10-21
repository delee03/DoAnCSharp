using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Garage_Management.BUS
{
    public class DataContext
    {
        private static ImageFormat GetRawFormat(PictureBox pic)
        {
            return pic.Image.RawFormat;
        }

        // Convert image to byte
        public byte[] ImageToByteArrary(PictureBox pic)
        {
            using(MemoryStream ms = new MemoryStream())
            {
                pic.Image.Save(ms, GetRawFormat(pic));
                return ms.ToArray();
            }
        }

        // Convert byte to image
        public Image ByteArrayToImage(byte[] bytes)
        {
            using(MemoryStream ms = new MemoryStream(bytes))
            {
                Image img = Image.FromStream(ms);
                return img;
            }
        }
    }
}
