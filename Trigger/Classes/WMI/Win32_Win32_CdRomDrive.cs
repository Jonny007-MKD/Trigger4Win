using System;
using System.ComponentModel;
using System.Management;
using System.Collections;
using System.Globalization;

#pragma warning disable

namespace Trigger.Classes.WMI
{
    // Die ShouldSerialize<PropertyName>-Funktionen werden vom VS-Eigenschaftenbrowser verwendet, um zu überprüfen, ob eine bestimmte Eigenschaft serialisiert werden muss. Diese Funktionen werden für alle ValueType-Eigenschaften hinzugefügt (Eigenschaften des Typs Int32, BOOL usw. , die nicht auf NULL festgelegt werden können). Die Funktionen verwenden die Is<PropertyName>Null-Funktion. Die Funktionen werden auch in der TypeConverter-Implementierung für die Eigenschaften verwendet, um die jeweilige Eigenschaft in Bezug auf den NULL-Wert zu überprüfen, damit für einen Drag & Drop-Vorgang in Visual Studio ein leerer Wert im Eigenschaftenbrowser angezeigt werden kann.
    // Mit Funktionen der Art 'Is<PropertyName>Null()' wird überprüft, ob eine Eigenschaft NULL ist.
    // Die Reset<PropertyName>-Funktionen werden für Read/Write-Eigenschaften hinzugefügt, die NULL-Werte zulassen. Diese Funktionen werden vom VS-Designer im Eigenschaftenbrowser verwendet, um eine Eigenschaft auf NULL festzulegen.
    // Für jede Eigenschaft, die zur Klasse für WMI hinzugefügt wurde, sind Attribute festgelegt, um das Verhalten im Visual Studio-Designer sowie die zu verwendende TypeConverter-Klasse zu definieren.
    // Die Funktionen 'ToDateTime' und 'ToDmtfDateTime' zum Konvertieren von Datum und Uhrzeit werden zu der Klasse hinzugefügt, damit DMTF-Zeitangaben in 'DateTime' konvertiert werden können (und umgekehrt).
    // Eine für die WMI-Klasse generierte Klasse mit früher Bindung.Win32_CdRomDrive
    public class CdRomDrive : Component {
        
        // Private Eigenschaft, die den WMI-Namespace enthält, in dem sich die Klasse befindet.
        private static string CreatedWmiNamespace = "root\\CimV2";
        
        // Private Eigenschaft, die den Namen der WMI-Klasse enthält, die diese Klasse erstellt hat.
        private static string CreatedClassName = "Win32_CdRomDrive";
        
        // Private Membervariable, die 'ManagementScope' enthält, das von den verschiedenen Methoden verwendet wird.
        private static ManagementScope statMgmtScope = null;
        
        private ManagementSystemProperties PrivateSystemProperties;
        
        // Zugrunde liegendes lateBound-WMI-Objekt.
        private ManagementObject PrivateLateBoundObject;
        
        // Membervariable, in der das automatic commit-Verhalten für die Klasse gespeichert wird.
        private bool AutoCommitProp;
        
        // Private Variable, die die eingebettete Eigenschaft enthält, die die Instanz darstellt.
        private ManagementBaseObject embeddedObj;
        
        // Das aktuelle WMI-Objekt.
        private ManagementBaseObject curObj;
        
        // Flag zum Anzeigen, ob die Instanz ein eingebettetes Objekt ist.
        private bool isEmbedded;
        
        // Nachstehend sind unterschiedliche Konstruktorüberladungen aufgeführt, um eine Instanz der Klasse mit einem WMI-Objekt zu initialisieren.
        public CdRomDrive() {
            this.InitializeObject(null, null, null);
        }
        
        public CdRomDrive(string keyDeviceID) {
            this.InitializeObject(null, new ManagementPath(CdRomDrive.ConstructPath(keyDeviceID)), null);
        }
        
        public CdRomDrive(ManagementScope mgmtScope, string keyDeviceID) {
            this.InitializeObject(((ManagementScope)(mgmtScope)), new ManagementPath(CdRomDrive.ConstructPath(keyDeviceID)), null);
        }
        
        public CdRomDrive(ManagementPath path, ObjectGetOptions getOptions) {
            this.InitializeObject(null, path, getOptions);
        }
        
        public CdRomDrive(ManagementScope mgmtScope, ManagementPath path) {
            this.InitializeObject(mgmtScope, path, null);
        }
        
        public CdRomDrive(ManagementPath path) {
            this.InitializeObject(null, path, null);
        }
        
        public CdRomDrive(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) {
            this.InitializeObject(mgmtScope, path, getOptions);
        }
        
        public CdRomDrive(ManagementObject theObject) {
            Initialize();
            if ((CheckIfProperClass(theObject) == true)) {
                PrivateLateBoundObject = theObject;
                PrivateSystemProperties = new ManagementSystemProperties(PrivateLateBoundObject);
                curObj = PrivateLateBoundObject;
            }
            else {
                throw new ArgumentException("Der Klassenname stimmt nicht überein.");
            }
        }
        
        public CdRomDrive(ManagementBaseObject theObject) {
            Initialize();
            if ((CheckIfProperClass(theObject) == true)) {
                embeddedObj = theObject;
                PrivateSystemProperties = new ManagementSystemProperties(theObject);
                curObj = embeddedObj;
                isEmbedded = true;
            }
            else {
                throw new ArgumentException("Der Klassenname stimmt nicht überein.");
            }
        }
        
