using UnityEngine;

public class ResManger : MonoBehaviour
{
    public static GameObject LoadPrefab(string path)
    {
        return Resources.Load<GameObject>(path);
    }
    public static Sprite LoadSprite(string path)
    {
        return Resources.Load<Sprite>(path);
    }

}
