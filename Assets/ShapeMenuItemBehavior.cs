using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

namespace AccidentalPicasso.UI.Palette
{

    public class ShapeMenuItemBehavior : MonoBehaviour
    {
        private Vector3 originalRelativePosition;

        private IInteractableView InteractableView { get; set; }
        protected bool _started = false;
        private ShapesManager shapesManager;
        private PaletteBehavior paletteBehavior;


        protected virtual void Awake()
        {
            shapesManager = FindObjectOfType<ShapesManager>();
            paletteBehavior = FindObjectOfType<PaletteBehavior>();
            InteractableView = GetComponentInChildren<GrabInteractable>();
        }
        protected virtual void Start()
        {
            this.BeginStart(ref _started);
            originalRelativePosition = transform.localPosition;
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
                if(transform.localPosition != originalRelativePosition)
                {
                    RemoveFromMenu(shapesManager.transform);
                }
            }
            return;
        }

        private void RemoveFromMenu(Transform newParent)
        {
            // move gameobject to new parent
            this.transform.SetParent(newParent, true);
            paletteBehavior.UpdatePrimitiveOptions();
            // disable this monobehavior
            enabled = false;
        }
    }
}

