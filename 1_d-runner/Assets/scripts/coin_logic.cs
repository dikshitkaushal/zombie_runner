using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin_logic : MonoBehaviour
{
    AudioSource source;
    public AudioClip clip;
    public MeshRenderer mesh;
    private bool isplayed=false;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(Vector3.right, 1f);
        

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player"))
        {
           
            if (!isplayed)
            {
                isplayed = true;
                source.PlayOneShot(clip);
            }
            mesh.enabled = false;
           
        }
    }

}
