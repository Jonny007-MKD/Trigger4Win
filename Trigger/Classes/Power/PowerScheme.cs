using System;
using Systemm = System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;

namespace Trigger.Classes.Power
{
	public class PowerScheme
	{
		#region DllImport
		/// <summary>
		/// Enumerates the specified elements in a power scheme. This function is normally called in a loop incrementing the Index parameter to retrieve subkeys until they've all been enumerated.
		/// </summary>
		/// <param name="RootPowerKey">This parameter is reserved for future use and must be set to NULL</param>
		/// <param name="SchemeGuid">The identifier of the power scheme. If this parameter is NULL, an enumeration of the power policies is returned.</param>
		/// <param name="SubGroupOfPowerSetting">The subgroup of power settings. If this parameter is NULL, an enumeration of settings under the PolicyGuid key is returned.</param>
		/// <param name="AccessFlags">
		/// <para>A set of flags that specifies what will be enumerated</para>
		/// <para>ACCESS_SCHEME: 16, ACCESS_SUBGROUP: 17, ACCESS_INDIVIDUAL_SETTING: 18</para>
		/// </param>
		/// <param name="Index">The zero-based index of the scheme, subgroup, or setting that is being enumerated.</param>
		/// <param name="Buffer">A pointer to a variable to receive the elements. If this parameter is NULL, the function retrieves the size of the buffer required.</param>
		/// <param name="BufferSize">A pointer to a variable that on input contains the size of the buffer pointed to by the Buffer parameter. If the Buffer parameter is NULL or if the BufferSize is not large enough, the function will return <see cref="ERROR_MORE_DATA"/> and the variable receives the required buffer size.</param>
		/// <returns>Returns ERROR_SUCCESS (zero) if the call was successful, and a nonzero value if the call failed. If the buffer size passed in the BufferSize parameter is too small, or if the Buffer parameter is NULL, ERROR_MORE_DATA will be returned and the DWORD pointed to by the BufferSize parameter will be filled in with the required buffer size.</returns>
		[DllImport("powrprof.dll")]
		internal static extern uint PowerEnumerate(IntPtr RootPowerKey, IntPtr SchemeGuid, ref Guid SubGroupOfPowerSetting, uint AccessFlags, uint Index, ref Guid Buffer, ref uint BufferSize);

		[DllImport("powrprof.dll")]
		internal static extern uint PowerGetActiveScheme(IntPtr UserRootPowerKey, ref IntPtr ActivePolicyGuid);

		[DllImport("powrprof.dll")]
		internal static extern uint PowerReadACValue(IntPtr RootPowerKey, IntPtr SchemeGuid, IntPtr SubGroupOfPowerSettingGuid, ref Guid PowerSettingGuid, ref int Type, ref IntPtr Buffer, ref uint BufferSize);

		[DllImport("powrprof.dll", CharSet = CharSet.Unicode)]
		internal static extern uint PowerReadFriendlyName(IntPtr RootPowerKey, IntPtr SchemeGuid, IntPtr SubGroupOfPowerSettingGuid, IntPtr PowerSettingGuid, StringBuilder Buffer, ref uint BufferSize);

		[DllImport("kernel32.dll")]
		internal static extern IntPtr LocalFree(IntPtr hMem);
		#endregion
		
		#region Properties
		#region Static
		#region Guids
		#region PowerBroadcast Message types
		internal static Guid GUID_BATTERY_PERCENTAGE_REMAINING = new Guid("A7AD8041-B45A-4CAE-87A3-EECBB468A9E1");
		internal static Guid GUID_MONITOR_POWER_ON = new Guid(0x02731015, 0x4510, 0x4526, 0x99, 0xE6, 0xE5, 0xA1, 0x7E, 0xBD, 0x1A, 0xEA);
		internal static Guid GUID_ACDC_POWER_SOURCE = new Guid(0x5D3E9A59, 0xE9D5, 0x4B00, 0xA6, 0xBD, 0xFF, 0x34, 0xFF, 0x51, 0x65, 0x48);
		internal static Guid GUID_POWERSCHEME_PERSONALITY = new Guid(0x245D8541, 0x3943, 0x4422, 0xB0, 0x25, 0x13, 0xA7, 0x84, 0xF6, 0x79, 0xB7);
		internal static Guid GUID_LIDCLOSE_ACTION = new Guid(0xBA3E0F4D, 0xB817, 0x4094, 0xA2, 0xD1, 0xD5, 0x63, 0x79, 0xE6, 0xA0, 0xF3);
		#endregion

