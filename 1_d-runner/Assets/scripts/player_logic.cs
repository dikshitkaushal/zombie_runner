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
    public void save()
    {
        PlayerPrefs.SetFloat("pos_x", transform.position.x);
        PlayerPrefs.SetFloat("pos_y", transform.position.y);
        PlayerPrefs.SetFloat("pos_z", transform.position.z);

        PlayerPrefs.SetFloat("rot_x", transform.rotation.eulerAngles.x);
        PlayerPrefs.SetFloat("rot_y", transform.rotation.eulerAngles.y);
        PlayerPrefs.SetFloat("rot_z", transform.rotation.eulerAngles.z);
    }
    public void load()
    {
        float pos_x = PlayerPrefs.GetFloat("pos_x");
        float pos_y = PlayerPrefs.GetFloat("pos_y");
        float pos_z = PlayerPrefs.GetFloat("pos_z");

        float rot_x = PlayerPrefs.GetFloat("rot_x");
        float rot_y = PlayerPrefs.GetFloat("rot_y");
        float rot_z = PlayerPrefs.GetFloat("rot_z");

        m_charactercontroller.enabled = false;

        transform.position = new Vector3(pos_x, pos_y, pos_z);
        transform.rotation = Quaternion.Euler(rot_x, rot_y, rot_z);

        m_charactercontroller.enabled = true;
    }
}
