using UnityEngine;

public class ResManger : MonoBehaviour
{
    public static GameObject LoadPrefab(string path)
    {
        return Resources.Load<GameObject>(path);
    }
}
