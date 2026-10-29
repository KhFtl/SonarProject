using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Text;

namespace SonarProject
{
    public class RadarRenderer
    {
        public Color RadarColor { get; set; } = Color.FromArgb(0, 255, 65); // Зелений колір радара
        public Color BackgroundColor { get; set; } = Color.FromArgb(10, 15, 10);

        public void Render(Graphics g, Rectangle bounds, RadarEngine engine)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(BackgroundColor);

            // Точка центру радара — знизу по центру вікна
            int centerX = bounds.Width / 2;
            int centerY = bounds.Height - 30;
            int radius = Math.Min(centerX - 30, centerY - 30);

            if (radius <= 0) return;

            DrawGrid(g, centerX, centerY, radius);
            DrawSweepLine(g, centerX, centerY, radius, engine.CurrentAngle);
            DrawBlips(g, centerX, centerY, radius, engine.Blips);
        }

        private void DrawGrid(Graphics g, int cx, int cy, int radius)
        {
            using (Pen gridPen = new Pen(Color.FromArgb(80, RadarColor), 1.5f))
            using (Font font = new Font("Consolas", 9f))
            using (Brush textBrush = new SolidBrush(RadarColor))
            {
                // Дуги (концентричні півкола)
                for (int i = 1; i <= 4; i++)
                {
                    int r = (radius / 4) * i;
                    g.DrawArc(gridPen, cx - r, cy - r, r * 2, r * 2, 180, 180);
                }

                for (int angle = 0; angle <= 180; angle += 30)
                {
                    double rad = angle * Math.PI / 180.0;
                    int x = cx - (int)(radius * Math.Cos(rad));
                    int y = cy - (int)(radius * Math.Sin(rad));

                    g.DrawLine(gridPen, cx, cy, x, y);

                    int labelX = cx - (int)((radius + 20) * Math.Cos(rad)) - 10;
                    int labelY = cy - (int)((radius + 20) * Math.Sin(rad)) - 5;
                    g.DrawString($"{angle}°", font, textBrush, labelX, labelY);
                }
            }
        }

        private void DrawSweepLine(Graphics g, int cx, int cy, int radius, float angle)
        {
            double rad = angle * Math.PI / 180.0;
            int endX = cx - (int)(radius * Math.Cos(rad));
            int endY = cy - (int)(radius * Math.Sin(rad));

            // Градієнтний шлейф позаду променя
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddPie(cx - radius, cy - radius, radius * 2, radius * 2, 180 + angle - 12, 12);
                using (PathGradientBrush pgb = new PathGradientBrush(path))
                {
                    pgb.CenterPoint = new PointF(cx, cy);
                    pgb.CenterColor = Color.FromArgb(100, RadarColor);
                    pgb.SurroundColors = new Color[] { Color.Transparent };
                    g.FillPath(pgb, path);
                }
            }

            // Основна лінія сканера ("палочка")
            using (Pen sweepPen = new Pen(RadarColor, 2.5f))
            {
                g.DrawLine(sweepPen, cx, cy, endX, endY);
            }
        }

        private void DrawBlips(Graphics g, int cx, int cy, int radius, IReadOnlyList<RadarBlip> blips)
        {
            foreach (var blip in blips)
            {
                double rad = blip.Angle * Math.PI / 180.0;
                int bx = cx - (int)(radius * blip.Distance * Math.Cos(rad));
                int by = cy - (int)(radius * blip.Distance * Math.Sin(rad));

                int blipSize = 10;
                int alpha = (int)Math.Max(0, Math.Min(255, blip.Alpha));

                // Малювання об'єкта (червоний колір з динамічною прозорістю Alpha)
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(alpha, 255, 50, 50)))
                using (Pen glowPen = new Pen(Color.FromArgb(alpha / 2, 255, 100, 100), 2f))
                {
                    g.FillEllipse(brush, bx - blipSize / 2, by - blipSize / 2, blipSize, blipSize);
                    g.DrawEllipse(glowPen, bx - blipSize / 2 - 2, by - blipSize / 2 - 2, blipSize + 4, blipSize + 4);
                }
            }
        }
    }
}
