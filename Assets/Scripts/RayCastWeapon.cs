using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastWeapon : MonoBehaviour
{
    class Bullet
    {
        public float time;
        public Vector3 initialPosition;
        public Vector3 initialVelocity;
        public TrailRenderer tracer;
    }
    public bool isFiring = false;
    public int fireRate = 25;
    public float bulletspeed = 1000.0f;
    public float bulletDrop = 0.0f;
    public ParticleSystem[] muzzleFlash;
    public ParticleSystem hiteffect;
    public TrailRenderer tracereffect;
    public Transform raycastorigin;
    public Transform raycastdestination;
    public float damage = 10f;

    Ray ray;
    RaycastHit hitinfo;
    float accTime;
    List<Bullet> bullets = new List<Bullet>();
    float maxlifetime = 3.0f;

    Vector3 GetPosition(Bullet bullet)
    {
        //p + v*t + 0.5*g*t*t
        Vector3 gravity = Vector3.down * bulletDrop;
        return (bullet.initialPosition )+ (bullet.initialVelocity * bullet.time )+ (0.5f * gravity * bullet.time * bullet.time);
    }

    Bullet CreateBullet(Vector3 position, Vector3 velocity)
    {
        Bullet bullet = new Bullet();
        bullet.initialPosition = position;
        bullet.initialVelocity = velocity;
        bullet.time = 0.0f;
        bullet.tracer = Instantiate(tracereffect, position, Quaternion.identity);
        bullet.tracer.AddPosition(position);
        return bullet;
        
    }
    public void StartFiring()
    {
        isFiring = true;
        accTime = 0.0f;
        FireBullet();
    }

    public void UpdateFiring(float deltaTime)
    {
        accTime += deltaTime;
        float fireInterval = 1.0f / fireRate;
        while(accTime >= 0.0f)
        {
            FireBullet();
            accTime -= fireInterval;
        }
    }

    public void UpdateBullet(float deltaTime)
    {
        SimulateBullets(deltaTime);
        DestroyBullet();
    }

    void SimulateBullets(float deltaTime)
    {
        bullets.ForEach(bullet =>
        {
            Vector3 p0 = GetPosition(bullet);
            bullet.time += deltaTime;
            Vector3 p1 = GetPosition(bullet);
            RaycastSegment(p0, p1, bullet);
        });
    }

    void DestroyBullet()
    {
        bullets.RemoveAll(bullet => bullet.time >= maxlifetime);
    }

    void RaycastSegment(Vector3 start, Vector3 end, Bullet bullet)
    {
        Vector3 direction = end - start;
        float distance = direction.magnitude;
        ray.origin = start;
        ray.direction = direction;
        if (Physics.Raycast(ray, out hitinfo, distance))
        {
            hiteffect.transform.position = hitinfo.point;
            hiteffect.transform.forward = hitinfo.normal;
            hiteffect.Emit(1);

            bullet.time = maxlifetime;
           
           
            bullet.tracer.transform.position = hitinfo.point;
            bullet.time = maxlifetime;

            var hitbox = hitinfo.collider.GetComponent<HitBox>();
            if (hitbox)
            {
                hitbox.OnRaycastHit(this, ray.direction);
            }
        }
        bullet.tracer.transform.position = end;
    }

    private void FireBullet()
    {
        if (!PauseMenu.ispaused)
        {
            foreach (var particle in muzzleFlash)
            {
                particle.Emit(1);
            }

            Vector3 velocity = (raycastdestination.position - raycastorigin.position).normalized * bulletspeed;
            var bullet = CreateBullet(raycastorigin.position, velocity);
            bullets.Add(bullet);
        }
       
        
    }

    public void StopFiring()
    {
        isFiring = false;
    }

   
}
