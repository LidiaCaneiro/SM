using UnityEngine;

public class HearingSensor : MonoBehaviour
{
    public Transform target;

    private NoiseEmitter noiseEmitter;

    void Start()
    {
        if (target != null)
            noiseEmitter = target.GetComponent<NoiseEmitter>();
    }

    public bool CanHearTarget(out Vector3 heardPosition)
    {
        heardPosition = Vector3.zero;

        if (noiseEmitter == null)
            return false;

        float noiseRadius = noiseEmitter.GetNoiseRadius();

        if (noiseRadius <= 0f)
            return false;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= noiseRadius)
        {
            heardPosition = target.position;
            return true;
        }

        return false;
    }
}