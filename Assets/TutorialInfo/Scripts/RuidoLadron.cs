using UnityEngine;

public class NoiseEmitter : MonoBehaviour
{
    public float noiseRadius = 0f;
    public float noiseDuration = 0.5f;

    private float noiseTimer = 0f;

    public void MakeNoise(float radius)
    {
        noiseRadius = radius;
        noiseTimer = noiseDuration;
    }

    void Update()
    {
        if (noiseTimer > 0)
        {
            noiseTimer -= Time.deltaTime;

            if (noiseTimer <= 0)
            {
                noiseRadius = 0f;
            }
        }
    }

    public float GetNoiseRadius()
    {
        return noiseRadius;
    }
}