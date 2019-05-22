using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerScript : MonoBehaviour {
    public GameObject entity;
    public GameObject entitiesContainer;
    private GameObject spawnedEntity;
    public bool isHuman = true;
    public float spawnTiming = 10;
    public float timer;

    void Start () {
        timer = spawnTiming;
	}
	
	void Update () {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = spawnTiming;
            // Create entity + move it to the entities handler
            spawnedEntity = Instantiate(entity,
                                        gameObject.transform.position,
                                        gameObject.transform.rotation);
            if (isHuman)
                spawnedEntity.transform.parent = entitiesContainer.transform;
        }

	}
}
