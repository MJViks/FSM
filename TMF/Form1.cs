using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FSM
{
    public partial class Form1 : Form
    {
        private Point Enemy;
        private const int RunAwaySpeed = 3,
                            Speed = 2,
                            Visibility = 100;
        private List<Ant> Ants = new List<Ant>();

        public Form1()
        {
            InitializeComponent();
            Ant.Leafs = Ant.GetAllLeafs<CheckBox>(Controls);
        }
            //---------FORM ACTIONS---------

        private void Game() 
        {
            foreach (Ant ant in Ants) {
                ant.Brain.Update();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();

            CheckBox ch = new CheckBox();
            ch.AutoSize = true;
            ch.Location = new System.Drawing.Point(rnd.Next(20, Width), rnd.Next(20, Height));
            ch.Size = new System.Drawing.Size(15, 14);
            ch.TabIndex = 1;
            ch.UseVisualStyleBackColor = true;
            ch.MouseDown += new MouseEventHandler(this.ChLeaf_MouseDown);

            Controls.Add(ch);
            Ant.Leafs = Ant.GetAllLeafs<CheckBox>(Controls);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();

            Button btn = new Button();
            btn.Anchor = System.Windows.Forms.AnchorStyles.None;
            btn.Location = new System.Drawing.Point(rnd.Next(20, Width), rnd.Next(20, Height));
            btn.Size = new System.Drawing.Size(23, 23);
            btn.TabIndex = 4;
            btn.Text = "button1";
            btn.UseVisualStyleBackColor = true;
            btn.Click += new System.EventHandler(this.BtnAnt_Click);

            Controls.Add(btn);

            foreach (Control c in Controls) {
                if (c is CheckBox ch)//test
                    ch.Enabled = true;
            }
        }

        private void BtnAnt_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            Ants.Add(new Ant(btn, RunAwaySpeed, Speed, Visibility, rtbHome, this));
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Game();
        }
        private void ChLeaf_MouseDown(object sender, MouseEventArgs e)
        {
            CheckBox ch = sender as CheckBox;
            ch.DoDragDrop(sender, DragDropEffects.None);
            ch.Location = new Point(MousePosition.X - (chLeaf.Width / 2), MousePosition.Y - (chLeaf.Height * 2));
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            Enemy.X = e.X;
            Enemy.Y = e.Y;
            Ant.Enemy = new Point(e.X, e.Y);
        }
    }
}
