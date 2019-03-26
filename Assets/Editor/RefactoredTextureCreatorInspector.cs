using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RefactoredTextureCreator))]
public class RefactoredTextureCreatorInspector : Editor {

	private RefactoredTextureCreator creator;

	private void OnEnable () {
		creator = target as RefactoredTextureCreator;
		Undo.undoRedoPerformed += RefreshCreator;
	}

	private void OnDisable () {
		Undo.undoRedoPerformed -= RefreshCreator;
	}

	private void RefreshCreator () {
		if (Application.isPlaying) {
			creator.FillTexture();
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
