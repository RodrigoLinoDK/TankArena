using UnityEngine;

public class AIShell : MonoBehaviour
{

    public GameObject explosion;
    Rigidbody body;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        body = GetComponent<Rigidbody>();    
    }

    void Update()
    {
        transform.forward = body.linearVelocity;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "tank")
        {
            GameObject exp = Instantiate(explosion, this.transform.position, Quaternion.identity);
            Destroy(exp, 0.5f );
            Destroy(this.gameObject);
        }
    }
}
