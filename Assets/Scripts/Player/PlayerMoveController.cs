using UnityEngine;
using System.Collections;

public class PlayerMoveController : MonoBehaviour {

    private CharacterController cc;
    private Animator anim;

    // Use this for initialization
    void Start()
    {

        cc = GetComponentInChildren<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }


    // Wait end of frame to manage charactercontroller, because gravity is managed by virtual controller
    void LateUpdate()
    {
        float speed = 0;
        if ((ETCInput.GetAxis("Vertical") != 0 || ETCInput.GetAxis("Horizontal") != 0))
        {
            speed = Mathf.Max(Mathf.Abs(ETCInput.GetAxis("Vertical")), Mathf.Abs(ETCInput.GetAxis("Horizontal")));
        }
        anim.SetFloat("Speed", speed);
        if (cc.isGrounded && ETCInput.GetAxis("Vertical") == 0 && ETCInput.GetAxis("Horizontal") == 0)
        {
        }
        if (ETCInput.GetButtonDown("ButtonAttack"))
        {
            anim.SetInteger("Action", 1);
        }
        if (ETCInput.GetButtonUp("ButtonAttack"))
        {
            anim.SetInteger("Action", 0);
        }

    }
}
