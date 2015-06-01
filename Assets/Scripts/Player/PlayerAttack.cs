using UnityEngine;
using System.Collections;
using XInputDotNetPure;


public class PlayerAttack : MonoBehaviour
{
    public static float attackTimer;
	public float cancelCharge;
    static float attackMove;
    public float pruebaCount;
    public float coolDown;
    public static float attackCount;
    public float finalTime;
    public int timecounter = 0;

    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;


	public GameObject chargeDetector;

	public static float stepCount;

    private Animator animator;

    public bool combatActivate = false;

	public GameObject attackBox;
	//public GameObject weaponTrail;

	public static float clickCount = 0;

	public float stamina;

	public bool enemyHit;

    // Use this for initialization
    void Start()
    {
		stamina = PlayerMovement.stamina;
        attackTimer = 0;
        finalTime = 0;
        coolDown = 1f;
        animator = GetComponent<Animator>();
		enemyHit = PlayerMovement.enemyHit;
    }

    // Update is called once per frame
    void Update()
    {
		/*if (PlayerMovement.combat)
			weaponTrail.SetActive (true);
		else
			weaponTrail.SetActive (false);
		/*if (attackCount > 0)
			weaponTrail.SetActive (true);
		else
			weaponTrail.SetActive (false);*/
		if (attackTimer == 0)
			attackBox.SetActive (false);

        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }

        prevState = state;
        state = GamePad.GetState(playerIndex);
        
        pruebaCount = attackCount;

		// >>>>>>>>>>>>>> ENEMY HIT TRUE <<<<<<<<<<<<<
        if (PlayerMovement.enemyHit) 
		{
			stepCount = 0;
			attackCount = 0;
			attackTimer = 0;
			clickCount = 0;
			animator.SetFloat ("Attack", 0);
			animator.SetBool ("Charge", false);
		} 

