using System.Collections.Generic;
using UnityEngine;

public class FireFlicker : MonoBehaviour
{
    private Light _light;

    [Tooltip("Minimum random light intensity")]
    public float minIntensity = 0f;

    [Tooltip("Maximum random light intensity")]
    public float maxIntensity = 1f;

    [Tooltip("How much to smooth out the randomness; lower values = sparks, higher = lantern")] [Range(1, 50)]
    public int smoothing = 5;

    // Continuous average calculation via FIFO queue
    // Saves us iterating every time we update, we just change by the delta
    private Queue<float> _smoothQueue;
    private float _lastSum = 0;


    /// <summary>
    /// Reset the randomness and start again. You usually don't need to call
    /// this, deactivating/reactivating is usually fine but if you want a strict
    /// restart you can do.
    /// </summary>
    public void Reset()
    {
        _smoothQueue.Clear();
        _lastSum = 0;
    }

    void Start()
    {
        _smoothQueue = new Queue<float>(smoothing);
        // External or internal light?
        _light = GetComponent<Light>();
    }

    void Update()
    {
        // pop off an item if too big
        while (_smoothQueue.Count >= smoothing)
        {
            _lastSum -= _smoothQueue.Dequeue();
        }

        // Generate random new item, calculate new average
        float newVal = Random.Range(minIntensity, maxIntensity);
        _smoothQueue.Enqueue(newVal);
        _lastSum += newVal;

        // Calculate new smoothed average
        _light.intensity = _lastSum / (float) _smoothQueue.Count;
    }
}