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

        private Point Target, enemy;
        private CheckBox[] Leafs;
        private CheckBox targetLeaf = null;
        private bool isFear;
        private const int RunAwaySpeed = 2,
                            Speed = 1,
                            visibility = 100;

        public Form1()
        {
            InitializeComponent();
            Fsm = new FSM();
            Leafs = GetAllLeafs(Controls);
            isFear = false;
        }

        private CheckBox[] GetAllLeafs(Control.ControlCollection controls) {
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

        private void SetRandomTarget(Button ant, Point target) {
            Random rnd = new Random();
            int rndX = rnd.Next(ant.Location.X - visibility, ant.Location.X + visibility);
            int rndY = rnd.Next(ant.Location.Y - visibility, ant.Location.Y + visibility);
            int rndXRight = (rndX % 2) > 0 ? rndX + 1 : rndX;
            int rndYRight = (rndY % 2) > 0 ? rndY + 1 : rndY;

            if (rndXRight > Width || rndYRight > Height || rndXRight < 0 || rndYRight < 0)
                return;
            Target = new Point(rndXRight, rndYRight);
        }

        private bool InTarget(Button ant, Point target) {
            int X = target.X;
            int Y = target.Y;
            if ((X == ant.Location.X) && (Y == ant.Location.Y))
                return true;
            return false;
        }
        private void GoOneStepToTarget(Button ant, Point target, int speed, bool isFear) {
            int X = target.X;
            int Y = target.Y;
            if ((X == ant.Location.X) && (Y == ant.Location.Y))
                return;
            int absX = Math.Abs(X - ant.Location.X);
            int absY = Math.Abs(Y - ant.Location.Y);
            
            int wayDirectionX = absX == 0 ? 0 : (X - ant.Location.X) / absX;
            int wayDirectionY = absY == 0 ? 0 : (Y - ant.Location.Y) / absY;

            if (isFear) {
                wayDirectionX *= -1;
                wayDirectionY *= -1;
            }
                
            ant.Location = new Point(ant.Location.X + (wayDirectionX * speed), ant.Location.Y + (wayDirectionY * speed));
            return;
        }

        private CheckBox FoundLeaf(Button ant, CheckBox[] leafs) { 
            foreach (CheckBox leaf in leafs) {
                //Расстояние между точками: AB = sqrt((x1 - x2)^2 - (y1 - y2)^2)
                if (leaf.Checked)
                    continue;
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

        private void ChFear(Button ant, Point enemy) {
            //Расстояние между точками: AB = sqrt((x1 - x2)^2 - (y1 - y2)^2)

            int xAntCentre = ant.Location.X + (ant.Width / 2);      //x Муровья
            int yAntCentre = ant.Location.Y + (ant.Height / 2);     //y Муровья
            double A = Math.Pow(xAntCentre - enemy.X, 2);                   //Квадрат разности A
            double B = Math.Pow(yAntCentre - enemy.Y, 2);                   //Квадрат разности B
            double Distance = Math.Sqrt(A + B);                         //Корень

            //Если расстояние между точками меньше радиуса, то муровей напуган
            if (Distance < visibility) {
                isFear = true;
                Target = enemy;
                targetLeaf = null;
                return;
            }
            isFear = false;
        }

        private void RunAway(Button ant)
        {
            Text += "RunAway";
            ChFear(ant, enemy);
            GoOneStepToTarget(ant, Target, RunAwaySpeed, isFear);
            if (InTarget(ant, Target))
                SetRandomTarget(ant, Target);
        }

        private void Serch(Button ant) {
            GoOneStepToTarget(ant, Target, Speed, isFear);
            if (InTarget(ant, Target))
                SetRandomTarget(ant, Target);
        }

        private void SerchLeaf(Button ant)
        {
            ChFear(ant, enemy);
            Text += "SerchLeaf";
            CheckBox foundLeaf = FoundLeaf(ant, Leafs);
            if (foundLeaf == null)
                Serch(ant);
            else {
                GoOneStepToTarget(ant, Target, Speed, isFear);
                if (!InTarget(ant, Target))
                    Target = new Point(foundLeaf.Location.X - (foundLeaf.Width /2), foundLeaf.Location.Y - (foundLeaf.Height / 2));

                Point foundLeafCentre = new Point(foundLeaf.Location.X + (foundLeaf.Width / 2) , foundLeaf.Location.Y + (foundLeaf.Height / 2));
                if (DotInSqrt(ant.Location, ant.Width, ant.Height, foundLeafCentre, 0))
                    targetLeaf = foundLeaf;
            }
        }

        private bool DotInSqrt(Point sqrt, int width, int height, Point dot, int range) {
            int x1 = sqrt.X - range;
            int x2 = sqrt.X + width + range;
            int y1 = sqrt.Y - range;
            int y2 = sqrt.Y + height + range;
            if (dot.X > x1 && dot.Y > y1 && dot.X < x2 && dot.Y < y2)
                return true;
            return false;
        }

        private bool InHome(Button ant, RichTextBox home)
        {
            if (DotInSqrt(home.Location, home.Width, home.Height, ant.Location, -5))
                return true;
            return false;
        }

        private void GoHomeWithLeaf(Button ant, RichTextBox home, CheckBox leaf) {
            Text += "GoHomeWithLeaf";
            Point homeCentre = new Point(home.Location.X + (home.Width / 2), home.Location.Y + (home.Height / 2));
            GoOneStepToTarget(ant, homeCentre, Speed, isFear);
            if (InHome(ant, home))
            {
                SetRandomTarget(ant, Target);
                leaf.Checked = true;
                targetLeaf = null;
            }
            else
                leaf.Location = ant.Location;
        }

        //---------FORM ACTIONS---------

        private void Game() {
            if (isFear) {
                Fsm.PushState(() => { RunAway(btnAnt); });
                return;
            }

            if (targetLeaf == null)
            {
                Fsm.PushState(() => { SerchLeaf(btnAnt); });
                return;
            }
            else {
                Fsm.PushState(() => { GoHomeWithLeaf(btnAnt, rtbHome, targetLeaf); });
                return;
            }
        }
        private void BtnAnt_Click(object sender, EventArgs e)
        {
            SetRandomTarget(btnAnt, Target);
            CheckBox[] Leafs = new CheckBox[1];
            Leafs[0] = chLeaf;
            timer1.Enabled = true;
            //isFoundLeaf(btnAnt, Leafs);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Text = Target.X.ToString() + " " + Target.Y.ToString() + " | " + enemy.X.ToString() + " " + enemy.Y.ToString() + " | ";
            Game();
            Fsm.Update();
        }
        private void ChLeaf_MouseDown(object sender, MouseEventArgs e)
        {
            chLeaf.DoDragDrop(sender, DragDropEffects.None);
            chLeaf.Location = new Point(MousePosition.X - (chLeaf.Width / 2), MousePosition.Y - (chLeaf.Height * 2));
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            enemy.X = e.X;
            enemy.Y = e.Y;
        }
    }
}
