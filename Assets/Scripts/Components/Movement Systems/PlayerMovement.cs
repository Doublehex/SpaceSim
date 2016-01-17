using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		if (gameObject.GetComponent<Movement> () == null) {
			gameObject.AddComponent<Movement>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		/*Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
		Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos);
		lookPos = lookPos - gameObject.transform.position;
		float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
		gameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);*/
		
		Vector3 v;
		//Debug.Log (playerShip.transform.rotation.z);
		/*if (gameObject.transform.rotation.z >= 0.0f) {
			v = new Vector3 (Input.GetAxis ("Vertical"), -Input.GetAxis ("Horizontal"));
		} else {
			v = new Vector3 (Input.GetAxis ("Vertical"), Input.GetAxis ("Horizontal"));
		}*/

		v = new Vector3 (Input.GetAxis ("Vertical"), 0.0f);
		transform.Rotate(0.0f, 0.0f, -Input.GetAxis ("Horizontal") * 2.0f);

		gameObject.GetComponent<Movement> ().movement = v;

		Camera.main.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -50);
	}
}
