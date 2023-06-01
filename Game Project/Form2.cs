using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Game_Project
{
    public partial class Form2 : Form
    {
        //variables to be used
        SoundPlayer sp=new SoundPlayer();
        public Form2()
        {
            InitializeComponent();
            sp.SoundLocation = "C:/Users/HP/Desktop/Game Project/Game Project/bin/Debug/bgsounds.wav"; //backgound song location
            sp.Play();
        }

        //a play button
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Collect minimum of 20 coins each Level!!");

            Form1 form = new Form1();
            form.Show();
            this.Hide();
            
        }
        
        //help button
        private void button2_Click(object sender, EventArgs e)
        {
            Form5 form = new Form5();
            form.Show();
            this.Hide();
        }
    }
}
