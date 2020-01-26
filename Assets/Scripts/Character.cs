using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private string characterName;
    public int CharacterName
    {
        get { return playerRelation; }
    }
    private int playerRelation;

    public int PlayerRelation
    {
        get { return playerRelation; }
        set { playerRelation = value; }
    }

    private Sprite portrait;
    private Sprite fullBody;

    public Character(string name)
    {
        characterName = name;
        PlayerRelation = 0;

        // get portrait and fullBody from character name
    }
}