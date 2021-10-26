using UnityEngine;

namespace Levels
{
    public class SuperMushroomScript : BaseContact
    {
        [SerializeField] private GameObject _winGame;
        public override void Contact()
        {
            GameObject win = Instantiate(_effect, transform.position, transform.rotation);
            Destroy(win, 5f);
            _winGame.SetActive(true);
            gameObject.SetActive(false);
            _lifeScript.spawnPlayer = new Vector3(-2, -2, 0);

            if (PlayerPrefs.GetInt("LevelSave") < _sceneDrive.UnlockLevel)
            {
                PlayerPrefs.SetInt("LevelSave", _sceneDrive.UnlockLevel);
            }

            _healthScript.PlusDamage(_bonusScore, _bonusHp);
            _scoreManager.RecordVal();
            _recordEvent.Raise();
            _healthScript.healthBar.SetHealth(_healthScript.Hp);           

            if (_healthScript.Hp > _healthScript.HpMax)
            {
                _healthScript.HpMax = _healthScript.Hp;
                _healthScript.healthBar.SetMaxHealth(_healthScript.HpMax);
            }
        }

        
    }
}