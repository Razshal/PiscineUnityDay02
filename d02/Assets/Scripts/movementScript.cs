using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementScript : MonoBehaviour {
    private Vector3 target;
    private Vector3 rotation;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            rotation = target - transform.position;
            rotation.Normalize();
            transform.rotation = 
                Quaternion.Euler(0f, 0f, Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg - 90);
        }
        if (Vector2.Distance(transform.position, target) > 0.01f)
            transform.position = Vector2.MoveTowards(transform.position, target, 0.1f);
	}
}
