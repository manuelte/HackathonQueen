using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {

	public GameObject enemyPrefab;
	public float width = 10.0f;
	public float height = 4.0f;
	public float speed = 1.0f;
	public float spawnDelay = 0.5f;
	float xmin;
	float xmax;
	bool movingLeft = true;
	int bounces = 0;

	//Return an empty enemy position to spawn an enemy
	Transform NextFreePosition() {
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount == 0) {
				return childPositionGameObject;
			}
		}
		return null;
	}

	void SpawnUntilFull() {
		Transform freePosition = NextFreePosition ();
		if (freePosition) {
			GameObject enemy = Instantiate (enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}
		if (NextFreePosition ()) {
			Invoke ("SpawnUntilFull", spawnDelay);
		}
	}

	//Spawn the enemies on screen
	void SpawnEnemies() {
		foreach (Transform child in transform) {
			GameObject enemy = Instantiate (enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}


	}


	// Use this for initialization
	void Start () {
		//Get camera frame
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint (new Vector3 (0,0,distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint (new Vector3 (1,0,distance));
		xmin = leftmost.x;
		xmax = rightmost.x;

		//Spawn enemies
		//SpawnEnemies();
		SpawnUntilFull();
	}

	public void OnDrawGizmos() {
		Gizmos.DrawWireCube (transform.position, new Vector3 (width, height));
	}

	// Update is called once per frame
	void Update () {
		//Left-right movement
		if(movingLeft) {
			this.transform.position = new Vector3 (Mathf.Clamp (this.transform.position.x - (speed * Time.deltaTime), xmin, xmax),this.transform.position.y, this.transform.position.z);
			if(this.transform.position.x - (width/2) <= xmin) {
				movingLeft = false;
			}
		} else {
			this.transform.position = new Vector3 (Mathf.Clamp (this.transform.position.x + (speed * Time.deltaTime), xmin, xmax),this.transform.position.y, this.transform.position.z);
			if(this.transform.position.x + (width/2) >= xmax) {
				movingLeft = true;
				bounces++;
			}
		}

		//Move down every 3 cycles
		/*
		if(bounces > 0 && bounces%3 == 0){
			this.transform.position += Vector3.down;
			bounces = 0;
		}
		*/
		if (AllMembersAreDead ()) {
			SpawnUntilFull();
		}
	}

	//Check if there are enemies still alive
	bool AllMembersAreDead() {
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount > 0) {
				return false;
			}
		}
		return true;
	}

}
