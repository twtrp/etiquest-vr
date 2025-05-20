using UnityEngine;
using EzySlice;

public class KnifeTarget : MonoBehaviour
{
    public Material crossSectionMaterial;

    public void Cut(Vector3 planePosition, Vector3 planeNormal)
    {
        transform.SetParent(null, true);

        SlicedHull hull = gameObject.Slice(planePosition, planeNormal);
        if (hull == null) return;

        GameObject upper = hull.CreateUpperHull(gameObject, crossSectionMaterial);
        GameObject lower = hull.CreateLowerHull(gameObject, crossSectionMaterial);

        string originalName = gameObject.name;
        if (originalName.EndsWith(" Piece"))
        {
            originalName = originalName.Substring(0, originalName.Length - " Piece".Length);
        }

        string baseName = originalName + " Piece";

        upper.name = baseName;
        lower.name = baseName;

        InitSlicedPart(upper, baseName);
        InitSlicedPart(lower, baseName);

        Destroy(gameObject);
    }

    private void InitSlicedPart(GameObject part, string name)
    {
        part.tag = gameObject.tag;
        part.name = name;

        Rigidbody rb = part.AddComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        MeshCollider col = part.AddComponent<MeshCollider>();
        col.convex = true;

        KnifeTarget newCuttable = part.AddComponent<KnifeTarget>();
        newCuttable.crossSectionMaterial = crossSectionMaterial;
    }
}
