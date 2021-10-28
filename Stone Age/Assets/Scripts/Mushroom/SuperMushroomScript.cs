using System;
using UnityEngine;

namespace Levels
{
    public class SuperMushroomScript : BaseContact
    {
        [SerializeField] private GameObject _winGame;
        public static event Action OnPlayerWin;
        public override void Contact()
        {
            base.Contact();
            _winGame.SetActive(true);
            
            _lifeScript.SpawnPlayer = new Vector3(-2, -2, 0);

            OnPlayerWin?.Invoke();

            if (PlayerPrefs.GetInt("LevelSave") < _sceneDrive.UnlockLevel)
            {
                PlayerPrefs.SetInt("LevelSave", _sceneDrive.UnlockLevel);
            }            
        }        
    }
}