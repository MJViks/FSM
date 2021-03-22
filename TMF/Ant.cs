using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FSM
{
    class Ant
    {
        public Button ThisAnt;
        private readonly int RunAwaySpeed, Speed, Visibility;
        private Point Target;
        private Control Box, Home;
        public Control TargetLeaf;
        public bool IsFear;
        public string Status;
        public FSM Brain;
        public static Control[] Leafs;
        public static Point Enemy;
        public Ant(Button ant, int runAwaySpeed, int speed, int visibility, Control home, Control box) {
            ThisAnt = ant;
            RunAwaySpeed = runAwaySpeed;
            Speed = speed;
            Visibility = visibility;
            Box = box;
            IsFear = false;
            Home = home;
            Status = String.Empty;
            SetRandomTarget();
            Brain = new FSM();
            Brain.PushState(() => { SerchLeaf(); });
        }

        static public T[] GetAllLeafs<T>(Control.ControlCollection controls)
        {
            T[] answer = new T[0];
            foreach (Control value in controls)
            {
                if (value is T CurrentLeaf)//test
                {
                    Array.Resize(ref answer, answer.Length + 1);  //Изменение размера массива (Ссылка на массив, новая длинна)
                    answer[answer.GetUpperBound(0)] = CurrentLeaf;
                }
            }
            return answer;
        }

        private void SetRandomTarget()
        {
            Random rnd = new Random();
            int rndX = rnd.Next(ThisAnt.Location.X - Visibility, ThisAnt.Location.X + Visibility);
            int rndY = rnd.Next(ThisAnt.Location.Y - Visibility, ThisAnt.Location.Y + Visibility);
            int rndXRight = (rndX % 2) > 0 ? rndX + 1 : rndX;
            int rndYRight = (rndY % 2) > 0 ? rndY + 1 : rndY;

            if (rndXRight > Box.Width || rndYRight > Box.Height || rndXRight < Box.Location.X || rndYRight < Box.Location.Y)
                return;
            Target = new Point(rndXRight, rndYRight);
        }

        private bool InTarget()
        {
            int X = Target.X;
            int Y = Target.Y;
            if ((X == ThisAnt.Location.X) && (Y == ThisAnt.Location.Y))
                return true;
            return false;
        }

        private void GoOneStepToTarget(int speed)
        {
            for (int i = 0; i < speed; i++) { 
            int X = Target.X;
            int Y = Target.Y;
            if ((X == ThisAnt.Location.X) && (Y == ThisAnt.Location.Y))
                return;
            int absX = Math.Abs(X - ThisAnt.Location.X);
            int absY = Math.Abs(Y - ThisAnt.Location.Y);

            int wayDirectionX = absX == 0 ? 0 : (X - ThisAnt.Location.X) / absX;
            int wayDirectionY = absY == 0 ? 0 : (Y - ThisAnt.Location.Y) / absY;

            if (IsFear)
            {
                wayDirectionX *= -1;
                wayDirectionY *= -1;
            }

            ThisAnt.Location = new Point(ThisAnt.Location.X + (wayDirectionX), 
                ThisAnt.Location.Y + (wayDirectionY));
            }
            return;
        }

        private Control FoundLeaf()
        {
            foreach (Control leaf in Leafs)
            {
                
                if (!leaf.Enabled)
                    continue;
                Point antCentre = SupportFunc.getCentre(ThisAnt.Location, ThisAnt.Width, ThisAnt.Height);
                Point leafCentre = SupportFunc.getCentre(leaf.Location, leaf.Width, leaf.Height);
                if (SupportFunc.InRadius(antCentre, leafCentre, Visibility))
                    return leaf;
            }
            return null;
        }

        private void ChFear()
        {
            Point antCentre = SupportFunc.getCentre(ThisAnt.Location, ThisAnt.Width, ThisAnt.Height);
            
            if (SupportFunc.InRadius(antCentre, Enemy, Visibility))
            {
                IsFear = true;
                Target = Enemy;
                TargetLeaf = null;
                return;
            }
            IsFear = false;
        }
        public void RunAway()
        {
            if (!IsFear)
                Brain.popState();
            ChFear();
            GoOneStepToTarget(RunAwaySpeed);
            if (InTarget())
                SetRandomTarget();
        }
        private void Serch()
        {
            GoOneStepToTarget(Speed);
            if (InTarget())
                SetRandomTarget();
        }

        public void SerchLeaf()
        {
            if (IsFear)
                Brain.PushState(()=> { RunAway(); });
            if (TargetLeaf != null)
                Brain.PushState(() => { GoHomeWithLeaf(); });

            ChFear();
            Control foundLeaf = FoundLeaf();
            if (foundLeaf == null)
                Serch();
            else
            {
                GoOneStepToTarget(Speed);
                Point foundLeafCentre = SupportFunc.getCentre(foundLeaf.Location, foundLeaf.Width, foundLeaf.Height);
                if (!InTarget())
                    Target = foundLeafCentre;

                if (SupportFunc.DotInSqrt(ThisAnt.Location, ThisAnt.Width, ThisAnt.Height, foundLeafCentre, 5))
                    TargetLeaf = foundLeaf;
            }
        }

        private bool InHome()
        {
            if (SupportFunc.DotInSqrt(Home.Location, Home.Width, Home.Height, ThisAnt.Location, -5))
                return true;
            return false;
        }

        public void GoHomeWithLeaf()
        {
            if(TargetLeaf == null)
                Brain.PushState(() => { SerchLeaf(); });
            Point homeCentre = SupportFunc.getCentre(Home.Location, Home.Width, Home.Height);
            Target = homeCentre;
            GoOneStepToTarget(Speed);
            if (InHome())
            {
                SetRandomTarget();
                TargetLeaf.Enabled = false;
                TargetLeaf = null;
                Brain.popState();
            }
            else
                TargetLeaf.Location = ThisAnt.Location;
        }
    }
}
