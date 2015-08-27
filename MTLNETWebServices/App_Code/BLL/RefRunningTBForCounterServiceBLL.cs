using System;
using System.Collections.Generic;
using System.Web;
using MTL_WSLOGTableAdapters;

/// <summary>
/// Summary description for RefRunningTBForCounterServiceBLL
/// </summary>

[System.ComponentModel.DataObject]
public class RefRunningTBForCounterServiceBLL
{
    private RefRunningTBForCounterServiceTableAdapter _RefRunningTBForCounterServiceAdapters = null;
    protected RefRunningTBForCounterServiceTableAdapter Adapter
    {
        get
        {
            if (_RefRunningTBForCounterServiceAdapters == null)
                _RefRunningTBForCounterServiceAdapters = new RefRunningTBForCounterServiceTableAdapter();
            return _RefRunningTBForCounterServiceAdapters;
        }
    }

    public RefRunningTBForCounterServiceBLL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public MTL_WSLOG.RefRunningTBForCounterServiceDataTable GetRefRunningTBsForCounterService()
    {
        return Adapter.GetRefRunningTBsForCounterService();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
    public int AddRefRunningTBAndReturnForCounterService()
    {
        // Create a new RefRunningTBRow instance
        //MTL_WSLOG.RefRunningTBDataTable RefRunningTBs = new MTL_WSLOG.RefRunningTBDataTable();
        //MTL_WSLOG.RefRunningTBRow RefRunningTB = RefRunningTBs.NewRefRunningTBRow();

        // Add the new RefRunningTB
        //RefRunningTBs.AddRefRunningTBRow(RefRunningTB);
        //int rowsAffected = Adapter.Update(RefRunningTBs);
        int rowsAffected = Convert.ToInt32(Adapter.InsertRefRunningTBForCounterService());

        // Return true if precisely one row was inserted, otherwise false
        //return rowsAffected == 1;
        return rowsAffected;
    }
}
