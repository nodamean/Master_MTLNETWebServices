using System;
using System.Collections.Generic;
using System.Web;
using MTL_WSLOGTableAdapters;

/// <summary>
/// Summary description for WSLogForCounterServiceBLL
/// </summary>
public class WSLogForCounterServiceBLL
{
    private WSLogForCounterServiceTableAdapter _WSLogForCounterServiceAdapters = null;
    protected WSLogForCounterServiceTableAdapter Adapter
    {
        get
        {
            if (_WSLogForCounterServiceAdapters == null)
                _WSLogForCounterServiceAdapters = new WSLogForCounterServiceTableAdapter();
            return _WSLogForCounterServiceAdapters;
        }
    }

    public WSLogForCounterServiceBLL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public MTL_WSLOG.WSLogForCounterServiceDataTable GetWSLogsForCounterService()
    {
        return Adapter.GetWSLogsForCounterService();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
    public bool AddWSLogForCounterService(string Partner, string IPaddress, string Action, string WSName, string WSMethod, int? RefRow, string TX_ID, string LOG_ID, string VENDOR_ID, string SERVICE_ID, string METHOD, string COUNTER_NO, string TERM_NO, string POS_TAX_ID, string SERVICE_RUN_NO, string RECORD_STATUS, string CLIENT_SERVICE_RUN, string AMOUNT_RECEIVED, string VAT_AMOUNT, string BILL_TYPE, string REFERENCE_1, string REFERENCE_2, string REFERENCE_3, string REFERENCE_4, string CUSTOMER_NAME, string CUSTOMER_ADDR_1, string CUSTOMER_ADDR_2, string CUSTOMER_ADDR3, string CUSTOMER_TEL_NO, string ZONE, string R_SERVICE_RUNNO, string CANCEL_OPERATING, string OPERATE_BY_STAFF, string SYSTEM_DATE_TIME, string USERID, string PASSWORD)
    {
        // Create a new WSLogRow instance
        MTL_WSLOG.WSLogForCounterServiceDataTable WSLogsForCounterService = new MTL_WSLOG.WSLogForCounterServiceDataTable();
        MTL_WSLOG.WSLogForCounterServiceRow WSLogForCounterService = WSLogsForCounterService.NewWSLogForCounterServiceRow();

        if (Partner == null)
        {
            WSLogForCounterService.SetPartnerNull();
        }
        else
        {
            WSLogForCounterService.Partner = Partner;
        }
        WSLogForCounterService.IPaddress = IPaddress;
        if (Action == null)
        {
            WSLogForCounterService.SetActionNull();
        }
        else
        {
            WSLogForCounterService.Action = Action;
        }
        WSLogForCounterService.LogDateTime = DateTime.Now;
        if (WSName == null)
        {
            WSLogForCounterService.SetWSNameNull();
        }
        else
        {
            WSLogForCounterService.WSName = WSName;
        }
        if (WSMethod == null)
        {
            WSLogForCounterService.SetWSMethodNull();
        }
        else
        {
            WSLogForCounterService.WSMethod = WSMethod;
        }
        if (RefRow == null)
        {
            WSLogForCounterService.SetRefRowNull();
        }
        else
        {
            WSLogForCounterService.RefRow = Convert.ToInt32(RefRow);
        }

        // Add the new WSLog
        WSLogsForCounterService.AddWSLogForCounterServiceRow(WSLogForCounterService);
        int rowsAffected = Adapter.Update(WSLogsForCounterService);

        // Return true if precisely one row was inserted, otherwise false
        return rowsAffected == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
    public bool AddWSLogRequest(string Partner, string IPaddress, string Action, string WSName, string WSMethod, int? RefRow, string TX_ID, string LOG_ID, string VENDOR_ID, string SERVICE_ID, string METHOD, string COUNTER_NO, string TERM_NO, string POS_TAX_ID, string SERVICE_RUN_NO, string RECORD_STATUS, string CLIENT_SERVICE_RUN, string AMOUNT_RECEIVED, string VAT_AMOUNT, string BILL_TYPE, string REFERENCE_1, string REFERENCE_2, string REFERENCE_3, string REFERENCE_4, string CUSTOMER_NAME, string CUSTOMER_ADDR_1, string CUSTOMER_ADDR_2, string CUSTOMER_ADDR_3, string CUSTOMER_TEL_NO, string ZONE, string R_SERVICE_RUNNO, string CANCEL_OPERATING, string OPERATE_BY_STAFF, string SYSTEM_DATE_TIME, string USERID, string PASSWORD)
    {
        // Create a new WSLogRow instance
        MTL_WSLOG.WSLogForCounterServiceDataTable WSLogsForCounterService = new MTL_WSLOG.WSLogForCounterServiceDataTable();
        MTL_WSLOG.WSLogForCounterServiceRow WSLogForCounterService = WSLogsForCounterService.NewWSLogForCounterServiceRow();

        if (Partner == null)
        {
            WSLogForCounterService.SetPartnerNull();
        }
        else
        {
            WSLogForCounterService.Partner = Partner;
        }
        WSLogForCounterService.IPaddress = IPaddress;
        if (Action == null)
        {
            WSLogForCounterService.SetActionNull();
        }
        else
        {
            WSLogForCounterService.Action = Action;
        }
        WSLogForCounterService.LogDateTime = DateTime.Now;
        if (WSName == null)
        {
            WSLogForCounterService.SetWSNameNull();
        }
        else
        {
            WSLogForCounterService.WSName = WSName;
        }
        if (WSMethod == null)
        {
            WSLogForCounterService.SetWSMethodNull();
        }
        else
        {
            WSLogForCounterService.WSMethod = WSMethod;
        }
        if (RefRow == null)
        {
            WSLogForCounterService.SetRefRowNull();
        }
        else
        {
            WSLogForCounterService.RefRow = Convert.ToInt32(RefRow);
        }
        if (TX_ID == null)
        {
            WSLogForCounterService.SetTX_IDNull();
        }
        else
        {
            WSLogForCounterService.TX_ID = TX_ID;
        }
        if (LOG_ID == null)
        {
            WSLogForCounterService.SetLOG_IDNull();
        }
        else
        {
            WSLogForCounterService.LOG_ID = LOG_ID;
        }
        if (VENDOR_ID == null)
        {
            WSLogForCounterService.SetVENDOR_IDNull();
        }
        else
        {
            WSLogForCounterService.VENDOR_ID = VENDOR_ID;
        }
        if (SERVICE_ID == null)
        {
            WSLogForCounterService.SetSERVICE_IDNull();
        }
        else
        {
            WSLogForCounterService.SERVICE_ID = SERVICE_ID;
        }
        if (METHOD == null)
        {
            WSLogForCounterService.SetMETHODNull();
        }
        else
        {
            WSLogForCounterService.METHOD = METHOD;
        }
        if (COUNTER_NO == null)
        {
            WSLogForCounterService.SetCOUNTER_NONull();
        }
        else
        {
            WSLogForCounterService.COUNTER_NO = COUNTER_NO;
        }
        if (TERM_NO == null)
        {
            WSLogForCounterService.SetTERM_NONull();
        }
        else
        {
            WSLogForCounterService.TERM_NO = TERM_NO;
        }
        if (POS_TAX_ID == null)
        {
            WSLogForCounterService.SetPOS_TAX_IDNull();
        }
        else
        {
            WSLogForCounterService.POS_TAX_ID = POS_TAX_ID;
        }
        if (SERVICE_RUN_NO == null)
        {
            WSLogForCounterService.SetSERVICE_RUN_NONull();
        }
        else
        {
            WSLogForCounterService.SERVICE_RUN_NO = SERVICE_RUN_NO;
        }
        if (RECORD_STATUS == null)
        {
            WSLogForCounterService.SetRECORD_STATUSNull();
        }
        else
        {
            WSLogForCounterService.RECORD_STATUS = RECORD_STATUS;
        }
        if (CLIENT_SERVICE_RUN == null)
        {
            WSLogForCounterService.SetCLIENT_SERVICE_RUNNull();
        }
        else
        {
            WSLogForCounterService.CLIENT_SERVICE_RUN = CLIENT_SERVICE_RUN;
        }
        if (AMOUNT_RECEIVED == null)
        {
            WSLogForCounterService.SetAMOUNT_RECEIVEDNull();
        }
        else
        {
            WSLogForCounterService.AMOUNT_RECEIVED = AMOUNT_RECEIVED;
        }
        if (VAT_AMOUNT == null)
        {
            WSLogForCounterService.SetVAT_AMOUNTNull();
        }
        else
        {
            WSLogForCounterService.VAT_AMOUNT = VAT_AMOUNT;
        }
        if (BILL_TYPE == null)
        {
            WSLogForCounterService.SetBILL_TYPENull();
        }
        else
        {
            WSLogForCounterService.BILL_TYPE = BILL_TYPE;
        }
        if (REFERENCE_1 == null)
        {
            WSLogForCounterService.SetREFERENCE_1Null();
        }
        else
        {
            WSLogForCounterService.REFERENCE_1 = REFERENCE_1;
        }
        if (REFERENCE_2 == null)
        {
            WSLogForCounterService.SetREFERENCE_2Null();
        }
        else
        {
            WSLogForCounterService.REFERENCE_2 = REFERENCE_2;
        }
        if (REFERENCE_3 == null)
        {
            WSLogForCounterService.SetREFERENCE_3Null();
        }
        else
        {
            WSLogForCounterService.REFERENCE_3 = REFERENCE_3;
        }
        if (REFERENCE_4 == null)
        {
            WSLogForCounterService.SetREFERENCE_4Null();
        }
        else
        {
            WSLogForCounterService.REFERENCE_4 = REFERENCE_4;
        }
        if (CUSTOMER_NAME == null)
        {
            WSLogForCounterService.SetCUSTOMER_NAMENull();
        }
        else
        {
            WSLogForCounterService.CUSTOMER_NAME = CUSTOMER_NAME;
        }
        if (CUSTOMER_ADDR_1 == null)
        {
            WSLogForCounterService.SetCUSTOMER_ADDR_1Null();
        }
        else
        {
            WSLogForCounterService.CUSTOMER_ADDR_1 = CUSTOMER_ADDR_1;
        }
        if (CUSTOMER_ADDR_2 == null)
        {
            WSLogForCounterService.SetCUSTOMER_ADDR_2Null();
        }
        else
        {
            WSLogForCounterService.CUSTOMER_ADDR_2 = CUSTOMER_ADDR_2;
        }
        if (CUSTOMER_ADDR_3 == null)
        {
            WSLogForCounterService.SetCUSTOMER_ADDR_3Null();
        }
        else
        {
            WSLogForCounterService.CUSTOMER_ADDR_3 = CUSTOMER_ADDR_3;
        }
        if (CUSTOMER_TEL_NO == null)
        {
            WSLogForCounterService.SetCUSTOMER_TEL_NONull();
        }
        else
        {
            WSLogForCounterService.CUSTOMER_TEL_NO = CUSTOMER_TEL_NO;
        }
        if (ZONE == null)
        {
            WSLogForCounterService.SetZONENull();
        }
        else
        {
            WSLogForCounterService.ZONE = ZONE;
        }
        if (R_SERVICE_RUNNO == null)
        {
            WSLogForCounterService.SetR_SERVICE_RUNNONull();
        }
        else
        {
            WSLogForCounterService.R_SERVICE_RUNNO = R_SERVICE_RUNNO;
        }
        if (CANCEL_OPERATING == null)
        {
            WSLogForCounterService.SetCANCEL_OPERATINGNull();
        }
        else
        {
            WSLogForCounterService.CANCEL_OPERATING = CANCEL_OPERATING;
        }
        if (OPERATE_BY_STAFF == null)
        {
            WSLogForCounterService.SetOPERATE_BY_STAFFNull();
        }
        else
        {
            WSLogForCounterService.OPERATE_BY_STAFF = OPERATE_BY_STAFF;
        }
        if (SYSTEM_DATE_TIME == null)
        {
            WSLogForCounterService.SetSYSTEM_DATE_TIMENull();
        }
        else
        {
            WSLogForCounterService.SYSTEM_DATE_TIME = SYSTEM_DATE_TIME;
        }
        if (USERID == null)
        {
            WSLogForCounterService.SetUSERIDNull();
        }
        else
        {
            WSLogForCounterService.USERID = USERID;
        }
        if (PASSWORD == null)
        {
            WSLogForCounterService.SetPASSWORDNull();
        }
        else
        {
            WSLogForCounterService.PASSWORD = PASSWORD;
        }

        // Add the new WSLog
        WSLogsForCounterService.AddWSLogForCounterServiceRow(WSLogForCounterService);
        int rowsAffected = Adapter.Update(WSLogsForCounterService);

        // Return true if precisely one row was inserted, otherwise false
        return rowsAffected == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
    public bool AddWSLogResponse(string Partner, string IPaddress, string Action, string WSName, string WSMethod, int? RefRow, string opTX_ID, string opLOG_ID, string opVENDOR_ID, string opSERVICE_ID, string opMETHOD, string opSUCCESS, string opCODE, string opDESC, string opREFERENCE_1, string opREFERENCE_2, string opREFERENCE_3, string opREFERENCE_4, string opCUSTOMER_NAME, string opCUSTOMER_ADDR_1, string opCUSTOMER_ADDR_2, string opCUSTOMER_ADDR_3, string opCUSTOMER_TEL_NO, string opRETURN1, string opRETURN2, string opRETURN3, string opAMOUNT_RECEIVED, string opPRINT_SLIP)
    {
        // Create a new WSLogRow instance
        MTL_WSLOG.WSLogForCounterServiceDataTable WSLogsForCounterService = new MTL_WSLOG.WSLogForCounterServiceDataTable();
        MTL_WSLOG.WSLogForCounterServiceRow WSLogForCounterService = WSLogsForCounterService.NewWSLogForCounterServiceRow();

        if (Partner == null)
        {
            WSLogForCounterService.SetPartnerNull();
        }
        else
        {
            WSLogForCounterService.Partner = Partner;
        }
        WSLogForCounterService.IPaddress = IPaddress;
        if (Action == null)
        {
            WSLogForCounterService.SetActionNull();
        }
        else
        {
            WSLogForCounterService.Action = Action;
        }
        WSLogForCounterService.LogDateTime = DateTime.Now;
        if (WSName == null)
        {
            WSLogForCounterService.SetWSNameNull();
        }
        else
        {
            WSLogForCounterService.WSName = WSName;
        }
        if (WSMethod == null)
        {
            WSLogForCounterService.SetWSMethodNull();
        }
        else
        {
            WSLogForCounterService.WSMethod = WSMethod;
        }
        if (RefRow == null)
        {
            WSLogForCounterService.SetRefRowNull();
        }
        else
        {
            WSLogForCounterService.RefRow = Convert.ToInt32(RefRow);
        }
        if (opTX_ID == null)
        {
            WSLogForCounterService.SetopTX_IDNull();
        }
        else
        {
            WSLogForCounterService.opTX_ID = opTX_ID;
        }
        if (opLOG_ID == null)
        {
            WSLogForCounterService.SetopLOG_IDNull();
        }
        else
        {
            WSLogForCounterService.opLOG_ID = opLOG_ID;
        }
        if (opVENDOR_ID == null)
        {
            WSLogForCounterService.SetopVENDOR_IDNull();
        }
        else
        {
            WSLogForCounterService.opVENDOR_ID = opVENDOR_ID;
        }
        if (opSERVICE_ID == null)
        {
            WSLogForCounterService.SetopSERVICE_IDNull();
        }
        else
        {
            WSLogForCounterService.opSERVICE_ID = opSERVICE_ID;
        }
        if (opMETHOD == null)
        {
            WSLogForCounterService.SetopMETHODNull();
        }
        else
        {
            WSLogForCounterService.opMETHOD = opMETHOD;
        }
        if (opSUCCESS == null)
        {
            WSLogForCounterService.SetopSUCCESSNull();
        }
        else
        {
            WSLogForCounterService.opSUCCESS = opSUCCESS;
        }
        if (opCODE == null)
        {
            WSLogForCounterService.SetopCODENull();
        }
        else
        {
            WSLogForCounterService.opCODE = opCODE;
        }
        if (opDESC == null)
        {
            WSLogForCounterService.SetopDESCNull();
        }
        else
        {
            WSLogForCounterService.opDESC = opDESC;
        }
        if (opREFERENCE_1 == null)
        {
            WSLogForCounterService.SetopREFERENCE_1Null();
        }
        else
        {
            WSLogForCounterService.opREFERENCE_1 = opREFERENCE_1;
        }
        if (opREFERENCE_2 == null)
        {
            WSLogForCounterService.SetopREFERENCE_2Null();
        }
        else
        {
            WSLogForCounterService.opREFERENCE_2 = opREFERENCE_2;
        }
        if (opREFERENCE_3 == null)
        {
            WSLogForCounterService.SetopREFERENCE_3Null();
        }
        else
        {
            WSLogForCounterService.opREFERENCE_3 = opREFERENCE_3;
        }
        if (opREFERENCE_4 == null)
        {
            WSLogForCounterService.SetopREFERENCE_4Null();
        }
        else
        {
            WSLogForCounterService.opREFERENCE_4 = opREFERENCE_4;
        }
        if (opCUSTOMER_NAME == null)
        {
            WSLogForCounterService.SetCUSTOMER_NAMENull();
        }
        else
        {
            WSLogForCounterService.opCUSTOMER_NAME = opCUSTOMER_NAME;
        }
        if (opCUSTOMER_ADDR_1 == null)
        {
            WSLogForCounterService.SetopCUSTOMER_ADDR_1Null();
        }
        else
        {
            WSLogForCounterService.opCUSTOMER_ADDR_1 = opCUSTOMER_ADDR_1;
        }
        if (opCUSTOMER_ADDR_2 == null)
        {
            WSLogForCounterService.SetopCUSTOMER_ADDR_2Null();
        }
        else
        {
            WSLogForCounterService.opCUSTOMER_ADDR_2 = opCUSTOMER_ADDR_2;
        }
        if (opCUSTOMER_ADDR_3 == null)
        {
            WSLogForCounterService.SetopCUSTOMER_ADDR_3Null();
        }
        else
        {
            WSLogForCounterService.opCUSTOMER_ADDR_3 = opCUSTOMER_ADDR_3;
        }
        if (opCUSTOMER_TEL_NO == null)
        {
            WSLogForCounterService.SetopCUSTOMER_TEL_NONull();
        }
        else
        {
            WSLogForCounterService.opCUSTOMER_TEL_NO = opCUSTOMER_TEL_NO;
        }
        if (opRETURN1 == null)
        {
            WSLogForCounterService.SetopRETURN1Null();
        }
        else
        {
            WSLogForCounterService.opRETURN1 = opRETURN1;
        }
        if (opRETURN2 == null)
        {
            WSLogForCounterService.SetopRETURN2Null();
        }
        else
        {
            WSLogForCounterService.opRETURN2 = opRETURN2;
        }
        if (opRETURN3 == null)
        {
            WSLogForCounterService.SetopRETURN3Null();
        }
        else
        {
            WSLogForCounterService.opRETURN3 = opRETURN3;
        }
        if (opAMOUNT_RECEIVED == null)
        {
            WSLogForCounterService.SetAMOUNT_RECEIVEDNull();
        }
        else
        {
            WSLogForCounterService.AMOUNT_RECEIVED = opAMOUNT_RECEIVED;
        }
        if (opPRINT_SLIP == null)
        {
            WSLogForCounterService.SetopPRINT_SLIPNull();
        }
        else
        {
            WSLogForCounterService.opPRINT_SLIP = opPRINT_SLIP;
        }

        // Add the new WSLog
        WSLogsForCounterService.AddWSLogForCounterServiceRow(WSLogForCounterService);
        int rowsAffected = Adapter.Update(WSLogsForCounterService);

        // Return true if precisely one row was inserted, otherwise false
        return rowsAffected == 1;
    }
}
