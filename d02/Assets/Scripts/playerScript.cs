using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour {
    private Vector3 target;
    private Vector3 rotation;
    private AudioSource audioSource;
    private Animator animator;
    private selectionScript selectionScript;

    public bool ignoreClick = false;
    public AudioClip aknowledge;
    public bool selected = false;

    public enum Status
    {
        STAY,
        MOVING
    }
    public Status status;

	void Start () {
        status = Status.STAY;
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        selectionScript = transform.parent.gameObject.GetComponent<selectionScript>();
	}

    void PlayAudio(AudioClip clip) {
        audioSource.clip = clip;
        audioSource.Play();
    }

	void OnMouseDown()
	{
        selectionScript.ThatWasASelectionClick();
        if (Input.GetKey(KeyCode.LeftControl))
            selected = true;
        else
        {
            selectionScript.UnselectAll();
            selected = true;
        }
	}

	void Update () {
        if (!ignoreClick && selected)
        {
            // Set appropriate rotation
            if (Input.GetMouseButtonDown(0))
            {
                PlayAudio(aknowledge);
                target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                rotation = target - transform.position;
                rotation.Normalize();
                transform.rotation =
                    Quaternion.Euler(0f, 0f, Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg - 90);
                status = Status.MOVING;
            }
        } 
        else if (ignoreClick)
            ignoreClick = false;
        
        // Set animation and translation
        if (status == Status.MOVING && Vector2.Distance(transform.position, target) > 0.3f)
        {
            animator.SetBool("move", true);
            transform.position = Vector2.MoveTowards(transform.position, target, 0.1f);
        }
        else if (status != Status.STAY)
        {
            status = Status.STAY;
            animator.SetBool("move", false);
        }
	}
}