		#region Default PowerSchemes
		internal static Guid GUID_MAX_POWER_SAVINGS = new Guid(0xA1841308, 0x3541, 0x4FAB, 0xBC, 0x81, 0xF7, 0x15, 0x56, 0xF2, 0x0B, 0x4A);
		internal static Guid GUID_MIN_POWER_SAVINGS = new Guid(0x8C5E7FDA, 0xE8BF, 0x4A96, 0x9A, 0x85, 0xA6, 0xE2, 0x3A, 0x8C, 0x63, 0x5C);	// No Power Savings - Almost no power savings measures are used.
		internal static Guid GUID_TYPICAL_POWER_SAVINGS = new Guid(0x381B4222, 0xF694, 0x41F0, 0x96, 0x85, 0xFF, 0x5B, 0xB2, 0x60, 0xDF, 0x2E);	// Typical Power Savings - Fairly aggressive power savings measures are used.
		#endregion

		#region PowerScheme Settings SubGroups
		internal static Guid NO_SUBGROUP_GUID = new Guid("fea3413e-7e05-4911-9a71-700331f1c294");
		internal static Guid GUID_DISK_SUBGROUP = new Guid("0012ee47-9041-4b5d-9b77-535fba8b1442");
		internal static Guid GUID_SYSTEM_BUTTON_SUBGROUP = new Guid("4f971e89-eebd-4455-a8de-9e59040e7347");
		internal static Guid GUID_PROCESSOR_SETTINGS_SUBGROUP = new Guid("54533251-82be-4824-96c1-47b60b740d00");
		internal static Guid GUID_VIDEO_SUBGROUP = new Guid("7516b95f-f776-4464-8c53-06167f40cc99");
		internal static Guid GUID_BATTERY_SUBGROUP = new Guid("e73a048d-bf27-4f12-9731-8b2076e8891f");
		internal static Guid GUID_SLEEP_SUBGROUP = new Guid("238C9FA8-0AAD-41ED-83F4-97BE242C8F20");
		internal static Guid GUID_PCIEXPRESS_SETTINGS_SUBGROUP = new Guid("501a4d13-42af-4429-9fd1-a8218c268e20");
		#endregion

