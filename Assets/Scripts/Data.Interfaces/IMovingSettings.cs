using UnityEngine;

public interface IMovingSettings
{
    /// Базовая скорость перемещения
    ///
    float BaseSpeed { get; }
    ///
    /// Базовая скорость поворота
    ///
    float BaseRotationSpeed { get; }
}
