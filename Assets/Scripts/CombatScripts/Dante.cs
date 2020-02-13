using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dante : Fighter
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void readInPlanning(string path)
    {
        //waiting for Tadeo's planning implementation
    }

    public override string decideMove(string playerMove, int turnCount, string playerState, string oppoState)
    {
        if(turnCount<=1)
        {
            return "Taunt";
        }
        else if (turnCount > 1 && turnCount <= 5)
        {
            return "Attack";
        }
        else if(turnCount>5 &&turnCount<9)
        {
            readInPlanning("hey tadeo, middle finger @ u");
            return "Block";     //just blocking until we have some
        }
        else if(turnCount==9)
        {
            return "Finisher";
        }

        if (playerState == "grounded")
            return "Pin";
        else if (playerState == "pinned")
            return "Hold";
        else
            return "Finisher";
        
        return "";
    }
}
