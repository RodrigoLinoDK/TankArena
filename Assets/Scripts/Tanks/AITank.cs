using UnityEngine;

public class AITank : MonoBehaviour
{
    public Transform[] patrolPoints;
    public Transform cannon;
    public Transform bulletSpawn;
    public GameObject bulletPrefab;
    public GameObject enemy; //Jogador

    //Configuracao de movimento
    public float patrolSpeed = 3f;
    public float chaseSpeed = 4f;
    public float rotationSpeed = 5f;

    //Deteccao e combate
    public float viewDistance = 20f;
    public float attackRange = 15f;
    public float fireRate = 1f;
    private float nextFireTime = 0f;

    private int currentPatrolIndex = 0;
    private bool chasing = false;

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, enemy.transform.position);

        if (distanceToPlayer <= viewDistance)
        {
            chasing = true;
        }
        else if (chasing && distanceToPlayer > viewDistance * 1.5f)
        {
            // Perdeu o jogador de vista
            chasing = false;
        }

        if (chasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        Transform targetPoint = patrolPoints[currentPatrolIndex];
        Vector3 direction = (targetPoint.position - transform.position).normalized;
        direction.y = 0;

        //Mover
        transform.position += direction * patrolSpeed * Time.deltaTime;

        //Rotacionar
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        //Avancar para o proximo ponto
        if (Vector3.Distance(transform.position, targetPoint.position) < 1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }

    void ChasePlayer()
    {
        Vector3 direction = (enemy.transform.position - transform.position).normalized;
        direction.y = 0;

        //Mover
        transform.position += direction * chaseSpeed * Time.deltaTime;

        //Rotacionar
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        //Atirar
        if (Vector3.Distance(transform.position, enemy.transform.position) <= attackRange)
        {
            AimAndShoot();
        }
    }

    void AimAndShoot()
    {
        //Gira o canhao para mirar
        Vector3 targetDirection = enemy.transform.position - cannon.position;
        Quaternion lookRotation = Quaternion.LookRotation(targetDirection);
        cannon.rotation = Quaternion.Slerp(cannon.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        //Atira se pronto
        if (Time.time >= nextFireTime)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.linearVelocity = bulletSpawn.forward * 15f;

            nextFireTime = Time.time + 1f / fireRate;
        }
    }
}