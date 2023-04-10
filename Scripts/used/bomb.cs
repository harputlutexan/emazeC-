using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    public AudioClip clip;
    private AudioSource audSource;
    private GameObject _player;
    private player playerScript;
    private bool collisionWithPlayer;

    void Start()
    {
        //script = _player.GetComponent<player>();
        collisionWithPlayer = false;
        audSource = GetComponent<AudioSource>();
        GetComponent<Animator>().enabled = false;
        _player = GameObject.Find("player");
        playerScript =  _player.GetComponent<player>();
    }

    public void _playAudio()
    {
        if(PlayerPrefs.GetInt("issoundon") == 1)
        {
            audSource.PlayOneShot(clip);
        }
    }

    public void _destroy()
    {
        Destroy(gameObject);
    }
    public void _hurt()
    {
        if (!playerScript.hurtedSoMakeBombInactive)
        {
            playerScript.patladi = collisionWithPlayer;
        }
            


        //playerScript.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GetComponent<Animator>().enabled = true;
            collisionWithPlayer = true; 
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collisionWithPlayer = false;
        }
    }
}