		// >>>>>>>>>>> ENEMY HIT FALSE <<<<<<<<<<
		else 
		{
			// >>>>>>>>>>> ATTACK TIMER COUNTER <<<<<<<<<<<<<
			if (clickCount > 0) 
			{
				if (clickCount == 7)
					attackTimer -= 7 * Time.deltaTime;
				else
					attackTimer += Time.deltaTime;
			} 
			else 
			{
				attackTimer = 0;
			}

			// >>>>>>>>>>> ATTACK STATE <<<<<<<<<<<<<<
			if (attackCount == 1) 
			{
				if ((attackTimer > 0.1f) && (attackTimer < 0.2f))
				{
					attackBox.SetActive (true);
					//weaponTrail.SetActive (true);
				}
				else 
				{
					attackBox.SetActive (false);
					//weaponTrail.SetActive (false);
				}

				if ((attackTimer >= 0.4f) && (attackTimer < 0.7f)) {
					if (clickCount >= 2) {
						animator.SetFloat ("Attack", 2);
						attackCount = 2;
						stepCount = 2;
						attackTimer = 0;
					}
				} else if (attackTimer > 0.7f) {
					animator.SetFloat ("Attack", 0);
					stepCount = 0;

					if (attackTimer > 0.9f) {
						attackCount = 0;
						attackTimer = 0;
						clickCount = 0;
						PlayerMovement.hit = false;
					}
				}


			} else if (attackCount == 2) 
			{
				if ((attackTimer > 0.05) && (attackTimer < 0.25))
					attackBox.SetActive (true);
				else
					attackBox.SetActive (false);

				if ((attackTimer >= 0.3f) && (attackTimer < 0.6f)) {
					if (clickCount == 3) {
						animator.SetFloat ("Attack", 3);
						attackCount = 3;
						stepCount = 3;
						attackTimer = 0;

					}
				} 
				else if (attackTimer >= 0.6f) {
					animator.SetFloat ("Attack", 0);
					stepCount = 0;

					if (attackTimer > 0.8f) {
						attackCount = 0;
						attackTimer = 0;
						clickCount = 0;
					}
				}
				
				
			} 
			else if (attackCount == 3) 
			{
				if ((attackTimer > 0.27f) && (attackTimer < 0.5f))
					attackBox.SetActive (true);
				else
					attackBox.SetActive (false);
				animator.SetFloat ("Attack", 0);

				if (attackTimer > 1f) 
				{
					animator.SetFloat ("Attack", 0);
					stepCount = 0;
					
					if (attackTimer > 1.2f) 
					{
						attackCount = 0;
						attackTimer = 0;
						clickCount = 0;
						PlayerMovement.hit = false;
					}
				}
			} 
			else if (attackCount == 6) 
			{
				if (attackTimer >= 1.5f)
					attackTimer = 1.5f;
			} 
			else if (attackCount == 7) 
			{

				if ((attackTimer < 0) || (ChargeDetector.hit))
				{
					animator.SetFloat ("Attack", 8);
					clickCount = 8;
					stepCount = 8;
					attackTimer = 0;
					attackCount = 8;
				}
			} 
			else if (attackCount == 8) 
			{

				animator.SetBool("Charge", false);
				animator.SetBool ("Fly", false);
				animator.SetFloat ("ChargeCounter", 0);

				chargeDetector.SetActive(false);

				ChargeDetector.hit = false;

				if ((attackTimer > 0) && (attackTimer < 0.25f))
					attackBox.SetActive (true);
				else
					attackBox.SetActive (false);

				if ((clickCount >= 2) && (clickCount <= 3))
				{
					if ((attackTimer > 0.4f) && (attackTimer < 0.7f))
					{
						animator.SetFloat ("Attack", 2);

						stepCount = 2;
						attackTimer = 0;
						attackCount = 2;

					}
				} 
				else if (attackTimer > 0.7f) 
				{
					animator.SetFloat ("Attack", 0);
				}
				if (attackTimer > 1f) 
				{
					stepCount = 0;
					attackCount = 0;
					attackTimer = 0;
					clickCount = 0;
					PlayerMovement.hit = false;
				}
			}

			// >>>>>>>>>> MOUSE CLICK COUNTER <<<<<<<<<<<<<<<

			if (PlayerMovement.combat) 
			{
				if (PlayerMovement.seatheCooldown == 0)
				{
					if(animator.GetBool("Grounded") == true)
					{
						if ((clickCount == 0) && (PlayerMovement.stamina > 20))
						{
							if ((Input.GetKeyDown (KeyCode.Mouse0)) || (prevState.Triggers.Left <= 0.7f && state.Triggers.Left > 0.7f)) 
							{
								animator.SetFloat ("Attack", 1);
								attackCount = 1;
								clickCount = 1;
								stepCount = 1;
							}
                            if ((Input.GetKeyDown(KeyCode.Mouse1)) || (prevState.Triggers.Right <= 0.7f && state.Triggers.Right > 0.7f)) 
							{
								animator.SetBool ("Charge", true);
								attackCount = 6;
								clickCount = 6;
								stepCount = 6;
							}
						} 
						else if ((clickCount == 1) && (PlayerMovement.stamina > 20))
						{
                            if ((Input.GetKeyDown(KeyCode.Mouse1)) || (prevState.Triggers.Right <= 0.7f && state.Triggers.Right > 0.7f)) 
							{
								clickCount = 2;
							}
						} 
						else if ((clickCount == 2) && (PlayerMovement.stamina > 20))
						{
                            if ((Input.GetKeyDown(KeyCode.Mouse0)) || (prevState.Triggers.Left <= 0.7f && state.Triggers.Left > 0.7f)) 
							{
								clickCount = 3;
							}
						} 
						else if (clickCount == 6) 
						{
							if (attackTimer < 0.6f)
							{
								if((Input.GetKeyUp(KeyCode.Mouse1)) || (prevState.Triggers.Right >= 0.3f && state.Triggers.Right < 0.3f))
								{
									animator.SetBool ("Charge", false);

								}
							}
							if ((animator.GetBool ("Charge") == false) && (attackTimer > 0.9f))
							{
								cancelCharge += Time.deltaTime;
								if(cancelCharge > 0.2f)
								{
									clickCount = 0;
									attackCount = 0;
									attackTimer = 0;
									stepCount = 0;
									cancelCharge = 0;
								}
							}
							if ((attackTimer > 0.6f) && (animator.GetBool("Charge") == true))
							{
								animator.SetFloat ("ChargeCounter", 1, 0.2f, Time.deltaTime);

                                if ((Input.GetKeyUp(KeyCode.Mouse1)) || (prevState.Triggers.Right >= 0.3f && state.Triggers.Right < 0.3f))
								{
									animator.SetBool ("Fly", true);

									attackCount = 7;
									clickCount = 7;
									stepCount = 7;
								}
							}
						} 
						else if ((clickCount == 8) && (PlayerMovement.stamina > 20))
						{
                            if ((Input.GetKeyDown(KeyCode.Mouse1)) || (prevState.Triggers.Right <= 0.7f && state.Triggers.Right > 0.7f))
								clickCount = 2;
						}
					}
					else if( PlayerMovement.grounded == false)
					{
						if(clickCount == 0)
						{

						}
					}
				}
			} 
			else 
			{
				animator.SetFloat ("Attack", 0);
				attackCount = 0;

			}
		}
    }
}