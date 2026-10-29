using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace SonarProject
{
    public partial class Main : Form
    {
        private readonly RadarEngine _engine;
        private readonly RadarRenderer _renderer;
        private readonly System.Windows.Forms.Timer _renderTimer;
        private DateTime _lastFrameTime;
        private readonly Random _random = new Random();
        private SerialPort _serialPort;
        private void InitSerialPort()
        {
            _serialPort = new SerialPort();

            // Основні параметри підключення
            _serialPort.PortName = "COM3";     // Номер вашого COM-порту з Диспетчера пристроїв
            _serialPort.BaudRate = 9600;       // Швидкість має збігатися з Serial.begin(9600) в Arduino
            _serialPort.DataBits = 8;
            _serialPort.Parity = Parity.None;
            _serialPort.StopBits = StopBits.One;

            // Подія надходження нових даних від Arduino
            _serialPort.DataReceived += OnSerialDataReceived;

            try
            {
                _serialPort.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка відкриття порту: {ex.Message}", "Помилка COM-порту",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void OnSerialDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string data = _serialPort.ReadTo(".");
                string[] parts = data.Split(',');

                if (parts.Length == 2 &&
                    float.TryParse(parts[0], out float angle) &&
                    float.TryParse(parts[1], out float distance))
                {
                    BeginInvoke(new Action(() =>
                    {
                        // Повертаємо промінь радара відповідно до реального кута
                        _engine.SetAngle(angle);

                        // 1. ЗМІНА: Враховуємо лише перешкоди від 2 см до 20 см
                        if (distance >= 2f && distance <= 20f)
                        {
                            // 2. ЗМІНА: Ділимо на 20f (відстань 20 см буде на самому краю кола радара)
                            float normDistance = distance / 20f; 
                            _engine.AddBlip(angle, normDistance);
                        }
                    }));
                }
            }
            catch
            {
                // Ігноруємо поодинокі збої передачі
            }
        }

        // Закриваємо порт при закритті форми
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_serialPort != null && _serialPort.IsOpen)
            {
                _serialPort.Close();
                _serialPort.Dispose();
            }
            base.OnFormClosing(e);
        }

        public Main()
        {
            Text = "180° Radar Simulator";
            ClientSize = new Size(800, 480);
            StartPosition = FormStartPosition.CenterScreen;

            // Подвійне буферизування усуває мерехтіння при частому перемальовуванні
            DoubleBuffered = true;

            _engine = new RadarEngine();
            _renderer = new RadarRenderer();

            InitSerialPort();
            
            _renderTimer = new System.Windows.Forms.Timer { Interval = 16 }; // ~60 FPS
            _renderTimer.Tick += OnTimerTick;

            _lastFrameTime = DateTime.Now;
            _renderTimer.Start();

            // Клік мишкою на формі додає перешкоду у вибрану точку
            MouseClick += OnFormMouseClick;
            InitializeComponent();
        }
        private void OnTimerTick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            float deltaTime = (float)(now - _lastFrameTime).TotalSeconds;
            _lastFrameTime = now;

            _engine.Update(deltaTime); // Залишаємо для плавної зміни альфи (згасання)
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            _renderer.Render(e.Graphics, ClientRectangle, _engine);
        }

        private void OnFormMouseClick(object sender, MouseEventArgs e)
        {
            int cx = ClientRectangle.Width / 2;
            int cy = ClientRectangle.Height - 30;
            int radius = Math.Min(cx - 30, cy - 30);

            int dx = cx - e.X;
            int dy = cy - e.Y;

            if (dy > 0) // Якщо клікнули у зоні півкола радара
            {
                double dist = Math.Sqrt(dx * dx + dy * dy);
                if (dist <= radius)
                {
                    float normDist = (float)(dist / radius);
                    float angle = (float)(Math.Atan2(dy, dx) * 180.0 / Math.PI);
                    _engine.AddBlip(angle, normDist);
                }
            }
        }
    }
}
