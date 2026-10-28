using UnityEngine;

public class GlowingFloatingOrb : MonoBehaviour
{
    [Header("Floating Settings")]
    [SerializeField] private float floatSpeed = 3f;     // How fast it bobs up and down
    [SerializeField] private float floatStrength = 0.2f; // How high/low it travels

    [Header("Glow Settings")]
    [SerializeField] private float pulseSpeed = 2f;
    [SerializeField] private float minIntensity = 1f;
    [SerializeField] private float maxIntensity = 3f;

    private Vector3 startPosition;
    private Material orbMaterial;

    void Start()
    {
        // 1. Remember where the orb started on the map
        startPosition = transform.position;

        // 2. Grab the material so we can change its brightness
        orbMaterial = GetComponent<Renderer>().material;
    }

    void Update()
    {
        // --- PHYSICAL MOVEMENT (FLOATING) ---
        // Calculate a smooth up-and-down wave pattern using Mathf.Sin
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatStrength;
        // Apply the new physical position
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);


        // --- VISUAL EFFECT (PULSING) ---
        // Calculate a smooth value bouncing between 0 and 1
        float lerpFactor = (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f;
        // Turn that into a value between your min and max brightness limits
        float currentIntensity = Mathf.Lerp(minIntensity, maxIntensity, lerpFactor);
        // Feed the brightness back into the shader to update the screen display
        orbMaterial.SetColor("_EmissionColor", orbMaterial.color * currentIntensity);
    }
}

