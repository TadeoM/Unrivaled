using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Cradle;
using Cradle.StoryFormats.Harlowe;
enum Location
{
    House,
    Gym,
    Wendys,
    Stage,
}

public class UController : MonoBehaviour
{
    public TwineTextPlayer storyController;
    public List<string> CurrentDialogue { get; set; }
    private int currDay;
    private int locationIndexer;
    private Location currLocation;

    private void Awake()
    {
        currDay = 1;
        locationIndexer = 0;
        currLocation = (Location)locationIndexer;
        ChangeStory();
    }

    private void Update()
    {

    }

    public void InputCheck()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
            Debug.Log("Something");
    }

    public void ChangeStory()
    {
        string newStoryName = (Location)locationIndexer + "_Day" + currDay;
        // Find all assets labelled with 'architecture' :
        string[] guids1 = AssetDatabase.FindAssets(newStoryName);
        
        foreach (string guid1 in guids1)
        {
            Debug.Log(AssetDatabase.GUIDToAssetPath(guid1));
        }
        //storyController.Story = "";
        locationIndexer++;
        currDay++;
    }
}
