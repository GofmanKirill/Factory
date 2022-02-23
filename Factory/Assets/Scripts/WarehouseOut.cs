using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class WarehouseOut : Warehouse
{
    [SerializeField] private Resource _resourceOut;

    [SerializeField, Range(1, 60)] private int _timeSpawn = 1;

    public UnityEvent OnWarehouseDown;
    public Resource ResourceOut { get => _resourceOut; }
    public int TimeSpawn { get => _timeSpawn; }






    public IEnumerator ResourceSpawnTime()
    {
        yield return new WaitForSeconds(TimeSpawn);

        if (!GetTransform(TransformResIn))
        {
            OnWarehouseDown?.Invoke();
            yield break;
        }

        AddResources();


        StartCoroutine(ResourceSpawnTime());
    }






    public void AddResources()
    {
        GameObject res = Instantiate(ResourceOut.PrefabResourse, TransformIn);

        res.GetComponent<ResourceMove>().InitializeResourse(GetTransform(TransformResIn), ResourceOut);

    }
}
