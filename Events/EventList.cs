using System;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Data;
using System.Diagnostics;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.ComponentModel;

namespace Tasker.Events
{
	/// <summary>
	///Represents the strongly named DataTable class.
	///</summary>
	[Serializable()]
	[XmlSchemaProviderAttribute("GetTypedTableSchema")]
	public partial class EventList : TypedTableBase<EventListRow>
	{

		private DataColumn columnName;

		private DataColumn columnText;

		private DataColumn columnDescription;

		private DataColumn columnType;

		private DataColumn columnPossibleValues;

		/// <summary></summary>
		[DebuggerNonUserCodeAttribute()]
		[GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public EventList()
		{
			this.TableName = "EventList";
			this.BeginInit();
			this.InitClass();
			this.EndInit();
		}

		[DebuggerNonUserCodeAttribute()]
		[GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		internal EventList(DataTable table)
		{
			this.TableName = table.TableName;
			if ((table.CaseSensitive != table.DataSet.CaseSensitive))
			{
				this.CaseSensitive = table.CaseSensitive;
			}
			if ((table.Locale.ToString() != table.DataSet.Locale.ToString()))
			{
				this.Locale = table.Locale;
			}
			if ((table.Namespace != table.DataSet.Namespace))
			{
				this.Namespace = table.Namespace;
			}
			this.Prefix = table.Prefix;
			this.MinimumCapacity = table.MinimumCapacity;
		}

		/// <summary></summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		[DebuggerNonUserCodeAttribute()]
		[GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected EventList(SerializationInfo info, StreamingContext context) :
			base(info, context)
		{
			this.InitVars();
		}

		/// <summary></summary>
		[DebuggerNonUserCodeAttribute()]
		[GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn NameColumn
		{
			get
			{
				return this.columnName;
			}
		}

		/// <summary></summary>
		[DebuggerNonUserCodeAttribute()]
		[GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn TextColumn
		{
			get
			{
				return this.columnText;
			}
		}

		/// <summary></summary>
		[DebuggerNonUserCodeAttribute()]
		[GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn DescriptionColumn
		{
			get
			{
				return this.columnDescription;
			}
		}

		/// <summary></summary>
		[DebuggerNonUserCodeAttribute()]
		[GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn TypeColumn
		{
			get
			{
				return this.columnType;
			}
		}

		/// <summary></summary>
		[DebuggerNonUserCodeAttribute()]
		[GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn PossibleValuesColumn
		{
			get
			{
				return this.columnPossibleValues;
			}
		}

		/// <summary></summary>
		[DebuggerNonUserCodeAttribute()]
		[GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[Browsable(false)]
		public int Count
		{
			get
			{
				return this.Rows.Count;
			}
		}

		/// <summary></summary>
		[DebuggerNonUserCodeAttribute()]
		[GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public EventListRow this[int index]
		{
			get
			{
				return ((EventListRow)(this.Rows[index]));
			}
		}

		/// <summary></summary>
		[DebuggerNonUserCodeAttribute()]
		[GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void AddEventListRow(EventListRow row)
		{
			this.Rows.Add(row);
		}

		/// <summary></summary>
		[DebuggerNonUserCodeAttribute()]
		[GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public EventListRow AddEventListRow(string Name, string Text, string Description, sbyte Type, string[] PossibleValues)
		{
			EventListRow rowEventListRow = ((EventListRow)(this.NewRow()));
			object[] columnValuesArray = new object[] {
                        Name,
                        Text,
                        Description,
                        Type,
                        PossibleValues};
			rowEventListRow.ItemArray = columnValuesArray;
			this.Rows.Add(rowEventListRow);
			return rowEventListRow;
		}

		/// <summary></summary>
		[DebuggerNonUserCodeAttribute()]
		[GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public override DataTable Clone()
		{
			EventList cln = ((EventList)(base.Clone()));
			cln.InitVars();
			return cln;
		}

		/// <summary></summary>
		/// <returns></returns>
		[DebuggerNonUserCodeAttribute()]
		[GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected override DataTable CreateInstance()
		{
			return new EventList();
		}

		[DebuggerNonUserCodeAttribute()]
		[GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		internal void InitVars()
		{
			this.columnName = base.Columns["Name"];
			this.columnText = base.Columns["Text"];
			this.columnDescription = base.Columns["Description"];
			this.columnType = base.Columns["Type"];
			this.columnPossibleValues = base.Columns["PossibleValues"];
		}

		[DebuggerNonUserCodeAttribute()]
		[GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		private void InitClass()
		{
			this.columnName = new DataColumn("Name", typeof(string), null, MappingType.Element);
			base.Columns.Add(this.columnName);
			this.columnText = new DataColumn("Text", typeof(string), null, MappingType.Element);
			base.Columns.Add(this.columnText);
			this.columnDescription = new DataColumn("Description", typeof(string), null, MappingType.Element);
			base.Columns.Add(this.columnDescription);
			this.columnType = new DataColumn("Type", typeof(sbyte), null, MappingType.Element);
			base.Columns.Add(this.columnType);
			this.columnPossibleValues = new DataColumn("PossibleValues", typeof(string[]), null, MappingType.Element);
			base.Columns.Add(this.columnPossibleValues);
			this.Constraints.Add(new UniqueConstraint("Constraint1", new DataColumn[] {
                                this.columnName}, false));
			this.columnName.AllowDBNull = false;
			this.columnName.Unique = true;
			this.columnText.AllowDBNull = false;
			this.columnType.AllowDBNull = false;
		}

		/// <summary></summary>
		[DebuggerNonUserCodeAttribute()]
		[GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public EventListRow NewEventListRow()
		{
			return ((EventListRow)(this.NewRow()));
		}

		/// <summary></summary>
		/// <param name="builder"></param>
		/// <returns></returns>
		[DebuggerNonUserCodeAttribute()]
		[GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
		{
			return new EventListRow(builder);
		}

		/// <summary></summary>
		/// <returns></returns>
		[DebuggerNonUserCodeAttribute()]
		[GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected override Type GetRowType()
		{
			return typeof(EventListRow);
		}

		/// <summary></summary>
		[DebuggerNonUserCodeAttribute()]
		[GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void RemoveEventListRow(EventListRow row)
		{
			this.Rows.Remove(row);
		}
	}

	/// <summary>
	///Represents strongly named DataRow class.
	///</summary>
	public partial class EventListRow : DataRow
	{

		private EventList tableEventList;

		[DebuggerNonUserCodeAttribute()]
		[GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		internal EventListRow(DataRowBuilder rb) :
			base(rb)
		{
			this.tableEventList = ((EventList)(this.Table));
		}

		/// <summary></summary>
		[DebuggerNonUserCodeAttribute()]
		[GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public string Name
		{
			get
			{
				return ((string)(this[this.tableEventList.NameColumn]));
			}
			set
			{
				this[this.tableEventList.NameColumn] = value;
			}
		}

		/// <summary></summary>
		[DebuggerNonUserCodeAttribute()]
		[GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public string Text
		{
			get
			{
				return ((string)(this[this.tableEventList.TextColumn]));
			}
			set
			{
				this[this.tableEventList.TextColumn] = value;
			}
		}

		/// <summary></summary>
		[DebuggerNonUserCodeAttribute()]
		[GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public string Description
		{
			get
			{
				try
				{
					return ((string)(this[this.tableEventList.DescriptionColumn]));
				}
				catch (InvalidCastException e)
				{
					throw new StrongTypingException("Der Wert für Spalte Description in Tabelle EventList ist DBNull.", e);
				}
			}
			set
			{
				this[this.tableEventList.DescriptionColumn] = value;
			}
		}

		/// <summary></summary>
		[DebuggerNonUserCodeAttribute()]
		[GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public Manager.EventTypes Type
		{
			get
			{
				return ((Manager.EventTypes)(this[this.tableEventList.TypeColumn]));
			}
			set
			{
				this[this.tableEventList.TypeColumn] = value;
			}
		}

		/// <summary></summary>
		[DebuggerNonUserCodeAttribute()]
		[GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public string[] PossibleValues
		{
			get
			{
				try
				{
					return ((string[])(this[this.tableEventList.PossibleValuesColumn]));
				}
				catch (InvalidCastException e)
				{
					throw new StrongTypingException("Der Wert für Spalte PossibleValues in Tabelle EventList ist DBNull.", e);
				}
			}
			set
			{
				this[this.tableEventList.PossibleValuesColumn] = value;
			}
		}

		/// <summary></summary>
		[DebuggerNonUserCodeAttribute()]
		[GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsDescriptionNull()
		{
			return this.IsNull(this.tableEventList.DescriptionColumn);
		}

		/// <summary></summary>
		[DebuggerNonUserCodeAttribute()]
		[GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetDescriptionNull()
		{
			this[this.tableEventList.DescriptionColumn] = Convert.DBNull;
		}

		/// <summary></summary>
		[DebuggerNonUserCodeAttribute()]
		[GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsPossibleValuesNull()
		{
			return this.IsNull(this.tableEventList.PossibleValuesColumn);
		}

		/// <summary></summary>
		[DebuggerNonUserCodeAttribute()]
		[GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetPossibleValuesNull()
		{
			this[this.tableEventList.PossibleValuesColumn] = Convert.DBNull;
		}
	}
        
}