		#region Guid for each individual setting
		internal static Guid GUID_SET_AHCI_POWERMGMT_HDIPM = new Guid("0b2d69d7-a2a1-449c-9680-f91c70521c60"); // AHCI Link Power Management - HIPM/DIPM
		internal static Guid GUID_SET_DRIVE_TIMEOUT = new Guid("6738e2c4-e8a5-4a42-b16a-e040e769756e"); // Festplatte ausschalten nach
		internal static Guid GUID_SET_DRIVE_IGNORE_TIMEOUT = new Guid("80e3c60e-bb94-4ad8-bbe0-0d3195efc663"); // Zeit für das Ignorieren von Festplattenaktivitäten
		internal static Guid GUID_SET_AHCI_POWERMGMT_ADAPTIVE = new Guid("dab60367-53fe-4fbc-825e-521d069d2456"); // AHCI Link Power Management - Adaptive
		internal static Guid GUID_SET_JAVASCRIPT_TIMERFREQ = new Guid("4c793e7d-a264-42e1-87d3-7a0d2f523ccd"); // JavaScript-Timerfrequenz
		internal static Guid GUID_SET_DIASHOW = new Guid("309dce9b-bef4-4119-9921-a851fb12f0f4"); // Diashow
		internal static Guid GUID_SET_STANDBY = new Guid("12bbebe6-58d6-4636-95bb-3217ef867c1a"); // Energiesparmodus
		internal static Guid GUID_SET_ALLOW_HYBRID_STANDBY = new Guid("94ac6d29-73ce-41a6-809f-6363ba21b47e"); // Hybriden Standbymodus zulassen
		internal static Guid GUID_SET_HIBERNATE_TIMEOUT = new Guid("9d7815a6-7ee4-497e-8888-515a05f02364"); // Ruhezustand nach
		/*internal static Guid GUID_SET_ _ = new Guid("25dfa149-5dd1-4736-b5ab-e8a37b5b8187"); // Richtlinie für das Zulassen des Abwesenheitsmodus
		internal static Guid GUID_SET_ _ = new Guid("29f6c1db-86da-48c5-9fdb-f2b67b1f44da"); // Deaktivierung nach
		internal static Guid GUID_SET_ _ = new Guid("7bc4a2f9-d8fc-4469-b07b-33eb785aaca0"); // Leerlaufzeit nach unbeaufsichtigter Reaktivierung
		internal static Guid GUID_SET_ _ = new Guid("a4b195f5-8225-47d8-8012-9d41369786e2"); // Richtlinie für erforderliches System zulassen
		internal static Guid GUID_SET_ _ = new Guid("abfc2519-3608-4c2a-94ea-171b0ed546ab"); // Energiesparzustände erlauben
		internal static Guid GUID_SET_ _ = new Guid("bd3b718a-0680-4d9d-8ab2-e1d2b4ac806d"); // Zeitgeber zur Aktivierung zulassen*/
		internal static Guid GUID_SET_ALLOW_STANDBY_WITH_OPENED_REMOTE_FILES = new Guid("d4c1d4c8-d5cc-43d3-b83e-fc51215cb04d"); // Standbymodus mit remote geöffneten Dateien zulassen
		internal static Guid GUID_SET_SELECTIVE_USB_ENERGY_SAVING = new Guid("48e6b7a6-50f5-4782-a5d4-53bb8f07e226"); // Einstellung für selektives USB-Energiesparen
		internal static Guid GUID_SET_SHUT = new Guid("5ca83367-6e45-459f-a27b-476b1d01c936"); // Zuklappen
		internal static Guid GUID_SET_DEFAULT_SHUTDOWN_ACTION = new Guid("7648efa3-dd9c-4e3e-b566-50f929386280"); // Standardaktion für Beenden
		internal static Guid GUID_SET_FORCE_SHUTDOWN = new Guid("833a6b62-dfa4-46d1-82f8-e09e34d029d6"); // Erzwungenes Herunterfahren für Tastenaktionen und Zuklappen aktivieren
		internal static Guid GUID_SET_ENERGYSAVE_KEY_ACTION = new Guid("96996bc0-ad50-47ec-923b-6f41874dd9eb"); // Energiespartastenaktion
		internal static Guid GUID_SET_SHOW_POWER_SWITCH_IN_STARTMENU = new Guid("a7066653-8d6c-40a8-910e-a1f54b84c7e5"); // Netzschalter im Startmenü
		/*internal static Guid GUID_SET_ _ = new Guid("ee12f906-d277-404b-b6da-e5fa1a576df5"); // Verbindungszustand-Energieverwaltung
		internal static Guid GUID_SET_THRESHOLD_RAISE_PROCESSOR = new Guid("06cadf0e-64ed-448a-8927-ce7bf90eb35d"); // Schwellenwert zum Erhöhen der Prozessorleistung
		internal static Guid GUID_SET_PROCESSOR_PARK_KERN = new Guid("0cc5b647-c1df-4637-891a-dec35c318583"); // Prozessorleistung: Parken von Kernen - Kerne minimal
		internal static Guid GUID_SET_ _ = new Guid("1299023c-bc28-4f0a-81ec-d3295a8d815d"); // Prozessorleistung: Parken von Kernen - Faktor zum Reduzieren des Verlaufs für übermäßige Nutzung
		internal static Guid GUID_SET_PROCESSOR_THRESHOLD_REDUCE = new Guid("12a0ab44-fe28-4fa9-b3bd-4b64f44960a6"); // Schwellenwert zum Reduzieren der Prozessorleistung
		internal static Guid GUID_SET_RAISE_PROCESSOR_TIMEOUT = new Guid("2ddd5a84-5a71-437e-912a-db0b8c788732"); // Prozessorleistung: Parken von Kernen - Zeit bis zum Erhöhen
		internal static Guid GUID_SET_ALLOW_RESTRICT = new Guid("3b04d4fd-1cc7-4f23-ab1c-d1337819c4bb"); // Drosselungszustände zulassen
		internal static Guid GUID_SET_RESTRICT_RULE = new Guid("40fbefc7-2e9d-4d25-a185-0cfd8574bac6"); // Prozessorleistung - Reduzierungsrichtlinie
		internal static Guid GUID_SET_ _ = new Guid("447235c7-6a8d-4cc0-8e24-9eaf70b96e2b"); // Prozessorleistung: Parken von Kernen - Leistungszustand beim Parken
		internal static Guid GUID_SET_ = new Guid("45bcc044-d885-43e2-8605-ee0ec6e96b59"); // Leistungssteigerungsrichtlinie für Prozessoren
		internal static Guid GUID_SET_ = new Guid("465e1f50-b610-473a-ab58-00d1077dc418"); // Prozessorleistung - Erhöhungsrichtlinie
		internal static Guid GUID_SET_ = new Guid("4b92d758-5a24-4851-a470-815d78aee119"); // Prozessorleerlauf - Schwellenwert für Herabstufung
		internal static Guid GUID_SET_ = new Guid("4d2b0152-7d5c-498b-88e2-34345392a2c5"); // Prozessorleistung - Intervall für Überprüfung der Zeit
		internal static Guid GUID_SET_ = new Guid("5b33697b-e89d-4d38-aa46-9e7dfb7cd2f9"); // Prozessorleistung: Parken von Kernen - Schwellenwert für Affinitätsverlauf
		internal static Guid GUID_SET_ = new Guid("5d76a2ca-e8c0-402f-a133-2158492d58ad"); // Prozessorleerlauf deaktivieren
		internal static Guid GUID_SET_ = new Guid("68dd2f27-a4ce-4e11-8487-3794e4135dfa"); // Prozessorleistung: Parken von Kernen - Reduzierungsschwellenwert
		internal static Guid GUID_SET_ = new Guid("6c2993b0-8f48-481f-bcc6-00dd2742aa06"); // Prozessorleerlauf - Skalierung des Schwellenwerts
		internal static Guid GUID_SET_ = new Guid("71021b41-c749-4d21-be74-a00f335d582b"); // Prozessorleistung: Parken von Kernen - Reduzierungsrichtlinie
		internal static Guid GUID_SET_ = new Guid("7b224883-b3cc-4d79-819f-8374152cbe7c"); // Prozessorleerlauf - Schwellenwert für Heraufstufung
		internal static Guid GUID_SET_ = new Guid("7d24baa7-0b84-480f-840c-1b0743c00f5f"); // Prozessorleistungs-Verlaufsanzahl
		internal static Guid GUID_SET_ = new Guid("8809c2d8-b155-42d4-bcda-0d345651b1db"); // Prozessorleistung: Parken von Kernen - Gewichtung für übermäßige Nutzung
		internal static Guid GUID_SET_ = new Guid("893dee8e-2bef-41e0-89c6-b55d0929964c"); // Minimaler Leistungszustand des Prozessors
		internal static Guid GUID_SET_ = new Guid("8f7b45e3-c393-480a-878c-f67ac3d07082"); // Prozessorleistung: Parken von Kernen - Faktor zum Reduzieren des Affinitätsverlaufs
		internal static Guid GUID_SET_ = new Guid("943c8cb6-6f93-4227-ad87-e9a3feec08d1"); // Prozessorleistung: Parken von Kernen - Schwellenwert für übermäßige Kernnutzung
		internal static Guid GUID_SET_ = new Guid("94d3a615-a899-4ac5-ae2b-e4d8f634367f"); // Systemkühlungsrichtlinie
		internal static Guid GUID_SET_ = new Guid("984cf492-3bed-4488-a8f9-4286c97bf5aa"); // Prozessorleistung - Zeit bis zum Erhöhen
		internal static Guid GUID_SET_ = new Guid("9ac18e92-aa3c-4e27-b307-01ae37307129"); // Prozessorleistung: Parken von Kernen - Schwellenwert für den Verlauf für übermäßige Nutzung
		internal static Guid GUID_SET_ = new Guid("a55612aa-f624-42c6-a443-7397d064c04f"); // Prozessorleistung: Parken von Kernen - Außerkraftsetzung von Kernen
		internal static Guid GUID_SET_ = new Guid("bc5038f7-23e0-4960-96da-33abaf5935ec"); // Maximaler Leistungszustand des Prozessors
		internal static Guid GUID_SET_ = new Guid("c4581c31-89ab-4597-8e2b-9c9cab440e6b"); // Prozessorleerlauf - Überprüfung der Zeit
		internal static Guid GUID_SET_ = new Guid("c7be0679-2817-4d69-9d02-519a537ed0c6"); // Prozessorleistung: Parken von Kernen - Erhöhungsrichtlinie
		internal static Guid GUID_SET_ = new Guid("d8edeb9b-95cf-4f95-a73c-b061973693c8"); // Prozessorleistung - Zeit bis zum Reduzieren
		internal static Guid GUID_SET_ = new Guid("df142941-20f3-4edf-9a4a-9c83d3d717d1"); // Prozessorleistung: Parken von Kernen - Erhöhungsschwellenwert
		internal static Guid GUID_SET_ = new Guid("dfd10d17-d5eb-45dd-877a-9a34ddd15c82"); // Prozessorleistung: Parken von Kernen - Zeit bis zum Reduzieren
		internal static Guid GUID_SET_ = new Guid("e70867f1-fa2f-4f4e-aea1-4d8a0ba23b20"); // Prozessorleistung: Parken von Kernen - Affinitätsgewichtung
		internal static Guid GUID_SET_ = new Guid("ea062031-0e34-4ff1-9b6d-eb1059334028"); // Prozessorleistung: Parken von Kernen - Kerne maximal*/
		internal static Guid GUID_SET_DIM_SCREEN_TIMEOUT = new Guid("17aaa29b-8b43-4b94-aafe-35f64daaf1ee"); // Bildschirm verdunkeln nach
		internal static Guid GUID_SET_SWITCHOFF_SCREEN_TIMEOUT = new Guid("3c0bc021-c8a8-4e07-a973-6b14cbcb2b7e"); // Bildschirm ausschalten nach
		internal static Guid GUID_SET_USER_ANNOYANCE_TIMEOUT = new Guid("82dbcf2d-cd67-40c5-bfdc-9f1a5ccd4663"); // Zeitlimit für Benutzerstörung
		internal static Guid GUID_SET_ADAPTIVE = new Guid("90959d22-d6a1-49b9-af93-bce885ad335b"); // Adaptive Bildschirmabschaltung
		internal static Guid GUID_SET_ALLOW_SCREEN_RULE = new Guid("a9ceb8da-cd46-44fb-a98b-02af69de4623"); // Richtlinie für erforderliche Anzeige zulassen
		internal static Guid GUID_SET_SCREEN_BRIGHTNESS = new Guid("aded5e82-b909-4619-9949-f5d71dac0bcb"); // Bildschirmhelligkeit
		internal static Guid GUID_SET_RAISE_ADAPTIVE_TIME_INTERVAL = new Guid("eed904df-b142-4183-b10b-5a1197a37864"); // Adaptives Zeitlimit erhöhen um
		internal static Guid GUID_SET_SCREEN_BRIGHTNESS_DIM = new Guid("f1fbfde2-a960-4165-9f88-50667911ce96"); // Bildschirmhelligkeit beim Verdunkeln
		internal static Guid GUID_SET_ADAPTIVE_BRIGHTNESS = new Guid("fbd9aa66-9553-4097-ba44-ed6e9d65eab8"); // Adaptive Helligkeit aktivieren
		internal static Guid GUID_SET_PLAYING_MEDIA = new Guid("03680956-93bc-4294-bba6-4e0f09bb717f"); // Bei der Freigabe von Medien
		internal static Guid GUID_SET_PLAYING_VIDEO = new Guid("34c7b99f-9a6d-4b3c-8dc7-b6693b78cef4"); // Bei der Videowiedergabe
		internal static Guid GUID_SET_ACTION_CRITICAL_BATTERY = new Guid("637ea02f-bbcb-4015-8e2c-a1c7b9c0b546"); // Aktion bei kritischer Akkukapazität
		internal static Guid GUID_SET_LOW_BATTERY = new Guid("8183ba9a-e910-48da-8769-14ae6dc1170a"); // Niedrige Akkukapazität
		internal static Guid GUID_SET_CRITICAL_BATTERY = new Guid("9a66d8d7-4ff7-4ef9-b5a2-5a326ca2a469"); // Kritische Akkukapazität
		internal static Guid GUID_SET_NOTIFICATION_LOW_BATTERY = new Guid("bcded951-187b-4d05-bccc-f7e51960c258"); // Benachrichtigung bei niedriger Akkukapazität
		internal static Guid GUID_SET_ACTION_LOW_BATTERY = new Guid("d8742dcb-3e6a-4b3c-b3fe-374623cdcf06"); // Aktion bei niedriger Akkukapazität
		internal static Guid GUID_SET_BATTERY_LEVEL_ENERGY_RESERVE = new Guid("f3c5027d-cd16-4930-aa6b-90db844a8f00"); // Akkustand für Reservestrom
		#endregion
		#endregion

