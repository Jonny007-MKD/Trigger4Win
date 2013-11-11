using System;
using Systemm = System;
using Forms = System.Windows.Forms;
using System.Windows.Forms;
using System.Drawing;

namespace Tasker.Classes.Screen
{
	/// <summary>For fixed-resolution display devices only, how the display presents a low-resolution mode on a higher-resolution display. For example, if a display device's resolution is fixed at 1024 x 768 pixels but its mode is set to 640 x 480 pixels, the device can either display a 640 x 480 image somewhere in the interior of the 1024 x 768 screen space or stretch the 640 x 480 image to fill the larger screen space</summary>
	public enum FixedOutput : byte
	{
		/// <summary>The display's default setting.</summary>
		Default = 0,
		/// <summary>The low-resolution image is centered in the larger screen space.</summary>
		Stretch = 1,
		/// <summary>The low-resolution image is stretched to fill the larger screen space.</summary>
		Center = 2,
	}
	/// <summary>Specifies the color resolution, in bits per pixel, of the display device</summary>
	public enum ColorDepth : byte
	{
		/// <summary>4 Bit -> 16 Colors</summary>
		_16_Colors = 4,
		/// <summary>4 Bit -> 16 Colors</summary>
		_4bit = 4,
		/// <summary>8 Bit -> 256 Colors</summary>
		_256_Colors = 8,
		/// <summary>8 Bit -> 256 Colors</summary>
		_8bit = 8,
		/// <summary>16 Bit -> 65.536 Colors</summary>
		_65536_Colors = 16,
		/// <summary>16 Bit -> 65.536 Colors</summary>
		_16bit = 16,
		/// <summary>32 Bit -> 4.294.967.296 Colors</summary>
		_4294967296_Colors = 32,
		/// <summary>32 Bit -> 4.294.967.296 Colors</summary>
		_32bit = 32,
	}

