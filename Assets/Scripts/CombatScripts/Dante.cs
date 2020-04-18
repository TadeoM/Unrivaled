using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Dante : Fighter
{
    // Start is called before the first frame update
    void Start()
    {
            readInPlanning("Gym_Dante_Day2_Planning.txt");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void readInPlanning(string path)
    {
        //waiting for Tadeo's planning implementation
        //string wholeDecisions = File.ReadAllText(path);
            //File.OpenRead(path);
        
        
        
        //Debug.Log(bruh);
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
        else if(turnCount>5 &&turnCount<8)
        {
            //readInPlanning("Gym_Dante_Day2_Planning");
            if(playerMove=="Block"||playerMove=="Sell"||playerMove=="Taunt")
            {
                return "Attack";
            }
            else if(playerMove=="Attack"||playerMove=="Finisher")
            {
                return "Block";
            }
            else if (playerMove=="Pin")
            {
                return "Pin";
            }
            else if (playerMove == "Recover")
            {
                return "Taunt";
            }

        }
        else if(turnCount==8)
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
