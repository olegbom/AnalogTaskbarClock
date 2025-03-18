using System.Drawing;
using System.Windows.Forms;

namespace AnalogTaskbarClock
{
    public partial class Form1 : Form
    {
        private const double Dpi = 1.0;

        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.LimeGreen;
            this.TransparencyKey = Color.LimeGreen;
            this.Load += (o, e) =>
            {
                this.Size = new Size((int)(60* Dpi), (int)(60*Dpi));
            };
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            notifyIcon1.ShowBalloonTip(500);
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.TopMost = true;
            if (Screen.PrimaryScreen?.Bounds.Size is { } ss)
            {
                this.Location = new Point(ss.Width - (int)(62 * Dpi), ss.Height - (int)(90 * Dpi));
            }
            this.Refresh();
        }

        private readonly SolidBrush _backBrush = new SolidBrush(Color.FromArgb(255, 30, 30, 30));
        private readonly Pen _secondPen = new Pen(Color.FromArgb(230, 230, 230), 1);
        private readonly Pen _minutePen = new Pen(Color.FromArgb(230, 230, 230), 2);
        private readonly Pen _hourPen = new Pen(Color.FromArgb(230, 230, 230), 3);
        private readonly Pen _cpen = new Pen(Color.FromArgb(255, 230, 230, 230), 1);
        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            int cx = Size.Width / 2;
            int cy = Size.Height / 2;
            var now = DateTime.Now.TimeOfDay;
            
            e.Graphics.FillEllipse(_backBrush, 0, 0, cx * 2, cy * 2);
            double args = now.TotalSeconds * Math.PI * 2 / 60 + Math.PI;
            Draw(_secondPen, args, cx);
            double argm = now.TotalMinutes * Math.PI * 2 / 60 + Math.PI;
            Draw(_minutePen, argm, cx * 0.95);
            double argh = now.TotalHours * Math.PI * 2 / 12 + Math.PI;
            Draw(_hourPen, argh, cx * 0.6);

            for (int i = 0; i < 12; i++)
            {
                double arg = i * Math.PI * 2 / 12;
                int x1 = cx + (int)(cx * (-Math.Sin(arg)));
                int y1 = cy + (int)(cx * (Math.Cos(arg)));
                int x2 = cx + (int)(cx * 0.9 * (-Math.Sin(arg)));
                int y2 = cy + (int)(cx * 0.9 * (Math.Cos(arg)));
                e.Graphics.DrawLine(_cpen, x1, y1, x2, y2);
            }

            void Draw(Pen pen, double arg, double amp)
            {
                int x = cx + (int)(amp * (-Math.Sin(arg)));
                int y = cy + (int)(amp * (Math.Cos(arg)));
                e.Graphics.DrawLine(pen, cx, cy, x, y);
            }
        }

    }
}
