using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class SliceObject : MonoBehaviour
{
    public Transform startSlicePoint;
    public Transform endSlicePoint;
    public VelocityEstimator velocityEstimator;
    public LayerMask sliceableLayer;
    public Material crossSectionMaterial;
    public float cutForce;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit hit, sliceableLayer);
        if (hasHit)
        {
            GameObject target = hit.transform.gameObject;
            Slice(target);
        }
    }

    public void Slice(GameObject target)
    {
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
        planeNormal.Normalize();

        SlicedHull hull = target.Slice(endSlicePoint.position, planeNormal);

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
        int SliceableLayer = LayerMask.NameToLayer("Sliceable");
        //slicedObject.layer = SliceableLayer;

        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        rb.AddExplosionForce(cutForce, slicedObject.transform.position, 1);

        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        collider.convex = true;
    }
}