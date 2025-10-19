using UnityEngine;

public class Shell : MonoBehaviour {

    public GameObject explosion;
    float speed = 0f;
    float mass = 1f;
    float force = 30f;
    float drag = 1f;
    float acceleration;
    float ySpeed = 0f;
    float gravity = -9.8f;
    float gravityAcceleration = 0f;
    public int damage = 1;

    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "tank") {
            GameObject exp = Instantiate(explosion, this.transform.position, Quaternion.identity);
            Destroy(exp, 0.5f);
            Destroy(this.gameObject);
        }

        EnemyHealth enemy = col.collider.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }

    void Update()
    {
        speed *= (1 - Time.deltaTime * drag);
        ySpeed += gravityAcceleration * Time.deltaTime * 0.01f;
        transform.Translate(0, ySpeed, speed * Time.deltaTime);
    }

    private void Start() 
    {
        acceleration = force / mass;
        speed += acceleration;
        gravityAcceleration = gravity / mass;
    }

    void LateUpdate() 
    {

    }
}
