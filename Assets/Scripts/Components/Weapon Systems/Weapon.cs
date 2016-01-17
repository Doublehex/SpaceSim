using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{

	public bool activated = true;
	public bool firing = false;
	LineRenderer line;
	public Material lineMaterial;
	Vector3 original;
	float zRotation;

	// Use this for initialization
	void Start ()
	{
		//qTo = gameObject.transform.localRotation;
		original = new Vector3 (0, 0, 90.0f);
		zRotation = 0.0f;

		line = gameObject.GetComponent<LineRenderer> ();
		if (line == null) {
			line = gameObject.AddComponent<LineRenderer> ();
		}

		line.SetVertexCount (2);
		line.material = new Material (Shader.Find ("Custom/Vertex Colors"));
		line.SetWidth (0.1f, 0.1f);
		line.SetColors (Color.red, Color.red);
	}

	public static GameObject CreateWeapon (string weaponName)
	{
		GameObject weapon = GameObject.Instantiate (Resources.Load (weaponName)) as GameObject;
		weapon.AddComponent<Weapon> ();

		return weapon;
	}

	public static GameObject CreateWeapon (string weaponName, GameObject parent, Vector3 relativeLocation)
	{
		GameObject weapon = CreateWeapon (weaponName);
		weapon.transform.SetParent (parent.transform);
		weapon.transform.localPosition = relativeLocation;
		weapon.layer = parent.layer;

		return weapon;
	}
	
	public void fire ()
	{
		firing = true;
	}

	public void stopFiring ()
	{
		line.enabled = false;
		firing = false;
	}

	// Update is called once per frame
/*	void LateUpdate ()
	{

		if (activated) {

			Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			//transform.localRotation = Quaternion.LookRotation (Vector3.forward, mousePos - transform.position);

			//zRotation = Mathf.Atan2 (mousePos.y - transform.position.y, mousePos.x - transform.position.x) * (180 / Mathf.PI);
			//zRotation = Mathf.Clamp (transform.localEulerAngles.z, original.z - 30.0f, original.z + 30.0f);
			
			//transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, transform.localEulerAngles.y, zRotation);

			Vector3 dir = mousePos - transform.position;
			dir.Normalize ();
			
			float zAngle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;// - 90;
			
			Quaternion desiredRot = Quaternion.Euler (0, 0, zAngle);
			
			transform.rotation = Quaternion.RotateTowards (transform.rotation, desiredRot, 60.0f * Time.deltaTime);

			zRotation = Mathf.Clamp (transform.localEulerAngles.z, original.z - 30.0f, original.z + 30.0f);
			transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, transform.localEulerAngles.y, zRotation);
		}

		if (firing) {
			line.enabled = true;
			//line.sortingOrder = 1;

			Ray2D ray;
			//float distance = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position).magnitude;
			//ray = new Ray2D(gameObject.transform.position, (Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position)/ distance );
			ray = new Ray2D (gameObject.transform.position, gameObject.transform.right);

			line.SetPosition (0, new Vector3 (ray.origin.x, ray.origin.y, -1.0f));
			
			int layerMask = ~(1 << LayerMask.NameToLayer ("Player"));
			RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, 10.0f, layerMask);
			
			if (hit.collider != null && hit.collider.gameObject != gameObject) {
				line.SetPosition (1, hit.point);
				//Debug.Log (hit.collider.gameObject);
			}
			// Otherwise shoot freely
			else {
				Vector2 destination = ray.GetPoint (10.0f);
				line.SetPosition (1, new Vector3 (destination.x, destination.y, -1.0f));
			}
			
		} else {
			line.enabled = false;
		}
	}*/
}
