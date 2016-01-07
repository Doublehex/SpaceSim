using UnityEngine;
using System.Collections;

public class UniverseChunk : MonoBehaviour {

	static Object starSprite = Resources.Load ("Sprites/BackgroundStar");
	public int x, y;
	GameObject[] stars;
	public bool Loaded;
	// Use this for initialization
	void Start () {
	}

	public static UniverseChunk Create(int x, int y)
	{
		UniverseChunk chunk = new UniverseChunk ();

		chunk.stars = new GameObject[20];
		chunk.x = x;
		chunk.y = y;
		chunk.Loaded = false;

		return chunk;
	}

	public void LoadStars()
	{
		float scale;
		Loaded = true;
		//Random.seed = 987654321;
		
		for (int i = 0; i < Random.Range(10, 20); i++) {
			stars [i] = GameObject.Instantiate (starSprite) as GameObject;
			scale = Random.Range (0.1f, 0.75f);
			stars [i].transform.localScale = new Vector3 (scale, scale);
			stars [i].transform.position = new Vector3 ((x * 20) + Random.Range (0, 20), (y * 20) + Random.Range (0, 20));
			stars [i].gameObject.GetComponent<SpriteRenderer>().sortingOrder = -99;
		}
	}

	public void UnloadStars()
	{
		foreach (GameObject star in stars) {
			Destroy(star);
		}

		Loaded = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
