using UnityEngine;

public class Aldeano : MonoBehaviour
{
    public CharacterController controller;

    public Transform aldeano;
    public Transform enemy;

    public AnimationClip attack;
    public AnimationClip waitingforbattle;
    public AnimationClip run;
    public AnimationClip idle;
    public AnimationClip die;
    public AnimationClip finaldie;
    public AnimationClip getHit;

    public float range;
    public float speed;
    public float cdAttack;
    public float chargeAttack;
    public float finalDie;
    public float counterHit;

    private bool follow;
    private bool statusHealth = true;

    private AttackType attackCurrent;
    private Vector3 positionAttack;

    private enum AttackType { IDLE, ATTACK, DEAD, COMBAT, HIT };

    private void Chase()
    {
        /*
        Vector3 relativePos = samurai.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        */
        transform.LookAt(enemy.position);
        controller.SimpleMove(transform.forward * speed);
    }

    private bool InRange()
    {
        if (Vector3.Distance(transform.position, enemy.position) < range)
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
        cdAttack = 0;
        chargeAttack = 0;
        finalDie = 0;
        counterHit = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        float distance = Vector3.Distance(enemy.transform.position, transform.position);

        Vector3 dir = (enemy.transform.position - transform.position).normalized;

        float direction = Vector3.Dot(dir, transform.forward);

        PlayerHealth status = (PlayerHealth)aldeano.GetComponent("PlayerHealth");

        if (status.curHealth <= 0)
        {
            statusHealth = false;
            attackCurrent = AttackType.DEAD;
        }

        if (statusHealth)
        {
            cdAttack -= Time.deltaTime;

            if (cdAttack < 0) cdAttack = 0;

            if (distance < 1)
            {
                if (direction > 0)
                {
                    if (cdAttack == 0)
                    {
                        attackCurrent = AttackType.ATTACK;
                        cdAttack = 3;
                    }
                }
            }
            else if ((InRange()) && (distance > 1)) GetComponent<Animation>().CrossFade(run.name);
        }

        switch (attackCurrent)
        {
            case AttackType.IDLE:
                {
                    follow = true;
                    chargeAttack = 0;
                }
                break;

            case AttackType.ATTACK:
                {
                    follow = false;
                    EnemyHealth eh = (EnemyHealth)enemy.GetComponent("EnemyHealth");
                    //PlayerMovement sa = (PlayerMovement)samurai.GetComponent("PlayerMovement");
                    chargeAttack += Time.deltaTime;
                    GetComponent<Animation>().CrossFade(attack.name);

                    if (chargeAttack >= 1)
                    {
                        if (distance < 1)
                        {
                            eh.AddjustCurrentHealth(-3);
                        }
                        attackCurrent = AttackType.IDLE;
                    }
                }
                break;

            case AttackType.DEAD:
                {
                    GetComponent<Animation>().Play(die.name);
                    follow = false;
                    finalDie += Time.deltaTime;

                    if (finalDie >= 1) GetComponent<Animation>().Play(finaldie.name);
                }
                break;

            case AttackType.COMBAT:
                {
                    GetComponent<Animation>().CrossFade(waitingforbattle.name);
                }
                break;

            case AttackType.HIT:
                {
                    GetComponent<Animation>().CrossFade(getHit.name);
                    counterHit += Time.deltaTime;
                    if (counterHit >= 1) attackCurrent = AttackType.IDLE;
                }
                break;
        }

        if (statusHealth)
        {
            if (InRange())
            {
                if (follow) Chase();
            }
            else
            {
                attackCurrent = AttackType.IDLE;
                GetComponent<Animation>().CrossFade(idle.name);
            }
        }
    }
}