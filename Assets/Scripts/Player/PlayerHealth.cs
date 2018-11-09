using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);


    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    //PlayerShooting playerShooting;
    bool isDead = false;//set flag!
    bool damaged =false;

    //return func -> property
    public bool IsDead()
    {
        return isDead;
    }

    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        //playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;
    }


    void Update ()
    {
        if (damaged)
        {
            //R, G, B, Alpha
            damageImage.color = new Color(1, 0, 0, 0.1f);
        }
        else
        {
            //start value, finish value, time
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, Time.deltaTime * flashSpeed);
        }
        damaged = false;
    }


    public void TakeDamage (int amount)
    {
        if (isDead)
            return;

        //1. less currentHealth
        currentHealth -= amount;

        //2. make a hit effect on screen
        damaged = true;

        //3. define HPUI
        healthSlider.value = currentHealth;

        //4. play the hit sound 
        playerAudio.Play();

        //5. excute player death
        if (currentHealth <= 0)
        {
            Death();
        }
    }


    void Death ()
    {
        //set flag
        if (isDead)
            return;

        isDead = true;

        //1. play death animation
        anim.SetTrigger("Die");

        //2. turn off the playMovement script!
        playerMovement.enabled = false;

        //3.play death sound
        playerAudio.clip = deathClip;
        playerAudio.Play();
    }


    public void RestartLevel ()
    {
        SceneManager.LoadScene (0);
    }
}