		internal const uint ERROR_MORE_DATA = 234;

		internal const uint ACCESS_SCHEME = 16;
		internal const uint ACCESS_SUBGROUP = 17;
		internal const uint ACCESS_INDIVIDUAL_SETTING = 18;

		public static PowerScheme Active
		{
			get
			{
				IntPtr activeGuidPtr = IntPtr.Zero;
				uint index = PowerGetActiveScheme(IntPtr.Zero, ref activeGuidPtr);
				if (index != 0)
					throw new Win32Exception();

				return new PowerScheme(activeGuidPtr);
			}
		}
		public static List<PowerScheme> AllPowerSchemes
		{
			get
			{
				return null;
			}
		}
		#endregion

		#region Instance
		#region Guid
		/// <summary><para>The <see cref="Guid"/> of this <see cref="PowerScheme"/></para></summary>
		internal Guid guid;
		/// <summary><para>The <see cref="Guid"/> of this <see cref="PowerScheme"/></para></summary>
		public Guid Guid
		{
			get { return guid; }
			private set { guid = value; }
		}
		#endregion
		#region DriveLetter
		/// <summary><para>The <see cref="PowerScheme"/>'s name</para></summary>
		internal string name;
		/// <summary><para>The <see cref="PowerScheme"/>'s name</para></summary>
		public string Name
		{
			get { return name; }
			private set { name = value; }
		}
		#endregion
		#region Other Settings
		/// <summary><para>Contains all the additional settings of this <see cref="PowerScheme"/></para></summary>
		internal Dictionary<Guid, ulong> settings = new Dictionary<Guid, ulong>(80);

