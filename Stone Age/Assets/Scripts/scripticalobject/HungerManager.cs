using UnityEngine;

namespace Levels
{

    [CreateAssetMenu(fileName = "HungerManager", menuName = "ScriptableObjects/NewHungerManager")]
    public class HungerManager : ScriptableObject
    {
        [SerializeField] private int _hunger;
        [SerializeField] private int _hungerGameOver;
        [SerializeField] private int _life;

        public int Life { get { return _life; } set { _life = value; } }
        public int Hunger { get { return _hunger; } set { _hunger = value; } }
        public int HungerGameOver { get { return _hungerGameOver; } }

        private void OnEnable()
        {
            Life = 5;
        }

        public void HungerVal(int damage)
        {
            Hunger -= damage;
        }
        public void HungerPlus(int eat)
        {
            Hunger += eat;
        }

        public void LifeVal(int damage)
        {
            Life -= damage;
        }
    }
}


