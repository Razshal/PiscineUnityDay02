using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour {
    public int life;
    public int maxLife = 100;
    public string unitName = "Orc Unit";
    public selectionScript selectionScript;
    public bool isBuilding = false;
    public bool isFriendly = false;
    public GameObject townCenter;

	private void OnMouseDown()
	{
        selectionScript.enemyFocus = gameObject;
	}

    public void ReceiveDamages(int damage)
    {
        life -= damage;
        if (life <= 0)
        {
            if (townCenter == gameObject && !isFriendly)
                Debug.Log("The Human Team wins.");
            else if (gameObject.tag == "OrcBuilding")
                townCenter.GetComponentInChildren<spawnerScript>().spawnTiming += 2.5f;
			Destroy(gameObject);
        }
        else
            Debug.Log(unitName + " [" + life + "/" + maxLife + "]HP has been attacked");
    }

	void Start () {
        life = maxLife;
        selectionScript = FindObjectOfType<selectionScript>();
	}
}
