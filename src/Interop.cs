using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
namespace InstallPad
{
    class Interop
    {
        // Converts points on screen to points relative to the upper left corner of the client window, hWnd
        [DllImport("user32.dll")]
        private static extern bool ScreenToClient(IntPtr hWnd, ref Interop.POINT lpPoint);

        public static System.Drawing.Point UpperLeftCornerOfWindow(IntPtr hWnd)
        {
            Interop.POINT p = new POINT(0, 0);
            ScreenToClient(hWnd, ref p);

            // Make these positive
            return new Point(p.X * -1, p.Y * -1);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public static implicit operator Point(POINT p)
            {
                return new Point(p.X, p.Y);
            }

            public static implicit operator POINT(Point p)
            {
                return new POINT(p.X, p.Y);
            }
            public override string ToString()
            {
                return "(" + this.X + ", " + this.Y + ")";
            }

        }
    }
}
