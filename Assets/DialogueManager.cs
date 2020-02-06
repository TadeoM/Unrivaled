using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using Fungus;

public class DialogueManager : MonoBehaviour
{
    public Flowchart flowchart;
    public int nextDialogueName;
    public int goToNextIndex;
    public bool check = true;
    public List<Variable> flowchartVariables;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);

        flowchart = GameObject.FindGameObjectWithTag("dialogue").GetComponent<Flowchart>();
        GetVariables();

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
        }        
    }

    void GetNextDialogue()
    {
        check = false;
        Debug.Log("Here");
        string nextDialogueName = "";

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
                    if((bool)flowchartVariables[i].GetValue())
                        SceneManager.LoadScene("CombatTestScene");
                    break;
                default:
                    break;
            }
        }
        Debug.Log(nextDialogueName);

        Destroy(flowchart.gameObject);
        
        var newDialogue = AssetDatabase.LoadAssetAtPath<Flowchart>("Assets/Resources/Stories/" + nextDialogueName + ".prefab");
        flowchart = Instantiate(newDialogue);
        GetVariables();
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
                default:
                    break;
            }
        }
        check = true;
    }
}
