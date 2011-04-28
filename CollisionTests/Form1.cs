using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using wing_ding_pong._2D;

namespace CollisionTests
{
    public partial class Form1 : Form
    {
        wing_ding_pong._2D.Speed _circleSpeed = new wing_ding_pong._2D.Speed(new wing_ding_pong._2D.Vector(2, 3), new TimeSpan(0,0,0,0, 80));
        wing_ding_pong.CollidableObjects.Circle _circle;
        wing_ding_pong.CollidableObjects.Circle _circle2;
        wing_ding_pong.CollidableObjects.Rectangle _wall1;
        wing_ding_pong.CollidableObjects.Rectangle _wall2;
        wing_ding_pong.CollidableObjects.Rectangle _wall3;
        wing_ding_pong.CollidableObjects.Rectangle _wall4;
        IList<wing_ding_pong.CollidableObjects.Tile> _collidableObjects = new List<wing_ding_pong.CollidableObjects.Tile>();
        IList<Speed> _objectSpeeds = new List<Speed>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
            //_circle = new wing_ding_pong.CollidableObjects.Circle(ProjectFromFormX(this.Width / 2), ProjectFromFormY(this.Height / 2), 10);
            _circle = new wing_ding_pong.CollidableObjects.Circle(this.Width / 2 + 50, this.Height / 2, 10);
            _collidableObjects.Add(_circle);
            _objectSpeeds.Add(new Speed(new Vector(14, 17), new TimeSpan(100)));
            _circle2 = new wing_ding_pong.CollidableObjects.Circle(this.Width / 2, this.Height / 2, 10);
            _collidableObjects.Add(_circle2);
            _objectSpeeds.Add(new Speed(new Vector(-15, 14), new TimeSpan(100)));
            //_wall1 = new wing_ding_pong.CollidableObjects.Rectangle(ProjectFromFormX(0), ProjectFromFormY(this.Height / 2), 50, this.Height - 52);
            _wall1 = new wing_ding_pong.CollidableObjects.Rectangle(0,this.Height / 2, 50, this.Height - 52);
            _collidableObjects.Add(_wall1);
            _objectSpeeds.Add(new Speed(new Vector(0, 0), new TimeSpan(1000)));
            //_wall2 = new wing_ding_pong.CollidableObjects.Rectangle(ProjectFromFormX(this.Width / 2), ProjectFromFormY(0), this.Width - 52, 50);
            _wall2 = new wing_ding_pong.CollidableObjects.Rectangle(this.Width / 2, 0, this.Width - 52, 50);
            _collidableObjects.Add(_wall2);
            _objectSpeeds.Add(new Speed(new Vector(0, 0), new TimeSpan(1000)));
            //_wall3 = new wing_ding_pong.CollidableObjects.Rectangle(ProjectFromFormX(this.Width), ProjectFromFormY(this.Height / 2), 50, this.Height - 52);
            _wall3 = new wing_ding_pong.CollidableObjects.Rectangle(this.Width, this.Height / 2, 50, this.Height - 52);
            _collidableObjects.Add(_wall3);
            _objectSpeeds.Add(new Speed(new Vector(0, 0), new TimeSpan(1000)));
            //_wall4 = new wing_ding_pong.CollidableObjects.Rectangle(ProjectFromFormX(this.Width / 2), ProjectFromFormY(this.Height - 40), this.Width - 52, 50);
            _wall4 = new wing_ding_pong.CollidableObjects.Rectangle(this.Width / 2, this.Height - 40, this.Width - 52, 50);
            _collidableObjects.Add(_wall4);
            _objectSpeeds.Add(new Speed(new Vector(0, 0), new TimeSpan(1000)));
            wing_ding_pong.CollisionDetection.RegisterCollisionTrait<wing_ding_pong.CollidableObjects.Circle,
                                                                    wing_ding_pong.CollidableObjects.Rectangle>
                                                                    (new wing_ding_pong.Traits.CircleRecCollisionCheckTraits());
            wing_ding_pong.CollisionDetection.RegisterCollisionTrait<wing_ding_pong.CollidableObjects.Circle,
                                                                    wing_ding_pong.CollidableObjects.Circle>
                                                                    (new wing_ding_pong.Traits.CircleCircleCollisionCheckTraits());
            timer1.Start();
        }

        private double ProjectFromFormX(double x)
        {
            return x - (this.Width / 2);
        }

        private double ProjectFromFormY(double y)
        {
            return (y * -1) + (this.Height / 2);
        }

        private double ProjectToFormX(double x)
        {
            return x + (this.Width / 2);
        }

