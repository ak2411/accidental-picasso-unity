
using Oculus.Interaction;
using Oculus.Interaction.Input;
using UnityEngine;
using System.Collections;

namespace AccidentalPicasso.Custom.Hand
{
    [RequireComponent(typeof(HandPointer))]
    /// <summary>
    /// Gets the hand pinch and alter the target object's material metallic value based on pinch strength
    /// </summary>
    public class NewMonoBehaviour : MonoBehaviour
    {
        private HandPointer _handPointer;
        private bool _hasPinched;

        private void Awake()
        {
            _handPointer = GetComponent<HandPointer>();
        }
        private void Update()
        {
            CheckPinch(_handPointer.DominantHand);
        }

        private void CheckPinch(IHand hand)
        {
            var pinchStrength = hand.GetFingerPinchStrength(HandFinger.Index);
            var isIndexFingerPinching = hand.GetFingerIsPinching(HandFinger.Index);
            var confidence = hand.GetFingerIsHighConfidence(HandFinger.Index);

            if (_handPointer.CurrentTarget)
            {
                Material currentMaterial = _handPointer.CurrentTarget.GetComponent<Renderer>().material;
                currentMaterial.SetFloat("_Metallic", pinchStrength);

                if (!_hasPinched && isIndexFingerPinching && confidence)
                {
                    _hasPinched = true;
                }
            }
            if (_hasPinched && !isIndexFingerPinching)
            {
                _hasPinched = false;
            }

        }
    }
}

