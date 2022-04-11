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
    public partial class Кегли : Form
    {
        private Point _mouseLocation;

        

        private Rectangle _upBorderRect;
        private Rectangle[] _rightBorderRects;//БОРТИКИ
        private Rectangle[] _leftBorderRects;

        private Vector[] _holesCenters; //ДЫРА

        float width = 870;
        float height = 506;

        float _ballDiametr = 60f;
        float _ballRadius = 30f;

        int _maxBallVelocity = 30;
        float _ballDumping = 0.945f;

        private List<Ball> _balls;
        private Ball _whiteBall;
        private bool _mouseDowm;

        public class Ball //ШАР 
        {
            public Vector position;
            public Color color;
            public Vector velocity;

            public Ball(Color color, Vector position)
            {
                this.position = position;
                this.color = color;
                this.velocity = new Vector();
            }
        }

        public Кегли() 
        {
            InitializeComponent();
            width = this.width - 30;
            height = this.height - 50;
            //БОРТИКИ И ПЕРЕДВИЖЕНИЕ ШАРА
            _leftBorderRects = new Rectangle[] { leftBorder1.Bounds, leftBorder2.Bounds, leftBorder3.Bounds, leftBorder4.Bounds, leftBorder5.Bounds, leftBorder6.Bounds };
            Controls.Remove(leftBorder1);
            Controls.Remove(leftBorder2);
            Controls.Remove(leftBorder3);
            Controls.Remove(leftBorder4);
            Controls.Remove(leftBorder5);
            Controls.Remove(leftBorder6);

            _rightBorderRects = new Rectangle[] { rightBorder1.Bounds, rightBorder2.Bounds, rightBorder3.Bounds, rightBorder4.Bounds, rightBorder5.Bounds, rightBorder6.Bounds };
            Controls.Remove(rightBorder1);
            Controls.Remove(rightBorder2);
            Controls.Remove(rightBorder3);
            Controls.Remove(rightBorder4);
            Controls.Remove(rightBorder5);
            Controls.Remove(rightBorder6);

            _upBorderRect = upBorder.Bounds;
            Controls.Remove(upBorder);

            _holesCenters = new Vector[]
            {
               getControlCenter(hole1),
            };

            Controls.Remove(hole1);

            _whiteBall = new Ball(Color.White, new Vector(getControlCenter(whileBall).X, getControlCenter(whileBall).Y, 0));

            _balls = new List<Ball>();
            _balls.Add(_whiteBall);

            Controls.Remove(whileBall);
        }

        private void Кегли_Paint(object sender, PaintEventArgs e)
        {
            DrawBall(e.Graphics);

            if (_mouseDowm)
            {
                // линия от курсора до шара
                Point ball = new Point((int)_whiteBall.position.X, (int)_whiteBall.position.Y);

                Pen pen = new Pen(Color.Black, 4f);
                e.Graphics.DrawLine(pen, ball, _mouseLocation);
                pen.Dispose();
            }
        }

        private void Кегли_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseDowm = true;
            _mouseLocation = e.Location;

            this.MouseMove += GameForm_MouseMove;
            this.MouseUp += GameForm_MouseUp;

        }
        private void GameForm_MouseMove(object sender, MouseEventArgs e)
        {
            _mouseLocation = e.Location;
        }

        private void GameForm_MouseUp(object sender, MouseEventArgs e)
        {
            _mouseDowm = false;
            this.MouseMove -= GameForm_MouseMove;
            this.MouseUp -= GameForm_MouseUp;

            Vector mouse = new Vector((double)_mouseLocation.X, (double)_mouseLocation.Y, 0);
            _whiteBall.velocity = (_whiteBall.position - mouse);
        }

        public void DrawBall(Graphics g)
        {
            Pen blackPen = new Pen(Color.Black, 3);
            SolidBrush solidBrush = new SolidBrush(Color.White);

            float positionX = 0;
            float positionY = 0;
            RectangleF outerRect;

            foreach (Ball ball in _balls)
            {
                positionX = (float)ball.position.X - _ballRadius;
                positionY = (float)ball.position.Y - _ballRadius;
                outerRect = new RectangleF(positionX, positionY, _ballDiametr, _ballDiametr);

                solidBrush.Color = ball.color;
                g.DrawEllipse(blackPen, outerRect);
                g.FillEllipse(solidBrush, outerRect);
            }

            blackPen.Dispose();
            solidBrush.Dispose();
        }

        private bool checkBallInHoles(Ball ball)
        {
            foreach (Vector v in _holesCenters)
            {
                if ((v - ball.position).Length() <= _ballRadius)
                {
                    System.Console.WriteLine($"{ball.color} in hole!");
                    return true;
                }
            }
            return false;
        }

        private Vector getControlCenter(Control control)
        {
            double X = control.Location.X + control.Width * 0.5f;
            double Y = control.Location.Y + control.Height * 0.5f;
            return new Vector(X, Y, 0);
        }

        internal void UpdateWorld()
        {
            foreach (Ball ball in _balls)
            {
                if (ball.velocity.Length() > _maxBallVelocity)
                {
                    ball.velocity = ball.velocity.Unit() * _maxBallVelocity;
                }

                ball.position += ball.velocity;

                ball.velocity *= _ballDumping;
                if (ball.velocity.Length() < 0.1f) ball.velocity = Vector.zero;
            }

           


            foreach (Ball ball in _balls)
            {
                updateBorderCollisions(ball);
            }

            foreach (Ball ball in _balls)
            {
                if (checkBallInHoles(ball))
                {
                    _balls.Remove(ball);
                    Refresh();
                    break;
                }
            }
        }
        private void updateBorderCollisions(Ball ball)
        {
            
            if (_upBorderRect.Contains((int)(ball.position.X - _ballRadius), (int)ball.position.Y))
            {
                ball.velocity.Y *= -1;
                ball.position.Y = _upBorderRect.Bottom + _ballRadius;
            }

            foreach (Rectangle rect in _leftBorderRects)
            {
                if (rect.Contains((int)ball.position.X, (int)(ball.position.Y - _ballRadius)))
                {
                    ball.velocity.X *= -1;
                    ball.position.X = rect.Right + _ballRadius;
                }
            }
            foreach (Rectangle rect in _rightBorderRects)
            {
                if (rect.Contains((int)ball.position.X, (int)(ball.position.Y - _ballRadius)))
                {
                    ball.velocity.X *= -1;
                    ball.position.X = rect.X - _ballRadius;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
  
    
}
