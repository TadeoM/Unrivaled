using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UStory : MonoBehaviour
{
    private Sprite backgroundImage;
    private List<UDialogue> dialogues = new List<UDialogue>();
    private List<Character> allCharacters = new List<Character>();

    private UDialogue currentDialogue;
    private int dialogueIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextDialogue()
    {
        dialogueIndex++;
        currentDialogue = dialogues[dialogueIndex];
        currentDialogue.DisplayDialogue();
    }

    public void GetCharacters()
    {
        foreach (var dialogue in dialogues)
        {
            if (!allCharacters.Contains(dialogue.Characters[1]))
            {
                allCharacters.Add(dialogue.Characters[1]);
            }
        }
    }

    public UStory NewStory()
    {
        return null;
    }
}
