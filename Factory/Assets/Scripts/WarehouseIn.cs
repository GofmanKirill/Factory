using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WarehouseIn : Warehouse
{
    [SerializeField] private Resource[] _resourceIn;
    public Resource[] ResourceIn { get => _resourceIn; }



    public UnityEvent OnNoResources;


    public int[] CountResourses { get; set; }

    internal List<ResourceMove> ResourceMoves = new List<ResourceMove>();
    
    
    
    private void OnEnable()
    {
        CountResourses = new int[ResourceIn.Length];
    }




    public void RemoveResources()
    {
        for (int i = 0; i < ResourceIn.Length; i++)
        {
            foreach (ResourceMove resourceMove in ResourceMoves)
            {
                if (resourceMove.Resource == ResourceIn[i])
                {
                    resourceMove.SetTargetTransform(TransformIn);
                    CountResourses[i]--;
                    ResourceMoves.Remove(resourceMove);
                    Destroy(resourceMove.gameObject, 2);
                    break;
                }
            }
        }
    }





    public bool CheckResourceIn(Resource resource)
    {
        for (int i = 0; i < _resourceIn.Length; i++)
        {
            if (_resourceIn[i] == resource&& CountResource(i))
            {
                CountResourses[i]++;
                return true;
            }
        }

        return false;
    }






    private bool CountResource(int i)
    {
        if (TransformResIn.Length / _resourceIn.Length <= CountResourses[i]) return false;

        return true;
    }







    public bool CheckResourceCount()
    {
        for (int i = 0; i < CountResourses.Length; i++)
        {
            if (CountResourses[i] == 0) return false;
        }

        return true;
    }
}
