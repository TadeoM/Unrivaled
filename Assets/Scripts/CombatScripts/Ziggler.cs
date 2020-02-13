using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Ziggler : Fighter
{
    // Start is called before the first frame update
    void Start()
    {
        fighterName = "Ziggler";
        maxStamina = 100f;
        currentStamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override string decideMove(string playerMove, int turnCount, string playerState, string oppoState)
    {
        

        return "Sell";        

    }

    public override void readInPlanning(string path)
    {
        //we readin
    }

}
