using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

	public float health = 200;
	public GameObject projectile2;
	public float projectile2Speed = 5f;
	public float shotPerSecond = 2f;
	public int scoreValue = 150;
	private ScoreKeeper scoreKeeper;
	public AudioClip fireSound;
	public AudioClip deathSound;
	
	void Start(){
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>() as ScoreKeeper;
	}
	
	void OnTriggerEnter2D(Collider2D collider){
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if(missile){
			health -= missile.GetDamage();
			missile.Hit();
			if(health <= 0){
				AudioSource.PlayClipAtPoint(deathSound, transform.position);
				Destroy(gameObject);
				scoreKeeper.Score(scoreValue);
			}
		}
	}
	
	void Update(){
		float probability = Time.deltaTime * shotPerSecond;
		if(Random.value < probability){
			Fire();
			AudioSource.PlayClipAtPoint(fireSound, transform.position);
		}
	}
	
	
	void Fire(){
		GameObject beam =  Instantiate(projectile2, this.transform.position, Quaternion.identity) as GameObject;
		beam.rigidbody2D.velocity = new Vector2(0f, -projectile2Speed);
	}
}
