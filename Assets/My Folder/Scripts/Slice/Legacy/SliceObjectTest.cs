using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class SliceObjectTest : MonoBehaviour
{
    public Transform planeDebug;
    public GameObject target;
    public Material crossSectionMaterial;
    public float cutForce = 2000;

    // Start is called before the first frame update
    void Start()
    {
        Slice(target);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Slice(GameObject target)
    {
        SlicedHull hull = target.Slice(planeDebug.position, planeDebug.up);

        if (hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(target, crossSectionMaterial);
            SetUpSlicedComponent(upperHull);

            GameObject lowerHull = hull.CreateLowerHull(target, crossSectionMaterial);
            SetUpSlicedComponent(lowerHull);

            Destroy(target);
        }
    }

    public void SetUpSlicedComponent(GameObject slicedObject)
    {
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        collider.convex = true;
        rb.AddExplosionForce(cutForce, slicedObject.transform.position, 1);
    }
}
