using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollScript : MonoBehaviour
{

    Rigidbody[] rb;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentsInChildren<Rigidbody>();
        anim = GetComponent<Animator>();
        DeactivateRagdoll();
    }

    public void DeactivateRagdoll()
    {
        foreach(var rigidbody in rb)
        {
            rigidbody.isKinematic = true;
        }
        anim.enabled = true;
    }
    public void ActivateRagdoll()
    {
        foreach (var rigidbody in rb)
        {
            rigidbody.isKinematic = false;
        }
        anim.enabled = false;
    }

    public void ApplyForce(Vector3 force)
    {
        var rigidBody = anim.GetBoneTransform(HumanBodyBones.Hips).GetComponent<Rigidbody>();
        rigidBody.AddForce(force, ForceMode.VelocityChange);
    }
}
