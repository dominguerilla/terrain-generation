using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Terrain2DCreator))]
public class Terrain2DCreatorInspector : Editor {

    private Terrain2DCreator tc;

    private void OnEnable() {
        tc = (Terrain2DCreator)target;
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
