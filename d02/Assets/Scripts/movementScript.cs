using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementScript : MonoBehaviour {
    private Vector3 target;
    private Vector3 rotation;
    private AudioSource audioSource;
    private Animator animator;
    public AudioClip aknowledge;

    public enum Status
    {
        STAY,
        MOVING
    }
    public Status status;

	// Use this for initialization
	void Start () {
        status = Status.STAY;
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
	}

    void PlayAudio(AudioClip clip) {
        audioSource.clip = clip;
        audioSource.Play();
    }
	
	// Update is called once per frame
	void Update () {
        // Setting rotation and start
        if (Input.GetMouseButtonDown(0)) {
            PlayAudio(aknowledge);
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            rotation = target - transform.position;
            rotation.Normalize();
            transform.rotation = 
                Quaternion.Euler(0f, 0f, Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg - 90);
            animator.SetBool("move", true);
        }
        if (Vector2.Distance(transform.position, target) > 0.01f)
        {
			transform.position = Vector2.MoveTowards(transform.position, target, 0.1f);
            status = Status.MOVING;
        }
        else if (status != Status.STAY) {
            status = Status.STAY;
            animator.SetBool("move", false);
        }
	}
}
