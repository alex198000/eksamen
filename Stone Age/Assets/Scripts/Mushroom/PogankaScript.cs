using UnityEngine;

namespace Levels
{
    public class PogankaScript : BaseContact
    {
        public override void Contact()
        {
            _healthScript.TakeDamage(_bonusScore);
            _healthScript.HealthBar.SetHealth(_healthScript.Hp);
            gameObject.SetActive(false);
            GameObject dangerous = Instantiate(_effect, transform.position, transform.rotation);
            Destroy(dangerous, 5f);
        }
    }
}