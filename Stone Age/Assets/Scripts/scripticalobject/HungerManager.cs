using UnityEngine;

[CreateAssetMenu(fileName = "HungerManager", menuName = "ScriptableObjects/NewHungerManager")]
public class HungerManager : ScriptableObject
{
    [SerializeField] private int hunger;
    [SerializeField] private int hungerGameOver;   
    //[SerializeField] private int damage;
    [SerializeField] private int life;
    
   
    public int Life { get { return life; } set { life = value; } }
     public int Hunger { get { return hunger; }  set { hunger = value; } }
    public int HungerGameOver { get { return hungerGameOver; } }
   
    private void OnEnable()
    {       
        Life = 5;
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



