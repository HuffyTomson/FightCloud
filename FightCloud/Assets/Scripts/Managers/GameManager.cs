using UnityEngine;
using System.Collections;
using Huffy.Utilities;

public class GameManager : MonoBehaviour
{
    public int id = 0;
    private StateMachine<GameManager> sm;
    
    IEnumerator Start ()
    {
        
        yield return new WaitForSeconds(0.125f);
        
        sm = new StateMachine<GameManager>(new TestStateOne(this));
    }

    void Update()
    {
        if (sm != null)
            sm.Update();
    }
    
}
