using System;
using System.Collections.Generic;
using System.Text;

namespace SonarProject
{
    public class RadarEngine
    {
        public float CurrentAngle { get; private set; } = 0f;
        public float ScanSpeed { get; set; } = 80f; // Швидкість обертання (град/сек)

        private bool _movingRight = true;
        private readonly List<RadarBlip> _blips = new List<RadarBlip>();

        public IReadOnlyList<RadarBlip> Blips => _blips.AsReadOnly();
        
        public void SetAngle(float angle)
        {
            CurrentAngle = angle;
        }
        
        public void Update(float deltaTime)
        {
            // Рух променя туди і назад в межах 0..180 градусів
            if (_movingRight)
            {
                CurrentAngle += ScanSpeed * deltaTime;
                if (CurrentAngle >= 180f)
                {
                    CurrentAngle = 180f;
                    _movingRight = false;
                }
            }
            else
            {
                CurrentAngle -= ScanSpeed * deltaTime;
                if (CurrentAngle <= 0f)
                {
                    CurrentAngle = 0f;
                    _movingRight = true;
                }
            }

            // Оновлення стану та видалення повністю згаслих перешкод
            for (int i = _blips.Count - 1; i >= 0; i--)
            {
                _blips[i].Update(deltaTime);
                if (_blips[i].IsExpired)
                {
                    _blips.RemoveAt(i);
                }
            }
        }

        public void AddBlip(float angle, float distance)
        {
            _blips.Add(new RadarBlip(angle, distance));
        }
    }
}
