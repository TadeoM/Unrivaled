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

    enum WrestlerState
    {
        standing,
        grounded,
        pinned
    }
    public int turnCount;           //how many turns the match has gone on for
    public int tapCount;            //how many turns a pin has been happening
    public float audienceInterest;
    public List<string> possiblePlayerMoves;
    public GameObject[] buttons;
    public GameObject playerRef;
    public bool isPlayerPinned;
    public bool isOpponentPinned;

    public string currentBattleID;
    private int currentCenterButton;
    private string playerMove;
    private string enemyMove;

    private MatchState matchState;
    private WrestlerState playerState;
    private WrestlerState enemyState;
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

    private Vector3[] buttonPlacement = {
                                         new Vector3(0f, -3.5f, -1.5f),
                                        new Vector3(-2.75f, -1.5f, -1f),
                                        new Vector3(-4f, -0.5f, -0.5f),
                                        new Vector3(-5f, 0.5f, 0f),
                                        new Vector3(5f, 0.5f, 0f),
                                        new Vector3(4f, -0.5f, -0.5f),
                                        new Vector3(2.75f, -1.5f, -1f)

                                        };









    #endregion

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        tapCount = 0;
        turnCount = 0;
        Player.maxStamina = 100f;
        Player.stamina = Player.maxStamina;
        matchState = MatchState.decisionPhase;
        playerState = WrestlerState.standing;
        enemyState = WrestlerState.standing;

        

        updatePossibleMoves();

    }


    // Update is called once per frame
    void Update()
    {

        //Main Combat Switch statement
        switch (matchState)
        {
            case MatchState.decisionPhase:

                //moving menu left and right logic
                if (Input.GetKeyDown(KeyCode.A))
                {
                    updateMenu(false);
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    updateMenu(true);
                }

                //if space hit, select the current button in the middle
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    decideMove();
                }
                break;
            case MatchState.actionPhase:
                actionPhaseAnimating();
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
        possiblePlayerMoves = new List<string>();

        if (playerState == WrestlerState.pinned)
        {
            //check if can kickout eligible
            if (Player.stamina > 15f)
            {
                possiblePlayerMoves.Add("Kickout");

            }

            //check if can sell
            possiblePlayerMoves.Add("Sell");

        }
        else if(playerState == WrestlerState.standing)
        {
            possiblePlayerMoves.Add("Sell");

            //ATTACK
            
            if (Player.stamina >= ATTACK_STAMINA)
                possiblePlayerMoves.Add("Attack");
            

            //taunt
            
            possiblePlayerMoves.Add("Taunt");

            //block check            
            if (Player.stamina >= BLOCK_STAMINA)
                possiblePlayerMoves.Add("Block");

            //check for pin            
            if (Player.stamina >= PIN_STAMINA)
                possiblePlayerMoves.Add("Pin");


            //finisher            
            if (Player.stamina >= SPECIAL_MOVE_STAMINA)
                possiblePlayerMoves.Add("Finisher");

            //recover
            possiblePlayerMoves.Add("Recover");          


        }

        setMenu();

    }


    void setMenu()
    {
        Debug.Log("we settin menu");
        buttons = new GameObject[possiblePlayerMoves.Count];
        currentCenterButton = 0;
        for (int i = 0; i < possiblePlayerMoves.Count; i++)
        {            
            buttons[i] = Instantiate(Resources.Load("Prefabs/buttonTEMP")) as GameObject;
            buttons[i].transform.GetChild(0).GetComponent<TextMesh>().text = possiblePlayerMoves[i];
            buttons[i].transform.position = playerRef.transform.position + buttonPlacement[i];
        }
        buttons[0].transform.localScale = new Vector3(1.6f, 1.6f, 1.6f);
        Debug.Log(possiblePlayerMoves[currentCenterButton]);

    }

    //moves the menu selection
    void updateMenu(bool movingRight)
    {
        
        if (movingRight)
        {
            //change current selection
            currentCenterButton--;
            if (currentCenterButton < 0)
                currentCenterButton = possiblePlayerMoves.Count - 1;

            //move all the buttons to new positions
            for (int i = 0; i < possiblePlayerMoves.Count; i++)
            {
                //set scale
                if (i == currentCenterButton)
                    buttons[i].transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                else
                    buttons[i].transform.localScale = Vector3.one;


                if (i - currentCenterButton == -1)
                    buttons[i].transform.position = playerRef.transform.position + buttonPlacement[possiblePlayerMoves.Count-1];
                else
                {
                    
                    if (i - currentCenterButton < -1)
                    {
                        buttons[i].transform.position = playerRef.transform.position + buttonPlacement[i - currentCenterButton + possiblePlayerMoves.Count];

                    }
                    else
                        buttons[i].transform.position = playerRef.transform.position + buttonPlacement[i -currentCenterButton];

                }
            }

        }          
        else            //Moving left
        {

            //change current selection
            currentCenterButton++;
            if (currentCenterButton >= possiblePlayerMoves.Count)
                currentCenterButton = 0;

            //move all the buttons to new positions
            for (int i = 0; i < possiblePlayerMoves.Count; i++)
            {
                //set scale
                if (i == currentCenterButton)
                    buttons[i].transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                else
                    buttons[i].transform.localScale = Vector3.one;


                if (i - currentCenterButton == -1)
                    buttons[i].transform.position = playerRef.transform.position + buttonPlacement[possiblePlayerMoves.Count - 1];
                else
                {

                    if (i - currentCenterButton < -1)
                    {
                        buttons[i].transform.position = playerRef.transform.position + buttonPlacement[i - currentCenterButton + possiblePlayerMoves.Count];

                    }
                    else
                        buttons[i].transform.position = playerRef.transform.position + buttonPlacement[i - currentCenterButton];

                }
            }
        }

        Debug.Log(possiblePlayerMoves[currentCenterButton]);
        //set the scale of the buttons rights
        //Debug.Log(currentCenterButton);
        //foreach (string k in possiblePlayerMovesKeys)
        //{
        //    Debug.Log(k);
        //}
        

    }


    //confirms the player move as well as deciding opponent move.
    void decideMove()
    {
        playerMove = possiblePlayerMoves[currentCenterButton];

        //deciding enemy move               TEMP for now, enemy will just sell no matter what
        if (currentBattleID == "Ziggler")
        {
            enemyMove = "Sell";
            switch (playerMove)
            {
                case "Attack":
                    audienceInterest += 5f;
                    break;
                case "Pin":
                    enemyState = WrestlerState.pinned;
                    audienceInterest += 5f;
                    break;
                case "Block":
                    audienceInterest -= 5f;
                    break;
                case "Finisher":
                    audienceInterest += 12f;
                    break;
                case "Sell":
                    audienceInterest -= 7f;
                    break;
                case "Recover":
                    audienceInterest -= 2f;
                    break;
                case "Taunt":
                    audienceInterest -= 2f;
                    break;
                default:
                    break;
            }

        }


        #region Move interaction logic

        switch (playerMove)
        {
            case "Attack":               
                break;
            case "Pin":
                break;
            case "Block":
                break;
            case "Finisher":
                break;
            case "Sell":
                break;
            case "Recover":
                break;
            case "Taunt":
                break;
            default:
                break;
        }

        #endregion


        matchState = MatchState.actionPhase;
        turnCount++;
    }

    //this method is to be used for positioning during action phase. The actual choosing of the animation should be able to be done through editor's animator.
    void actionPhaseAnimating()
    {

    }

}
