using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public bool activated = true;
	public bool firing = true;
	LineRenderer line;
	public Material lineMaterial;

	// Use this for initialization
	void Start () {
		line = gameObject.GetComponent<LineRenderer> ();
		if (line == null) {
			line = gameObject.AddComponent<LineRenderer> ();
		}
		Debug.Log (line);
		//line = GetComponent<LineRenderer>();
		line.SetVertexCount(2);
		line.material = new Material (Shader.Find("Particles/Additive"));
		line.SetWidth(0.1f, 0.25f);
		line.SetColors (Color.red, Color.blue);
	}
	
	public void fire()
	{
		firing = true;
	}

	// Update is called once per frame
	void Update () {
	
		if (firing) {
			line.enabled = true;
			/*Vector3 mousePos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0);
			Vector3 lookPos = Camera.main.ScreenToWorldPoint (mousePos);
			//lookPos = lookPos; - gameObject.transform.position;
			lookPos.z = 0.0f;

			line.SetPosition (0, gameObject.transform.position);
			line.SetPosition (1, new Vector3(lookPos.normalized.x * 20.0f, lookPos.normalized.y * 20.0f, 0.0f));
			*/
			Ray2D ray = new Ray2D(gameObject.transform.position, gameObject.transform.right);

			line.SetPosition(0, ray.origin);
			// Calculate end point
			RaycastHit2D _hit = Physics2D.Raycast(ray.origin, ray.direction, 10.0f);
			Debug.Log (_hit.rigidbody);
			
			// If we impacted something, end the laser at its position
			if (_hit.collider != null && _hit.collider.gameObject != gameObject)
			{
				line.SetPosition(1, _hit.point);
			}
			// Otherwise shoot freely
			else
			{
				line.SetPosition(1, ray.GetPoint(10.0f));
			}
			
		} else {
			line.enabled = false;
		}
	}
}
