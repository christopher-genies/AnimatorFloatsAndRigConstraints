# Animator Floats And Rig Constraints
This Unity project demonstrates the one frame lag apparent in the returned values Animator.GetFloat calls.

![Animator Float Properties Lag](AnimatorFloatPropertiesLag.gif)

## Requirements
- Unity 2021.3.26f1
- Animation Rigging Package 1.1.1

## Explanation

When using the Animator component to get animated float parameter values, the values lag by one frame. That is to say that calling Animator.GetFloat will return the float parameter values from the last frame. This can be remedied by moving the Animator.GetFloat call to the LateUpdate stage, however, there is no way to do this when creating a custom RigConstraint with the Animation Rigging Package.

## Reproduction Steps
- Open up the Scenes/SampleScene.unity scene
- Enter play mode
- Pause playback
- Step through the animation frame by frame
- Observe the green arm on the left and review the TraditionalFloatParameterGetter script
- There is a lag in the joint rotation animation and the blend shape animation
- On the TraditionalFloatParameterGetter component, enable the Use Late Update option
- The lag is no longer present
- Observe the red arm on the right and review the FloatParameterRigConstraint script
- There is no apparent way to obtain the up to date animated float parameter values within the custom RigConstraint
