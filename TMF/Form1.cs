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
        }
            //---------FORM ACTIONS---------

        private void Game() 
        {
            foreach (KeyValuePair<FSM, Ant> ant in Ants)
            {
                ant.Key.popState();
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
            Random rnd = new Random();

            CheckBox ch = new CheckBox();
            ch.AutoSize = true;
            ch.Location = new System.Drawing.Point(rnd.Next(20, Width), rnd.Next(20, Height));
            ch.Size = new System.Drawing.Size(15, 14);
            ch.TabIndex = 1;
            ch.UseVisualStyleBackColor = true;
            ch.MouseDown += new MouseEventHandler(this.ChLeaf_MouseDown);

            Controls.Add(ch);
            Array.Resize(ref Leafs, Leafs.Length + 1);
            Leafs[Leafs.Length - 1] = ch;
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
        }

        private void BtnAnt_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            Ants.Add(new FSM(), new Ant(btn, RunAwaySpeed, Speed, Visibility, rtbHome, this));
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
