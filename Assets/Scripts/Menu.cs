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
        SceneManager.LoadScene(1);
        Destroy(this);
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
        dialogueManager.InitialStartLoad("Stories/" + folder + "/" + info[1]);
        yield return null;
  
        Destroy(this);
        yield return null;
    }
}
