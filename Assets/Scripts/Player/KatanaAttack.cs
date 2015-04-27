using UnityEngine;

public class KatanaAttack : MonoBehaviour
{
    // Use this for initialization

	public float attackCount;
	public float attackTimer;

	public bool attackHit;

	//public WolfIA enemy;

    private void Start()
    {
	
    }

    // Update is called once per frame
    private void Update()
    {


		if (attackTimer == 0)
			attackHit = false;

		attackCount = PlayerAttack.attackCount;
		attackTimer = PlayerAttack.attackTimer;





	}

    private void OnTriggerEnter(Collider other)
    {
		WolfIA enemy = (WolfIA)other.GetComponent ("WolfIA");

		if(attackHit == false)
		{
			if (other.tag == "Wolf") 
		    {
			    if (attackCount == 1)
				{
					//enemy.Damage(-30);

					attackHit = true;
					Debug.Log ("HIT 1");
				}
						
				if (attackCount == 2)
				{
					//enemy.Damage(-30);
					attackHit = true;
					Debug.Log ("HIT 2");
				}
				else if (attackCount ==3)
				{
					if(attackTimer >= 0.4f)
					{
						//enemy.Damage(-50);
						Debug.Log ("HIT 3");
					}
				}




			}
		}
    }
}