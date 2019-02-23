using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureCreator : MonoBehaviour {
    
    [Range(2, 512)]
    public int resolution = 256;
    
    private Texture2D texture;

    private void OnEnable() {
        if(texture == null){
            texture = new Texture2D(resolution,resolution, TextureFormat.RGB24, true);
            texture.name = "Procedural Texture";
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.filterMode = FilterMode.Trilinear;
            texture.anisoLevel = 9;
            GetComponent<MeshRenderer>().material.mainTexture = texture;
        }
        FillTexture();
    }

    public void FillTexture(){
        if(texture.width != resolution){
            texture.Resize(resolution, resolution);
        }

        Vector3 bottomLeft = transform.TransformPoint(new Vector3(-.5f, -.5f));
        Vector3 bottomRight = transform.TransformPoint(new Vector3(.5f, -.5f));
        Vector3 topLeft = transform.TransformPoint(new Vector3(-.5f, .5f));
        Vector3 topRight = transform.TransformPoint(new Vector3(.5f, .5f));

        float stepSize = 1f / resolution;
        for(int y = 0; y < resolution; y++){
            Vector3 point0 = Vector3.Lerp(bottomLeft,topLeft,(y + .5f) * stepSize);
            Vector3 point1 = Vector3.Lerp(bottomRight,topRight,(y + .5f) * stepSize);
            for(int x = 0; x < resolution; x++){
                Vector3 point = Vector3.Lerp(point0, point1, (x + .5f) * stepSize);
                texture.SetPixel(x, y, Color.white * Noise.Value(point));
            }
        }
        texture.Apply();
    }

    private void Update() {
        if(transform.hasChanged){
            transform.hasChanged = false;
            FillTexture();
        }
    }

}
