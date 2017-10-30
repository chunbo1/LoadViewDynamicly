﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LoadViewDynamicly
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="MDH2")]
	public partial class DataClasses1DataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertSchedule(Schedule instance);
    partial void UpdateSchedule(Schedule instance);
    partial void DeleteSchedule(Schedule instance);
    partial void InsertTeacher(Teacher instance);
    partial void UpdateTeacher(Teacher instance);
    partial void DeleteTeacher(Teacher instance);
    partial void InsertClassStudent(ClassStudent instance);
    partial void UpdateClassStudent(ClassStudent instance);
    partial void DeleteClassStudent(ClassStudent instance);
    partial void InsertClass(Class instance);
    partial void UpdateClass(Class instance);
    partial void DeleteClass(Class instance);
    partial void InsertStudent(Student instance);
    partial void UpdateStudent(Student instance);
    partial void DeleteStudent(Student instance);
    #endregion
		
		public DataClasses1DataContext() : 
				base(global::LoadViewDynamicly.Properties.Settings.Default.MDH2ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Schedule> Schedules
		{
			get
			{
				return this.GetTable<Schedule>();
			}
		}
		
		public System.Data.Linq.Table<Teacher> Teachers
		{
			get
			{
				return this.GetTable<Teacher>();
			}
		}
		
		public System.Data.Linq.Table<ClassStudent> ClassStudents
		{
			get
			{
				return this.GetTable<ClassStudent>();
			}
		}
		
		public System.Data.Linq.Table<Class> Classes
		{
			get
			{
				return this.GetTable<Class>();
			}
		}
		
		public System.Data.Linq.Table<vwCST> vwCSTs
		{
			get
			{
				return this.GetTable<vwCST>();
			}
		}
		
		public System.Data.Linq.Table<Student> Students
		{
			get
			{
				return this.GetTable<Student>();
			}
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.UpdateCS")]
		public int UpdateCS([global::System.Data.Linq.Mapping.ParameterAttribute(Name="Id", DbType="Int")] System.Nullable<int> id, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="StudentId", DbType="Int")] System.Nullable<int> studentId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="ClassId", DbType="Int")] System.Nullable<int> classId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="TuitionPaid", DbType="Int")] System.Nullable<int> tuitionPaid, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Comment", DbType="VarChar(50)")] string comment)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), id, studentId, classId, tuitionPaid, comment);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.AddCS")]
		public int AddCS([global::System.Data.Linq.Mapping.ParameterAttribute(Name="StudentId", DbType="Int")] System.Nullable<int> studentId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="ClassId", DbType="Int")] System.Nullable<int> classId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="TuitionPaid", DbType="Int")] System.Nullable<int> tuitionPaid, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Comment", DbType="VarChar(50)")] string comment, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Id", DbType="Int")] ref System.Nullable<int> id)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), studentId, classId, tuitionPaid, comment, id);
			id = ((System.Nullable<int>)(result.GetParameterValue(4)));
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.DeleteCS")]
		public int DeleteCS([global::System.Data.Linq.Mapping.ParameterAttribute(Name="Id", DbType="Int")] System.Nullable<int> id)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), id);
			return ((int)(result.ReturnValue));
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Schedules")]
	public partial class Schedule : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private System.Nullable<int> _ClassId;
		
		private System.Nullable<int> _TeacherId;
		
		private string _Weekday;
		
		private System.Nullable<System.DateTime> _StartTime;
		
		private System.Nullable<System.DateTime> _EndTime;
		
		private string _Status;
		
		private string _Comment;
		
		private System.DateTime _UpdateDateTime;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnClassIdChanging(System.Nullable<int> value);
    partial void OnClassIdChanged();
    partial void OnTeacherIdChanging(System.Nullable<int> value);
    partial void OnTeacherIdChanged();
    partial void OnWeekdayChanging(string value);
    partial void OnWeekdayChanged();
    partial void OnStartTimeChanging(System.Nullable<System.DateTime> value);
    partial void OnStartTimeChanged();
    partial void OnEndTimeChanging(System.Nullable<System.DateTime> value);
    partial void OnEndTimeChanged();
    partial void OnStatusChanging(string value);
    partial void OnStatusChanged();
    partial void OnCommentChanging(string value);
    partial void OnCommentChanged();
    partial void OnUpdateDateTimeChanging(System.DateTime value);
    partial void OnUpdateDateTimeChanged();
    #endregion
		
		public Schedule()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ClassId", DbType="Int")]
		public System.Nullable<int> ClassId
		{
			get
			{
				return this._ClassId;
			}
			set
			{
				if ((this._ClassId != value))
				{
					this.OnClassIdChanging(value);
					this.SendPropertyChanging();
					this._ClassId = value;
					this.SendPropertyChanged("ClassId");
					this.OnClassIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TeacherId", DbType="Int")]
		public System.Nullable<int> TeacherId
		{
			get
			{
				return this._TeacherId;
			}
			set
			{
				if ((this._TeacherId != value))
				{
					this.OnTeacherIdChanging(value);
					this.SendPropertyChanging();
					this._TeacherId = value;
					this.SendPropertyChanged("TeacherId");
					this.OnTeacherIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Weekday", DbType="VarChar(20)")]
		public string Weekday
		{
			get
			{
				return this._Weekday;
			}
			set
			{
				if ((this._Weekday != value))
				{
					this.OnWeekdayChanging(value);
					this.SendPropertyChanging();
					this._Weekday = value;
					this.SendPropertyChanged("Weekday");
					this.OnWeekdayChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_StartTime", DbType="DateTime")]
		public System.Nullable<System.DateTime> StartTime
		{
			get
			{
				return this._StartTime;
			}
			set
			{
				if ((this._StartTime != value))
				{
					this.OnStartTimeChanging(value);
					this.SendPropertyChanging();
					this._StartTime = value;
					this.SendPropertyChanged("StartTime");
					this.OnStartTimeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EndTime", DbType="DateTime")]
		public System.Nullable<System.DateTime> EndTime
		{
			get
			{
				return this._EndTime;
			}
			set
			{
				if ((this._EndTime != value))
				{
					this.OnEndTimeChanging(value);
					this.SendPropertyChanging();
					this._EndTime = value;
					this.SendPropertyChanged("EndTime");
					this.OnEndTimeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Status", DbType="VarChar(50)")]
		public string Status
		{
			get
			{
				return this._Status;
			}
			set
			{
				if ((this._Status != value))
				{
					this.OnStatusChanging(value);
					this.SendPropertyChanging();
					this._Status = value;
					this.SendPropertyChanged("Status");
					this.OnStatusChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Comment", DbType="VarChar(250)")]
		public string Comment
		{
			get
			{
				return this._Comment;
			}
			set
			{
				if ((this._Comment != value))
				{
					this.OnCommentChanging(value);
					this.SendPropertyChanging();
					this._Comment = value;
					this.SendPropertyChanged("Comment");
					this.OnCommentChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UpdateDateTime", DbType="DateTime NOT NULL")]
		public System.DateTime UpdateDateTime
		{
			get
			{
				return this._UpdateDateTime;
			}
			set
			{
				if ((this._UpdateDateTime != value))
				{
					this.OnUpdateDateTimeChanging(value);
					this.SendPropertyChanging();
					this._UpdateDateTime = value;
					this.SendPropertyChanged("UpdateDateTime");
					this.OnUpdateDateTimeChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Teachers")]
	public partial class Teacher : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private string _FirstName;
		
		private string _LastName;
		
		private string _Address;
		
		private string _Email;
		
		private string _HomePhone;
		
		private string _CellPhone;
		
		private System.DateTime _UpdateDateTime;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnFirstNameChanging(string value);
    partial void OnFirstNameChanged();
    partial void OnLastNameChanging(string value);
    partial void OnLastNameChanged();
    partial void OnAddressChanging(string value);
    partial void OnAddressChanged();
    partial void OnEmailChanging(string value);
    partial void OnEmailChanged();
    partial void OnHomePhoneChanging(string value);
    partial void OnHomePhoneChanged();
    partial void OnCellPhoneChanging(string value);
    partial void OnCellPhoneChanged();
    partial void OnUpdateDateTimeChanging(System.DateTime value);
    partial void OnUpdateDateTimeChanged();
    #endregion
		
		public Teacher()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FirstName", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string FirstName
		{
			get
			{
				return this._FirstName;
			}
			set
			{
				if ((this._FirstName != value))
				{
					this.OnFirstNameChanging(value);
					this.SendPropertyChanging();
					this._FirstName = value;
					this.SendPropertyChanged("FirstName");
					this.OnFirstNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastName", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string LastName
		{
			get
			{
				return this._LastName;
			}
			set
			{
				if ((this._LastName != value))
				{
					this.OnLastNameChanging(value);
					this.SendPropertyChanging();
					this._LastName = value;
					this.SendPropertyChanged("LastName");
					this.OnLastNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Address", DbType="VarChar(50)")]
		public string Address
		{
			get
			{
				return this._Address;
			}
			set
			{
				if ((this._Address != value))
				{
					this.OnAddressChanging(value);
					this.SendPropertyChanging();
					this._Address = value;
					this.SendPropertyChanged("Address");
					this.OnAddressChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Email", DbType="VarChar(50)")]
		public string Email
		{
			get
			{
				return this._Email;
			}
			set
			{
				if ((this._Email != value))
				{
					this.OnEmailChanging(value);
					this.SendPropertyChanging();
					this._Email = value;
					this.SendPropertyChanged("Email");
					this.OnEmailChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HomePhone", DbType="VarChar(50)")]
		public string HomePhone
		{
			get
			{
				return this._HomePhone;
			}
			set
			{
				if ((this._HomePhone != value))
				{
					this.OnHomePhoneChanging(value);
					this.SendPropertyChanging();
					this._HomePhone = value;
					this.SendPropertyChanged("HomePhone");
					this.OnHomePhoneChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CellPhone", DbType="VarChar(50)")]
		public string CellPhone
		{
			get
			{
				return this._CellPhone;
			}
			set
			{
				if ((this._CellPhone != value))
				{
					this.OnCellPhoneChanging(value);
					this.SendPropertyChanging();
					this._CellPhone = value;
					this.SendPropertyChanged("CellPhone");
					this.OnCellPhoneChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UpdateDateTime", DbType="DateTime NOT NULL")]
		public System.DateTime UpdateDateTime
		{
			get
			{
				return this._UpdateDateTime;
			}
			set
			{
				if ((this._UpdateDateTime != value))
				{
					this.OnUpdateDateTimeChanging(value);
					this.SendPropertyChanging();
					this._UpdateDateTime = value;
					this.SendPropertyChanged("UpdateDateTime");
					this.OnUpdateDateTimeChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.ClassStudents")]
	public partial class ClassStudent : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private System.Nullable<int> _StudentId;
		
		private System.Nullable<int> _ClassId;
		
		private System.Nullable<int> _TuitionPaid;
		
		private string _Comment;
		
		private System.DateTime _UpdateDateTime;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnStudentIdChanging(System.Nullable<int> value);
    partial void OnStudentIdChanged();
    partial void OnClassIdChanging(System.Nullable<int> value);
    partial void OnClassIdChanged();
    partial void OnTuitionPaidChanging(System.Nullable<int> value);
    partial void OnTuitionPaidChanged();
    partial void OnCommentChanging(string value);
    partial void OnCommentChanged();
    partial void OnUpdateDateTimeChanging(System.DateTime value);
    partial void OnUpdateDateTimeChanged();
    #endregion
		
		public ClassStudent()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_StudentId", DbType="Int")]
		public System.Nullable<int> StudentId
		{
			get
			{
				return this._StudentId;
			}
			set
			{
				if ((this._StudentId != value))
				{
					this.OnStudentIdChanging(value);
					this.SendPropertyChanging();
					this._StudentId = value;
					this.SendPropertyChanged("StudentId");
					this.OnStudentIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ClassId", DbType="Int")]
		public System.Nullable<int> ClassId
		{
			get
			{
				return this._ClassId;
			}
			set
			{
				if ((this._ClassId != value))
				{
					this.OnClassIdChanging(value);
					this.SendPropertyChanging();
					this._ClassId = value;
					this.SendPropertyChanged("ClassId");
					this.OnClassIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TuitionPaid", DbType="Int")]
		public System.Nullable<int> TuitionPaid
		{
			get
			{
				return this._TuitionPaid;
			}
			set
			{
				if ((this._TuitionPaid != value))
				{
					this.OnTuitionPaidChanging(value);
					this.SendPropertyChanging();
					this._TuitionPaid = value;
					this.SendPropertyChanged("TuitionPaid");
					this.OnTuitionPaidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Comment", DbType="VarChar(250)")]
		public string Comment
		{
			get
			{
				return this._Comment;
			}
			set
			{
				if ((this._Comment != value))
				{
					this.OnCommentChanging(value);
					this.SendPropertyChanging();
					this._Comment = value;
					this.SendPropertyChanged("Comment");
					this.OnCommentChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UpdateDateTime", DbType="DateTime NOT NULL")]
		public System.DateTime UpdateDateTime
		{
			get
			{
				return this._UpdateDateTime;
			}
			set
			{
				if ((this._UpdateDateTime != value))
				{
					this.OnUpdateDateTimeChanging(value);
					this.SendPropertyChanging();
					this._UpdateDateTime = value;
					this.SendPropertyChanged("UpdateDateTime");
					this.OnUpdateDateTimeChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Classes")]
	public partial class Class : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private string _Division;
		
		private string _ClassName;
		
		private string _Location;
		
		private string _Semester;
		
		private string _Dayofweek;
		
		private string _Timeofweek;
		
		private System.Nullable<double> _Tuition;
		
		private System.Nullable<int> _TeacherId;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnDivisionChanging(string value);
    partial void OnDivisionChanged();
    partial void OnClassNameChanging(string value);
    partial void OnClassNameChanged();
    partial void OnLocationChanging(string value);
    partial void OnLocationChanged();
    partial void OnSemesterChanging(string value);
    partial void OnSemesterChanged();
    partial void OnDayofweekChanging(string value);
    partial void OnDayofweekChanged();
    partial void OnTimeofweekChanging(string value);
    partial void OnTimeofweekChanged();
    partial void OnTuitionChanging(System.Nullable<double> value);
    partial void OnTuitionChanged();
    partial void OnTeacherIdChanging(System.Nullable<int> value);
    partial void OnTeacherIdChanged();
    #endregion
		
		public Class()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Division", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Division
		{
			get
			{
				return this._Division;
			}
			set
			{
				if ((this._Division != value))
				{
					this.OnDivisionChanging(value);
					this.SendPropertyChanging();
					this._Division = value;
					this.SendPropertyChanged("Division");
					this.OnDivisionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ClassName", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string ClassName
		{
			get
			{
				return this._ClassName;
			}
			set
			{
				if ((this._ClassName != value))
				{
					this.OnClassNameChanging(value);
					this.SendPropertyChanging();
					this._ClassName = value;
					this.SendPropertyChanged("ClassName");
					this.OnClassNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Location", DbType="VarChar(50)")]
		public string Location
		{
			get
			{
				return this._Location;
			}
			set
			{
				if ((this._Location != value))
				{
					this.OnLocationChanging(value);
					this.SendPropertyChanging();
					this._Location = value;
					this.SendPropertyChanged("Location");
					this.OnLocationChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Semester", DbType="VarChar(50)")]
		public string Semester
		{
			get
			{
				return this._Semester;
			}
			set
			{
				if ((this._Semester != value))
				{
					this.OnSemesterChanging(value);
					this.SendPropertyChanging();
					this._Semester = value;
					this.SendPropertyChanged("Semester");
					this.OnSemesterChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Dayofweek", DbType="VarChar(50)")]
		public string Dayofweek
		{
			get
			{
				return this._Dayofweek;
			}
			set
			{
				if ((this._Dayofweek != value))
				{
					this.OnDayofweekChanging(value);
					this.SendPropertyChanging();
					this._Dayofweek = value;
					this.SendPropertyChanged("Dayofweek");
					this.OnDayofweekChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Timeofweek", DbType="VarChar(50)")]
		public string Timeofweek
		{
			get
			{
				return this._Timeofweek;
			}
			set
			{
				if ((this._Timeofweek != value))
				{
					this.OnTimeofweekChanging(value);
					this.SendPropertyChanging();
					this._Timeofweek = value;
					this.SendPropertyChanged("Timeofweek");
					this.OnTimeofweekChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Tuition", DbType="Float")]
		public System.Nullable<double> Tuition
		{
			get
			{
				return this._Tuition;
			}
			set
			{
				if ((this._Tuition != value))
				{
					this.OnTuitionChanging(value);
					this.SendPropertyChanging();
					this._Tuition = value;
					this.SendPropertyChanged("Tuition");
					this.OnTuitionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TeacherId", DbType="Int")]
		public System.Nullable<int> TeacherId
		{
			get
			{
				return this._TeacherId;
			}
			set
			{
				if ((this._TeacherId != value))
				{
					this.OnTeacherIdChanging(value);
					this.SendPropertyChanging();
					this._TeacherId = value;
					this.SendPropertyChanged("TeacherId");
					this.OnTeacherIdChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.vwCST")]
	public partial class vwCST
	{
		
		private string _Division;
		
		private string _ClassName;
		
		private string _Location;
		
		private string _Semester;
		
		private string _Dayofweek;
		
		private string _Timeofweek;
		
		private System.Nullable<double> _Tuition;
		
		private string _Teacher;
		
		private System.Nullable<int> _StudentId;
		
		private string _Student;
		
		public vwCST()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Division", DbType="VarChar(50)")]
		public string Division
		{
			get
			{
				return this._Division;
			}
			set
			{
				if ((this._Division != value))
				{
					this._Division = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ClassName", DbType="VarChar(50)")]
		public string ClassName
		{
			get
			{
				return this._ClassName;
			}
			set
			{
				if ((this._ClassName != value))
				{
					this._ClassName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Location", DbType="VarChar(50)")]
		public string Location
		{
			get
			{
				return this._Location;
			}
			set
			{
				if ((this._Location != value))
				{
					this._Location = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Semester", DbType="VarChar(50)")]
		public string Semester
		{
			get
			{
				return this._Semester;
			}
			set
			{
				if ((this._Semester != value))
				{
					this._Semester = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Dayofweek", DbType="VarChar(50)")]
		public string Dayofweek
		{
			get
			{
				return this._Dayofweek;
			}
			set
			{
				if ((this._Dayofweek != value))
				{
					this._Dayofweek = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Timeofweek", DbType="VarChar(50)")]
		public string Timeofweek
		{
			get
			{
				return this._Timeofweek;
			}
			set
			{
				if ((this._Timeofweek != value))
				{
					this._Timeofweek = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Tuition", DbType="Float")]
		public System.Nullable<double> Tuition
		{
			get
			{
				return this._Tuition;
			}
			set
			{
				if ((this._Tuition != value))
				{
					this._Tuition = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Teacher", DbType="VarChar(101)")]
		public string Teacher
		{
			get
			{
				return this._Teacher;
			}
			set
			{
				if ((this._Teacher != value))
				{
					this._Teacher = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_StudentId", DbType="Int")]
		public System.Nullable<int> StudentId
		{
			get
			{
				return this._StudentId;
			}
			set
			{
				if ((this._StudentId != value))
				{
					this._StudentId = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Student", DbType="VarChar(101)")]
		public string Student
		{
			get
			{
				return this._Student;
			}
			set
			{
				if ((this._Student != value))
				{
					this._Student = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Students")]
	public partial class Student : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private string _FirstName;
		
		private string _LastName;
		
		private string _Gender;
		
		private string _BirthDate;
		
		private string _ContactName;
		
		private string _Address;
		
		private string _Email;
		
		private string _CellPhone;
		
		private string _HomePhone;
		
		private string _StudentPhone;
		
		private string _Comment;
		
		private System.Nullable<System.DateTime> _UpdateDateTime;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnFirstNameChanging(string value);
    partial void OnFirstNameChanged();
    partial void OnLastNameChanging(string value);
    partial void OnLastNameChanged();
    partial void OnGenderChanging(string value);
    partial void OnGenderChanged();
    partial void OnBirthDateChanging(string value);
    partial void OnBirthDateChanged();
    partial void OnContactNameChanging(string value);
    partial void OnContactNameChanged();
    partial void OnAddressChanging(string value);
    partial void OnAddressChanged();
    partial void OnEmailChanging(string value);
    partial void OnEmailChanged();
    partial void OnCellPhoneChanging(string value);
    partial void OnCellPhoneChanged();
    partial void OnHomePhoneChanging(string value);
    partial void OnHomePhoneChanged();
    partial void OnStudentPhoneChanging(string value);
    partial void OnStudentPhoneChanged();
    partial void OnCommentChanging(string value);
    partial void OnCommentChanged();
    partial void OnUpdateDateTimeChanging(System.Nullable<System.DateTime> value);
    partial void OnUpdateDateTimeChanged();
    #endregion
		
		public Student()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FirstName", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string FirstName
		{
			get
			{
				return this._FirstName;
			}
			set
			{
				if ((this._FirstName != value))
				{
					this.OnFirstNameChanging(value);
					this.SendPropertyChanging();
					this._FirstName = value;
					this.SendPropertyChanged("FirstName");
					this.OnFirstNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastName", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string LastName
		{
			get
			{
				return this._LastName;
			}
			set
			{
				if ((this._LastName != value))
				{
					this.OnLastNameChanging(value);
					this.SendPropertyChanging();
					this._LastName = value;
					this.SendPropertyChanged("LastName");
					this.OnLastNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Gender", DbType="VarChar(2)")]
		public string Gender
		{
			get
			{
				return this._Gender;
			}
			set
			{
				if ((this._Gender != value))
				{
					this.OnGenderChanging(value);
					this.SendPropertyChanging();
					this._Gender = value;
					this.SendPropertyChanged("Gender");
					this.OnGenderChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BirthDate", DbType="VarChar(50)")]
		public string BirthDate
		{
			get
			{
				return this._BirthDate;
			}
			set
			{
				if ((this._BirthDate != value))
				{
					this.OnBirthDateChanging(value);
					this.SendPropertyChanging();
					this._BirthDate = value;
					this.SendPropertyChanged("BirthDate");
					this.OnBirthDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ContactName", DbType="VarChar(50)")]
		public string ContactName
		{
			get
			{
				return this._ContactName;
			}
			set
			{
				if ((this._ContactName != value))
				{
					this.OnContactNameChanging(value);
					this.SendPropertyChanging();
					this._ContactName = value;
					this.SendPropertyChanged("ContactName");
					this.OnContactNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Address", DbType="VarChar(50)")]
		public string Address
		{
			get
			{
				return this._Address;
			}
			set
			{
				if ((this._Address != value))
				{
					this.OnAddressChanging(value);
					this.SendPropertyChanging();
					this._Address = value;
					this.SendPropertyChanged("Address");
					this.OnAddressChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Email", DbType="VarChar(50)")]
		public string Email
		{
			get
			{
				return this._Email;
			}
			set
			{
				if ((this._Email != value))
				{
					this.OnEmailChanging(value);
					this.SendPropertyChanging();
					this._Email = value;
					this.SendPropertyChanged("Email");
					this.OnEmailChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CellPhone", DbType="VarChar(50)")]
		public string CellPhone
		{
			get
			{
				return this._CellPhone;
			}
			set
			{
				if ((this._CellPhone != value))
				{
					this.OnCellPhoneChanging(value);
					this.SendPropertyChanging();
					this._CellPhone = value;
					this.SendPropertyChanged("CellPhone");
					this.OnCellPhoneChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HomePhone", DbType="VarChar(50)")]
		public string HomePhone
		{
			get
			{
				return this._HomePhone;
			}
			set
			{
				if ((this._HomePhone != value))
				{
					this.OnHomePhoneChanging(value);
					this.SendPropertyChanging();
					this._HomePhone = value;
					this.SendPropertyChanged("HomePhone");
					this.OnHomePhoneChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_StudentPhone", DbType="VarChar(50)")]
		public string StudentPhone
		{
			get
			{
				return this._StudentPhone;
			}
			set
			{
				if ((this._StudentPhone != value))
				{
					this.OnStudentPhoneChanging(value);
					this.SendPropertyChanging();
					this._StudentPhone = value;
					this.SendPropertyChanged("StudentPhone");
					this.OnStudentPhoneChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Comment", DbType="VarChar(250)")]
		public string Comment
		{
			get
			{
				return this._Comment;
			}
			set
			{
				if ((this._Comment != value))
				{
					this.OnCommentChanging(value);
					this.SendPropertyChanging();
					this._Comment = value;
					this.SendPropertyChanged("Comment");
					this.OnCommentChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UpdateDateTime", DbType="DateTime")]
		public System.Nullable<System.DateTime> UpdateDateTime
		{
			get
			{
				return this._UpdateDateTime;
			}
			set
			{
				if ((this._UpdateDateTime != value))
				{
					this.OnUpdateDateTimeChanging(value);
					this.SendPropertyChanging();
					this._UpdateDateTime = value;
					this.SendPropertyChanged("UpdateDateTime");
					this.OnUpdateDateTimeChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
