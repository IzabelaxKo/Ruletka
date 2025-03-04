using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class RoundedButton : Button
{
    public int BorderRadius { get; set; } = 20; // Promień zaokrąglenia

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        GraphicsPath path = new GraphicsPath();
        path.AddArc(0, 0, BorderRadius * 2, BorderRadius * 2, 180, 90);
        path.AddArc(Width - BorderRadius * 2, 0, BorderRadius * 2, BorderRadius * 2, 270, 90);
        path.AddArc(Width - BorderRadius * 2, Height - BorderRadius * 2, BorderRadius * 2, BorderRadius * 2, 0, 90);
        path.AddArc(0, Height - BorderRadius * 2, BorderRadius * 2, BorderRadius * 2, 90, 90);
        path.CloseFigure();

        this.Region = new Region(path);
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        using (SolidBrush brush = new SolidBrush(this.BackColor))
        {
            e.Graphics.FillPath(brush, path);
        }

        using (Pen pen = new Pen(this.ForeColor, 2))
        {
            e.Graphics.DrawPath(pen, path);
        }

        StringFormat sf = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), this.ClientRectangle, sf);
    }
}
