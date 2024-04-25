using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace _project.Scripts.Core
{
    public class PointerUtils
    {
        public static PointerUtils Instance = new();
        
        private GraphicRaycaster[] _raycasters;
        private Vector2 _pointerPosition;
        private PointerEventData _pointerEventData;
        private List<RaycastResult> _rayCastUIResults = new List<RaycastResult>();

        public GraphicRaycaster[] GetRaycasters()
        {
            if (_raycasters == null)
            {
                _raycasters = Object.FindObjectsOfType<GraphicRaycaster>();
            }

            return _raycasters;
        }
        
        public static bool IsPointerOverUI { 
            get {
                Instance._raycasters = Instance.GetRaycasters();
                Instance._pointerPosition = Pointer.current.position.ReadValue();
                Instance._pointerEventData = new PointerEventData(EventSystem.current)
                {
                    position = Instance._pointerPosition
                };
                Instance._rayCastUIResults.Clear();
                foreach (GraphicRaycaster raycaster in Instance._raycasters) { 
                    raycaster.Raycast(Instance._pointerEventData, Instance._rayCastUIResults);
                    if (Instance._rayCastUIResults.Count > 0)
                        break;
                }
                return Instance._rayCastUIResults.Count > 0;
            }
        } 



    }
}