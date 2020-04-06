using System.Collections.Generic;

namespace Gamaga.InputSystem
{
	//this class try to imitate the virtual axis of the built in inputs for Unity
	//so you can get the input for mobile devices in a convenient way
    public static class InputManager 
    {
		private static Dictionary<string, float> axis = new Dictionary<string, float>();

		/// <summary>
		/// create a virtual axis
		/// </summary>
		/// <param name="axisName"></param>
		public static void CreateAxis(string axisName)
		{
			axis[axisName] = 0.0f;
		}

		/// <summary>
		/// get axis value
		/// </summary>
		/// <param name="axisName"></param>
		/// <returns></returns>
		public static float GetAxis(string axisName)
		{
			return axis[axisName];
		}

		/// <summary>
		/// Set axis value
		/// </summary>
		/// <param name="axisName"></param>
		/// <param name="value"></param>
		public static void SetAxis(string axisName, float value)
		{
			axis[axisName] = value;
		}
	}

}
