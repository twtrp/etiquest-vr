using UnityEngine;

public class CuttingTool : MonoBehaviour
{
    public Transform startSlicePoint;
    public Transform endSlicePoint;
    public VelocityEstimator velocityEstimator;
    public LayerMask sliceableLayer;
    public float cutCooldown = 0.5f; // seconds

    private float lastCutTime = -Mathf.Infinity;

    void FixedUpdate()
    {
        if (Time.time - lastCutTime < cutCooldown)
            return;

        if (Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit hit, sliceableLayer))
        {
            CuttableObject cuttable = hit.transform.GetComponent<CuttableObject>();
            if (cuttable != null)
            {
                Vector3 velocity = velocityEstimator.GetVelocityEstimate();
                Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity).normalized;

                cuttable.Cut(endSlicePoint.position, planeNormal);
                lastCutTime = Time.time;
            }
        }
    }
}
