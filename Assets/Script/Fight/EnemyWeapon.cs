using TestTask.Attribute;
using UnityEngine;
namespace TestTask.Fight
{
    public class EnemyWeapon : Weapon
    {
        public override void OnCollision(Attributes attribute)
        {
             attribute.TakeDamage(damage);
            Instantiate(AfterEffectOnHit, transform.position, transform.rotation);
        }

        public override void ResetRigidBody()
        {
            
        }
    }


}
