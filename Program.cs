using System.Drawing.Drawing2D;

namespace Smile;

public class MainForm : Form
{
    public MainForm()
    {
        this.Text = "MainForm";
        this.KeyDown += MainForm_KeyDown;
        this.Paint += MainForm_Paint;
        GoFullscreen();
    }

    void GoFullscreen()
    {
        this.WindowState = FormWindowState.Normal;
        this.FormBorderStyle = FormBorderStyle.None;
        this.WindowState = FormWindowState.Maximized;
    }

    private void MainForm_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            this.Close();
        }
    }

    private void MainForm_Paint(object sender, PaintEventArgs e)
    {
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        DrawBackground(e.Graphics, this.ClientSize);

        // Make the smiley path.
        using (GraphicsPath path = MakeSmileyPath(this.ClientSize))
        {
            // Draw the shadow.
            e.Graphics.TranslateTransform(6, 6);
            Color color = Color.FromArgb(64, 0, 0, 0);
            using (Pen thick_pen = new Pen(color, 5))
            {
                e.Graphics.DrawPath(thick_pen, path);
            }
            e.Graphics.ResetTransform();

            // Draw the face.
            using (Pen thick_pen = new Pen(Color.Black, 3))
            {
                e.Graphics.DrawPath(thick_pen, path);
            }
        }
    }

    private GraphicsPath MakeSmileyPath(Size size)
    {
        GraphicsPath path = new GraphicsPath();

        // Head.
        RectangleF rect;
        rect = new RectangleF(
            size.Width * 0.1f,
            size.Height * 0.1f,
            size.Width * 0.8f,
            size.Height * 0.8f);
        path.AddEllipse(rect);

        // Smile.
        rect = new RectangleF(
            size.Width * 0.25f,
            size.Height * 0.25f,
            size.Width * 0.5f,
            size.Height * 0.5f);
        path.AddArc(rect, 20, 140);

        // Nose.
        rect = new RectangleF(
            size.Width * 0.45f,
            size.Height * 0.4f,
            size.Width * 0.1f,
            size.Height * 0.2f);
        path.AddEllipse(rect);

        // Left eye.
        rect = new RectangleF(
            size.Width * 0.3f,
            size.Height * 0.3f,
            size.Width * 0.1f,
            size.Height * 0.2f);
        path.AddEllipse(rect);
        rect.Width /= 2;
        rect.Height /= 2;
        rect.X += rect.Width;
        rect.Y += rect.Height / 2;
        path.AddEllipse(rect);

        // Right eye.
        rect = new RectangleF(
            size.Width * 0.6f,
            size.Height * 0.3f,
            size.Width * 0.1f,
            size.Height * 0.2f);
        path.AddEllipse(rect);
        rect.Width /= 2;
        rect.Height /= 2;
        rect.X += rect.Width;
        rect.Y += rect.Height / 2;
        path.AddEllipse(rect);

        return path;
    }

    private void DrawBackground(Graphics gr, Size size)
    {
        float width = size.Width / 4;
        float height = size.Height / 4;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if ((i + j) % 2 == 0)
                {
                    gr.FillRectangle(Brushes.Gray, i * width, j * height, width, height);
                }
            }
        }
    }
}

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }    
}