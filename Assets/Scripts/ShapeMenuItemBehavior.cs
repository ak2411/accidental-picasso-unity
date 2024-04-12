using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

namespace AccidentalPicasso.UI.Palette
{

    public class ShapeMenuItemBehavior : MonoBehaviour
    {
        private Vector3 originalRelativePosition;
        private Quaternion originalRelativeRotation;
        private IInteractableView InteractableView { get; set; }
        protected bool _started = false;
        private ShapesManager shapesManager;
        private PaletteBehavior paletteBehavior;
        private float replacePrimitiveThreshold = 0.03f;
        private bool shouldResetPose = false;
        private bool removedFromMenu = false;
        private bool isSelected = false;

        protected virtual void Awake()
        {
            shapesManager = FindObjectOfType<ShapesManager>();
            paletteBehavior = FindObjectOfType<PaletteBehavior>();
            InteractableView = GetComponentInChildren<InteractableGroupView>();
        }
        protected virtual void Start()
        {
            //Debug.Log("Test debug log");
            this.BeginStart(ref _started);
            transform.GetLocalPositionAndRotation(out originalRelativePosition, out originalRelativeRotation);
            this.AssertField(InteractableView, nameof(InteractableView));
            //TODO: Ensure that shape is not null
            this.EndStart(ref _started);
        }

        protected void Update()
        {
            // Need to do updates here or else the order may be wrong
            if (isSelected)
            {
                if (!removedFromMenu && Vector3.Distance(transform.localPosition, originalRelativePosition) > replacePrimitiveThreshold)
                {
                    removedFromMenu = true;
                    this.transform.SetParent(shapesManager.transform, true);
                }
            }
            if (shouldResetPose)
            {
                ResetPositionAndLocation();
            }
        }

        protected virtual void OnEnable()
        {
            if (_started)
            {
                InteractableView.WhenStateChanged += HandleStateChange;
            }
        }

        protected virtual void OnDisable()
        {
            if (_started)
            {
                InteractableView.WhenStateChanged -= HandleStateChange;
            }
        }

        private void HandleStateChange(InteractableStateChangeArgs args)
        {
            if (args.NewState == InteractableState.Select)
            {
                isSelected = true;
            }
            if (args.PreviousState == InteractableState.Select && args.NewState != InteractableState.Select)
            {
                isSelected = false;
                if(removedFromMenu)
                {
                    enabled = false;
                    paletteBehavior.UpdatePrimitiveOptions();
                }
                else
                {
                    shouldResetPose = true;
                }
            }
        }

        private void ResetPositionAndLocation()
        {
            transform.SetLocalPositionAndRotation(originalRelativePosition, originalRelativeRotation);
            shouldResetPose = false;
        }

        public void UpdateColor(Color color)
        {
            GetComponentInChildren<MaterialPropertyBlockEditor>().MaterialPropertyBlock.SetColor("_Color", color);
        }
    }
}

