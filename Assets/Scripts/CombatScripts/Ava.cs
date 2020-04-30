using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Ava : Fighter
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
        if (turnCount <= 1)
        {
            return "Attack";
        }
        else if (turnCount ==2)
        {
            return "Recover";
        }
        else if (turnCount >= 3 &&playerState!="pinned"&&playerState!="grounded")
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
