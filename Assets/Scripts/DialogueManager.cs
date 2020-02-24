using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using Fungus;

public class DialogueManager : MonoBehaviour
{
    public Flowchart flowchart;
    public string nextDialogueName;
    public int goToNextIndex;
    public bool check = true;
    public List<Variable> flowchartVariables;
    public GameObject characters;
    public GameObject stage;

    private int opIndex;
    private string organizedPlay;
    private string prevPlanning;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);

        flowchart = GameObject.FindGameObjectWithTag("dialogue").GetComponent<Flowchart>();
        GetVariables();
        if (!Directory.Exists(Application.dataPath + "/FightPlans"))
        {

            Directory.CreateDirectory(Application.dataPath + "/FightPlans");
        }
        //Create Text file.. but dont know how.. How to create the Text file to save to.
        //save to textfile

        

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
            temp = flowchartVariables[opIndex].GetValue();
            if(temp is System.String)
            {
                if(prevPlanning != (string)temp)
                {
                    prevPlanning = temp.ToString();

                    string[] actions = organizedPlay.Split(';');
                    bool replace = false;
                    Debug.Log("new for loop");
                    if(actions.Length > 1 && temp.ToString().Length > 1)
                    {
                        for (int i = 0; i < actions.Length-1; i++)
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
                    else
                    {
                        Debug.Log("Skipped");
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
                    Debug.Log(organizedPlay);
                }
            }
        }
        
    }
    public void ResetBlocks()
    {
        flowchart.Reset(true, true);
    }

    public void StartDialogue()
    {
        flowchart.ExecuteBlock(nextDialogueName + "_Init");
    }

    public void StopDialogue()
    {
        List<Block> executingBlocks = flowchart.GetExecutingBlocks();
        Debug.Log(executingBlocks.Count);
        flowchart.StopAllBlocks();
    }

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
                    string path = Application.dataPath + "/Resources/FightPlans";

                    if (!File.Exists(path))
                    {
                        File.WriteAllText(path + flowchart.GetName() + ".txt", organizedPlay);
                    }
                    break;
                default:
                    break;
            }
        }
        // grab the day so that we can access the correct folder
        string[] temp = nextDialogueName.Split('_');
        

        if (waitInCombat == false)
        {
            string folder = temp[2];
            Destroy(flowchart.gameObject);

            var newDialogue = Resources.Load<Flowchart>("Stories/"+ folder + "/" + nextDialogueName);
            flowchart = Instantiate(newDialogue);
            GetVariables();
        }
        else
        {
            string folder = temp[2];
            flowchart = null;
            SceneManager.LoadScene("CombatTestScene");

            var newDialogue = Resources.Load<Flowchart>("Stories/" + folder + "/" + nextDialogueName);
            
            DontDestroyOnLoad(Instantiate(characters));
            DontDestroyOnLoad(Instantiate(stage));
            flowchart = Instantiate(newDialogue);
            DontDestroyOnLoad(flowchart);
            flowchart.StopAllBlocks();
            GetVariables();           
        }
    }

    void GetVariables()
    {
        flowchartVariables = flowchart.Variables;
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
                default:
                    break;
            }
        }
        check = true;
    }
}
