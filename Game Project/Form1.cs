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
    public partial class Form1 : Form
    {
        //variables to be used
        bool goLeft, goRight, jumping, hasKey;

        int jumpSpeed = 8;
        int force = 6;
        int score = 0;

        int playerSpeed = 8;
        int backgroundspeed = 6;

        SoundPlayer sp = new SoundPlayer();
        SoundPlayer sp2 = new SoundPlayer();
        SoundPlayer sp3 = new SoundPlayer();

        public Form1()
        {
            InitializeComponent();
            sp.SoundLocation = "C:/Users/HP/Desktop/Game Project/Game Project/bin/Debug/bgsounds.wav"; //Sound Location
            sp.Play();
        }

        //Executing all functions and rules and the display
        private void MainTimerEvent(object sender, EventArgs e)
        {

            
            txtScore.Text = "Score:" + score;
            txtLevel.Text = "LEVEL 1";
            player.Top += jumpSpeed;

            if (goLeft==true && player.Left > 50)
            {
                player.Left -= playerSpeed;
                
            }
            if (goRight == true && player.Left + (player.Width + 50) < this.ClientSize.Width)
            {
                player.Left += playerSpeed;
            }

            if (goLeft == true && background.Left < 0 )
            {
                background.Left += backgroundspeed;
                MoveGameElements("forward");
            }
            if (goRight == true && background.Left > -965)
            {
                background.Left -= backgroundspeed;
                MoveGameElements("back");
            }

            if (jumping == true)
            {
                jumpSpeed = -10;
                force -= 1;
            }
            else
            {
                jumpSpeed = 10;
            }

            if (jumping == true && force < 0)
            {
                jumping = false;
                
            }
            

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "platform")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds) && jumping == false)
                    {
                        force = 6;
                        player.Top = x.Top - player.Height;
                        jumpSpeed = 0;
                    }
                    x.BringToFront();

                }
                if (x is PictureBox && (string)x.Tag == "coin")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                    {
                        x.Visible = false;
                        score += 1;
                    }

                }
            }
            if (player.Bounds.IntersectsWith(key.Bounds))
            {
                key.Visible = false;
                hasKey = true;  
            }

            if (player.Bounds.IntersectsWith(door.Bounds) && hasKey == true && score >= 20)
            {
                door.Image = Properties.Resources.door_open;
                GameTimer.Stop();
                sp3.SoundLocation = "C:/Users/HP/Desktop/Game Project/Game Project/bin/Debug/unlock.wav";
                sp3.Play();
                MessageBox.Show("Well Done!! Level Complete!" + Environment.NewLine + "Click OK to play next Level!");
                Form3 form = new Form3();
                form.Show();
                this.Hide();
            }
            //if (player.Bounds.IntersectsWith(door.Bounds) && hasKey == true && score <= 20)
            //{
            //    //door.Image = Properties.Resources.door_open;
            //    GameTimer.Stop();
            //    //sp3.SoundLocation = "C:/Users/HP/Desktop/Game Project/Game Project/bin/Debug/unlock.wav";
            //    //sp3.Play();
            //    //MessageBox.Show("Well Done!! Level Complete!" + Environment.NewLine + "Click OK to play next Level!");
            //    MessageBox.Show("Please collect enough coins!!");
            //    GameTimer.Start();
                

            //}


            if (player.Top + player.Height > this.ClientSize.Height)
            {
                sp2.SoundLocation = "C:/Users/HP/Desktop/Game Project/Game Project/bin/Debug/diying.wav";
                sp2.Play();
                GameTimer.Stop();
                MessageBox.Show("Ooops! You Died!!" + Environment.NewLine + "Click OK to play again!");
                RestartGame();
            }


        }

        //Activating all keys
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.Space && jumping == false)
            {
                jumping = true;
                
            }
            
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (e.KeyCode == Keys.Space && jumping == true)
            {
                jumping = false;
                
            }
        }
        //When closing the game, the debug to stop as well
        private void CloseGame(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        //Restarting the game
        private void RestartGame()
        {
            Form1 newWindow = new Form1();
            newWindow.Show();
            this.Hide();
        }

        //Giving all elements of the system motions or movements while playing and directions
        private void MoveGameElements(string direction)
        {

            foreach(Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "platform" || x is PictureBox && (string)x.Tag == "coin" || x is PictureBox && (string)x.Tag == "key" || x is PictureBox && (string)x.Tag == "door")
                {
                    if(direction == "back")
                    {
                        x.Left -= backgroundspeed;
                    }
                    if (direction == "forward")
                    {
                        x.Left += backgroundspeed;
                    }
                }
            }

        }

    }
}
