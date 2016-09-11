using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float Speed = 10.0f;
	public float padding = 0.5f;
	
	private float xmin;
	private float xmax;
	private float ymin = -4;
	private float ymax = 4;
	public GameObject projectile;
	public float projectileSpeed = 5f;
	public float fireRate = 0.1f;
	public float health = 500f;
	public AudioClip fireSound;
	public LevelManager levelManger; 
	// Use this for initialization
	void Start () {
		float distance = this.transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0.0f,0.0f,distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1.0f,0.0f,distance));
		xmin = leftmost.x + padding;
		xmax = rightmost.x - padding;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 RightShift = new Vector3(Speed * Time.deltaTime, 0.0f, 0.0f);
		Vector3 UpShift =  new Vector3(0.0f, Speed * Time.deltaTime, 0.0f);
		if(Input.GetKey(KeyCode.LeftArrow)){
			this.transform.position -= RightShift;
		}
		if(Input.GetKey(KeyCode.RightArrow)){
			this.transform.position += RightShift;
		}
		if(Input.GetKey(KeyCode.UpArrow)){
			this.transform.position += UpShift;
		}
		if(Input.GetKey(KeyCode.DownArrow)){
			this.transform.position -= UpShift;
		}
		float x_new = Mathf.Clamp(this.transform.position.x, xmin, xmax);
		float y_new = Mathf.Clamp(this.transform.position.y, ymin, ymax);
		this.transform.position = new Vector3(x_new, y_new, this.transform.position.z);
		if(Input.GetKeyDown(KeyCode.Space)){
			InvokeRepeating("Fire", 0.00001f, fireRate);
		}
		if(Input.GetKeyUp(KeyCode.Space)){
			CancelInvoke("Fire");
		}
	}
	
	void Fire(){
		GameObject beam =  Instantiate(projectile, this.transform.position, Quaternion.identity) as GameObject;
		beam.rigidbody2D.velocity = new Vector2(0f, projectileSpeed);
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}
	
	void OnTriggerEnter2D(Collider2D collider){
		Projectile2 missile = collider.gameObject.GetComponent<Projectile2>();
		if(missile){
			health -= missile.GetDamage();
			missile.Hit();;
			if(health <= 0){
				Destroy(gameObject);
				levelManger.LoadLevel("Win Screen");
			}
		}
	}
	
}
