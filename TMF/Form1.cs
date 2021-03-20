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
        private FSM Fsm;

        private Point Target;
        private const int visibility = 100;
        private CheckBox[] Leafs;
        private bool withLeaf;
        public Form1()
        {
            InitializeComponent();
            Fsm = new FSM();
            Leafs = getAllLeafs(Controls);
            withLeaf = false;
        }

        private CheckBox[] getAllLeafs(Control.ControlCollection controls) {
            CheckBox[] answer = new CheckBox[0];
            foreach (Control value in controls)
            {
                CheckBox CurrentLeaf = value as CheckBox;
                if (CurrentLeaf != null)
                {
                    Array.Resize(ref answer, answer.Length + 1);  //Изменение размера массива (Ссылка на массив, новая длинна)
                    answer[answer.GetUpperBound(0)] = CurrentLeaf;
                }
            }
            return answer;
        }

        private void chLeaf_MouseDown(object sender, MouseEventArgs e)
        {
            chLeaf.DoDragDrop(sender, DragDropEffects.None);
            chLeaf.Location = new Point(MousePosition.X - (chLeaf.Width / 2), MousePosition.Y - (chLeaf.Height * 2));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Fsm.Update();
        }

        private void setRandomTarget(Button ant, Point target) {
            Random rnd = new Random();
            int rndX = rnd.Next(ant.Location.X - visibility, ant.Location.X + visibility);
            int rndY = rnd.Next(ant.Location.Y - visibility, ant.Location.Y + visibility);
            int rndXRight = (rndX % 2) > 0 ? rndX + 1 : rndX;
            int rndYRight = (rndY % 2) > 0 ? rndY + 1 : rndY;
            Target = new Point(rndXRight, rndYRight);
        }
        private bool isGoOneStepToTarget(Button ant, Point target, int speed) {
            this.Text = target.X.ToString() + ' ' + target.Y.ToString() + '|' + ant.Location.X.ToString() + ' ' + ant.Location.Y.ToString();
            int X = target.X;
            int Y = target.Y;
            if ((X == ant.Location.X) && (Y == ant.Location.Y))
                return false;
            int absX = Math.Abs(X - ant.Location.X);
            int absY = Math.Abs(Y - ant.Location.Y);
            int wayDirectionX = absX == 0 ? 0 : (X - ant.Location.X) / absX;
            int wayDirectionY = absY == 0 ? 0 : (Y - ant.Location.Y) / absY;
            ant.Location = new Point(ant.Location.X + (wayDirectionX * speed), ant.Location.Y + (wayDirectionY * speed));
            return true;
        }

        private CheckBox FoundLeaf(Button ant, CheckBox[] leafs) { 
            foreach (CheckBox leaf in leafs) {
                //Расстояние между точками: AB = sqrt((x1 - x2)^2 - (y1 - y2)^2)

                int xAntCentre = ant.Location.X + (ant.Width / 2);      //x Муровья
                int yAntCentre = ant.Location.Y + (ant.Height / 2);     //y Муровья
                int xLeafCentre = leaf.Location.X + (leaf.Width / 2);   //x Листа
                int yLeafCentre = leaf.Location.Y + (leaf.Height / 2);  //y Листа
                double A = Math.Pow(xAntCentre - xLeafCentre, 2);       //Квадрат разности A
                double B = Math.Pow(yAntCentre - yLeafCentre, 2);       //Квадрат разности B
                double Distance = Math.Sqrt(A + B);                     //Корень

                //Если расстояние между точками меньше радиуса, то лист найден
                if (Distance < visibility)
                    return leaf;

                //Отрисовка радиуса
                //Pen pen = new Pen(Brushes.Red);
                //Graphics krug = this.CreateGraphics();
                //krug.DrawEllipse(pen, xAntCentre - visibility, yAntCentre - visibility, visibility * 2, visibility * 2);
            }

            return null;
        }

        private void RunAway(Button ant)
        {
            if (!isGoOneStepToTarget(ant, Target, 2))
                setRandomTarget(ant, Target);
        }

        private void Serch(Button ant) {
            if (!isGoOneStepToTarget(ant, Target, 1))
                setRandomTarget(ant, Target);
        }

        private void SerchLeaf(Button ant)
        {
            CheckBox foundLeaf = FoundLeaf(ant, Leafs);
            if (foundLeaf == null)
                Serch(ant);
            else {
                
                if (!isGoOneStepToTarget(ant, Target, 1))
                    Target = new Point(foundLeaf.Location.X - (foundLeaf.Width /2), foundLeaf.Location.Y - (foundLeaf.Height / 2));

            }
        }

        private void btnAnt_Click(object sender, EventArgs e)
        {
            setRandomTarget(btnAnt, Target);
            CheckBox[] Leafs = new CheckBox[1];
            Leafs[0] = chLeaf;
            //isFoundLeaf(btnAnt, Leafs);
            Fsm.pushState(() => { SerchLeaf(btnAnt); });
        }
    }
}
