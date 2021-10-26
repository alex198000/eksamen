using UnityEngine;

namespace Levels
{
    public class FruitScript : BaseContact
    {
        public override void Contact()
        {
            GameObject fru = Instantiate(_effect, transform.position, transform.rotation);
            Destroy(fru, 5f);
            _healthScript.PlusDamage(_bonusScore, _bonusHp);
            _healthScript.healthBar.SetHealth(_healthScript.Hp);
            
            _scoreManager.RecordVal();
            _recordEvent.Raise();

            if (_healthScript.Hp > _healthScript.HpMax)
            {
                _healthScript.HpMax = _healthScript.Hp;
                _healthScript.healthBar.SetMaxHealth(_healthScript.HpMax);
            }
        }       
    }
}