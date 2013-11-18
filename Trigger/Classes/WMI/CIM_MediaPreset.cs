using System;
using System.ComponentModel;
using System.Management;
using System.Collections;
using System.Globalization;


namespace Trigger.Classes.WIM
{
// Die ShouldSerialize<PropertyName>-Funktionen werden vom VS-Eigenschaftenbrowser verwendet, um zu überprüfen, ob eine bestimmte Eigenschaft serialisiert werden muss. Diese Funktionen werden für alle ValueType-Eigenschaften hinzugefügt (Eigenschaften des Typs Int32, BOOL usw. , die nicht auf NULL festgelegt werden können). Die Funktionen verwenden die Is<PropertyName>Null-Funktion. Die Funktionen werden auch in der TypeConverter-Implementierung für die Eigenschaften verwendet, um die jeweilige Eigenschaft in Bezug auf den NULL-Wert zu überprüfen, damit für einen Drag & Drop-Vorgang in Visual Studio ein leerer Wert im Eigenschaftenbrowser angezeigt werden kann.
    // Mit Funktionen der Art 'Is<PropertyName>Null()' wird überprüft, ob eine Eigenschaft NULL ist.
    // Die Reset<PropertyName>-Funktionen werden für Read/Write-Eigenschaften hinzugefügt, die NULL-Werte zulassen. Diese Funktionen werden vom VS-Designer im Eigenschaftenbrowser verwendet, um eine Eigenschaft auf NULL festzulegen.
    // Für jede Eigenschaft, die zur Klasse für WMI hinzugefügt wurde, sind Attribute festgelegt, um das Verhalten im Visual Studio-Designer sowie die zu verwendende TypeConverter-Klasse zu definieren.
    // Eine für die WMI-Klasse generierte Klasse mit früher Bindung.CIM_MediaPresent
    public class MediaPresent : Component {
        
        // internal Eigenschaft, die den WMI-Namespace enthält, in dem sich die Klasse befindet.
        internal static string CreatedWmiNamespace = "root\\CimV2";
        
        // internal Eigenschaft, die den Namen der WMI-Klasse enthält, die diese Klasse erstellt hat.
        internal static string CreatedClassName = "CIM_MediaPresent";
        
        // internal Membervariable, die 'ManagementScope' enthält, das von den verschiedenen Methoden verwendet wird.
        internal static ManagementScope statMgmtScope = null;
        
        internal ManagementSystemProperties internalSystemProperties;
        
        // Zugrunde liegendes lateBound-WMI-Objekt.
        internal ManagementObject internalLateBoundObject;
        
        // Membervariable, in der das automatic commit-Verhalten für die Klasse gespeichert wird.
        internal bool AutoCommitProp;
        
        // internal Variable, die die eingebettete Eigenschaft enthält, die die Instanz darstellt.
        internal ManagementBaseObject embeddedObj;
        
        // Das aktuelle WMI-Objekt.
        internal ManagementBaseObject curObj;
        
        // Flag zum Anzeigen, ob die Instanz ein eingebettetes Objekt ist.
        internal bool isEmbedded;
        
        // Nachstehend sind unterschiedliche Konstruktorüberladungen aufgeführt, um eine Instanz der Klasse mit einem WMI-Objekt zu initialisieren.
        public MediaPresent() {
            this.InitializeObject(null, null, null);
        }
        
        public MediaPresent(ManagementPath path, ObjectGetOptions getOptions) {
            this.InitializeObject(null, path, getOptions);
        }
        
        public MediaPresent(ManagementScope mgmtScope, ManagementPath path) {
            this.InitializeObject(mgmtScope, path, null);
        }
        
        public MediaPresent(ManagementPath path) {
            this.InitializeObject(null, path, null);
        }
        
        public MediaPresent(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) {
            this.InitializeObject(mgmtScope, path, getOptions);
        }
        
