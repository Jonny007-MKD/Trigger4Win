using System;
using System.ComponentModel;
using System.Management;
using System.Collections;
using System.Globalization;
using System.Net;
using System.Collections.Generic;

#pragma warning disable

namespace Trigger.Classes.WMI
{
    /// <summary>
	/// <para>Eine für die WMI-Klasse generierte Klasse mit früher Bindung.</para>
	/// <para>Die ShouldSerialize<PropertyName>-Funktionen werden vom VS-Eigenschaftenbrowser verwendet, um zu überprüfen, ob eine bestimmte Eigenschaft serialisiert werden muss. Diese Funktionen werden für alle ValueType-Eigenschaften hinzugefügt (Eigenschaften des Typs Int32, BOOL usw. , die nicht auf NULL festgelegt werden können). Die Funktionen verwenden die Is<PropertyName>Null-Funktion. Die Funktionen werden auch in der TypeConverter-Implementierung für die Eigenschaften verwendet, um die jeweilige Eigenschaft in Bezug auf den NULL-Wert zu überprüfen, damit für einen Drag & Drop-Vorgang in Visual Studio ein leerer Wert im Eigenschaftenbrowser angezeigt werden kann.</para>
	/// <para>Mit Funktionen der Art 'Is<PropertyName>Null()' wird überprüft, ob eine Eigenschaft NULL ist.</para>
	/// <para>Die Reset<PropertyName>-Funktionen werden für Read/Write-Eigenschaften hinzugefügt, die NULL-Werte zulassen. Diese Funktionen werden vom VS-Designer im Eigenschaftenbrowser verwendet, um eine Eigenschaft auf NULL festzulegen.</para>
	/// <para>Für jede Eigenschaft, die zur Klasse für WMI hinzugefügt wurde, sind Attribute festgelegt, um das Verhalten im Visual Studio-Designer sowie die zu verwendende TypeConverter-Klasse zu definieren.</para>
    /// </summary>
    public class NetworkAdapterConfiguration : Component
	{
		#region internal Properties
		/// <summary>
		/// <para>Eigenschaft, die den WMI-Namespace enthält, in dem sich die Klasse befindet.</para>
		/// </summary>
        internal static string CreatedWmiNamespace = "root\\CimV2";
        
        /// <summary>
		/// <para>Eigenschaft, die den Namen der WMI-Klasse enthält, die diese Klasse erstellt hat.</para>
        /// </summary>
        internal static string CreatedClassName = "Win32_NetworkAdapterConfiguration";
        
        /// <summary>
		/// <para>Membervariable, die 'ManagementScope' enthält, das von den verschiedenen Methoden verwendet wird.</para>
        /// </summary>
		internal static ManagementScope statMgmtScope = null;
        
		/// <summary>
		/// <para></para>
		/// </summary>
        internal ManagementSystemProperties internalSystemProperties;
        
		/// <summary>
		/// <para>Zugrunde liegendes lateBound-WMI-Objekt.</para>
		/// </summary>
        internal ManagementObject internalLateBoundObject;
        
		/// <summary>
		/// <para>Membervariable, in der das automatic commit-Verhalten für die Klasse gespeichert wird.</para>
		/// </summary>
        internal bool AutoCommitProp;
        
        /// <summary>
		/// <para>Variable, die die eingebettete Eigenschaft enthält, die die Instanz darstellt.</para>
        /// </summary>
        internal ManagementBaseObject embeddedObj;
        
        /// <summary>
		/// <para>Das aktuelle WMI-Objekt.</para>
        /// </summary>
        internal ManagementBaseObject curObj;
        
        /// <summary>
		/// <para>Flag zum Anzeigen, ob die Instanz ein eingebettetes Objekt ist.</para>
        /// </summary>
        internal bool isEmbedded;
		#endregion

		#region Constructors
		// Nachstehend sind unterschiedliche Konstruktorüberladungen aufgeführt, um eine Instanz der Klasse mit einem WMI-Objekt zu initialisieren.
        public NetworkAdapterConfiguration() {
            this.InitializeObject(null, null, null);
        }
        
        public NetworkAdapterConfiguration(uint keyIndex) {
            this.InitializeObject(null, new ManagementPath(NetworkAdapterConfiguration.ConstructPath(keyIndex)), null);
        }
        
        public NetworkAdapterConfiguration(ManagementScope mgmtScope, uint keyIndex) {
            this.InitializeObject(((ManagementScope)(mgmtScope)), new ManagementPath(NetworkAdapterConfiguration.ConstructPath(keyIndex)), null);
        }
        
        public NetworkAdapterConfiguration(ManagementPath path, ObjectGetOptions getOptions) {
            this.InitializeObject(null, path, getOptions);
        }
        
        public NetworkAdapterConfiguration(ManagementScope mgmtScope, ManagementPath path) {
            this.InitializeObject(mgmtScope, path, null);
        }
        
        public NetworkAdapterConfiguration(ManagementPath path) {
            this.InitializeObject(null, path, null);
        }
        
        public NetworkAdapterConfiguration(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) {
            this.InitializeObject(mgmtScope, path, getOptions);
        }
        
        public NetworkAdapterConfiguration(ManagementObject theObject) {
            Initialize();
            if ((CheckIfProperClass(theObject) == true)) {
                internalLateBoundObject = theObject;
                internalSystemProperties = new ManagementSystemProperties(internalLateBoundObject);
                curObj = internalLateBoundObject;
            }
            else {
                throw new ArgumentException("Der Klassenname stimmt nicht überein.");
            }
        }
        
        public NetworkAdapterConfiguration(ManagementBaseObject theObject) {
            Initialize();
            if ((CheckIfProperClass(theObject) == true)) {
                embeddedObj = theObject;
                internalSystemProperties = new ManagementSystemProperties(theObject);
                curObj = embeddedObj;
                isEmbedded = true;
            }
            else {
                throw new ArgumentException("Der Klassenname stimmt nicht überein.");
            }
        }
		#endregion

