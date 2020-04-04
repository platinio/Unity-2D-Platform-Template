using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Gamaga.InputSystem
{
	public enum MovementDirection
	{
		Vertical,
		Horizontal,
		Both
	}

	/// <summary>
	/// Simple virtual joystick
	/// </summary>
	public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
	{

		#region INSPECTOR
		[SerializeField] MovementDirection movementDirection = MovementDirection.Both;
		[SerializeField] private float movementRange = 40.0f;
		[SerializeField] private string horizontalAxisName = "Horizontal";
		[SerializeField] private string verticalAxisName = "Vertical";
		[SerializeField] private Image baseStick = null;
		[SerializeField] private Image stick = null;
		[SerializeField] private bool hideOnRelease = false;
		[SerializeField] private bool useDistance = false;
		#endregion

		#region PRIVATE
		private RectTransform stickRect = null;
		private RectTransform baseRect = null;
		private Vector2 stickCenterPos = Vector2.zero;
		private Vector2 stickInitialPos = Vector2.zero;
		private Vector2 baseInitialPos = Vector2.zero;
		#endregion

		void Start()
		{
			//get references
			stickRect = stick.GetComponent<RectTransform>();
			baseRect = baseStick.GetComponent<RectTransform>();

			//initialize values
			stickCenterPos = stickRect.position;
			stickInitialPos = stickRect.position;
			baseInitialPos = baseRect.position;

			//hide the joystick
			if (hideOnRelease)
				Hide(true);

			//create the axis
			InputManager.CreateAxis(horizontalAxisName);
			InputManager.CreateAxis(verticalAxisName);
		}


		public void OnDrag(PointerEventData eventData)
		{
			//set joystick pos
			Vector2 stickPos = eventData.position;


			//restric joystick movement
			if (movementDirection == MovementDirection.Vertical)
			{
				stickPos.x = stickCenterPos.x;
			}
			else if (movementDirection == MovementDirection.Horizontal)
			{
				stickPos.y = stickCenterPos.y;
			}



			Vector2 difference = new Vector2(stickPos.x, stickPos.y) - stickCenterPos;
			float diffMagnitude = difference.magnitude;
			Vector2 normalizedDifference = difference / diffMagnitude;

			// if the joystick is out of range
			if (diffMagnitude > movementRange)
			{
				stickPos = stickCenterPos + normalizedDifference * movementRange;
			}

			Vector2 m = new Vector2(stickRect.position.x, stickRect.position.y) - new Vector2(baseRect.position.x, baseRect.position.y);
			m.Normalize();

			//if we use distance, this is most commonf for vehicles or running etc
			if (useDistance)
			{
				//distance from center to stcik
				float distance = Vector2.Distance(stickPos, baseRect.position);
				m = new Vector2(m.x * (distance / movementRange), m.y * (distance / movementRange));
			}



			//updateaxis value
			InputManager.SetAxis(horizontalAxisName, m.x);
			InputManager.SetAxis(verticalAxisName, m.y);
			stickRect.position = stickPos;
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			//set valus to 0
			InputManager.SetAxis(horizontalAxisName, 0.0f);
			InputManager.SetAxis(verticalAxisName, 0.0f);

			stickRect.anchoredPosition = Vector2.zero;
			baseRect.position = baseInitialPos;

			if (hideOnRelease)
				Hide(true);
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			//hide joystick
			if (hideOnRelease)
			{
				Hide(false);
			}

			baseRect.position = eventData.position; ;
			stickRect.position = eventData.position;
			stickCenterPos = eventData.position;

		}


		private void Hide(bool hide)
		{
			stick.gameObject.SetActive(!hide);
			baseStick.gameObject.SetActive(!hide);
		}


	}

}

