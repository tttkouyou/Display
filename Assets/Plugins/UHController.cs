using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using System.Runtime.InteropServices;


public class UHController : MonoBehaviour {

	public string serial_port_name = "/dev/cu.usbserial-A104X0TS";

	void Start()
	{
		print ("Opening UH");
		int flags = UH.CONTINUOUS_MODE_QUATERNION | UH.CONTINUOUS_MODE_PHOTOREFLECTOR;
		UH.open(serial_port_name,flags);

	}

	void Update()
	{
		//print ("Reading from UH...");

		if (UH.exists(serial_port_name)) {

			//Set object rotation to match UHs rotation
			transform.rotation = UH.rotation(serial_port_name);
			print ("Rotation UH...");

			//Set color from Photo Reflector values
			double[] photo_reflectors = UH.photoReflectors(serial_port_name);
			Vector3 color_vec;
			color_vec.x = (float)photo_reflectors[0];
			color_vec.y = (float)photo_reflectors[1];
			color_vec.z = (float)photo_reflectors[2];
			color_vec.Normalize ();
			Color main_color = Color.red;
			main_color.r = color_vec.x;
			main_color.g = color_vec.y;
			main_color.b = color_vec.z;
			main_color.a = 1;
			Renderer rend = GetComponent<Renderer> ();
			rend.material.shader = Shader.Find ("Specular");
			rend.material.SetColor ("_Color", main_color);


			if (Input.GetKeyDown (KeyCode.Alpha0)) {
				UH.stimulateChannel (serial_port_name, 0);
			}else if (Input.GetKeyDown (KeyCode.Alpha1)) {
				UH.stimulateChannel (serial_port_name, 1);
			}else if (Input.GetKeyDown (KeyCode.Alpha2)) {
				UH.stimulateChannel (serial_port_name, 2);
			}else if (Input.GetKeyDown (KeyCode.Alpha3)) {
				UH.stimulateChannel (serial_port_name, 3);
			}else if (Input.GetKeyDown (KeyCode.Alpha4)) {
				UH.stimulateChannel (serial_port_name, 4);
			}else if (Input.GetKeyDown (KeyCode.Alpha5)) {
				UH.stimulateChannel (serial_port_name, 5);
			}else if (Input.GetKeyDown (KeyCode.Alpha6)) {
				UH.stimulateChannel (serial_port_name, 6);
			}else if (Input.GetKeyDown (KeyCode.Alpha7)) {
				UH.stimulateChannel (serial_port_name, 7);
			}


		}

	}

	void OnDestroy()
	{
		print ("Closing UH");
		UH.close (serial_port_name);
	}

}
