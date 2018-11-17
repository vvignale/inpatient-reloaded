using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TVFlicker : MonoBehaviour {

	private Light light;
	private float minIntensity = 0f;
	private float maxIntensity = 1f;
	public int smoothing = 5;

	private float intensityStart; 
	private float waitInterval; 
	private float waitTimer; 
	public float maxInterval = .5f; 

	// Continuous average calculation via FIFO queue
	// Saves us iterating every time we update, we just change by the delta
	Queue<float> smoothQueue;
	float lastSum = 0;

	public void Reset() {
		smoothQueue.Clear();
		lastSum = 0;
	}

	void Start() {
		smoothQueue = new Queue<float>(smoothing);
		light = GetComponent<Light>();
		intensityStart = light.intensity; 
		waitTimer = 0; 
		waitInterval = Random.Range (0.0f, maxInterval);
	}

	void Update() {

		waitTimer += Time.deltaTime; 

		if (waitTimer > waitInterval) {

			waitTimer = 0;
			waitInterval = Random.Range (0.0f, maxInterval);

			// pop off an item if too big
			while (smoothQueue.Count >= smoothing) {
				lastSum -= smoothQueue.Dequeue ();
			}

			// Generate random new item, calculate new average
			float newVal = Random.Range (minIntensity * intensityStart, maxIntensity * intensityStart);
			smoothQueue.Enqueue (newVal);
			lastSum += newVal;

			// Calculate new smoothed average
			light.intensity = lastSum / (float)smoothQueue.Count;
		}
	}

}
