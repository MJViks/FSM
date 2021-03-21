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
        private FSM Fsm1, Fsm2, Fsm3;

        private Point Enemy;
        private Control[] Leafs;
        private const int RunAwaySpeed = 3,
                            Speed = 2,
                            Visibility = 100;
        private Dictionary<FSM, Ant> Ants = new Dictionary<FSM, Ant>();

        public Form1()
        {
            InitializeComponent();
            Fsm1 = new FSM();
            Fsm2 = new FSM();
            Fsm3 = new FSM();
            Leafs = Ant.GetAllLeafs<CheckBox>(Controls);
            Ants.Add(Fsm1,new Ant(button1, RunAwaySpeed, Speed, Visibility, rtbHome, this));
            Ants.Add(Fsm2, new Ant(btnAnt, RunAwaySpeed, Speed, Visibility, rtbHome, this));
            Ants.Add(Fsm3, new Ant(button2, RunAwaySpeed, Speed, Visibility, rtbHome, this));
        }
            //---------FORM ACTIONS---------

            private void Game() 
            {
            foreach (KeyValuePair<FSM, Ant> ant in Ants)
            {
                if (ant.Value.IsFear)
                {
                    ant.Key.PushState(() => { ant.Value.RunAway(Enemy); });
                    continue;
                }

                if (ant.Value.TargetLeaf == null)
                {
                    ant.Key.PushState(() => { ant.Value.SerchLeaf(Leafs, Enemy); });
                    continue;
                }
                else
                {
                    ant.Key.PushState(() => { ant.Value.GoHomeWithLeaf(); });
                    continue;
                }
            }

            }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            CheckBox ch = new CheckBox();
            ch.AutoSize = true;
            ch.Location = new System.Drawing.Point(233, 148);
            ch.Size = new System.Drawing.Size(15, 14);
            ch.TabIndex = 1;
            ch.UseVisualStyleBackColor = true;
            ch.MouseDown += new MouseEventHandler(this.ChLeaf_MouseDown);

            Controls.Add(ch);
            Array.Resize(ref Leafs, Leafs.Length + 1);
            Leafs[Leafs.Length - 1] = ch;
        }

        private void BtnAnt_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Game();
            foreach (KeyValuePair<FSM, Ant> ant in Ants)
            {
                ant.Key.Update();
            }
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
        }
    }
}
