using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class HealthSystem : MonoBehaviour
{
    //Configurações de Vida do jogador
    public int vidaMaxima = 3;
    public int vidaAtual;

    //Feedback de dano
    public Renderer[] renderers;
    public float tempoDePiscada = 0.1f;
    public int numeroDePiscadas = 3;

    //HUD
    public Text textoVida;

    void Start()
    {
        vidaAtual = vidaMaxima;
        AtualizarHUD();
    }

    public void TomarDano(int dano) //Faz o jogador sofrer dano
    {
        vidaAtual -= dano;
        AtualizarHUD();
        StartCoroutine(Piscar());

        if (CameraShake.instancia != null)
            StartCoroutine(CameraShake.instancia.Tremer(0.2f, 0.2f));

        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    IEnumerator Piscar()
    {
        for (int i = 0; i < numeroDePiscadas; i++)
        {
            foreach (Renderer r in renderers)
                r.material.color = Color.white;

            yield return new WaitForSeconds(tempoDePiscada);

            foreach (Renderer r in renderers)
                r.material.color = Color.green;

            yield return new WaitForSeconds(tempoDePiscada);
        }
    }

    void AtualizarHUD()
    {
        if (textoVida != null)
            textoVida.text = "Vida: " + vidaAtual.ToString();
    }


    void Morrer() //Reinicia a cena quando a vida do jogador chegar a 0
    {
        Debug.Log("Player morreu!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
