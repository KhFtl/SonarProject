using System;
using System.Collections.Generic;
using System.Text;

namespace SonarProject
{
    public class RadarBlip
    {
        public float Angle { get; set; }      // Кут у градусах (0 - 180)
        public float Distance { get; set; }   // Нормалізована відстань від центру (0.0 - 1.0)
        public float Alpha { get; private set; } = 255f; // Поточна прозорість (255 - 0)

        private readonly float _fadeSpeed; // Швидкість згасання (одиниць альфи за секунду)

        public bool IsExpired => Alpha <= 0;

        public RadarBlip(float angle, float distance, float fadeSpeed = 90f)
        {
            Angle = angle;
            Distance = distance;
            _fadeSpeed = fadeSpeed;
        }

        public void Update(float deltaTime)
        {
            Alpha -= _fadeSpeed * deltaTime;
            if (Alpha < 0) Alpha = 0;
        }
    }
}
