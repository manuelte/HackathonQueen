using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    //variables
    public float maxHealth = 3;
    public float speed = 6.0f;
    public float health;
    public float damageHandler = 1000;
    public bool isWeapon;
    float resourceCount;
    float xmin;
    float xmax;
    float ymin;
    float ymax;
    public GameObject weapon;

	// Use this for initialization
	void Start ()
    {
        health = maxHealth;
        float playerDepth = transform.position.z - Camera.main.transform.position.z;
        xmin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, playerDepth)).x;
        xmax = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, playerDepth)).x;
        ymin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, playerDepth)).y;
        ymax = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, playerDepth)).y;
    }

    void Attack() {
    	GameObject slash = Instantiate(weapon,transform.position,Quaternion.identity) as GameObject;
    	slash.transform.parent = this.transform;
		slash.transform.position += new Vector3(0.63f,0.16f,0f);
		slash.transform.Rotate(180.0f,0f,0f);
    }

	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.position = new Vector3 ((Mathf.Clamp((this.transform.position.x - speed * Time.deltaTime), (xmin + this.transform.localScale.x / 2), (xmax - this.transform.localScale.x / 2))), this.transform.position.y, this.transform.position.z);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.position = new Vector3((Mathf.Clamp((this.transform.position.x + speed * Time.deltaTime), (xmin + this.transform.localScale.x / 2), (xmax - this.transform.localScale.x / 2))), this.transform.position.y, this.transform.position.z);
        }

        if(Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.position = new Vector3(this.transform.position.x, (Mathf.Clamp((this.transform.position.y - speed * Time.deltaTime), (ymin + this.transform.localScale.y / 2), (ymax - this.transform.localScale.y / 2))), this.transform.position.z);
        }

        if(Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.position = new Vector3(this.transform.position.x, (Mathf.Clamp((this.transform.position.y + speed * Time.deltaTime), (ymin + this.transform.localScale.y / 2), (ymax - this.transform.localScale.y / 2))), this.transform.position.z);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isWeapon = true;
            Attack();
        }
    }

    //Shoot or gather whatever is in front
    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.CompareTag("enemy") == true)
        {
            Enemy enemy = trigger.gameObject.GetComponent<Enemy>();
            damageHandler -= enemy.damage;
            Debug.Log("Health: " + damageHandler.ToString());
        }

        if (trigger.CompareTag("resource") == true)
        {
            resourceCount++;
            Destroy(trigger.gameObject);
        }
    }
}
