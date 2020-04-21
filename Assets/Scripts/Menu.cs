using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fungus;

public class Menu : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this);
    }
    public void StartGame()
    {
        StartCoroutine(NewGame());
    }

    public void LoadGame()
    {
        Destroy(GameObject.Find("Canvas")/*.GetComponentInChildren<GameObject>()*/);

        using (StreamReader reader = new StreamReader(Application.dataPath + "/Resources/Saves/save.txt"))
        {
            string line = string.Empty;
            List<string> info = new List<string>();
            while ((line = reader.ReadLine()) != null)
            {
                info.Add(line);
            }

            StartCoroutine(LoadDialogue(info));
        }

    }
    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadDialogue(List<string> info)
    {
        string[] temp = info[1].Split('_');
        string folder = temp[2];

        SceneManager.LoadScene(info[2]);
        yield return null;

        GameObject altemp = GameObject.FindGameObjectWithTag("diagManager");
        DialogueManager dialogueManager = GameObject.FindGameObjectWithTag("diagManager").GetComponent<DialogueManager>();
        dialogueManager.LoadFlowchart("Stories/" + folder + "/" + info[1]);
        // get characters
        GameObject charactersGameObject = GameObject.FindGameObjectWithTag("characters");
        CharacterStats[] characters = charactersGameObject.GetComponentsInChildren<CharacterStats>();

        foreach (CharacterStats character in characters)
        {
            using (StreamReader reader = new StreamReader(Application.dataPath + "/Resources/Saves/" + character.characterName + ".txt"))
            {
                string line = string.Empty;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] lines = line.Split(':');
                    int result = 0;
                    bool success = int.TryParse(lines[1], out result);

                    character.RelationshipMeter = result;
                }
            }
            Debug.Log(character.characterName + ": " + character.RelationshipMeter);
        }
        // set their character meters to the correct values


        yield return null;
  
        Destroy(this);
        yield return null;
    }

    IEnumerator NewGame()
    {

        SceneManager.LoadScene(1);
        yield return null;
       
        GameObject altemp = GameObject.FindGameObjectWithTag("diagManager");
        DialogueManager dialogueManager = GameObject.FindGameObjectWithTag("diagManager").GetComponent<DialogueManager>();
        dialogueManager.LoadFlowchart("Stories/Day1/Gym_Ava_Day1");
        yield return null;

        Destroy(this);
        yield return null;
    }
}
