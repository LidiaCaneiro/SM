using UnityEngine;

public class Treasure : MonoBehaviour
{
    public bool collected = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            collected = true;
            Debug.Log("TESORO ROBADO");
            gameObject.SetActive(false);
        }
    }
}