        private double ProjectToFormY(double y)
        {
            return (y - (this.Height / 2)) * -1;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Update(timer1.Interval);
            this.Draw();
        }

        protected void Draw()
        {
            double prjX, prjY;
            Pen circlePen = new Pen(Color.Black);
            Pen recPen;
            Graphics g = this.CreateGraphics();
            g.Clear(Color.LightGray);
            //prjX = ProjectToFormX(_circle.Pos.X);
            //prjY = ProjectToFormY(_circle.Pos.Y);
            prjX = _circle.Pos.X;
            prjY = _circle.Pos.Y;
            g.DrawEllipse(circlePen, (int)(prjX - _circle.Radius), (int)(prjY - _circle.Radius), (int)(_circle.Radius * 2), (int)(_circle.Radius * 2));
            prjX = _circle2.Pos.X;
            prjY = _circle2.Pos.Y;
            g.DrawEllipse(circlePen, (int)(prjX - _circle2.Radius), (int)(prjY - _circle2.Radius), (int)(_circle2.Radius * 2), (int)(_circle2.Radius * 2));
            //prjX = ProjectToFormX(_wall1.UpperLeft.X);
            //prjY = ProjectToFormY(_wall1.UpperLeft.Y);
            prjX = _wall1.UpperLeft.X;
            prjY = _wall1.UpperLeft.Y;
            recPen = new Pen(Color.Red);
            g.DrawRectangle(recPen, new Rectangle((int)prjX, (int)prjY, (int)_wall1.Width, (int)_wall1.Height));
            //prjX = ProjectToFormX(_wall2.UpperLeft.X);
            //prjY = ProjectToFormY(_wall2.UpperLeft.Y);
            prjX = _wall2.UpperLeft.X;
            prjY = _wall2.UpperLeft.Y;
            recPen = new Pen(Color.Blue);
            g.DrawRectangle(recPen, new Rectangle((int)prjX, (int)prjY, (int)_wall2.Width, (int)_wall2.Height));
            //prjX = ProjectToFormX(_wall3.UpperLeft.X);
            //prjY = ProjectToFormY(_wall3.UpperLeft.Y);
            prjX = _wall3.UpperLeft.X;
            prjY = _wall3.UpperLeft.Y;
            recPen = new Pen(Color.Green);
            g.DrawRectangle(recPen, new Rectangle((int)prjX, (int)prjY, (int)_wall3.Width, (int)_wall3.Height));
            //prjX = ProjectToFormX(_wall4.UpperLeft.X);
            //prjY = ProjectToFormY(_wall4.UpperLeft.Y);
            prjX = _wall4.UpperLeft.X;
            prjY = _wall4.UpperLeft.Y;
            recPen = new Pen(Color.Orange);
            g.DrawRectangle(recPen, new Rectangle((int)prjX, (int)prjY, (int)_wall4.Width, (int)_wall4.Height));
        }

        protected void Update(int ticks)
        {
            double movementDistance;
            TimeSpan dT = new TimeSpan(ticks);
            Vector obj1Dp, obj2Dp;
            Vector obj1Direction, obj2Direction;
            for (int i = 0; i < _collidableObjects.Count; i++)
            {
                for (int j = i + 1; j < _collidableObjects.Count; j++)
                {
                    if (wing_ding_pong.CollisionDetection.StaticTileStaticTileCollision(_collidableObjects[i], _collidableObjects[j],
                                                                                        out obj1Dp, out obj1Direction, out obj2Dp,
                                                                                        out obj2Direction))
                    {
                        movementDistance = Math.Sqrt(wing_ding_pong._2D.Math2D.DistanceSquared(_objectSpeeds[i].Distance));
                        _collidableObjects[i].Move(obj1Dp.X, obj1Dp.Y);
                        _objectSpeeds[i].Distance.X = obj1Direction.X * movementDistance;
                        _objectSpeeds[i].Distance.Y = obj1Direction.Y * movementDistance;
                        _collidableObjects[j].Move(obj2Dp.X, obj2Dp.Y);
                        movementDistance = Math.Sqrt(wing_ding_pong._2D.Math2D.DistanceSquared(_objectSpeeds[j].Distance));
                        _objectSpeeds[j].Distance.X = obj2Direction.X * movementDistance;
                        _objectSpeeds[j].Distance.Y = obj2Direction.Y * movementDistance;
                    }
                }
                _collidableObjects[i].Move(_objectSpeeds[i].GetVector(dT).X, _objectSpeeds[i].GetVector(dT).Y);
            }
        }
    }
}
