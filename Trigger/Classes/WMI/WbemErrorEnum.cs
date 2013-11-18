using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trigger.Classes.WMI
{
	enum WbemError : uint
	{
		///<summary><para>The call was successful.</para></summary>
		NoErr = 0x0,
		///<summary><para>The call failed.</para></summary>
		ErrFailed = 0x80041001,
		/// <summary><para>The object could not be found.</para></summary>
		ErrNotFound = 0x80041002,
		///<summary><para>The current user does not have permission to perform the action.</para></summary>
		ErrAccessDenied = 0x80041003,
		///<summary><para>The provider has failed at some time other than during initialization.</para></summary>
		ErrProviderFailure = 0x80041004,
		///<summary><para>A type mismatch occurred.</para></summary>
		ErrTypeMismatch = 0x80041005,
		///<summary><para>There was not enough memory for the operation.</para></summary>
		ErrOutOfMemory = 0x80041006,
		///<summary><para>The SWbemNamedValue object is not valid.</para></summary>
		ErrInvalidContext = 0x80041007,
		///<summary><para>One of the paraeters to the call is not correct.</para></summary>
		ErrInvalidparaeter = 0x80041008,
		///<summary><para>The resource, typically a remote server, is not currently available.</para></summary>
		ErrNotAvailable = 0x80041009,
		///<summary><para>An internal, critical, and unexpected error occurred. Report this error to Microsoft Technical Support.</para></summary>
		ErrCriticalError = 0x8004100A,
		///<summary><para>One or more network packets were corrupted during a remote session.</para></summary>
		ErrInvalidStream = 0x8004100B,
		///<summary><para>The feature or operation is not supported.</para></summary>
		ErrNotSupported = 0x8004100C,
		///<summary><para>The parent class specified is not valid.</para></summary>
		ErrInvalidSuperclass = 0x8004100D,
		///<summary><para>The namespace specified could not be found.</para></summary>
		ErrInvalidNamespace = 0x8004100E,
		///<summary><para>The specified instance is not valid.</para></summary>
		ErrInvalidObject = 0x8004100F,
		///<summary><para>The specified class is not valid.</para></summary>
		ErrInvalidClass = 0x80041010,
		///<summary><para>A provider referenced in the schema does not have a corresponding registration.</para></summary>
		ErrProviderNotFound = 0x80041011,
		///<summary><para>A provider referenced in the schema has an incorrect or incomplete registration. This error may be caused by a missing pragma namespace command in the MOF file used to register the provider, resulting in the provider being registered in the wrong WMI namespace. This error may also be caused by a corrupt repository, which may be fixed by deleting it and recompiling the MOF files.</para></summary>
		ErrInvalidProviderRegistration = 0x80041012,
		///<summary><para>COM cannot locate a provider referenced in the schema. This error may be caused by any of the following:</para><para>The provider is using a WMI DLL that does not match the .lib fileused when the provider was built.|The provider's DLL or any of the DLLs on which it depends is corrupt.</para><para>The provider failed to export DllRegisterServer.</para><para>An in-process provider was not registered using /regsvr32.</para><para>An out-of-process provider was not registered using /regserver.</para></summary>
		ErrProviderLoadFailure = 0x80041013,
		///<summary><para>A component, such as a provider, failed to initialize for internal reasons.</para></summary>
		ErrInitializationFailure = 0x80041014,
		///<summary><para>A networking error occurred, preventing normal operation.</para></summary>
		ErrTransportFailure = 0x80041015,
		///<summary><para>The requested operation is not valid. This error usually applies to invalid attempts to delete classes or properties.</para></summary>
		ErrInvalidOperation = 0x80041016,
		///<summary><para>The requested operation is not valid. This error usually applies to invalid attempts to delete classes or properties.</para></summary>
		ErrInvalidQuery = 0x80041017,
		///<summary><para>The requested query language is not supported.</para></summary>
		ErrInvalidQueryType = 0x80041018,
		///<summary><para>In a put operation, the ChangeFlagCreateOnly flag was specified, but the instance already exists.</para></summary>
		ErrAlreadyExists = 0x80041019,
		///<summary><para>It is not possible to perform the add operation on this qualifier because the owning object does not permit overrides.</para></summary>
		ErrOverrideNotAllowed = 0x8004101A,
		///<summary><para>The user attempted to delete a qualifier that was not owned. The qualifier was inherited from a parent class.</para></summary>
		ErrPropagatedQualifier = 0x8004101B,
		///<summary><para>The user attempted to delete a property that was not owned. The property was inherited from a parent class.</para></summary>
		ErrPropagatedProperty = 0x8004101C,
		///<summary><para>The client made an unexpected and illegal sequence of calls, such as calling EndEnumeration before calling BeginEnumeration.</para></summary>
		ErrUnexpected = 0x8004101D,
		///<summary><para>The user requested an illegal operation, such as spawning a class from an instance.</para></summary>
		ErrIllegalOperation = 0x8004101E,
		///<summary><para>There was an illegal attempt to specify a key qualifier on a property that cannot be a key. The keys are specified in the class definition for an object, and cannot be altered on a per-instance basis.</para></summary>
		ErrCannotBeKey = 0x8004101F,
		///<summary><para>The current object is not a valid class definition. Either it is incomplete, or it has not been registered with WMI using SWbemObject.Put_.</para></summary>
		ErrIncompleteClass = 0x80041020,
		///<summary><para>The syntax of an input paraeter is incorrect for the applicable data structure. For example, when a CIM datetime structure does not have the correct format when passed to SWbemDateTime.SetFileTime.</para></summary>
		ErrInvalidSyntax = 0x80041021,
		///<summary><para>Reserved for future use.</para></summary>
		ErrNondecoratedObject = 0x80041022,
		///<summary><para>The property that you are attempting to modify is read-only.</para></summary>
		ErrReadOnly = 0x80041023,
		///<summary><para>The provider cannot perform the requested operation. This would include a query that is too complex, retrieving an instance, creating or updating a class, deleting a class, or enumerating a class.</para></summary>
		ErrProviderNotCapable = 0x80041024,
		///<summary><para>An attempt was made to make a change that would invalidate a subclass.</para></summary>
		ErrClassHasChildren = 0x80041025,
		///<summary><para>An attempt has been made to delete or modify a class that has instances.</para></summary>
		ErrClassHasInstances = 0x80041026,
		///<summary><para>Reserved for future use.</para></summary>
		ErrQueryNotImplemented = 0x80041027,
		///<summary><para>A value of Nothing was specified for a property that may not be Nothing, such as one that is marked by a Key, Indexed, or Not_Null qualifier.</para></summary>
		ErrIllegalNull = 0x80041028,
		///<summary><para>The CIM type specified for a property is not valid.</para></summary>
		ErrInvalidQualifierType = 0x80041029,
		///<summary><para>The CIM type specified for a property is not valid.</para></summary>
		ErrInvalidPropertyType = 0x8004102A,
		///<summary><para>The request was made with an out-of-range value, or is incompatible with the type.</para></summary>
		ErrValueOutOfRange = 0x8004102B,
		///<summary><para>An illegal attempt was made to make a class singleton, such as when the class is derived from a non-singleton class.</para></summary>
		ErrCannotBeSingleton = 0x8004102C,
		///<summary><para>The CIM type specified is not valid.</para></summary>
		ErrInvalidCimType = 0x8004102D,
		///<summary><para>The requested method is not available.</para></summary>
		ErrInvalidMethod = 0x8004102E,
		///<summary><para>The paraeters provided for the method are not valid.</para></summary>
		ErrInvalidMethodparaeters = 0x8004102F,
		///<summary><para>There was an attempt to get qualifiers on a system property.</para></summary>
		ErrSystemProperty = 0x80041030,
		///<summary><para>The property type is not recognized.</para></summary>
		ErrInvalidProperty = 0x80041031,
		///<summary><para>An asynchronous process has been canceled internally or by the user. Note that due to the timing and nature of the asynchronous operation the operation may not have been truly canceled.</para></summary>
		ErrCallCancelled = 0x80041032,
		///<summary><para>The user has requested an operation while WMI is in the process of shutting down.</para></summary>
		ErrShuttingDown = 0x80041033,
		///<summary><para>An attempt was made to reuse an existing method name from a parent class, and the signatures did not match.</para></summary>
		ErrPropagatedMethod = 0x80041034,
		///<summary><para>One or more paraeter values, such as a query text, is too complex or unsupported. WMI is therefore requested to retry the operation with simpler paraeters.</para></summary>
		ErrUnsupportedparaeter = 0x80041035,
		///<summary><para>A paraeter was missing from the method call.</para></summary>
		ErrMissingparaeter = 0x80041036,
		///<summary><para>A method paraeter has an ID qualifier that is not valid.</para></summary>
		ErrInvalidparaeterId = 0x80041037,
		///<summary><para>One or more of the method paraeters have ID qualifiers that are out of sequence.</para></summary>
		ErrNonConsecutiveparaeterIds = 0x80041038,
		///<summary><para>The return value for a method has an ID qualifier.</para></summary>
		ErrparaeterIdOnRetval = 0x80041039,
		///<summary><para>The specified object path was not valid.</para></summary>
		ErrInvalidObjectPath = 0x8004103A,
		/// <summary><para>Windows Server 2003:  Disk is out of space or the 4 GB limit on WMI repository (CIM repository) size is reached.</para><para>Windows XP/2000/NT:  Disk is out of space.</para></summary>
		ErrOutOfDiskSpace = 0x8004103B,
		///<summary><para>The supplied buffer was too small to hold all the objects in the enumerator or to read a string property.</para></summary>
		ErrBufferTooSmall = 0x8004103C,
		///<summary><para>The provider does not support the requested put operation.</para></summary>
		ErrUnsupportedPutExtension = 0x8004103D,
		///<summary><para>An object with an incorrect type or version was encountered during marshaling.</para></summary>
		ErrUnknownObjectType = 0x8004103E,
		///<summary><para>A packet with an incorrect type or version was encountered during marshaling.</para></summary>
		ErrUnknownPacketType = 0x8004103F,
		///<summary><para>The packet has an unsupported version.</para></summary>
		ErrMarshalVersionMismatch = 0x80041040,
		///<summary><para>The packet appears to be corrupted.</para></summary>
		ErrMarshalInvalidSignature = 0x80041041,
		///<summary><para>An attempt has been made to mismatch qualifiers, such as putting [key] on an object instead of a property.</para></summary>
		ErrInvalidQualifier = 0x80041042,
		///<summary><para>A duplicate paraeter has been declared in a CIM method.</para></summary>
		ErrInvalidDuplicateparaeter = 0x80041043,
		///<summary><para>Reserved for future use.</para></summary>
		ErrTooMuchData = 0x80041044,
		///<summary><para>A call to IWbemObjectSink::Indicate has failed. The provider may choose to refire the event.</para></summary>
		ErrServerTooBusy = 0x80041045,
		///<summary><para>The specified flavor was not valid.</para></summary>
		ErrInvalidFlavor = 0x80041046,
		///<summary><para>An attempt has been made to create a reference that is circular (for example, deriving a class from itself).</para></summary>
		ErrCircularReference = 0x80041047,
		///<summary><para>The specified class is not supported.</para></summary>
		ErrUnsupportedClassUpdate = 0x80041048,
		///<summary><para>An attempt was made to change a key when instances or subclasses are already using the key.</para></summary>
		ErrCannotChangeKeyInheritance = 0x80041049,
		///<summary><para>An attempt was made to change an index when instances or subclasses are already using the index.</para></summary>
		ErrCannotChangeIndexInheritance = 0x80041050,
		///<summary><para>An attempt was made to create more properties than the current version of the class supports.</para></summary>
		ErrTooManyProperties = 0x80041051,
		///<summary><para>A property was redefined with a conflicting type in a derived class.</para></summary>
		ErrUpdateTypeMismatch = 0x80041052,
		///<summary><para>An attempt was made in a derived class to override a non-overrideable qualifier.</para></summary>
		ErrUpdateOverrideNotAllowed = 0x80041053,
		///<summary><para>A method was redeclared with a conflicting signature in a derived class.</para></summary>
		ErrUpdatePropagatedMethod = 0x80041054,
		///<summary><para>An attempt was made to execute a method not marked with [implemented] in any relevant class.</para></summary>
		ErrMethodNotImplemented = 0x80041055,
		///<summary><para>An attempt was made to execute a method marked with [disabled].</para></summary>
		ErrMethodDisabled = 0x80041056,
		///<summary><para>The refresher is busy with another operation.</para></summary>
		ErrRefresherBusy = 0x80041057,
		///<summary><para>The filtering query is syntactically not valid.</para></summary>
		ErrUnparsableQuery = 0x80041058,
		///<summary><para>The FROM clause of a filtering query references a class that is not an event class (not derived from __Event).</para></summary>
		ErrNotEventClass = 0x80041059,
		///<summary><para>A GROUP BY clause was used without the corresponding GROUP WITHIN clause.</para></summary>
		ErrMissingGroupWithin = 0x8004105A,
		///<summary><para>A GROUP BY clause was used. Aggregation on all properties is not supported.</para></summary>
		ErrMissingAggregationList = 0x8004105B,
		///<summary><para>Dot notation was used on a property that is not an embedded object.</para></summary>
		ErrPropertyNotAnObject = 0x8004105C,
		///<summary><para>A GROUP BY clause references a property that is an embedded object without using dot notation.</para></summary>
		ErrAggregatingByObject = 0x8004105D,
		///<summary><para>An event provider registration query ( __EventProviderRegistration) did not specify the classes for which events were provided.</para></summary>
		ErrUninterpretableProviderQuery = 0x8004105F,
		///<summary><para>An request was made to back up or restore the repository while WMI was using it.</para></summary>
		ErrBackupRestoreWinmgmtRunning = 0x80041060,
		///<summary><para>The asynchronous delivery queue overflowed due to the event consumer being too slow.</para></summary>
		ErrQueueOverflow = 0x80041061,
		///<summary><para>The operation failed because the client did not have the necessary security privilege.</para></summary>
		ErrPrivilegeNotHeld = 0x80041062,
		///<summary><para>The operator is not valid for this property type.</para></summary>
		ErrInvalidOperator = 0x80041063,
		///<summary><para>The user specified a username, password or authority for a local connection. The user must use a blank username/password and rely on default security.</para></summary>
		ErrLocalCredentials = 0x80041064,
		///<summary><para>The class was made abstract when its parent class is not abstract.</para></summary>
		ErrCannotBeAbstract = 0x80041065,
		///<summary><para>An amended object was put without the FlagUseAmendedQualifiers flag being specified.</para></summary>
		ErrAmendedObject = 0x80041066,
		///<summary><para>Windows Server 2003 and Windows XP:  The client was not retrieving objects quickly enough from an enumeration. This constant is returned when a client creates an enumeration object but does not retrieve objects from the enumerator in a timely fashion, causing the enumerator's object caches to get backed up.</para></summary>
		ErrClientTooSlow = 0x80041067,
		///<summary><para>Windows Server 2003 and Windows XP:  A null security descriptor was used.</para></summary>
		ErrNullSecurityDescriptor = 0x80041068,
		///<summary><para>Windows Server 2003 and Windows XP:  The operation timed out.</para></summary>
		ErrTimeout = 0x80041069,
		///<summary><para>Windows Server 2003 and Windows XP:  The association being used is not valid.</para></summary>
		ErrInvalidAssociation = 0x8004106A,
		///<summary><para>Windows Server 2003 and Windows XP:  The operation was ambiguous.</para></summary>
		ErrAmbiguousOperation = 0x8004106B,
		///<summary><para>Windows Server 2003 and Windows XP:  WMI is taking up too much memory. This could be caused either by low memory availability or excessive memory consumption by WMI.</para></summary>
		ErrQuotaViolation = 0x8004106C,
		///<summary><para>Windows Server 2003 and Windows XP:  The operation resulted in a transaction conflict.</para></summary>
		ErrTransactionConflict = 0x8004106D,
		///<summary><para>Windows Server 2003 and Windows XP:  The transaction forced a rollback.</para></summary>
		ErrForcedRollback = 0x8004106E,
		///<summary><para>Windows Server 2003 and Windows XP:  The locale used in the call is not supported.</para></summary>
		ErrUnsupportedLocale = 0x8004106F,
		///<summary><para>Windows Server 2003 and Windows XP:  The object handle is out of date.</para></summary>
		ErrHandleOutOfDate = 0x80041070,
		///<summary><para>Windows Server 2003 and Windows XP:  Indicates that the connection to the SQL database failed.</para></summary>
		ErrConnectionFailed = 0x80041071,
		///<summary><para>Windows Server 2003 and Windows XP:  The handle request was not valid.</para></summary>
		ErrInvalidHandleRequest = 0x80041072,
		///<summary><para>Windows Server 2003 and Windows XP:  The property name contains more than 255 characters.</para></summary>
		ErrPropertyNameTooWide = 0x80041073,
		///<summary><para>Windows Server 2003 and Windows XP:  The class name contains more than 255 characters.</para></summary>
		ErrClassNameTooWide = 0x80041074,
		///<summary><para>Windows Server 2003 and Windows XP:  The method name contains more than 255 characters.</para></summary>
		ErrMethodNameTooWide = 0x80041075,
		///<summary><para>Windows Server 2003 and Windows XP:  The qualifier name contains more than 255 characters.</para></summary>
		ErrQualifierNameTooWide = 0x80041076,
		///<summary><para>Windows Server 2003 and Windows XP:  Indicates that an SQL command should be rerun because there is a deadlock in SQL. This can be returned only when data is being stored in an SQL database.</para></summary>
		ErrRerunCommand = 0x80041077,
		///<summary><para>Windows Server 2003 and Windows XP:  The database version does not match the version that the repository driver processes.</para></summary>
		ErrDatabaseVerMismatch = 0x80041078,
		///<summary><para>Windows Server 2003 and Windows XP:  WMI cannot do the delete operation because the provider does not allow it.</para></summary>
		ErrVetoDelete = 0x8004107A,
		///<summary><para>Windows Server 2003 and Windows XP:  WMI cannot do the put operation because the provider does not allow it.</para></summary>
		ErrVetoPut = 0x8004107A,
		///<summary><para>Windows Server 2003 and Windows XP:  The specified locale identifier was not valid for the operation.</para></summary>
		ErrInvalidLocale = 0x80041080,
		///<summary><para>Windows Server 2003 and Windows XP:  The provider is suspended.</para></summary>
		ErrProviderSuspended = 0x80041081,
		///<summary><para>Windows Server 2003 and Windows XP:  The object must be committed and retrieved again before the requested operation can succeed. This constant is returned when an object must be committed and re-retrieved to see the property value.</para></summary>
		ErrSynchronizationRequired = 0x80041082,
		///<summary><para>Windows Server 2003 and Windows XP:  The operation cannot be completed because no schema is available.</para></summary>
		ErrNoSchema = 0x80041083,
		///<summary><para>Windows Server 2003 and Windows XP:  The provider registration cannot be done because the provider is already registered.</para></summary>
		ErrProviderAlreadyRegistered = 0x80041084,
		///<summary><para>Windows Server 2003 and Windows XP:  The provider for the requested data is not registered.</para></summary>
		ErrProviderNotRegistered = 0x80041085,
		///<summary><para>Windows Server 2003 and Windows XP:  A fatal transport error occurred and other transport will not be attempted.</para></summary>
		ErrFatalTransportError = 0x80041086,
		///<summary><para>Windows Server 2003 and Windows XP:  The client connection to WINMGMT must be encrypted for this operation. The IWbemServices proxy security settings should be adjusted and the operation retried.</para></summary>
		ErrEncryptedConnectionRequired = 0x80041087,
		///<summary><para>Windows Server 2003 and Windows XP:  A provider failed to report results within the specified timeout.</para></summary>
		WBEM_E_PROVIDER_TIMED_OUT = 0x80041088,
		///<summary><para>Windows Server 2003 and Windows XP:  User attempted to put an instance with no defined key.</para></summary>
		WBEM_E_NO_KEY = 0x80041089,
		///<summary><para>Windows Server 2003 and Windows XP:  User attempted to register a provider instance but the COM server for the provider instance was unloaded.</para></summary>
		WBEM_E_PROVIDER_DISABLED = 0x8004108A,
		///<summary><para>Windows Server 2003 and Windows XP:  The provider registration overlaps with the system event domain.</para></summary>
		ErrRegistrationTooBroad = 0x80042001,
		///<summary><para>Windows Server 2003 and Windows XP:  A WITHIN clause was not used in this query.</para></summary>
		ErrRegistrationTooPrecise = 0x80042002,
		///<summary><para>Windows Server 2003 and Windows XP:  Automation-specific error.</para></summary>
		ErrTimedout = 0x80043001,
		///<summary><para>Windows Server 2003 and Windows XP:  The user deleted an override default value for the current class. The default value for this property in the parent class has been reactivated. An automation-specific error.</para></summary>
		ErrResetToDefault = 0x80043002,
	}
}
