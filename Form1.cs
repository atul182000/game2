using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game2
{
    public partial class Form1 : Form
    {

        public class RussainRoulette
        {
            //global variable declaration
            bool enableDisable;
            bool enableDisable1;
            public int lifesnum = 2;
            public int[] gunArray = new int[5] { 0, 0, 0, 0, 0 };
            public int save;
            public int hit;

            //class method

            public int hitBybullet()
            {
                return hit;
            }

            public int saveBybullet()
            {
                return save;
            }

            public bool FireButtonAvailable()
            {

                return enableDisable;           //enable/disable button function.

            }

            public bool PlayAgainEnabledFalse()
            {

                return enableDisable1;          //enable/disable button function.

            }

            public bool BulletLoadedInGun(Label label6)    //accessing form properties.
            {

                label6.Enabled = true;
                return true;

            }

            public void loadBullet()                        //bullet load into gun function.
            {

                Array.Resize(ref gunArray, gunArray.Length + 1);
                gunArray[5] = 1;

            }

            public int spinChamber()                        //Gun chambers spin Function.
            {

                Random rand = new Random();
                int randomIndex = rand.Next(0, gunArray.Length);
                int randomNumber = gunArray[randomIndex];
                return randomNumber;

            }

            public int fireGun(int randNumber)              //this function fires the bullet
            {

                if (randNumber == gunArray[5])              //In this check we are checking that bullet hits or not
                {

                    System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"C:\Users\atul1\source\repos\game2\atul\awm.wav");
                    player.Play();
                    string message = "Do you Want to Revive and Play Again!!!!";
                    string title = "You are Dead";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result = MessageBox.Show(message, title, buttons);

                    if (result == DialogResult.Yes && lifesnum > 0 && lifesnum < 3)     // In this check we are checking whether person  psses away the shot or want to be revive:)
                    {

                        hit++;
                        lifesnum--;                     //2 life is given too any body who is playing.
                        enableDisable = true;
                        FireButtonAvailable();
                        spinChamber();

                        return 1;

                    }
                    else
                    {

                        enableDisable = false;
                        enableDisable1 = true;
                        PlayAgainEnabledFalse();        // In else part if life is used person is dead in shot and game become over
                        FireButtonAvailable();
                        message = "No Life Left :(";
                        title = "You Are Dead!!";
                        MessageBox.Show(message, title);

                        return 0;
                    }

                }
                else
                {

                    save++;
                    System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"C:\Users\atul1\source\repos\game2\atul\GunEmpty.wav");
                    player.Play();
                    enableDisable = true;
                    enableDisable1 = false;             //In this else if bullet is not fired then random function will fire again.
                    PlayAgainEnabledFalse();
                    FireButtonAvailable();
                    spinChamber();

                    return 0;
                }
            }
        }
        public Form1()
        {
            InitializeComponent();

            loadBullet.Enabled = true;
            spinChambers.Enabled = false;
            fireGun.Enabled = false;
            playAgain.Enabled = false;                  //Initial running component.
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;

        }

        RussainRoulette robj = new RussainRoulette();
        int passVariable;

        private void loadBullet_Click(object sender, EventArgs e)
        {

            robj.loadBullet();
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"C:\Users\atul1\source\repos\game2\atul\GunEmpty.wav");
            player.Play();
            loadBullet.Enabled = false;                     // this function add bullet in gun.
            spinChambers.Enabled = true;
            bool a = robj.BulletLoadedInGun(label6);
            label6.Visible = a;

        }

        private void spinChambers_Click(object sender, EventArgs e)
        {

            passVariable = robj.spinChamber();
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"C:\Users\atul1\source\repos\game2\atul\mag.wav");
            player.Play();
            spinChambers.Enabled = false;                                   // this function will take random number from the gun
            fireGun.Enabled = true;
            label6.Visible = false;
            bool a = robj.BulletLoadedInGun(label7);
            label7.Visible = a;

        }

        private void fireGun_Click(object sender, EventArgs e)
        {

            passVariable = robj.spinChamber();
            robj.fireGun(passVariable);                         //pass random value to fireGun function
            fireGun.Enabled = false;
            bool enableVar = robj.PlayAgainEnabledFalse();      //play again button will disable 
            playAgain.Enabled = enableVar;
            label7.Visible = false;
            bool enableVariable = robj.FireButtonAvailable();   //fire button will enable
            fireGun.Enabled = enableVariable;
            bool a = robj.BulletLoadedInGun(label7);          //enable label7
            label8.Visible = a;
            int b = robj.hitBybullet();
            label10.Text = b.ToString();
            int c = robj.saveBybullet();
            label9.Text = c.ToString();

        }

        private void playAgain_Click(object sender, EventArgs e)
        {

            System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"C:\Users\atul1\source\repos\game2\atul\GameOver.wav");
            player.Play();
            string message = "Game Is Over";
            string title = "Russian Roulette";
            MessageBox.Show(message, title);
            spinChambers.Enabled = false;                       //all buttons will become disable.
            playAgain.Enabled = false;                          //all buttons will become disable.     
            loadBullet.Enabled = true;                         //all buttons will become disable.    
            label8.Visible = false;                             //all label will be false.
            robj.lifesnum = 2;                                  //this will reset game life.
            robj.gunArray = new int[5] { 0, 0, 0, 0, 0 };       //this will reset as gun Empty.
            robj.save = 0;
            robj.hit = 0;
            label9.Text = "0";
            label10.Text = "0";
        }
    }
}
