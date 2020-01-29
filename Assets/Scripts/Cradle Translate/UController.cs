using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cradle;
using Cradle.StoryFormats.Harlowe;

public class UController : MonoBehaviour
{
    public TwineTextPlayer storyController;
    public List<StoryOutput> currentDialogue;
    public List<int> linkIndices;
    public int currLinkIndex = 0;
    public List<StoryLink> links;
    public Text dialogue;
    private int count = 0;
    private int wordIndex = 0;

    
    private void Awake()
    {
        currentDialogue = new List<StoryOutput>();
        linkIndices = new List<int>();
        links = new List<StoryLink>();
    }

    private void Update()
    {
        if (count % 10 == 0 && wordIndex < currentDialogue.Count)
        {
            //DisplayDialogue(linkIndices.Contains(wordIndex));
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

    public void AddWord(StoryOutput text, bool isLink, StoryLink link)
    {
        currentDialogue.Add(text);
        Debug.Log(text);
        if (isLink)
        {
            linkIndices.Add(currentDialogue.Count - 1);
            links.Add(link);
        }
    }

   /* public void DisplayDialogue(bool link)
    {
        if (link)
        {
            Button uiLink = (Button)Instantiate(storyController.LinkTemplate);
            uiLink.gameObject.SetActive(true);
            uiLink.name = "[[" + currentDialogue[wordIndex].Text + "]]";

            Text uiLinkText = uiLink.GetComponentInChildren<Text>();
            uiLinkText.text = currentDialogue[wordIndex].Text;
            uiLink.onClick.AddListener(() =>
            {
                storyController.Story.DoLink(link);
            });
            currLinkIndex++;
        }
        else
        {
            Text uiText = (Text)Instantiate(storyController.WordTemplate);
            uiText.gameObject.SetActive(true);
            uiText.text = word;
            uiText.name = word;
        }
        uiText.text = currentDialogue[wordIndex].Text;


        uiText.rectTransform.SetParent(storyController.Container);
        if (wordIndex >= 0)
            uiText.rectTransform.SetSiblingIndex(wordIndex);

        var elem = uiText.rectTransform.gameObject.AddComponent<TwineTextPlayerElement>();
        //elem.SourceOutput = output;
    }*/
}
