using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;

    AudioSource audioSource;
    [SerializeField] float thrust = 500f;
    [SerializeField] float rotForce = 125f;

    [SerializeField] AudioClip mainEngine;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Caching reference to RigidBody

        audioSource = GetComponent<AudioSource>(); // Cache the audio source component
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust() 
    {
        if(Input.GetKey(KeyCode.Space)) 
        {
            rb.AddRelativeForce(Vector3.up * Time.deltaTime * thrust);
            if(!audioSource.isPlaying) {
                audioSource.PlayOneShot(mainEngine);
            }
        } 
        else {
            audioSource.Stop();
        }
    }

    void ProcessRotation() 
    {
        if(Input.GetKey(KeyCode.A))
        {
            RotatePlayer(rotForce);
        }
        else if(Input.GetKey(KeyCode.D)) 
        {
            RotatePlayer(-rotForce);
        }
    }

    private void RotatePlayer(float rotationThisFrame)
    {
        rb.freezeRotation = true; // Freezing rotation to manually rotate
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationThisFrame);
        rb.freezeRotation = false; // Unfreezing rotation so physics system takes over
    }
}
