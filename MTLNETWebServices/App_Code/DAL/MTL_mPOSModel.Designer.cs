﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[assembly: EdmSchemaAttribute()]
namespace DAL
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class MTL_mPOSEntities : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new MTL_mPOSEntities object using the connection string found in the 'MTL_mPOSEntities' section of the application configuration file.
        /// </summary>
        public MTL_mPOSEntities() : base("name=MTL_mPOSEntities", "MTL_mPOSEntities")
        {
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new MTL_mPOSEntities object.
        /// </summary>
        public MTL_mPOSEntities(string connectionString) : base(connectionString, "MTL_mPOSEntities")
        {
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new MTL_mPOSEntities object.
        /// </summary>
        public MTL_mPOSEntities(EntityConnection connection) : base(connection, "MTL_mPOSEntities")
        {
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<TemporaryReceiptUsingLog> TemporaryReceiptUsingLog
        {
            get
            {
                if ((_TemporaryReceiptUsingLog == null))
                {
                    _TemporaryReceiptUsingLog = base.CreateObjectSet<TemporaryReceiptUsingLog>("TemporaryReceiptUsingLog");
                }
                return _TemporaryReceiptUsingLog;
            }
        }
        private ObjectSet<TemporaryReceiptUsingLog> _TemporaryReceiptUsingLog;

        #endregion

        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the TemporaryReceiptUsingLog EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToTemporaryReceiptUsingLog(TemporaryReceiptUsingLog temporaryReceiptUsingLog)
        {
            base.AddObject("TemporaryReceiptUsingLog", temporaryReceiptUsingLog);
        }

        #endregion

    }

    #endregion

    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="MTL_mPOSModel", Name="TemporaryReceiptUsingLog")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class TemporaryReceiptUsingLog : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new TemporaryReceiptUsingLog object.
        /// </summary>
        /// <param name="rid">Initial value of the rid property.</param>
        public static TemporaryReceiptUsingLog CreateTemporaryReceiptUsingLog(global::System.Int32 rid)
        {
            TemporaryReceiptUsingLog temporaryReceiptUsingLog = new TemporaryReceiptUsingLog();
            temporaryReceiptUsingLog.rid = rid;
            return temporaryReceiptUsingLog;
        }

        #endregion

        #region Simple Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 rid
        {
            get
            {
                return _rid;
            }
            set
            {
                if (_rid != value)
                {
                    OnridChanging(value);
                    ReportPropertyChanging("rid");
                    _rid = StructuralObject.SetValidValue(value, "rid");
                    ReportPropertyChanged("rid");
                    OnridChanged();
                }
            }
        }
        private global::System.Int32 _rid;
        partial void OnridChanging(global::System.Int32 value);
        partial void OnridChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String TemporaryReceiptNumber
        {
            get
            {
                return _TemporaryReceiptNumber;
            }
            set
            {
                OnTemporaryReceiptNumberChanging(value);
                ReportPropertyChanging("TemporaryReceiptNumber");
                _TemporaryReceiptNumber = StructuralObject.SetValidValue(value, true, "TemporaryReceiptNumber");
                ReportPropertyChanged("TemporaryReceiptNumber");
                OnTemporaryReceiptNumberChanged();
            }
        }
        private global::System.String _TemporaryReceiptNumber;
        partial void OnTemporaryReceiptNumberChanging(global::System.String value);
        partial void OnTemporaryReceiptNumberChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> TemporaryReceiptDate
        {
            get
            {
                return _TemporaryReceiptDate;
            }
            set
            {
                OnTemporaryReceiptDateChanging(value);
                ReportPropertyChanging("TemporaryReceiptDate");
                _TemporaryReceiptDate = StructuralObject.SetValidValue(value, "TemporaryReceiptDate");
                ReportPropertyChanged("TemporaryReceiptDate");
                OnTemporaryReceiptDateChanged();
            }
        }
        private Nullable<global::System.DateTime> _TemporaryReceiptDate;
        partial void OnTemporaryReceiptDateChanging(Nullable<global::System.DateTime> value);
        partial void OnTemporaryReceiptDateChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String PayFor
        {
            get
            {
                return _PayFor;
            }
            set
            {
                OnPayForChanging(value);
                ReportPropertyChanging("PayFor");
                _PayFor = StructuralObject.SetValidValue(value, true, "PayFor");
                ReportPropertyChanged("PayFor");
                OnPayForChanged();
            }
        }
        private global::System.String _PayFor;
        partial void OnPayForChanging(global::System.String value);
        partial void OnPayForChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String PayForNumber
        {
            get
            {
                return _PayForNumber;
            }
            set
            {
                OnPayForNumberChanging(value);
                ReportPropertyChanging("PayForNumber");
                _PayForNumber = StructuralObject.SetValidValue(value, true, "PayForNumber");
                ReportPropertyChanged("PayForNumber");
                OnPayForNumberChanged();
            }
        }
        private global::System.String _PayForNumber;
        partial void OnPayForNumberChanging(global::System.String value);
        partial void OnPayForNumberChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String PayAmount
        {
            get
            {
                return _PayAmount;
            }
            set
            {
                OnPayAmountChanging(value);
                ReportPropertyChanging("PayAmount");
                _PayAmount = StructuralObject.SetValidValue(value, true, "PayAmount");
                ReportPropertyChanged("PayAmount");
                OnPayAmountChanged();
            }
        }
        private global::System.String _PayAmount;
        partial void OnPayAmountChanging(global::System.String value);
        partial void OnPayAmountChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String AgentNumber
        {
            get
            {
                return _AgentNumber;
            }
            set
            {
                OnAgentNumberChanging(value);
                ReportPropertyChanging("AgentNumber");
                _AgentNumber = StructuralObject.SetValidValue(value, true, "AgentNumber");
                ReportPropertyChanged("AgentNumber");
                OnAgentNumberChanged();
            }
        }
        private global::System.String _AgentNumber;
        partial void OnAgentNumberChanging(global::System.String value);
        partial void OnAgentNumberChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> LogDateTime
        {
            get
            {
                return _LogDateTime;
            }
            set
            {
                OnLogDateTimeChanging(value);
                ReportPropertyChanging("LogDateTime");
                _LogDateTime = StructuralObject.SetValidValue(value, "LogDateTime");
                ReportPropertyChanged("LogDateTime");
                OnLogDateTimeChanged();
            }
        }
        private Nullable<global::System.DateTime> _LogDateTime;
        partial void OnLogDateTimeChanging(Nullable<global::System.DateTime> value);
        partial void OnLogDateTimeChanged();

        #endregion

    }

    #endregion

}
