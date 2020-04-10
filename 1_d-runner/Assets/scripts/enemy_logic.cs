using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
enum enemystate
{
    bitting,
    gettingup,
    roar,
    chase,
    hit,
    die,
}

public class enemy_logic : MonoBehaviour
{
    NavMeshAgent m_navmesh;
     int i = 1;
    float distance;
    Transform player;
    enemystate state = enemystate.bitting;
    Animator m_animator;
    private float roardistance = 5.5f;
    private float chasedistance = 4f;
    private float hitdistance;
    private bool isdead = false;

    // Start is called before the first frame update
    void Start()
    {
        m_navmesh = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        m_animator = GetComponent<Animator>();
        hitdistance = m_navmesh.stoppingDistance;
    }

    // Update is called once per frame
    void Update()
    {
        
        float distance = Vector3.Distance(transform.position, player.position);
        switch (state)
        {
            case (enemystate.bitting):
                {
                    biting();
                    
                    break;
                }
          
            case (enemystate.roar):
                {
                    roar();
                    break;
                }
            case (enemystate.chase):
                {
                    chase();
                   
                    break;
                }
            case (enemystate.hit):
                {
                    attack();
                    break;
                }
           /* case (enemystate.die):
                {
                    die();              cannot apply die stage in switch condition in update
                                        die cannot occur every frame per second
                    break;
                }*/
        }
        if (isdead)
        {
            i = 0;
           /* m_navmesh.isStopped = true;
            m_navmesh.velocity = Vector3.zero;*/
            m_navmesh.enabled = false;

            enabled = false;
        }

    }
    private void LateUpdate()
    {
        Vector3 diff = player.position - transform.position;
        Quaternion angle = Quaternion.LookRotation(new Vector3(diff.x, 0, diff.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, angle, 0.05f);

    }

    private void biting()
    {
        m_animator.SetTrigger("biting");
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < roardistance)
        {
            state = enemystate.roar;
        }
    }

  
    private void roar()
    {
       
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < chasedistance)
        {
            state = enemystate.chase;
        }
        m_animator.SetTrigger("roar");
    }
    private void chase()
    {
        m_animator.SetTrigger("chase");
        m_navmesh.SetDestination(player.position);
        m_navmesh.isStopped = false;
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < hitdistance)
        {

            state = enemystate.hit;
        }
    }
    private void attack()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < hitdistance)
        {
            m_navmesh.isStopped = true;
            m_navmesh.velocity = Vector3.zero;
            m_animator.SetTrigger("hit");
        }
        else
        {
            state = enemystate.chase;
        }
        
       
    }
    void die()
    {
        Debug.Log("executed again");
        i = 0;
        isdead = true;

        m_animator.SetTrigger("isdead");
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player" && i==1)
        {
            die();
        }
    }
    public void save(int index)
    {
      
        PlayerPrefs.SetInt("enabeled"+index, i);
        PlayerPrefs.SetInt("enemystate" + index, (int)state);
        PlayerPrefs.SetFloat("pos_x"+index, transform.position.x);
        PlayerPrefs.SetFloat("pos_y"+index, transform.position.y);
        PlayerPrefs.SetFloat("pos_z"+index, transform.position.z);

        PlayerPrefs.SetFloat("rot_x"+index, transform.rotation.eulerAngles.x);
        PlayerPrefs.SetFloat("rot_y"+index, transform.rotation.eulerAngles.y);
        PlayerPrefs.SetFloat("rot_z"+index, transform.rotation.eulerAngles.z);

        AnimatorStateInfo info = m_animator.GetCurrentAnimatorStateInfo(0);
        int animhash = info.fullPathHash;
        PlayerPrefs.SetInt("enemyhash" + index, animhash);
        float animtime = info.normalizedTime;
        PlayerPrefs.SetFloat("enemytime" + index, animtime);
    }
    public void load(int index)
    {
        
        state = (enemystate)PlayerPrefs.GetInt("enemystate" + index);
        i = PlayerPrefs.GetInt("enabeled"+index);
        float pos_x = PlayerPrefs.GetFloat("pos_x"+index);
        float pos_y = PlayerPrefs.GetFloat("pos_y"+index);
        float pos_z = PlayerPrefs.GetFloat("pos_z"+index);

        float rot_x = PlayerPrefs.GetFloat("rot_x"+index);
        float rot_y = PlayerPrefs.GetFloat("rot_y"+index);
        float rot_z = PlayerPrefs.GetFloat("rot_z"+index);

        int animhash = PlayerPrefs.GetInt("enemyhash" + index);
        float enemytime = PlayerPrefs.GetFloat("enemytime" + index);
        m_animator.Play(animhash, 0, enemytime);
        if(i==0)
        {
            isdead = true;
        }
        else if(i==1)
        {
            Debug.Log("executed");
            m_navmesh.enabled = true;
            enabled = true;
            isdead = false;
        }


       

        transform.position = new Vector3(pos_x, pos_y, pos_z);
        transform.rotation = Quaternion.Euler(rot_x, rot_y, rot_z);

       
    }
}
