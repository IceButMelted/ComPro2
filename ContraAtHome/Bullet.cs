using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;   
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;
using System.Diagnostics;

namespace ContraAtHome
{
    public class Bullet : PictureBox
    {
        public int BulletSpeed { get; set; }
        public string Direction { get; set; }
        int screenWidth;
        int screenHeight;
        private Timer bulletTimer;

        public Bullet(string tag, int speed, Point location, Color color, int ScreenWith, int ScreenHeight)
        {
            Tag = tag;
            BulletSpeed = speed;
            Size = new Size(10, 10);
            Location = location;
            BackColor = color;
            this.screenHeight = ScreenHeight;
            this.screenWidth = ScreenHeight;

            bulletTimer = new Timer { Interval = 16 }; // ~60 FPS
            bulletTimer.Tick += BulletTimer_Tick;
            bulletTimer.Start();
        }

        private void BulletTimer_Tick(object sender, EventArgs e)
        {
            MoveBullet();

            if (Left < 0 || Right > 800 || Top < 0 || Bottom > 600)
            {
                DisposeBullet();
                Debug.WriteLine("Bullet dispose");
            }
        }

        public void MoveBullet()
        {
            switch (Direction)
            {
                case "left":
                    Left -= BulletSpeed;
                    break;
                case "right":
                    Left += BulletSpeed;
                    break;
                case "up":
                    Top -= BulletSpeed;
                    break;
                case "down":
                    Top += BulletSpeed;
                    break;
            }
        }

        private void DisposeBullet()
        {
            bulletTimer.Stop();
            bulletTimer.Dispose();
            Parent?.Controls.Remove(this);
            Dispose();
        }
    }
}