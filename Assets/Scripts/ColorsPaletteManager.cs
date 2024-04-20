using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class ColorsPaletteManager : MonoBehaviour
{
    [SerializeField]
    private List<string> _colors = new List<string> { "#EB5757", "#F2994A", "#F2C94C", "#9B51E0", "#56CCF2", "#2F80ED", "#219653", "#000000", "#FFFFFF", "#E0E0E0" };

    [SerializeField]
    private GameObject _colorVisualRef;

    [SerializeField]
    private float _padding = -0.02f;
    [SerializeField]
    private string _colorShaderName = "_Color";

    private void Awake()
    {
        var position = this.transform.position;
        for (int i = 0; i < _colors.Count; i++)
        {
            var color = _colors[i];
            GameObject colorItem = Instantiate(_colorVisualRef);
            colorItem.transform.position = position;
            colorItem.transform.SetParent(this.transform, true);
            var material = colorItem.GetComponentInChildren<MaterialPropertyBlockEditor>();
            var _colorShaderID = Shader.PropertyToID(_colorShaderName);
            Color parsedColor;
            if(ColorUtility.TryParseHtmlString(color, out parsedColor))
            {
                material.MaterialPropertyBlock.SetColor(_colorShaderID, parsedColor);
                colorItem.GetComponent<ColorPokeHandler>().Color = parsedColor;
                colorItem.GetComponent<ColorPokeHandler>().StartPosition = colorItem.transform.localPosition;
            }
            position = position + new Vector3(0.0f, _padding, 0.0f);
            if (i+1 == _colors.Count/2)
            {
                position = this.transform.position + new Vector3(_padding, 0.0f, 0.0f);
            }
        }
    }
}
