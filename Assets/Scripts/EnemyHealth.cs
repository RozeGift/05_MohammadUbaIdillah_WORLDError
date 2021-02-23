using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    
    public static float  maxHealth;
    public float dieForce = 10.0f;
    [HideInInspector]
    public static float currentHealth;
    RagdollScript ragdoll;
    SkinnedMeshRenderer skinnedMeshRenderer;
    UIHealth healthbar;

    public float blinkintensity;
    public float blinkDuration;
    float blinkTimer;

    void Start()
    {
        ragdoll = GetComponent<RagdollScript>();
        currentHealth = maxHealth;
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        healthbar = GetComponentInChildren<UIHealth>();

        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach(var rigidbody in rigidBodies)
        {
            HitBox hitBox = rigidbody.gameObject.AddComponent<HitBox>();
            hitBox.health = this;
        }
    }

    public void TakeDamage(float amount, Vector3 direction)
    {
        currentHealth -= amount;
        healthbar.SetHealthBarPercentage(currentHealth / maxHealth);
        if(currentHealth <= 0.0f)
        {
            Die(direction);
        }

        blinkTimer = blinkDuration;
    }

    private void Die(Vector3 direction)
    {
        Destroy(gameObject);
        
    }

    private void Update()
    {
        blinkTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);
        float intensity = (lerp * blinkintensity) + 1.0f;
        skinnedMeshRenderer.material.color = Color.white * intensity;
    }


}
