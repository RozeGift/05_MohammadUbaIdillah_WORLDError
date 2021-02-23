using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.SceneManagement;

public class CharacterAim : MonoBehaviour
{
    public float turnSpeed = 15f;
    public float aimdur = 0.3f;

    Camera maincamera;
    RayCastWeapon weapon;

    private Animator anim;
    private AudioSource audsource;
    public Rig aimLayer;

    // Start is called before the first frame update
    void Start()
    {
        maincamera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        weapon = GetComponentInChildren<RayCastWeapon>();
        anim = GetComponent<Animator>();
        audsource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //rotation of the camera at the y axis
        float yourCamera = maincamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yourCamera, 0), turnSpeed * Time.deltaTime);
    }

    private void LateUpdate()
    {
        if (aimLayer)
        {
            if (Input.GetButton("Fire2"))
            {
                aimLayer.weight += Time.deltaTime / aimdur;
            }
            else
            {
                aimLayer.weight -= Time.deltaTime / aimdur;
            }
        }

        if (!PauseMenu.ispaused)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                weapon.StartFiring();
                audsource.Play();
            }
            if (weapon.isFiring)
            {
                weapon.UpdateFiring(Time.deltaTime);
            }
            weapon.UpdateBullet(Time.deltaTime);

            if (Input.GetButtonUp("Fire1"))
            {
                weapon.StopFiring();
                audsource.Stop();
            }
        }
    }

   
}
