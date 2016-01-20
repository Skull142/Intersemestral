using UnityEngine;
using System.Collections;

public enum FireType
{
	AUTO,
	MANUAL,
	SEMI,
}

public enum MouseButtonCode
{
	LEFT = 0,
	RIGHT,
	MIDDLE
}

public class Weapon : MonoBehaviour
{
	public float damage = 34f;
	public float reachability = 100f;
	public int charger = 20;
	public int maxAmmo = 360;
	public float rechargeTime = 0.5f;
	public FireType fireType = FireType.AUTO;
	public MouseButtonCode mouseButton = MouseButtonCode.LEFT;
	public LayerMask layer;
	public float firerate = 0.1f;
	public bool empty
	{
		get
		{
			return this.charger <= 0 ? true : false;
		}

	}
	public Transform boquilla;

	void Update()
	{
		switch( this.fireType )
		{
			case FireType.AUTO:
				if (Input.GetMouseButton (0)) 
				{
					Ray ray = new Ray (this.boquilla.position, this.boquilla.forward);
					RaycastHit hit;
					if (Physics.Raycast (ray, out hit, this.reachability, this.layer)) {
						print (hit.collider.gameObject.name);
						hit.collider.gameObject.SendMessage ("TakeDamage", this.damage, SendMessageOptions.DontRequireReceiver);
					}
				}
				break;
		}
	}

}
