using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
struct UHSimpleInterface
{
	public bool valid;

	public double qw;
	public double qx;
	public double qy;
	public double qz;

	public double pr0;
	public double pr1;
	public double pr2;
	public double pr3;
	public double pr4;
	public double pr5;
	public double pr6;
	public double pr7;

	public double temp;

	public float last_computed_frequency;
}

public static class UH {
	
	public static int CONTINUOUS_MODE_QUATERNION       = 0x01;
	public static int CONTINUOUS_MODE_RAW_ACCELERATION = 0x02;
	public static int CONTINUOUS_MODE_RAW_GYROMETER    = 0x04;
	public static int CONTINUOUS_MODE_PHOTOREFLECTOR   = 0x08;
	public static int CONTINUOUS_MODE_TEMPERATURE      = 0x10;

	[DllImport("libUHAPI")]
	private static extern void openUH (string serialport, int flags); 

	[DllImport("libUHAPI")]
	private static extern void closeUH (string serialport); 

	[DllImport("libUHAPI")]
	private static extern UHSimpleInterface readUH (string serialport); 

	[DllImport("libUHAPI")]
	private static extern bool existsUH(string serialport); 

	[DllImport("libUHAPI")]
	private static extern void stimulateChannelUH(string serial_port, int channel);

	[DllImport("libUHAPI")]
	private static extern void increaseStimulationTimeUH(string serial_port);

	[DllImport("libUHAPI")]
	private static extern void decreaseStimulationTimeUH(string serial_port);

	[DllImport("libUHAPI")]
	private static extern void increaseStimulationVoltageUH(string serial_port);

	[DllImport("libUHAPI")]
	private static extern void decreaseStimulationVoltageUH(string serial_port);

	public static void open(string serialport, int flags)
	{
		openUH (serialport, flags);
	}

	public static void close(string serialport)
	{
		closeUH (serialport);
	}

	public static bool exists(string serialport)
	{
		return existsUH (serialport);
	}

	public static Quaternion rotation(string serialport)
	{
		UHSimpleInterface outputinfo = readUH (serialport);
		Quaternion Rot;
		Rot.w = (float)outputinfo.qw;
		Rot.x = (-1) * (float)outputinfo.qx;
		Rot.y = (-1) * (float)outputinfo.qz;
		Rot.z = (-1) * (float)outputinfo.qy;
		return Rot;
	}

	public static double[] photoReflectors(string serialport)
	{
		UHSimpleInterface outputinfo = readUH (serialport);
		double[] return_value = new double[8];
		return_value [0] = outputinfo.pr0;
		return_value [1] = outputinfo.pr1;
		return_value [2] = outputinfo.pr2;
		return_value [3] = outputinfo.pr3;
		return_value [4] = outputinfo.pr4;
		return_value [5] = outputinfo.pr5;
		return_value [6] = outputinfo.pr6;
		return_value [7] = outputinfo.pr7;
		return return_value;
	}

	public static void stimulateChannel(string serialport, int channel)
	{
		stimulateChannelUH (serialport, channel);
	}

	public static void increaseStimulationTime(string serialport)
	{
		increaseStimulationTimeUH (serialport);
	}

	public static void decreaseStimulationTime(string serialport)
	{
		decreaseStimulationTimeUH (serialport);
	}

	public static void increaseStimulationVoltage(string serialport)
	{
		increaseStimulationVoltageUH (serialport);
	}

	public static void decreaseStimulationVoltage(string serialport)
	{
		decreaseStimulationVoltageUH (serialport);
	}

}
