using UnityEngine;

public class LightPoleFall : MonoBehaviour
{
    public float rotationSpeed = 20f; // Speed of rotation
    private bool startRotating = false;
    private Quaternion targetRotation;

    void Start()
    {
        targetRotation = Quaternion.Euler( transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y,-80f);

    }

    void Update()
    {
        // Rotate the light pole if it's supposed to start rotating
        if (startRotating)
        {
            
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            // Stop rotating once the pole has reached the target rotation
            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                startRotating = false;
            }
        }
    }

void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger!");
            startRotating = true;
        }
    }
}


