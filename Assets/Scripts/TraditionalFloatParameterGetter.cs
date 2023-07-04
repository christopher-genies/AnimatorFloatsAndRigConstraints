using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraditionalFloatParameterGetter : MonoBehaviour
{
    public Animator animator;

    public SkinnedMeshRenderer skinnedMeshRenderer;

    public string floatParameterName = "upperArmMuscleFlex";

    public string blendShapeName = "bicepBulge.Arm1";

    public bool useLateUpdate;

    private int blendShapeIndex;

    void Start()
    {
        blendShapeIndex = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(blendShapeName);
    }

    // Float values obtained here lag by one frame.
    void Update()
    {
        if (!useLateUpdate)
            skinnedMeshRenderer.SetBlendShapeWeight(blendShapeIndex, animator.GetFloat(floatParameterName) * 100f);
    }

    void LateUpdate()
    {
        if (useLateUpdate)
            skinnedMeshRenderer.SetBlendShapeWeight(blendShapeIndex, animator.GetFloat(floatParameterName) * 100f);
    }
}
