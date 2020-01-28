using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cradle;
using Cradle.StoryFormats.Harlowe;

public class UController : MonoBehaviour
{
    public TwineTextPlayer storyController;
    public List<string> CurrentDialogue { get; set; }
    private int count = 0;
    private int wordIndex = 0;

    private void Update()
    {
        if(count % 10 == 0)
        {
            DisplayDialogue(null, CurrentDialogue[wordIndex]);
            wordIndex++;
        }
        if (Input.anyKeyDown)
            InputCheck();
        count++;
    }

    public void InputCheck()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
            Debug.Log("Something");
    }

    public void ChangeStory(Story newStory)
    {
        storyController.Story = newStory;
    }

    public void DisplayDialogue(GameObject textObject, string text)
    {
    }
}
