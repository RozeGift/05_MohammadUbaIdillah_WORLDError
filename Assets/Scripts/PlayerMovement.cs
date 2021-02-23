using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float jumpspeed = 4f;
    int jumpcount = 0;
    Animator animator;
    Vector2 input;
    // Start is called before the first frame update
    void Start()
   
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        animator.SetFloat("InputX", input.x);
        animator.SetFloat("InputY", input.y);

        if(HealthBar.health <= 0)
        {
            Destroy(gameObject);
        }
        if(Input.GetKeyDown(KeyCode.Space) && jumpcount == 0)
        {
            rb.AddForce(Vector3.up * jumpspeed, ForceMode.Impulse);
            jumpcount = 1;
        }
        if(HealthBar.health <= 0)
        {
            SceneManager.LoadScene("LoseScene");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            jumpcount = 0;
        }
        if(collision.gameObject.CompareTag("Goal"))
        {
            SceneManager.LoadScene("WinScene");
        }
    }


}
