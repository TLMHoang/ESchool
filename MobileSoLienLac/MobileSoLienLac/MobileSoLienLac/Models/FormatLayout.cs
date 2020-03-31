using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MobileSoLienLac.Models
{
    public class FormatLayout
    {
        public Color BackgroupColor { get; set; }
        public Color TextColor { get; set; }

        public FormatLayout(Color bgrc, Color txtc)
        {
            BackgroupColor = bgrc;
            TextColor = txtc;
        }

        public Color bgrColor(bool LayoutDark)
        {
            return LayoutDark ? Color.Gray : Color.White;
        }

        public Color txtColor(bool LayoutDark)
        {
            return LayoutDark ? Color.Gray : Color.White;
        }

    }
}
