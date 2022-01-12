using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T_Rex_run
{
    public partial class Form1 : Form
    {
        bool jumping = false;
        int jumpSpeed;
        int force = 12;
        int score = 0;
        int obstaclesSpeed = 10;
        Random rand = new Random();
        int position;
        bool IsGameOver = false;


        public Form1()
        {
            InitializeComponent();

            GameReset();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            trex.Top += jumpSpeed;

            txtscore.Text = "Score: " + score;

            if (jumping == true && force < 0)
            {
                jumping = false;
            }

            if (jumping == true)
            {
                jumpSpeed = -12;
                force -= 1;
            }
            else
            {
                jumpSpeed = 12;
            }

            if (trex.Top > 360 && jumping == false)
            {
                force = 12;
                trex.Top = 361;
                jumpSpeed = 0;
            }

            foreach(Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "obstacle")
                {
                    x.Left -= obstaclesSpeed;

                    if(x.Left < -100)
                    {
                        x.Left = this.ClientSize.Width + rand.Next(200, 500) + (x.Width * 15);
                        score++;
                    }

                    if (trex.Bounds.IntersectsWith(x.Bounds))
                    {
                        gameTimer.Stop();
                        trex.Image = Properties.Resources.dead;
                        txtscore.Text += "         Press Space to restart the game";
                        IsGameOver = true;

                    }
                }
            }

            if (score > 10)
            {
                obstaclesSpeed = 13;
            }

            if (score > 50)
            {
                obstaclesSpeed = 16;
            }

            if (score > 150)
            {
                obstaclesSpeed = 20;
            }

            if (score > 350)
            {
                obstaclesSpeed = 35;
            }
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && jumping == false)
            {
                jumping = true;
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (jumping == true)
            {
                jumping = false;
            }

            if (e.KeyCode == Keys.Space && IsGameOver == true)
            {
                GameReset();
            }
        }

        private void GameReset()
        {
            force = 12;
            jumpSpeed = 0;
            jumping = false;
            score = 0;
            obstaclesSpeed = 10;
            txtscore.Text = "Score" + score;
            trex.Image = Properties.Resources.running;
            IsGameOver = false;
            trex.Top = 361;

            foreach (Control x in this.Controls)
            {

                if (x is PictureBox && (string)x.Tag == "obstacle")
                {
                    position = this.ClientSize.Width + rand.Next(500, 800) + (x.Width * 10);

                    x.Left = position;
                }
            
            }

            gameTimer.Start();

        }
    }
}
