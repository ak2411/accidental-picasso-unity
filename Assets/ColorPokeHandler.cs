using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class ColorPokeHandler : MonoBehaviour
{
    [SerializeField]
    private PokeInteractable pokeInteractable;
    [SerializeField]
    private ShapesManager shapesManager;
    private Color _color = Color.white;
    public Color Color
    {
        get
        {
            return _color;
        }
        set
        {
            _color = value;
        }
    }

    protected void Awake()
    {
        shapesManager = FindObjectOfType<ShapesManager>();
        pokeInteractable.WhenStateChanged += HandleStateChange;
    }

    private void HandleStateChange(InteractableStateChangeArgs args)
    {
        switch (args.NewState)
        {
            case InteractableState.Select:
                shapesManager.UpdateColor(_color);
                return;
            default:
                return;
        }
            
    }
}
