using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class MassWatcher : MonoBehaviour
{
    private Rigidbody2D rb;
    private float previousMass;

    [System.Serializable]
    public class FloatEvent : UnityEvent<float> { }

    public FloatEvent OnMassChanged;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        previousMass = rb.mass;
    }

    private void Update()
    {
        if (Mathf.Abs(rb.mass - previousMass) > Mathf.Epsilon)
        {
            OnMassChanged.Invoke(rb.mass - previousMass);
            previousMass = rb.mass;
        }
    }
}

