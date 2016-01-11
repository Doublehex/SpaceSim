using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public bool activated = true;
	public bool firing = false;
	LineRenderer line;
	public Material lineMaterial;
	Quaternion qTo;

	// Use this for initialization
	void Start () {
		qTo = gameObject.transform.localRotation;

		line = gameObject.GetComponent<LineRenderer> ();
		if (line == null) {
			line = gameObject.AddComponent<LineRenderer> ();
		}
		//line = GetComponent<LineRenderer>();
		line.SetVertexCount(2);
		//Material whiteDiffuseMat = new Material(Shader.Find("Unlit/Texture"));
		line.material = new Material(Shader.Find("Custom/Vertex Colors"));
		line.SetWidth(0.1f, 0.1f);
		line.SetColors (Color.red, Color.red);
	}

	public static GameObject CreateWeapon(string weaponName)
	{
		GameObject weapon = GameObject.Instantiate(Resources.Load(weaponName)) as GameObject;
		weapon.AddComponent<Weapon> ();

		return weapon;
	}

	public static GameObject CreateWeapon(string weaponName, GameObject parent, Vector3 relativeLocation)
	{
		GameObject weapon = CreateWeapon (weaponName);
		weapon.transform.SetParent (parent.transform);
		weapon.transform.localPosition = relativeLocation;
		weapon.layer = parent.layer;

		return weapon;
	}
	
	public void fire()
	{
		firing = true;
	}

	public void stopFiring()
	{
		line.enabled = false;
		firing = false;
	}

	// Update is called once per frame
	void LateUpdate () {

		if (activated) {

			Vector3 p2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3 p1 = gameObject.transform.position;
			float angle = Mathf.Atan2(p2.y - p1.y, p2.x - p1.x)*180 / Mathf.PI;

			Quaternion desired = Quaternion.AngleAxis(angle, Vector3.forward);
			gameObject.transform.localRotation = Quaternion.RotateTowards(gameObject.transform.localRotation, desired, 1.0f * Time.deltaTime);

			//Debug.Log(angle);
		}

		if (firing) {
			line.enabled = true;
			//line.sortingOrder = 1;

			Ray2D ray;
			//float distance = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position).magnitude;
			//ray = new Ray2D(gameObject.transform.position, (Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position)/ distance );
			ray = new Ray2D(gameObject.transform.position, gameObject.transform.right );

			line.SetPosition(0, new Vector3(ray.origin.x, ray.origin.y, 1.0f));
			
			int layerMask = ~(1<<LayerMask.NameToLayer("Player"));
			RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 10.0f, layerMask);
			
			if (hit.collider != null && hit.collider.gameObject != gameObject)
			{
				line.SetPosition(1, hit.point);
			}
			// Otherwise shoot freely
			else
			{
				Vector2 destination = ray.GetPoint(10.0f);
				line.SetPosition(1, new Vector3(destination.x, destination.y, 1.0f));
			}
			
		} else {
			line.enabled = false;
		}
	}
}
