using UnityEngine;

public class BrickMovement : MonoBehaviour
{

    public float speed = 10f;
    public float accelerationSpeed = 15f;
    public float boundary = 7.5f;
    private Vector3 targetPos;
    private float holdTime = 0f;
    private bool acceleration = false;

    void Start()
    {
        
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        if (Mathf.Abs(horizontal) > 0.01f)
        {
            holdTime += Time.deltaTime;
            if (holdTime >= 0.5f && !acceleration)
                acceleration = true;
        }
        else
        {
            acceleration = false;
            holdTime = 0f;
        }

        float currentSpeed = acceleration ? accelerationSpeed : speed;
        Vector3 movement = new Vector3(horizontal, 0f, 0f) * currentSpeed * Time.deltaTime;
        transform.Translate(movement);

        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, -boundary, boundary);
        transform.position = position;
    }
}
