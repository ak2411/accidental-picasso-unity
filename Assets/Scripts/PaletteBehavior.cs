using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AccidentalPicasso.UI.Palette
{
    /// <summary>
    /// Simple primitive class with variables that are helpful for spawning new primitives
    /// </summary>
    public class Primitive
    {
        public string name;
        public Vector3 position;
        public Quaternion rotation;
        public GameObject gameObject;

        public Primitive(string primitiveName, GameObject primitiveGameObject, Vector3 primitivePosition, Quaternion primitiveRotation)
        {
            name = primitiveName;
            position = primitivePosition;
            gameObject = primitiveGameObject;
            rotation = primitiveRotation;
        }
    }
    /// <summary>
    /// Controls the behavior of the palette.
    /// </summary>
    public class PaletteBehavior : MonoBehaviour
    {
        [SerializeField]
        private GameObject _menuParent;
        [SerializeField]
        private List<GameObject> _primitives;
        private List<Primitive> primitiveReferences = new List<Primitive>();
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

        protected void Awake()
        {
            foreach(GameObject primitive in _primitives)
            {
                Primitive createdPrimitive = new Primitive(primitive.name, primitive.gameObject, primitive.transform.localPosition, primitive.transform.localRotation);
                primitiveReferences.Add(createdPrimitive);
            }
        }

        //protected void Start()
        //{
        //    // whenever it is instantiated again, check if we need to update the primitive options
        //    UpdatePrimitiveOptions();
        //}

        public void UpdatePrimitiveOptions()
        {
            if(_primitivesContainer.childCount < primitiveReferences.Count)
            {
                foreach(Primitive primitive in primitiveReferences)
                {
                    Transform foundPrimitiveOption = _primitivesContainer.Find(primitive.name);
                    if (foundPrimitiveOption == null)
                    {
                        // Add replacement
                        GameObject replacementPrimitive = Instantiate(primitive.gameObject);
                        replacementPrimitive.name = primitive.name;
                        replacementPrimitive.transform.SetParent(_primitivesContainer, false);
                        replacementPrimitive.transform.SetLocalPositionAndRotation(primitive.position, primitive.rotation);
                    }
                }
            }
        }
    }

}
