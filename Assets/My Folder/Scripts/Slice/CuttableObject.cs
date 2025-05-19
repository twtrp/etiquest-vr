using UnityEngine;
using EzySlice;

public class CuttableObject : MonoBehaviour
{
    public int maxCuts = 3;
    public int currentCuts = 0;
    public float cutForce = 5f;
    public Material crossSectionMaterial;

    public void Cut(Vector3 planePosition, Vector3 planeNormal)
    {
        if (currentCuts >= maxCuts) return;

        SlicedHull hull = gameObject.Slice(planePosition, planeNormal);
        if (hull == null) return;

        GameObject upper = hull.CreateUpperHull(gameObject, crossSectionMaterial);
        GameObject lower = hull.CreateLowerHull(gameObject, crossSectionMaterial);

        InitSlicedPart(upper, currentCuts + 1);
        InitSlicedPart(lower, currentCuts + 1);

        Destroy(gameObject);
    }

    private void InitSlicedPart(GameObject part, int inheritedCutCount)
    {
        part.layer = gameObject.layer;

        Rigidbody rb = part.AddComponent<Rigidbody>();
        rb.AddExplosionForce(cutForce, part.transform.position, 1);
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        MeshCollider col = part.AddComponent<MeshCollider>();
        col.convex = true;

        CuttableObject newCuttable = part.AddComponent<CuttableObject>();
        newCuttable.currentCuts = inheritedCutCount;
        newCuttable.maxCuts = maxCuts;
        newCuttable.cutForce = cutForce;
        newCuttable.crossSectionMaterial = crossSectionMaterial;
    }
}
