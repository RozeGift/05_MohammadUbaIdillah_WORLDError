using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacerOnStartMovement : MonoBehaviour
{
    public float movementspeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * movementspeed * Time.deltaTime);
    }
}
