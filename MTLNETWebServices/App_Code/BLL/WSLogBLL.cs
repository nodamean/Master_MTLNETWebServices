using MTL_WSLOGTableAdapters;
using System;
using System.Collections.Generic;
using System.Web;


/// <summary>
/// Summary description for WSLogBLL
/// </summary>

[System.ComponentModel.DataObject]
public class WSLogBLL
{
    private MTL_WSLOGTableAdapters.WSLogTableAdapter _WSLogAdapters = null;
    protected WSLogTableAdapter Adapter
    {
        get
        {
            if (_WSLogAdapters == null)
                _WSLogAdapters = new WSLogTableAdapter();
            return _WSLogAdapters;
        }
    }

	public WSLogBLL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public MTL_WSLOG.WSLogDataTable GetWSLogs()
    {
        return Adapter.GetWSLogs();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
    public bool AddWSLog(string Partner, string IPaddress, string Action, string WSName, string WSMethod, string Detail, int? RefRow)
    {
        // Create a new WSLogRow instance
        MTL_WSLOG.WSLogDataTable WSLogs = new MTL_WSLOG.WSLogDataTable();
        MTL_WSLOG.WSLogRow WSLog = WSLogs.NewWSLogRow();

        if (Partner == null)
        {
            WSLog.SetPartnerNull();
        }
        else
        {
            WSLog.Partner = Partner;
        }
        WSLog.IPaddress = IPaddress;
        if (Action == null)
        {
            WSLog.SetActionNull();
        }
        else
        {
            WSLog.Action = Action;
        }
        WSLog.LogDateTime = DateTime.Now;
        if (WSName == null)
        {
            WSLog.SetWSNameNull();
        }
        else
        {
            WSLog.WSName = WSName;
        }
        if (WSMethod == null)
        {
            WSLog.SetWSMethodNull();
        }
        else
        {
            WSLog.WSMethod = WSMethod;
        }
        if (Detail == null)
        {
            WSLog.SetDetailNull();
        }
        else
        {
            if (Detail.Length >= 500)
            {
                WSLog.Detail = Detail.Substring(0, 499);
            }
            else
            {
                WSLog.Detail = Detail;
            }
        }
        if (RefRow == null)
        {
            WSLog.SetRefRowNull();
        }
        else
        {
            WSLog.RefRow = Convert.ToInt32(RefRow);
        }

        // Add the new WSLog
        WSLogs.AddWSLogRow(WSLog);
        int rowsAffected = Adapter.Update(WSLogs);

        // Return true if precisely one row was inserted, otherwise false
        return rowsAffected == 1;
    }
}
