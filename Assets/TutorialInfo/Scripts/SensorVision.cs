using UnityEngine;

public class VisionSensor : MonoBehaviour
{
    public Transform target;
    public float visionDistance = 10f;
    public float visionAngle = 60f;

    public bool CanSeeTarget()
    {
        if (target == null) return false;

        Vector3 origin = transform.position + Vector3.up * 1.5f;
        Vector3 direction = target.position - origin;

        if (direction.magnitude > visionDistance)
            return false;

        float angle = Vector3.Angle(direction, transform.forward);
        if (angle > visionAngle)
            return false;

        if (Physics.Raycast(origin, direction.normalized, out RaycastHit hit, visionDistance))
        {
            if (hit.transform.CompareTag("Player"))
                return true;
        }

        return false;
    }
}