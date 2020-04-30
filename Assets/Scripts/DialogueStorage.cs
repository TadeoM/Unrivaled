using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DialogueStorage : MonoBehaviour
{
    public GameObject scrollView;
    public SayDialog sayTemp;
    GameObject newText;
    GameObject newName;
    RectTransform contentRect;
    public GameObject characters;
    public Dictionary<string, Font> characterFonts;
    bool paused = false;

    List<string> allDialogueSeen = new List<string>();
    private void Start() 
    {
        newText = Resources.Load<GameObject>("Prefabs/AllDialoguePhrase");
        newName= Resources.Load<GameObject>("Prefabs/AllDialogueName");
        characterFonts = new Dictionary<string, Font>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!SceneManager.GetActiveScene().name.Contains("Combat"))
        {
            if (contentRect == null)
            {
                contentRect = GameObject.Find("AllDialogueContent").GetComponent<RectTransform>();
            }
            if (sayTemp == null)
            {
                sayTemp = SayDialog.GetSayDialog();
            }
            if (scrollView == null)
            {
                scrollView = GameObject.Find("AllDialogueView");
                scrollView.SetActive(false);
            }
            if (characters == null)
            {
                characters = GameObject.Find("Characters");
                CharacterStats[] allChar = characters.GetComponentsInChildren<CharacterStats>();
                //characterFonts.Clear();
                foreach (CharacterStats character in allChar)
                {
                    characterFonts.Add(character.name.ToLower(), character.characterFont);
                }
            }
            InputCheck();
        }
    }

    void InputCheck()
    {
        if (sayTemp.GetComponent<Writer>().IsWaitingInput
        && sayTemp.gameObject.GetComponent<DialogInput>().ClickModeAccessor == ClickMode.ClickAnywhere
        && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            UpdateList();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            ToggleDisplayList();
        }
    }

    void UpdateList()
    {
        string dialogueToAdd = sayTemp.GetComponent<SayDialog>().GetStoryText;
        string characterToAdd = sayTemp.GetComponent<SayDialog>().GetNameString;
        string phrase = "<b>" + characterToAdd + "</b>: " + dialogueToAdd; 

        allDialogueSeen.Add(phrase);

        GameObject newCharOBJ = Instantiate(newName);
        GameObject newPhraseOBJ = Instantiate(newText);
        newCharOBJ.transform.SetParent(contentRect.transform, false);
        newPhraseOBJ.transform.SetParent(contentRect.transform, false);

        Text newCharTEXT = newCharOBJ.GetComponentInChildren<Text>();
        Text newPhraseTEXT = newPhraseOBJ.GetComponentInChildren<Text>();
        RectTransform newCharTransform = newCharOBJ.GetComponent<RectTransform>();
        RectTransform newPhraseTransform = newPhraseOBJ.GetComponent<RectTransform>();

        newCharTEXT.text = characterToAdd;
        newPhraseTEXT.text = dialogueToAdd;

        newCharTEXT.font = characterFonts[characterToAdd.ToLower()];

        newCharTransform.anchoredPosition = new Vector2(newCharTransform.anchoredPosition.x, -(allDialogueSeen.Count - 1) * 50);

        newPhraseTransform.anchoredPosition = new Vector2(newPhraseTransform.anchoredPosition.x, -(allDialogueSeen.Count - 1) * 50);
        contentRect.sizeDelta = new Vector2(0, (allDialogueSeen.Count) * 50);

        
        
    }
    
    void ToggleDisplayList()
    {
        scrollView.SetActive(!scrollView.activeSelf);
        if (scrollView.activeSelf)
            sayTemp.gameObject.GetComponent<DialogInput>().ClickModeAccessor = ClickMode.Disabled;
        else
            sayTemp.gameObject.GetComponent<DialogInput>().ClickModeAccessor = ClickMode.ClickAnywhere;
    }
}
