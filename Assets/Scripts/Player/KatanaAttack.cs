using UnityEngine;

public class KatanaAttack : MonoBehaviour
{
    // Use this for initialization
    public float combatTime;

    public bool combatActive;

    private PlayerAttack combatActivate;

    private void Start()
    {
        combatTime = 0;
        combatActive = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0)) combatActive = true;

        if ((Input.GetKey(KeyCode.Mouse0)) && combatActive) combatTime = 0;

        if (combatActive)
        {
            combatTime += 60 * Time.deltaTime;

            if (combatTime >= 120)
            {
                combatTime = 0;
                combatActive = !combatActive;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            EnemyHealth enemy = (EnemyHealth)other.GetComponent("EnemyHealth");
            if (combatTime != 0)
            {
                enemy.AddjustCurrentHealth(-30);
            }
        }
    }
}