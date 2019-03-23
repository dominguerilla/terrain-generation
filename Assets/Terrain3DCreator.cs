using UnityEngine;


public enum Biome {
    OCEAN,
    BEACH,
    SCORCHED,
    BARE,
    TUNDRA,
    SNOW,
    TEMPERATE_DESERT,
    SHRUBLAND,
    TAIGA,
    GRASSLAND,
    TEMPERATE_DECIDUOUS_FOREST,
    TEMPERATE_RAIN_FOREST,
    SUBTROPICAL_DESERT,
    TROPICAL_SEASONAL_FOREST,
    TROPICAL_RAIN_FOREST,
}

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Terrain3DCreator : MonoBehaviour {

    public NoiseMaker elevationGenerator;

    [Range(1, 200)]
	public int resolution = 10;

	public Vector3 offset;
	public Vector3 rotation;

	public bool coloringForStrength;

    public Color oceanColor;
    public Color beachColor;
    public Color snowColor;
    public Color taigaColor;
    public Color temperateRainForestColor;
    public Color tropicalRainForestColor;
    public Color errorColor;

	private Mesh mesh;
	private Vector3[] vertices;
	private Vector3[] normals;
	private Color[] colors;

	private int currentResolution;

	private void OnEnable () {
		if (mesh == null) {
			mesh = new Mesh();
			mesh.name = "Surface Mesh";
			GetComponent<MeshFilter>().mesh = mesh;
		}
        elevationGenerator.onChange.AddListener(Refresh);
		Refresh();
	}

	public void Refresh () {
		if (resolution != currentResolution) {
			CreateGrid();
		}
		Quaternion q = Quaternion.Euler(rotation);
		Vector3 point00 = q * new Vector3(-0.5f, -0.5f) + offset;
		Vector3 point10 = q * new Vector3( 0.5f, -0.5f) + offset;
		Vector3 point01 = q * new Vector3(-0.5f, 0.5f) + offset;
		Vector3 point11 = q * new Vector3( 0.5f, 0.5f) + offset;

		float stepSize = 1f / resolution;
		float amplitude = elevationGenerator.GetAmplitude();
		for (int v = 0, y = 0; y <= resolution; y++) {
			Vector3 point0 = Vector3.Lerp(point00, point01, y * stepSize);
			Vector3 point1 = Vector3.Lerp(point10, point11, y * stepSize);
			for (int x = 0; x <= resolution; x++, v++) {
				Vector3 point = Vector3.Lerp(point0, point1, x * stepSize);

                // setting the elevation
                float elevationSample = elevationGenerator.GetNoise(point);
				elevationSample = elevationGenerator.type == NoiseMethodType.Value ? (elevationSample - 0.5f) : (elevationSample * 0.5f);
                Biome biome = GetBiome(elevationSample);
                Color biomeColor = GetBiomeColor(biome);
				if (coloringForStrength) {
					colors[v] = biomeColor;
					elevationSample *= amplitude;
				}
				else {
					elevationSample *= amplitude;
					colors[v] = biomeColor;
				}
                vertices[v].y = DampenBiomeElevation(biome, elevationSample);
			}
		}
		mesh.vertices = vertices;
		mesh.colors = colors;
		mesh.RecalculateNormals();
	}

    /// <summary>
    /// Flattens certain biome elevations.
    /// </summary>
    /// <returns></returns>
    float DampenBiomeElevation(Biome biome, float elevation) {
        float factor = 1.0f;
        switch (biome) {
            case Biome.BEACH:
                factor = 0.2f;
                break;
            case Biome.TROPICAL_RAIN_FOREST:
                factor = 0.2f;
                break;
            case Biome.TAIGA:
                factor = 0.2f;
                break;
            case Biome.SNOW:
                factor = 0.22f;
                break;
            default:
                factor = 1.0f;
                break;
        }
        return elevation * factor;
    }

    Biome GetBiome(float elevation) {
        if (elevation < 0.0) return Biome.OCEAN;
        if (elevation < 0.05) return Biome.BEACH;
        if (elevation < 0.11) return Biome.TROPICAL_RAIN_FOREST;
        if (elevation < 0.3) return Biome.TAIGA;
        return Biome.SNOW;
    }

    Color GetBiomeColor(Biome biome) {
        switch(biome) {
            case Biome.OCEAN:
                return oceanColor;
            case Biome.BEACH:
                return beachColor;
            case Biome.SNOW:
                return snowColor;
            case Biome.TAIGA:
                return taigaColor;
            case Biome.TEMPERATE_RAIN_FOREST:
                return temperateRainForestColor;
            case Biome.TROPICAL_RAIN_FOREST:
                return tropicalRainForestColor;
            default:
                return errorColor;
        }
    }

	private void CreateGrid () {
		currentResolution = resolution;
		mesh.Clear();
		vertices = new Vector3[(resolution + 1) * (resolution + 1)];
		colors = new Color[vertices.Length];
		normals = new Vector3[vertices.Length];
		Vector2[] uv = new Vector2[vertices.Length];
		float stepSize = 1f / resolution;
		for (int v = 0, z = 0; z <= resolution; z++) {
			for (int x = 0; x <= resolution; x++, v++) {
				vertices[v] = new Vector3(x * stepSize - 0.5f, 0f, z * stepSize - 0.5f);
				colors[v] = Color.black;
				normals[v] = Vector3.up;
				uv[v] = new Vector2(x * stepSize, z * stepSize);
			}
		}
		mesh.vertices = vertices;
		mesh.colors = colors;
		mesh.normals = normals;
		mesh.uv = uv;

		int[] triangles = new int[resolution * resolution * 6];
		for (int t = 0, v = 0, y = 0; y < resolution; y++, v++) {
			for (int x = 0; x < resolution; x++, v++, t += 6) {
				triangles[t] = v;
				triangles[t + 1] = v + resolution + 1;
				triangles[t + 2] = v + 1;
				triangles[t + 3] = v + 1;
				triangles[t + 4] = v + resolution + 1;
				triangles[t + 5] = v + resolution + 2;
			}
		}
		mesh.triangles = triangles;
	}
}