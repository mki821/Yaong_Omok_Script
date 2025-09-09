using UnityEngine;

public class SetResolution : MonoBehaviour
{
    private void Awake() {
        if(Screen.height >= 1440) {
            Screen.SetResolution(2560, 1440, true);
        }
        else {
            Screen.SetResolution(1920, 1080, true);
        }
    }
}
