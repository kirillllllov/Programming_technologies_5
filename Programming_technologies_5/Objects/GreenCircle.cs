using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programming_technologies_5.Objects
{
    internal class GreenCircle : BaseObject
    {
        public GreenCircle(float x, float y, float angle) : base(x, y, angle) { }
        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.Green), -25, -15, 30, 30);
            g.DrawEllipse(new Pen(Color.Green, 2), -25, -15, 30, 30);

        }
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-3, -3, 6, 6);
            return path;
        }
    }
}
