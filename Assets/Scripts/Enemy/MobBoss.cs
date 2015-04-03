using UnityEngine;

public class MobBoss : MonoBehaviour
{
    public CharacterController controller;

    public Transform enemy;
    public Transform samurai;

    public AnimationClip blow;
    public AnimationClip nibble;
    public AnimationClip waitingforbattle;
    public AnimationClip run;
    public AnimationClip idle;
    public AnimationClip die;
    public AnimationClip jump;
    public AnimationClip finaldie;

    public float range;
    public float speed;
    public float cdBlow;
    public float cdJump;
    public float cdNibble;
    public float chargeJump;
    public float timeJump;
    public float chargeBlow;
    public float chargeNibble;

    public float distance;
    public float finalDie;

    private int rotationSpeed = 3;

    private bool follow;
    private bool statusHealth = true;

    private AttackType attackCurrent;
    private Vector3 positionAttack;

    private enum AttackType { IDLE, JUMP, BLOW, NIBBLE, DEAD, COMBAT, ROTATION };

    private void Chase()
    {
        Vector3 relativePos = new Vector3(samurai.position.x, this.transform.position.y, samurai.position.z);
        //Quaternion rotation = Quaternion.LookRotation(relativePos);
        this.transform.LookAt(relativePos);
        //transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);


        controller.SimpleMove(transform.forward * speed);
    }

    private void Rotation()
    {
        Vector3 relativePos = (samurai.position - transform.position);
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    private bool InRange()
    {
        if (Vector3.Distance(transform.position, samurai.position) < range)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Use this for initialization
    private void Start()
    {
        attackCurrent = AttackType.IDLE;
        cdNibble = 5;
        cdBlow = 2;
        cdJump = 0;
        timeJump = 0;
        chargeJump = 5;
        chargeNibble = 0;
        chargeBlow = 0;
        finalDie = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        distance = Vector3.Distance(samurai.transform.position, transform.position);

        Vector3 dir = (samurai.transform.position - transform.position).normalized;

        float direction = Vector3.Dot(dir, transform.forward);

        EnemyHealth status = (EnemyHealth)enemy.GetComponent("EnemyHealth");

        if (status.curHealth <= 0)
        {
            statusHealth = false;
            attackCurrent = AttackType.DEAD;
        }

        if (statusHealth)
        {
            cdNibble -= Time.deltaTime;
            cdBlow -= Time.deltaTime;
            cdJump -= Time.deltaTime;

            if (cdNibble < 0) cdNibble = 0;
            if (cdBlow < 0) cdBlow = 0;
            if (cdJump < 0) cdJump = 0;
            if (chargeJump < 0) chargeJump = 0;

            if (distance < 3)
            {
                //follow = false;
                if (direction < 3f)
                {
                    if (cdBlow == 0)
                    {
                        attackCurrent = AttackType.BLOW;
                        cdBlow = 2;
                    }
                    else if (cdNibble == 0)
                    {
                        attackCurrent = AttackType.NIBBLE;
                        cdNibble = 5;
                    }
                }
            }
            if ((InRange()) && (distance > 8))
            {
                if (cdJump == 0)
                {
                    positionAttack = samurai.transform.position;
                    attackCurrent = AttackType.JUMP;
                    cdJump = 5;
                    chargeJump = 2;
                }
            }
            else if ((InRange()) && (distance > 4f)) GetComponent<Animation>().CrossFade(run.name);
        }

        switch (attackCurrent)
        {
            case AttackType.IDLE:
                {
                    follow = true;
                    chargeJump = 0;
                    chargeNibble = 0;
                    chargeBlow = 0;
                    timeJump = 0;
                }
                break;

            case AttackType.BLOW:
                {
                    follow = false;
                    PlayerHealth eh = (PlayerHealth)samurai.GetComponent("PlayerHealth");
                    chargeBlow += Time.deltaTime;
                    GetComponent<Animation>().CrossFade(blow.name);

                    if (chargeBlow >= 2)
                    {
                        if (distance < 3f)
                        {
                            eh.AddjustCurrentHealth(-6);
                            attackCurrent = AttackType.IDLE;
                        }
                        else if (distance > 3f)
                        {
                            eh.AddjustCurrentHealth(0);
                            attackCurrent = AttackType.IDLE;
                        }
                    }
                }
                break;

            case AttackType.NIBBLE:
                {
                    follow = false;
                    PlayerHealth eh = (PlayerHealth)samurai.GetComponent("PlayerHealth");
                    chargeNibble += Time.deltaTime;
                    GetComponent<Animation>().CrossFade(nibble.name);

                    if (chargeNibble >= 2)
                    {
                        if (distance < 3f)
                        {
                            eh.AddjustCurrentHealth(-12);
                            attackCurrent = AttackType.IDLE;
                        }
                        else if (distance > 3f)
                        {
                            eh.AddjustCurrentHealth(0);
                            attackCurrent = AttackType.IDLE;
                        }
                    }
                }
                break;

            case AttackType.JUMP:
                {
                    follow = false;
                    PlayerHealth eh = (PlayerHealth)samurai.GetComponent("PlayerHealth");
                    PlayerMovement sa = (PlayerMovement)samurai.GetComponent("PlayerMovement");
                    chargeJump -= Time.deltaTime;
                    if (chargeJump <= 5)
                    {
                        GetComponent<Animation>().CrossFade(jump.name);
                        Vector3 relativePos = samurai.position - transform.position;
                        Quaternion rotation = Quaternion.LookRotation(relativePos);
                        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
                    }
                    if (chargeJump <= 0)
                    {
                        transform.position = Vector3.Lerp(transform.position, positionAttack, 0.02f);
                        timeJump += Time.deltaTime;
                    }
                    if (timeJump >= 0.7f)
                    {
                        if (distance < 4f)
                        {
                            eh.AddjustCurrentHealth(-20);
                            sa.stun = true;
                        }
                        Debug.Log("pepepe");
                        attackCurrent = AttackType.IDLE;
                    }
                }
                break;

            case AttackType.DEAD:
                {
                    follow = false;
                    GetComponent<Animation>().CrossFade(die.name);
                    finalDie += Time.deltaTime;

                    if (finalDie >= 4) GetComponent<Animation>().Play(finaldie.name);
                }
                break;

            case AttackType.COMBAT:
                {
                    GetComponent<Animation>().CrossFade(waitingforbattle.name);
                }
                break;
        }

        if (statusHealth)
        {
            if (InRange())
            {
                if ((distance > 3) && follow) Chase();
                if ((distance <= 3) && follow) Rotation();
            }
            else
            {
                attackCurrent = AttackType.IDLE;
                GetComponent<Animation>().CrossFade(idle.name);
            }
        }
    }
}