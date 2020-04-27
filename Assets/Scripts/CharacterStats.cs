using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int RelationshipMeter { get; set; } // a number between 0 and 100
    public string characterName;
    public Font characterFont;
    public Color characterColor;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void StoreStats()
    {
        string path = Application.dataPath + "/Resources/Saves/";
        string textToMake = "meter:"+RelationshipMeter.ToString();
        
        if (!File.Exists(path))
        {
            File.WriteAllText(path + characterName + ".txt", textToMake);
        }
        else
        {
            string text = File.ReadAllText(path + characterName + ".txt");
            text = text.Replace(text, textToMake);
            File.WriteAllText(path + characterName + ".txt", text);
        }
    }
}
