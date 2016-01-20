using UnityEngine;
using System.Collections;

public class PlayerControl : LivingObject 
{
	public float speed;
	public Vector2 difVertical = new Vector2 (1f, -0.5f);
	public Vector2 difHorizontal = new Vector2 (1f, -0.5f);

	// Use this for initialization
	public override void Start () 
	{
		base.Start ();
	}

	public override void TakeDamage(float damage)
	{
		base.TakeDamage (damage);
		//correr animacion de daño
	}
	public override void Dead()
	{
		base.Dead ();
		//Animacion muerte
	}
	
	// Update is called once per frame
	public override void Update () 
	{
		//No es necesario hacer esto, Checar en fisicas.
		if (Input.GetKey (KeyCode.LeftShift))
			speed = 50f;
		else
			speed = 25f;
		float dirForward = Input.GetAxisRaw ("Vertical");
		float dirRight = Input.GetAxisRaw ("Horizontal");
		if ( dirForward !=0)
			this.transform.position += this.transform.forward * (dirForward >0 ? (this.difVertical.x):(this.difVertical.y))  * Time.deltaTime * speed; 
		if ( dirRight != 0) 	
			this.transform.position += this.transform.right * (dirRight >0 ? (this.difHorizontal.x):(this.difHorizontal.y))  * Time.deltaTime * speed;
	}
	private void TestMovimiento()
	{
		if (Input.GetAxis ("Vertical") != 0)
			this.transform.position += new Vector3 (0f, Input.GetAxis ("Vertical"), 0f) * speed * Time.deltaTime;
		print (Input.GetAxis ("Vertical"));
		if (Input.GetAxis ("Horizontal") != 0)
			this.transform.position += new Vector3 (Input.GetAxis ("Horizontal"), 0f, 0f) * speed * Time.deltaTime;
		print (Input.GetAxis ("Horizontal"));
		if (Input.GetKey (KeyCode.Space))
			this.transform.Rotate (Vector3.one * Time.deltaTime * speed);
	}
}
