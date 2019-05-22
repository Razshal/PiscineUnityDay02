using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectionScript : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}

    public void ThatWasASelectionClick()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<playerScript>().ignoreClick = true;
        }
    }

    public void UnselectAll()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<playerScript>().selected = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse1))
            UnselectAll();
	}
}
