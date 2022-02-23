using UnityEngine;

[CreateAssetMenu(menuName ="Resource")]
public class Resource : ScriptableObject
{
    [SerializeField] private GameObject _prefabResourse;
    public GameObject PrefabResourse { get => _prefabResourse; }
}
