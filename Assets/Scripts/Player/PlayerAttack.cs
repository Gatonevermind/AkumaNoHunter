using UnityEngine;
using System.Collections;
using XInputDotNetPure;


public class PlayerAttack : MonoBehaviour
{
    public static float attackTimer;
    public static float attackMove;
    public float pruebatiempo;
    public float coolDown;
    public float attackCount;
    public float finalTime;
    public int timecounter = 0;

    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    private Animator animator;

    public bool combatActivate = false;

    public bool activate;


    // Use this for initialization
    void Start()
    {
        attackTimer = 0;
        finalTime = 0;
        coolDown = 1f;
        animator = GetComponent<Animator>();
        activate = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }

        prevState = state;
        state = GamePad.GetState(playerIndex);
        
        pruebatiempo = attackTimer;

        if (activate)
        {

            if (((Input.GetKeyDown(KeyCode.Q)) || (state.Buttons.X == ButtonState.Pressed)) && (PlayerMovement.grounded == 0))
            {
                combatActivate = !combatActivate;
            }

            if (combatActivate)
            {
                if (attackTimer > 0)
                {
                    attackTimer += Time.deltaTime;

                }

                if ((attackTimer >= 0.6f) && (attackTimer < 0.8))
                {
                    if (attackCount == 1)
                    {
                        animator.SetFloat("Attack", 0);
                    }

                }
                else if (attackTimer >= 1.4f)
                {

                    if (attackCount == 1)
                    {
                        attackTimer = 0;
                        attackCount = 0;
                    }

                }

                if ((attackTimer >= 1.4f) && (attackTimer < 1.5f))
                {
                    if (attackCount == 2)
                    {
                        animator.SetFloat("Attack", 0);
                    }
                }
                else if (attackTimer >= 2.1f)
                {

                    if (attackCount == 2)
                    {
                        attackTimer = 0;
                        attackCount = 0;
                    }

                }

                if (attackTimer >= 2.1f)
                {
                    if (attackCount == 3)
                    {
                        animator.SetFloat("Attack", 0);
                    }
                }
                else if (attackTimer >= 2.6f)
                {
                    attackTimer = 0;
                    attackCount = 0;
                }

                if (PlayerMovement.seatheCooldown == 0)
                {


                    if (((Input.GetKeyDown(KeyCode.Mouse0)) || (state.Triggers.Right > 0)) && (attackCount == 0))
                    {
                        animator.SetFloat("Attack", 1);
                        attackCount = 1;
                        attackTimer = 0.1f;
                    }
                    else if (((Input.GetKeyDown(KeyCode.Mouse0)) || (state.Triggers.Right > 0)) && (attackCount == 1))
                    {
                        animator.SetFloat("Attack", 2);
                        attackCount = 2;
                    }
                    else if (((Input.GetKeyDown(KeyCode.Mouse0)) || (state.Triggers.Right > 0)) && (attackCount == 2))
                    {
                        animator.SetFloat("Attack", 3);
                        attackCount = 3;
                    }
                }
            }
            else
            {
                animator.SetFloat("Attack", 0);
                attackCount = 0;
            }

            if (attackCount == 3)
            {

                finalTime += Time.deltaTime;

                if (finalTime >= 1)
                {
                    attackCount = 0;
                    finalTime = 0;

                }

            }
            if (attackTimer >= 3f)
            {
                attackTimer = 0;
                attackCount = 0;
                animator.SetFloat("Attack", 0);
            }

        }
    }
}