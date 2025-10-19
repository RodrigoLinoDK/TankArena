using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instancia;

    private void Awake()
    {
        instancia = this;
    }

    public IEnumerator Tremer(float duracao, float intensidade)
    {
        Vector3 posOriginal = transform.localPosition;

        float tempo = 0f;
        while (tempo < duracao)
        {
            float offsetX = Random.Range(-1f, 1f) * intensidade;
            float offsetY = Random.Range(-1f, 1f) * intensidade;

            transform.localPosition = posOriginal + new Vector3(offsetX, offsetY, 0f);

            tempo += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = posOriginal;
    }
}
