using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamaga.InputSystem
{

	public static class JoystickAxis
	{
		private static Dictionary<string, float> m_axis = new Dictionary<string, float>();

		public static void CreateAxis(string axisName)
		{
			m_axis[axisName] = 0.0f;
		}

		public static float GetAxis(string axisName)
		{
			return m_axis[axisName];
		}

		public static void SetAxis(string axisName, float value)
		{
			m_axis[axisName] = value;
		}

	}

}
