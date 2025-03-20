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
        public int Counter { get; private set; } = 10; // Начальное значение счетчика
        public Action<GreenCircle> OnSizeZero;  // Событие при достижении размера 0
        public Action<GreenCircle> OnCounterZero; // Событие при окончании отсчета

        private Timer shrinkTimer;
        private Timer counterTimer;

        public GreenCircle(float x, float y, float angle) : base(x, y, angle)
        {
            shrinkTimer = new Timer { Interval = 100 }; // Уменьшение размера каждые 100 мс
            shrinkTimer.Tick += (s, e) => ReduceSize(0.5f);
            shrinkTimer.Start();

            counterTimer = new Timer { Interval = 1000 }; // Уменьшение счетчика каждую секунду
            counterTimer.Tick += (s, e) => ReduceCounter();
            counterTimer.Start();
        }

        public override void Render(Graphics g)
        {
            // Рисование кружка
            g.FillEllipse(new SolidBrush(Color.Green), -Size / 2, -Size / 2, Size, Size);
            g.DrawEllipse(new Pen(Color.Green, 2), -Size / 2, -Size / 2, Size, Size);

            // Рисование счетчика внутри кружка
            g.DrawString(
                Counter.ToString(),
                new Font("Verdana", 8),
                new SolidBrush(Color.White),
                -8, -6
            );
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

        private void ReduceCounter()
        {
            Counter--;
            if (Counter <= 0)
            {
                OnCounterZero?.Invoke(this);
            }
        }

        public void ResetSize()
        {
            Size = 30; // Новый начальный размер
        }

        public void ResetCounter()
        {
            Counter = 10; // Новый начальный счетчик
        }

        public void RelocateAndReset(Random rand)
        {
            X = rand.Next(0, 800); // Подстройте размеры в зависимости от области
            Y = rand.Next(0, 600);
            ResetSize();
            ResetCounter();
        }
    }


}
