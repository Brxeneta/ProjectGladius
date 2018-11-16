using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour {

    public Transform player;
    private float speed = 4;
    private float rotSpeed = 80;
    private float rot = 0;
    private float gravity = 8;
    private float attackcd = 2;

    Vector3 moveDir = Vector3.zero;

    CharacterController scontroller;
    Animator sanim;

    // Use this for initialization
    void Start ()
    {
        scontroller = GetComponent<CharacterController>();
        sanim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (Vector3.Distance(player.position, this.transform.position) < 10)
        {
            Vector3 direction = player.position - this.transform.position;
            direction.y = 0;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

            if (direction.magnitude > 1.5)
            {
                sanim.SetBool("running", true);
                sanim.SetInteger("condition", 1);
                this.transform.Translate(0, 0, 0.05f);
            }
            else if (direction.magnitude < 1.5)
            {
                sanim.SetBool("attacking", true);
                sanim.SetInteger("condition", 2);
                this.transform.Translate(0, 0, 0);
            }
        }
        else if (Vector3.Distance(player.position, this.transform.position) > 10)
        {
            sanim.SetBool("running", false);
            sanim.SetInteger("condition", 0);
            this.transform.Translate(0, 0, 0);
        }
	}

    void Attacking()
    {

        StartCoroutine(AttackRoutine());

    }

    IEnumerator AttackRoutine()
    {
        sanim.SetBool("attacking", true);
        sanim.SetInteger("condition", 2);
        yield return new WaitForSeconds(1);
        sanim.SetInteger("condition", 0);
        sanim.SetBool("attacking", false);
    }

}
