using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

namespace AccidentalPicasso.UI.Palette
{
    /// <summary>
    /// Handles all the behavior for shape options
    /// </summary>
    
    public class PaletteShapeInteractableBehavior : MonoBehaviour
    {
        [SerializeField, Interface(typeof(IInteractableView))]
        private UnityEngine.Object _interactableView;
        private IInteractableView InteractableView { get; set; }

        [SerializeField]
        private PrimitiveType _shape;
        [SerializeField]
        private ShapesManager _shapeManager;
        protected bool _started = false;

        protected virtual void Awake()
        {
            InteractableView = _interactableView as IInteractableView;
        }

        protected virtual void Start()
        {

            this.BeginStart(ref _started);

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
            switch (args.NewState)
            {
                case InteractableState.Select:
                    _shapeManager.AddShape(_shape, this.transform.position);
                    return;
                default:
                    if(args.PreviousState == InteractableState.Select)
                    {
                        _shapeManager.UnselectShape(true);
                    }
                    return;
            }
        }

        public void UpdateColor(Color color)
        {
            this.GetComponent<MaterialPropertyBlockEditor>().MaterialPropertyBlock.SetColor("_Color", color);
        }
    }
}