
public interface IWeapon
{
    bool IsCanShoot { get; }

    float ShotDelay { get; }

    void TryFire();
    void Shot();

}
