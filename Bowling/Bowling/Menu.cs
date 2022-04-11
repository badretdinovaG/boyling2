using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bowling
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void обАвтореToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string caption = "Об авторе";
            MessageBox.Show("Работу выполнила студентка группы 3043 " + "\nБадретдинова Гузель" + "\nbadretguzel@icloud.com", caption);
        }

        private void правилаИгрыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string caption = "Правила";        
            MessageBox.Show("Игра представляет собой игру в Spare. То есть игроку дается только один бросок, чтобы сбить 10 кеглей. Игрок использует хаусбол, чтобы прицелиться для броска по кеглям. Требуется выбить как можно больше кеглей. Если сбиты все кегли, то это страйк!", caption);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            start_Bowling();
        }

        private void start_Bowling()
        {
            Кегли gameform = new Кегли();
            gameform.Show();

            DateTime currentUpdateTime;
            DateTime lastUpdateTime;
            TimeSpan frameTime;

            currentUpdateTime = DateTime.Now;
            lastUpdateTime = DateTime.Now;

            while (gameform.Created == true)
            {
                currentUpdateTime = DateTime.Now;
                frameTime = currentUpdateTime - lastUpdateTime;
                if (frameTime.TotalMilliseconds > 20)
                {
                    Application.DoEvents();
                    gameform.UpdateWorld();
                    gameform.Refresh();
                    lastUpdateTime = DateTime.Now;
                }
            }
        }
    }
}
