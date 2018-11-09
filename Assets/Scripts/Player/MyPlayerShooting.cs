using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayerShooting : MonoBehaviour 
{
    public float shootTime = 0.15f;
    public float effectDisplayTime = 0.2f;
    float timer = 0.0f;
    public float range = 100.0f;
    public int damage = 20;

    ParticleSystem effect;
    Light light;
    AudioSource audio;
    LineRenderer lineRenderer;


	void Start () 
    {
        effect = GetComponent<ParticleSystem>();
        light = GetComponent<Light>();
        audio = GetComponent<AudioSource>();
        lineRenderer = GetComponent<LineRenderer>();

	}
	
	// Update is called once per frame
	void Update () 
    {
        timer += Time.deltaTime;

	    //1. left mouse button click
        if (Input.GetMouseButton(0) && timer >= shootTime)
        {
            Shoot();
            timer = 0;
        }

        if (timer >= effectDisplayTime)
        {
            light.enabled = false;
            lineRenderer.enabled = false;
        }

    }

    void Shoot()
    {
        //how to play first!!
        effect.Stop();
        effect.Play();

        light.enabled = true;

        audio.Play();

        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, transform.position);

        //start position which is position of gunberrelend
        Vector3 endPos = transform.position;

        //end position
        endPos += transform.forward * range;
        lineRenderer.SetPosition(1, endPos);

        //raycast start position and vector
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;

        int layer = LayerMask.GetMask("Shootable");

        //ray, hit info, range, layermask!
        if (Physics.Raycast(ray, out hitInfo, range, layer))
        {
            //end position is position of hitInpo
            lineRenderer.SetPosition(1, hitInfo.point);

            EnemyHealth health = hitInfo.transform.GetComponent<EnemyHealth>();

            if (health != null)
            {
                health.TakeDamage(damage, hitInfo.point);
            }


        }

    }
}
