using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calendar
{
    public partial class Notification : Form
    {
        #region Constructor
        public Notification(string desc, bool snooze)
        {
            InitializeComponent();
            Description = desc;
            CanSnooze = snooze;
        }
        #endregion 

        #region Properties
        public string Description { get; set; }

        public bool CanSnooze { get; set; }
        #endregion

        #region events
        private void Notification_Load(object sender, EventArgs e)
        {
            apt_Label.Text = Description;
            snooze_Button.Enabled = CanSnooze;
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.Alarm01);
            player.Play();
        }

        private void snooze_Button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void dismiss_Button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        #endregion
    }
}
