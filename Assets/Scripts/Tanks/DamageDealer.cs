using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int dano = 1;

    private void OnCollisionEnter(Collision collision)
    {
        //Verifica se o objeto colidido tem o script de vida
        HealthSystem health = collision.gameObject.GetComponent<HealthSystem>();
        if (health != null)
        {
            health.TomarDano(dano);
        }
    }
}
