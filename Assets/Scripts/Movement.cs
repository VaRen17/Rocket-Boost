using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction rotation;
    [SerializeField] InputAction thrust;
    [SerializeField] float thrustStrength = 1000f;
    [SerializeField] float rotationStrength = 100f;
    [SerializeField] AudioClip engineSound;
    [SerializeField] ParticleSystem mainBoosterPrtc;
    [SerializeField] ParticleSystem leftBoosterPrtc;
    [SerializeField] ParticleSystem rightBoosterPrtc;

    AudioSource audioSource;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }
    private void StartThrusting()
    {
        rb.AddRelativeForce(thrustStrength * Time.fixedDeltaTime * Vector3.up);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(engineSound);
        }
        if (!mainBoosterPrtc.isPlaying)
        {
            mainBoosterPrtc.Play();
        }
    }
    private void StopThrusting()
    {
        audioSource.Stop();
        mainBoosterPrtc.Stop();
    }

    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        if (rotationInput < 0)
        {
            RotateRight();
        }
        else if (rotationInput > 0)
        {
            RotateLeft();
        }
        else
        {
            StopRotating();
        }
    }
    private void RotateRight()
    {
        ApplyRotation(rotationStrength);
        if (!rightBoosterPrtc.isPlaying)
        {
            rightBoosterPrtc.Play();
        }
    }
    private void RotateLeft()
        {
            ApplyRotation(-rotationStrength);
            if (!leftBoosterPrtc.isPlaying)
            {
                leftBoosterPrtc.Play();
            }
        }
    private void StopRotating()
        {
            leftBoosterPrtc.Stop();
            rightBoosterPrtc.Stop();
        }
    private void ApplyRotation(float rotationStrength)
    {
        rb.freezeRotation = true;
        transform.Rotate(rotationStrength * Time.fixedDeltaTime * Vector3.forward);
        rb.freezeRotation = false;
    }
}