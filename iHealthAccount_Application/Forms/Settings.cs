using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using iHealthAccount.Application.Properties;
using iHealthAccount.BusinessLogic;
using iHealthAccount.Messaging;
using iHealthAccount.Configurations;
using iHealthAccount.DataAccess;
using iHealthAccount.Formatting;

namespace iHealthAccount.Application.Forms
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            InitializeIcon();
        }

        private void InitializeIcon()
        {
            this.Icon = Icon.FromHandle(Resources.address_book_alt_icon.GetHicon());
        }

        private void checkBoxWriteTrace_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxWriteLog_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
