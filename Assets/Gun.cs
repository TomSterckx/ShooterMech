﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gun : MonoBehaviour {

    public float damage = 25f;
    public float range = 100f;
    
    public Camera fpsCam;

    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public float impactForce = 30f;
    public int maxAmmo = 10;
    public int currentAmmo;
    private bool isReloading = false;
    public float reloadTime = 2f;
    public float fireRate = 3f;
    public float nextTimeToFire = 0f;
    public AudioClip audioclip;
    public Animator animator;
    private void Start()
    {
        if (currentAmmo == -1)
        { currentAmmo = maxAmmo; }
             
    }

    void Update () {

        if (isReloading == true)

        {
            return;
        }

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

		if (Input.GetButton("Fire1") && Time.time>= nextTimeToFire && currentAmmo >= 1)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            
        }
	}
    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading..");
        animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        animator.SetBool("Reloading", false);
        isReloading = false;
        
    }

    void Shoot () {
        muzzleFlash.Play();
        AudioSource.PlayClipAtPoint(audioclip, transform.position);
        currentAmmo --; 

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target1 = hit.transform.GetComponent<Target>();
            if (target1 != null)
            {
                target1.TakeDamage(damage);
                GameObject ImpactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(ImpactGO, 2f);
            }
          
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
     

            
        }
    }
}
