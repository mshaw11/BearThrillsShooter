//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Level : MonoBehaviour {
//    public Transform memberPrefab;
//    public Transform enemyPrefab;
//    public int numberOfMembers;
//    public int numberOfEnemies;
//    public List<Member> members;
//    public List<Enemy> enemies;
//    public float bounds;
//    public float spawnRadius;

//	// Use this for initialization
//	void Start()
//    {
//        members = new List<Member>();
//        enemies = new List<Enemy>();

//        //spawn members
//        Spawn(memberPrefab, numberOfMembers);
//        Spawn(enemyPrefab, numberOfEnemies);

//        members.AddRange(FindObjectsOfType<Member>());
//        enemies.AddRange(FindObjectsOfType<Enemy>());
//    }
	
//    void Spawn(Transform prefab, int count)
//    {
//        for (int i = 0; i < count; i++)
//        {
//            //Instantiate(prefab, new Vector3(Random.Range(-spawnRadius, spawnRadius), Random.Range(-spawnRadius, spawnRadius), 0), Quaternion.identity);
//            Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
//        }
//    }




//}
