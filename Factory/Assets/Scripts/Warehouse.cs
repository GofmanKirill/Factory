
using UnityEngine;
public class Warehouse: MonoBehaviour
{
    [SerializeField] private Transform _transformIn;
    [SerializeField] private Transform[] _transformResIn;



    public Transform TransformIn { get => _transformIn; }
    public Transform[] TransformResIn { get => _transformResIn; }





    public static Transform GetTransform(Transform[] transforms)
    {
        for (int i = 0; i < transforms.Length; i++)
        {
            if (transforms[i].childCount == 0) return transforms[i];
        }

        return null;
    }
}