	/// <summary><para>A class that provides more information than <see cref="Screen"/></para></summary>
	public class ScreenEx : Device.Device // : Forms.Screen
	{
		#region Properties
		/// <summary><para>Gets the bounds of the display.</para></summary>
		private Rectangle bounds;
		/// <summary><para>Gets the bounds of the display.</para></summary>
		public Rectangle Bounds
		{
			get { return bounds; }
			set
			{
				bounds = value;
				this.fields |= DM.PELSWIDTH | DM.PELSHEIGHT;
			}
		}
		/// <summary><para>Specifies the color resolution, in bits per pixel, of the display device</para></summary>
		private byte bitsPerPixel;
		/// <summary><para>Specifies the color resolution, in bits per pixel, of the display device</para></summary>
		public byte BitsPerPixel
		{
			get { return bitsPerPixel; }
			set
			{
				bitsPerPixel = value;
				fields |= DM.BITSPERPEL;
			}
		}
		/// <summary><para>Specifies the color resolution, in bits per pixel, of the display device</para></summary>
		public ColorDepth ColorDepth
		{
			get { return (ColorDepth)this.BitsPerPixel; }
			set { this.BitsPerPixel = (byte)value; }
		}
		/// <summary><para>Gets a value indicating whether a particular display is the primary device.</para></summary>
		private bool primary;
		/// <summary><para>Gets a value indicating whether a particular display is the primary device.</para></summary>
		public bool Primary
		{
			get { return primary; }
			private set { primary = value; }
		}
		/// <summary>The version number of the initialization data specification on which the structure is based</summary>
		private short specVersion;
		/// <summary>The version number of the initialization data specification on which the structure is based</summary>
		public short SpecVersion
		{
			get { return specVersion; }
			private set { specVersion = value; }
		}
		/// <summary>The driver version number assigned by the driver developer</summary>
		private short driverVersion;
		/// <summary>The driver version number assigned by the driver developer</summary>
		public short DriverVersion
		{
			get { return driverVersion; }
			private set { driverVersion = value; }
		}
		/// <summary><para>For display devices only, the orientation at which images should be presented.</para></summary>
		private Forms.ScreenOrientation orientation;
		/// <summary><para>For display devices only, the orientation at which images should be presented.</para></summary>
		public Forms.ScreenOrientation Orientation
		{
			get { return orientation; }
			set
			{
				orientation = value;
				fields |= DM.DISPLAYORIENTATION;
			}
		}
		/// <summary>For fixed-resolution display devices only, how the display presents a low-resolution mode on a higher-resolution display. For example, if a display device's resolution is fixed at 1024 x 768 pixels but its mode is set to 640 x 480 pixels, the device can either display a 640 x 480 image somewhere in the interior of the 1024 x 768 screen space or stretch the 640 x 480 image to fill the larger screen space</summary>
		private FixedOutput fixedOutput;
		/// <summary>For fixed-resolution display devices only, how the display presents a low-resolution mode on a higher-resolution display. For example, if a display device's resolution is fixed at 1024 x 768 pixels but its mode is set to 640 x 480 pixels, the device can either display a 640 x 480 image somewhere in the interior of the 1024 x 768 screen space or stretch the 640 x 480 image to fill the larger screen space</summary>
		public FixedOutput FixedOutput
		{
			get { return fixedOutput; }
			set
			{
				fixedOutput = value;
				fields |= DM.DISPLAYFIXEDOUTPUT;
			}
		}
		/// <summary><para>The number of pixels per logical inch.</para></summary>
		private short logPixels;
		/// <summary><para>The number of pixels per logical inch.</para></summary>
		public short LogPixels
		{
			get { return logPixels; }
			set
			{
				logPixels = value;
				fields |= DM.LOGPIXELS;
			}
		}
		/// <summary><para>Specifies the device's display mode. This member can be a combination of the following values.</para></summary>
		/// <example>
		/// <para>GRAYSCALE: Specifies that the display is a noncolor device. If this flag is not set, color is assumed.</para>
		/// <para>INTERLACED: Specifies that the display mode is interlaced. If the flag is not set, noninterlaced is assumed.</para>
		/// </example>
		private int displayFlags;
		/// <summary><para>Specifies the device's display mode. This member can be a combination of the following values.</para></summary>
		/// <example>
		/// <para>GRAYSCALE: Specifies that the display is a noncolor device. If this flag is not set, color is assumed.</para>
		/// <para>INTERLACED: Specifies that the display mode is interlaced. If the flag is not set, noninterlaced is assumed.</para>
		/// </example>
		public int DisplayFlags
		{
			get { return displayFlags; }
			set
			{
				displayFlags = value;
				fields |= DM.DISPLAYFLAGS;
			}
		}
		/// <summary>
		/// <para>Specifies the frequency, in hertz (cycles per second), of the display device in a particular mode. This value is also known as the display device's vertical refresh rate. Display drivers use this member. It is used, for example, in the ChangeDisplaySettings function. Printer drivers do not use this member.</para>
		/// <para>May be the value 0 or 1. These values represent the display hardware's default refresh rate. This default rate is typically set by switches on a display card or computer motherboard, or by a configuration program that does not use display functions such as ChangeDisplaySettings.</para>
		/// </summary>
		private byte frequency;
		/// <summary>
		/// <para>Specifies the frequency, in hertz (cycles per second), of the display device in a particular mode. This value is also known as the display device's vertical refresh rate. Display drivers use this member. It is used, for example, in the ChangeDisplaySettings function. Printer drivers do not use this member.</para>
		/// <para>May be the value 0 or 1. These values represent the display hardware's default refresh rate. This default rate is typically set by switches on a display card or computer motherboard, or by a configuration program that does not use display functions such as ChangeDisplaySettings.</para>
		/// </summary>
		public byte Frequency
		{
			get { return frequency; }
			set
			{
				frequency = value;
				fields |= DM.DISPLAYFREQUENCY;
			}
		}

		/// <summary>Specifies whether certain members of the DEVMODE structure have been initialized. If a member is initialized, its corresponding bit is set, otherwise the bit is clear. A driver supports only those DEVMODE members that are appropriate for the printer or display technology.</summary>
		/// <remarks>http://msdn.microsoft.com/en-us/library/windows/desktop/dd183565%28v=vs.85%29.aspx</remarks>
		private DM fields = 0;
		#endregion

