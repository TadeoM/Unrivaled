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
        Block,
        Kickout
    }

    enum animationTiming
    {
        preAnim,
        anim,
        postAnim,
        waiting
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
    private bool initialPause;              // make sure we've initially paused the game
    private int currentCenterButton;        //place in the button array that the player is currently hovering
    private string playerMove;              //the action theplayer is going to take
    private string enemyMove;               //the action the opponent is going to take
    private float tempTimer;                //temp being used
    private Vector3 startingPlayerPos;
    private Vector3 startingOppoPos;
    private Vector3 pinnedPos;
    private Vector3 pinPos;
    private Vector3 middleLeft;
    private Vector3 middleRight;


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
    private animationTiming playerAnimTiming;
    private animationTiming oppoAnimTiming;


    public DialogueManager dialogueManager;
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
                                        new Vector3(0f, -1.5f, -4.5f),
                                        new Vector3(-2f, -0.5f, -0.5f),
                                        new Vector3(-2f, -0.5f, -0.5f),
                                        new Vector3(-3f, 0.5f, 0f),
                                        new Vector3(3f, 0.5f, 0f),
                                        new Vector3(2f, -0.5f, -0.5f),
                                        new Vector3(2f, -0.5f, -0.5f)

                                        };









    #endregion

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        tapCount = 0;
        turnCount = 0;
        audienceInterest = 15f;
        initialPause = false;

        Player.maxStamina = 100f;
        Player.stamina = Player.maxStamina;
        matchState = MatchState.decisionPhase;
        playerState = WrestlerState.standing;
        enemyState = WrestlerState.standing;

        oppoRef = oppoRefGO.GetComponent<Fighter>();

        staminaMeterFill = GameObject.FindGameObjectWithTag("MeterFill");
        audienceMeterFill = GameObject.FindGameObjectWithTag("audienceMeter");
        audienceMeterFillStartingXScale = audienceMeterFill.transform.localScale.x;
        TimerGO = GameObject.FindGameObjectWithTag("CombatTimer");

        
        if(playerRef)
        {
            playerAnimRef = playerRef.GetComponentInChildren<Animator>();
            oppoAnimRef = oppoRefGO.GetComponent<Animator>();
            playerAnimParams = playerAnimRef.parameters;
            //playerAnimRef = playerRef.GetComponent<Animator>();
            currentPlayerAnim = animation.Idle;
            currentOppoAnim = animation.Idle;
        }


        endingText.GetComponent<MeshRenderer>().enabled = false;

        updateCombatUI();
        updatePossibleMoves();

        
    }


    // Update is called once per frame
    void Update()
    {
        if(dialogueManager == null && GameObject.FindGameObjectWithTag("diagManager") != null)
        {
            dialogueManager = GameObject.FindGameObjectWithTag("diagManager").GetComponent<DialogueManager>();
        }
        if(dialogueManager != null && dialogueManager.sayDialog != null && initialPause == false)
        {
            dialogueManager.Pause();
            initialPause = true;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            endMatch(false);
        }
        //Main Combat Switch statement
        switch (matchState)
        {
            case MatchState.decisionPhase:
                if (playerState == WrestlerState.standing)
                {
                    transitionToAnimation(animation.Idle, "Jade_Idle", true);
                    MenuCuffing();
                    if (enemyState == WrestlerState.standing)
                        transitionToAnimation(animation.Idle, oppoRefGO.name+"_Idle", false);
                    else if (enemyState == WrestlerState.pinned)
                        transitionToAnimation(animation.Pinned, oppoRefGO.name + "_Pinned", false);
                    else
                        transitionToAnimation(animation.Grounded, oppoRefGO.name + "_Grounded", false);
                }
                else if (playerState == WrestlerState.pinned)
                {
                    transitionToAnimation(animation.Pinned, "Jade_Pinned", true);
                    transitionToAnimation(animation.Pin, oppoRefGO.name + "_Pin", false);
                    playerRef.transform.position = pinnedPos;
                    oppoRefGO.transform.position = pinPos;
                }
                else if (enemyState == WrestlerState.pinned)
                {
                    transitionToAnimation(animation.Pinned, "Jade_Pin", true);
                    transitionToAnimation(animation.Pin, oppoRefGO.name + "_Pinned", false);
                    playerRef.transform.position = pinPos;
                    oppoRefGO.transform.position = pinnedPos;
                }
                else 
                {
                    transitionToAnimation(animation.Grounded, "Jade_Grounded", true);
                    
                    if (enemyState == WrestlerState.standing)
                        transitionToAnimation(animation.Idle, oppoRefGO.name + "_Idle", false);
                    else if (enemyState == WrestlerState.pinned)
                        transitionToAnimation(animation.Pinned, oppoRefGO.name + "_Pinned", false);
                    else
                        transitionToAnimation(animation.Grounded, oppoRefGO.name + "_Grounded", false);
                }


                
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
        else if (playerState == WrestlerState.grounded)
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
            buttons[i].transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);

        }
        buttons[0].transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);


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
                    buttons[i].transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
                else
                    buttons[i].transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);


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
                    buttons[i].transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
                else
                    buttons[i].transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);


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

        if(playerState==WrestlerState.pinned && playerMove=="Sell")
        {
            endMatch(false);
            return;
        }

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
        try
        {
            dialogueManager.LoadFlowchart(playerMove, enemyMove, "jade", oppoRef.name.ToLower());

        }
        catch (System.Exception)
        {

            
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
        //Debug.Log("Player Move: " + playerMove + "\nEnemy Move: " + enemyMove);
        
        if(playerMove=="Finisher"   ||
            playerMove=="Attack"    ||
            playerMove=="Pin"       )
        {
            playerAnimTiming = animationTiming.preAnim;
        }

        //Debug.Log("Stamina" + Player.stamina);
        //Debug.Log("Audience Interest" + audienceInterest);
        //Debug.Log("Player Move: " + playerMove + "\nEnemy Move: " + enemyMove);
        try
        {
            if (dialogueManager.flowchart != null)
            {
                //dialogueManager.ResetBlocks();
                //dialogueManager.StopDialogue();
                //dialogueManager.StartDialogue();
            }
        }
        catch (System.Exception)
        {

            //Debug.LogWarning("uh oh, dialogue manager issues!!!!!");
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
        if(playerAnimTiming == animationTiming.preAnim)
        {
            //transitionToAnimation(animation.Block, "Jade_Run", true);
            playerAnimTiming = animationTiming.anim;

            if (playerRef.transform.position.x   <   oppoRefGO.transform.position.x - 2f)
            {
                //playerRef.transform.position = playerRef.transform.position + Vector3.right * 0.02f;
            }
            else
            {
                playerAnimTiming=animationTiming.anim;
            }

        }
        if (playerAnimTiming==animationTiming.anim)
        {
            string playerAnimName = "Jade_";
            if (playerMove == "Attack")
            {
                //walk first
                //if()come back here list
                playerAnimName += "Hit";
                transitionToAnimation(animation.Attack, playerAnimName, true);
                if (playerAnimTiming == animationTiming.preAnim)
                {
                    playerAnimRef.Play("Jade_Walk");
                    
                }
            }
            else if (playerMove == "Block")
            {
                playerAnimName += "Block";
                transitionToAnimation(animation.Block, playerAnimName, true);
            }
            else if (playerMove == "Taunt")
            {
                playerAnimName += "Taunt";
                transitionToAnimation(animation.Taunt, playerAnimName, true);
            }
            else if (playerMove == "Pin")
            {
                playerAnimName += "Pin";
                transitionToAnimation(animation.Pin, playerAnimName, true);
            }
            else if (playerMove == "Sell")
            {
                playerAnimName += "Sell";

                transitionToAnimation(animation.Sell, playerAnimName, true);
            }
            else if (playerMove == "Finisher")
            {
                playerAnimName += "Hit";
                transitionToAnimation(animation.Finisher, playerAnimName, true);
            }
            else if (playerMove == "Recover")
            {
                playerAnimName += "Recover";
                transitionToAnimation(animation.Recover, playerAnimName, true);
            }
            else if (playerMove == "Kickout")
            {
                playerAnimName += "Kickout";
                transitionToAnimation(animation.Kickout, playerAnimName, true);
            }

        }

        #endregion



        #region oppo animating
        string oppoName = oppoRefGO.name;
        string oppoAnimName = oppoName+"_";
        if (enemyMove == "Attack")
        {
            //walk first
            //if()come back here list
            oppoAnimName += "Hit";
            transitionToAnimation(animation.Attack, oppoAnimName, false);
            if (playerAnimTiming == animationTiming.preAnim)
            {
                playerAnimRef.Play("Jade_Walk");

            }
        }
        else if (enemyMove == "Block")
        {
            oppoAnimName += "Block";
            transitionToAnimation(animation.Block, oppoAnimName, false);
        }
        else if (enemyMove == "Taunt")
        {
            oppoAnimName += "Taunt";
            transitionToAnimation(animation.Taunt, oppoAnimName, false);
        }
        else if (enemyMove == "Pin")
        {
            oppoAnimName += "Pin";
            transitionToAnimation(animation.Pin, oppoAnimName, false);
        }
        else if (enemyMove == "Sell")
        {
            oppoAnimName += "Sell";

            transitionToAnimation(animation.Sell, oppoAnimName, false);
        }
        else if (enemyMove == "Finisher")
        {
            oppoAnimName += "Hit";
            transitionToAnimation(animation.Finisher, oppoAnimName, false);
        }
        else if (enemyMove == "Recover")
        {
            oppoAnimName += "Recover";
            transitionToAnimation(animation.Recover, oppoAnimName, false);
        }
        else if (enemyMove == "Kickout")
        {
            oppoAnimName += "Kickout";
            transitionToAnimation(animation.Kickout, oppoAnimName, false);
        }
        #endregion

    }

    

    void transitionToAnimation(animation anim, string newAnimationName,bool isPlayer)
    {
        if (isPlayer)
        {
            if(currentPlayerAnim!=anim)
            {
                //Debug.Log(newAnimationName);
                playerAnimRef.Play(newAnimationName);
                currentPlayerAnim = anim;
                if(anim== animation.Attack || anim == animation.Finisher || anim == animation.Pin || anim == animation.Pinned)
                    playerAnimTiming = animationTiming.preAnim;
            }
        }
        else
        {
            if (currentOppoAnim != anim)
            {
                //Debug.Log("Oppo anim name:"+newAnimationName);

                oppoAnimRef.Play(newAnimationName);
                currentOppoAnim = anim;
                oppoAnimTiming = animationTiming.preAnim;

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

    //stops displaying all combat menu things except for the currently select and one in each direction
    void MenuCuffing()
    {
        if (matchState != MatchState.decisionPhase)
        {
            return;
        }

        if (currentCenterButton == 0)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (i != 0 && i != buttons.Length - 1 && i != 1)
                {
                    buttons[i].SetActive(false);
                }
                else
                {
                    buttons[i].SetActive(true);
                }
            }
        }
        else if (currentCenterButton == buttons.Length - 1)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (i != 0 && i != buttons.Length - 1 && i != buttons.Length-2)
                {
                    buttons[i].SetActive(false);
                }
                else
                {
                    buttons[i].SetActive(true);
                }
            }
        }
        else
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (i != currentCenterButton && i != currentCenterButton - 1 && i != currentCenterButton + 1)
                {
                    buttons[i].SetActive(false);
                }
                else
                {
                    buttons[i].SetActive(true);
                }
            }
        }

    }

    void endMatch(bool playerWin)
    {
        //dialogueManager.flowchartVariables[dialogueManager.goToNextIndex]. true;
        //Debug.Log(dialogueManager.flowchartVariables[dialogueManager.goToNextIndex]);
        //Debug.Log(dialogueManager.flowchartVariables[dialogueManager.goToNextIndex]);
        //Debug.Log(dialogueManager.flowchartVariables[dialogueManager.goToNextIndex].GetValue());

        matchState = MatchState.ending;
        endingText.GetComponent<MeshRenderer>().enabled = true;
        dialogueManager.Unpause();
        string nextDiag ="";
        switch (oppoRef.name)
        {
            case "Dante":
                nextDiag = "JadeRoom_Jade_Day4";
                if (playerWin)
                {
                    nextDiag += "_Poor";
                }
                else
                {
                    nextDiag += "_Well";

                }
                break;
            case "Knox":
                break;
            case "Ava":
                nextDiag = "JadeRoom_AvaRoute_Day12";
                break;
            case "Cassandra":
                nextDiag = "JadeRoom_AvaRoute_Day16";
                break;
            default:
                break;
        }
        // must load correct flowchart, but also get the correct day, so depending on the opponent, the next flowchart must but be the correct day.
        if (playerWin)
        {
            endingText.GetComponent<TextMesh>().text = "Jade wins!!";
        }
        else
        {
            endingText.GetComponent<TextMesh>().text = oppoRefGO.name +" wins!!";

        }
        dialogueManager.LoadFlowchart("MatchEnd", nextDiag, "Jade", oppoRef.name);
        
    }

    void updateCombatUI()
    {
        //scaling the stamina and audience meters to match their values
        staminaMeterFill.transform.localScale = new Vector3(Player.stamina / Player.maxStamina, staminaMeterFill.transform.localScale.y, 1f);
        audienceMeterFill.transform.localScale = new Vector3( (audienceInterest/100f)*audienceMeterFillStartingXScale , audienceMeterFill.transform.localScale.y, 1f);

        TimerGO.GetComponent<TextMesh>().text = (turnCount * 5)+"";
    }


}
