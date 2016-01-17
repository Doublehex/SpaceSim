using UnityEngine;
using System.Collections;

public class Starfield : MonoBehaviour {
	
	private Transform tx;
	private ParticleSystem.Particle[] points;
	
	public int starsMax = 20;
	public float starSize = 1;
	public float starDistance = 20;
	private float starDistanceSqr;
	
	
	// Use this for initialization
	void Start () {
		tx = transform;
		starDistanceSqr = starDistance * starDistance;
	}
	
	
	private void CreateStars() {
		points = new ParticleSystem.Particle[starsMax];
		
		for (int i = 0; i < starsMax; i++) {
			points[i].position = new Vector3 (tx.position.x + Random.Range(-20.0f, 20.0f), tx.position.y + Random.Range(-20.0f, 20.0f), 98.0f);
			points[i].color = new Color(1,1,1, 1);
			points[i].size = Random.Range(0.1f, 0.25f);
		}
	}
	
	
	// Update is called once per frame
	void Update () {
		if ( points == null ) CreateStars();
		
		for (int i = 0; i < starsMax; i++) {

			//Debug.Log (Vector3.Distance(points[i].position, tx.position));
			if (Vector3.Distance(points[i].position, tx.position) > 50.0f)
			{
				points[i].position = new Vector3 (tx.position.x + Random.Range(-20.0f, 20.0f), tx.position.y + Random.Range(-20.0f, 20.0f), 0);
			}
			/*if ((points[i].position - tx.position).sqrMagnitude > starDistanceSqr) {
				Debug.Log((points[i].position - tx.position).sqrMagnitude);
				//points[i].position = new Vector3 (tx.position.x + Random.Range(-20.0f, 20.0f), tx.position.y + Random.Range(-20.0f, 20.0f), 0);
			}*/
			
			/*if ((points[i].position - tx.position).sqrMagnitude <= starClipDistanceSqr) {
				float percent = (points[i].position - tx.position).sqrMagnitude / starClipDistanceSqr;
				points[i].color = new Color(1,1,1, percent);
				points[i].size = percent * starSize;
			}*/
			
			
		}
		
		GetComponent<ParticleSystem>().SetParticles ( points, points.Length );
		
	}
}