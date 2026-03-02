
//interface for objects that can recieve damage
//this acts as the bridge between a hit
//(collision/raycast) and the health system
public interface IDamageReciever
{
    void ApplyDamage(DamageInfo info);
}
