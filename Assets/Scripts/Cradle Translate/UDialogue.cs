using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDialogue : MonoBehaviour
{
    private Character[] characters;

    public Character[] Characters
    {
        get { return characters; }
    }

    private string sentence;
    private string[] responses;
    private int charRelationIncrease;

    public UDialogue(Character[] chars, string phrase, string[] resp, int cri)
    {
        sentence = phrase;
        responses = resp;
        charRelationIncrease = cri;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayDialogue()
    {
        Debug.Log(sentence);
        foreach (var response in responses)
        {
            Debug.Log("Response: " + response);
        }
       
    }
}
