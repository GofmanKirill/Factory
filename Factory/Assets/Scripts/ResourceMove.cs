using System.Collections;
using UnityEngine;

public class ResourceMove : MonoBehaviour
{
    public Transform GetTransform { get; private set; }

    public Resource Resource { get; private set; }
     
    public float Distanse { get; private set; }


    [SerializeField,Range(1f,100f)]private float _speed;





    private void Update()
    {
        if (GetTransform)
        {
            transform.position = Vector3.MoveTowards(transform.position, GetTransform.position, Time.deltaTime * _speed);
            transform.LookAt(GetTransform);
        }
    }







    public void InitializeResourse(Transform getTransform, Resource resource)
    {
        Resource = resource;
        SetTargetTransform(getTransform);
    }


    private void OnDestroy()
    {
        StopAllCoroutines();
    }


    public void SetTargetTransform(Transform getTransform)
    {
        StopAllCoroutines();
        GetTransform = getTransform;
        Distanse = Vector3.Distance(transform.position, GetTransform.position);
        transform.SetParent(GetTransform);
        StartCoroutine(StopResourse(getTransform));
    }
    public void SetTargetTransform(Transform parent,Transform targetMove)
    {
        StopAllCoroutines();
        GetTransform = targetMove;
        Distanse = Vector3.Distance(transform.position, GetTransform.position);
        StartCoroutine(StopResourse(parent));
    }







    private IEnumerator StopResourse(Transform parent)
    {
        yield return new WaitUntil(() => GetTransform.position == transform.position);


        transform.SetParent(parent);
        transform.localRotation = Quaternion.identity; 
        GetTransform = null;
    }
}
