using UnityEngine;

[RequireComponent(typeof(NoiseEmitter))]
public class ThiefController : MonoBehaviour
{
    public float walkSpeed = 4f;
    public float runSpeed = 7f;

    private NoiseEmitter noiseEmitter;

    void Start()
    {
        noiseEmitter = GetComponent<NoiseEmitter>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(h, 0, v);
        bool isMoving = move.magnitude > 0.1f;
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        transform.Translate(move.normalized * currentSpeed * Time.deltaTime, Space.World);

        if (isMoving)
        {
            transform.forward = move;

            // 🔊 Generar ruido
            if (isRunning)
                noiseEmitter.MakeNoise(10f); // ruido fuerte
            else
                noiseEmitter.MakeNoise(5f);  // ruido suave
        }
    }
}