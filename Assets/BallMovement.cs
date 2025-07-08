using UnityEngine;
using System.Collections;

public class BallMovement : MonoBehaviour
{
    Rigidbody rb;


    [SerializeField]  float speed = 10f;
  

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
        yield return new WaitForSeconds(3f); // Se asteapta 3 secunde 

        LaunchBall(); // Se lanseaza bila
    }
}
