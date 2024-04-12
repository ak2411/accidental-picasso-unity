using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using AccidentalPicasso.UI.Palette;

public class ColorPokeHandler : MonoBehaviour
{
    [SerializeField]
    private PokeInteractable pokeInteractable;
    [SerializeField]
    private PaletteBehavior paletteBehavior;
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
        paletteBehavior = FindObjectOfType<PaletteBehavior>();
        pokeInteractable.WhenStateChanged += HandleStateChange;
    }

    private void HandleStateChange(InteractableStateChangeArgs args)
    {
        switch (args.NewState)
        {
            case InteractableState.Select:
                paletteBehavior.UpdateColor(_color);
                return;
            default:
                return;
        }
    }
}
