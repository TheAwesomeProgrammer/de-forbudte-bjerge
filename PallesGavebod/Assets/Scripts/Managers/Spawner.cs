using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{

    public GameObject Coin;
    public GameObject Enemy;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SpawnThingsWhenButtonPressed(int pId)
    {
        GameObject[] tSpawnItems = GameObject.FindGameObjectsWithTag("SpawnItem");
       
                foreach (GameObject tSpawnItem in tSpawnItems)
                {
                    SpawnInterface tSpawnInterface = tSpawnItem.GetComponent<SpawnInterface>();
                    if(tSpawnInterface.Id == pId)
                    {
                       if(tSpawnInterface.SpawnType == ESpawnType.Coin)
                       {
                           Instantiate(Coin, tSpawnItem.transform.position, transform.rotation);
                       }
                       else if (tSpawnInterface.SpawnType == ESpawnType.Enemy)
                       {
                           GameObject tEnemy =  Instantiate(Enemy, tSpawnItem.transform.position, transform.rotation) as GameObject;
                           tEnemy.GetComponent<Enemy>().HasBeenHidding = true;
                       }
                       
                    }
                }
    }
}
