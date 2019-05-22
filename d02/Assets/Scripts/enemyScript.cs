using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour {

    public int life = 100;
    public selectionScript selectionScript;

	private void OnMouseDown()
	{
        selectionScript.enemyFocus = gameObject;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
