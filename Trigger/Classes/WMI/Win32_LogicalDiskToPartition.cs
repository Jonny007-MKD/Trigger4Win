namespace ROOT.CIMV2.Win32 {
    using System;
    using System.ComponentModel;
    using System.Management;
    using System.Collections;
    using System.Globalization;
    
    
    // Die ShouldSerialize<PropertyName>-Funktionen werden vom VS-Eigenschaftenbrowser verwendet, um zu überprüfen, ob eine bestimmte Eigenschaft serialisiert werden muss. Diese Funktionen werden für alle ValueType-Eigenschaften hinzugefügt (Eigenschaften des Typs Int32, BOOL usw. , die nicht auf NULL festgelegt werden können). Die Funktionen verwenden die Is<PropertyName>Null-Funktion. Die Funktionen werden auch in der TypeConverter-Implementierung für die Eigenschaften verwendet, um die jeweilige Eigenschaft in Bezug auf den NULL-Wert zu überprüfen, damit für einen Drag & Drop-Vorgang in Visual Studio ein leerer Wert im Eigenschaftenbrowser angezeigt werden kann.
    // Mit Funktionen der Art 'Is<PropertyName>Null()' wird überprüft, ob eine Eigenschaft NULL ist.
    // Die Reset<PropertyName>-Funktionen werden für Read/Write-Eigenschaften hinzugefügt, die NULL-Werte zulassen. Diese Funktionen werden vom VS-Designer im Eigenschaftenbrowser verwendet, um eine Eigenschaft auf NULL festzulegen.
    // Für jede Eigenschaft, die zur Klasse für WMI hinzugefügt wurde, sind Attribute festgelegt, um das Verhalten im Visual Studio-Designer sowie die zu verwendende TypeConverter-Klasse zu definieren.
    // Eine für die WMI-Klasse generierte Klasse mit früher Bindung.Win32_LogicalDiskToPartition
    public class LogicalDiskToPartition : System.ComponentModel.Component {
        
        // Private Eigenschaft, die den WMI-Namespace enthält, in dem sich die Klasse befindet.
        private static string CreatedWmiNamespace = "root\\CimV2";
        
        // Private Eigenschaft, die den Namen der WMI-Klasse enthält, die diese Klasse erstellt hat.
        private static string CreatedClassName = "Win32_LogicalDiskToPartition";
        
        // Private Membervariable, die 'ManagementScope' enthält, das von den verschiedenen Methoden verwendet wird.
        private static System.Management.ManagementScope statMgmtScope = null;
        
        private ManagementSystemProperties PrivateSystemProperties;
        
        // Zugrunde liegendes lateBound-WMI-Objekt.
        private System.Management.ManagementObject PrivateLateBoundObject;
        
        // Membervariable, in der das automatic commit-Verhalten für die Klasse gespeichert wird.
        private bool AutoCommitProp;
        
        // Private Variable, die die eingebettete Eigenschaft enthält, die die Instanz darstellt.
        private System.Management.ManagementBaseObject embeddedObj;
        
        // Das aktuelle WMI-Objekt.
        private System.Management.ManagementBaseObject curObj;
        
        // Flag zum Anzeigen, ob die Instanz ein eingebettetes Objekt ist.
        private bool isEmbedded;
        
        // Nachstehend sind unterschiedliche Konstruktorüberladungen aufgeführt, um eine Instanz der Klasse mit einem WMI-Objekt zu initialisieren.
        public LogicalDiskToPartition() {
            this.InitializeObject(null, null, null);
        }
        
        public LogicalDiskToPartition(System.Management.ManagementPath keyAntecedent, System.Management.ManagementPath keyDependent) {
            this.InitializeObject(null, new System.Management.ManagementPath(LogicalDiskToPartition.ConstructPath(keyAntecedent, keyDependent)), null);
        }
        
        public LogicalDiskToPartition(System.Management.ManagementScope mgmtScope, System.Management.ManagementPath keyAntecedent, System.Management.ManagementPath keyDependent) {
            this.InitializeObject(((System.Management.ManagementScope)(mgmtScope)), new System.Management.ManagementPath(LogicalDiskToPartition.ConstructPath(keyAntecedent, keyDependent)), null);
        }
        
        public LogicalDiskToPartition(System.Management.ManagementPath path, System.Management.ObjectGetOptions getOptions) {
            this.InitializeObject(null, path, getOptions);
        }
        
        public LogicalDiskToPartition(System.Management.ManagementScope mgmtScope, System.Management.ManagementPath path) {
            this.InitializeObject(mgmtScope, path, null);
        }
        
        public LogicalDiskToPartition(System.Management.ManagementPath path) {
            this.InitializeObject(null, path, null);
        }
        
        public LogicalDiskToPartition(System.Management.ManagementScope mgmtScope, System.Management.ManagementPath path, System.Management.ObjectGetOptions getOptions) {
            this.InitializeObject(mgmtScope, path, getOptions);
        }
        
        public LogicalDiskToPartition(System.Management.ManagementObject theObject) {
            Initialize();
            if ((CheckIfProperClass(theObject) == true)) {
                PrivateLateBoundObject = theObject;
                PrivateSystemProperties = new ManagementSystemProperties(PrivateLateBoundObject);
                curObj = PrivateLateBoundObject;
            }
            else {
                throw new System.ArgumentException("Der Klassenname stimmt nicht überein.");
            }
        }
        
        public LogicalDiskToPartition(System.Management.ManagementBaseObject theObject) {
            Initialize();
            if ((CheckIfProperClass(theObject) == true)) {
                embeddedObj = theObject;
                PrivateSystemProperties = new ManagementSystemProperties(theObject);
                curObj = embeddedObj;
                isEmbedded = true;
            }
            else {
                throw new System.ArgumentException("Der Klassenname stimmt nicht überein.");
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
        public System.Management.ManagementBaseObject LateBoundObject {
            get {
                return curObj;
            }
        }
        
        // 'ManagementScope' des Objekts.
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Management.ManagementScope Scope {
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
        public System.Management.ManagementPath Path {
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
                        throw new System.ArgumentException("Der Klassenname stimmt nicht überein.");
                    }
                    PrivateLateBoundObject.Path = value;
                }
            }
        }
        
        // Öffentliche statische Bereichseigenschaft, die von den verschiedenen Methoden verwendet wird.
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static System.Management.ManagementScope StaticScope {
            get {
                return statMgmtScope;
            }
            set {
                statMgmtScope = value;
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Referenz \"Antecedent\" stellt die Win32-Datenträgerpartition mit den Eigenscha" +
            "ften einer Datenträgerpartition dar, auf der sich der logische Datenträger befin" +
            "det.")]
        public System.Management.ManagementPath Antecedent {
            get {
                if ((curObj["Antecedent"] != null)) {
                    return new System.Management.ManagementPath(curObj["Antecedent"].ToString());
                }
                return null;
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Referenz \"Dependent\" stellt den logischen Win32-Datenträger dar, der die Eige" +
            "nschaften eines logischen Datenträgers auf einer physikalischen Datenträgerparti" +
            "tion enthält.")]
        public System.Management.ManagementPath Dependent {
            get {
                if ((curObj["Dependent"] != null)) {
                    return new System.Management.ManagementPath(curObj["Dependent"].ToString());
                }
                return null;
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsEndingAddressNull {
            get {
                if ((curObj["EndingAddress"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("\"EndingAddress\" gibt die Endadresse der höheren Speicherebene in der niedrigeren " +
            "Speicherebene an. Diese Eigenschaft kann zum Zuordnen von nicht zusammenhängende" +
            "n Erweiterungen in einer höheren Gruppierung verwendet werden.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ulong EndingAddress {
            get {
                if ((curObj["EndingAddress"] == null)) {
                    return System.Convert.ToUInt64(0);
                }
                return ((ulong)(curObj["EndingAddress"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsStartingAddressNull {
            get {
                if ((curObj["StartingAddress"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("\"StartingAddress\" gibt die Startadresse der höheren Speicherebene in der niedrige" +
            "ren Speicherebene an.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ulong StartingAddress {
            get {
                if ((curObj["StartingAddress"] == null)) {
                    return System.Convert.ToUInt64(0);
                }
                return ((ulong)(curObj["StartingAddress"]));
            }
        }
        
        private bool CheckIfProperClass(System.Management.ManagementScope mgmtScope, System.Management.ManagementPath path, System.Management.ObjectGetOptions OptionsParam) {
            if (((path != null) 
                        && (string.Compare(path.ClassName, this.ManagementClassName, true, System.Globalization.CultureInfo.InvariantCulture) == 0))) {
                return true;
            }
            else {
                return CheckIfProperClass(new System.Management.ManagementObject(mgmtScope, path, OptionsParam));
            }
        }
        
        private bool CheckIfProperClass(System.Management.ManagementBaseObject theObj) {
            if (((theObj != null) 
                        && (string.Compare(((string)(theObj["__CLASS"])), this.ManagementClassName, true, System.Globalization.CultureInfo.InvariantCulture) == 0))) {
                return true;
            }
            else {
                System.Array parentClasses = ((System.Array)(theObj["__DERIVATION"]));
                if ((parentClasses != null)) {
                    int count = 0;
                    for (count = 0; (count < parentClasses.Length); count = (count + 1)) {
                        if ((string.Compare(((string)(parentClasses.GetValue(count))), this.ManagementClassName, true, System.Globalization.CultureInfo.InvariantCulture) == 0)) {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        
        private bool ShouldSerializeEndingAddress() {
            if ((this.IsEndingAddressNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeStartingAddress() {
            if ((this.IsStartingAddressNull == false)) {
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
        public void CommitObject(System.Management.PutOptions putOptions) {
            if ((isEmbedded == false)) {
                PrivateLateBoundObject.Put(putOptions);
            }
        }
        
        private void Initialize() {
            AutoCommitProp = true;
            isEmbedded = false;
        }
        
        private static string ConstructPath(System.Management.ManagementPath keyAntecedent, System.Management.ManagementPath keyDependent) {
            string strPath = "root\\CimV2:Win32_LogicalDiskToPartition";
            strPath = string.Concat(strPath, string.Concat(".Antecedent=", ((System.Management.ManagementPath )(keyAntecedent)).ToString()));
            strPath = string.Concat(strPath, string.Concat(",Dependent=", ((System.Management.ManagementPath )(keyDependent)).ToString()));
            return strPath;
        }
        
        private void InitializeObject(System.Management.ManagementScope mgmtScope, System.Management.ManagementPath path, System.Management.ObjectGetOptions getOptions) {
            Initialize();
            if ((path != null)) {
                if ((CheckIfProperClass(mgmtScope, path, getOptions) != true)) {
                    throw new System.ArgumentException("Der Klassenname stimmt nicht überein.");
                }
            }
            PrivateLateBoundObject = new System.Management.ManagementObject(mgmtScope, path, getOptions);
            PrivateSystemProperties = new ManagementSystemProperties(PrivateLateBoundObject);
            curObj = PrivateLateBoundObject;
        }
        
        // Unterschiedliche Überladungen von 'GetInstances()' unterstützen beim Aufzählen von Instanzen der WMI-Klasse.
        public static LogicalDiskToPartitionCollection GetInstances() {
            return GetInstances(null, null, null);
        }
        
        public static LogicalDiskToPartitionCollection GetInstances(string condition) {
            return GetInstances(null, condition, null);
        }
        
        public static LogicalDiskToPartitionCollection GetInstances(string[] selectedProperties) {
            return GetInstances(null, null, selectedProperties);
        }
        
        public static LogicalDiskToPartitionCollection GetInstances(string condition, string[] selectedProperties) {
            return GetInstances(null, condition, selectedProperties);
        }
        
        public static LogicalDiskToPartitionCollection GetInstances(System.Management.ManagementScope mgmtScope, System.Management.EnumerationOptions enumOptions) {
            if ((mgmtScope == null)) {
                if ((statMgmtScope == null)) {
                    mgmtScope = new System.Management.ManagementScope();
                    mgmtScope.Path.NamespacePath = "root\\CimV2";
                }
                else {
                    mgmtScope = statMgmtScope;
                }
            }
            System.Management.ManagementPath pathObj = new System.Management.ManagementPath();
            pathObj.ClassName = "Win32_LogicalDiskToPartition";
            pathObj.NamespacePath = "root\\CimV2";
            System.Management.ManagementClass clsObject = new System.Management.ManagementClass(mgmtScope, pathObj, null);
            if ((enumOptions == null)) {
                enumOptions = new System.Management.EnumerationOptions();
                enumOptions.EnsureLocatable = true;
            }
            return new LogicalDiskToPartitionCollection(clsObject.GetInstances(enumOptions));
        }
        
        public static LogicalDiskToPartitionCollection GetInstances(System.Management.ManagementScope mgmtScope, string condition) {
            return GetInstances(mgmtScope, condition, null);
        }
        
        public static LogicalDiskToPartitionCollection GetInstances(System.Management.ManagementScope mgmtScope, string[] selectedProperties) {
            return GetInstances(mgmtScope, null, selectedProperties);
        }
        
        public static LogicalDiskToPartitionCollection GetInstances(System.Management.ManagementScope mgmtScope, string condition, string[] selectedProperties) {
            if ((mgmtScope == null)) {
                if ((statMgmtScope == null)) {
                    mgmtScope = new System.Management.ManagementScope();
                    mgmtScope.Path.NamespacePath = "root\\CimV2";
                }
                else {
                    mgmtScope = statMgmtScope;
                }
            }
            System.Management.ManagementObjectSearcher ObjectSearcher = new System.Management.ManagementObjectSearcher(mgmtScope, new SelectQuery("Win32_LogicalDiskToPartition", condition, selectedProperties));
            System.Management.EnumerationOptions enumOptions = new System.Management.EnumerationOptions();
            enumOptions.EnsureLocatable = true;
            ObjectSearcher.Options = enumOptions;
            return new LogicalDiskToPartitionCollection(ObjectSearcher.Get());
        }
        
        [Browsable(true)]
        public static LogicalDiskToPartition CreateInstance() {
            System.Management.ManagementScope mgmtScope = null;
            if ((statMgmtScope == null)) {
                mgmtScope = new System.Management.ManagementScope();
                mgmtScope.Path.NamespacePath = CreatedWmiNamespace;
            }
            else {
                mgmtScope = statMgmtScope;
            }
            System.Management.ManagementPath mgmtPath = new System.Management.ManagementPath(CreatedClassName);
            System.Management.ManagementClass tmpMgmtClass = new System.Management.ManagementClass(mgmtScope, mgmtPath, null);
            return new LogicalDiskToPartition(tmpMgmtClass.CreateInstance());
        }
        
        [Browsable(true)]
        public void Delete() {
            PrivateLateBoundObject.Delete();
        }
        
        // Enumeratorimplementierung zum Aufzählen von Instanzen der Klasse.
        public class LogicalDiskToPartitionCollection : object, ICollection {
            
            private ManagementObjectCollection privColObj;
            
            public LogicalDiskToPartitionCollection(ManagementObjectCollection objCollection) {
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
            
            public virtual void CopyTo(System.Array array, int index) {
                privColObj.CopyTo(array, index);
                int nCtr;
                for (nCtr = 0; (nCtr < array.Length); nCtr = (nCtr + 1)) {
                    array.SetValue(new LogicalDiskToPartition(((System.Management.ManagementObject)(array.GetValue(nCtr)))), nCtr);
                }
            }
            
            public virtual System.Collections.IEnumerator GetEnumerator() {
                return new LogicalDiskToPartitionEnumerator(privColObj.GetEnumerator());
            }
            
            public class LogicalDiskToPartitionEnumerator : object, System.Collections.IEnumerator {
                
                private ManagementObjectCollection.ManagementObjectEnumerator privObjEnum;
                
                public LogicalDiskToPartitionEnumerator(ManagementObjectCollection.ManagementObjectEnumerator objEnum) {
                    privObjEnum = objEnum;
                }
                
                public virtual object Current {
                    get {
                        return new LogicalDiskToPartition(((System.Management.ManagementObject)(privObjEnum.Current)));
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
            
            private System.Type baseType;
            
            public WMIValueTypeConverter(System.Type inBaseType) {
                baseConverter = TypeDescriptor.GetConverter(inBaseType);
                baseType = inBaseType;
            }
            
            public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Type srcType) {
                return baseConverter.CanConvertFrom(context, srcType);
            }
            
            public override bool CanConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Type destinationType) {
                return baseConverter.CanConvertTo(context, destinationType);
            }
            
            public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value) {
                return baseConverter.ConvertFrom(context, culture, value);
            }
            
            public override object CreateInstance(System.ComponentModel.ITypeDescriptorContext context, System.Collections.IDictionary dictionary) {
                return baseConverter.CreateInstance(context, dictionary);
            }
            
            public override bool GetCreateInstanceSupported(System.ComponentModel.ITypeDescriptorContext context) {
                return baseConverter.GetCreateInstanceSupported(context);
            }
            
            public override PropertyDescriptorCollection GetProperties(System.ComponentModel.ITypeDescriptorContext context, object value, System.Attribute[] attributeVar) {
                return baseConverter.GetProperties(context, value, attributeVar);
            }
            
            public override bool GetPropertiesSupported(System.ComponentModel.ITypeDescriptorContext context) {
                return baseConverter.GetPropertiesSupported(context);
            }
            
            public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(System.ComponentModel.ITypeDescriptorContext context) {
                return baseConverter.GetStandardValues(context);
            }
            
            public override bool GetStandardValuesExclusive(System.ComponentModel.ITypeDescriptorContext context) {
                return baseConverter.GetStandardValuesExclusive(context);
            }
            
            public override bool GetStandardValuesSupported(System.ComponentModel.ITypeDescriptorContext context) {
                return baseConverter.GetStandardValuesSupported(context);
            }
            
            public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, System.Type destinationType) {
                if ((baseType.BaseType == typeof(System.Enum))) {
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
                            && (baseType.BaseType == typeof(System.ValueType)))) {
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
        [TypeConverter(typeof(System.ComponentModel.ExpandableObjectConverter))]
        public class ManagementSystemProperties {
            
            private System.Management.ManagementBaseObject PrivateLateBoundObject;
            
            public ManagementSystemProperties(System.Management.ManagementBaseObject ManagedObject) {
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
