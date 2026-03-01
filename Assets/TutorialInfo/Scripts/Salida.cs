using UnityEngine;

public class Exit : MonoBehaviour
{
    public Treasure treasure;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && treasure.collected)
        {
            Debug.Log("VICTORIA — EL LADRÓN ESCAPÓ");
        }
    }
}