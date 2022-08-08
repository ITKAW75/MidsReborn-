using System.Windows.Forms;

namespace Mids_Reborn.Core
{
    public partial class ZStatus : Form
    {
        private Label lblStatus1;
        private Label lblStatus2;
        private Label lblTitle;

        public ZStatus()
        {
            InitializeComponent();
        }

        public string StatusText1
        {
            set
            {
                if (value == lblStatus1.Text)
                    return;
                lblStatus1.Text = value;
                lblStatus1.Refresh();
            }
        }

        public string StatusText2
        {
            set
            {
                if (value == lblStatus2.Text)
                    return;
                lblStatus2.Text = value;
                lblStatus2.Refresh();
            }
        }
    }
}