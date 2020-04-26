using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using Fungus;
using System;

public class DialogueManager : MonoBehaviour
{
    public Flowchart flowchart;
    public string nextDialogueName;
    public int goToNextIndex;
    public bool check = true;
    public List<Variable> flowchartVariables;
    public GameObject characters;
    public GameObject stage;
    public GameObject fadeImage;
    public bool testing;

    private int opIndex = -1;
    private int backgroundIndex = -1;
    private string currBackground;
    private string organizedPlay;
    private string prevPlanning;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);
        characters = GameObject.FindGameObjectWithTag("characters");
        // only need to run this if you are testing something starting in the Daily Scene
        // if you are running the game from the main menu, set testing to false
        if (testing)
        {
            flowchart = GameObject.FindGameObjectWithTag("dialogue").GetComponent<Flowchart>();
            GetVariables();
        }
        fadeImage = Resources.Load<GameObject>("Prefabs/transition");
        characters = GameObject.FindGameObjectWithTag("characters");
        stage = GameObject.FindGameObjectWithTag("stage");

        if (!Directory.Exists(Application.dataPath + "/Resources/FightPlans/"))
        {
            Directory.CreateDirectory(Application.dataPath + "/Resources/FightPlans");
        }
        if (!Directory.Exists(Application.dataPath + "/Resources/CharacterStats/"))
        {
            Directory.CreateDirectory(Application.dataPath + "/Resources/CharacterStats/");
        }
        if (!Directory.Exists(Application.dataPath + "/Resources/Saves/"))
        {
            Directory.CreateDirectory(Application.dataPath + "/Resources/Saves");
        }
        // at the end of using a flowchart, check the values of the relationship meter variables and add them to the corresponding character
    }

    // Update is called once per frame
    void Update()
    {
        if(flowchartVariables.Count > 0)
        {
            var temp = flowchartVariables[goToNextIndex].GetValue();

            if (temp is System.Boolean)
            {
                if (check && (bool)temp)
                    GetNextDialogue();
            }
            if(opIndex > 0)
            {
                temp = flowchartVariables[opIndex].GetValue();
                if (temp is System.String)
                {
                    if (prevPlanning != (string)temp)
                    {
                        prevPlanning = temp.ToString();

                        string[] actions = organizedPlay.Split(';');
                        bool replace = false;

                        if (actions.Length > 1 && temp.ToString().Length > 1)
                        {
                            for (int i = 0; i < actions.Length - 1; i++)
                            {
                                string[] newAction = temp.ToString().Split(':');
                                string[] inspectAction = actions[i].Split(':');
                                if (newAction[1].Contains("max") && inspectAction[1].Contains("max"))
                                {
                                    replace = true;
                                    actions[i] = temp.ToString();
                                }
                                else if (newAction[2].Contains(inspectAction[2]))
                                {
                                    replace = true;
                                    actions[i] = temp.ToString();
                                }
                            }
                        }

                        if (!replace)
                        {
                            organizedPlay += (string)temp;
                        }
                        else
                        {
                            organizedPlay = "";
                            for (int i = 0; i < actions.Length; i++)
                            {
                                organizedPlay += actions[i];
                            }
                        }
                    }
                }
            }

            if(backgroundIndex > 0)
            {
                temp = flowchartVariables[backgroundIndex].GetValue();
                if (temp.ToString() != currBackground)
                {
                    ChangeBackground(temp.ToString());
                }
            }
        }
    }

    /// <summary>
    /// Grabs the information needed to go to the next dialogue or to combat and saves relationship increases.
    /// </summary>
    void GetNextDialogue()
    {
        check = false;
        nextDialogueName = "";
        bool waitInCombat = false;
        // go through all variables and respective
        for (int i = 0; i < flowchartVariables.Count; i++)
        {
            switch (flowchartVariables[i].Key)
            {
                case "nextDialogue":
                    nextDialogueName = flowchartVariables[i].GetValue() as string;
                    break;
                case "avaIncrease":
                    break;
                case "lutherIncrease":
                    break;
                case "goToCombat":
                    if((bool)flowchartVariables[i].GetValue() == true)
                        waitInCombat = true;
                    break;
                case "organizedPlay":
                    string path = Application.dataPath + "/Resources/FightPlans/";

                    if (!File.Exists(path))
                    {
                        File.WriteAllText(path + flowchart.GetName() + ".txt", organizedPlay);
                    }
                    break;
                case "avaMeter":
                    GetCharacter("Ava", (int)flowchartVariables[i].GetValue());
                    break;
                case "danteMeter":
                    GetCharacter("Dante", (int)flowchartVariables[i].GetValue());
                    break;
                case "lutherMeter":
                    GetCharacter("Luther", (int)flowchartVariables[i].GetValue());
                    break;
                default:
                    break;
            }
        }

        using (StreamWriter writer = new StreamWriter(Application.dataPath + "/Resources/Saves/save.txt"))
        {
            writer.WriteLine(System.DateTime.Now);
            writer.WriteLine(nextDialogueName);
            if (waitInCombat)
            {
                writer.WriteLine("Combat");
            }
            else
            {
                writer.WriteLine("Daily");
            }
        }

        SaveCharacters();

        // grab the day so that we can access the correct folder
              string[] temp = nextDialogueName.Split('_');

        string folder = temp[2];
        Destroy(flowchart.gameObject);

        Flowchart newDialogue = Resources.Load<Flowchart>("Stories/" + folder + "/" + nextDialogueName);

        StartCoroutine(Fade(newDialogue, waitInCombat));
    }

    /// <summary>
    /// Gets a specified character and stores the stats for saving.
    /// </summary>
    /// <param name="characterName"></param>
    /// <param name="increase"></param>
    void GetCharacter(string characterName, int increase)
    {
        CharacterStats[] characterList = characters.GetComponentsInChildren<CharacterStats>();

        foreach (CharacterStats character in characterList)
        {
            if (character.gameObject.name == characterName)
            {
                character.RelationshipMeter += increase;
                break;
            }
        }
    }

    /// <summary>
    /// Gets a specified character and stores the stats for saving.
    /// </summary>
    /// <param name="characterName"></param>
    /// <param name="increase"></param>
    void SaveCharacters()
    {
        CharacterStats[] characterList = characters.GetComponentsInChildren<CharacterStats>();

        foreach (CharacterStats character in characterList)
        {
            character.StoreStats();
        }
    }

    /// <summary>
    /// Gets variables for the flowchart
    /// </summary>
    void GetVariables()
    {
        flowchartVariables = flowchart.Variables;
        opIndex = -1;

        for (int i = 0; i < flowchartVariables.Count; i++)
        {
            switch (flowchartVariables[i].Key)
            {
                case "nextDialogue":
                    break;
                case "goToNext":
                    goToNextIndex = i;
                    break;
                case "avaIncrease":
                    break;
                case "lutherIncrease":
                    break;
                case "organizedPlay":
                    organizedPlay = flowchartVariables[i].GetValue() as string;
                    opIndex = i;
                    break;
                case "background":
                    currBackground = flowchartVariables[i].GetValue().ToString();
                    backgroundIndex = i;
                    ChangeBackground(flowchartVariables[i].GetValue().ToString());
                    break;
                default:
                    break;
            }
        }
        check = true;
    }

    void ChangeBackground(string newBG)
    {
        currBackground = newBG;
        GameObject newBackground = Resources.Load<GameObject>("Prefabs/" + currBackground);
        Destroy(GameObject.FindGameObjectWithTag("background"));
        Instantiate(newBackground);
    }

    public void LoadFlowchart(string location)
    {
        GameObject checker = GameObject.FindGameObjectWithTag("dialogue");
        if (checker != null)
        {
            Destroy(checker);
        }
        Flowchart newDialogue = Resources.Load<Flowchart>(location);
        flowchart = Instantiate(newDialogue);
        GetVariables();
    }

    IEnumerator Fade(Flowchart newDialogue, bool toCombat)
    {
        var tempOBJ = Instantiate(fadeImage);
        DontDestroyOnLoad(tempOBJ);
        float time = 2;
        for (float ft = 0; ft <= time; ft += 1 * Time.deltaTime)
        {
            Color temp = tempOBJ.GetComponent<SpriteRenderer>().color;
            temp.a = ft / (time/2);
            tempOBJ.GetComponent<SpriteRenderer>().color = temp;
            yield return null;
        }
        if (toCombat)
        {
            flowchart = null;
            SceneManager.LoadScene("Combat");
            yield return null;
            characters = GameObject.FindGameObjectWithTag("characters");
            stage = GameObject.FindGameObjectWithTag("stage");
        }

        for (float ft = time; ft >= 0; ft -= 1 * Time.deltaTime)
        {
            Color temp = tempOBJ.GetComponent<SpriteRenderer>().color;
            temp.a = ft / (time/2);
            tempOBJ.GetComponent<SpriteRenderer>().color = temp;
            yield return null;
        }
        Destroy(tempOBJ);
        flowchart = Instantiate(newDialogue);
        flowchart.StopAllBlocks();
        if (toCombat)
        {
            DontDestroyOnLoad(flowchart);
            DontDestroyOnLoad(Instantiate(characters));
            DontDestroyOnLoad(Instantiate(stage));
        }

        GetVariables();
        yield return null;
    }
}
