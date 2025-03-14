using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;   
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace ContraAtHome
{
    class Bullet : PictureBox
    {
        public string direction;
        public int bulletLeft;
        public int bulletTop;

        private int bulletSpeed = 20;
        private PictureBox bullet = new PictureBox();
        private Timer bulletTimer = new Timer();

        public void MakeBullet(Form form)
        {
            bullet.BackColor = Color.Black;
            bullet.Size = new Size(10, 10);
            bullet.Tag = "bullet";
            bullet.Left = bulletLeft;
            bullet.Top = bulletTop;


            form.Controls.Add(bullet);

            bullet.BringToFront();

            bulletTimer.Interval = bulletSpeed;
            bulletTimer.Tick += new EventHandler(BulletTimerEvent);
            bulletTimer.Start();

        }

        public void MakeEnemyBullet(Form f)
        {
            bullet.BackColor = Color.DarkRed;
            bullet.Size = new Size(10, 10);
            bullet.Left = bulletLeft;
            bullet.Top = bulletTop;
            bullet.Tag = "EnemyBullet";
            bullet.Name = "bullet";

            f.Controls.Add(bullet);
            bullet.BringToFront();

            bulletTimer.Interval = bulletSpeed;
            bulletTimer.Tick += new EventHandler(BulletTimerEvent);
            bulletTimer.Start();
        }

        private void BulletTimerEvent(object sender, EventArgs e)
        {
            if (direction == "left")
            {
                bullet.Left -= bulletSpeed;
            }
            if (direction == "right")
            {
                bullet.Left += bulletSpeed;
            }
            if (direction == "up")
            {
                bullet.Top -= bulletSpeed;
            }
            if (bullet.Left < 16 || bullet.Left > 860 || bullet.Top < 10 || bullet.Top > 616)
            {
                bulletTimer.Stop();
                bulletTimer.Dispose();
                bullet.Dispose();
                bulletTimer = null;
                bullet = null;
            }
        }

    }



}
