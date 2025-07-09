using UnityEngine;
using System.Collections;

public class BallMovement : MonoBehaviour
{
    Rigidbody rb;


    [SerializeField] float speed = 10f;


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        StartCoroutine(DelayedLaunch());
    }

    void LaunchBall()
    {

        Vector3 dir = new Vector3(Random.Range(-1f, 1f), 1f, 0f).normalized;//Vector3(x,y,z)=>se randomizeaza directia pe x, pe y ramane constanta, pe z=0 pt ca folosim planul XoY, Vector normalizat => mentine viteza constanta indifirenet de directie (lungimea lui adica modulul este egal cu 1)
        rb.linearVelocity = dir * speed;
    }

    void FixedUpdate()
    {
        // Mentinerea Vitezei constante
        rb.linearVelocity = rb.linearVelocity.normalized * speed;
    }

    IEnumerator DelayedLaunch()
    {
        rb.linearVelocity = Vector3.zero; // Bila nu se misca
        yield return new WaitForSeconds(2f); // Se asteapta 2 secunde 

        LaunchBall(); // Se lanseaza bila
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            Vector3 contactPoint = collision.contacts[0].point;//punctul de contact al coliziunii
            Vector3 paddleCenter = collision.transform.position; 
            float paddleWidth = collision.collider.bounds.size.x;//latimea paletei

            float offset = contactPoint.x - paddleCenter.x;// distanta de la centrul paletei la punctul de contact
            float normalizedOffset = offset / (paddleWidth / 2f);//normalizarea acestei distante(-1, 1)
            float maxAngle = 75f;
            float angle = normalizedOffset * maxAngle * Mathf.Deg2Rad;//Mathf.Deg2Rad converteste gradele in radiani

            Vector3 newDirection = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0).normalized;//Mathf.Sin da deviatia pe x , Math.cos da deviatia pe y. Le folosim in ordinea asta pt ca in centrul trigonometric sin este pe verticala iar cos este pe orizontala
            rb.linearVelocity = newDirection * speed;
        }
    }
}
