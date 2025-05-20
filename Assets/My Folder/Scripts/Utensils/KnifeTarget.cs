using UnityEngine;
using EzySlice;

public class KnifeTarget : MonoBehaviour
{
    public Material crossSectionMaterial;
    private float cutForce = 10f;

    public void Cut(Vector3 planePosition, Vector3 planeNormal)
    {
        transform.SetParent(null, true);

        SlicedHull hull = gameObject.Slice(planePosition, planeNormal);
        if (hull == null) return;

        GameObject upper = hull.CreateUpperHull(gameObject, crossSectionMaterial);
        GameObject lower = hull.CreateLowerHull(gameObject, crossSectionMaterial);

        InitSlicedPart(upper);
        InitSlicedPart(lower);

        Destroy(gameObject);
    }

    private void InitSlicedPart(GameObject part)
    {
        part.tag = gameObject.tag;

        Rigidbody rb = part.AddComponent<Rigidbody>();
        rb.AddExplosionForce(cutForce, part.transform.position, 1);
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        MeshCollider col = part.AddComponent<MeshCollider>();
        col.convex = true;

        KnifeTarget newCuttable = part.AddComponent<KnifeTarget>();
        newCuttable.cutForce = cutForce;
        newCuttable.crossSectionMaterial = crossSectionMaterial;
    }
}
