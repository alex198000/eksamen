using UnityEngine;

[CreateAssetMenu(fileName = "SceletonManager", menuName = "ScriptableObjects/NewSceletonManager")]
public class SceletonManager : ScriptableObject
{
    [SerializeField] private int sceletonPlus;
    [SerializeField] private int sceletonMinus;


    public int SceletonPlus { get { return sceletonPlus; }  set { sceletonPlus = value; } }    
    public int SceletonMinus { get { return sceletonMinus; } set { sceletonMinus = value; } }


    private void OnEnable()
    {
        SceletonPlus = 0;
        SceletonMinus = 0;
    }

    //public void SceletonPlusValue()
    //{
    //    SceletonPlus++;
    //}

    //public void SceletonMinusValue()
    //{
    //    SceletonMinus++;
    //}
}



