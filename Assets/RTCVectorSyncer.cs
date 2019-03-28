using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Keeps a RefactoredTextureCreator synchronized with a Terrain3DCreator.
/// Useful for viewing both generated terrain and the noise it was created with.
/// </summary>
public class RTCVectorSyncer : MonoBehaviour
{
    public RefactoredTextureCreator rtc;
    public Terrain3DCreator creator;

    // Start is called before the first frame update
    void Start()
    {
        creator.onRefresh.AddListener(SyncOffset);        
    }

    public void SyncOffset() {
        rtc.offset = creator.offset;
        rtc.FillTexture();
    }
}
