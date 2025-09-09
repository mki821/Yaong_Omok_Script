using UnityEngine;
using Newtonsoft.Json;

public class Decoder : MonoBehaviour
{
    public static T Decode<T>(string json) {
        try {
            T t = JsonConvert.DeserializeObject<T>(json);

            return t;
        }
        catch {
            Debug.LogWarning(json);
        }
        return default;
    }
}
