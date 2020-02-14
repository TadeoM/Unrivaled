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
                if(organizedPlay != (string)temp)
                {
                    organizedPlay = (string)temp;
                    Debug.Log(Application.dataPath);
                    string path = Application.dataPath + "/FightPlans";

                    if (!File.Exists(path))
                    {
                        File.WriteAllText(Application.dataPath + "/FightPlans/" + flowchart.GetName() + ".txt", organizedPlay);
                    }
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

        for (int i = 0; i < flowchartVariables.Count; i++)
        {
            switch (flowchartVariables[i].Key)
            {
                case "nextDialogue":
                    nextDialogueName = flowchartVariables[i].GetValue() as string;
                    
                    if (nextDialogueName.Contains("goToCombat"))
                    {
                        
                    }
                    break;
                case "avaIncrease":
                    break;
                case "lutherIncrease":
                    break;
                case "goToCombat": 
                    if((bool)flowchartVariables[i].GetValue() == true)
                        waitInCombat = true;
                    break;
                default:
                    break;
            }
        }

        string[] temp = nextDialogueName.Split('_');
        string folder = temp[2];

        if (waitInCombat == false)
        {
            Destroy(flowchart.gameObject);

            var newDialogue = Resources.Load<Flowchart>("Stories/"+ folder + "/" + nextDialogueName);
            flowchart = Instantiate(newDialogue);
            GetVariables();
        }
        else
        {
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
