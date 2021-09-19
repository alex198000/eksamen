using UnityEngine;

[CreateAssetMenu(fileName = "ScoreManager", menuName = "ScriptableObjects/NewScoreManager")]
public class ScoreManager : ScriptableObject
{
    [SerializeField] private int score;
    [SerializeField] private int record;
    [SerializeField] private int hunger;
    [SerializeField] private int hungerGameOver;
    [SerializeField] private int scoreGameWin;
    [SerializeField] private int damage;
    [SerializeField] private int life;
    
    public int Score { get { return score; }  set { score = value; } }

    public int Life { get { return life; } set { life = value; } }
    public int Record { get { return record; } set { record = value; } }
    public int Hunger { get { return hunger; }  set { hunger = value; } }
    public int HungerGameOver { get { return hungerGameOver; } }
    public int ScoreGameWin { get { return scoreGameWin; } }

    private void OnEnable()
    {
        Score = 0;
        Life = 5;
        Record = PlayerPrefs.GetInt("Record");
        
    }

    public void ScoreVal(int sco)
    {
        Score+= sco;
    }

    public void RecordVal()
    {
        if(Record < Score)
        {        
        Record = score;
        PlayerPrefs.SetInt("Record", record);
        }
    }

    public void HungerVal(int damage)
    {
        Hunger-= damage;
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



