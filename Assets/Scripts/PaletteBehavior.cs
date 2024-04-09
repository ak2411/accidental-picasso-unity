using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AccidentalPicasso.UI.Palette
{
    /// <summary>
    /// Controls the behavior of the palette.
    /// </summary>
    public class PaletteBehavior : MonoBehaviour
    {
        [SerializeField]
        private GameObject _menuParent;
        [SerializeField]
        private List<GameObject> _primitives;
        [SerializeField]
        private Transform _primitivesContainer;
        
        /// <summary>
        /// Show/hide the menu
        /// </summary>
        public void ToggleMenu(bool visible) {
            if(!visible && _menuParent.activeSelf) {
                _menuParent.SetActive(false);
            } else if(visible && !_menuParent.activeSelf) {
                _menuParent.SetActive(true);
            }
        }

        //protected void Start()
        //{
        //    // whenever it is instantiated again, check if we need to update the primitive options
        //    UpdatePrimitiveOptions();
        //}

        public void UpdatePrimitiveOptions()
        {
            if(_primitivesContainer.childCount < _primitives.Count)
            {
                foreach(GameObject primitive in _primitives)
                {
                    Transform foundPrimitiveOption = _primitivesContainer.Find(primitive.name);
                    if (foundPrimitiveOption == null)
                    {
                        // Add replacement
                        GameObject replacementPrimitive = Instantiate(primitive);
                        replacementPrimitive.transform.SetParent(_primitivesContainer, true);
                    }
                }
            }
        }
    }

}
