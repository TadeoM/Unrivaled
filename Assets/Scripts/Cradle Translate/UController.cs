using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
enum Location
{
    JadeRoom,
    Gym,
    Wendys,
    Stage,
}
enum Characters
{
    Luther,
    Ava,
    Jade,
    Stage,
}

public class UController : MonoBehaviour
{
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
        string newStoryName = (Location)locationIndexer + "_Ava_Day" + currDay;

        // Find all assets labelled with 'architecture' :
        string[] guids1 = AssetDatabase.FindAssets(newStoryName);
        //Story[] stories = Resources.LoadAll<Story>("Stories/Twine/Stories");
        
        Debug.Log(guids1.Length);
        MonoBehaviour nextStory = null; //storyController.gameObject.AddComponent<Story>();

        string temp = AssetDatabase.GUIDToAssetPath(guids1[0]);
        string[] t = temp.Split('/');
        temp = "";
        for (int i = 2; i < t.Length; i++)
        {
            temp += t[i] + "/";
        }
        temp = temp.Split('.')[0];
        //nextStory = AssetDatabase.LoadAssetAtPath<Story>(guid1);
        Debug.Log(temp);
        //nextStory = AssetDatabase.LoadAssetAtPath<Story>(temp);

        var newStoryTemp = Resources.LoadAll(temp)[1];

        Debug.Log(newStoryTemp.GetType());
        //nextStory = newStoryTemp;
        MonoScript tempClass = (MonoScript)newStoryTemp;
        Debug.Log(tempClass.GetClass());

        System.Type tempType = tempClass.GetClass();
        
        locationIndexer++;
        currDay++;
    }
}
