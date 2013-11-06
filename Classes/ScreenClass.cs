using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Tasker.Classes.Screen
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct ScreenSettingsDevMode
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		public string dmDeviceName;

		/// <summary>The version number of the initialization data specification on which the structure is based</summary>
		public short dmSpecVersion;
		/// <summary>The driver version number assigned by the driver developer</summary>
		public short dmDriverVersion;
		/// <summary>Specifies the size, in bytes, of the DEVMODE structure, not including any private driver-specific data that might follow the structure's public members. Set this member to sizeof (DEVMODE) to indicate the version of the DEVMODE structure being used. </summary>
		public short dmSize;
		/// <summary>Contains the number of bytes of private driver-data that follow this structure. If a device driver does not use device-specific information, set this member to zero.</summary>
		public short dmDriverExtra;
		/// <summary>Specifies whether certain members of the DEVMODE structure have been initialized. If a member is initialized, its corresponding bit is set, otherwise the bit is clear. A driver supports only those DEVMODE members that are appropriate for the printer or display technology.</summary>
		/// <remarks>http://msdn.microsoft.com/en-us/library/windows/desktop/dd183565%28v=vs.85%29.aspx</remarks>
		public int dmFields;
		/// <summary>For display devices only, a POINTL structure that indicates the positional coordinates of the display device in reference to the desktop area. The primary display device is always located at coordinates (0,0).</summary>
		public int dmPositionX;
		/// <summary>For display devices only, a POINTL structure that indicates the positional coordinates of the display device in reference to the desktop area. The primary display device is always located at coordinates (0,0).</summary>
		public int dmPositionY;
		/// <summary><para>For display devices only, the orientation at which images should be presented. If DM_DISPLAYORIENTATION is not set, this member must be zero. If DM_DISPLAYORIENTATION is set, this member must be one of the following values</para></summary>
		/// <example>
		/// <para>DMDO_DEFAULT (0): The display orientation is the natural orientation of the display device; it should be used as the default.</para>
		/// <para>DMDO_90 (1): The display orientation is rotated 90 degrees (measured clockwise) from DMDO_DEFAULT.</para>
		/// <para>DMDO_180 (2): The display orientation is rotated 180 degrees (measured clockwise) from DMDO_DEFAULT.</para>
		/// <para>DMDO_270 (3): The display orientation is rotated 270 degrees (measured clockwise) from DMDO_DEFAULT.</para>
		/// </example>
		/// <remarks>Windows 2000: Not supported</remarks>
		public int dmDisplayOrientation;
		/// <summary>For fixed-resolution display devices only, how the display presents a low-resolution mode on a higher-resolution display. For example, if a display device's resolution is fixed at 1024 x 768 pixels but its mode is set to 640 x 480 pixels, the device can either display a 640 x 480 image somewhere in the interior of the 1024 x 768 screen space or stretch the 640 x 480 image to fill the larger screen space. If DM_DISPLAYFIXEDOUTPUT is not set, this member must be zero. If DM_DISPLAYFIXEDOUTPUT is set, this member must be one of the following values</summary>
		/// <example>
		/// <para>DMDFO_DEFAULT: The display's default setting.</para>
		/// <para>DMDFO_CENTER: The low-resolution image is centered in the larger screen space.</para>
		/// <para>DMDFO_STRETCH: The low-resolution image is stretched to fill the larger screen space.</para>
		/// </example>
		/// <remarks>Windows 2000:  Not supported</remarks>
		public int dmDisplayFixedOutput;
		/// <summary> Switches between color and monochrome on color printers. The following are the possible values:</summary>
		/// <example><para>DMCOLOR_COLOR</para><para>DMCOLOR_MONOCHROME</para></example>
		public short dmColor;
		/// <summary><para>Selects duplex or double-sided printing for printers capable of duplex printing. Following are the possible values.</para></summary>
		/// <example>
		/// <para>DMDUP_SIMPLEX: Normal (nonduplex) printing.</para>
		/// <para>DMDUP_HORIZONTAL: Short-edge binding, that is, the long edge of the page is horizontal.</para>
		/// <para>DMDUP_VERTICAL: Long-edge binding, that is, the long edge of the page is vertical.</para>
		/// </example>
		public short dmDuplex;
		/// <summary><para>Specifies the y-resolution, in dots per inch, of the printer. If the printer initializes this member, the dmPrintQuality member specifies the x-resolution, in dots per inch, of the printer.</para></summary>
		public short dmYResolution;
		/// <summary><para> Specifies how TrueType fonts should be printed. This member can be one of the following values.</para></summary>
		/// <example>
		/// <para>DMTT_BITMAP: Prints TrueType fonts as graphics. This is the default action for dot-matrix printers.</para>
		/// <para>DMTT_DOWNLOAD: Downloads TrueType fonts as soft fonts. This is the default action for Hewlett-Packard printers that use Printer Control Language (PCL).</para>
		/// <para>DMTT_DOWNLOAD_OUTLINE:  Downloads TrueType fonts as outline soft fonts. </para>
		/// <para>DMTT_SUBDEV: Substitutes device fonts for TrueType fonts. This is the default action for PostScript printers.</para>
		/// </example>
		public short dmTTOption;
		/// <summary><para> Specifies whether collation should be used when printing multiple copies. (This member is ignored unless the printer driver indicates support for collation by setting the dmFields member to DM_COLLATE.) This member can be one of the following values.</para></summary>
		/// <example>
		/// <para>DMCOLLATE_TRUE: Collate when printing multiple copies.</para>
		/// <para>DMCOLLATE_FALSE: Do not collate when printing multiple copies.</para>
		/// </example>
		public short dmCollate;

		/// <summary><para>A zero-terminated character array that specifies the name of the form to use; for example, "Letter" or "Legal". A complete set of names can be retrieved by using the EnumForms function.</para></summary>
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		public string dmFormName;

		/// <summary><para>The number of pixels per logical inch. Printer drivers do not use this member.</para></summary>
		public short dmLogPixels;
		/// <summary><para>Specifies the color resolution, in bits per pixel, of the display device (for example: 4 bits for 16 colors, 8 bits for 256 colors, or 16 bits for 65,536 colors). Display drivers use this member, for example, in the ChangeDisplaySettings function. Printer drivers do not use this member.</para></summary>
		public short dmBitsPerPel;
		/// <summary>Specifies the width, in pixels, of the visible device surface. Display drivers use this member, for example, in the ChangeDisplaySettings function. Printer drivers do not use this member.</summary>
		public int dmPelsWidth;
		/// <summary>Specifies the height, in pixels, of the visible device surface. Display drivers use this member, for example, in the ChangeDisplaySettings function. Printer drivers do not use this member.</summary>
		public int dmPelsHeight;
		/// <summary><para>Specifies the device's display mode. This member can be a combination of the following values.</para></summary>
		/// <example>
		/// <para>DM_GRAYSCALE: Specifies that the display is a noncolor device. If this flag is not set, color is assumed.</para>
		/// <para>DM_INTERLACED: Specifies that the display mode is interlaced. If the flag is not set, noninterlaced is assumed.</para>
		/// </example>
		public int dmDisplayFlags;
		/// <summary>
		/// <para>Specifies the frequency, in hertz (cycles per second), of the display device in a particular mode. This value is also known as the display device's vertical refresh rate. Display drivers use this member. It is used, for example, in the ChangeDisplaySettings function. Printer drivers do not use this member.</para>
		/// <para>When you call the EnumDisplaySettings function, the dmDisplayFrequency member may return with the value 0 or 1. These values represent the display hardware's default refresh rate. This default rate is typically set by switches on a display card or computer motherboard, or by a configuration program that does not use display functions such as ChangeDisplaySettings.</para>
		/// </summary>
		public int dmDisplayFrequency;
		/// <summary><para>Specifies how ICM is handled. For a non-ICM application, this member determines if ICM is enabled or disabled. For ICM applications, the system examines this member to determine how to handle ICM support. This member can be one of the following predefined values, or a driver-defined value greater than or equal to the value of DMICMMETHOD_USER.</para></summary>
		/// <example>
		/// <para>DMICMMETHOD_NONE: Specifies that ICM is disabled.</para>
		/// <para>DMICMMETHOD_SYSTEM: Specifies that ICM is handled by Windows.</para>
		/// <para>DMICMMETHOD_DRIVER: Specifies that ICM is handled by the device driver.</para>
		/// <para>DMICMMETHOD_DEVICE: Specifies that ICM is handled by the destination device.</para>
		/// </example>
		/// <remarks>The printer driver must provide a user interface for setting this member. Most printer drivers support only the DMICMMETHOD_SYSTEM or DMICMMETHOD_NONE value. Drivers for PostScript printers support all values.</remarks>
		public int dmICMMethod;
		/// <summary><para>Specifies which color matching method, or intent, should be used by default. This member is primarily for non-ICM applications. ICM applications can establish intents by using the ICM functions. This member can be one of the following predefined values, or a driver defined value greater than or equal to the value of DMICM_USER.</para></summary>
		/// <example>
		/// <para>DMICM_ABS_COLORIMETRIC: Color matching should optimize to match the exact color requested without white point mapping. This value is most appropriate for use with proofing.</para>
		/// <para>DMICM_COLORIMETRIC: Color matching should optimize to match the exact color requested. This value is most appropriate for use with business logos or other images when an exact color match is desired.</para>
		/// <para>DMICM_CONTRAST: Color matching should optimize for color contrast. This value is the most appropriate choice for scanned or photographic images when dithering is desired.</para>
		/// <para>DMICM_SATURATE: Color matching should optimize for color saturation. This value is the most appropriate choice for business graphs when dithering is not desired.</para>
		/// </example>
		public int dmICMIntent;
		/// <summary><para> Specifies the type of media being printed on. The member can be one of the following predefined values, or a driver-defined value greater than or equal to the value of DMMEDIA_USER.</para></summary>
		/// <example>
		/// <para>DMMEDIA_STANDARD: Plain paper.</para>
		/// <para>DMMEDIA_GLOSSY: Glossy paper.</para>
		/// <para>DMMEDIA_TRANSPARENCY: Transparent film.</para>
		/// </example>
		/// <remarks> To retrieve a list of the available media types for a printer, use the DeviceCapabilities function with the DC_MEDIATYPES flag.</remarks>
		public int dmMediaType;
		/// <summary><para> Specifies how dithering is to be done. The member can be one of the following predefined values, or a driver-defined value greater than or equal to the value of DMDITHER_USER.</para></summary>
		/// <example>
		/// <para>DMDITHER_NONE: No dithering.</para>
		/// <para>DMDITHER_COARSE: Dithering with a coarse brush.</para>
		/// <para>DMDITHER_FINE: Dithering with a fine brush.</para>
		/// <para>DMDITHER_LINEART: Line art dithering, a special dithering method that produces well defined borders between black, white, and gray scaling. It is not suitable for images that include continuous graduations in intensity and hue, such as scanned photographs.</para>
		/// <para>DMDITHER_GRAYSCALE: Device does gray scaling.</para>
		/// </example>
		public int dmDitherType;
		/// <summary>Not used; must be zero.</summary>
		public int dmReserved1;
		/// <summary>Not used; must be zero.</summary>
		public int dmReserved2;
		/// <summary>This member must be zero.</summary>
		public int dmPanningWidth;
		/// <summary>This member must be zero.</summary>
		public int dmPanningHeight;

		/// <summary>
		/// <para>You really should use this constructor!</para>
		/// <para>Alternatively you have to set dmSize manually before using it: this.dmSize = (short)Marshal.SizeOf(this); </para>
		/// </summary>
		/// <param name="x">Does not matter</param>
		public ScreenSettingsDevMode(bool x)
		{
			this.dmDeviceName = new String(new char[32]);
			this.dmSpecVersion = new short();
			this.dmDriverVersion = new short();
			this.dmDriverExtra = new short();
			this.dmFields = new int();
			this.dmPositionX = new int();
			this.dmPositionY = new int();
			this.dmDisplayOrientation = new int();
			this.dmDisplayFixedOutput = new int();
			this.dmColor = new short();
			this.dmDuplex = new short();
			this.dmYResolution = new short();
			this.dmTTOption = new short();
			this.dmCollate = new short();
			this.dmFormName = new String(new char[32]);
			this.dmLogPixels = new short();
			this.dmBitsPerPel = new short();
			this.dmPelsWidth = new int();
			this.dmPelsHeight = new int();
			this.dmDisplayFlags = new int();
			this.dmDisplayFrequency = new int();
			this.dmICMMethod = new int();
			this.dmICMIntent = new int();
			this.dmMediaType = new int();
			this.dmDitherType = new int();
			this.dmReserved1 = 0;
			this.dmReserved2 = 0;
			this.dmPanningWidth = 0;
			this.dmPanningHeight = 0;
			this.dmSize = new short();

			this.dmSize = (short)Marshal.SizeOf(this);
		}
	};

	public partial class Scraseen
	{
		// constants
		private const int DMDO_DEFAULT = 0;
		private const int DMDO_90 = 1;
		private const int DMDO_180 = 2;
		private const int DMDO_270 = 3;

		private const int DISP_CHANGE_SUCCESSFUL = 0;
		private const int DISP_CHANGE_RESTART = 1;
		private const int DISP_CHANGE_FAILED = -1;
	}
}