		public bool Adaptive
		{
			get
			{
				return Convert.ToBoolean(settings[GUID_SET_ADAPTIVE]);
			}
		}
		public bool AdaptiveBrightness
		{
			get
			{
				return Convert.ToBoolean(settings[GUID_SET_ADAPTIVE_BRIGHTNESS]);
			}
		}
		public ulong DimScreenTimeout
		{
			get
			{
				return Convert.ToUInt32(settings[GUID_SET_DIM_SCREEN_TIMEOUT]);
			}
		}
		public ulong DiskDriveTimeout
		{
			get
			{
				return Convert.ToUInt32(settings[GUID_SET_DRIVE_TIMEOUT]);
			}
		}
		public byte ScreenBrightness
		{
			get
			{
				return Convert.ToByte(settings[GUID_SET_SCREEN_BRIGHTNESS]);
			}
		}
		public byte ScreenBrightnessDim
		{
			get
			{
				return Convert.ToByte(settings[GUID_SET_SCREEN_BRIGHTNESS_DIM]);
			}
		}
		public bool SelectiveUsbEnergySaving
		{
			get
			{
				return Convert.ToBoolean(settings[GUID_SET_SELECTIVE_USB_ENERGY_SAVING]);
			}
		}
		public ulong SwitchoffScreenTimeout
		{
			get
			{
				return Convert.ToUInt32(settings[GUID_SET_SWITCHOFF_SCREEN_TIMEOUT]);
			}
		}
		public ulong UserAnnoyanceTimeout
		{
			get
			{
				return Convert.ToUInt32(settings[GUID_SET_USER_ANNOYANCE_TIMEOUT]);
			}
		}
		#endregion
		#endregion
		#endregion