        public MediaPresent(ManagementObject theObject) {
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
        
        public MediaPresent(ManagementBaseObject theObject) {
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
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Gerät für den Medienzugriff.")]
        public ManagementPath Antecedent {
            get {
                if ((curObj["Antecedent"] != null)) {
                    return new ManagementPath(curObj["Antecedent"].ToString());
                }
                return null;
            }
            set {
                curObj["Antecedent"] = ((ManagementPath)(value)).Path;
                if (((isEmbedded == false) 
                            && (AutoCommitProp == true))) {
                    internalLateBoundObject.Put();
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Die Speichererweiterung auf die mit dem Gerät für den Medienzugriff zugegriffen w" +
            "ird.")]
        public ManagementPath Dependent {
            get {
                if ((curObj["Dependent"] != null)) {
                    return new ManagementPath(curObj["Dependent"].ToString());
                }
                return null;
            }
            set {
                curObj["Dependent"] = ((ManagementPath)(value)).Path;
                if (((isEmbedded == false) 
                            && (AutoCommitProp == true))) {
                    internalLateBoundObject.Put();
                }
            }
        }
        
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
        
        internal void ResetAntecedent() {
            curObj["Antecedent"] = null;
            if (((isEmbedded == false) 
                        && (AutoCommitProp == true))) {
                internalLateBoundObject.Put();
            }
        }
        
        internal void ResetDependent() {
            curObj["Dependent"] = null;
            if (((isEmbedded == false) 
                        && (AutoCommitProp == true))) {
                internalLateBoundObject.Put();
            }
        }
        
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
        
        internal static string ConstructPath() {
            string strPath = "root\\CimV2:CIM_MediaPresent";
            return strPath;
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
        
        // Unterschiedliche Überladungen von 'GetInstances()' unterstützen beim Aufzählen von Instanzen der WMI-Klasse.
        public static MediaPresentCollection GetInstances() {
            return GetInstances(null, null, null);
        }
        
        public static MediaPresentCollection GetInstances(string condition) {
            return GetInstances(null, condition, null);
        }
        
        public static MediaPresentCollection GetInstances(string[] selectedProperties) {
            return GetInstances(null, null, selectedProperties);
        }
        
        public static MediaPresentCollection GetInstances(string condition, string[] selectedProperties) {
            return GetInstances(null, condition, selectedProperties);
        }
        
        public static MediaPresentCollection GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) {
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
            pathObj.ClassName = "CIM_MediaPresent";
            pathObj.NamespacePath = "root\\CimV2";
            ManagementClass clsObject = new ManagementClass(mgmtScope, pathObj, null);
            if ((enumOptions == null)) {
                enumOptions = new EnumerationOptions();
                enumOptions.EnsureLocatable = true;
            }
            return new MediaPresentCollection(clsObject.GetInstances(enumOptions));
        }
        
        public static MediaPresentCollection GetInstances(ManagementScope mgmtScope, string condition) {
            return GetInstances(mgmtScope, condition, null);
        }
        
        public static MediaPresentCollection GetInstances(ManagementScope mgmtScope, string[] selectedProperties) {
            return GetInstances(mgmtScope, null, selectedProperties);
        }
        
        public static MediaPresentCollection GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) {
            if ((mgmtScope == null)) {
                if ((statMgmtScope == null)) {
                    mgmtScope = new ManagementScope();
                    mgmtScope.Path.NamespacePath = "root\\CimV2";
                }
                else {
                    mgmtScope = statMgmtScope;
                }
            }
            ManagementObjectSearcher ObjectSearcher = new ManagementObjectSearcher(mgmtScope, new SelectQuery("CIM_MediaPresent", condition, selectedProperties));
            EnumerationOptions enumOptions = new EnumerationOptions();
            enumOptions.EnsureLocatable = true;
            ObjectSearcher.Options = enumOptions;
            return new MediaPresentCollection(ObjectSearcher.Get());
        }
        
        [Browsable(true)]
        public static MediaPresent CreateInstance() {
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
            return new MediaPresent(tmpMgmtClass.CreateInstance());
        }
        
        [Browsable(true)]
        public void Delete() {
            internalLateBoundObject.Delete();
        }
        
        // Enumeratorimplementierung zum Aufzählen von Instanzen der Klasse.
        public class MediaPresentCollection : object, ICollection {
            
            internal ManagementObjectCollection privColObj;
            
            public MediaPresentCollection(ManagementObjectCollection objCollection) {
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
                    array.SetValue(new MediaPresent(((ManagementObject)(array.GetValue(nCtr)))), nCtr);
                }
            }
            
            public virtual IEnumerator GetEnumerator() {
                return new MediaPresentEnumerator(privColObj.GetEnumerator());
            }
            
            public class MediaPresentEnumerator : object, IEnumerator {
                
                internal ManagementObjectCollection.ManagementObjectEnumerator privObjEnum;
                
                public MediaPresentEnumerator(ManagementObjectCollection.ManagementObjectEnumerator objEnum) {
                    privObjEnum = objEnum;
                }
                
                public virtual object Current {
                    get {
                        return new MediaPresent(((ManagementObject)(privObjEnum.Current)));
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
