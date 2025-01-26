using UnityEngine;

public class AudioPlayer : MonoBehaviour{
    private static AudioPlayer instance;
    void Awake(){
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
}
