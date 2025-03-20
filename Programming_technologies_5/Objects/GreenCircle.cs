using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timer = System.Windows.Forms.Timer;

namespace Programming_technologies_5.Objects
{
    internal class GreenCircle : BaseObject
    {
        public float Size { get; private set; } = 30; // Начальный размер
        public Action<GreenCircle> OnSizeZero; // Событие при достижении размера 0

        private Timer shrinkTimer;

        public GreenCircle(float x, float y, float angle) : base(x, y, angle)
        {
            shrinkTimer = new Timer { Interval = 100 }; // Уменьшение каждые 100 мс
            shrinkTimer.Tick += (s, e) => ReduceSize(0.5f); // Плавное уменьшение
            shrinkTimer.Start();
        }

        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.Green), -Size / 2, -Size / 2, Size, Size);
            g.DrawEllipse(new Pen(Color.Green, 2), -Size / 2, -Size / 2, Size, Size);
        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-Size / 2, -Size / 2, Size, Size);
            return path;
        }

        private void ReduceSize(float amount)
        {
            Size -= amount;
            if (Size <= 0)
            {
                OnSizeZero?.Invoke(this);
            }
        }

        public void ResetSize()
        {
            Size = 30; // Новый начальный размер
        }

        public void RelocateAndReset(Random rand)
        {
            X = rand.Next(0, 800); // Подстройте размеры в зависимости от области
            Y = rand.Next(0, 600);
            ResetSize(); // Восстановление размера при перемещении
        }
    }

}
