using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float width = 3.0f;
    public float height = 3.0f;
    public int health = 3;
    public int numberOfHits = 0;
    public int motionCount = 0;
    public int numberOfStepsBeforeChangingDirection = 20;
    float xmin;
    float ymin;
    float xmax;
    float ymax;
    public int damage = 100;
    int direction = 0; //0=up, 1=right, 2=down, 3=left
    public float speed = 1.0f;

	// Use this for initialization
	void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xmin = leftmost.x;
        xmax = rightmost.x;

        Vector3 top = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 bottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distance));
        ymin = top.y;
        ymax = bottom.y;

	}
	
	// Update is called once per frame
	void Update () {
		motionCount++;
		Debug.Log("Direction " + direction);
		if(direction == 0) {
			this.transform.position = new Vector3(this.transform.position.x, Mathf.Clamp(this.transform.position.y - (speed * Time.deltaTime), ymin, ymax), this.transform.position.z);
			if (this.transform.position.y - (height / 2) <= ymin || motionCount%numberOfStepsBeforeChangingDirection == 0)
            {
                direction = Random.Range(0,4);
            }

		} else if (direction == 1) {
			this.transform.position = new Vector3(Mathf.Clamp(this.transform.position.x + (speed * Time.deltaTime), xmin, xmax),this.transform.position.y , this.transform.position.z);
			if (this.transform.position.x + (width / 2) >= xmax || motionCount%numberOfStepsBeforeChangingDirection == 0)
            {
                direction = Random.Range(0,4);
            }

		} else if (direction == 2) {
			this.transform.position = new Vector3(this.transform.position.x, Mathf.Clamp(this.transform.position.y + (speed * Time.deltaTime), ymin, ymax), this.transform.position.z);
			if (this.transform.position.y + (height / 2) >= ymax || motionCount%numberOfStepsBeforeChangingDirection == 0)
            {
                direction = Random.Range(0,4);
            }
		} else {
			this.transform.position = new Vector3(Mathf.Clamp(this.transform.position.x - (speed * Time.deltaTime), xmin, xmax),this.transform.position.y , this.transform.position.z);
			if (this.transform.position.x - (width / 2) <= xmin || motionCount%numberOfStepsBeforeChangingDirection == 0)
            {
                direction = Random.Range(0,4);
            }

		}
	}

	void OnTriggerEnter2D(Collider2D trigger) {
		if(trigger.CompareTag("weapon")) {
			Destroy(this.gameObject);
		}
	}



}
