using UnityEngine;

namespace Levels
{

    [CreateAssetMenu(fileName = "ScoreManager", menuName = "ScriptableObjects/NewScoreManager")]
    public class ScoreManager : ScriptableObject
    {
        [SerializeField] private int _score;
        [SerializeField] private int _record;
        [SerializeField] private int _coins;
        [SerializeField] private int _scoreGameWin;

        public int Score { get { return _score; } set { _score = value; } }
        public int Record { get { return _record; } set { _record = value; } }
        public int Coins { get { return _coins; } set { _coins = value; } }
        public int ScoreGameWin { get { return _scoreGameWin; } }

        private void OnEnable()
        {
            Score = 0;
            Record = PlayerPrefs.GetInt("Record");
            Coins = PlayerPrefs.GetInt("Coins");
        }

        public void ScoreVal(int sco)
        {
            Score += sco;
        }

        public void CoinseVal(int coin)
        {
            Coins += coin;
            PlayerPrefs.SetInt("Coins", _coins);
        }

        public void RecordVal()
        {
            if (Record < Score)
            {
                Record = _score;
                PlayerPrefs.SetInt("Record", _record);
            }
        }
    }
}


