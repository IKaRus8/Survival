using System.Security.Cryptography;
using UnityEngine;
public interface IDamageSystem 
{
    void TakeDamage(IDamageble attacker, IDamageble victim, float damage);
}
