using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script : MonoBehaviour
{
    CharacterController m_chara;
    // Start is called before the first frame update
    void Start()
    {
        m_chara = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(m_chara.isGrounded);
    }
}
