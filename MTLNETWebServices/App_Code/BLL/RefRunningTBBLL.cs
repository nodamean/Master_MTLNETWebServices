using System;
using System.Collections.Generic;
using System.Web;
using MTL_WSLOGTableAdapters;

/// <summary>
/// Summary description for RefRunningTBBLL
/// </summary>

[System.ComponentModel.DataObject]
public class RefRunningTBBLL
{
    private RefRunningTBTableAdapter _RefRunningTBAdapters = null;
    protected RefRunningTBTableAdapter Adapter
    {
        get
        {
            if (_RefRunningTBAdapters == null)
                _RefRunningTBAdapters = new RefRunningTBTableAdapter();
            return _RefRunningTBAdapters;
        }
    }

    public RefRunningTBBLL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public MTL_WSLOG.RefRunningTBDataTable GetRefRunningTBs()
    {
        return Adapter.GetRefRunningTBs();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
    public int AddRefRunningTBAndReturn()
    {
        // Create a new RefRunningTBRow instance
        //MTL_WSLOG.RefRunningTBDataTable RefRunningTBs = new MTL_WSLOG.RefRunningTBDataTable();
        //MTL_WSLOG.RefRunningTBRow RefRunningTB = RefRunningTBs.NewRefRunningTBRow();

        // Add the new RefRunningTB
        //RefRunningTBs.AddRefRunningTBRow(RefRunningTB);
        //int rowsAffected = Adapter.Update(RefRunningTBs);
        int rowsAffected = Convert.ToInt32(Adapter.InsertRefRunningTB());

        // Return true if precisely one row was inserted, otherwise false
        //return rowsAffected == 1;
        return rowsAffected;
    }
}
