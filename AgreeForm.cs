using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DegreeWork_01
{
    public partial class AgreeForm : Form
    {
        Remind remind;
        bool isDeleted = false;
        public AgreeForm(string textToShow, Remind remind, bool isDeleted)
        {
            InitializeComponent();
            this.remind = remind;
            this.isDeleted = isDeleted;
            textLabel.Text = textToShow;

            this.KeyDown += Form5_SpaceDown;
            this.KeyDown += Form5_EnterDown;
        }

        private void Form5_SpaceDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Space) return;
            this.Close();
        }

        private void Form5_EnterDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            Engine engine = new Engine();
            if (isDeleted) engine.deleteRemind(remind);
            else engine.addRemind(remind);
            this.Close();
        }


    }
}