		#region Constructor
		/// <summary>
		/// <para>Creates a new instance of <see cref="PowerScheme"/> with the specified <paramref name="index"/></para>
		/// </summary>
		/// <param name="Guid"></param>
		public PowerScheme(IntPtr GuidPtr)
		{
			if (GuidPtr == IntPtr.Zero)
				throw new ArgumentException("GuidPtr cannot be Zero!", "GuidPtr");

			this.Guid = (Guid)Marshal.PtrToStructure(GuidPtr, typeof(Guid));

			this.init(GuidPtr);
		}
		/// <summary>
		/// <para>Creates a new instance of <see cref="PowerScheme"/> with the specified <paramref name="Guid"/></para>
		/// </summary>
		/// <param name="Guid"></param>
		public PowerScheme(Guid Guid)
		{
			this.Guid = Guid;

			IntPtr GuidPtr = Marshal.AllocHGlobal(Marshal.SizeOf(Guid)); ;
			Marshal.StructureToPtr(Guid, GuidPtr, false);
			this.init(GuidPtr);
		}
		#endregion

		#region Methods
		/// <summary>
		/// <para>Initiates the properties using the specified <paramref name="GuidPtr"/></para>
		/// </summary>
		/// <param name="GuidPtr"></param>
		internal void init(IntPtr GuidPtr)
		{
			this.Name = getName(GuidPtr);

			this.getSettings(GuidPtr);

			// free the space at GuidPtr
			IntPtr res = LocalFree(GuidPtr);
			if (res != IntPtr.Zero)
				throw new Win32Exception();
		}

