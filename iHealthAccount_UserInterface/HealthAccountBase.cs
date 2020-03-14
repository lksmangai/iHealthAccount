﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace iHealthAccount.UI
{
    public class HealthAccountBase : Form 
    {
        protected void SetBGColor(Form form)
        {
            form.BackColor = BGColor;
        }

        protected Color BGColor
        {
            get
            {
                return Color.FromArgb(122, 150, 223);
            }
        }
       
    }
}
