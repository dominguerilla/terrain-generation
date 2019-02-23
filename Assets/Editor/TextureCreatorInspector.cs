using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TextureCreator))]
public class TextureCreatorInspector : Editor {

    private TextureCreator tc;

    private void OnEnable() {
        tc = (TextureCreator)target;
        Undo.undoRedoPerformed += RefreshCreator;
    }

    private void OnDisable() {
        Undo.undoRedoPerformed -= RefreshCreator;
    }

    private void RefreshCreator(){
        if(Application.isPlaying){
            tc.FillTexture();
        }
    }

    public override void OnInspectorGUI() {
        EditorGUI.BeginChangeCheck();
        DrawDefaultInspector();
        if(EditorGUI.EndChangeCheck() && Application.isPlaying){
            RefreshCreator();
        }
    }
}
