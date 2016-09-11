using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public float width = 8f;
	public float height = 6.5f;
	public float speed = 5f;
	public float padding = 0f;
	private float xmin;
	private float xmax;
	public float spawnDelay = 0.5f;
	
	// Use this for initialization
	void Start () {
		SpawnEnemyUntilFull();
		float distance = this.transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0.0f,0.0f,distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1.0f,0.0f,distance));
		xmin = leftmost.x + padding;
		xmax = rightmost.x - padding;
	}
	
	public void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 1f));
	}
	
	// Update is called once per frame
	void Update () {
		float new_x = Mathf.Clamp(this.transform.position.x + speed * Time.deltaTime, xmin+width/2, xmax-width/2);
		this.transform.position = new Vector3(new_x, this.transform.position.y, this.transform.position.z);
		if(new_x == xmin+width/2) speed = Mathf.Abs(speed);
		if(new_x == xmax-width/2) speed = -Mathf.Abs(speed);
		if(AllEnemyIsDead()){
			SpawnEnemyUntilFull();
		}
	}
	
	Transform NextFreePosition(){
		foreach(Transform childPostionGameObject in transform){
			if(childPostionGameObject.childCount == 0) return childPostionGameObject.transform;
		}
		return null;
	}
	
	bool AllEnemyIsDead(){
		foreach(Transform childPostionGameObject in transform){
			if(childPostionGameObject.childCount > 0) return false;
		}
		return true;
	}
	
	void SpawnEnemy(){
		foreach(Transform child in transform){
			GameObject enemy =  Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child.transform;
		}
	}
	
	void SpawnEnemyUntilFull(){
		Transform freePosition = NextFreePosition();
		if(freePosition != null){
			GameObject enemy =  Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition.transform;
			Invoke("SpawnEnemyUntilFull", spawnDelay);
		}
	}
}
