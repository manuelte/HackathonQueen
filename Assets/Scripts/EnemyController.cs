using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	public Sprite[] hitSprites;
	public float maxHealth;
	public float health;
	public int damage = 100;
	public GameObject enemyProjectile;
	public float enemyProjectileSpeed = 12.0f;
	public float enemyProjectileFrequency = 0.1f;


	/*void LoadSprites(){
		int spriteIndex = (int)Mathf.Clamp((maxHealth - health),0f,maxHealth-1.0f);
		this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
	}*/

	void OnTriggerEnter2D(Collider2D trigger) {
		if (trigger.CompareTag ("immune") == true) {
			Projectile projectile = trigger.gameObject.GetComponent<Projectile> ();
			health -= projectile.damage;
			Destroy (trigger.gameObject);
			if (health <= 0) {
				Destroy (this.gameObject);
			}
			//LoadSprites ();
		}
	}

	void Fire() {
		//GetComponent<AudioSource>().Play();
		GameObject enemyBeam = Instantiate(enemyProjectile,transform.position,Quaternion.identity) as GameObject;
		enemyBeam.transform.position += new Vector3(0f,-0.5f,0f);
		enemyBeam.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, -enemyProjectileSpeed, 0);
	}

	void Start () {
		health = maxHealth;
		//LoadSprites ();
	}

	void Update () {

		float chanceToFire = Time.deltaTime * enemyProjectileFrequency;
		if (Random.value <= chanceToFire) {
			Fire ();
		}
	}
}
