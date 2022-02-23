using System.Collections;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Transform _transformSlots;
    public Transform TransformSlots { get => _transformSlots; }


    [SerializeField,Range(1,30)]private int _maxResourse;
    public int MaxResourse { get => _maxResourse; }
    public int CurrentResourse { get; set; }





    private void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent(out ResourceMove resourseMove))
        {
            if ( _maxResourse <= CurrentResourse) return;

            CurrentResourse++;

            Transform target = _transformSlots.childCount != 0 ? _transformSlots.GetChild(_transformSlots.childCount-1).transform : _transformSlots;

            resourseMove.SetTargetTransform(_transformSlots, target);
            resourseMove.GetComponent<BoxCollider>().enabled = false;


        }
        if (col.TryGetComponent(out WarehouseIn warehouseIn))
        {
            if (!Warehouse.GetTransform(warehouseIn.TransformResIn)) return;

            StartCoroutine(ClaimResourse(warehouseIn));
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.TryGetComponent(out WarehouseIn warehouseIn))
        {
            StopAllCoroutines();
        }
    }











    private IEnumerator ClaimResourse(WarehouseIn warehouseIn)
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            for (int i = 0; i < TransformSlots.childCount; i++)
            {
                int value = TransformSlots.childCount - i - 1;

                if (!Warehouse.GetTransform(warehouseIn.TransformResIn)) yield break;

                ResourceMove resourceMove = TransformSlots.GetChild(value).GetComponent<ResourceMove>();

                if (warehouseIn.CheckResourceIn(resourceMove.Resource))
                {
                    yield return new WaitForSeconds(0.1f);

                    warehouseIn.ResourceMoves.Add(resourceMove);
                    resourceMove.SetTargetTransform(Warehouse.GetTransform(warehouseIn.TransformResIn));
                    CurrentResourse--;
                }
            }
        }
    }


}
