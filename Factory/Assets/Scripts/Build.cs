using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Build : MonoBehaviour
{
    [Header("Created resource")]
    [SerializeField] private WarehouseOut _warehouseOut;

    [Header("Resources for production")]
    [SerializeField] private WarehouseIn _warehouseIn;
    
    [Header("UI Identifier")]
    [SerializeField] private UIBuildIdentifier _uIBuildIdentifier;

    [Space]
    public UnityEvent OnEnableProduction;




    private void Start()
    {
        EnableProduction();
    }

    private void OnEnable()
    {
        if (_warehouseOut)
        {
            _warehouseOut.OnWarehouseDown.AddListener(() => WarehouseFull());
            _warehouseOut.OnWarehouseDown.AddListener(() => _uIBuildIdentifier.WarningWarehouseFull());
        }

        if (_warehouseIn)
        {
            _warehouseIn.OnNoResources.AddListener(() => WarehouseNoResources());
            _warehouseIn.OnNoResources.AddListener(() => _uIBuildIdentifier.WarningNoResources());
        }

        OnEnableProduction.AddListener(() => _uIBuildIdentifier.ClearNotifications());
    }

    private void OnDisable()
    {
        if (_warehouseOut)
        {
            _warehouseOut.OnWarehouseDown.RemoveListener(() => WarehouseFull());
            _warehouseOut.OnWarehouseDown.RemoveListener(() => _uIBuildIdentifier.WarningWarehouseFull());
        }

        if (_warehouseIn)
        {
            _warehouseIn.OnNoResources.RemoveListener(() => WarehouseNoResources());
            _warehouseIn.OnNoResources.RemoveListener(() => _uIBuildIdentifier.WarningNoResources());
        }

        OnEnableProduction.RemoveListener(() => _uIBuildIdentifier.ClearNotifications());
    }



    private void EnableProduction()
    {
        OnEnableProduction?.Invoke();

        if (!_warehouseIn) StartCoroutine(_warehouseOut.ResourceSpawnTime());
        else StartCoroutine(CheckResourseInWarehouse());

    }




    public void WarehouseFull()
    {
        StartCoroutine(CheckWarehouseFull());
    }
    public void WarehouseNoResources()
    {
        StartCoroutine(CheckResourseCount());
    }





    private IEnumerator CheckWarehouseFull()
    {
        yield return new WaitUntil(() => Warehouse.GetTransform(_warehouseOut.TransformResIn));
        EnableProduction();
    }
    private IEnumerator CheckResourseCount()
    {
        yield return new WaitUntil(() => _warehouseIn.CheckResourceCount()&&_warehouseIn.ResourceMoves.Count!=0);
        EnableProduction();
    }






    private IEnumerator CheckResourseInWarehouse()
    {
        yield return new WaitForSeconds(0.5f);
        if (!_warehouseIn.CheckResourceCount())
        {
            _warehouseIn.OnNoResources?.Invoke();
            yield break;
        }
        if (!Warehouse.GetTransform(_warehouseOut.TransformResIn))
        {
            _warehouseOut.OnWarehouseDown?.Invoke();
            yield break;
        }


        _warehouseIn.RemoveResources();

        yield return new WaitForSeconds(_warehouseOut.TimeSpawn);

        _warehouseOut.AddResources();

        StartCoroutine(CheckResourseInWarehouse());
    }



}
