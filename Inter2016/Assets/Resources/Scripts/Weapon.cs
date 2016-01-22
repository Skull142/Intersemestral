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
	LEFT,
	RIGHT,
	MIDDLE
}

public class Weapon : MonoBehaviour
{
	public float damage = 34f;
	public float reachability = 100f;
	public int charger = 21;
	public int maxCharger = 21;
	public int ammo = 40;
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
			if (Input.GetMouseButton (this.MouseButton (this.mouseButton)) && this.charger > 0 && this.ammo > 0) {
				print ( this.fireType );
				this.charger--;
				Ray ray = Camera.main.ScreenPointToRay (new Vector3 (Screen.width / 2, Screen.height / 2, 0f));
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit, this.reachability, this.layer, QueryTriggerInteraction.Collide)) {
					print (hit.collider.gameObject.name);
					hit.collider.gameObject.SendMessage ("TakeDamage", this.damage, SendMessageOptions.DontRequireReceiver);
				}
			} else 
			{
				if (this.charger < 1 && this.ammo > 0) 
				{
					
				}
			}
				break;
		}
	}

	private int MouseButton ( MouseButtonCode code )
	{
		int aux = 0;
		switch (code) 
		{
			case MouseButtonCode.LEFT:
				aux = 0;
				break;
			case MouseButtonCode.RIGHT:
				aux = 1;
				break;
			case MouseButtonCode.MIDDLE:
				aux = 2;
				break;
		}
		return aux;
	}

}
