using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AILocomotion : MonoBehaviour
{
    public Transform playerTransform;
    public float maxTime = 1.0f;
    public float maxDistance = 1.0f;

    NavMeshAgent agent;
    Animator anim;
    float timer = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
       if(playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.enabled)
        {
            return;
        }
        timer -= Time.deltaTime;
        if (!agent.hasPath)
        {
            agent.destination = playerTransform.position;
        }

        if (timer < 0.0f)
        {
            Vector3 direction = (playerTransform.position - agent.destination);
            direction.y = 0;
            if (direction.sqrMagnitude > maxDistance * maxDistance)
            {
                if (agent.pathStatus != NavMeshPathStatus.PathPartial)
                {
                    agent.destination = playerTransform.position;
                }
            }
            timer = maxTime;
        }
        if (agent.hasPath)
        {
            anim.SetFloat("Speed", agent.velocity.magnitude);
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            HealthBar.health -= 10;
        }
    }
}