		/// <summary>
		/// <para>Gets the name of the power plan</para>
		/// </summary>
		/// <param name="GuidPtr"></param>
		/// <returns></returns>
		internal string getName(IntPtr GuidPtr)
		{
			uint buffSize = 0;
			StringBuilder buffer = new StringBuilder();
			Guid subGroupGuid = Guid.Empty;
			Guid powerSettingGuid = Guid.Empty;
			uint index = PowerReadFriendlyName(IntPtr.Zero, GuidPtr, IntPtr.Zero, IntPtr.Zero, buffer, ref buffSize);

			if (index == ERROR_MORE_DATA)
			{
				buffer.Capacity = (int)buffSize;
				index = PowerReadFriendlyName(IntPtr.Zero, GuidPtr, IntPtr.Zero, IntPtr.Zero, buffer, ref buffSize);
			}

			if (index != 0)
				throw new Win32Exception();
			return buffer.ToString();
		}

		/// <summary>
		/// <para>Enumerate over the settings</para>
		/// </summary>
		/// <param name="GuidPtr"></param>
		internal void getSettings(IntPtr GuidPtr)
		{
			//Get the Power Settings
			Guid SettingGuid = Guid.Empty;
			Guid SubGroupGuid = Guid.Empty;
			uint result;
			uint indexI = 0;
			uint BufferSize = Convert.ToUInt32(Marshal.SizeOf(typeof(Guid)));

			while (PowerEnumerate(IntPtr.Zero, GuidPtr, ref SubGroupGuid, ACCESS_SUBGROUP, indexI, ref SubGroupGuid, ref BufferSize) == 0)
			{
				uint indexJ = 0;

				while (PowerEnumerate(IntPtr.Zero, GuidPtr, ref SubGroupGuid, ACCESS_INDIVIDUAL_SETTING, indexJ, ref SettingGuid, ref BufferSize) == 0)
				{
					uint size = 4;
					IntPtr value = IntPtr.Zero;
					int type = 0;
					result = PowerReadACValue(IntPtr.Zero, GuidPtr, IntPtr.Zero, ref SettingGuid, ref type, ref value, ref size);
					// copy IntPtrs because they will be deleted (i think)
					IntPtr pSubGroup = Marshal.AllocHGlobal(Marshal.SizeOf(SubGroupGuid));
					Marshal.StructureToPtr(SubGroupGuid, pSubGroup, false);
					IntPtr pSetting = Marshal.AllocHGlobal(Marshal.SizeOf(SettingGuid));
					Marshal.StructureToPtr(SettingGuid, pSetting, false);

					uint nameSize = 200;
					StringBuilder name = new StringBuilder((int)nameSize);
					result = PowerReadFriendlyName(IntPtr.Zero, GuidPtr, pSubGroup, pSetting, name, ref nameSize);

					this.settings.Add(SettingGuid, (ulong)value);

					indexJ++;
				}

				SubGroupGuid = Guid.Empty;
				indexI++;
			}
		}
		#endregion

		#region Operators
		/// <summary>
		/// <para>Compars two <see cref="PowerScheme"/>s and returns true if they have the same Guid</para>
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			PowerScheme that = obj as PowerScheme;
			if (that == null)
				return false;

			return this.Guid == that.Guid;
		}

		public static bool operator == (PowerScheme thit, PowerScheme that)
		{
			return thit.Guid == that.Guid;
		}
		public static bool operator !=(PowerScheme thit, PowerScheme that)
		{
			return thit.Guid != that.Guid;
		}

		public override int GetHashCode()
		{
			return this.Guid.GetHashCode();
		}
		#endregion
	}
}
