using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    enum MatchState
    {
        decisionPhase,
        actionPhase,
        loading,
        ending          //may need to make more ending states
    }
    enum animation
    {
        Idle,
        Grounded,
        Attack,
        Walk,
        Pin,
        Pinned,
        Finisher,
        Sell,
        Hurt,
        Taunt,
        Recover,
        Block
    }
    public enum WrestlerState
    {
        standing,
        grounded,
        pinned
    }
    public int turnCount;                   //how many turns the match has gone on for
    public int tapCount;                    //how many turns a pin has been happening
    public static float audienceInterest;
    public List<string> possiblePlayerMoves;
    public GameObject[] buttons;
    public GameObject playerRef;
    public GameObject oppoRefGO;
    public Fighter oppoRef;
    public bool isPlayerPinned;
    public bool isOpponentPinned;
    public string currentBattleID;
    private int currentCenterButton;        //place in the button array that the player is currently hovering
    private string playerMove;              //the action theplayer is going to take
    private string enemyMove;               //the action the opponent is going to take
    private float tempTimer;                //temp being used
    private Vector3 startingPlayerPos;
    private Vector3 startingOppoPos;

    //animation
    private AnimatorControllerParameter[] playerAnimParams;
    private Animator playerAnimRef;
    private animation currentPlayerAnim;
    private Animator oppoAnimRef;
    private animation currentOppoAnim;


    //Combat UI Elements\
    public GameObject endingText;
    private GameObject TimerGO;
    private GameObject staminaMeterFill;
    private GameObject audienceMeterFill;
    
    private float audienceMeterFillStartingXScale;


    private MatchState matchState;
    private WrestlerState playerState;
    private WrestlerState enemyState;

    private DialogueManager dialogueManager;
    //CONSTANTS
    #region Constants

    #region Stamina Constants
    //Stamine requirements for moves
    public const float KICKOUT_STAMINA = 15f;     //temp number
    public const float FISHER_STAMINA = 15f;     //temp number
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
                                        new Vector3(0f, -2.5f, -1.5f),
                                        new Vector3(-1.75f, -1.5f, -1f),
                                        new Vector3(-2f, -0.5f, -0.5f),
                                        new Vector3(-3f, 0.5f, 0f),
                                        new Vector3(3f, 0.5f, 0f),
                                        new Vector3(2f, -0.5f, -0.5f),
                                        new Vector3(1.75f, -1.5f, -1f)

                                        };









    #endregion

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        tapCount = 0;
        turnCount = 0;
        audienceInterest = 15f;

        Player.maxStamina = 100f;
        Player.stamina = Player.maxStamina;
        matchState = MatchState.decisionPhase;
        playerState = WrestlerState.standing;
        enemyState = WrestlerState.standing;

        oppoRef = oppoRefGO.GetComponent<Fighter>();
        Debug.Log(oppoRef);

        staminaMeterFill = GameObject.FindGameObjectWithTag("MeterFill");
        audienceMeterFill = GameObject.FindGameObjectWithTag("audienceMeter");
        audienceMeterFillStartingXScale = audienceMeterFill.transform.localScale.x;
        TimerGO = GameObject.FindGameObjectWithTag("CombatTimer");

        
        if(playerRef)
        {
            playerAnimRef = playerRef.GetComponentInChildren<Animator>();
            playerAnimParams = playerAnimRef.parameters;
            //playerAnimRef = playerRef.GetComponent<Animator>();
            currentPlayerAnim = animation.Idle;
            currentOppoAnim = animation.Idle;
        }


        endingText.GetComponent<MeshRenderer>().enabled = false;

        updateCombatUI();
        updatePossibleMoves();
        try
        {
            dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();

        }
        catch (System.Exception)
        {

            
        }
    }


    // Update is called once per frame
    void Update()
    {

        //Main Combat Switch statement
        switch (matchState)
        {
            case MatchState.decisionPhase:
                if (playerState == WrestlerState.standing)
                    transitionToAnimation(animation.Idle, "femIdle", true);
                else if (playerState == WrestlerState.pinned)
                    transitionToAnimation(animation.Pinned, "femPinned", true);
                else
                    transitionToAnimation(animation.Grounded, "femPinned", true);
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
        
        possiblePlayerMoves = new List<string>();


        if(enemyState == WrestlerState.pinned)
        {
            possiblePlayerMoves.Add("Hold");
            possiblePlayerMoves.Add("Release");
        }
        else if (playerState == WrestlerState.pinned)
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
            if (Player.stamina >= FISHER_STAMINA)
                possiblePlayerMoves.Add("Finisher");

            //recover
            possiblePlayerMoves.Add("Recover");          


        }

        setMenu();

    }

    //
    public static void addAudienceInterest(float deltaInterest)
    {
        audienceInterest += deltaInterest;
    }


    void setMenu()
    {
        
        buttons = new GameObject[possiblePlayerMoves.Count];
        currentCenterButton = 0;
        for (int i = 0; i < possiblePlayerMoves.Count; i++)
        {            
            buttons[i] = Instantiate(Resources.Load("Prefabs/buttonTEMP")) as GameObject;
            buttons[i].transform.GetChild(0).GetComponent<TextMesh>().text = possiblePlayerMoves[i];
            buttons[i].transform.position = playerRef.transform.position + buttonPlacement[i];
        }
        buttons[0].transform.localScale = new Vector3(1.6f, 1.6f, 1.6f);
        

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

        enemyMove=oppoRef.decideMove(playerMove, turnCount,playerState.ToString(),enemyState.ToString());

        #region Global Move interaction

        switch (playerMove)
        {
            case "Attack":
                Player.stamina -= ATTACK_STAMINA;
                if (enemyMove == "Block")
                {
                    //attack gets blocked
                }
                else if(enemyMove == "Finisher")
                {
                    audienceInterest -= 5f;
                    //enemy's finisher is ruined
                }
                else if(enemyMove == "Sell")
                {
                    audienceInterest += 6f;

                }
                else if(enemyMove == "Taunt")
                {
                    audienceInterest += 3f;
                }
                else if(enemyMove=="Recover")
                {
                    audienceInterest += 4f;
                }
                else
                {
                    audienceInterest += 1f;
                }
                
                break;
            case "Pin":
                Player.stamina -= PIN_STAMINA;
                if(enemyMove=="Sell")
                {
                    enemyState = WrestlerState.pinned;
                    audienceInterest += 5f;
                }
                else if(enemyMove =="Attack")
                {
                    Player.stamina -= 5f;
                }
                else if(enemyMove=="Taunt"||
                        enemyMove=="Recover"||
                        enemyMove=="Block")
                {
                    enemyState = WrestlerState.pinned;
                    audienceInterest += 3f;
                }
                else if(enemyMove=="Finisher")
                {
                    audienceInterest += 7f;
                }
                else
                {
                    //clash
                }
                break;
            case "Block":
                Player.stamina -= BLOCK_STAMINA;
                if(enemyMove=="Attack")
                {
                    audienceInterest += 2f;
                }
                else if(enemyMove=="Pin")
                {
                    playerState = WrestlerState.pinned;
                }
                else if(enemyMove=="Taunt"||
                        enemyMove == "Block"||
                        enemyMove == "Recover")
                {

                }
                else if (enemyMove == "Sell")
                {
                    audienceInterest -= 10f;
                }
                else
                {
                    audienceInterest -= 5f;
                }
                break;
            case "Finisher":
                Player.stamina -= FISHER_STAMINA;
                if(enemyMove == "Sell")
                {
                    audienceInterest += 15f;
                }
                else if(enemyMove=="Recover"||
                        enemyMove == "Taunt"||
                        enemyMove == "Get Up")
                {
                    audienceInterest += 10f;
                }
                else
                {
                    audienceInterest -= 5f;
                }
                break;
            case "Sell":
                if (enemyMove == "Sell")
                {
                    audienceInterest -= 15f;
                }
                else if(enemyMove == "Block"||
                        enemyMove == "Recover"||
                        enemyMove == "Taunt")
                {
                    audienceInterest -= 10f;
                }
                else if(enemyMove == "Pin")
                {
                    audienceInterest += 5f;
                    playerState = WrestlerState.pinned;
                }
                else if(enemyMove=="Attack")
                {
                    audienceInterest += 6f;
                    
                }
                else if (enemyMove == "Finisher")
                {
                    audienceInterest += 15f;
                    playerState = WrestlerState.grounded;
                }
                break;
            case "Recover":
                //if not interupted restore stamina
                if (enemyMove != "Attack" && enemyMove != "Finisher" && enemyMove != "Pin")
                {
                    Player.stamina += 30f;
                    if (Player.stamina > Player.maxStamina)
                        Player.stamina = Player.maxStamina;
                }
                else if (enemyMove == "Finisher")
                {
                    audienceInterest += 10f;
                }
                else if(enemyMove == "Pin")
                {
                    audienceInterest += 4f;
                    playerState = WrestlerState.pinned;
                }
                
                break;
            case "Taunt":
                if (enemyMove != "Attack"   && 
                    enemyMove != "Finisher" && 
                    enemyMove != "Pin"      &&
                    enemyMove!= "Sell")
                {
                    audienceInterest += 5;
                }

                break;
            case "Hold":
                if(enemyState == WrestlerState.pinned)
                {
                    tapCount++;
                    if (tapCount >= 3)
                    {
                        endMatch(true);
                        return;
                    }
                }
                break;
            case "Release":
                if (enemyState == WrestlerState.pinned)
                {
                    tapCount = 0;
                    enemyState = WrestlerState.grounded;
                    //may move this, we will see
                }
                break;
            default:
                Debug.LogWarning("No accurate player move");
                break;
        }


        if(enemyMove=="Get Up" &&enemyState == WrestlerState.grounded)
        {
            enemyState = WrestlerState.standing;
            
        }

        if (Player.stamina > Player.maxStamina)
            Player.stamina = Player.maxStamina;
        #endregion

        turnCount++;
        updateCombatUI();


        tempTimer = 2f;

        matchState = MatchState.actionPhase;
        foreach (GameObject gameObject in buttons)
        {
            Destroy(gameObject);
        }
        possiblePlayerMoves = null;
        //Debug.Log("Stamina" + Player.stamina);
        //Debug.Log("Audience Interest" + audienceInterest);
        Debug.Log("Player Move: " + playerMove + "\nEnemy Move: " + enemyMove);
        

        //Debug.Log("Stamina" + Player.stamina);
        //Debug.Log("Audience Interest" + audienceInterest);
        //Debug.Log("Player Move: " + playerMove + "\nEnemy Move: " + enemyMove);
        try
        {
            if (dialogueManager.flowchart != null)
            {
                dialogueManager.ResetBlocks();
                dialogueManager.StopDialogue();
                dialogueManager.StartDialogue();
            }
        }
        catch (System.Exception)
        {

            Debug.LogWarning("uh oh, dialogue manager issues!!!!!");
        }
        
    }

    //this method is to be used for positioning during action phase. The actual choosing of the animation should be able to be done through editor's animator.
    void actionPhaseAnimating()
    {
        
        //for now we just use a timer then go back to decision phase;
        if(tempTimer<=0)
        {
            matchState = MatchState.decisionPhase;
            updatePossibleMoves();
            
        }
        else
        {
            tempTimer -= Time.deltaTime;
        }

        #region player animating
        if (playerMove == "Attack")
        {
            transitionToAnimation(animation.Attack, "femHit", true);

        }
        else if(playerMove == "Block")
        {
            transitionToAnimation(animation.Block, "femBlock", true);
        }
        else if(playerMove == "Taunt")
        {
            transitionToAnimation(animation.Taunt, "femTaunt", true);
        }
        else if (playerMove == "Pin")
        {
            transitionToAnimation(animation.Pin, "femPin", true);
        }
        else if(playerMove == "Sell")
        {
            transitionToAnimation(animation.Sell, "femSell", true);
        }
        else if (playerMove == "Finisher")
        {
            transitionToAnimation(animation.Finisher, "femHit", true);
        }
        else if(playerMove=="Recover")
        {
            transitionToAnimation(animation.Recover, "femRecovery", true);
        }
        #endregion

        #region oppo animating
        string oppoName = oppoRefGO.name;

        #endregion

    }



    void transitionToAnimation(animation anim, string newAnimationName,bool isPlayer)
    {
        if (isPlayer)
        {
            if(currentPlayerAnim!=anim)
            {
                playerAnimRef.Play(newAnimationName);
                currentPlayerAnim = anim;
            }
        }
        else
        {
            if (currentOppoAnim != anim)
            {
                oppoAnimRef.Play(newAnimationName);
                currentOppoAnim = anim;
            }
        }
    }



    void transitionToAnimation(animation anim, string newAnimationName, bool isPlayer,int frameDelay)
    {
        if (isPlayer)
        {
            if (currentPlayerAnim != anim)
            {
                playerAnimRef.Play(newAnimationName);
                currentPlayerAnim = anim;
            }
        }
        else
        {
            if (currentOppoAnim != anim)
            {
                oppoAnimRef.Play(newAnimationName);
                currentOppoAnim = anim;
            }
        }
    }

    void endMatch(bool playerWin)
    {
        matchState = MatchState.ending;
        endingText.GetComponent<MeshRenderer>().enabled = true;

        if (playerWin)
        {
            endingText.GetComponent<TextMesh>().text = "You win!!";
        }
        else
        {
            endingText.GetComponent<TextMesh>().text = "You Lose :((";

        }
    }

    void updateCombatUI()
    {
        //scaling the stamina and audience meters to match their values
        staminaMeterFill.transform.localScale = new Vector3(Player.stamina / Player.maxStamina, staminaMeterFill.transform.localScale.y, 1f);
        audienceMeterFill.transform.localScale = new Vector3( (audienceInterest/100f)*audienceMeterFillStartingXScale , audienceMeterFill.transform.localScale.y, 1f);

        TimerGO.GetComponent<Text>().text = (turnCount * 5)+"";
    }
}
