using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    enum MatchState
    {
        decisionPhase,
        actionPhase,
        loading,
        ending          //may need to make more ending states
    }

    enum PlayerState
    {
        standing,
        grounded,
        pinned
    }
    public int turnCount;           //how many turns the match has gone on for
    public int tapCount;            //how many turns a pin has been happening
    public List<string> possiblePlayerMovesKeys;
    public Dictionary<string, bool> possiblePlayerMoves; //moves the player can currently do
    public GameObject[] buttons;
    public GameObject playerRef;
    public bool isPlayerPinned;
    public bool isOpponentPinned;

    private string playerMove;
    private string enemyMove;

    private MatchState matchState;

    //CONSTANTS
    #region Constants

    #region Stamina Constants
    //Stamine requirements for moves
    public const float KICKOUT_STAMINA = 15f;     //temp number
    public const float SPECIAL_MOVE_STAMINA = 15f;     //temp number
    public const float ATTACK_STAMINA = 15f;     //temp number
    public const float PIN_STAMINA = 15f;     //this is stamina required to pin
    public const float BLOCK_STAMINA = 15f;     //this is stamina required to pin
    #endregion

    #region Menu Placement Constants
    //these are contants to be added to the the player ref 
    //okay vector3s cant be actual consts but i wont be changing em

    //actually no Im just gonna put the values directly into an array cause fuck it

    //private Vector3 currentButton = new Vector3(0f, -3.5f, -1.5f);
    //private Vector3 left1Button = new Vector3(2.75f, -1.5f, -1f);
    //private Vector3 left2Button = new Vector3(4f, -0.5f, -0.5f);
    //private Vector3 left3Button = new Vector3(5f, 0.5f, 0f);
    //private Vector3 right1Button = new Vector3(-2.75f, -1.5f, -1f);
    //private Vector3 right2Button = new Vector3(-4f, -0.5f, -0.5f);
    //private Vector3 right3Button = new Vector3(-5f, 0.5f, 0f);

    private Vector3[] buttonPlacement = {new Vector3(5f, 0.5f, 0f),
                                        new Vector3(4f, -0.5f, -0.5f),
                                        new Vector3(2.75f, -1.5f, -1f),
                                         new Vector3(0f, -3.5f, -1.5f),
                                        new Vector3(-2.75f, -1.5f, -1f),
                                        new Vector3(-4f, -0.5f, -0.5f),
                                        new Vector3(-5f, 0.5f, 0f) };









    #endregion

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        tapCount = 0;
        turnCount = 0;
        Player.maxStamina = 100f;
        Player.stamina = Player.maxStamina;
        updatePossibleMoves();
        matchState = MatchState.decisionPhase;

    }

    
    // Update is called once per frame
    void Update()
    {

        

        //Main Combat Switch statement
        switch (matchState)
        {
            case MatchState.decisionPhase:

                break;
            case MatchState.actionPhase:
                break;
            case MatchState.loading:
                break;
            case MatchState.ending:
                break;
            default:
                break;
        }
    }

    void updatePossibleMoves()
    {
        Debug.Log("wwodihofja");
        possiblePlayerMoves = new Dictionary<string, bool>();
        possiblePlayerMovesKeys = new List<string>();

        if (isPlayerPinned)
        {
            //check if can kickout eligible
            if (Player.stamina > 15f)
            {
                possiblePlayerMoves.Add("Kickout", true);
                possiblePlayerMovesKeys.Add("Kickout");

            }

            //check if can sell
            possiblePlayerMoves.Add("Sell", true);
            possiblePlayerMovesKeys.Add("Sell");

        }
        else
        {
            possiblePlayerMoves.Add("Sell", true);
            possiblePlayerMovesKeys.Add("Sell");

            //ATTACK
            possiblePlayerMovesKeys.Add("Attack");
            if (Player.stamina >= ATTACK_STAMINA)
                possiblePlayerMoves.Add("Attack", true);
            else
                possiblePlayerMoves.Add("Attack", false);

            //taunt
            possiblePlayerMoves.Add("Taunt", true);
            possiblePlayerMovesKeys.Add("Taunt");

            //block check
            possiblePlayerMovesKeys.Add("Block");
            if (Player.stamina >= BLOCK_STAMINA)
                possiblePlayerMoves.Add("Block", true);
            else
                possiblePlayerMoves.Add("Block", false);


            //check for pin
            possiblePlayerMovesKeys.Add("Pin");
            if (Player.stamina >= PIN_STAMINA)
                possiblePlayerMoves.Add("Pin", true);
            else
                possiblePlayerMoves.Add("Pin", false);


            //finisher
            possiblePlayerMovesKeys.Add("Finisher");
            if (Player.stamina >= SPECIAL_MOVE_STAMINA)
                possiblePlayerMoves.Add("Finisher", true);
            else
                possiblePlayerMoves.Add("Finisher", false);

            //recover
            possiblePlayerMovesKeys.Add("Recover");
            possiblePlayerMoves.Add("Recover", true);


        }

        setMenu();

    }


    void setMenu()
    {
        Debug.Log("we settin menu");
        buttons = new GameObject[possiblePlayerMoves.Count];
        for (int i = 0; i < possiblePlayerMoves.Count; i++)
        {
            buttons[i] = Instantiate(Resources.Load("Prefabs/buttonTEMP")) as GameObject;
            buttons[i].transform.GetChild(0).GetComponent<TextMesh>().text = possiblePlayerMovesKeys[i];
            buttons[i].transform.position = playerRef.transform.position + buttonPlacement[i];
        }

    }

    void decidingMove()
    {

    }

}
