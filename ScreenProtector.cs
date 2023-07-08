using System;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenProtectorns
{
    public partial class ScreenProtector : Form
    {
        private Timer timer;
        private bool shouldClose;
        private float x;
        private float y;
        private float dx;
        private float dy;

        public ScreenProtector()
        {
            // Set form properties
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            // Disable input events
            KeyDown += ScreensaverForm_KeyDown;
            //MouseMove += ScreensaverForm_MouseMove;
            MouseClick += ScreensaverForm_MouseClick;

            // Create and configure the timer
            timer = new Timer();
            timer.Interval = 16; // 60 frames per second
            timer.Tick += Timer_Tick;

            // Initialize line position and direction
            x = 0;
            y = 0;
            dx = 2;
            dy = 2;

            // Start the timer
            timer.Start();
        }

        private void ScreensaverForm_KeyDown(object sender, KeyEventArgs e)
        {
            shouldClose = true;
        }

        //private void ScreensaverForm_MouseMove(object sender, MouseEventArgs e)
        //{
            
        //}

        private void ScreensaverForm_MouseClick(object sender, MouseEventArgs e)
        {
            shouldClose = true;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Update line position
            x += dx;
            y += dy;

            // Check boundaries and reverse direction if needed
            if (x < 0 || x > Width)
                dx = -dx;
            if (y < 0 || y > Height)
                dy = -dy;

            // Invalidate the form to trigger a redraw
            Invalidate();

            // Check the shouldClose flag and terminate the application if true
            if (shouldClose)
            {
                Close();
                return;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Perform initial drawing and animation
            base.OnPaint(e);

            // Draw a line at the current position
            using (var pen = new Pen(Color.Red, 2))
            {
                e.Graphics.DrawLine(pen, 0, 0, x, y);
            }
        }



        private void ScreensaverForm_Load(object sender, EventArgs e)
        {

        }
    }
}