		#region enum Field
		/// <summary>
		/// <para>Specifies whether certain members of the DEVMODE structure have been initialized. If a member is initialized, its corresponding bit is set, otherwise the bit is clear. A driver supports only those DEVMODE members that are appropriate for the printer or display technology.</para>
		/// </summary>
		[Flags]
		private enum DM : int
		{
			///<summary><para></para></summary>
			ORIENTATION = 0x00000001,
			///<summary><para></para></summary>
			PAPERSIZE = 0x00000002,
			///<summary><para></para></summary>
			PAPERLENGTH = 0x00000004,
			///<summary><para></para></summary>
			PAPERWIDTH = 0x00000008,
			///<summary><para></para></summary>
			SCALE = 0x00000010,
			///<summary><para></para></summary>
			COPIES = 0x00000100,
			///<summary><para></para></summary>
			DEFAULTSOURCE = 0x00000200,
			///<summary><para></para></summary>
			PRINTQUALITY = 0x00000400,
			///<summary><para></para></summary>
			POSITION = 0x00000020,
			///<summary><para></para></summary>
			DISPLAYORIENTATION = 0x00000080,
			///<summary><para></para></summary>
			DISPLAYFIXEDOUTPUT = 0x20000000,
			///<summary><para></para></summary>
			COLOR = 0x00000800,
			///<summary><para></para></summary>
			DUPLEX = 0x00001000,
			///<summary><para></para></summary>
			YRESOLUTION = 0x00002000,
			///<summary><para></para></summary>
			TTOPTION = 0x00004000,
			///<summary><para></para></summary>
			COLLATE = 0x00008000,
			///<summary><para></para></summary>
			FORMNAME = 0x00010000,
			///<summary><para></para></summary>
			LOGPIXELS = 0x00020000,
			///<summary><para></para></summary>
			BITSPERPEL = 0x00040000,
			///<summary><para></para></summary>
			PELSWIDTH = 0x00080000,
			///<summary><para></para></summary>
			PELSHEIGHT = 0x00100000,
			///<summary><para></para></summary>
			DISPLAYFLAGS = 0x00200000,
			///<summary><para></para></summary>
			NUP = 0x00000040,
			///<summary><para></para></summary>
			DISPLAYFREQUENCY = 0x00400000,
			///<summary><para></para></summary>
			ICMMETHOD = 0x00800000,
			///<summary><para></para></summary>
			ICMINTENT = 0x01000000,
			///<summary><para></para></summary>
			MEDIATYPE = 0x02000000,
			///<summary><para></para></summary>
			DITHERTYPE = 0x04000000,
			///<summary><para></para></summary>
			PANNINGWIDTH = 0x08000000,
			///<summary><para></para></summary>
			PANNINGHEIGHT = 0x10000000,
		}
		#endregion

		#region Constructors
		public ScreenEx(Forms.Screen screen, ScreenSettingsDevMode devMode) : base(screen.DeviceName)
		{
			this.Type = DeviceType.Screen;
			this.Name = screen.DeviceName;
			this.BitsPerPixel = (byte)screen.BitsPerPixel;
			this.Bounds = new Rectangle(screen.Bounds.Location, screen.Bounds.Size);
			this.Primary = screen.Primary;
			this.SpecVersion = devMode.dmSpecVersion;
			this.DriverVersion = devMode.dmDriverVersion;
			this.FixedOutput = (FixedOutput)devMode.dmDisplayFixedOutput;
			this.LogPixels = devMode.dmLogPixels;
			this.ColorDepth = (ColorDepth)devMode.dmBitsPerPel;
			this.DisplayFlags = devMode.dmDisplayFlags;
			this.Frequency = (byte)devMode.dmDisplayFrequency;
			this.Orientation = (ScreenOrientation)devMode.dmDisplayOrientation;
			this.fields = 0;
		}
		#endregion

		#region Methods
		/// <summary>
		/// <para>Converts this class to a <see cref="ScreenSettingsDevMode"/> struct which then may be used to update the settings</para>
		/// </summary>
		/// <returns></returns>
		public ScreenSettingsDevMode ToDEVMODE()
		{
			ScreenSettingsDevMode devMode = new ScreenSettingsDevMode(true);
			devMode.dmBitsPerPel = this.BitsPerPixel;
			devMode.dmDeviceName = this.Name;
			devMode.dmDisplayFixedOutput = (int)this.FixedOutput;
			devMode.dmDisplayFlags = this.DisplayFlags;
			devMode.dmDisplayFrequency = this.Frequency;
			devMode.dmDisplayOrientation = (int)this.Orientation;
			devMode.dmDriverVersion = this.DriverVersion;
			devMode.dmFields = (int)this.fields;
			devMode.dmFormName = "";
			devMode.dmLogPixels = this.LogPixels;
			devMode.dmPelsHeight = this.Bounds.Height;
			devMode.dmPelsWidth = this.Bounds.Width;
			devMode.dmPositionX = this.Bounds.X;
			devMode.dmPositionY = this.Bounds.Y;
			devMode.dmSpecVersion = this.SpecVersion;
			devMode.dmSize = (short)Systemm.Runtime.InteropServices.Marshal.SizeOf(devMode);
			return devMode;
		}
		#endregion
	}
}
