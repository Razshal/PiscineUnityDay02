using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour {
    public Vector3 target;
    private Vector3 relativeTarget;
    private AudioSource audioSource;
    private Animator animator;
    private selectionScript selectionScript;
    private float radians;

	public GameObject enemy;
    public AudioClip aknowledge;
    public AudioClip fight;
	public bool ignoreClick = false;
    public bool selected = false;
    public float minDistance = 0.4f;

    public enum Status
    {
        STAY,
        ATTACK,
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

    private void ResetAnimations()
    {
        animator.SetBool("move", status == Status.MOVING);
        animator.SetBool("attack", status == Status.ATTACK);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject == enemy)
            status = Status.ATTACK;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (enemy && collision.gameObject == enemy)
            status = Status.MOVING;
    }

	void Update () {
        ResetAnimations();
        if (!ignoreClick && selected)
        {
            // Set appropriate rotation
            if (Input.GetMouseButtonDown(0))
            {
                PlayAudio(aknowledge);
                target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                status = Status.MOVING;
            }
        } 
        else if (ignoreClick)
            ignoreClick = false;
        
        // Set animation and translation
        if (status == Status.MOVING
            && (Vector2.Distance(transform.position, target) > minDistance || enemy))
        {
            if (enemy && !Input.GetMouseButtonDown(0))
                target = enemy.transform.position;
            relativeTarget = target - transform.position;
            radians = Mathf.Atan2(relativeTarget.y, relativeTarget.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.Euler(0f, 0f, radians);
            transform.position = Vector2.MoveTowards(transform.position, target, 0.1f);
        }
        else if (status != Status.ATTACK && !enemy)
            status = Status.STAY;
	}
}
