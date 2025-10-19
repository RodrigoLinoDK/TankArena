using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //Vida
    public int maxHealth = 3;
    private int currentHealth;
    public GameObject explosion;

    //Feedback de Dano
    public Renderer[] renderers;
    public Color flashColor = Color.white;
    public float flashDuration = 0.1f;
    private Color[] originalColors;

    private void Start()
    {
        currentHealth = maxHealth;

        // Salva as cores originais do tanque inimigo
        originalColors = new Color[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
        {
            originalColors[i] = renderers[i].material.color;
        }
    }

    public void TakeDamage(int damage) //Faz o inimigo sofrer dano
    {
        currentHealth -= damage;
        StartCoroutine(FlashDamage());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    System.Collections.IEnumerator FlashDamage() //Faz o inimigo piscar ao sofrer dano
    {
        foreach (Renderer r in renderers)
        {
            r.material.color = flashColor;
        }

        yield return new WaitForSeconds(flashDuration);

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = originalColors[i];
        }
    }

    void Die() //Destroi o objeto do inimigo e instancia as particulas de explosão
    {
        // Efeito de morte
        GameObject exp = Instantiate(explosion, this.transform.position, Quaternion.identity);
        Destroy(exp, 0.5f);
        Destroy(gameObject);
    }
}