using UnityEngine;

public static class Noise {
    
    public static float Value(Vector3 point){
        int i = (int)point.x;
        return i%2;
    }

}
