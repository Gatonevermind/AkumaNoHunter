﻿using UnityEngine;

public class Mob : MonoBehaviour
{
    public CharacterController controller;

    public Transform enemy;
    public Transform samurai;

    public AnimationClip attack;
    public AnimationClip waitingforbattle;
    public AnimationClip run;
    public AnimationClip idle;
    public AnimationClip die;
    public AnimationClip jump;
    public AnimationClip finaldie;
    public AnimationClip getHit;

    public float range;
    public float speed;
    public float cdBlow;
    public float cdJump;
    public float cdNibble;
    public float chargeJump;
    public float timeJump;
    public float chargeBlow;
    public float chargeNibble;
    public float finalDie;
    public float counterHit;

    private bool follow;
    private bool statusHealth = true;

    private AttackType attackCurrent;
    private Vector3 positionAttack;

    private enum AttackType { IDLE, JUMP, BLOW, NIBBLE, DEAD, COMBAT, HIT };

    private void Chase()
    {
        /*
        Vector3 relativePos = samurai.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        */

        transform.LookAt(samurai.position);
        controller.SimpleMove(transform.forward * speed);
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
        cdNibble = 0;
        cdBlow = 0;
        cdJump = 0;
        timeJump = 0;
        chargeJump = 2;
        chargeNibble = 0;
        chargeBlow = 0;
        finalDie = 0;
        counterHit = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        float distance = Vector3.Distance(samurai.transform.position, transform.position);

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

            if (distance < 1)
            {
                if (direction > 0)
                {
                    if (cdBlow == 0)
                    {
                        attackCurrent = AttackType.BLOW;
                        cdBlow = 6;
                    }
                }
            }
            if ((InRange()) && (distance > 4))
            {
                if (cdJump == 0)
                {
                    positionAttack = samurai.transform.position;
                    attackCurrent = AttackType.JUMP;
                    cdJump = 5;
                    chargeJump = 2;
                }
            }
            else if ((InRange()) && (distance > 1)) GetComponent<Animation>().CrossFade(run.name);
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
                    //PlayerMovement sa = (PlayerMovement)samurai.GetComponent("PlayerMovement");
                    chargeBlow += Time.deltaTime;
                    GetComponent<Animation>().CrossFade(attack.name);

                    if (chargeBlow >= 1)
                    {
                        if (distance < 1)
                        {
                            eh.AddjustCurrentHealth(-3);
                        }
                        attackCurrent = AttackType.IDLE;
                    }
                }
                break;

            case AttackType.NIBBLE:
                {
                    follow = false;
                    PlayerHealth eh = (PlayerHealth)samurai.GetComponent("PlayerHealth");
                    //PlayerMovement sa = (PlayerMovement)samurai.GetComponent("PlayerMovement");
                    chargeNibble += Time.deltaTime;
                    GetComponent<Animation>().CrossFade(attack.name);

                    if (chargeNibble >= 1)
                    {
                        if (distance < 1f)
                        {
                            eh.AddjustCurrentHealth(-8);
                        }
                        attackCurrent = AttackType.IDLE;
                    }
                }
                break;

            case AttackType.JUMP:
                {
                    follow = false;
                    PlayerHealth eh = (PlayerHealth)samurai.GetComponent("PlayerHealth");
                    chargeJump -= Time.deltaTime;
                    if (chargeJump <= 1.45f) GetComponent<Animation>().CrossFade(jump.name);
                    if (chargeJump <= 0)
                    {
                        transform.position = Vector3.Lerp(transform.position, positionAttack, 0.04f);
                        timeJump += Time.deltaTime;
                    }
                    if (timeJump >= 1)
                    {
                        attackCurrent = AttackType.IDLE;
                        if (distance < 1)
                        {
                            eh.AddjustCurrentHealth(-15);
                        }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "sword")
        {
            attackCurrent = AttackType.HIT;
        }
    }
}