		#region Public Properties
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
                return internalSystemProperties;
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
                    return internalLateBoundObject.Scope;
                }
                else {
                    return null;
                }
            }
            set {
                if ((isEmbedded == false)) {
                    internalLateBoundObject.Scope = value;
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
                    return internalLateBoundObject.Path;
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
                    internalLateBoundObject.Path = value;
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
        public bool IsArpAlwaysSourceRouteNull {
            get {
                if ((curObj["ArpAlwaysSourceRoute"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Die Eigenschaft ""ArpAlwaysSourceRoute"" gibt an, ob für ARP (Address Resolution Protocol) immer Quellrouting verwendet werden muss. Wenn diese Eigenschaft TRUE ist, überträgt TCP/IP ARP-Abfragen mit aktiviertem Quellrouting in Token Ring-Netzwerken. Standardmäßig werden ARP-Abfragen ohne Quellrouting ausgeführt. Wenn keine Antwort empfangen wird, wird der Vorgang mit aktiviertem Quellrouting wiederholt. Quellrouting ermöglicht das Routing von Netzwerkpaketen über verschiedene Netzwerktypen. Standard: FALSE.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool ArpAlwaysSourceRoute {
            get {
                if ((curObj["ArpAlwaysSourceRoute"] == null)) {
                    return Convert.ToBoolean(0);
                }
                return ((bool)(curObj["ArpAlwaysSourceRoute"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsArpUseEtherSNAPNull {
            get {
                if ((curObj["ArpUseEtherSNAP"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Die Eigenschaft ""ArpUseEtherSNAP"" gibt an, ob Ethernetpakete IEEE 802.3 SNAP-Codierung verwendet. Wenn dieser Parameter auf 1 gesetzt ist, überträgt TCP/IP Ethernetpakete mit 802.3 SNAP-Codierung. Standardmäßig werden Pakete im DIX-Ethernetformat gesendet. Windows NT/Windows 2000-Systeme können beide Formate empfangen. Standard: FALSE.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool ArpUseEtherSNAP {
            get {
                if ((curObj["ArpUseEtherSNAP"] == null)) {
                    return Convert.ToBoolean(0);
                }
                return ((bool)(curObj["ArpUseEtherSNAP"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Eine kurze Textbeschreibung (eine Zeile) des Objekts \"CIM_Setting\".")]
        public string Caption {
            get {
                return ((string)(curObj["Caption"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"DatabasePath\" gibt einen gültigen Windows-Dateipfad zu den Stand" +
            "ard-Internetdatenbankdateien (HOSTS, LMHOSTS, NETWORKS, PROTOCOLS) an. Der Pfad " +
            "wird von der Windows Sockets-Schnittstelle verwendet. Nur Windows NT und Windows" +
            " 2000.")]
        public string DatabasePath {
            get {
                return ((string)(curObj["DatabasePath"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDeadGWDetectEnabledNull {
            get {
                if ((curObj["DeadGWDetectEnabled"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Die Eigenschaft ""DeadGWDetectEnabled"" gibt an, ob deaktivierte Gateways identifiziert werden. Wenn der Wert dieses Parameters TRUE ist, werden deaktivierte Gateways von TCP identifiziert. TCP ändert IP zu einem Reservegateway, wenn ein Segment mehrmals übertragen wird, ohne eine Rückmeldung zu erhalten. Standard: TRUE.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool DeadGWDetectEnabled {
            get {
                if ((curObj["DeadGWDetectEnabled"] == null)) {
                    return Convert.ToBoolean(0);
                }
                return ((bool)(curObj["DeadGWDetectEnabled"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"DefaultIPGateway\" enthält eine Liste der IP-Adressen von Standar" +
            "dgateways, die vom Computersystem verwendet werden.\nBeispiel: 194.161.12.1 194.1" +
            "62.46.1")]
        public string[] DefaultIPGateway {
            get {
                return ((string[])(curObj["DefaultIPGateway"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDefaultTOSNull {
            get {
                if ((curObj["DefaultTOS"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"DefaultTOS\" gibt den Standard-TOS-Wert (Type Of Service) im Head" +
            "er ausgehender IP-Pakete an. Die Werte werden in RFC 791 definiert. Standard: 0," +
            " Gültiger Bereich: 0 - 255.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public byte DefaultTOS {
            get {
                if ((curObj["DefaultTOS"] == null)) {
                    return Convert.ToByte(0);
                }
                return ((byte)(curObj["DefaultTOS"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDefaultTTLNull {
            get {
                if ((curObj["DefaultTTL"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Die Eigenschaft ""DefaultTTL"" gibt den TTL-Standardwert (Time To Live) im Header ausgehender IP-Pakete an. TTL gibt den Anzahl der Router an, die ein IP-Paket übergibt, bevor es verworfen wird. Jeder Router verringert diesen Wert um Eins und verwirft das Paket, wenn TTL 0 ist. Standard: 32. Gültiger Bereich: 1 - 255.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public byte DefaultTTL {
            get {
                if ((curObj["DefaultTTL"] == null)) {
                    return Convert.ToByte(0);
                }
                return ((byte)(curObj["DefaultTTL"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Eine Textbeschreibung des Objekts \"CIM_Setting\".")]
        public string Description {
            get {
                return ((string)(curObj["Description"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDHCPEnabledNull {
            get {
                if ((curObj["DHCPEnabled"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"DHCPEnabled\" gibt an, ob ein DHCP-Server (Dynamic Host Configura" +
            "tion Protocol) beim Herstellen einer Netzwerkverbindung dem Computersystem autom" +
            "atisch eine IP-Adresse zuweist.\nWerte: TRUE oder FALSE. TRUE gibt an, dass DHCP " +
            "aktiviert ist.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool DHCPEnabled {
            get {
                if ((curObj["DHCPEnabled"] == null)) {
                    return Convert.ToBoolean(0);
                }
                return ((bool)(curObj["DHCPEnabled"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDHCPLeaseExpiresNull {
            get {
                if ((curObj["DHCPLeaseExpires"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"DHCPLeaseExpires\" gibt das Ablaufdatum für eine geleaste IP-Adre" +
            "sse an, die dem Computer vom DHCP-Server zugewiesen wurde.\nBeispiel: 20521201000" +
            "230.000000000")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public DateTime DHCPLeaseExpires {
            get {
                if ((curObj["DHCPLeaseExpires"] != null)) {
                    return ToDateTime(((string)(curObj["DHCPLeaseExpires"])));
                }
                else {
                    return DateTime.MinValue;
                }
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDHCPLeaseObtainedNull {
            get {
                if ((curObj["DHCPLeaseObtained"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"DHCPLeaseObtained\" gibt das Ausstellungsdatum der geleasten IP-A" +
            "dresse an, die dem Computer vom DHCP-Server zugewiesen wurde.\nBeispiel: 19521201" +
            "000230.000000000")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public DateTime DHCPLeaseObtained {
            get {
                if ((curObj["DHCPLeaseObtained"] != null)) {
                    return ToDateTime(((string)(curObj["DHCPLeaseObtained"])));
                }
                else {
                    return DateTime.MinValue;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"DHCPServer\" gibt die IP-Adresse des DHCP-Servers an.\nBeispiel: 1" +
            "54.55.34")]
        public string DHCPServer {
            get {
                return ((string)(curObj["DHCPServer"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"DNSDomain\" gibt einen Organisationsnamen gefolgt von einem Punkt" +
            " und einer Erweiterung an. Der Name kann alle Buchstaben von A bis Z, die Ziffer" +
            "n 0 bis 9, Bindestriche und einen Punkt als Trennzeichen enthalten.\nBeispiel: mi" +
            "crosoft.com.")]
        public string DNSDomain {
            get {
                return ((string)(curObj["DNSDomain"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Die Eigenschaft ""DNSDomainSuffixSearchOrder"" gibt die DNS-Domänensuffixe an, die während der Namensauflösung an Hostnamen angehängt werden. Wenn ein vollqualifizierter Domänenname (FQDN) von einem Hostnamen aufgelöst wird, wird der lokale Domänenname angehängt. Wenn der Domänenname nicht angehängt werden kann, werden anhand der Domänensuffixliste zusätzliche FQDNs erstellt.
Beispiel: samples.microsoft.com example.microsoft.com")]
        public string[] DNSDomainSuffixSearchOrder {
            get {
                return ((string[])(curObj["DNSDomainSuffixSearchOrder"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDNSEnabledForWINSResolutionNull {
            get {
                if ((curObj["DNSEnabledForWINSResolution"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Die Eigenschaft ""DNSEnabledForWINSResolution"" gibt an, ob DNS (Domain Name System) für die Namensauflösung über WINS (Windows Internet Naming Service) aktiviert ist. Wenn der Name mit DNS nicht aufgelöst werden kann, wird die Anforderung an WINS weitergeleitet.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool DNSEnabledForWINSResolution {
            get {
                if ((curObj["DNSEnabledForWINSResolution"] == null)) {
                    return Convert.ToBoolean(0);
                }
                return ((bool)(curObj["DNSEnabledForWINSResolution"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Die Eigenschaft ""DNSHostName"" gibt den Hostnamen an, der zum Identifizieren des lokalen Computers für die Authentifizierung durch einige Dienstprogramme verwendet wird. Andere TCP/IP-basierte Dienstprogramme können diesen Wert verwenden, um den Namen des lokalen Computers zu ermitteln. Hostnamen werden auf DNS-Servern in einer Tabelle gespeichert, die Namen zu IP-Adressen für DNS zuordnet. Der Name kann aus den Buchstaben A bis Z, den Zahlen 0 bis 9, einem Bindestrich und einem Punkt als Trennzeichen bestehen. Standardmäßig ist dieser Wert der Microsoft-Netzwerkcomputername. Der Netzwerkadministrator kann einen anderen Hostnamen erstellen, ohne den Computernamen zu ändern.
Beispiel: corpdns")]
        public string DNSHostName {
            get {
                return ((string)(curObj["DNSHostName"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"DNSServerSearchOrder\" gibt die Reihenfolge der Server-IP-Adresse" +
            "n an, die für DNS-Serverabfragen verwendet werden.")]
        public string[] DNSServerSearchOrder {
            get {
                return ((string[])(curObj["DNSServerSearchOrder"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDomainDNSRegistrationEnabledNull {
            get {
                if ((curObj["DomainDNSRegistrationEnabled"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Die Eigenschaft ""DomainDNSRegistrationEnabled"" gibt an, ob die IP-Adressen für diese Verbindung in DNS unter dem Domänennamen registriert werden (zusätzlich zur Registrierung unter dem vollständigen DNS-Namen des Computers). Der Domänenname dieser Verbindung wird mit der Methode ""SetDNSDomain()"" festgelegt oder von DHCP zugeordnet. Der registrierte Name ist der Hostname des Computers mit dem angehängten Domänennamen. Nur Windows 2000.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool DomainDNSRegistrationEnabled {
            get {
                if ((curObj["DomainDNSRegistrationEnabled"] == null)) {
                    return Convert.ToBoolean(0);
                }
                return ((bool)(curObj["DomainDNSRegistrationEnabled"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsForwardBufferMemoryNull {
            get {
                if ((curObj["ForwardBufferMemory"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Die Eigenschaft ""ForwardBufferMemory"" gibt die Größe des Speichers an, der von IP zum Speichern von Paketdaten in der Routerpaketwarteschlange zugeordnet wird. Wenn dieser Puffer voll ist, beginnt der Router Pakete in der Warteschlange zu verwerfen. Der Wert dieses Parameters muss ein Vielfaches von 256 sein, da Datenpuffer von Paketwarteschlangen 256 Bytes groß sind. Für größere Pakete sind mehrere Puffer miteinander verkettet. Der IP-Header für ein Paket wird separat gespeichert. Dieser Parameter wird ignoriert und die Puffer werden nicht zugeordnet, wenn der IP-Router nicht aktiviert ist. Die Puffergröße kann der Netzwerk-MTU entsprechen oder kleiner als 0xFFFFFFFF sein. Standard: 74240 (fünfzig 1480 Byte Pakete, gerundet zu einem Vielfachen von 256).")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint ForwardBufferMemory {
            get {
                if ((curObj["ForwardBufferMemory"] == null)) {
                    return Convert.ToUInt32(0);
                }
                return ((uint)(curObj["ForwardBufferMemory"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsFullDNSRegistrationEnabledNull {
            get {
                if ((curObj["FullDNSRegistrationEnabled"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Die Eigenschaft ""FullDNSRegistrationEnabled"" gibt an, ob die IP-Adressen für diese Verbindung unter dem vollständigen DNS-Namen des Computers registriert sind. Der vollständige DNS-Name des Computers wird in der Registerkarte Netzwerkidentifikation in der Systemsteuerung angezeigt. Nur Windows 2000.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool FullDNSRegistrationEnabled {
            get {
                if ((curObj["FullDNSRegistrationEnabled"] == null)) {
                    return Convert.ToBoolean(0);
                }
                return ((bool)(curObj["FullDNSRegistrationEnabled"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("\"GatewayCostMetric\" gibt eine ganze Zahl zwischen 1 und 9999 für die Kostenmetrik" +
            " zum Berechnen der schnellsten, zuverlässigsten und/oder der kostengünstigsten R" +
            "outen an. Dieses Argument entspricht \"DefaultIPGateway\". Nur Windows 2000.")]
        public ushort[] GatewayCostMetric {
            get {
                return ((ushort[])(curObj["GatewayCostMetric"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsIGMPLevelNull {
            get {
                if ((curObj["IGMPLevel"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Die Eigenschaft ""IGMPLevel"" gibt an, in wie weit das System IP-Multicast unterstützt und das IGM-Protokoll verwendet (Internet Group Management) verwendet. Auf Ebene 0 wird Multicast nicht unterstützt. Auf Ebene 1 werden nur IP-Multicastpakete gesendet. Auf Ebene 2 werden IP-Multicastpakete gesendet und das ICG-Protokoll wird zum Empfangen von Multicastpaketen verwendet. Standard: 2.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public IGMPLevelValues IGMPLevel {
            get {
                if ((curObj["IGMPLevel"] == null)) {
                    return ((IGMPLevelValues)(Convert.ToInt32(3)));
                }
                return ((IGMPLevelValues)(Convert.ToInt32(curObj["IGMPLevel"])));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsIndexNull {
            get {
                if ((curObj["Index"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"Index\" gibt die Indexnummer der Win32-Netzwerkadapterkonfigurati" +
            "on an. Die Indexnummer wird verwendet, wenn mehrere Konfigurationen verfügbar si" +
            "nd.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint Index {
            get {
                if ((curObj["Index"] == null)) {
                    return Convert.ToUInt32(0);
                }
                return ((uint)(curObj["Index"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsInterfaceIndexNull {
            get {
                if ((curObj["InterfaceIndex"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"InterfaceIndex\" enthält den Indexwert, der die lokale Schnittste" +
            "lle eindeutig identifiziert.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint InterfaceIndex {
            get {
                if ((curObj["InterfaceIndex"] == null)) {
                    return Convert.ToUInt32(0);
                }
                return ((uint)(curObj["InterfaceIndex"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"IPAddress\" enthält eine Liste der IP-Adressen, die dem aktuellen" +
            " Netzwerkadapter zugewiesen sind.\nBeispiel: 155.34.22.0")]
        public string[] IPAddress {
            get {
                return ((string[])(curObj["IPAddress"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsIPConnectionMetricNull {
            get {
                if ((curObj["IPConnectionMetric"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"""IPConnectionMetric"" gibt die Kosten für die Verwendung der konfigurierten Routen für den IP-Adapter an und ist der gewichtete Wert für die Routen in der IP-Routingtabelle. Wenn mehrere Routen zu einem Ziel in der IP-Routingtabelle vorhanden sind, wird die Route mit der niedrigsten Metrik verwendet. Der Standardwert ist 1. Nur Windows 2000.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint IPConnectionMetric {
            get {
                if ((curObj["IPConnectionMetric"] == null)) {
                    return Convert.ToUInt32(0);
                }
                return ((uint)(curObj["IPConnectionMetric"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsIPEnabledNull {
            get {
                if ((curObj["IPEnabled"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"IPEnabled\" gibt an, ob TCP/IP für diesen Netzwerkadapter gebunde" +
            "n und aktiviert ist.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool IPEnabled {
            get {
                if ((curObj["IPEnabled"] == null)) {
                    return Convert.ToBoolean(0);
                }
                return ((bool)(curObj["IPEnabled"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsIPFilterSecurityEnabledNull {
            get {
                if ((curObj["IPFilterSecurityEnabled"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Die Eigenschaft ""IPFilterSecurityEnabled"" gibt an, ob die IP-Portsicherheit global für alle IP-Netzwerkadapter aktiviert wird. Diese Eigenschaft wird mit folgenden Eigenschaften verwendet: IPSecPermitTCPPorts, IPSecPermitUDPPorts und IPSecPermitIPProtocols. TRUE gibt an, dass die IP-Portsicherheit aktiviert ist, und die Sicherheitswerte der einzelnen Netzwerkadapter aktiv sind. FALSE gibt an, dass die IP-Filtersicherheit für alle Netzwerkadapter deaktiviert ist und ermöglicht ungefilterten Port- und Protokollverkehr.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool IPFilterSecurityEnabled {
            get {
                if ((curObj["IPFilterSecurityEnabled"] == null)) {
                    return Convert.ToBoolean(0);
                }
                return ((bool)(curObj["IPFilterSecurityEnabled"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsIPPortSecurityEnabledNull {
            get {
                if ((curObj["IPPortSecurityEnabled"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"IPPortSecurityEnabled\" gibt an, ob die IP-Portsicherheit global " +
            "für alle IP-Netzwerkadapter aktiviert wurde. Diese Eigenschaft wurde durch \"IPFi" +
            "lterSecurityEnabled\" ersetzt.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool IPPortSecurityEnabled {
            get {
                if ((curObj["IPPortSecurityEnabled"] == null)) {
                    return Convert.ToBoolean(0);
                }
                return ((bool)(curObj["IPPortSecurityEnabled"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Die Eigenschaft ""IPSecPermitIPProtocols"" listet die Protokolle auf, die über IP ausgeführt werden können. Die Liste, die mit der Methode ""EnableIPSec"" definiert wird, ist leer oder enthält numerische Werte. Der Wert Null gibt an, dass alle Protokolle über Zugriffsrechte verfügen. Eine leere Zeichenfolge gibt an, dass kein Protokoll ausgeführt werden kann, wenn ""IPFilterSecurityEnabled"" TRUE ist.")]
        public string[] IPSecPermitIPProtocols {
            get {
                return ((string[])(curObj["IPSecPermitIPProtocols"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Die Eigenschaft ""IPSecPermitTCPPorts"" listet die Ports mit Zugriffsberechtigung für TCP auf. Die Protokolliste wird mit der Methode ""EnableIPSec"" definiert. Diese Liste ist leer oder enthält numerische Werte. Der Wert Null gibt an, dass alle Ports Zugriffsberechtigung haben. Eine leere Zeichenfolge gibt an, dass keinem Port die Zugriffsberechtigung erteilt wurde, wenn ""IPFilterSecurityEnabled"" TRUE ist.")]
        public string[] IPSecPermitTCPPorts {
            get {
                return ((string[])(curObj["IPSecPermitTCPPorts"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Die Eigenschaft ""IPSecPermitUDPPorts"" listet die Ports mit UDP-Zugriffsberechtigung auf. Die Protokolliste wird mit der Methode ""EnableIPSec"" definiert. Diese Liste ist leer oder enthält numerische Werte. Der Wert Null gibt an, dass alle Ports Zugriffsberechtigung haben. Eine leere Zeichenfolge gibt an, dass keinem Port die Zugriffsberechtigung erteilt wurde, wenn ""IPFilterSecurityEnabled"" TRUE ist.")]
        public string[] IPSecPermitUDPPorts {
            get {
                return ((string[])(curObj["IPSecPermitUDPPorts"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"IPSubnet\" enthält eine Liste der Subnetzmasken, die dem aktuelle" +
            "n Netzwerkadapter zugewiesen sind.\nBeispiel: 255.255.0")]
        public string[] IPSubnet {
            get {
                return ((string[])(curObj["IPSubnet"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsIPUseZeroBroadcastNull {
            get {
                if ((curObj["IPUseZeroBroadcast"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Die Eigenschaft ""IPUseZeroBroadcast"" gibt an, ob IP-Zeros-Broadcasts verwendet werden. Wenn dieser Parameter TRUE ist, verwendet IP Zeros-Broadcasts (0.0.0.0) und das System verwendet Ones-Broadcasts (255.255.255.255). Gewöhnlich verwenden Computersysteme Ones-Broadcasts, aber die von BSD-Implementation abgeleiteten verwenden Zeros-Broadcasts. Systeme, die nicht die gleichen Broadcasts verwenden, können nicht im gleichen Netzwerk ausgeführt werden. Standard: FALSE.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool IPUseZeroBroadcast {
            get {
                if ((curObj["IPUseZeroBroadcast"] == null)) {
                    return Convert.ToBoolean(0);
                }
                return ((bool)(curObj["IPUseZeroBroadcast"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"IPXAddress\" gibt die IPX-Adresse (Internetworking Packet Exchang" +
            "e) des Netzwerkadapters an. Die IPX-Adresse identifiziert einen Computer im Netz" +
            "werk.")]
        public string IPXAddress {
            get {
                return ((string)(curObj["IPXAddress"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsIPXEnabledNull {
            get {
                if ((curObj["IPXEnabled"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"IPXEnabled\" legt fest, ob das IPX-Protokoll (Internetwork Packet" +
            " Exchange) für diesen Adapter gebunden und aktiviert ist.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool IPXEnabled {
            get {
                if ((curObj["IPXEnabled"] == null)) {
                    return Convert.ToBoolean(0);
                }
                return ((bool)(curObj["IPXEnabled"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"IPXFrameType\" stellt ein Array mit Frametypkennungen dar. Die We" +
            "rte entsprechen den Elementen in \"IPXNetworkNumber\".")]
        public IPXFrameTypeValues[] IPXFrameType {
            get {
                Array arrEnumVals = ((Array)(curObj["IPXFrameType"]));
                IPXFrameTypeValues[] enumToRet = new IPXFrameTypeValues[arrEnumVals.Length];
                int counter = 0;
                for (counter = 0; (counter < arrEnumVals.Length); counter = (counter + 1)) {
                    enumToRet[counter] = ((IPXFrameTypeValues)(Convert.ToInt32(arrEnumVals.GetValue(counter))));
                }
                return enumToRet;
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsIPXMediaTypeNull {
            get {
                if ((curObj["IPXMediaType"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"IPXMediaType\" stellt eine IPX-Medientypkennung (Internetworking " +
            "Packet Exchange) dar.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public IPXMediaTypeValues IPXMediaType {
            get {
                if ((curObj["IPXMediaType"] == null)) {
                    return ((IPXMediaTypeValues)(Convert.ToInt32(0)));
                }
                return ((IPXMediaTypeValues)(Convert.ToInt32(curObj["IPXMediaType"])));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Die Eigenschaft ""IPXNetworkNumber"" gibt die Zeichen an, die eine Rahmen-/Netzwerkadapterkombination im Computersystem eindeutig kennzeichnen. Die NetWare Link (NWLink) IPX/SPX-kompatible Übertragung in Windows 2000 und Windows NT 4.0, oder höher, verwendet zwei unterschiedliche Netzwerknummerntypen. Diese externe Netzwerknummer muss für jedes Netzwerksegment eindeutig sein. Die Reihenfolge in dieser Liste entspricht den Elementen in der Eigenschaft ""IPXFrameType"".")]
        public string[] IPXNetworkNumber {
            get {
                return ((string[])(curObj["IPXNetworkNumber"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"IPXVirtualNetNumber\" identifiziert das Computersystem im Netzwer" +
            "k. Windows NT/2000 verwendet die virtuelle Netzwerknummer für internes Routing.")]
        public string IPXVirtualNetNumber {
            get {
                return ((string)(curObj["IPXVirtualNetNumber"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsKeepAliveIntervalNull {
            get {
                if ((curObj["KeepAliveInterval"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Die Eigenschaft ""KeepAliveInterval"" gibt das Zeitintervall zwischen Keep Alive-Übertragungen an, bis eine Rückmeldung empfangen wird. Wenn eine Rückmeldung empfangen wird, wird die Verzögerung bis zur nächsten Keep Alive-Übertragung erneut vom Wert in ""KeepAliveTime"" gesteuert. Die Verbindung wird abgebrochen, nachdem die von ""TcpMaxDataRetransmissions"" angegebene Anzahl der erneuten Übertragungen unbeantwortet bleiben. Standard: 1000. Gültiger Bereich: 1 - 0xFFFFFFFF.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint KeepAliveInterval {
            get {
                if ((curObj["KeepAliveInterval"] == null)) {
                    return Convert.ToUInt32(0);
                }
                return ((uint)(curObj["KeepAliveInterval"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsKeepAliveTimeNull {
            get {
                if ((curObj["KeepAliveTime"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Die Eigenschaft ""KeepAliveTime"" gibt an, wie oft TCP durch Senden eines Keep Alive-Pakets überprüft, ob eine Verbindung im Leerlauf noch verfügbar ist. Wenn das Remotesystem erreicht werden kann und aktiv ist, wird die Keep Alive-Übertragung bestätigt. Keep Alive-Pakete werden nicht standardmäßig gesendet. Diese Funktion kann von einer Anwendung aktiviert werden. Standard: 7.200.000 (zwei Stunden).")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint KeepAliveTime {
            get {
                if ((curObj["KeepAliveTime"] == null)) {
                    return Convert.ToUInt32(0);
                }
                return ((uint)(curObj["KeepAliveTime"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"MACAddress\" gibt die MAC-Adresse (Media Access Control) des Netz" +
            "werkadapters an. Eine MAC-Adresse wird vom Hersteller zugewiesen, um den Netzwer" +
            "kadapter eindeutig zu kennzeichnen.\nBeispiel: 00:80:C7:8F:6C:96")]
        public string MACAddress {
            get {
                return ((string)(curObj["MACAddress"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsMTUNull {
            get {
                if ((curObj["MTU"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Die Eigenschaft ""MTU"" überschreibt die Standard-MTU (Maximum Transmission Unit) für eine Netzwerkschnittstelle. Die MTU ist die maximale Paketgröße, einschließlich der Übertragungsheader), die über das Netzwerk übertragen werden. Das IP-Datagramm kann mehrere Pakete umfassen. Der Wertebereich umfasst die minimale Paketgröße (68) bis zur vom Netzwerk unterstützten MTU.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint MTU {
            get {
                if ((curObj["MTU"] == null)) {
                    return Convert.ToUInt32(0);
                }
                return ((uint)(curObj["MTU"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsNumForwardPacketsNull {
            get {
                if ((curObj["NumForwardPackets"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Die Eigenschaft ""NumForwardPackets"" gibt die Anzahl der IP-Paketheader für die Routerpaketwarteschlange an. Wenn alle Header verwendet werden, werden Pakete in der Warteschlange verworfen. Dieser Wert sollte mindestens dem Wert in der Eigenschaft ""ForwardBufferMemory"" dividiert durch die maximale IP-Datengröße des mit dem Router verbundenen Netzwerks entsprechen. Der Wert sollte nicht größer als der Wert in ""ForwardBufferMemory"" dividiert durch 256 sein, da für jedes Paket mindestens 256 Bytes Pufferspeicher verwendet wird. Die optimale Anzahl weitergeleiteter Paket hängt vom Typ des Netzwerkverkehrs ab und liegt zwischen diesen zwei Werten. Wenn der Router deaktiviert ist, wird dieser Parameter ignoriert und es werden keine Header zugeordnet. Standard: 50. Gültiger Bereich: 1 - 0xFFFFFFFE.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint NumForwardPackets {
            get {
                if ((curObj["NumForwardPackets"] == null)) {
                    return Convert.ToUInt32(0);
                }
                return ((uint)(curObj["NumForwardPackets"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPMTUBHDetectEnabledNull {
            get {
                if ((curObj["PMTUBHDetectEnabled"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Die Eigenschaft ""PMTUBHDetectEnabled"" gibt an, ob Black Hole-Router erkannt werden. Wenn der Wert dieses Parameters TRUE ist, versucht TCP Black Hole-Router während des Identifizierens des MTU-Pfades zu erkennen. Ein Black Hole-Router gibt keine ICMP-Meldungen bei nicht erreichbaren Zielen zurück, wenn ein IP-Datagramm fragmentiert werden muss und das Bit ""Keine Fragmentierung"" gesetzt ist. TCP ist vom Empfang dieser Meldungen abhängig, um einen MTU-Pfad zu identifizieren. Wenn diese Funktion aktiviert ist, sendet TCP Segmente ohne das Bit ""Keine Fragmentierung"", wenn mehrere Segmentübertragungen nicht bestätigt werden. Wenn das Segment bestätigt wird, wird die maximale Segmentgröße erhöht und das Bit ""Keine Fragmentierung"" wird für künftige Pakete gesetzt. Wenn die Black Hole-Erkennung aktiviert ist, wird die maximale Anzahl der erneuten Segmentübertragungen erhöht. Der Standardwert dieser Eigenschaft ist FALSE.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool PMTUBHDetectEnabled {
            get {
                if ((curObj["PMTUBHDetectEnabled"] == null)) {
                    return Convert.ToBoolean(0);
                }
                return ((bool)(curObj["PMTUBHDetectEnabled"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPMTUDiscoveryEnabledNull {
            get {
                if ((curObj["PMTUDiscoveryEnabled"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Die Eigenschaft ""PMTUDiscoveryEnabled"" gibt an, ob der MTU-Pfad (Maximum Transmission Unit) identifiziert wird. Wenn der Wert dieses Parameters True ist, versucht TCP die MTU (die größte Paketgröße) über den Pfad zum Remotehost zu identifizieren. Durch Identifizieren des MTU-Pfads und Beschränken der TCP-Segmente auf diese Größe, kann die Fragmentierung von Routern vermieden werden, die Netzwerke mit unterschiedlicher MTU verbinden. Die Fragmentierung wirkt sich ungünstig auf den TCP-Durchsatz und die Netzwerkauslastung aus. Wenn der Wert dieses Parameter False ist, wird eine MTU von 576 Bytes für alle Verbindungen außerhalb des Subnetzes verwendet. Standard: TRUE.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool PMTUDiscoveryEnabled {
            get {
                if ((curObj["PMTUDiscoveryEnabled"] == null)) {
                    return Convert.ToBoolean(0);
                }
                return ((bool)(curObj["PMTUDiscoveryEnabled"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"ServiceName\" gibt den Dienstnamen des Netzwerkadapters an. Diese" +
            "r Name ist gewöhnlich kürzer als der vollständige Produktname.\nBeispiel: Elnkii." +
            "")]
        public string ServiceName {
            get {
                return ((string)(curObj["ServiceName"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Kennung für das Objekt \"CIM_Setting\".")]
        public string SettingID {
            get {
                return ((string)(curObj["SettingID"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsTcpipNetbiosOptionsNull {
            get {
                if ((curObj["TcpipNetbiosOptions"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"TcpipNetbiosOptions\" gibt eine Bitmap für die möglichen NetBIOS-" +
            "Einstellungen über TCP/IP an. Nur Windows 2000.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public TcpipNetbiosOptionsValues TcpipNetbiosOptions {
            get {
                if ((curObj["TcpipNetbiosOptions"] == null)) {
                    return ((TcpipNetbiosOptionsValues)(Convert.ToInt32(3)));
                }
                return ((TcpipNetbiosOptionsValues)(Convert.ToInt32(curObj["TcpipNetbiosOptions"])));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsTcpMaxConnectRetransmissionsNull {
            get {
                if ((curObj["TcpMaxConnectRetransmissions"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Die Eigenschaft ""TcpMaxConnectRetransmissions"" gibt an, wie oft TCP versucht, eine Verbindungsanforderung auszuführen, bevor diese abgebrochen wird. Der ursprüngliche Zeitüberschreitungswert von 3 Sekunden wird bei jedem Versuch verdoppelt. Standard: 3. Gültiger Bereich: 0 - 0xFFFFFFFF.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint TcpMaxConnectRetransmissions {
            get {
                if ((curObj["TcpMaxConnectRetransmissions"] == null)) {
                    return Convert.ToUInt32(0);
                }
                return ((uint)(curObj["TcpMaxConnectRetransmissions"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsTcpMaxDataRetransmissionsNull {
            get {
                if ((curObj["TcpMaxDataRetransmissions"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Die Eigenschaft ""TcpMaxDataRetransmissions"" gibt an, wie oft TCP versucht, einzelne Datensegmente erneut zu übertragen, bevor die Verbindung abgebrochen wird. Der Zeitüberschreitungswert wird mit jeder erneuten Übertragung verdoppelt. Standard: 5. Gültiger Bereich: 0 - 0xFFFFFFFF.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint TcpMaxDataRetransmissions {
            get {
                if ((curObj["TcpMaxDataRetransmissions"] == null)) {
                    return Convert.ToUInt32(0);
                }
                return ((uint)(curObj["TcpMaxDataRetransmissions"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsTcpNumConnectionsNull {
            get {
                if ((curObj["TcpNumConnections"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"TcpNumConnections\" gibt die maximale Anzahl der gleichzeitig geö" +
            "ffneten Verbindungen an. Standard: 0xFFFFFE. Gültiger Bereich: 0 - 0xFFFFFE.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint TcpNumConnections {
            get {
                if ((curObj["TcpNumConnections"] == null)) {
                    return Convert.ToUInt32(0);
                }
                return ((uint)(curObj["TcpNumConnections"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsTcpUseRFC1122UrgentPointerNull {
            get {
                if ((curObj["TcpUseRFC1122UrgentPointer"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Die Eigenschaft ""TcpUseRFC1122UrgentPointer"" gibt an, ob TCP für dringende Daten die RFC 1122-Spezifikation oder den Modus von BSD (Berkeley Software Design) abgeleiteten Systemen verwendet. Die zwei Methoden interpretieren den Dringlichkeitszeiger unterschiedlich und können nicht zusammen verwendet werden. Windows 2000 und Windows NT 3.51, oder höher, verwenden den BSD-Modus. TRUE gibt an, dass dringende Daten im RFC 1122=Modus gesendet werden. Standard: FALSE.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool TcpUseRFC1122UrgentPointer {
            get {
                if ((curObj["TcpUseRFC1122UrgentPointer"] == null)) {
                    return Convert.ToBoolean(0);
                }
                return ((bool)(curObj["TcpUseRFC1122UrgentPointer"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsTcpWindowSizeNull {
            get {
                if ((curObj["TcpWindowSize"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Die Eigenschaft ""TcpWindowSize"" enthält die maximale TCP-Empfangsfenstergröße. Das Empfangsfenster gibt die Anzahl der Bytes an, die ohne Bestätigung übertragen werden können. Größere Empfangsfenster verbessern die Leistung über Netzwerke mit hoher Verzögerung oder Bandbreite. Das Empfangsfenster sollte ein gerades Vielfaches der maximalen TCP-Segmentgröße sein. Standard: Die TCP-Datengröße multipliziert mit 4 oder ein gerades Vielfaches der TCP-Datengröße, aufgerundet zu nächsten Vielfachen von 8192. Der Standard für Ethernetnetzwerke ist 8760. Gültiger Bereich: 0 - 65535.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ushort TcpWindowSize {
            get {
                if ((curObj["TcpWindowSize"] == null)) {
                    return Convert.ToUInt16(0);
                }
                return ((ushort)(curObj["TcpWindowSize"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsWINSEnableLMHostsLookupNull {
            get {
                if ((curObj["WINSEnableLMHostsLookup"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"WINSEnableLMHostsLookup\" gibt an, ob lokale Abfragedateien verwe" +
            "ndet werden. Abfragedateien enthalten die Zuordnungen von IP-Adressen zu Hostnam" +
            "en. Pfad: %SystemRoot%\\system32\\drivers\\usw.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool WINSEnableLMHostsLookup {
            get {
                if ((curObj["WINSEnableLMHostsLookup"] == null)) {
                    return Convert.ToBoolean(0);
                }
                return ((bool)(curObj["WINSEnableLMHostsLookup"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Die Eigenschaft ""WINSHostLookupFile"" enthält den Pfad zu einer WINS-Abfragedatei im lokalen  Diese Datei enthält die Zuordnung von IP-Adressen zu Hostnamen. Wenn die angegebene Datei gefunden wird, wird sie in den Ordner %SystemRoot%\system32\drivers\etc kopiert. Diese Eigenschaft ist nur gültig, wenn die Eigenschaft ""WINSEnableLMHostsLookup"" TRUE ist.")]
        public string WINSHostLookupFile {
            get {
                return ((string)(curObj["WINSHostLookupFile"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"WINSPrimaryServer\" gibt die IP-Adresse für den primären WINS-Ser" +
            "ver an. ")]
        public string WINSPrimaryServer {
            get {
                return ((string)(curObj["WINSPrimaryServer"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Die Eigenschaft ""WINSScopeID"" isoliert eine Gruppe von Computersystemen, die nur miteinander kommunizieren. Die Bereichskennung ist ein Zeichenfolgenwert, der an den NetBIOS-Namen angehängt und für alle NetBIOS-Transaktionen über TCP/IP verwendet wird. Computer, die mit der gleichen Bereichskennung konfiguriert sind, können mit diesem Computer kommunizieren. TCP/IP-Clients mit unterschiedlichen Bereichskennungen ignorieren Pakete von Computern mit dieser Bereichskennung. Diese Eigenschaft ist nur gültig, wenn die Methode ""EnableWINS"" erfolgreich ausgeführt wird.")]
        public string WINSScopeID {
            get {
                return ((string)(curObj["WINSScopeID"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Eigenschaft \"WINSSecondaryServer\" gibt die IP-Adresse für den sekundären WINS" +
            "-Server an. ")]
        public string WINSSecondaryServer {
            get {
                return ((string)(curObj["WINSSecondaryServer"]));
            }
        }
		#endregion

		#region internal Methods
		internal bool CheckIfProperClass(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions OptionsParam) {
            if (((path != null) 
                        && (string.Compare(path.ClassName, this.ManagementClassName, true, CultureInfo.InvariantCulture) == 0))) {
                return true;
            }
            else {
                return CheckIfProperClass(new ManagementObject(mgmtScope, path, OptionsParam));
            }
        }
        
        internal bool CheckIfProperClass(ManagementBaseObject theObj) {
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
        
        internal bool ShouldSerializeArpAlwaysSourceRoute() {
            if ((this.IsArpAlwaysSourceRouteNull == false)) {
                return true;
            }
            return false;
        }
        
        internal bool ShouldSerializeArpUseEtherSNAP() {
            if ((this.IsArpUseEtherSNAPNull == false)) {
                return true;
            }
            return false;
        }
        
        internal bool ShouldSerializeDeadGWDetectEnabled() {
            if ((this.IsDeadGWDetectEnabledNull == false)) {
                return true;
            }
            return false;
        }
        
        internal bool ShouldSerializeDefaultTOS() {
            if ((this.IsDefaultTOSNull == false)) {
                return true;
            }
            return false;
        }
        
        internal bool ShouldSerializeDefaultTTL() {
            if ((this.IsDefaultTTLNull == false)) {
                return true;
            }
            return false;
        }
        
        internal bool ShouldSerializeDHCPEnabled() {
            if ((this.IsDHCPEnabledNull == false)) {
                return true;
            }
            return false;
        }
        
        internal bool ShouldSerializeDHCPLeaseExpires() {
            if ((this.IsDHCPLeaseExpiresNull == false)) {
                return true;
            }
            return false;
        }
        
        internal bool ShouldSerializeDHCPLeaseObtained() {
            if ((this.IsDHCPLeaseObtainedNull == false)) {
                return true;
            }
            return false;
        }
        
        internal bool ShouldSerializeDNSEnabledForWINSResolution() {
            if ((this.IsDNSEnabledForWINSResolutionNull == false)) {
                return true;
            }
            return false;
        }
        
        internal bool ShouldSerializeDomainDNSRegistrationEnabled() {
            if ((this.IsDomainDNSRegistrationEnabledNull == false)) {
                return true;
            }
            return false;
        }
        
        internal bool ShouldSerializeForwardBufferMemory() {
            if ((this.IsForwardBufferMemoryNull == false)) {
                return true;
            }
            return false;
        }
        
        internal bool ShouldSerializeFullDNSRegistrationEnabled() {
            if ((this.IsFullDNSRegistrationEnabledNull == false)) {
                return true;
            }
            return false;
        }
        
        internal bool ShouldSerializeIGMPLevel() {
            if ((this.IsIGMPLevelNull == false)) {
                return true;
            }
            return false;
        }
        
        internal bool ShouldSerializeIndex() {
            if ((this.IsIndexNull == false)) {
                return true;
            }
            return false;
        }
        
        internal bool ShouldSerializeInterfaceIndex() {
            if ((this.IsInterfaceIndexNull == false)) {
                return true;
            }
            return false;
        }
        
        internal bool ShouldSerializeIPConnectionMetric() {
            if ((this.IsIPConnectionMetricNull == false)) {
                return true;
            }
            return false;
        }
        
        internal bool ShouldSerializeIPEnabled() {
            if ((this.IsIPEnabledNull == false)) {
                return true;
            }
            return false;
        }
        
        internal bool ShouldSerializeIPFilterSecurityEnabled() {
            if ((this.IsIPFilterSecurityEnabledNull == false)) {
                return true;
            }
            return false;
        }
        
        internal bool ShouldSerializeIPPortSecurityEnabled() {
            if ((this.IsIPPortSecurityEnabledNull == false)) {
                return true;
            }
            return false;
        }
        
        internal bool ShouldSerializeIPUseZeroBroadcast() {
            if ((this.IsIPUseZeroBroadcastNull == false)) {
                return true;
            }
            return false;
        }
        
        internal bool ShouldSerializeIPXEnabled() {
            if ((this.IsIPXEnabledNull == false)) {
                return true;
            }
            return false;
        }
        
        internal bool ShouldSerializeIPXMediaType() {
            if ((this.IsIPXMediaTypeNull == false)) {
                return true;
            }
            return false;
        }
        
        internal bool ShouldSerializeKeepAliveInterval() {
            if ((this.IsKeepAliveIntervalNull == false)) {
                return true;
            }
            return false;
        }
        
        internal bool ShouldSerializeKeepAliveTime() {
            if ((this.IsKeepAliveTimeNull == false)) {
                return true;
            }
            return false;
        }
        
        internal bool ShouldSerializeMTU() {
            if ((this.IsMTUNull == false)) {
                return true;
            }
            return false;
        }
        
        internal bool ShouldSerializeNumForwardPackets() {
            if ((this.IsNumForwardPacketsNull == false)) {
                return true;
            }
            return false;
        }
        
        internal bool ShouldSerializePMTUBHDetectEnabled() {
            if ((this.IsPMTUBHDetectEnabledNull == false)) {
                return true;
            }
            return false;
        }
        
        internal bool ShouldSerializePMTUDiscoveryEnabled() {
            if ((this.IsPMTUDiscoveryEnabledNull == false)) {
                return true;
            }
            return false;
        }
        
        internal bool ShouldSerializeTcpipNetbiosOptions() {
            if ((this.IsTcpipNetbiosOptionsNull == false)) {
                return true;
            }
            return false;
        }
        
        internal bool ShouldSerializeTcpMaxConnectRetransmissions() {
            if ((this.IsTcpMaxConnectRetransmissionsNull == false)) {
                return true;
            }
            return false;
        }
        
        internal bool ShouldSerializeTcpMaxDataRetransmissions() {
            if ((this.IsTcpMaxDataRetransmissionsNull == false)) {
                return true;
            }
            return false;
        }
        
        internal bool ShouldSerializeTcpNumConnections() {
            if ((this.IsTcpNumConnectionsNull == false)) {
                return true;
            }
            return false;
        }
        
        internal bool ShouldSerializeTcpUseRFC1122UrgentPointer() {
            if ((this.IsTcpUseRFC1122UrgentPointerNull == false)) {
                return true;
            }
            return false;
        }
        
        internal bool ShouldSerializeTcpWindowSize() {
            if ((this.IsTcpWindowSizeNull == false)) {
                return true;
            }
            return false;
        }
        
        internal bool ShouldSerializeWINSEnableLMHostsLookup() {
            if ((this.IsWINSEnableLMHostsLookupNull == false)) {
                return true;
            }
            return false;
        }
		#endregion

		#region Public Methods
		[Browsable(true)]
        public void CommitObject() {
            if ((isEmbedded == false)) {
                internalLateBoundObject.Put();
            }
        }
        
        [Browsable(true)]
        public void CommitObject(PutOptions putOptions) {
            if ((isEmbedded == false)) {
                internalLateBoundObject.Put(putOptions);
            }
        }
        
        internal void Initialize() {
            AutoCommitProp = true;
            isEmbedded = false;
        }
        
        internal void InitializeObject(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) {
            Initialize();
            if ((path != null)) {
                if ((CheckIfProperClass(mgmtScope, path, getOptions) != true)) {
                    throw new ArgumentException("Der Klassenname stimmt nicht überein.");
                }
            }
            internalLateBoundObject = new ManagementObject(mgmtScope, path, getOptions);
            internalSystemProperties = new ManagementSystemProperties(internalLateBoundObject);
            curObj = internalLateBoundObject;
        }
        
        [Browsable(true)]
        public void Delete() {
            internalLateBoundObject.Delete();
        }
        
        public uint DisableIPSec() {
            if ((isEmbedded == false)) {
                ManagementBaseObject inParams = null;
                ManagementBaseObject outParams = internalLateBoundObject.InvokeMethod("DisableIPSec", inParams, null);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return Convert.ToUInt32(0);
            }
		}

		public uint EnableIPSec(string[] IPSecPermitIPProtocols, string[] IPSecPermitTCPPorts, string[] IPSecPermitUDPPorts)
		{
			if ((isEmbedded == false))
			{
				ManagementBaseObject inParams = null;
				inParams = internalLateBoundObject.GetMethodParameters("EnableIPSec");
				inParams["IPSecPermitIPProtocols"] = ((string[])(IPSecPermitIPProtocols));
				inParams["IPSecPermitTCPPorts"] = ((string[])(IPSecPermitTCPPorts));
				inParams["IPSecPermitUDPPorts"] = ((string[])(IPSecPermitUDPPorts));
				ManagementBaseObject outParams = internalLateBoundObject.InvokeMethod("EnableIPSec", inParams, null);
				return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
			}
			else
			{
				return Convert.ToUInt32(0);
			}
		}

		/// <summary>
		/// <para>Enables the configuration of the ip addresses and subnet mask through an DHCP server.</para>
		/// <para>This is the opposite of <see cref="EnableStatic(IPAddress[], IPAddress[])"/></para>
		/// <para>This method requires admin privileges (<see cref="Main.RequireAdministrator"/>)</para>
		/// </summary>
		/// <param name="ni">The <see cref="NetworkInterface"/> that shall be modified</param>
		/// <returns><para>An WMI Error Constant or some other error type</para>
		/// <para>0x80041003: Current user does not have permission to perform the action.</para>
		/// <para>see http://msdn.microsoft.com/en-us/library/aa394559%28v=vs.85%29.aspx for 0x8*******</para>
		/// <para>or net helpmsg %ret% otherwise</para>
		/// </returns>
		/// <remarks>http://msdn.microsoft.com/en-us/library/aa390378%28v=vs.85%29.aspx</remarks>
		public uint EnableDHCP()
		{
			if ((isEmbedded == false))
			{
				ManagementBaseObject inParams = null;
				ManagementBaseObject outParams = internalLateBoundObject.InvokeMethod("EnableDHCP", inParams, null);
				return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
			}
			else
			{
				return Convert.ToUInt32(0);
			}
		}

		/// <summary>
		/// <para>Sets a new IP Address and its Subnet mask of the local machine</para>
		/// <para>This method requires admin privileges (<see cref="Main.RequireAdministrator"/>)</para>
		/// <para>This is the oppsite of <see cref="EnableDHCP"/></para>
		/// </summary>
		/// <param name="IPAddress">The IP Address</param>
		/// <returns><para>An WMI Error Constant or some other error type</para>
		/// <para>0x80041003: Current user does not have permission to perform the action.</para>
		/// <para>see http://msdn.microsoft.com/en-us/library/aa394559%28v=vs.85%29.aspx for 0x8*******</para>
		/// <para>or net helpmsg %ret% otherwise</para>
		/// </returns>
		/// <remarks>http://msdn.microsoft.com/en-us/library/aa390383%28v=vs.85%29.aspx</remarks>
		public uint EnableStatic(IPAddress IPAddress)
		{
			return this.EnableStatic(new string[] { IPAddress.ToString() }, new string[] { IPAddress.GetSubnet().ToString() });
		}
		/// <summary>
		/// <para>Sets a new IP Address and its Subnet mask of the local machine</para>
		/// <para>This method requires admin privileges (<see cref="Main.RequireAdministrator"/>)</para>
		/// <para>This is the oppsite of <see cref="EnableDHCP"/></para>
		/// </summary>
		/// <param name="IPAddress">The IP Address</param>
		/// <param name="SubnetMask">The Subnetmask that shall be set</param>
		/// <returns><para>An WMI Error Constant or some other error type</para>
		/// <para>0x80041003: Current user does not have permission to perform the action.</para>
		/// <para>see http://msdn.microsoft.com/en-us/library/aa394559%28v=vs.85%29.aspx for 0x8*******</para>
		/// <para>or net helpmsg %ret% otherwise</para>
		/// </returns>
		/// <remarks>http://msdn.microsoft.com/en-us/library/aa390383%28v=vs.85%29.aspx</remarks>
		public uint EnableStatic(IPAddress IPAddress, IPAddress SubnetMask)
		{
			return this.EnableStatic(new string[] { IPAddress.ToString() }, new string[] { SubnetMask.ToString() });
		}
		/// <summary>
		/// <para>Sets a new IP Address and its Subnet mask of the local machine</para>
		/// <para>This method requires admin privileges (<see cref="Main.RequireAdministrator"/>)</para>
		/// <para>This is the oppsite of <see cref="EnableDHCP"/></para>
		/// </summary>
		/// <param name="IPAddresses">The IP Addresses</param>
		/// <param name="SubnetMasks">The Subnet masks</param>
		/// <returns><para>An WMI Error Constant or some other error type</para>
		/// <para>0x80041003: Current user does not have permission to perform the action.</para>
		/// <para>see http://msdn.microsoft.com/en-us/library/aa394559%28v=vs.85%29.aspx for 0x8*******</para>
		/// <para>or net helpmsg %ret% otherwise</para>
		/// </returns>
		/// <remarks>http://msdn.microsoft.com/en-us/library/aa390383%28v=vs.85%29.aspx</remarks>
		public uint EnableStatic(IPAddress[] IPAddresses, IPAddress[] SubnetMasks)
		{
			return EnableStatic(IPAddresses.ToStrings(), SubnetMasks.ToStrings());
		}
		/// <summary>
		/// <para>Sets a new IP Address and its Submask of the local machine</para>
		/// <para>This method requires admin privileges (<see cref="Main.RequireAdministrator"/>)</para>
		/// <para>This is the oppsite of <see cref="EnableDHCP"/></para>
		/// </summary>
		/// <param name="IPAddress">The ip address that shall be set</param>
		/// <param name="SubnetMask">The subnet mask that shall be set</param>
		/// <returns><para>An WMI Error Constant or some other error type</para>
		/// <para>0x80041003: Current user does not have permission to perform the action.</para>
		/// <para>see http://msdn.microsoft.com/en-us/library/aa394559%28v=vs.85%29.aspx for 0x8*******</para>
		/// <para>or net helpmsg %ret% otherwise</para>
		/// </returns>
		/// <remarks>http://msdn.microsoft.com/en-us/library/aa390383%28v=vs.85%29.aspx</remarks>
        internal uint EnableStatic(string[] IPAddress, string[] SubnetMask) {
            if ((isEmbedded == false)) {
                ManagementBaseObject inParams = null;
                inParams = internalLateBoundObject.GetMethodParameters("EnableStatic");
                inParams["IPAddress"] = ((string[])(IPAddress));
                inParams["SubnetMask"] = ((string[])(SubnetMask));
                ManagementBaseObject outParams = internalLateBoundObject.InvokeMethod("EnableStatic", inParams, null);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return Convert.ToUInt32(0);
            }
        }
        
        public uint ReleaseDHCPLease() {
            if ((isEmbedded == false)) {
                ManagementBaseObject inParams = null;
                ManagementBaseObject outParams = internalLateBoundObject.InvokeMethod("ReleaseDHCPLease", inParams, null);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return Convert.ToUInt32(0);
            }
        }
        
        public uint RenewDHCPLease() {
            if ((isEmbedded == false)) {
                ManagementBaseObject inParams = null;
                ManagementBaseObject outParams = internalLateBoundObject.InvokeMethod("RenewDHCPLease", inParams, null);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return Convert.ToUInt32(0);
            }
        }
        
        public uint SetDNSDomain(string DNSDomain) {
            if ((isEmbedded == false)) {
                ManagementBaseObject inParams = null;
                inParams = internalLateBoundObject.GetMethodParameters("SetDNSDomain");
                inParams["DNSDomain"] = ((string)(DNSDomain));
                ManagementBaseObject outParams = internalLateBoundObject.InvokeMethod("SetDNSDomain", inParams, null);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return Convert.ToUInt32(0);
            }
        }

		/// <summary>
		/// <para>Resets the DNS server search order</para>
		/// <para>This method requires admin privileges (<see cref="Main.RequireAdministrator"/>)</para>
		/// </summary>
		/// <returns><para>An WMI Error Constant or some other error type</para>
		/// <para>0x80041003: Current user does not have permission to perform the action.</para>
		/// <para>see http://msdn.microsoft.com/en-us/library/aa394559%28v=vs.85%29.aspx for 0x8*******</para>
		/// <para>or net helpmsg %ret% otherwise</para>
		/// </returns>
		/// <remarks>http://msdn.microsoft.com/en-us/library/aa393295%28v=vs.85%29.aspx</remarks>
		public uint ResetDNSServerSearchOrder()
		{
			return SetDNSServerSearchOrder(new string[] { null });
		}
		/// <summary>
		/// <para>Sets the specified <paramref name="DNS"/> server as default server for DNS requests</para>
		/// <para>This method requires admin privileges (<see cref="Main.RequireAdministrator"/>)</para>
		/// </summary>
		/// <param name="DNSServer">The DNS server address</param>
		/// <returns><para>An WMI Error Constant or some other error type</para>
		/// <para>0x80041003: Current user does not have permission to perform the action.</para>
		/// <para>see http://msdn.microsoft.com/en-us/library/aa394559%28v=vs.85%29.aspx for 0x8*******</para>
		/// <para>or net helpmsg %ret% otherwise</para>
		/// </returns>
		/// <remarks>http://msdn.microsoft.com/en-us/library/aa393295%28v=vs.85%29.aspx</remarks>
		public uint SetDNSServerSearchOrder(IPAddress DNSServer)
		{
			return SetDNSServerSearchOrder(new string[] { DNSServer.ToString() });
		}
		/// <summary>
		/// <para>Sets the specified <paramref name="DNS"/> servers as default servers for DNS requests</para>
		/// <para>This method requires admin privileges (<see cref="Main.RequireAdministrator"/>)</para>
		/// </summary>
		/// <param name="DNSServers">The DNS server addresses in descending? priority</param>
		/// <returns><para>An WMI Error Constant or some other error type</para>
		/// <para>0x80041003: Current user does not have permission to perform the action.</para>
		/// <para>see http://msdn.microsoft.com/en-us/library/aa394559%28v=vs.85%29.aspx for 0x8*******</para>
		/// <para>or net helpmsg %ret% otherwise</para>
		/// </returns>
		/// <remarks>http://msdn.microsoft.com/en-us/library/aa393295%28v=vs.85%29.aspx</remarks>
		public uint SetDNSServerSearchOrder(IPAddress[] DNSServers)
		{
			return SetDNSServerSearchOrder(DNSServers.ToStrings());
		}
		/// <summary>
		/// <para>Sets the specified <paramref name="DNS"/> servers as default servers for DNS requests</para>
		/// <para>This method requires admin privileges (<see cref="Main.RequireAdministrator"/>)</para>
		/// </summary>
		/// <param name="DNSServers">The DNS server addresses in descending? priority</param>
		/// <returns><para>An WMI Error Constant or some other error type</para>
		/// <para>0x80041003: Current user does not have permission to perform the action.</para>
		/// <para>see http://msdn.microsoft.com/en-us/library/aa394559%28v=vs.85%29.aspx for 0x8*******</para>
		/// <para>or net helpmsg %ret% otherwise</para>
		/// </returns>
		/// <remarks>http://msdn.microsoft.com/en-us/library/aa393295%28v=vs.85%29.aspx</remarks>
        internal uint SetDNSServerSearchOrder(string[] DNSServers) {
            if ((isEmbedded == false)) {
                ManagementBaseObject inParams = null;
                inParams = internalLateBoundObject.GetMethodParameters("SetDNSServerSearchOrder");
                inParams["DNSServerSearchOrder"] = ((string[])(DNSServerSearchOrder));
                ManagementBaseObject outParams = internalLateBoundObject.InvokeMethod("SetDNSServerSearchOrder", inParams, null);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return Convert.ToUInt32(0);
            }
        }
        
        public uint SetDynamicDNSRegistration(bool DomainDNSRegistrationEnabled, bool FullDNSRegistrationEnabled) {
            if ((isEmbedded == false)) {
                ManagementBaseObject inParams = null;
                inParams = internalLateBoundObject.GetMethodParameters("SetDynamicDNSRegistration");
                inParams["DomainDNSRegistrationEnabled"] = ((bool)(DomainDNSRegistrationEnabled));
                inParams["FullDNSRegistrationEnabled"] = ((bool)(FullDNSRegistrationEnabled));
                ManagementBaseObject outParams = internalLateBoundObject.InvokeMethod("SetDynamicDNSRegistration", inParams, null);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return Convert.ToUInt32(0);
            }
        }

		/// <summary>
		/// <para>Set the <see cref="IPAddress"/> of the gateway</para>
		/// <para>This method requires admin privileges (<see cref="Main.RequireAdministrator"/>)</para>
		/// </summary>
		/// <param name="gateway">The <see cref="IPAddress"/> of the gateway that shall be set as default</param>
		/// <returns><para>An WMI Error Constant or some other error type</para>
		/// <para>0x80041003: Current user does not have permission to perform the action.</para>
		/// <para>see http://msdn.microsoft.com/en-us/library/aa394559%28v=vs.85%29.aspx for 0x8*******</para>
		/// <para>or net helpmsg %ret% otherwise</para>
		/// </returns>
		/// <remarks>http://msdn.microsoft.com/en-us/library/aa393301%28v=vs.85%29.aspx</remarks>
		public uint SetGateway(IPAddress DefaultIPGateway)
		{
			return SetGateways(new string[] { DefaultIPGateway.ToString() }, new ushort[] { 1 });
		}
		/// <summary>
		/// <para>Sets the <see cref="IPAddress"/> of up to 5 Gateways</para>
		/// <para>This method requires admin privileges (<see cref="Main.RequireAdministrator"/>)</para>
		/// </summary>
		/// <param name="gateways">The <see cref="IPAddress"/> of the gateway that shall be set. No more than 5 IPAdresses are allowed!</param>
		/// <returns><para>An WMI Error Constant or some other error type</para>
		/// <para>0x80041003: Current user does not have permission to perform the action.</para>
		/// <para>see http://msdn.microsoft.com/en-us/library/aa394559%28v=vs.85%29.aspx for 0x8*******</para>
		/// <para>or net helpmsg %ret% otherwise</para>
		/// </returns>
		/// <remarks>http://msdn.microsoft.com/en-us/library/aa393301%28v=vs.85%29.aspx</remarks>
		public uint SetGateways(IPAddress[] DefaultIPGateway)
		{
			ushort[] costs = new ushort[DefaultIPGateway.Length];
			for (byte i = 0; i < DefaultIPGateway.Length; i++)
				costs[i] = 1;
			return SetGateways(DefaultIPGateway.ToStrings(), costs);
		}
		/// <summary>
		/// <para>Sets the <see cref="IPAddress"/> of up to 5 Gateways</para>
		/// <para>This method requires admin privileges (<see cref="Main.RequireAdministrator"/>)</para>
		/// </summary>
		/// <param name="gateways">The <see cref="IPAddress"/> of the gateway that shall be set. No more than 5 IPAdresses are allowed!</param>
		/// <param name="GatewayCostMetric">The cost metric of the gateway used for route calculation</param>
		/// <returns><para>An WMI Error Constant or some other error type</para>
		/// <para>0x80041003: Current user does not have permission to perform the action.</para>
		/// <para>see http://msdn.microsoft.com/en-us/library/aa394559%28v=vs.85%29.aspx for 0x8*******</para>
		/// <para>or net helpmsg %ret% otherwise</para>
		/// </returns>
		/// <remarks>http://msdn.microsoft.com/en-us/library/aa393301%28v=vs.85%29.aspx</remarks>
		public uint SetGateways(IPAddress[] DefaultIPGateway, ushort[] GatewayCostMetric)
		{
			return SetGateways(DefaultIPGateway.ToStrings(), GatewayCostMetric);
		}
		/// <summary>
		/// <para>Sets the <see cref="IPAddress"/> of up to 5 Gateways</para>
		/// <para>This method requires admin privileges (<see cref="Main.RequireAdministrator"/>)</para>
		/// </summary>
		/// <param name="gateways">The <see cref="IPAddress"/> of the gateway that shall be set. No more than 5 IPAdresses are allowed!</param>
		/// <param name="GatewayCostMetric">The cost metric of the gateway used for route calculation</param>
		/// <returns><para>An WMI Error Constant or some other error type</para>
		/// <para>0x80041003: Current user does not have permission to perform the action.</para>
		/// <para>see http://msdn.microsoft.com/en-us/library/aa394559%28v=vs.85%29.aspx for 0x8*******</para>
		/// <para>or net helpmsg %ret% otherwise</para>
		/// </returns>
		/// <remarks>http://msdn.microsoft.com/en-us/library/aa393301%28v=vs.85%29.aspx</remarks>
        internal uint SetGateways(string[] DefaultIPGateway, ushort[] GatewayCostMetric)
		{
			if (DefaultIPGateway.Length > 5)
				return 69;
            if ((isEmbedded == false)) {
                ManagementBaseObject inParams = null;
                inParams = internalLateBoundObject.GetMethodParameters("SetGateways");
                inParams["DefaultIPGateway"] = ((string[])(DefaultIPGateway));
                inParams["GatewayCostMetric"] = ((ushort[])(GatewayCostMetric));
                ManagementBaseObject outParams = internalLateBoundObject.InvokeMethod("SetGateways", inParams, null);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return Convert.ToUInt32(0);
            }
        }
        
        public uint SetIPConnectionMetric(uint IPConnectionMetric) {
            if ((isEmbedded == false)) {
                ManagementBaseObject inParams = null;
                inParams = internalLateBoundObject.GetMethodParameters("SetIPConnectionMetric");
                inParams["IPConnectionMetric"] = ((uint)(IPConnectionMetric));
                ManagementBaseObject outParams = internalLateBoundObject.InvokeMethod("SetIPConnectionMetric", inParams, null);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return Convert.ToUInt32(0);
            }
        }
        
        public uint SetIPXFrameTypeNetworkPairs(uint[] IPXFrameType, string[] IPXNetworkNumber) {
            if ((isEmbedded == false)) {
                ManagementBaseObject inParams = null;
                inParams = internalLateBoundObject.GetMethodParameters("SetIPXFrameTypeNetworkPairs");
                inParams["IPXFrameType"] = ((uint[])(IPXFrameType));
                inParams["IPXNetworkNumber"] = ((string[])(IPXNetworkNumber));
                ManagementBaseObject outParams = internalLateBoundObject.InvokeMethod("SetIPXFrameTypeNetworkPairs", inParams, null);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return Convert.ToUInt32(0);
            }
        }
        
        public uint SetTcpipNetbios(uint TcpipNetbiosOptions) {
            if ((isEmbedded == false)) {
                ManagementBaseObject inParams = null;
                inParams = internalLateBoundObject.GetMethodParameters("SetTcpipNetbios");
                inParams["TcpipNetbiosOptions"] = ((uint)(TcpipNetbiosOptions));
                ManagementBaseObject outParams = internalLateBoundObject.InvokeMethod("SetTcpipNetbios", inParams, null);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return Convert.ToUInt32(0);
            }
        }

		/// <summary>
		/// <para>Set's WINS (Windows Internet Name Service = DNS in LANs) servers of the local machine</para>
		/// <para>This method requires admin privileges (<see cref="Main.RequireAdministrator"/>)</para>
		/// </summary>
		/// <param name="PrimaryServer">Primary WINS server address</param>
		/// <param name="SecondaryServer">Secondary WINS server address</param>
		public uint SetWINSServer(IPAddress PrimaryServer, IPAddress SecondaryServer)
		{
			return SetWINSServer(PrimaryServer.ToString(), SecondaryServer.ToString());
		}
		/// <summary>
		/// <para>Set's WINS (Windows Internet Name Service = DNS in LANs) servers of the local machine</para>
		/// <para>This method requires admin privileges (<see cref="Main.RequireAdministrator"/>)</para>
		/// </summary>
		/// <param name="PrimaryServer">Primary WINS server address</param>
		/// <param name="SecondaryServer">Secondary WINS server address</param>
		internal uint SetWINSServer(string PrimaryServer, string SecondaryServer)
		{
            if ((isEmbedded == false)) {
                ManagementBaseObject inParams = null;
                inParams = internalLateBoundObject.GetMethodParameters("SetWINSServer");
                inParams["WINSPrimaryServer"] = ((string)(PrimaryServer));
                inParams["WINSSecondaryServer"] = ((string)(SecondaryServer));
                ManagementBaseObject outParams = internalLateBoundObject.InvokeMethod("SetWINSServer", inParams, null);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return Convert.ToUInt32(0);
            }
        }
		#endregion

		#region internal static Methods
		// Konvertiert einen Zeitpunkt (Datum und Uhrzeit) im DMTF-Format in ein DateTime-Objekt.
		internal static DateTime ToDateTime(string dmtfDate)
		{
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
			if ((dmtf == null))
			{
				throw new ArgumentOutOfRangeException();
			}
			if ((dmtf.Length == 0))
			{
				throw new ArgumentOutOfRangeException();
			}
			if ((dmtf.Length != 25))
			{
				throw new ArgumentOutOfRangeException();
			}
			try
			{
				tempString = dmtf.Substring(0, 4);
				if (("****" != tempString))
				{
					year = int.Parse(tempString);
				}
				tempString = dmtf.Substring(4, 2);
				if (("**" != tempString))
				{
					month = int.Parse(tempString);
				}
				tempString = dmtf.Substring(6, 2);
				if (("**" != tempString))
				{
					day = int.Parse(tempString);
				}
				tempString = dmtf.Substring(8, 2);
				if (("**" != tempString))
				{
					hour = int.Parse(tempString);
				}
				tempString = dmtf.Substring(10, 2);
				if (("**" != tempString))
				{
					minute = int.Parse(tempString);
				}
				tempString = dmtf.Substring(12, 2);
				if (("**" != tempString))
				{
					second = int.Parse(tempString);
				}
				tempString = dmtf.Substring(15, 6);
				if (("******" != tempString))
				{
					ticks = (long.Parse(tempString) * ((long)((TimeSpan.TicksPerMillisecond / 1000))));
				}
				if (((((((((year < 0)
							|| (month < 0))
							|| (day < 0))
							|| (hour < 0))
							|| (minute < 0))
							|| (minute < 0))
							|| (second < 0))
							|| (ticks < 0)))
				{
					throw new ArgumentOutOfRangeException();
				}
			}
			catch (Exception e)
			{
				throw new ArgumentOutOfRangeException(null, e.Message);
			}
			datetime = new DateTime(year, month, day, hour, minute, second, 0);
			datetime = datetime.AddTicks(ticks);
			TimeSpan tickOffset = TimeZone.CurrentTimeZone.GetUtcOffset(datetime);
			int UTCOffset = 0;
			int OffsetToBeAdjusted = 0;
			long OffsetMins = ((long)((tickOffset.Ticks / TimeSpan.TicksPerMinute)));
			tempString = dmtf.Substring(22, 3);
			if ((tempString != "******"))
			{
				tempString = dmtf.Substring(21, 4);
				try
				{
					UTCOffset = int.Parse(tempString);
				}
				catch (Exception e)
				{
					throw new ArgumentOutOfRangeException(null, e.Message);
				}
				OffsetToBeAdjusted = ((int)((OffsetMins - UTCOffset)));
				datetime = datetime.AddMinutes(((double)(OffsetToBeAdjusted)));
			}
			return datetime;
		}

		// Konvertiert ein DateTime-Objekt in das DMTF-Format für Datum und Uhrzeit.
		internal static string ToDmtfDateTime(DateTime date)
		{
			string utcString = string.Empty;
			TimeSpan tickOffset = TimeZone.CurrentTimeZone.GetUtcOffset(date);
			long OffsetMins = ((long)((tickOffset.Ticks / TimeSpan.TicksPerMinute)));
			if ((Math.Abs(OffsetMins) > 999))
			{
				date = date.ToUniversalTime();
				utcString = "+000";
			}
			else
			{
				if ((tickOffset.Ticks >= 0))
				{
					utcString = string.Concat("+", ((long)((tickOffset.Ticks / TimeSpan.TicksPerMinute))).ToString().PadLeft(3, '0'));
				}
				else
				{
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
			if ((strMicrosec.Length > 6))
			{
				strMicrosec = strMicrosec.Substring(0, 6);
			}
			dmtfDateTime = string.Concat(dmtfDateTime, strMicrosec.PadLeft(6, '0'));
			dmtfDateTime = string.Concat(dmtfDateTime, utcString);
			return dmtfDateTime;
		}

		internal static string ConstructPath(uint keyIndex)
		{
			string strPath = "root\\CimV2:Win32_NetworkAdapterConfiguration";
			strPath = string.Concat(strPath, string.Concat(".Index=", ((uint)(keyIndex)).ToString()));
			return strPath;
		}
		#endregion

		#region Public static Methods
		// Unterschiedliche Überladungen von 'GetInstances()' unterstützen beim Aufzählen von Instanzen der WMI-Klasse.
		public static NetworkAdapterConfigurationCollection GetInstances()
		{
			return GetInstances(null, null, null);
		}

		public static NetworkAdapterConfigurationCollection GetInstances(string condition)
		{
			return GetInstances(null, condition, null);
		}

		public static NetworkAdapterConfigurationCollection GetInstances(string[] selectedProperties)
		{
			return GetInstances(null, null, selectedProperties);
		}

		public static NetworkAdapterConfigurationCollection GetInstances(string condition, string[] selectedProperties)
		{
			return GetInstances(null, condition, selectedProperties);
		}

		public static NetworkAdapterConfigurationCollection GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions)
		{
			if ((mgmtScope == null))
			{
				if ((statMgmtScope == null))
				{
					mgmtScope = new ManagementScope();
					mgmtScope.Path.NamespacePath = "root\\CimV2";
				}
				else
				{
					mgmtScope = statMgmtScope;
				}
			}
			ManagementPath pathObj = new ManagementPath();
			pathObj.ClassName = "Win32_NetworkAdapterConfiguration";
			pathObj.NamespacePath = "root\\CimV2";
			ManagementClass clsObject = new ManagementClass(mgmtScope, pathObj, null);
			if ((enumOptions == null))
			{
				enumOptions = new EnumerationOptions();
				enumOptions.EnsureLocatable = true;
			}
			return new NetworkAdapterConfigurationCollection(clsObject.GetInstances(enumOptions));
		}

		public static NetworkAdapterConfigurationCollection GetInstances(ManagementScope mgmtScope, string condition)
		{
			return GetInstances(mgmtScope, condition, null);
		}

		public static NetworkAdapterConfigurationCollection GetInstances(ManagementScope mgmtScope, string[] selectedProperties)
		{
			return GetInstances(mgmtScope, null, selectedProperties);
		}

		public static NetworkAdapterConfigurationCollection GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties)
		{
			if ((mgmtScope == null))
			{
				if ((statMgmtScope == null))
				{
					mgmtScope = new ManagementScope();
					mgmtScope.Path.NamespacePath = "root\\CimV2";
				}
				else
				{
					mgmtScope = statMgmtScope;
				}
			}
			ManagementObjectSearcher ObjectSearcher = new ManagementObjectSearcher(mgmtScope, new SelectQuery("Win32_NetworkAdapterConfiguration", condition, selectedProperties));
			EnumerationOptions enumOptions = new EnumerationOptions();
			enumOptions.EnsureLocatable = true;
			ObjectSearcher.Options = enumOptions;
			return new NetworkAdapterConfigurationCollection(ObjectSearcher.Get());
		}

		[Browsable(true)]
		public static NetworkAdapterConfiguration CreateInstance()
		{
			ManagementScope mgmtScope = null;
			if ((statMgmtScope == null))
			{
				mgmtScope = new ManagementScope();
				mgmtScope.Path.NamespacePath = CreatedWmiNamespace;
			}
			else
			{
				mgmtScope = statMgmtScope;
			}
			ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
			ManagementClass tmpMgmtClass = new ManagementClass(mgmtScope, mgmtPath, null);
			return new NetworkAdapterConfiguration(tmpMgmtClass.CreateInstance());
		}

		public static uint EnableIPFilterSec(bool IPFilterSecurityEnabled)
		{
			bool IsMethodStatic = true;
			if ((IsMethodStatic == true))
			{
				ManagementBaseObject inParams = null;
				ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
				ManagementClass classObj = new ManagementClass(statMgmtScope, mgmtPath, null);
				inParams = classObj.GetMethodParameters("EnableIPFilterSec");
				inParams["IPFilterSecurityEnabled"] = ((bool)(IPFilterSecurityEnabled));
				ManagementBaseObject outParams = classObj.InvokeMethod("EnableIPFilterSec", inParams, null);
				return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
			}
			else
			{
				return Convert.ToUInt32(0);
			}
		}

		public static uint EnableDNS(string DNSDomain, string[] DNSDomainSuffixSearchOrder, string DNSHostName, string[] DNSServerSearchOrder)
		{
			bool IsMethodStatic = true;
			if ((IsMethodStatic == true))
			{
				ManagementBaseObject inParams = null;
				ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
				ManagementClass classObj = new ManagementClass(statMgmtScope, mgmtPath, null);
				inParams = classObj.GetMethodParameters("EnableDNS");
				inParams["DNSDomain"] = ((string)(DNSDomain));
				inParams["DNSDomainSuffixSearchOrder"] = ((string[])(DNSDomainSuffixSearchOrder));
				inParams["DNSHostName"] = ((string)(DNSHostName));
				inParams["DNSServerSearchOrder"] = ((string[])(DNSServerSearchOrder));
				ManagementBaseObject outParams = classObj.InvokeMethod("EnableDNS", inParams, null);
				return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
			}
			else
			{
				return Convert.ToUInt32(0);
			}
		}

		public static uint EnableWINS(bool DNSEnabledForWINSResolution, bool WINSEnableLMHostsLookup, string WINSHostLookupFile, string WINSScopeID)
		{
			bool IsMethodStatic = true;
			if ((IsMethodStatic == true))
			{
				ManagementBaseObject inParams = null;
				ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
				ManagementClass classObj = new ManagementClass(statMgmtScope, mgmtPath, null);
				inParams = classObj.GetMethodParameters("EnableWINS");
				inParams["DNSEnabledForWINSResolution"] = ((bool)(DNSEnabledForWINSResolution));
				inParams["WINSEnableLMHostsLookup"] = ((bool)(WINSEnableLMHostsLookup));
				inParams["WINSHostLookupFile"] = ((string)(WINSHostLookupFile));
				inParams["WINSScopeID"] = ((string)(WINSScopeID));
				ManagementBaseObject outParams = classObj.InvokeMethod("EnableWINS", inParams, null);
				return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
			}
			else
			{
				return Convert.ToUInt32(0);
			}
		}

		public static uint ReleaseDHCPLeaseAll()
		{
			bool IsMethodStatic = true;
			if ((IsMethodStatic == true))
			{
				ManagementBaseObject inParams = null;
				ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
				ManagementClass classObj = new ManagementClass(statMgmtScope, mgmtPath, null);
				ManagementBaseObject outParams = classObj.InvokeMethod("ReleaseDHCPLeaseAll", inParams, null);
				return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
			}
			else
			{
				return Convert.ToUInt32(0);
			}
		}

		public static uint RenewDHCPLeaseAll()
		{
			bool IsMethodStatic = true;
			if ((IsMethodStatic == true))
			{
				ManagementBaseObject inParams = null;
				ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
				ManagementClass classObj = new ManagementClass(statMgmtScope, mgmtPath, null);
				ManagementBaseObject outParams = classObj.InvokeMethod("RenewDHCPLeaseAll", inParams, null);
				return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
			}
			else
			{
				return Convert.ToUInt32(0);
			}
		}

		public static uint SetArpAlwaysSourceRoute(bool ArpAlwaysSourceRoute)
		{
			bool IsMethodStatic = true;
			if ((IsMethodStatic == true))
			{
				ManagementBaseObject inParams = null;
				ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
				ManagementClass classObj = new ManagementClass(statMgmtScope, mgmtPath, null);
				inParams = classObj.GetMethodParameters("SetArpAlwaysSourceRoute");
				inParams["ArpAlwaysSourceRoute"] = ((bool)(ArpAlwaysSourceRoute));
				ManagementBaseObject outParams = classObj.InvokeMethod("SetArpAlwaysSourceRoute", inParams, null);
				return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
			}
			else
			{
				return Convert.ToUInt32(0);
			}
		}

		public static uint SetArpUseEtherSNAP(bool ArpUseEtherSNAP)
		{
			bool IsMethodStatic = true;
			if ((IsMethodStatic == true))
			{
				ManagementBaseObject inParams = null;
				ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
				ManagementClass classObj = new ManagementClass(statMgmtScope, mgmtPath, null);
				inParams = classObj.GetMethodParameters("SetArpUseEtherSNAP");
				inParams["ArpUseEtherSNAP"] = ((bool)(ArpUseEtherSNAP));
				ManagementBaseObject outParams = classObj.InvokeMethod("SetArpUseEtherSNAP", inParams, null);
				return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
			}
			else
			{
				return Convert.ToUInt32(0);
			}
		}

		public static uint SetDatabasePath(string DatabasePath)
		{
			bool IsMethodStatic = true;
			if ((IsMethodStatic == true))
			{
				ManagementBaseObject inParams = null;
				ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
				ManagementClass classObj = new ManagementClass(statMgmtScope, mgmtPath, null);
				inParams = classObj.GetMethodParameters("SetDatabasePath");
				inParams["DatabasePath"] = ((string)(DatabasePath));
				ManagementBaseObject outParams = classObj.InvokeMethod("SetDatabasePath", inParams, null);
				return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
			}
			else
			{
				return Convert.ToUInt32(0);
			}
		}

		public static uint SetDeadGWDetect(bool DeadGWDetectEnabled)
		{
			bool IsMethodStatic = true;
			if ((IsMethodStatic == true))
			{
				ManagementBaseObject inParams = null;
				ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
				ManagementClass classObj = new ManagementClass(statMgmtScope, mgmtPath, null);
				inParams = classObj.GetMethodParameters("SetDeadGWDetect");
				inParams["DeadGWDetectEnabled"] = ((bool)(DeadGWDetectEnabled));
				ManagementBaseObject outParams = classObj.InvokeMethod("SetDeadGWDetect", inParams, null);
				return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
			}
			else
			{
				return Convert.ToUInt32(0);
			}
		}

		public static uint SetDefaultTOS(byte DefaultTOS)
		{
			bool IsMethodStatic = true;
			if ((IsMethodStatic == true))
			{
				ManagementBaseObject inParams = null;
				ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
				ManagementClass classObj = new ManagementClass(statMgmtScope, mgmtPath, null);
				inParams = classObj.GetMethodParameters("SetDefaultTOS");
				inParams["DefaultTOS"] = ((byte)(DefaultTOS));
				ManagementBaseObject outParams = classObj.InvokeMethod("SetDefaultTOS", inParams, null);
				return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
			}
			else
			{
				return Convert.ToUInt32(0);
			}
		}

		public static uint SetDefaultTTL(byte DefaultTTL)
		{
			bool IsMethodStatic = true;
			if ((IsMethodStatic == true))
			{
				ManagementBaseObject inParams = null;
				ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
				ManagementClass classObj = new ManagementClass(statMgmtScope, mgmtPath, null);
				inParams = classObj.GetMethodParameters("SetDefaultTTL");
				inParams["DefaultTTL"] = ((byte)(DefaultTTL));
				ManagementBaseObject outParams = classObj.InvokeMethod("SetDefaultTTL", inParams, null);
				return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
			}
			else
			{
				return Convert.ToUInt32(0);
			}
		}

		public static uint SetDNSSuffixSearchOrder(string[] DNSDomainSuffixSearchOrder)
		{
			bool IsMethodStatic = true;
			if ((IsMethodStatic == true))
			{
				ManagementBaseObject inParams = null;
				ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
				ManagementClass classObj = new ManagementClass(statMgmtScope, mgmtPath, null);
				inParams = classObj.GetMethodParameters("SetDNSSuffixSearchOrder");
				inParams["DNSDomainSuffixSearchOrder"] = ((string[])(DNSDomainSuffixSearchOrder));
				ManagementBaseObject outParams = classObj.InvokeMethod("SetDNSSuffixSearchOrder", inParams, null);
				return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
			}
			else
			{
				return Convert.ToUInt32(0);
			}
		}

		public static uint SetForwardBufferMemory(uint ForwardBufferMemory)
		{
			bool IsMethodStatic = true;
			if ((IsMethodStatic == true))
			{
				ManagementBaseObject inParams = null;
				ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
				ManagementClass classObj = new ManagementClass(statMgmtScope, mgmtPath, null);
				inParams = classObj.GetMethodParameters("SetForwardBufferMemory");
				inParams["ForwardBufferMemory"] = ((uint)(ForwardBufferMemory));
				ManagementBaseObject outParams = classObj.InvokeMethod("SetForwardBufferMemory", inParams, null);
				return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
			}
			else
			{
				return Convert.ToUInt32(0);
			}
		}
		public static uint SetIGMPLevel(byte IGMPLevel)
		{
			bool IsMethodStatic = true;
			if ((IsMethodStatic == true))
			{
				ManagementBaseObject inParams = null;
				ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
				ManagementClass classObj = new ManagementClass(statMgmtScope, mgmtPath, null);
				inParams = classObj.GetMethodParameters("SetIGMPLevel");
				inParams["IGMPLevel"] = ((byte)(IGMPLevel));
				ManagementBaseObject outParams = classObj.InvokeMethod("SetIGMPLevel", inParams, null);
				return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
			}
			else
			{
				return Convert.ToUInt32(0);
			}
		}

		public static uint SetIPUseZeroBroadcast(bool IPUseZeroBroadcast)
		{
			bool IsMethodStatic = true;
			if ((IsMethodStatic == true))
			{
				ManagementBaseObject inParams = null;
				ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
				ManagementClass classObj = new ManagementClass(statMgmtScope, mgmtPath, null);
				inParams = classObj.GetMethodParameters("SetIPUseZeroBroadcast");
				inParams["IPUseZeroBroadcast"] = ((bool)(IPUseZeroBroadcast));
				ManagementBaseObject outParams = classObj.InvokeMethod("SetIPUseZeroBroadcast", inParams, null);
				return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
			}
			else
			{
				return Convert.ToUInt32(0);
			}
		}

		public static uint SetIPXVirtualNetworkNumber(string IPXVirtualNetNumber)
		{
			bool IsMethodStatic = true;
			if ((IsMethodStatic == true))
			{
				ManagementBaseObject inParams = null;
				ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
				ManagementClass classObj = new ManagementClass(statMgmtScope, mgmtPath, null);
				inParams = classObj.GetMethodParameters("SetIPXVirtualNetworkNumber");
				inParams["IPXVirtualNetNumber"] = ((string)(IPXVirtualNetNumber));
				ManagementBaseObject outParams = classObj.InvokeMethod("SetIPXVirtualNetworkNumber", inParams, null);
				return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
			}
			else
			{
				return Convert.ToUInt32(0);
			}
		}

		public static uint SetKeepAliveInterval(uint KeepAliveInterval)
		{
			bool IsMethodStatic = true;
			if ((IsMethodStatic == true))
			{
				ManagementBaseObject inParams = null;
				ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
				ManagementClass classObj = new ManagementClass(statMgmtScope, mgmtPath, null);
				inParams = classObj.GetMethodParameters("SetKeepAliveInterval");
				inParams["KeepAliveInterval"] = ((uint)(KeepAliveInterval));
				ManagementBaseObject outParams = classObj.InvokeMethod("SetKeepAliveInterval", inParams, null);
				return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
			}
			else
			{
				return Convert.ToUInt32(0);
			}
		}

		public static uint SetKeepAliveTime(uint KeepAliveTime)
		{
			bool IsMethodStatic = true;
			if ((IsMethodStatic == true))
			{
				ManagementBaseObject inParams = null;
				ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
				ManagementClass classObj = new ManagementClass(statMgmtScope, mgmtPath, null);
				inParams = classObj.GetMethodParameters("SetKeepAliveTime");
				inParams["KeepAliveTime"] = ((uint)(KeepAliveTime));
				ManagementBaseObject outParams = classObj.InvokeMethod("SetKeepAliveTime", inParams, null);
				return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
			}
			else
			{
				return Convert.ToUInt32(0);
			}
		}

		public static uint SetMTU(uint MTU)
		{
			bool IsMethodStatic = true;
			if ((IsMethodStatic == true))
			{
				ManagementBaseObject inParams = null;
				ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
				ManagementClass classObj = new ManagementClass(statMgmtScope, mgmtPath, null);
				inParams = classObj.GetMethodParameters("SetMTU");
				inParams["MTU"] = ((uint)(MTU));
				ManagementBaseObject outParams = classObj.InvokeMethod("SetMTU", inParams, null);
				return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
			}
			else
			{
				return Convert.ToUInt32(0);
			}
		}

		public static uint SetNumForwardPackets(uint NumForwardPackets)
		{
			bool IsMethodStatic = true;
			if ((IsMethodStatic == true))
			{
				ManagementBaseObject inParams = null;
				ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
				ManagementClass classObj = new ManagementClass(statMgmtScope, mgmtPath, null);
				inParams = classObj.GetMethodParameters("SetNumForwardPackets");
				inParams["NumForwardPackets"] = ((uint)(NumForwardPackets));
				ManagementBaseObject outParams = classObj.InvokeMethod("SetNumForwardPackets", inParams, null);
				return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
			}
			else
			{
				return Convert.ToUInt32(0);
			}
		}

		public static uint SetPMTUBHDetect(bool PMTUBHDetectEnabled)
		{
			bool IsMethodStatic = true;
			if ((IsMethodStatic == true))
			{
				ManagementBaseObject inParams = null;
				ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
				ManagementClass classObj = new ManagementClass(statMgmtScope, mgmtPath, null);
				inParams = classObj.GetMethodParameters("SetPMTUBHDetect");
				inParams["PMTUBHDetectEnabled"] = ((bool)(PMTUBHDetectEnabled));
				ManagementBaseObject outParams = classObj.InvokeMethod("SetPMTUBHDetect", inParams, null);
				return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
			}
			else
			{
				return Convert.ToUInt32(0);
			}
		}

		public static uint SetPMTUDiscovery(bool PMTUDiscoveryEnabled)
		{
			bool IsMethodStatic = true;
			if ((IsMethodStatic == true))
			{
				ManagementBaseObject inParams = null;
				ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
				ManagementClass classObj = new ManagementClass(statMgmtScope, mgmtPath, null);
				inParams = classObj.GetMethodParameters("SetPMTUDiscovery");
				inParams["PMTUDiscoveryEnabled"] = ((bool)(PMTUDiscoveryEnabled));
				ManagementBaseObject outParams = classObj.InvokeMethod("SetPMTUDiscovery", inParams, null);
				return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
			}
			else
			{
				return Convert.ToUInt32(0);
			}
		}

		public static uint SetTcpMaxConnectRetransmissions(uint TcpMaxConnectRetransmissions)
		{
			bool IsMethodStatic = true;
			if ((IsMethodStatic == true))
			{
				ManagementBaseObject inParams = null;
				ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
				ManagementClass classObj = new ManagementClass(statMgmtScope, mgmtPath, null);
				inParams = classObj.GetMethodParameters("SetTcpMaxConnectRetransmissions");
				inParams["TcpMaxConnectRetransmissions"] = ((uint)(TcpMaxConnectRetransmissions));
				ManagementBaseObject outParams = classObj.InvokeMethod("SetTcpMaxConnectRetransmissions", inParams, null);
				return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
			}
			else
			{
				return Convert.ToUInt32(0);
			}
		}

		public static uint SetTcpMaxDataRetransmissions(uint TcpMaxDataRetransmissions)
		{
			bool IsMethodStatic = true;
			if ((IsMethodStatic == true))
			{
				ManagementBaseObject inParams = null;
				ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
				ManagementClass classObj = new ManagementClass(statMgmtScope, mgmtPath, null);
				inParams = classObj.GetMethodParameters("SetTcpMaxDataRetransmissions");
				inParams["TcpMaxDataRetransmissions"] = ((uint)(TcpMaxDataRetransmissions));
				ManagementBaseObject outParams = classObj.InvokeMethod("SetTcpMaxDataRetransmissions", inParams, null);
				return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
			}
			else
			{
				return Convert.ToUInt32(0);
			}
		}

		public static uint SetTcpNumConnections(uint TcpNumConnections)
		{
			bool IsMethodStatic = true;
			if ((IsMethodStatic == true))
			{
				ManagementBaseObject inParams = null;
				ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
				ManagementClass classObj = new ManagementClass(statMgmtScope, mgmtPath, null);
				inParams = classObj.GetMethodParameters("SetTcpNumConnections");
				inParams["TcpNumConnections"] = ((uint)(TcpNumConnections));
				ManagementBaseObject outParams = classObj.InvokeMethod("SetTcpNumConnections", inParams, null);
				return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
			}
			else
			{
				return Convert.ToUInt32(0);
			}
		}

		public static uint SetTcpUseRFC1122UrgentPointer(bool TcpUseRFC1122UrgentPointer)
		{
			bool IsMethodStatic = true;
			if ((IsMethodStatic == true))
			{
				ManagementBaseObject inParams = null;
				ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
				ManagementClass classObj = new ManagementClass(statMgmtScope, mgmtPath, null);
				inParams = classObj.GetMethodParameters("SetTcpUseRFC1122UrgentPointer");
				inParams["TcpUseRFC1122UrgentPointer"] = ((bool)(TcpUseRFC1122UrgentPointer));
				ManagementBaseObject outParams = classObj.InvokeMethod("SetTcpUseRFC1122UrgentPointer", inParams, null);
				return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
			}
			else
			{
				return Convert.ToUInt32(0);
			}
		}

		public static uint SetTcpWindowSize(ushort TcpWindowSize)
		{
			bool IsMethodStatic = true;
			if ((IsMethodStatic == true))
			{
				ManagementBaseObject inParams = null;
				ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
				ManagementClass classObj = new ManagementClass(statMgmtScope, mgmtPath, null);
				inParams = classObj.GetMethodParameters("SetTcpWindowSize");
				inParams["TcpWindowSize"] = ((ushort)(TcpWindowSize));
				ManagementBaseObject outParams = classObj.InvokeMethod("SetTcpWindowSize", inParams, null);
				return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
			}
			else
			{
				return Convert.ToUInt32(0);
			}
		}
		#endregion

		#region Enums
		public enum IGMPLevelValues {
            
            Kein_Multicast = 0,
            
            IP_Multicast = 1,
            
            IP_IGMP_Multicast = 2,
            
            NULL_ENUM_VALUE = 3,
        }
        
        public enum IPXFrameTypeValues {
            
            Ethernet_II = 0,
            
            Ethernet_802_3 = 1,
            
            Ethernet_802_2 = 2,
            
            Ethernet_SNAP = 3,
            
            AUTO = 255,
            
            NULL_ENUM_VALUE = 256,
        }
        
        public enum IPXMediaTypeValues {
            
            Ethernet = 1,
            
            Token_Ring = 2,
            
            FDDI = 3,
            
            ARCNET = 8,
            
            NULL_ENUM_VALUE = 0,
        }
        
        public enum TcpipNetbiosOptionsValues {
            
            NetBios_über_DHCP_aktivieren = 0,
            
            NetBios_aktivieren = 1,
            
            NetBios_deaktivieren = 2,
            
            NULL_ENUM_VALUE = 3,
        }
		#endregion

		// Enumeratorimplementierung zum Aufzählen von Instanzen der Klasse.
        public class NetworkAdapterConfigurationCollection : object, ICollection<NetworkAdapterConfiguration>
		{
			#region internal Properties
			internal ManagementObjectCollection privColObj;
			#endregion

			#region Constructor
			public NetworkAdapterConfigurationCollection(ManagementObjectCollection objCollection) {
                privColObj = objCollection;
            }
			#endregion

			#region Public Properties
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

			public virtual bool IsReadOnly
			{
				get
				{
					return true;
				}
			}
			#endregion

			#region Public Methods
			public virtual void CopyTo(NetworkAdapterConfiguration[] array, int index) {
                privColObj.CopyTo(array, index);
                int nCtr;
                for (nCtr = 0; (nCtr < array.Length); nCtr = (nCtr + 1)) {
                    array.SetValue(new NetworkAdapterConfiguration(((ManagementObject)(array.GetValue(nCtr)))), nCtr);
                }
            }

			IEnumerator IEnumerable.GetEnumerator()
			{
				return ((NetworkAdapterConfigurationCollection)this).GetEnumerator();
			}
            public virtual IEnumerator<NetworkAdapterConfiguration> GetEnumerator() {
                return new NetworkAdapterConfigurationEnumerator(privColObj.GetEnumerator());
            }

			/// <summary>
			/// <para>Not working!</para>
			/// </summary>
			/// <param name="nac"></param>
			public virtual void Add(NetworkAdapterConfiguration nac)
			{
			}
			/// <summary>
			/// <para>Not working!</para>
			/// </summary>
			/// <param name="nac"></param>
			public virtual bool Remove(NetworkAdapterConfiguration nac)
			{
				return false;
			}
			/// <summary>
			/// <para>Not working!</para>
			/// </summary>
			/// <param name="nac"></param>
			public virtual void Clear()
			{
			}
			/// <summary>
			/// <para>Looks for an element in this collection</para>
			/// </summary>
			/// <param name="nac"></param>
			/// <returns></returns>
			public virtual bool Contains(NetworkAdapterConfiguration nac)
			{
				foreach (NetworkAdapterConfiguration nac1 in this)
					if (nac1 == nac)
						return true;
				return false;
			}
			#endregion

			#region IEnumerator
			public class NetworkAdapterConfigurationEnumerator : object, IEnumerator<NetworkAdapterConfiguration> {
                
                internal ManagementObjectCollection.ManagementObjectEnumerator privObjEnum;
                
                public NetworkAdapterConfigurationEnumerator(ManagementObjectCollection.ManagementObjectEnumerator objEnum) {
                    privObjEnum = objEnum;
                }

				object IEnumerator.Current
				{
					get
					{
						return ((NetworkAdapterConfigurationEnumerator)this).Current;
					}
				}
                public virtual NetworkAdapterConfiguration Current {
                    get {
                        return new NetworkAdapterConfiguration(((ManagementObject)(privObjEnum.Current)));
                    }
                }
                
                public virtual bool MoveNext() {
                    return privObjEnum.MoveNext();
                }
                
                public virtual void Reset() {
                    privObjEnum.Reset();
                }

				public virtual void Dispose()
				{
					this.privObjEnum.Dispose();
				}
			}
			#endregion

			#region Operators
			public NetworkAdapterConfiguration this[uint index]
			{
				get
				{
					if (index > this.Count)
						throw new ArgumentOutOfRangeException("index");

					IEnumerator<NetworkAdapterConfiguration> enumerator = this.GetEnumerator();
					enumerator.Reset();
					uint i = 0;
					do
					{
						if (!enumerator.MoveNext())
							throw new ArgumentOutOfRangeException("index");
					} while (i++ < index);
					return enumerator.Current;
				}
			}
			#endregion
		}
        
        // 'TypeConverter' zum Behandeln von NULL-Werten für ValueType-Eigenschaften.
        public class WMIValueTypeConverter : TypeConverter {
            
            internal TypeConverter baseConverter;
            
            internal Type baseType;
            
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
            
            internal ManagementBaseObject internalLateBoundObject;
            
            public ManagementSystemProperties(ManagementBaseObject ManagedObject) {
                internalLateBoundObject = ManagedObject;
            }
            
            [Browsable(true)]
            public int GENUS {
                get {
                    return ((int)(internalLateBoundObject["__GENUS"]));
                }
            }
            
            [Browsable(true)]
            public string CLASS {
                get {
                    return ((string)(internalLateBoundObject["__CLASS"]));
                }
            }
            
            [Browsable(true)]
            public string SUPERCLASS {
                get {
                    return ((string)(internalLateBoundObject["__SUPERCLASS"]));
                }
            }
            
            [Browsable(true)]
            public string DYNASTY {
                get {
                    return ((string)(internalLateBoundObject["__DYNASTY"]));
                }
            }
            
            [Browsable(true)]
            public string RELPATH {
                get {
                    return ((string)(internalLateBoundObject["__RELPATH"]));
                }
            }
            
            [Browsable(true)]
            public int PROPERTY_COUNT {
                get {
                    return ((int)(internalLateBoundObject["__PROPERTY_COUNT"]));
                }
            }
            
            [Browsable(true)]
            public string[] DERIVATION {
                get {
                    return ((string[])(internalLateBoundObject["__DERIVATION"]));
                }
            }
            
            [Browsable(true)]
            public string SERVER {
                get {
                    return ((string)(internalLateBoundObject["__SERVER"]));
                }
            }
            
            [Browsable(true)]
            public string NAMESPACE {
                get {
                    return ((string)(internalLateBoundObject["__NAMESPACE"]));
                }
            }
            
            [Browsable(true)]
            public string PATH {
                get {
                    return ((string)(internalLateBoundObject["__PATH"]));
                }
            }
        }
    }
}
