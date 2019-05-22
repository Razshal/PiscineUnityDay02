using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectionScript : MonoBehaviour {

    public GameObject enemyFocus;

    public void ThatWasASelectionClick()
    {
        foreach (Transform child in transform)
            child.GetComponent<playerScript>().ignoreClick = true;
    }

    public void UnselectAll()
    {
        foreach (Transform child in transform)
            child.GetComponent<playerScript>().selected = false;
    }

    public void selectEnemy(GameObject enemy)
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<playerScript>().selected)
                child.GetComponent<playerScript>().enemy = enemyFocus;
        }
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse1))
            UnselectAll();

        if (enemyFocus)
            selectEnemy(enemyFocus);
        else if (Input.GetMouseButtonDown(0) && !enemyFocus)
            selectEnemy(null);
        enemyFocus = null;
    }
}
