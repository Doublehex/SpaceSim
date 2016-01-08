using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public Vector3 movement;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (movement != Vector3.zero) {
			ShipData data = gameObject.GetComponent<ShipData>();
			Rigidbody2D rigidBody = gameObject.GetComponent<Rigidbody2D> ();
			rigidBody.AddRelativeForce (movement.normalized * data.moveSpeed, ForceMode2D.Force);

			float y =  Mathf.Clamp(rigidBody.velocity.y, -data.maxSpeed, data.maxSpeed);
			float x =  Mathf.Clamp(rigidBody.velocity.x, -data.maxSpeed, data.maxSpeed);
			rigidBody.velocity = new Vector2(x, y);

			movement = Vector3.zero;
		}
	}
}
