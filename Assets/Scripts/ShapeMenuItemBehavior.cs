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
        private float replacePrimitiveThreshold = 0.5f;


        protected virtual void Awake()
        {
            shapesManager = FindObjectOfType<ShapesManager>();
            paletteBehavior = FindObjectOfType<PaletteBehavior>();
            InteractableView = GetComponentInChildren<InteractableGroupView>();
            Debug.Log("threshold" + replacePrimitiveThreshold);
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
            if (args.PreviousState == InteractableState.Select && args.NewState != InteractableState.Select)
            {
                if(Vector3.Distance(transform.localPosition, originalRelativePosition) > replacePrimitiveThreshold)
                {
                    RemoveFromMenu(shapesManager.transform);
                } else
                {
                    // Reset to original relative position
                    Debug.Log("localPosition"+transform.localPosition);
                    transform.SetLocalPositionAndRotation(originalRelativePosition, originalRelativeRotation);
                    Debug.Log("UPDATED" + transform.name + transform.localPosition + " " + originalRelativePosition + transform.localRotation + " " + originalRelativeRotation);
                }
            }
            return;
        }

        private void RemoveFromMenu(Transform newParent)
        {
            // move gameobject to new parent
            this.transform.SetParent(newParent, true);
            // update hand menu with primitive replacements
            paletteBehavior.UpdatePrimitiveOptions();
            // disable this monobehavior
            enabled = false;
            // undo material changes
        }
    }
}

