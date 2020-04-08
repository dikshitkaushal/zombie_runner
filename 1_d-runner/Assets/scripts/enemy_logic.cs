﻿using System;
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
        Debug.Log(distance);
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
            m_navmesh.isStopped = true;
            m_navmesh.velocity = Vector3.zero;

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
        m_animator.SetTrigger("roar");
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < chasedistance)
        {
            state = enemystate.chase;
        }
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
        isdead = true;

        m_animator.SetTrigger("isdead");
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player" && !isdead)
        {
            die();
        }
    }
}