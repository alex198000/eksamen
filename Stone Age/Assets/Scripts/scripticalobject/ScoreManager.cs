using UnityEngine;

[CreateAssetMenu(fileName = "ScoreManager", menuName = "ScriptableObjects/NewScoreManager")]
public class ScoreManager : ScriptableObject
{
    [SerializeField] private int score;
    [SerializeField] private int record;   
    [SerializeField] private int scoreGameWin;  
    
    public int Score { get { return score; }  set { score = value; } }    
    public int Record { get { return record; } set { record = value; } }    
    public int ScoreGameWin { get { return scoreGameWin; } }

    private void OnEnable()
    {
        Score = 0;
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
}



