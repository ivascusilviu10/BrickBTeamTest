using UnityEngine;
using System.Collections;

public class BallMovement : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] float speed = 10f;
    [SerializeField] Vector3 offsetFromPaddle = new Vector3(0f, 3f, 0f);
    [SerializeField] float maxAngle = 75f;
    [SerializeField] Transform paddleTransform;
    [SerializeField] Renderer BallRenderer;

    bool isBallLaunched = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        BallRenderer.material.color = Color.white;
    }

    void Start()
    {
        StartCoroutine(DelayedLaunch());
    }
   
    void LaunchBall()
    {
        if (isBallLaunched) return;
        rb.isKinematic = false;
        isBallLaunched = true;

        Vector3 dir = new Vector3(Random.Range(-1f, 1f), 1f, 0f).normalized;//Vector3(x,y,z)=>se randomizeaza directia pe x, pe y ramane constanta, pe z=0 pt ca folosim planul XoY, Vector normalizat => mentine viteza constanta indifirenet de directie (lungimea lui adica modulul este egal cu 1)
        rb.linearVelocity = dir * speed;
    }

    void FixedUpdate()
    {
        // Mentinerea Vitezei constante
        rb.linearVelocity = rb.linearVelocity.normalized * speed;
    }
    void Update()
    {
        if (!isBallLaunched)
        {
            transform.position = paddleTransform.position + offsetFromPaddle;
        }
    }

    IEnumerator DelayedLaunch()
    {
        rb.linearVelocity = Vector3.zero; // Bila nu se misca
        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }  

            LaunchBall(); 
    }
    void OnCollisionEnter(Collision collision)
    {
         

        if (collision.gameObject.CompareTag("Paddle"))
        {//ball reflection
            Vector3 contactPoint = collision.contacts[0].point;//punctul de contact al coliziunii
            Vector3 paddleCenter = collision.transform.position; 
            float paddleWidth = collision.collider.bounds.size.x;//latimea paletei

            float offset = contactPoint.x - paddleCenter.x;// distanta de la centrul paletei la punctul de contact
            float normalizedOffset = offset / (paddleWidth / 2f);//normalizarea acestei distante(-1, 1)
           
            float angle = normalizedOffset * maxAngle * Mathf.Deg2Rad;//Mathf.Deg2Rad converteste gradele in radiani

            Vector3 newDirection = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0).normalized;//Mathf.Sin da deviatia pe x , Math.cos da deviatia pe y. Le folosim in ordinea asta pt ca in cercul trigonometric sin este pe verticala iar cos este pe orizontala
            rb.linearVelocity = newDirection * speed;
            //ball color
            BallRenderer.material.color = Color.red;
        }
        if(collision.gameObject.CompareTag("Brick"))
        {
            BallRenderer.material.color = Color.blue;
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ResetZone")) 
        {
            ResetBall();
        }
    }

    void ResetBall()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
        isBallLaunched = false;
        BallRenderer.material.color = Color.white;
        StartCoroutine(DelayedLaunch());
    }

   
}
