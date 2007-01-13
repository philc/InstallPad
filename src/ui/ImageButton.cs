using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace InstallPad.UI
{
    // This code shows a simple way to have a 
    // button-like control that has a background image.
    // freely available from
    // http://msdn2.microsoft.com/en-us/library/ms172532(VS.80).aspx

    //
    // Example usage:
    //
    //        PictureButton button = new PictureButton();
    //        button.Parent = this;
    //        button.Bounds = new Rectangle(10, 30, 150, 30);
    //        button.ForeColor = Color.White;
    //        button.BackgroundImage = someIMage
    //        button.PressedImage = some iamge
    //        button.Text = "click me";
    //        button.Click += new EventHandler(button_Click);
    public class ImageButton : Control
    {
        Image backgroundImage, pressedImage, overImage;
        
        bool pressed = false;

        // Whether the mouse is hovering over this button
        bool over = false;
        
        public override Image BackgroundImage
        {
            get
            {
                return this.backgroundImage;
            }
            set
            {
                // Make this button the size of the image
                this.Size = value.Size;

                this.backgroundImage = value;
            }
        }

        // Show when the button is pressed
        public Image PressedImage
        {
            get
            {
                return this.pressedImage;
            }
            set
            {
                this.pressedImage = value;
            }
        }
        // Shown when the mouse hovers over the button
        public Image OverImage
        {
            get
            {
                return this.overImage;
            }
            set
            {
                this.overImage = value;
            }
        }

        // When the mouse button is pressed, set the "pressed" flag to true 
        // and invalidate the form to cause a repaint.  The .NET Compact Framework 
        // sets the mouse capture automatically.
        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.pressed = true;
            this.Invalidate();
            base.OnMouseDown(e);
        }

        // When the mouse is released, reset the "pressed" flag 
        // and invalidate to redraw the button in the unpressed state.
        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.pressed = false;
            this.Invalidate();
            base.OnMouseUp(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            this.over = true;
            Console.WriteLine("enter: " + this.over);
            this.Refresh();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this.over = false;
            this.Refresh();
            base.OnMouseLeave(e);
        }
        // Override the OnPaint method to draw the background image and the text.
        protected override void OnPaint(PaintEventArgs e)
        {
            int w = this.backgroundImage.Size.Width;
            int h = this.backgroundImage.Size.Height;
            
            if (this.pressed && this.pressedImage != null && this.over)
                e.Graphics.DrawImage(this.pressedImage, 0, 0, w, h);
            else if (this.over && this.overImage!=null)
                e.Graphics.DrawImage(this.overImage, 0, 0, w, h);
            else
                e.Graphics.DrawImage(this.backgroundImage, 0,0,w,h);

            //// Draw the text if there is any.
            if (this.Text.Length > 0)
            {
                SizeF size = e.Graphics.MeasureString(this.Text, this.Font);

                // Center the text inside the client area of the PictureButton.
                e.Graphics.DrawString(this.Text,
                    this.Font,
                    new SolidBrush(this.ForeColor),
                    (this.ClientSize.Width - size.Width) / 2,
                    (this.ClientSize.Height - size.Height) / 2);
            }

            // Draw a border around the outside of the 
            // control to look like Pocket PC buttons.
            //e.Graphics.DrawRectangle(new Pen(Color.Black), 0, 0,
            //    this.ClientSize.Width - 1, this.ClientSize.Height - 1);

            base.OnPaint(e);
        }
    }
}

