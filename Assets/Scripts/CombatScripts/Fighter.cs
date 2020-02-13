using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Fighter : MonoBehaviour
{
    public enum MyEnum
    {

    }
    public string fighterName;
    public float maxStamina;
    public float currentStamina;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract string decideMove(string playerMove, int turnCount, string playerState, string oppoState);

    public abstract void readInPlanning(string path);
}
