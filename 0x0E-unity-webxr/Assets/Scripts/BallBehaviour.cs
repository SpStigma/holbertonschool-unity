using Unity.VisualScripting;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 5f;
    private bool isOnPiste = false;
    public static bool desableMouseKeyboard = false;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isOnPiste)
        {
            HandleBallControl();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("pist"))
        {
            Debug.Log("sur la piste");
            isOnPiste = true;
            desableMouseKeyboard = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("pist"))
        {
            isOnPiste = false;
            desableMouseKeyboard = false;
        }
    }

    private void HandleBallControl()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0, moveZ) * speed * Time.deltaTime;

        rb.MovePosition(rb.position + movement);
    }
}
