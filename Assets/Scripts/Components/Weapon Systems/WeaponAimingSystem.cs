using UnityEngine;
using System.Collections;

public enum AimingType
{
	Fixed,
	Gimbal,
	Turret
}

public class WeaponAimingSystem : MonoBehaviour {

	Vector3 original;
	float rotationMin;
	float rotationMax;

	// Use this for initialization
	void Start () {
		original = new Vector3 (0, 0, 0.0f);

		rotationMax = 15.0f;
		rotationMin = 15.0f;
	}

	void aimAtPoint(Vector3 point)
	{	
		Vector3 dir = point - transform.position;
		dir.Normalize ();
		
		float zAngle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
		
		Quaternion desiredRot = Quaternion.Euler (0, 0, zAngle);
		
		transform.rotation = Quaternion.RotateTowards (transform.rotation, desiredRot, 60.0f * Time.deltaTime);
		
		float clampedZRotation = Mathf.Clamp (transform.localEulerAngles.z, original.z - rotationMin, original.z + rotationMax);
		transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, transform.localEulerAngles.y, clampedZRotation);
	}

	// Update is called once per frame
	void Update () {
	

	}
}
