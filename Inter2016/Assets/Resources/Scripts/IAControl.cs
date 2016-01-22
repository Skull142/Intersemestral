using UnityEngine;
using System.Collections;

public enum IAState
{
	IDLE, 
	CHASING,
	ATTACKING,
	DEAD
}

[RequireComponent(typeof(NavMeshAgent), typeof(CapsuleCollider))]
public class IAControl : LivingObject 
{
	public Transform target;
	public float speed2Look = 50f;
	public float speed2Walk = 1.5f;
	private Vector3 distancia;
	public float stopingDistance = 2f;
	public float fieldOfViewRadius = 10f;
	public IAState state;
	//
	private NavMeshAgent agent;
	private Animator animator;
	private CapsuleCollider coll;
	public float time2Refresh = 0.1f;

	// Use this for initialization
	public override void Start () 
	{
		base.Start ();
		this.state = IAState.IDLE;
		this.agent = this.GetComponent<NavMeshAgent> ();
		this.animator = this.GetComponent<Animator> ();
		this.coll = this.GetComponent<CapsuleCollider> ();
		this.agent.stoppingDistance = this.stopingDistance;
		this.agent.speed = this.speed2Walk;
		this.agent.angularSpeed = this.speed2Look;
		this.agent.SetDestination (this.target.position);
		this.StartCoroutine ("CalculatePath");
	}
	
	// Update is called once per frame
	public override void Update()
	{
		if( !base.dead )
			this.CheckState ();
	}

	void LateUpdate()
	{
		this.animator.SetFloat ("SPEED", this.agent.speed);
	}
	public override void TakeDamage(float damage)
	{
		print (this.name+": RECIBE "+damage);
		base.health -= damage;
		if (base.health <= 0 && !base.dead) 
		{
			this.dead = true;
			this.Dead ();
		}
	}
	public override void Dead()
	{
		this.coll.enabled = false;
		print (this.name+": MUERTO");
		//
		this.StopAllCoroutines ();
		this.agent.Stop ();
		this.animator.SetTrigger ( "DEAD" );
		this.state = IAState.DEAD;
		//this.GetComponent<MeshRenderer> ().material.color = new Color (0f, 0f, 0f, 1f);
		//this.GetComponent<MeshRenderer> ().material.color = new Color32 (0, 0, 0, 255);
		base.Dead ();
		//Animacion muerte
	}

	public IEnumerator CalculatePath()
	{
		while ( this.state != IAState.DEAD && !base.dead ) 
		{
			this.RecalculatePath ();
			yield return new WaitForSeconds(this.time2Refresh);
		}
		yield break;
	}

	public void RecalculatePath()
	{
		NavMeshPath path= new NavMeshPath();
		this.agent.CalculatePath (this.target.position, path);
		this.agent.path = path;

	}

	void CheckState () 
	{
		distancia = this.target.position - this.transform.position;
		if (Close2You ()) 
		{
			if (this.state != IAState.CHASING) 
			{
				this.agent.speed = 3f;
				this.state = IAState.CHASING;
				this.agent.Resume();
			}
		}
		if (Close2Target ()) 
		{	
			if (this.state != IAState.ATTACKING) 
			{
				this.agent.speed = 0f;
				this.state = IAState.ATTACKING;
				this.agent.Stop();
			}
		}
	}

	void OldClass()
	{
		switch (this.state) 
		{
			case IAState.CHASING:
				this.CalculateAngle ();
				this.Translate2Target ();
				break;
			case IAState.ATTACKING:
				this.CalculateAngle ();
				break;
			case IAState.IDLE:
				break;
		}
	}
	private void CalculateAngle()
	{
		
		float ang = Mathf.Atan2 (distancia.y, distancia.x) * Mathf.Rad2Deg;
		this.transform.eulerAngles = new Vector3 (this.transform.eulerAngles.x, Mathf.LerpAngle (this.transform.eulerAngles.y, ang, Time.deltaTime * this.speed2Look), this.transform.eulerAngles.z); 
	}

	private void Translate2Target()
	{
		this.transform.position = Vector3.Lerp (this.transform.position, this.target.position, Time.deltaTime * speed2Walk);
	}

	private bool Close2Target()
	{
		return this.distancia.magnitude <= this.stopingDistance ? true : false;
			
	}

	private bool Close2You()
	{
		return this.distancia.magnitude <= this.fieldOfViewRadius ? true : false;
	}
}
