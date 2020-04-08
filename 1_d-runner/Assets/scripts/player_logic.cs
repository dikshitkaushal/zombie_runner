using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_logic : MonoBehaviour
{
    Animator m_animator;
    CharacterController m_charactercontroller;

    float movement_x;
    Vector3 m_movement;
    Vector3 jump;
    float gravity = 0.87f;
    float jumpheight = 0.25f;
    private float movespeed = 5f;
    public bool isjumping;

    // Start is called before the first frame update
    void Start()
    {
        isjumping = false;
        m_animator = GetComponent<Animator>();
        m_charactercontroller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        movement_x = Input.GetAxis("Horizontal");
        if(m_animator)
        {
            m_animator.SetFloat("movement_x",Mathf.Abs(movement_x));
        }
        if (Input.GetButtonDown("Jump") && m_charactercontroller.isGrounded)
        {
            isjumping = true;
        }
       
    }
    private void FixedUpdate()
    {
        Debug.Log(movement_x);   
        m_movement = Vector3.right* movement_x * movespeed * Time.deltaTime;
        if(isjumping)
        {
           
            jump.y = jumpheight;
            isjumping = false;

        }
         jump.y -= gravity * Time.deltaTime;

       

        if (m_charactercontroller)
        {
            m_charactercontroller.Move(m_movement + jump);
        }
        if (m_charactercontroller.isGrounded)
        {
            jump.y = 0;
        }
        direction();
    }
    void direction()
    {
        if(movement_x>0)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if(movement_x<0)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
    }
}
