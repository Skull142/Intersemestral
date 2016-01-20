using UnityEngine;
using System.Collections;

public class LivingObject : MonoBehaviour,IDamageable 
{
	public float health = 100f;
	public bool dead;

	public virtual void Start()
	{
		this.dead = false;
	}



	// Update is called once per frame
	public virtual void Update () {
	
	}

	public virtual void TakeDamage(float damage)
	{
		this.health -= damage;
		if (this.health <= 0 && !this.dead) 
		{
			this.dead = true;
			this.Dead ();
		}
	}
	public virtual void Dead()
	{
		Destroy ( this.gameObject, 3f);
	}
}
