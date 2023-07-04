using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;

[System.Serializable]
public struct FloatParametersRigData : IAnimationJobData
{
    public Animator animator;

    public SkinnedMeshRenderer skinnedMeshRenderer;

    public string animatedFloatName;

    [SyncSceneToStream]
    public float animatedFloatValue;

    public string blendShapeName;

    public bool IsValid()
    {
        return animator != null && !string.IsNullOrEmpty(blendShapeName) && !string.IsNullOrEmpty(animatedFloatName);
    }

    public void SetDefaultValues()
    {
        animator = null;
        blendShapeName = "bicepBulge.Arm1";
        animatedFloatName = "upperArmMuscleFlex";
    }
}

public struct FloatParametersRigJob : IWeightedAnimationJob
{
    public FloatProperty jobWeight { get; set; }

    public FloatProperty animatedFloatProperty { get; set; }

    public PropertyStreamHandle blendShapePropertyStream { get; set; }

    public void ProcessAnimation(AnimationStream stream)
    {
        // Set a blend shape weight based on custom calculation.
        // Note that this is a trivial demonstration, and the intended use case would use multiple inputs and non-trivial calculation.
        blendShapePropertyStream.SetFloat(stream, jobWeight.Get(stream) * animatedFloatProperty.Get(stream) * 100f);
    }

    public void ProcessRootMotion(AnimationStream stream) { }
}

public class FloatParametersRigJobBinder : AnimationJobBinder<FloatParametersRigJob, FloatParametersRigData>
{
    public override FloatParametersRigJob Create(Animator animator, ref FloatParametersRigData data, Component component)
    {
        var job = new FloatParametersRigJob
        {
            animatedFloatProperty = FloatProperty.Bind(animator, component, ConstraintsUtils.ConstructConstraintDataPropertyName(nameof(data.animatedFloatValue))),
            blendShapePropertyStream = animator.BindStreamProperty(data.skinnedMeshRenderer.transform, typeof(SkinnedMeshRenderer), $"blendShape.{data.blendShapeName}")
        };

        return job;
    }

    public override void Update(FloatParametersRigJob job, ref FloatParametersRigData data)
    {
        data.animatedFloatValue = data.animator.GetFloat(data.animatedFloatName);
    }

    public override void Destroy(FloatParametersRigJob job) { }
}

public class FloatParameterRigConstraint : RigConstraint<FloatParametersRigJob, FloatParametersRigData, FloatParametersRigJobBinder>
{
    // All of these methods produce the same result, the float value obtained from the animator is delayed by 1 frame.

    //private void Update()
    //{
    //    data.animatedFloatValue = data.animator.GetFloat(data.animatedFloatName);
    //}

    // Float values obtained here are in sync, however, this method is not called until after the animation jobs have completed.
    //private void LateUpdate()
    //{
    //    data.animatedFloatValue = data.animator.GetFloat(data.animatedFloatName);
    //}
}
