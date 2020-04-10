using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
enum coinstate
{
    active,
    inactive,
}

public class coin_logic : MonoBehaviour
{
    coinstate state;
    AudioSource source;
    public AudioClip clip;
    public MeshRenderer mesh;
    private bool isplayed=false;
    public Text cointxt;
    public static int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        state = coinstate.active;
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
                state = coinstate.inactive;
                score++;
                cointxt.text = "Coins : " + score;
                isplayed = true;
                source.PlayOneShot(clip);
            }
            mesh.enabled = false;
           
        }
    }
    public void save(int index)
    {
        PlayerPrefs.SetInt("coinstate"+index, (int)state);
        PlayerPrefs.SetInt("coins", score);
    }
    public void load(int index)
    {
        coinstate coinstate = (coinstate)PlayerPrefs.GetInt("coinstate"+index);
        score = PlayerPrefs.GetInt("coins");
        cointxt.text = "Coins : " + score;
        if (coinstate==coinstate.active)
        {
            mesh.enabled = true;
            isplayed = false;
        }
        else if(coinstate==coinstate.inactive)
        {
            mesh.enabled = false;
            isplayed = true;
        }
    }

}
