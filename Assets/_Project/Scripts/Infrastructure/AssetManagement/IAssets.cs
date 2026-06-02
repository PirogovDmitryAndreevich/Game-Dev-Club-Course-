using UnityEngine;

public interface IAssets : IService
{
    GameObject Instantiate(string path);
    GameObject Instantiate(string path, Vector2 at);
    GameObject Instantiate(string path, Transform parent);
}