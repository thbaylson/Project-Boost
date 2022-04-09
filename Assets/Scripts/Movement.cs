using UnityEngine;

public class Movement : MonoBehaviour
{
    // PARAMETERS - for tuning, typically set in the editor
    [SerializeField] float mainThrust = 1000f;    
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainThrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;

    // CACHE - e.g. references for readability or speed
    Rigidbody rb;
    AudioSource audioSource;

    // STATE - private instance (member) variables
    // e.g. bool isAlive;

    void Start()
    {
        this.rb = GetComponent<Rigidbody>();
        this.audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            StartLeftRotation();
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            StartRightRotation();
        }
        else
        {
            StopRotating();
        }
    }

    void StartThrusting()
    {
        //Debug.Log("Pressed Space - Thrusting");
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainThrustParticles.isPlaying)
        {
            mainThrustParticles.Play();
        }
    }

    void StopThrusting()
    {
        mainThrustParticles.Stop();
        audioSource.Stop();
    }

    private void StartLeftRotation()
    {
        //Debug.Log("Pressed A - Rotating Left");
        if (!rightThrustParticles.isPlaying)
        {
            rightThrustParticles.Play();
        }
        ApplyRotation(rotationThrust);
    }

    private void StartRightRotation()
    {
        //Debug.Log("Pressed D - Rotating Right");
        if (!leftThrustParticles.isPlaying)
        {
            leftThrustParticles.Play();
        }
        ApplyRotation(-rotationThrust);
    }

    private void StopRotating()
    {
        leftThrustParticles.Stop();
        rightThrustParticles.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;//Freeze rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;//Unfreeze rotation so the physics system can take over
    }
}
