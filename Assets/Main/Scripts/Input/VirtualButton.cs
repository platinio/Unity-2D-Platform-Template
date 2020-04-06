using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Gamaga.InputSystem 
{
    /// <summary>
    /// This is just an middle man for passing the mobile UI input to the InputManager
    /// so we can read the Input in a way similar to the built in InputManager
    /// </summary>
    public class VirtualButton : MonoBehaviour , IPointerDownHandler , IPointerUpHandler , IPointerExitHandler
    {
        [SerializeField] private string axisName = null;
        [SerializeField] private Color pressedColor = Color.black;
        [SerializeField] private Color normalColor = Color.white;

        private bool isPressed = false;
        private bool pointerDownThisFrame = false;
        private Image image = null;
       
        private void Awake()
        {
            image = GetComponent<Image>();
            InputManager.CreateAxis(axisName);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            isPressed = true;
            pointerDownThisFrame = true;           
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isPressed = false;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isPressed = false;           
        }

        private void Update()
        {
            image.color = isPressed ? pressedColor : normalColor;
            InputManager.SetAxis(axisName, pointerDownThisFrame ? 1.0f : 0.0f);
            pointerDownThisFrame = false;
        }


    }
}

