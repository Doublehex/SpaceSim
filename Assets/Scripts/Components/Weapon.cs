using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public bool activated = true;
	public bool firing = false;
	LineRenderer line;
	public Material lineMaterial;
	Quaternion original;
	private Vector3 dirXZ, forwardXZ, dirYZ, forwardYZ;
	public float leftExtent = 15.0f;
	public float rightExtent = 15.0f;
	public float upExtent = 0.0f;
	public float downExtent = 0.0f;

	// Use this for initialization
	void Start () {
		//qTo = gameObject.transform.localRotation;
		original = transform.rotation;

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

	float ClampAngle(float angle, float min, float max) {
		
		if (angle<90 || angle>270){       // if angle in the critic region...
			if (angle>180) angle -= 360;  // convert all angles to -180..+180
			if (max>180) max -= 360;
			if (min>180) min -= 360;
		}    
		angle = Mathf.Clamp(angle, min, max);
		if (angle<0) angle += 360;  // if angle negative, convert to 0..360
		return angle;
	}

	// Update is called once per frame
	void LateUpdate () {

		if (activated) {

			/*Vector3 lookTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3 dirToTarget = (lookTarget - transform.position);
			Vector3 originalForward = original * Vector3.forward;*/

			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			transform.localRotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);

			/*if (transform.localRotation.eulerAngles.z > 15.0f)
			{
				transform.localRotation = new Vector3(0.0f, 0.0f, 15.0f);
			}*/
			//transform.localRotation = new Quaternion(0, 0, ClampAngle(transform.localRotation.eulerAngles.z, original.eulerAngles.z - 40.0f, original.eulerAngles.z + 40.0f), 0.0f);


			Debug.Log ("Angle " + transform.rotation.eulerAngles.z);
			Debug.Log ("Local " + transform.localRotation.eulerAngles.z);
			Debug.Log ("Parent " + transform.parent.rotation.eulerAngles.z);
			//Debug.Log ("Clamped: " + clampedAngle);

			/*float rotationAmount = 15.0f;
			Vector3 p2 = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Vector3 p1 = gameObject.transform.position;
			float angle = Mathf.Atan2 (p2.y - p1.y, p2.x - p1.x) * 180 / Mathf.PI;
			float angleDifference = (gameObject.transform.rotation.eulerAngles.z - gameObject.transform.parent.rotation.z);
			if (angleDifference < rotationAmount || (angle < rotationAmount && angleDifference < 180.0f))
			{
				Quaternion desired = Quaternion.AngleAxis (angle, Vector3.forward);
				gameObject.transform.localRotation = Quaternion.RotateTowards (gameObject.transform.localRotation, desired, 30.0f * Time.deltaTime);
			}
			else if (angleDifference > 180.0f)
			{
				if (Mathf.Abs(angleDifference) > (360.0f - rotationAmount) || (angle > -rotationAmount && angleDifference > 180.0f))
				{
					Quaternion desired = Quaternion.AngleAxis (angle, Vector3.forward);
					gameObject.transform.localRotation = Quaternion.RotateTowards (gameObject.transform.localRotation, desired, 30.0f * Time.deltaTime);
				}
			}

			Debug.Log("Angle: "+ angle);
			Debug.Log ("Diff: " + angleDifference);*/
		}

		if (firing) {
			line.enabled = true;
			//line.sortingOrder = 1;

			Ray2D ray;
			//float distance = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position).magnitude;
			//ray = new Ray2D(gameObject.transform.position, (Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position)/ distance );
			ray = new Ray2D(gameObject.transform.position, gameObject.transform.right );

			line.SetPosition(0, new Vector3(ray.origin.x, ray.origin.y, -1.0f));
			
			int layerMask = ~(1<<LayerMask.NameToLayer("Player"));
			RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 10.0f, layerMask);
			
			if (hit.collider != null && hit.collider.gameObject != gameObject)
			{
				line.SetPosition(1, hit.point);
				Debug.Log (hit.collider.gameObject);
			}
			// Otherwise shoot freely
			else
			{
				Vector2 destination = ray.GetPoint(10.0f);
				line.SetPosition(1, new Vector3(destination.x, destination.y, -1.0f));
			}
			
		} else {
			line.enabled = false;
		}
	}
}
