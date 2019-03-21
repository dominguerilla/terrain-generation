using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMaker : MonoBehaviour {
	[Range(1, 200)]
	public int resolution = 10;

	public Vector3 offset;
	public Vector3 rotation;

	[Range(0f, 1f)]
	public float strength = 1f;

	public bool damping;

	public float frequency = 1f;
	
	[Range(1, 8)]
	public int octaves = 1;
	
	[Range(1f, 4f)]
	public float lacunarity = 2f;
	
	[Range(0f, 1f)]
	public float persistence = 0.5f;
	
	[Range(1, 3)]
	public int dimensions = 3;
	
	public NoiseMethodType type;

    public float GetNoise(Vector3 point) {
        return Noise.Sum(Noise.methods[(int)type][dimensions - 1], point, frequency, octaves, lacunarity, persistence);
    }

    public float GetAmplitude() {
        return damping ? strength / frequency : strength;
    }
	
}
