using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Cassandra : Fighter
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
        if (turnCount <= 3)
        {
            if (playerMove == "Attack")
                return "Block";
            else if (playerMove == "Taunt")
                return "Recover";
        }
        else if (turnCount >= 4 && turnCount <= 7)
        {
            if (playerState == "pinned")
            {
                return "Release";
            }
            else
                return "Pin";
        }
        else
        {
            return "Sell";

        }
        

       

        return "";
    }
}
