using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameralogic : MonoBehaviour
{
    GameObject player;
    float y = 1;
    float z = -5.2f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(player.transform.position.x, y, z);
        transform.position = Vector3.Slerp(transform.position, movement, 0.125f);
        transform.LookAt(player.transform);
    }
}
