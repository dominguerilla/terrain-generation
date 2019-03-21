using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Terrain3DCreator))]
public class Terrain3DCreatorInspector : Editor {

	private Terrain3DCreator creator;

	private void OnEnable () {
		creator = target as Terrain3DCreator;
		Undo.undoRedoPerformed += RefreshCreator;
	}

	private void OnDisable () {
		Undo.undoRedoPerformed -= RefreshCreator;
	}

	private void RefreshCreator () {
		if (Application.isPlaying) {
			creator.Refresh();
		}
	}

	public override void OnInspectorGUI () {
		EditorGUI.BeginChangeCheck();
		DrawDefaultInspector();
		if (EditorGUI.EndChangeCheck()) {
			RefreshCreator();
		}
	}
}