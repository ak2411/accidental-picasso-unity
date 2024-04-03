using Oculus.Interaction;
using Oculus.Interaction.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AccidentalPicasso.Custom.Hand
{
    /// <summary>
    /// Adds a raycast to the dominant hand and highlights the hit object 
    /// </summary>
    public class HandPointer : MonoBehaviour
    {
        [SerializeField, Interface(typeof(IHand))]
        private Object _leftHand;

        [SerializeField, Interface(typeof(IHand))]
        private Object _rightHand;

        [SerializeField]
        private LayerMask targetLayer;

        private IHand LeftHand { get; set; }
        private IHand RightHand { get; set; }

        public GameObject CurrentTarget { get; private set; }
        public IHand DominantHand {
            get {
                return LeftHand != null && LeftHand.IsDominantHand ? LeftHand : RightHand;
            }
        }

        private Color _originalColor;
        private Renderer _currentRenderer;
        private Color highlightColor = Color.red;

        protected virtual void Awake()
        {
            LeftHand = _leftHand as IHand;
            RightHand = _rightHand as IHand;
        }

        private void Update()
        {
            var hand = LeftHand.IsDominantHand ? LeftHand : RightHand;
            CheckHandPointer(hand);
        }

        void CheckHandPointer(IHand hand)
        {
            Pose handPose;
            if (hand.GetPointerPose(out handPose))
            {
                if(Physics.Raycast(handPose.position, handPose.forward, out RaycastHit hit, Mathf.Infinity, targetLayer))
                {
                    if(CurrentTarget != hit.transform.gameObject)
                    {
                        CurrentTarget = hit.transform.gameObject;
                        _currentRenderer = CurrentTarget.GetComponent<Renderer>();
                        _originalColor = _currentRenderer.material.color;
                        _currentRenderer.material.color = highlightColor;
                    }

                } else
                {
                    if(CurrentTarget != null)
                    {
                        _currentRenderer.material.color = _originalColor;
                        CurrentTarget = null;
                    }
                }
            }
        }
    }
}