        // Die Eigenschaft gibt den Namespace der WMI-Klasse zurück.
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string OriginatingNamespace {
            get {
                return "root\\CimV2";
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ManagementClassName {
            get {
                string strRet = CreatedClassName;
                if ((curObj != null)) {
                    if ((curObj.ClassPath != null)) {
                        strRet = ((string)(curObj["__CLASS"]));
                        if (((strRet == null) 
                                    || (strRet == string.Empty))) {
                            strRet = CreatedClassName;
                        }
                    }
                }
                return strRet;
            }
        }
        
        // Eigenschaft, die auf ein eingebettetes Objekt zeigt, um die Systemeigenschaften des WMI-Objekts abzurufen.
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ManagementSystemProperties SystemProperties {
            get {
                return PrivateSystemProperties;
            }
        }
        
        // Die Eigenschaft, die das zugrunde liegende lateBound-Objekt zurückgibt.
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ManagementBaseObject LateBoundObject {
            get {
                return curObj;
            }
        }
        
        // 'ManagementScope' des Objekts.
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ManagementScope Scope {
            get {
                if ((isEmbedded == false)) {
                    return PrivateLateBoundObject.Scope;
                }
                else {
                    return null;
                }
            }
            set {
                if ((isEmbedded == false)) {
                    PrivateLateBoundObject.Scope = value;
                }
            }
        }
        
        // Die Eigenschaft zum Anzeigen des Commitverhaltens des WMI-Objekts. Wenn die Eigenschaft den Wert 'true' hat, wird das WMI-Objekt automatisch nach jeder Eigenschaftsänderung gespeichert (d. h., nach der Änderung einer Eigenschaft wird 'Put()' aufgerufen).
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AutoCommit {
            get {
                return AutoCommitProp;
            }
            set {
                AutoCommitProp = value;
            }
        }
        
        // 'ManagementPath' des zugrunde liegenden WMI-Objekts.
        [Browsable(true)]
        public ManagementPath Path {
            get {
                if ((isEmbedded == false)) {
                    return PrivateLateBoundObject.Path;
                }
                else {
                    return null;
                }
            }
            set {
                if ((isEmbedded == false)) {
                    if ((CheckIfProperClass(null, value, null) != true)) {
                        throw new ArgumentException("Der Klassenname stimmt nicht überein.");
                    }
                    PrivateLateBoundObject.Path = value;
                }
            }
        }
        
        // Öffentliche statische Bereichseigenschaft, die von den verschiedenen Methoden verwendet wird.
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static ManagementScope StaticScope {
            get {
                return statMgmtScope;
            }
            set {
                statMgmtScope = value;
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsAvailabilityNull {
            get {
                if ((curObj["Availability"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Die Verfügbarkeit und der Status des Geräts. Die Eigenschaft ""Availability"" gibt z. B. an, dass das Gerät aktiv ist und sich nicht im Energiesparmodus (Wert=3) befindet. Die Energiesparmodi sind wie folgt definiert: Der Wert 13 (""Energiesparmodus - Unbekannt"") gibt an, dass sich das Gerät im Energiesparmodus befindet, aber der genaue Status unbekannt ist; 14 (""Niedriger Energiestatus"") gibt an, dass sich das Gerät im Energiesparmodus befindet, aber noch funktioniert und die Leistung verringert ist; 15 (""Standby"") gibt an, dass das Gerät nicht funktioniert, aber schnell reaktiviert werden kann; 17 (""Warnung"") gibt an, dass sich das Gerät sowohl in einem Warnungs- als auch in einem Energiesparmodus befindet.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public AvailabilityValues Availability {
            get {
                if ((curObj["Availability"] == null)) {
                    return ((AvailabilityValues)(Convert.ToInt32(0)));
                }
                return ((AvailabilityValues)(Convert.ToInt32(curObj["Availability"])));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Funktionen des Medienzugriffsgeräts. Z. B. könnte das Gerät ""Zufälliger Zugriff"", Wechselmedien und ""Automatische Reinigung"" unterstützen. In diesem Fall würden die Werte 3, 7 und 9 in das Array geschrieben. 
Für einige der aufgezählten Werte sind Erläuterungen erforderlich: 1) Wert 11: Unterstützt zweiseitige Medien und unterscheidet zwischen einem Gerät, das auf beide Seiten des zweiseitigen Mediums zugreifen kann, und einem Gerät, das nur auf eine Seite zugreifen kann, so dass das Medium umgedreht werden muss. 2) Wert 12: Bereitstellungsaufhebung zum Auswerfen ist nicht erforderlich. Dies bedeutet, dass Medien nicht explizit aus dem Gerät ausgeworfen werden müssen, bevor ein ""PickerElement"" darauf zugreifen kann.")]
        public CapabilitiesValues[] Capabilities {
            get {
                Array arrEnumVals = ((Array)(curObj["Capabilities"]));
                CapabilitiesValues[] enumToRet = new CapabilitiesValues[arrEnumVals.Length];
                int counter = 0;
                for (counter = 0; (counter < arrEnumVals.Length); counter = (counter + 1)) {
                    enumToRet[counter] = ((CapabilitiesValues)(Convert.ToInt32(arrEnumVals.GetValue(counter))));
                }
                return enumToRet;
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Formfreie Zeichenfolgen mit genaueren Erklärungen zu den im Array \"Capabilities\" " +
            "angezeigten Funktionen des Geräts für den Medienzugriff. Jeder Eintrag in diesem" +
            " Array bezieht sich auf den Eintrag im Array \"Capabilities\" unter dem gleichen I" +
            "ndex.")]
        public string[] CapabilityDescriptions {
            get {
                return ((string[])(curObj["CapabilityDescriptions"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"Caption\" gibt eine kurze Textbeschreibung (eine Zeile) des Objek" +
            "ts an.")]
        public string Caption {
            get {
                return ((string)(curObj["Caption"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Eine formfreie Zeichenfolge, die den Algorithmus oder das Programm angibt, der bzw. das die Komprimierung unterstützt. Wenn es nicht möglich ist, das Komprimierungsschema zu beschreiben, wird Folgendes angegeben: ""Unbekannt"", wenn nicht bekannt ist, ob das Gerät die Komprimierung unterstützt. ""Komprimiert"", wenn das Gerät die Komprimierung unterstützt, aber das Komprimierungsschema unbekannt ist. ""Nicht komprimiert"", wenn das Gerät die Komprimierung nicht unterstützt.")]
        public string CompressionMethod {
            get {
                return ((string)(curObj["CompressionMethod"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsConfigManagerErrorCodeNull {
            get {
                if ((curObj["ConfigManagerErrorCode"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Gibt den Fehlercode des Win32-Konfigurations-Managers an. Die folgenden Werte kön" +
            "nen zurückgegeben werden: \n0\tDieses Gerät funktioniert ordnungsgemäß. \n1\tDas Ger" +
            "ät funktioniert einwandfrei. \n2\tDer Treiber für dieses Gerät konnte nicht gelade" +
            "n werden. \n3\tDer Treiber für dieses Gerät ist entweder beschädigt, oder es stehe" +
            "n nicht genügend Arbeitsspeicher oder andere Ressourcen zur Verfügung. \n4\tDieses" +
            " Gerät funktioniert nicht ordnungsgemäß. Eventuell ist einer der Treiber oder di" +
            "e Registrierung beschädigt. \n5\tDer Treiber für dieses Gerät erfordert eine Resso" +
            "urce, die Windows nicht verwalten kann. \n6\tDie Startkonfiguration dieses Geräts " +
            "verursacht Konflikte mit anderen Geräten. \n7\tFilterung nicht möglich. \n8\tDas Tre" +
            "iberladegerät für dieses Gerät ist nicht vorhanden. \n9\tDieses Gerät funktioniert" +
            " nicht ordnungsgemäß, da die steuernde Firmware die Ressourcen für das Gerät fal" +
            "sch angibt. \n10\tDas Gerät kann nicht gestartet werden. \n11\tDas Gerät ist fehlges" +
            "chlagen. \n12\tDieses Gerät kann keine ausreichenden freien Ressourcen finden, die" +
            " verwendet werden können. \n13\tDie Ressourcen des Geräts können nicht verifiziert" +
            " werden. \n14\tSie müssen den Computer neu starten, damit dieses Gerät ordnungsgem" +
            "äß funktioniert. \n15\tDas Gerät funktioniert nicht richtig, da beim erneuten Aufl" +
            "isten möglicherweise ein Fehler aufgetreten ist. \n16\tEs konnten nicht alle Resso" +
            "urcen identifiziert werden, die das Gerät verwendet. \n17\tDieses Gerät fordert ei" +
            "nen unbekannten Ressourcentyp an. \n18\tDie Treiber für dieses Gerät müssen erneut" +
            " installiert werden. \n19\tDie Registrierung ist eventuell beschädigt. \n20\tFehler " +
            "beim Verwenden des VxD-Ladeprogramms. \n21\tSystemfehler: Versuchen Sie, den Treib" +
            "er für dieses Gerät zu ändern. Falls dies nicht funktioniert, finden Sie weitere" +
            " Informationen in der Hardwaredokumentation. Das Gerät wird entfernt. \n22\tDas Ge" +
            "rät wurde deaktiviert. \n23\tSystemfehler: Versuchen Sie, den Treiber für dieses G" +
            "erät zu ändern. Falls dies nicht funktioniert, finden Sie weitere Informationen " +
            "in der Hardwaredokumentation. \n24\tDieses Gerät ist entweder nicht vorhanden, fun" +
            "ktioniert nicht ordnungsgemäß, oder es wurden nicht alle Treiber installiert. \n2" +
            "5\tDas Gerät wird eingerichtet. \n26\tDas Gerät wird eingerichtet. \n27\tDas Gerät ha" +
            "t keine gültige Protokollkonfiguration. \n28\tDie Treiber für dieses Gerät wurden " +
            "nicht installiert. \n29\tDieses Gerät funktioniert nicht ordnungsgemäß, da die Fir" +
            "mware des Geräts die erforderlichen Ressourcen nicht zur Verfügung stellt. \n30\tD" +
            "ieses Gerät greift auf eine Interruptanforderung (IRQ) zu, die bereits von einem" +
            " anderen Gerät verwendet wird. \n31\tDas Gerät funktioniert nicht ordnungsgemäß, d" +
            "a Windows die für das Gerät erforderlichen Treiber nicht laden kann.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ConfigManagerErrorCodeValues ConfigManagerErrorCode {
            get {
                if ((curObj["ConfigManagerErrorCode"] == null)) {
                    return ((ConfigManagerErrorCodeValues)(Convert.ToInt32(32)));
                }
                return ((ConfigManagerErrorCodeValues)(Convert.ToInt32(curObj["ConfigManagerErrorCode"])));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsConfigManagerUserConfigNull {
            get {
                if ((curObj["ConfigManagerUserConfig"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Gibt an, ob das Gerät eine benutzerdefinierte Konfiguration verwendet.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool ConfigManagerUserConfig {
            get {
                if ((curObj["ConfigManagerUserConfig"] == null)) {
                    return Convert.ToBoolean(0);
                }
                return ((bool)(curObj["ConfigManagerUserConfig"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"""CreationClassName"" gibt den Namen der Klasse oder Teilklasse an, die beim Erstellen einer Instanz verwendet wird. Wenn diese Eigenschaft mit anderen Schlüsseleigenschaften dieser Klasse verwendet wird, können alle Instanzen der Klasse und der Teilklassen eindeutig erkannt werden.")]
        public string CreationClassName {
            get {
                return ((string)(curObj["CreationClassName"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDefaultBlockSizeNull {
            get {
                if ((curObj["DefaultBlockSize"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Standardblockgröße für das Gerät in Bytes.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ulong DefaultBlockSize {
            get {
                if ((curObj["DefaultBlockSize"] == null)) {
                    return Convert.ToUInt64(0);
                }
                return ((ulong)(curObj["DefaultBlockSize"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"Description\" gibt eine Textbeschreibung des Objekts an. ")]
        public string Description {
            get {
                return ((string)(curObj["Description"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"DeviceID\" enthält eine Zeichenfolge, die das CD-ROM-Laufwerk ein" +
            "deutig identifiziert.")]
        public string DeviceID {
            get {
                return ((string)(curObj["DeviceID"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"Drive\" gibt den Laufwerkbuchstaben des CD-ROM-Laufwerks an.\nBeis" +
            "piel: d:\\")]
        public string Drive {
            get {
                return ((string)(curObj["Drive"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDriveIntegrityNull {
            get {
                if ((curObj["DriveIntegrity"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"DriveIntegrity\" gibt an, ob Dateien vom CD-ROM-Laufwerk einwandf" +
            "rei gelesen werden können, indem ein Datenblock zweimal gelesen und die Ergebnis" +
            "se miteinander verglichen werden.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool DriveIntegrity {
            get {
                if ((curObj["DriveIntegrity"] == null)) {
                    return Convert.ToBoolean(0);
                }
                return ((bool)(curObj["DriveIntegrity"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsErrorClearedNull {
            get {
                if ((curObj["ErrorCleared"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"ErrorCleared\" ist ein boolescher Wert, der angibt, dass der in d" +
            "er Eigenschaft \"LastErrorCode\" angezeigte Fehler behoben ist.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool ErrorCleared {
            get {
                if ((curObj["ErrorCleared"] == null)) {
                    return Convert.ToBoolean(0);
                }
                return ((bool)(curObj["ErrorCleared"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die formfreie Zeichenfolge \"ErrorDescription\" enthält Informationen über den in d" +
            "er Eigenschaft \"LastErrorCode\" angezeigten Fehler und eventuelle Korrekturvorgän" +
            "ge.")]
        public string ErrorDescription {
            get {
                return ((string)(curObj["ErrorDescription"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die formfreie Zeichenfolge \"ErrorMethodology\" beschreibt die Typen der Fehlererke" +
            "nnung und -korrektur, die von diesem Gerät unterstützt werden.")]
        public string ErrorMethodology {
            get {
                return ((string)(curObj["ErrorMethodology"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsFileSystemFlagsNull {
            get {
                if ((curObj["FileSystemFlags"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"FileSystemFlags\" wurde durch die Eigenschaft \"FileSystemFlagsEx\"" +
            " ersetzt.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ushort FileSystemFlags {
            get {
                if ((curObj["FileSystemFlags"] == null)) {
                    return Convert.ToUInt16(0);
                }
                return ((ushort)(curObj["FileSystemFlags"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsFileSystemFlagsExNull {
            get {
                if ((curObj["FileSystemFlagsEx"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"FileSystemFlagsEx\" gibt die dem Win32 CD-ROM-Laufwerk zugeordnet" +
            "en Dateisystemkennzeichen an. Dieser Parameter kann eine Kombination der Flags F" +
            "S_FILE_COMPRESSION und FS_VOL_IS_COMPRESSED sein.\nBeispiel: 0.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public FileSystemFlagsExValues FileSystemFlagsEx {
            get {
                if ((curObj["FileSystemFlagsEx"] == null)) {
                    return ((FileSystemFlagsExValues)(Convert.ToInt32(36)));
                }
                return ((FileSystemFlagsExValues)(Convert.ToInt32(curObj["FileSystemFlagsEx"])));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"Id\" gibt den Laufwerkbuchstaben des CD-ROM-Laufwerks an.\nBeispie" +
            "l: d:\\ ")]
        public string Id {
            get {
                return ((string)(curObj["Id"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsInstallDateNull {
            get {
                if ((curObj["InstallDate"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"InstallDate\" gibt an, wann das Objekt installiert wurde. Wenn de" +
            "r Wert nicht angegeben ist, kann das Objekt trotzdem installiert sein.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public DateTime InstallDate {
            get {
                if ((curObj["InstallDate"] != null)) {
                    return ToDateTime(((string)(curObj["InstallDate"])));
                }
                else {
                    return DateTime.MinValue;
                }
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsLastErrorCodeNull {
            get {
                if ((curObj["LastErrorCode"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("\"LastErrorCode\" gibt den letzten Fehlercode des logischen Geräts an.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint LastErrorCode {
            get {
                if ((curObj["LastErrorCode"] == null)) {
                    return Convert.ToUInt32(0);
                }
                return ((uint)(curObj["LastErrorCode"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"Manufacturer\" gibt den Hersteller des Win32 CD-ROM-Laufwerks an." +
            "\nBeispiel: PLEXTOR")]
        public string Manufacturer {
            get {
                return ((string)(curObj["Manufacturer"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsMaxBlockSizeNull {
            get {
                if ((curObj["MaxBlockSize"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Maximale Größe in Bytes des Mediums, auf das zugegriffen wird.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ulong MaxBlockSize {
            get {
                if ((curObj["MaxBlockSize"] == null)) {
                    return Convert.ToUInt64(0);
                }
                return ((ulong)(curObj["MaxBlockSize"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsMaximumComponentLengthNull {
            get {
                if ((curObj["MaximumComponentLength"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Die Eigenschaft ""MaximumComponentLength"" gibt die maximale Länge einer Dateinamenkomponente an, die vom Win32-CD-ROM-Laufwerk unterstützt wird. Eine Dateinamenkomponente ist der Teil des Dateinamens zwischen den umgekehrten Schrägstrichen. Der Wert kann verwendet werden, um anzugeben, ob lange Dateinamen von einem Dateisystem unterstützt werden. Für ein FAT-Dateisystem, das lange Namen unterstützt, wird z. B. der Wert 255 gespeichert, statt 8.3. Lange Namen können auch von NTFS-Dateisystemen unterstützt werden.
Beispiel: 255")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint MaximumComponentLength {
            get {
                if ((curObj["MaximumComponentLength"] == null)) {
                    return Convert.ToUInt32(0);
                }
                return ((uint)(curObj["MaximumComponentLength"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsMaxMediaSizeNull {
            get {
                if ((curObj["MaxMediaSize"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Maximale Größe in KB des Mediums, die dieses Gerät unterstützt. Die Kilobytes wer" +
            "den als Byteanzahl mal 1000 interpretiert (nicht die Byteanzahl mal 1024).")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ulong MaxMediaSize {
            get {
                if ((curObj["MaxMediaSize"] == null)) {
                    return Convert.ToUInt64(0);
                }
                return ((ulong)(curObj["MaxMediaSize"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsMediaLoadedNull {
            get {
                if ((curObj["MediaLoaded"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"MediaLoaded\" gibt an, ob sich eine CD im Laufwerk befindet.\nWert" +
            "e: TRUE oder FALSE. TRUE gibt an, dass eine CD in das Laufwerk eingelegt ist.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool MediaLoaded {
            get {
                if ((curObj["MediaLoaded"] == null)) {
                    return Convert.ToBoolean(0);
                }
                return ((bool)(curObj["MediaLoaded"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"MediaType\" gibt den Medientyp an, der von diesem Gerät verwendet" +
            " wird. In dieser Klasse ist der Wert immer \"CD-ROM\".")]
        public string MediaType {
            get {
                return ((string)(curObj["MediaType"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"MfrAssignedRevisionLevel\" gibt den von der Firmwarerevision des " +
            "CD-ROM-Laufwerks angegebenen Hersteller an.\n")]
        public string MfrAssignedRevisionLevel {
            get {
                return ((string)(curObj["MfrAssignedRevisionLevel"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsMinBlockSizeNull {
            get {
                if ((curObj["MinBlockSize"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Minimale Größe in Bytes des Mediums, auf das zugegriffen wird.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ulong MinBlockSize {
            get {
                if ((curObj["MinBlockSize"] == null)) {
                    return Convert.ToUInt64(0);
                }
                return ((ulong)(curObj["MinBlockSize"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"Name\" definiert die Objektbezeichnung. Wenn sich diese Eigenscha" +
            "ft in einer Teilklasse befindet, kann die Eigenschaft \"Name\" als Eigenschaft \"Ke" +
            "y\" überschrieben werden.")]
        public string Name {
            get {
                return ((string)(curObj["Name"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsNeedsCleaningNull {
            get {
                if ((curObj["NeedsCleaning"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Boolescher Wert, der angibt, dass das Laufwerk gereinigt werden muss. In der Eige" +
            "nschaft \"Capabilities\" wird angegeben, ob eine automatische oder manuelle Reinig" +
            "ung möglich ist. ")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool NeedsCleaning {
            get {
                if ((curObj["NeedsCleaning"] == null)) {
                    return Convert.ToBoolean(0);
                }
                return ((bool)(curObj["NeedsCleaning"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsNumberOfMediaSupportedNull {
            get {
                if ((curObj["NumberOfMediaSupported"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Wenn das Gerät für den Medienzugriff mehrere individuelle Medien unterstützt, gib" +
            "t diese Eigenschaft die maximale Medienanzahl an, die unterstützt oder eingelegt" +
            " werden kann.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint NumberOfMediaSupported {
            get {
                if ((curObj["NumberOfMediaSupported"] == null)) {
                    return Convert.ToUInt32(0);
                }
                return ((uint)(curObj["NumberOfMediaSupported"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Gibt die Win32 Plug & Play-Gerätekennung des logischen Geräts an. Beispiel: *PNP0" +
            "30b")]
        public string PNPDeviceID {
            get {
                return ((string)(curObj["PNPDeviceID"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Gibt die Energie-spezifischen Funktionen des logischen Geräts an. Die Werte 0=""Unbekannt"", 1=""Nicht unterstützt"" und 2=""Deaktiviert"" können angegeben werden. Der Wert 3=""Aktiviert"" gibt an, dass die Energieverwaltungsfunktionen aktiviert sind, aber die exakte Funktion unbekannt ist oder die Informationen nicht verfügbar sind. ""Automatische Energiesparmodi"" (4) gibt an, dass ein Gerät seinen Energiestatus basierend auf dem Energieverbrauch oder anderen Kriterien ändern kann. ""Energiestatus einstellbar"" (5) gibt an, dass die Methode ""SetPowerState"" unterstützt wird. ""Energiezyklus unterstützt"" (6) gibt an, dass die Methode ""SetPowerState"" mit dem Parameter ""PowerState"" 5 (""Energiezyklus"") ausgeführt werden kann. ""Geplante Reaktivierung unterstützt"" (7) gibt an, dass die Methode ""SetPowerState"" mit dem Parameter ""PowerState"" 5 (""Energiezyklus"") und dem Parameter ""Time"" ausgeführt werden kann.")]
        public PowerManagementCapabilitiesValues[] PowerManagementCapabilities {
            get {
                Array arrEnumVals = ((Array)(curObj["PowerManagementCapabilities"]));
                PowerManagementCapabilitiesValues[] enumToRet = new PowerManagementCapabilitiesValues[arrEnumVals.Length];
                int counter = 0;
                for (counter = 0; (counter < arrEnumVals.Length); counter = (counter + 1)) {
                    enumToRet[counter] = ((PowerManagementCapabilitiesValues)(Convert.ToInt32(arrEnumVals.GetValue(counter))));
                }
                return enumToRet;
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPowerManagementSupportedNull {
            get {
                if ((curObj["PowerManagementSupported"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Boolescher Wert, der angibt, dass das Gerät im Stromsparmodus verwaltet werden kann, z. B. dass es in den Stromsparmodus versetzt werden kann. Dieser Boolescher Wert zeigt nicht an, ob die Energiesparverwaltungs-Funktionen zurzeit aktiviert sind oder welche der Funktionen unterstützt werden. Verwenden Sie das Array ""PowerManagementCapabilities"", um diese Informationen anzuzeigen. Falls dieser Wert auf FALSE festgelegt ist, sollte der ganzzahlige Wert 1 für die Zeichenfolge ""Not Supported"" der einzige Eintrag im Array ""PowerManagementCapabilities"" sein.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool PowerManagementSupported {
            get {
                if ((curObj["PowerManagementSupported"] == null)) {
                    return Convert.ToBoolean(0);
                }
                return ((bool)(curObj["PowerManagementSupported"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"RevisionLevel\" gibt die Firmwarerevision des Win32 CD-ROM-Laufwe" +
            "rks an.")]
        public string RevisionLevel {
            get {
                return ((string)(curObj["RevisionLevel"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsSCSIBusNull {
            get {
                if ((curObj["SCSIBus"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"SCSIBus\" gibt die SCSI-Busnummer des Laufwerks an.\nBeispiel: 0")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint SCSIBus {
            get {
                if ((curObj["SCSIBus"] == null)) {
                    return Convert.ToUInt32(0);
                }
                return ((uint)(curObj["SCSIBus"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsSCSILogicalUnitNull {
            get {
                if ((curObj["SCSILogicalUnit"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Die Eigenschaft ""SCSILogicalUnit"" gibt die SCSI LUN (Logical Unit Number) des Laufwerks an. Die LUN definiert, auf welchen SCSI-Controller in einem System mit mehreren Controllern zugegriffen wird. Die SCSI-Gerätekennung ist die Definition mehrerer Geräte auf einem Controller.
Beispiel: 0")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ushort SCSILogicalUnit {
            get {
                if ((curObj["SCSILogicalUnit"] == null)) {
                    return Convert.ToUInt16(0);
                }
                return ((ushort)(curObj["SCSILogicalUnit"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsSCSIPortNull {
            get {
                if ((curObj["SCSIPort"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"SCSIPort\" gibt die SCSI-Anschlussnummer des Laufwerks an.\nBeispi" +
            "el: 1")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ushort SCSIPort {
            get {
                if ((curObj["SCSIPort"] == null)) {
                    return Convert.ToUInt16(0);
                }
                return ((ushort)(curObj["SCSIPort"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsSCSITargetIdNull {
            get {
                if ((curObj["SCSITargetId"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"SCSITargetId\" gibt die SCSI-Kennung des Win32 CD-ROM-Laufwerks a" +
            "n.\nBeispiel: 0.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ushort SCSITargetId {
            get {
                if ((curObj["SCSITargetId"] == null)) {
                    return Convert.ToUInt16(0);
                }
                return ((ushort)(curObj["SCSITargetId"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"Serial number\" ist die vom Hersteller zugewiesene Nummer, durch " +
            "die die physikalischen Medien identifiziert werden. \nBeispiel: WD-WM3493798728 w" +
            "ird für die Serienummer eines Datenträgers verwendet.")]
        public string SerialNumber {
            get {
                return ((string)(curObj["SerialNumber"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsSizeNull {
            get {
                if ((curObj["Size"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"Size\" gibt die Größe des Laufwerks an.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ulong Size {
            get {
                if ((curObj["Size"] == null)) {
                    return Convert.ToUInt64(0);
                }
                return ((ulong)(curObj["Size"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Die Eigenschaft ""Status"" gibt den aktuellen Status des Objekts an. Es können betriebsbereite oder nicht betriebsbereite Zustände definiert werden. Betriebsbereite Zustände sind ""OK"", ""Heruntergestuft"" und ""Künftiger Fehler"". ""Künftiger Fehler"" gibt an, dass ein Element ordnungsgemäß funktioniert, aber in naher Zukunft ein Fehler auftreten wird. Ein Beispiel ist eine SMART-aktivierte Festplatte. Nicht betriebsbereite Zustände sind ""Fehler"", ""Starten"", ""Beenden"" und ""Dienst"". ""Dienst"" kann während des erneuten Spiegelns eines Datenträgers, beim erneuten Laden einer Benutzerberechtigungsliste oder einem anderen administrativen Vorgang zutreffen. Nicht alle Vorgänge sind online.")]
        public string Status {
            get {
                return ((string)(curObj["Status"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsStatusInfoNull {
            get {
                if ((curObj["StatusInfo"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Zeichenfolge \"StatusInfo\" gibt den Gerätestatus an: \"Aktiviert\" (Wert = 3), \"" +
            "Deaktiviert\" (4), \"Andere\" (1) oder \"Unbekannt\" (2). Der Wert 5 (\"Nicht anwendba" +
            "r\") wird verwendet, wenn diese Eigenschaft nicht auf das logische Gerät zutrifft" +
            ".")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public StatusInfoValues StatusInfo {
            get {
                if ((curObj["StatusInfo"] == null)) {
                    return ((StatusInfoValues)(Convert.ToInt32(0)));
                }
                return ((StatusInfoValues)(Convert.ToInt32(curObj["StatusInfo"])));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("\"CSCreationClassName\" des bereichsdefinierenden Systems.")]
        public string SystemCreationClassName {
            get {
                return ((string)(curObj["SystemCreationClassName"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Der Name des bereichsdefinierenden Systems.")]
        public string SystemName {
            get {
                return ((string)(curObj["SystemName"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsTransferRateNull {
            get {
                if ((curObj["TransferRate"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"TransferRate\" gibt die Übertragungsrate des CD-ROM-Laufwerks an." +
            " Der Wert 1 gibt an, dass die Rate nicht bestimmt werden konnte (wenn sich z. B." +
            " keine CD im Laufwerk befindet).")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public double TransferRate {
            get {
                if ((curObj["TransferRate"] == null)) {
                    return Convert.ToDouble(0);
                }
                return ((double)(curObj["TransferRate"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"VolumeName\" gibt den Datenträgernamen des Win32 CD-ROM-Laufwerks" +
            " an.")]
        public string VolumeName {
            get {
                return ((string)(curObj["VolumeName"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"VolumeSerialNumber\" gibt die Volumeseriennummer des Mediums im C" +
            "D-ROM-Laufwerk an.\nBeispiel: A8C3-D032")]
        public string VolumeSerialNumber {
            get {
                return ((string)(curObj["VolumeSerialNumber"]));
            }
        }
        
        private bool CheckIfProperClass(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions OptionsParam) {
            if (((path != null) 
                        && (string.Compare(path.ClassName, this.ManagementClassName, true, CultureInfo.InvariantCulture) == 0))) {
                return true;
            }
            else {
                return CheckIfProperClass(new ManagementObject(mgmtScope, path, OptionsParam));
            }
        }
        
        private bool CheckIfProperClass(ManagementBaseObject theObj) {
            if (((theObj != null) 
                        && (string.Compare(((string)(theObj["__CLASS"])), this.ManagementClassName, true, CultureInfo.InvariantCulture) == 0))) {
                return true;
            }
            else {
                Array parentClasses = ((Array)(theObj["__DERIVATION"]));
                if ((parentClasses != null)) {
                    int count = 0;
                    for (count = 0; (count < parentClasses.Length); count = (count + 1)) {
                        if ((string.Compare(((string)(parentClasses.GetValue(count))), this.ManagementClassName, true, CultureInfo.InvariantCulture) == 0)) {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        
        private bool ShouldSerializeAvailability() {
            if ((this.IsAvailabilityNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeConfigManagerErrorCode() {
            if ((this.IsConfigManagerErrorCodeNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeConfigManagerUserConfig() {
            if ((this.IsConfigManagerUserConfigNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeDefaultBlockSize() {
            if ((this.IsDefaultBlockSizeNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeDriveIntegrity() {
            if ((this.IsDriveIntegrityNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeErrorCleared() {
            if ((this.IsErrorClearedNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeFileSystemFlags() {
            if ((this.IsFileSystemFlagsNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeFileSystemFlagsEx() {
            if ((this.IsFileSystemFlagsExNull == false)) {
                return true;
            }
            return false;
        }
        
        // Konvertiert einen Zeitpunkt (Datum und Uhrzeit) im DMTF-Format in ein DateTime-Objekt.
        static DateTime ToDateTime(string dmtfDate) {
            DateTime initializer = DateTime.MinValue;
            int year = initializer.Year;
            int month = initializer.Month;
            int day = initializer.Day;
            int hour = initializer.Hour;
            int minute = initializer.Minute;
            int second = initializer.Second;
            long ticks = 0;
            string dmtf = dmtfDate;
            DateTime datetime = DateTime.MinValue;
            string tempString = string.Empty;
            if ((dmtf == null)) {
                throw new ArgumentOutOfRangeException();
            }
            if ((dmtf.Length == 0)) {
                throw new ArgumentOutOfRangeException();
            }
            if ((dmtf.Length != 25)) {
                throw new ArgumentOutOfRangeException();
            }
            try {
                tempString = dmtf.Substring(0, 4);
                if (("****" != tempString)) {
                    year = int.Parse(tempString);
                }
                tempString = dmtf.Substring(4, 2);
                if (("**" != tempString)) {
                    month = int.Parse(tempString);
                }
                tempString = dmtf.Substring(6, 2);
                if (("**" != tempString)) {
                    day = int.Parse(tempString);
                }
                tempString = dmtf.Substring(8, 2);
                if (("**" != tempString)) {
                    hour = int.Parse(tempString);
                }
                tempString = dmtf.Substring(10, 2);
                if (("**" != tempString)) {
                    minute = int.Parse(tempString);
                }
                tempString = dmtf.Substring(12, 2);
                if (("**" != tempString)) {
                    second = int.Parse(tempString);
                }
                tempString = dmtf.Substring(15, 6);
                if (("******" != tempString)) {
                    ticks = (long.Parse(tempString) * ((long)((TimeSpan.TicksPerMillisecond / 1000))));
                }
                if (((((((((year < 0) 
                            || (month < 0)) 
                            || (day < 0)) 
                            || (hour < 0)) 
                            || (minute < 0)) 
                            || (minute < 0)) 
                            || (second < 0)) 
                            || (ticks < 0))) {
                    throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception e) {
                throw new ArgumentOutOfRangeException(null, e.Message);
            }
            datetime = new DateTime(year, month, day, hour, minute, second, 0);
            datetime = datetime.AddTicks(ticks);
            TimeSpan tickOffset = TimeZone.CurrentTimeZone.GetUtcOffset(datetime);
            int UTCOffset = 0;
            int OffsetToBeAdjusted = 0;
            long OffsetMins = ((long)((tickOffset.Ticks / TimeSpan.TicksPerMinute)));
            tempString = dmtf.Substring(22, 3);
            if ((tempString != "******")) {
                tempString = dmtf.Substring(21, 4);
                try {
                    UTCOffset = int.Parse(tempString);
                }
                catch (Exception e) {
                    throw new ArgumentOutOfRangeException(null, e.Message);
                }
                OffsetToBeAdjusted = ((int)((OffsetMins - UTCOffset)));
                datetime = datetime.AddMinutes(((double)(OffsetToBeAdjusted)));
            }
            return datetime;
        }
        
        // Konvertiert ein DateTime-Objekt in das DMTF-Format für Datum und Uhrzeit.
        static string ToDmtfDateTime(DateTime date) {
            string utcString = string.Empty;
            TimeSpan tickOffset = TimeZone.CurrentTimeZone.GetUtcOffset(date);
            long OffsetMins = ((long)((tickOffset.Ticks / TimeSpan.TicksPerMinute)));
            if ((Math.Abs(OffsetMins) > 999)) {
                date = date.ToUniversalTime();
                utcString = "+000";
            }
            else {
                if ((tickOffset.Ticks >= 0)) {
                    utcString = string.Concat("+", ((long)((tickOffset.Ticks / TimeSpan.TicksPerMinute))).ToString().PadLeft(3, '0'));
                }
                else {
                    string strTemp = ((long)(OffsetMins)).ToString();
                    utcString = string.Concat("-", strTemp.Substring(1, (strTemp.Length - 1)).PadLeft(3, '0'));
                }
            }
            string dmtfDateTime = ((int)(date.Year)).ToString().PadLeft(4, '0');
            dmtfDateTime = string.Concat(dmtfDateTime, ((int)(date.Month)).ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, ((int)(date.Day)).ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, ((int)(date.Hour)).ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, ((int)(date.Minute)).ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, ((int)(date.Second)).ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, ".");
            DateTime dtTemp = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, 0);
            long microsec = ((long)((((date.Ticks - dtTemp.Ticks) 
                        * 1000) 
                        / TimeSpan.TicksPerMillisecond)));
            string strMicrosec = ((long)(microsec)).ToString();
            if ((strMicrosec.Length > 6)) {
                strMicrosec = strMicrosec.Substring(0, 6);
            }
            dmtfDateTime = string.Concat(dmtfDateTime, strMicrosec.PadLeft(6, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, utcString);
            return dmtfDateTime;
        }
        
        private bool ShouldSerializeInstallDate() {
            if ((this.IsInstallDateNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeLastErrorCode() {
            if ((this.IsLastErrorCodeNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeMaxBlockSize() {
            if ((this.IsMaxBlockSizeNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeMaximumComponentLength() {
            if ((this.IsMaximumComponentLengthNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeMaxMediaSize() {
            if ((this.IsMaxMediaSizeNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeMediaLoaded() {
            if ((this.IsMediaLoadedNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeMinBlockSize() {
            if ((this.IsMinBlockSizeNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeNeedsCleaning() {
            if ((this.IsNeedsCleaningNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeNumberOfMediaSupported() {
            if ((this.IsNumberOfMediaSupportedNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializePowerManagementSupported() {
            if ((this.IsPowerManagementSupportedNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeSCSIBus() {
            if ((this.IsSCSIBusNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeSCSILogicalUnit() {
            if ((this.IsSCSILogicalUnitNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeSCSIPort() {
            if ((this.IsSCSIPortNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeSCSITargetId() {
            if ((this.IsSCSITargetIdNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeSize() {
            if ((this.IsSizeNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeStatusInfo() {
            if ((this.IsStatusInfoNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeTransferRate() {
            if ((this.IsTransferRateNull == false)) {
                return true;
            }
            return false;
        }
        
        [Browsable(true)]
        public void CommitObject() {
            if ((isEmbedded == false)) {
                PrivateLateBoundObject.Put();
            }
        }
        
        [Browsable(true)]
        public void CommitObject(PutOptions putOptions) {
            if ((isEmbedded == false)) {
                PrivateLateBoundObject.Put(putOptions);
            }
        }
        
        private void Initialize() {
            AutoCommitProp = true;
            isEmbedded = false;
        }
        
        private static string ConstructPath(string keyDeviceID) {
            string strPath = "root\\CimV2:Win32_CdRomDrive";
            strPath = string.Concat(strPath, string.Concat(".DeviceID=", string.Concat("\"", string.Concat(keyDeviceID, "\""))));
            return strPath;
        }
        
        private void InitializeObject(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) {
            Initialize();
            if ((path != null)) {
                if ((CheckIfProperClass(mgmtScope, path, getOptions) != true)) {
                    throw new ArgumentException("Der Klassenname stimmt nicht überein.");
                }
            }
            PrivateLateBoundObject = new ManagementObject(mgmtScope, path, getOptions);
            PrivateSystemProperties = new ManagementSystemProperties(PrivateLateBoundObject);
            curObj = PrivateLateBoundObject;
        }
        
        // Unterschiedliche Überladungen von 'GetInstances()' unterstützen beim Aufzählen von Instanzen der WMI-Klasse.
        public static CdRomDriveCollection GetInstances() {
            return GetInstances(null, null, null);
        }
        
        public static CdRomDriveCollection GetInstances(string condition) {
            return GetInstances(null, condition, null);
        }
        
        public static CdRomDriveCollection GetInstances(string[] selectedProperties) {
            return GetInstances(null, null, selectedProperties);
        }
        
        public static CdRomDriveCollection GetInstances(string condition, string[] selectedProperties) {
            return GetInstances(null, condition, selectedProperties);
        }
        
        public static CdRomDriveCollection GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) {
            if ((mgmtScope == null)) {
                if ((statMgmtScope == null)) {
                    mgmtScope = new ManagementScope();
                    mgmtScope.Path.NamespacePath = "root\\CimV2";
                }
                else {
                    mgmtScope = statMgmtScope;
                }
            }
            ManagementPath pathObj = new ManagementPath();
            pathObj.ClassName = "Win32_CdRomDrive";
            pathObj.NamespacePath = "root\\CimV2";
            ManagementClass clsObject = new ManagementClass(mgmtScope, pathObj, null);
            if ((enumOptions == null)) {
                enumOptions = new EnumerationOptions();
                enumOptions.EnsureLocatable = true;
            }
            return new CdRomDriveCollection(clsObject.GetInstances(enumOptions));
        }
        
        public static CdRomDriveCollection GetInstances(ManagementScope mgmtScope, string condition) {
            return GetInstances(mgmtScope, condition, null);
        }
        
        public static CdRomDriveCollection GetInstances(ManagementScope mgmtScope, string[] selectedProperties) {
            return GetInstances(mgmtScope, null, selectedProperties);
        }
        
        public static CdRomDriveCollection GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) {
            if ((mgmtScope == null)) {
                if ((statMgmtScope == null)) {
                    mgmtScope = new ManagementScope();
                    mgmtScope.Path.NamespacePath = "root\\CimV2";
                }
                else {
                    mgmtScope = statMgmtScope;
                }
            }
            ManagementObjectSearcher ObjectSearcher = new ManagementObjectSearcher(mgmtScope, new SelectQuery("Win32_CdRomDrive", condition, selectedProperties));
            EnumerationOptions enumOptions = new EnumerationOptions();
            enumOptions.EnsureLocatable = true;
            ObjectSearcher.Options = enumOptions;
            return new CdRomDriveCollection(ObjectSearcher.Get());
        }
        
        [Browsable(true)]
        public static CdRomDrive CreateInstance() {
            ManagementScope mgmtScope = null;
            if ((statMgmtScope == null)) {
                mgmtScope = new ManagementScope();
                mgmtScope.Path.NamespacePath = CreatedWmiNamespace;
            }
            else {
                mgmtScope = statMgmtScope;
            }
            ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
            ManagementClass tmpMgmtClass = new ManagementClass(mgmtScope, mgmtPath, null);
            return new CdRomDrive(tmpMgmtClass.CreateInstance());
        }
        
        [Browsable(true)]
        public void Delete() {
            PrivateLateBoundObject.Delete();
        }
        
        public uint Reset() {
            if ((isEmbedded == false)) {
                ManagementBaseObject inParams = null;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("Reset", inParams, null);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return Convert.ToUInt32(0);
            }
        }
        
        public uint SetPowerState(ushort PowerState, DateTime Time) {
            if ((isEmbedded == false)) {
                ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("SetPowerState");
                inParams["PowerState"] = ((ushort)(PowerState));
                inParams["Time"] = ToDmtfDateTime(((DateTime)(Time)));
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("SetPowerState", inParams, null);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return Convert.ToUInt32(0);
            }
        }
        
        public enum AvailabilityValues {
            
            Andere = 1,
            
            Unbekannt = 2,
            
            Wird_ausgeführt_kein_Energiesparmodus = 3,
            
            Warnung = 4,
            
            Wird_getestet = 5,
            
            Nicht_anwendbar = 6,
            
            Ausschalten = 7,
            
            Offline = 8,
            
            Außer_Betrieb = 9,
            
            Heruntergestuft = 10,
            
            Nicht_installiert = 11,
            
            Installationsfehler = 12,
            
            Energiesparmodus_Unbekannt = 13,
            
            Energiesparmodus_Niedriger_Energiestatus = 14,
            
            Energiesparmodus_Standby = 15,
            
            Energiezyklus = 16,
            
            Energiesparmodus_Warnung = 17,
            
            Angehalten = 18,
            
            Nicht_bereit = 19,
            
            Nicht_konfiguriert = 20,
            
            Ruhe = 21,
            
            NULL_ENUM_VALUE = 0,
        }
        
        public enum CapabilitiesValues {
            
            Unbekannt = 0,
            
            Andere = 1,
            
            Sequenzieller_Zugriff = 2,
            
            Zufälliger_Zugriff = 3,
            
            Schreiben_wird_unterstützt = 4,
            
            Verschlüsselung = 5,
            
            Komprimierung = 6,
            
            Unterstützt_Wechselmedien = 7,
            
            Manuelle_Reinigung = 8,
            
            Automatische_Reinigung = 9,
            
            SMART_Meldung = 10,
            
            Unterstützt_zweiseitige_Medien = 11,
            
            Bereitstellungsaufhebung_zum_Auswerfen_ist_nicht_erforderlich = 12,
            
            NULL_ENUM_VALUE = 13,
        }
        
        public enum ConfigManagerErrorCodeValues {
            
            Das_Gerät_funktioniert_einwandfrei_ = 0,
            
            Das_Gerät_ist_nicht_richtig_konfiguriert_ = 1,
            
            Der_Treiber_für_dieses_Gerät_konnte_nicht_geladen_werden_ = 2,
            
            Der_Treiber_für_dieses_Gerät_ist_entweder_beschädigt_oder_es_stehen_nicht_genügend_Arbeitsspeicher_oder_andere_Ressourcen_zur_Verfügung_ = 3,
            
            Dieses_Gerät_funktioniert_nicht_ordnungsgemäß_Eventuell_ist_einer_der_Treiber_oder_die_Registrierung_beschädigt_ = 4,
            
            Der_Treiber_für_dieses_Gerät_erfordert_eine_Ressource_die_Windows_nicht_verwalten_kann_ = 5,
            
            Die_Startkonfiguration_dieses_Geräts_verursacht_Konflikte_mit_anderen_Geräten_ = 6,
            
            Filtervorgang_fehlgeschlagen_ = 7,
            
            Das_Treiberladegerät_für_dieses_Gerät_ist_nicht_vorhanden_ = 8,
            
            Dieses_Gerät_funktioniert_nicht_ordnungsgemäß_da_die_steuernde_Firmware_die_Ressourcen_für_das_Gerät_falsch_angibt_ = 9,
            
            Das_Gerät_kann_nicht_gestartet_werden_ = 10,
            
            Das_Gerät_ist_fehlgeschlagen_ = 11,
            
            Dieses_Gerät_kann_keine_ausreichenden_freien_Ressourcen_finden_die_verwendet_werden_können_ = 12,
            
            Die_Ressourcen_des_Geräts_können_nicht_verifiziert_werden_ = 13,
            
            Sie_müssen_den_Computer_neu_starten_damit_dieses_Gerät_ordnungsgemäß_funktioniert_ = 14,
            
            Das_Gerät_funktioniert_nicht_richtig_da_beim_erneuten_Auflisten_möglicherweise_ein_Fehler_aufgetreten_ist_ = 15,
            
            Es_konnten_nicht_alle_Ressourcen_identifiziert_werden_die_das_Gerät_verwendet_ = 16,
            
            Dieses_Gerät_fordert_einen_unbekannten_Ressourcentyp_an_ = 17,
            
            Die_Treiber_für_dieses_Gerät_müssen_erneut_installiert_werden_ = 18,
            
            Fehler_beim_Verwenden_des_VxD_Ladeprogramms_ = 19,
            
            Die_Registrierung_ist_eventuell_beschädigt_ = 20,
            
            Systemfehler_Versuchen_Sie_den_Treiber_für_dieses_Gerät_zu_ändern_Falls_dies_nicht_funktioniert_finden_Sie_weitere_Informationen_in_der_Hardwaredokumentation_Das_Gerät_wird_entfernt_ = 21,
            
            Das_Gerät_wurde_deaktiviert_ = 22,
            
            Systemfehler_Versuchen_Sie_den_Treiber_für_dieses_Gerät_zu_ändern_Falls_dies_nicht_funktioniert_finden_Sie_weitere_Informationen_in_der_Hardwaredokumentation_ = 23,
            
            Dieses_Gerät_ist_entweder_nicht_vorhanden_funktioniert_nicht_ordnungsgemäß_oder_es_wurden_nicht_alle_Treiber_installiert_ = 24,
            
            Das_Gerät_wird_eingerichtet_ = 25,
            
            Das_Gerät_wird_eingerichtet_0 = 26,
            
            Das_Gerät_hat_keine_gültige_Protokollkonfiguration_ = 27,
            
            Die_Treiber_für_dieses_Gerät_wurden_nicht_installiert_ = 28,
            
            Dieses_Gerät_funktioniert_nicht_ordnungsgemäß_da_die_Firmware_des_Geräts_die_erforderlichen_Ressourcen_nicht_zur_Verfügung_stellt_ = 29,
            
            Dieses_Gerät_greift_auf_eine_Interruptanforderung_IRQ_zu_die_bereits_von_einem_anderen_Gerät_verwendet_wird_ = 30,
            
            Das_Gerät_funktioniert_nicht_ordnungsgemäß_da_Windows_die_für_das_Gerät_erforderlichen_Treiber_nicht_laden_kann_ = 31,
            
            NULL_ENUM_VALUE = 32,
        }
        
        public enum FileSystemFlagsExValues {
            
            Suche_mit_Berücksichtigung_von_Groß_Kleinschreibung = 0,
            
            Beibehaltene_groß_bzw_kleingeschriebene_Namen = 1,
            
            Unicode_auf_Datenträger = 2,
            
            Beständige_Zugriffssteuerungslisten = 3,
            
            Dateikomprimierung = 4,
            
            Volumedatenträgerkontingente = 5,
            
            Unterstützt_geringe_Datendichte = 6,
            
            Unterstützt_Analysepunkte = 7,
            
            Unterstützt_Remotespeicher = 8,
            
            Unterstützt_lange_Dateinamen = 14,
            
            Volume_ist_komprimiert = 15,
            
            Unterstützt_Objektkennungen = 16,
            
            Unterstützt_Verschlüsselung = 17,
            
            Unterstützt_benannte_Datenströme = 18,
            
            NULL_ENUM_VALUE = 36,
        }
        
        public enum PowerManagementCapabilitiesValues {
            
            Unbekannt = 0,
            
            Nicht_unterstützt = 1,
            
            Deaktiviert = 2,
            
            Aktiviert = 3,
            
            Automatische_Energiesparmodi = 4,
            
            Energiestatus_einstellbar = 5,
            
            Energiezyklus_unterstützt = 6,
            
            Geplante_Reaktivierung_unterstützt = 7,
            
            NULL_ENUM_VALUE = 8,
        }
        
        public enum StatusInfoValues {
            
            Andere = 1,
            
            Unbekannt = 2,
            
            Aktiviert = 3,
            
            Deaktiviert = 4,
            
            Nicht_anwendbar = 5,
            
            NULL_ENUM_VALUE = 0,
        }
        
        // Enumeratorimplementierung zum Aufzählen von Instanzen der Klasse.
        public class CdRomDriveCollection : object, ICollection {
            
            private ManagementObjectCollection privColObj;
            
            public CdRomDriveCollection(ManagementObjectCollection objCollection) {
                privColObj = objCollection;
            }
            
            public virtual int Count {
                get {
                    return privColObj.Count;
                }
            }
            
            public virtual bool IsSynchronized {
                get {
                    return privColObj.IsSynchronized;
                }
            }
            
            public virtual object SyncRoot {
                get {
                    return this;
                }
            }
            
            public virtual void CopyTo(Array array, int index) {
                privColObj.CopyTo(array, index);
                int nCtr;
                for (nCtr = 0; (nCtr < array.Length); nCtr = (nCtr + 1)) {
                    array.SetValue(new CdRomDrive(((ManagementObject)(array.GetValue(nCtr)))), nCtr);
                }
            }
            
            public virtual IEnumerator GetEnumerator() {
                return new CdRomDriveEnumerator(privColObj.GetEnumerator());
            }
            
            public class CdRomDriveEnumerator : object, IEnumerator {
                
                private ManagementObjectCollection.ManagementObjectEnumerator privObjEnum;
                
                public CdRomDriveEnumerator(ManagementObjectCollection.ManagementObjectEnumerator objEnum) {
                    privObjEnum = objEnum;
                }
                
                public virtual object Current {
                    get {
                        return new CdRomDrive(((ManagementObject)(privObjEnum.Current)));
                    }
                }
                
                public virtual bool MoveNext() {
                    return privObjEnum.MoveNext();
                }
                
                public virtual void Reset() {
                    privObjEnum.Reset();
                }
            }
        }
        
        // 'TypeConverter' zum Behandeln von NULL-Werten für ValueType-Eigenschaften.
        public class WMIValueTypeConverter : TypeConverter {
            
            private TypeConverter baseConverter;
            
            private Type baseType;
            
            public WMIValueTypeConverter(Type inBaseType) {
                baseConverter = TypeDescriptor.GetConverter(inBaseType);
                baseType = inBaseType;
            }
            
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type srcType) {
                return baseConverter.CanConvertFrom(context, srcType);
            }
            
            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) {
                return baseConverter.CanConvertTo(context, destinationType);
            }
            
            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) {
                return baseConverter.ConvertFrom(context, culture, value);
            }
            
            public override object CreateInstance(ITypeDescriptorContext context, IDictionary dictionary) {
                return baseConverter.CreateInstance(context, dictionary);
            }
            
            public override bool GetCreateInstanceSupported(ITypeDescriptorContext context) {
                return baseConverter.GetCreateInstanceSupported(context);
            }
            
            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributeVar) {
                return baseConverter.GetProperties(context, value, attributeVar);
            }
            
            public override bool GetPropertiesSupported(ITypeDescriptorContext context) {
                return baseConverter.GetPropertiesSupported(context);
            }
            
            public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context) {
                return baseConverter.GetStandardValues(context);
            }
            
            public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) {
                return baseConverter.GetStandardValuesExclusive(context);
            }
            
            public override bool GetStandardValuesSupported(ITypeDescriptorContext context) {
                return baseConverter.GetStandardValuesSupported(context);
            }
            
            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) {
                if ((baseType.BaseType == typeof(Enum))) {
                    if ((value.GetType() == destinationType)) {
                        return value;
                    }
                    if ((((value == null) 
                                && (context != null)) 
                                && (context.PropertyDescriptor.ShouldSerializeValue(context.Instance) == false))) {
                        return  "NULL_ENUM_VALUE" ;
                    }
                    return baseConverter.ConvertTo(context, culture, value, destinationType);
                }
                if (((baseType == typeof(bool)) 
                            && (baseType.BaseType == typeof(ValueType)))) {
                    if ((((value == null) 
                                && (context != null)) 
                                && (context.PropertyDescriptor.ShouldSerializeValue(context.Instance) == false))) {
                        return "";
                    }
                    return baseConverter.ConvertTo(context, culture, value, destinationType);
                }
                if (((context != null) 
                            && (context.PropertyDescriptor.ShouldSerializeValue(context.Instance) == false))) {
                    return "";
                }
                return baseConverter.ConvertTo(context, culture, value, destinationType);
            }
        }
        
        // Eingebettete Klasse zum Darstellen der WMI-Systemeigenschaften.
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class ManagementSystemProperties {
            
            private ManagementBaseObject PrivateLateBoundObject;
            
            public ManagementSystemProperties(ManagementBaseObject ManagedObject) {
                PrivateLateBoundObject = ManagedObject;
            }
            
            [Browsable(true)]
            public int GENUS {
                get {
                    return ((int)(PrivateLateBoundObject["__GENUS"]));
                }
            }
            
            [Browsable(true)]
            public string CLASS {
                get {
                    return ((string)(PrivateLateBoundObject["__CLASS"]));
                }
            }
            
            [Browsable(true)]
            public string SUPERCLASS {
                get {
                    return ((string)(PrivateLateBoundObject["__SUPERCLASS"]));
                }
            }
            
            [Browsable(true)]
            public string DYNASTY {
                get {
                    return ((string)(PrivateLateBoundObject["__DYNASTY"]));
                }
            }
            
            [Browsable(true)]
            public string RELPATH {
                get {
                    return ((string)(PrivateLateBoundObject["__RELPATH"]));
                }
            }
            
            [Browsable(true)]
            public int PROPERTY_COUNT {
                get {
                    return ((int)(PrivateLateBoundObject["__PROPERTY_COUNT"]));
                }
            }
            
            [Browsable(true)]
            public string[] DERIVATION {
                get {
                    return ((string[])(PrivateLateBoundObject["__DERIVATION"]));
                }
            }
            
            [Browsable(true)]
            public string SERVER {
                get {
                    return ((string)(PrivateLateBoundObject["__SERVER"]));
                }
            }
            
            [Browsable(true)]
            public string NAMESPACE {
                get {
                    return ((string)(PrivateLateBoundObject["__NAMESPACE"]));
                }
            }
            
            [Browsable(true)]
            public string PATH {
                get {
                    return ((string)(PrivateLateBoundObject["__PATH"]));
                }
            }
        }
    }
}
