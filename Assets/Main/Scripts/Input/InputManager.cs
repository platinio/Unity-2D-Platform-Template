using System.Collections.Generic;

namespace Gamaga.InputSystem
{
    public static class InputManager 
    {
		private static Dictionary<string, float> axis = new Dictionary<string, float>();

		public static void CreateAxis(string axisName)
		{
			axis[axisName] = 0.0f;
		}

		public static float GetAxis(string axisName)
		{
			return axis[axisName];
		}

		public static void SetAxis(string axisName, float value)
		{
			axis[axisName] = value;
		}
	}

}
