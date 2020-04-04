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
		[SerializeField] MovementDirection m_movementDirection = MovementDirection.Both;
		[SerializeField] private float m_movementRange = 40.0f;
		[SerializeField] private string m_horizontalAxisName = "Horizontal";
		[SerializeField] private string m_verticalAxisName = "Vertical";
		[SerializeField] private Image m_base = null;
		[SerializeField] private Image m_stick = null;
		[SerializeField] private bool m_hideOnRelease = false;
		[SerializeField] private bool m_useDistance = false;
		#endregion

		#region PRIVATE
		private RectTransform m_stickRect = null;
		private RectTransform m_baseRect = null;
		private Vector2 m_stickCenterPos = Vector2.zero;
		private Vector2 m_stickInitialPos = Vector2.zero;
		private Vector2 m_baseInitialPos = Vector2.zero;
		#endregion

		void Start()
		{
			//get references
			m_stickRect = m_stick.GetComponent<RectTransform>();
			m_baseRect = m_base.GetComponent<RectTransform>();

			//initialize values
			m_stickCenterPos = m_stickRect.position;
			m_stickInitialPos = m_stickRect.position;
			m_baseInitialPos = m_baseRect.position;

			//hide the joystick
			if (m_hideOnRelease)
				Hide(true);

			//create the axis
			InputManager.CreateAxis(m_horizontalAxisName);
			InputManager.CreateAxis(m_verticalAxisName);
		}


		public void OnDrag(PointerEventData eventData)
		{
			//set joystick pos
			Vector2 stickPos = eventData.position;


			//restric joystick movement
			if (m_movementDirection == MovementDirection.Vertical)
			{
				stickPos.x = m_stickCenterPos.x;
			}
			else if (m_movementDirection == MovementDirection.Horizontal)
			{
				stickPos.y = m_stickCenterPos.y;
			}



			Vector2 difference = new Vector2(stickPos.x, stickPos.y) - m_stickCenterPos;
			float diffMagnitude = difference.magnitude;
			Vector2 normalizedDifference = difference / diffMagnitude;

			// if the joystick is out of range
			if (diffMagnitude > m_movementRange)
			{
				stickPos = m_stickCenterPos + normalizedDifference * m_movementRange;
			}

			Vector2 m = new Vector2(m_stickRect.position.x, m_stickRect.position.y) - new Vector2(m_baseRect.position.x, m_baseRect.position.y);
			m.Normalize();

			//if we use distance, this is most commonf for vehicles or running etc
			if (m_useDistance)
			{
				//distance from center to stcik
				float distance = Vector2.Distance(stickPos, m_baseRect.position);
				m = new Vector2(m.x * (distance / m_movementRange), m.y * (distance / m_movementRange));
			}



			//updateaxis value
			InputManager.SetAxis(m_horizontalAxisName, m.x);
			InputManager.SetAxis(m_verticalAxisName, m.y);
			m_stickRect.position = stickPos;
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			//set valus to 0
			InputManager.SetAxis(m_horizontalAxisName, 0.0f);
			InputManager.SetAxis(m_verticalAxisName, 0.0f);

			m_stickRect.anchoredPosition = Vector2.zero;
			m_baseRect.position = m_baseInitialPos;

			if (m_hideOnRelease)
				Hide(true);
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			//hide joystick
			if (m_hideOnRelease)
			{
				Hide(false);
			}

			m_baseRect.position = eventData.position; ;
			m_stickRect.position = eventData.position;
			m_stickCenterPos = eventData.position;

		}


		private void Hide(bool hide)
		{
			m_stick.gameObject.SetActive(!hide);
			m_base.gameObject.SetActive(!hide);
		}


	}

}

