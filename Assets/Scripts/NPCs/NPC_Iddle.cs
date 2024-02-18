using UnityEngine;

public class NPC_Iddle : MonoBehaviour
{
    [Header("Manual Setup")]
    [Range(0.5f, 2f)] public float frequency;
    [Range(0.1f, 1f)] public float amplitude;
    [Space]
    [Header("Random Setup")]
    public bool fullRandomize;
    [Range(0f, 2f)] public float minFrequency;
    [Range(0f, 4f)] public float maxFrequency;
    [Range(0f, 1f)] public float minAmplitude;
    [Range(0f, 2f)] public float maxAmplitude;

    private Vector3 startPos;
    private Vector3 movementDir;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        float randfloat = UnityEngine.Random.Range(0f, 1f);
        movementDir = randfloat < 0.5f ? Vector3.up : Vector3.right;
        if (fullRandomize)
        {
            frequency = UnityEngine.Random.Range(minFrequency, maxFrequency);
            amplitude = UnityEngine.Random.Range(minAmplitude, maxAmplitude);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 motion = movementDir * amplitude * Mathf.Sin(Time.time * frequency);
        transform.position = new Vector3(startPos.x + motion.x, startPos.y + motion.y, startPos.z);
    }
}
