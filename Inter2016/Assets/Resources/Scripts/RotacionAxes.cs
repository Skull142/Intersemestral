using UnityEngine;
using System.Collections;

public class RotacionAxes : MonoBehaviour 
{
	public Vector2 axesMask = new Vector3(1f, 1f);
	public Vector2 sensMouse = new Vector2(100f, 100f);
	public Vector2 limitesAng = new Vector2 (45f, 180f);

	void Update ()
	{
		Vector3 aux = this.transform.eulerAngles;
		Vector2 mousePos = new Vector2(Input.GetAxis ("Mouse X"), Input.GetAxis ("Mouse Y"));
		aux.x += mousePos.y * this.axesMask.x * this.sensMouse.x * Time.deltaTime * -1;
		//
		this.transform.eulerAngles = aux;
		//print (this.transform.eulerAngles);
		//
		Vector3 aux2 = this.transform.eulerAngles;
		Vector2 mousePos2 = new Vector2(Input.GetAxis ("Mouse X"), Input.GetAxis ("Mouse Y"));
		aux2.y += mousePos2.x * this.axesMask.y * this.sensMouse.y * Time.deltaTime;
		//
		this.transform.eulerAngles = aux2;
		//print (this.transform.eulerAngles);
	}

	public void InClass () 
	{
		Vector2 mousePos = new Vector2(Input.GetAxis ("Mouse X"), Input.GetAxis ("Mouse Y"));
		this.transform.Rotate ( new Vector3(mousePos.y * this.axesMask.x, 0f, 0f) * -1 * this.sensMouse.x * Time.deltaTime );
		this.transform.Rotate (new Vector3(0f,  mousePos.x * this.axesMask.y ,0f) * this.sensMouse.y * Time.deltaTime );
		float angAcotadoX = Mathf.Clamp (this.transform.eulerAngles.x, this.limitesAng.x, 360f-this.limitesAng.x);
		float angAcotadoY = Mathf.Clamp (this.transform.eulerAngles.y, this.limitesAng.y, 360f-this.limitesAng.y);
		print (angAcotadoX +""+ angAcotadoY);
		this.transform.localEulerAngles = new Vector3 (angAcotadoX, angAcotadoY, this.transform.eulerAngles.z);
	}
}
