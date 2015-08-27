using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;

using System.Web.Configuration;

/// <summary>
/// Summary description for service
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class service : System.Web.Services.WebService {

    public service () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    #region Declaration
    //ประกาศตัวแปรร่วมสำหรับใช้ภายใน
    protected string admin_username = "apluser";
    protected string admin_password = "rtyhgf";
    protected string ipaddress = HttpContext.Current.Request.UserHostAddress.ToString();
    protected int refnum;
    protected string MaximumAttempts = WebConfigurationManager.AppSettings["CS711MaximumAttempts"];
    protected string MaximumActiveMinutes = WebConfigurationManager.AppSettings["CS711MaximumActiveMinutes"];

    //ประกาศตัวแปร output
    protected string opTX_ID = "";
    protected string opLOG_ID = "";
    protected string opVENDOR_ID = ""; 
    protected string opSERVICE_ID = "";
    protected string opMETHOD = "";
    protected string opSUCCESS = "";
    protected string opCODE = "";
    protected string opDESC = "";
    protected string opREFERENCE_1 = "";
    protected string opREFERENCE_2 = "";
    protected string opREFERENCE_3 = "";
    protected string opREFERENCE_4 = ""; 
    protected string opCUSTOMER_NAME = "";
    protected string opCUSTOMER_ADDR_1 = "";
    protected string opCUSTOMER_ADDR_2 = "";
    protected string opCUSTOMER_ADDR_3 = "";
    protected string opCUSTOMER_TEL_NO = "";
    protected string opRETURN1 = "";
    protected string opRETURN2 = "";
    protected string opRETURN3 = "";
    protected string opAMOUNT_RECEIVED = "";
    protected string opPRINT_SLIP = "";

    //ประกาศคลาส RESPONSE สำหรับใช้เป็น output ของ Web Services
    public class RESPONSE
    {
        public string TX_ID;
        public string LOG_ID;
        public string VENDOR_ID;
        public string SERVICE_ID;
        public string METHOD;
        public string SUCCESS;
        public string CODE;
        public string DESC;
        public string REFERENCE_1;
        public string REFERENCE_2;
        public string REFERENCE_3;
        public string REFERENCE_4;
        public string CUSTOMER_NAME;
        public string CUSTOMER_ADDR_1;
        public string CUSTOMER_ADDR_2;
        public string CUSTOMER_ADDR_3;
        public string CUSTOMER_TEL_NO;
        public string RETURN1;
        public string RETURN2;
        public string RETURN3;
        public string AMOUNT_RECEIVED;
        public string PRINT_SLIP;
    }

    public class GetTransactionByOTP_Result
    {
        public string Result;
        public string ClientNumber;
        public string PolicyNumber;
        public string VoucherNumber;
        public string ClaimNumber;
        public string ClaimDate;
        public string MobileNumber;
        public string Status;
    }
    public class GetTransactionsList_Result
    {
        public string Result;
        public TransactionsList[] transactionsList;
    }
    public class TransactionsList
    {
        public string PIN;
        public string MobileNumber;
        public string ClientNumber;
        public string PolicyNumber;
        public string VoucherNumber;
        public string ClaimNumber;
        public string ClaimDate;
        public string ClientName;
        public string Amount;
        public string Status;
        public string OTPExpiryDate;
    }
    #endregion

    #region Web Methods
    [WebMethod(Description = "สำหรับ Counter Service REQUEST")]
    public RESPONSE REQUEST(string TX_ID, string LOG_ID, string VENDOR_ID, string SERVICE_ID, string METHOD, string COUNTER_NO, string TERM_NO, string POS_TAX_ID, string SERVICE_RUN_NO, string RECORD_STATUS, string CLIENT_SERVICE_RUN, string AMOUNT_RECEIVED, string VAT_AMOUNT, string BILL_TYPE, string REFERENCE_1, string REFERENCE_2, string REFERENCE_3, string REFERENCE_4, string CUSTOMER_NAME, string CUSTOMER_ADDR_1, string CUSTOMER_ADDR_2, string CUSTOMER_ADDR_3, string CUSTOMER_TEL_NO, string ZONE, string R_SERVICE_RUNNO, string CANCEL_OPERATING, string OPERATING_BY_STAFF, string SYSTEM_DATE_TIME, string USERID, string PASSWORD)
    {
        //Instantiate object from class

        //สร้าง object ของคลาส RESPONSE สำหรับใช้เป็น output ของ Web Services
        RESPONSE obj = new RESPONSE();
        //สร้าง object ของคลาส Running Reference สำหรับใช้เป็นหมายเลขอ้างอิงการ Request และ Response จาก CS-711
        RefRunningTBForCounterServiceBLL runningobj = new RefRunningTBForCounterServiceBLL();
        refnum = runningobj.AddRefRunningTBAndReturnForCounterService();
        //สร้าง object ของคลาส Logging สำหรับใช้บันทึก Log การ Request และ Response จาก CS-711
        WSLogForCounterServiceBLL logobj = new WSLogForCounterServiceBLL();

        //กำหนดค่าให้ตัวแปร output
        opTX_ID = TX_ID;
        opLOG_ID = LOG_ID;
        opVENDOR_ID = VENDOR_ID;
        opSERVICE_ID = SERVICE_ID;
        opMETHOD = METHOD;

        //LogRequest: Insert Log Request - บันทึก Log การ Request ที่ส่งมาจาก CS-711
        logobj.AddWSLogRequest(USERID, ipaddress, "Request", "service", METHOD, refnum, TX_ID, LOG_ID, VENDOR_ID, SERVICE_ID, METHOD, COUNTER_NO, TERM_NO, POS_TAX_ID, SERVICE_RUN_NO, RECORD_STATUS, CLIENT_SERVICE_RUN, AMOUNT_RECEIVED, VAT_AMOUNT, BILL_TYPE, REFERENCE_1, REFERENCE_2, REFERENCE_3, REFERENCE_4, CUSTOMER_NAME, CUSTOMER_ADDR_1, CUSTOMER_ADDR_2, CUSTOMER_ADDR_3, CUSTOMER_TEL_NO, ZONE, R_SERVICE_RUNNO, CANCEL_OPERATING, OPERATING_BY_STAFF, SYSTEM_DATE_TIME, USERID, PASSWORD);

        //ตรวจสอบว่า CS-711 ส่ง Input Parameters ที่จำเป็นมาให้ครบหรือไม่?
        //ถ้า CS-711 ส่ง Input Parameters ที่จำเป็นมาให้ไม่ครบ บันทึก Log Response และ return output
        if (TX_ID == "" && LOG_ID == "" && VENDOR_ID == "" && SERVICE_ID == "" && METHOD == "" && COUNTER_NO == "" && TERM_NO == "" && POS_TAX_ID == "" && SERVICE_RUN_NO == "" && RECORD_STATUS == "" && CLIENT_SERVICE_RUN == "" && AMOUNT_RECEIVED == "" && VAT_AMOUNT == "" && BILL_TYPE == "" && ZONE == "" && R_SERVICE_RUNNO == "" && OPERATING_BY_STAFF == "")
        {
            opSUCCESS = "False";
            opCODE = "9MTLXXXX";
            opDESC = "กรอกข้อมูล Input ที่จำเป็นมาให้ไม่ครบ";

            //กำหนดค่าให้ฟิลด์ต่างๆ ของ object สำหรับ RESPONSE class
            obj.TX_ID = opTX_ID;
            obj.LOG_ID = opLOG_ID;
            obj.VENDOR_ID = opVENDOR_ID;
            obj.SERVICE_ID = opSERVICE_ID;
            obj.METHOD = opMETHOD;
            obj.SUCCESS = opSUCCESS;
            obj.CODE = opCODE;
            obj.DESC = opDESC;
            obj.REFERENCE_1 = opREFERENCE_1;
            obj.REFERENCE_2 = opREFERENCE_2;
            obj.REFERENCE_3 = opREFERENCE_3;
            obj.REFERENCE_4 = opREFERENCE_4;
            obj.CUSTOMER_NAME = opCUSTOMER_NAME;
            obj.CUSTOMER_ADDR_1 = opCUSTOMER_ADDR_1;
            obj.CUSTOMER_ADDR_2 = opCUSTOMER_ADDR_2;
            obj.CUSTOMER_ADDR_3 = opCUSTOMER_ADDR_3;
            obj.CUSTOMER_TEL_NO = opCUSTOMER_TEL_NO;
            obj.RETURN1 = opRETURN1;
            obj.RETURN2 = opRETURN2;
            obj.RETURN3 = opRETURN3;
            obj.AMOUNT_RECEIVED = opAMOUNT_RECEIVED;
            obj.PRINT_SLIP = opPRINT_SLIP;

            //LogResponse: Insert Log Response - บันทึก Log Response ว่า CS-711 ส่ง Input Parameters ที่จำเป็นมาให้ไม่ครบ
            logobj.AddWSLogResponse(USERID, ipaddress, "Response", "service", METHOD, refnum, obj.TX_ID, obj.LOG_ID, obj.VENDOR_ID, obj.SERVICE_ID, obj.METHOD, obj.SUCCESS, obj.CODE, obj.DESC, obj.REFERENCE_1, obj.REFERENCE_2, obj.REFERENCE_3, obj.REFERENCE_4, obj.CUSTOMER_NAME, obj.CUSTOMER_ADDR_1, obj.CUSTOMER_ADDR_2, obj.CUSTOMER_ADDR_3, obj.CUSTOMER_TEL_NO, obj.RETURN1, obj.RETURN2, obj.RETURN3, obj.AMOUNT_RECEIVED, obj.PRINT_SLIP);

            return obj;
        }
        // ถ้า CS-711 ส่ง Input Parameters ที่จำเป็นมาให้ครบ ให้ดำเนินการตรวจสอบข้อมูลในระบบ
        else
        {
            //ตรวจสอบว่า CS-711 ส่ง Request Method อะไร มาให้ MTL และตรวจสอบข้อมูลในระบบ ผ่าน Web Services ภายใน
            try
            {
                //ตรวจสอบว่า CS-711 ต้องการเรียกใช้ Method อะไร?
                switch (opMETHOD)
                {
                    case "DataExchange":
                        //ตรวจสอบข้อมูลในระบบ ผ่าน Web Services ภายใน
                        try
                        {

                            if (true)
                            {
                                opSUCCESS = "True";
                                opCODE = "100";
                                opDESC = "SUCCESS";
                                opAMOUNT_RECEIVED = "";
                            }
                            else
                            {
                                opSUCCESS = "False";
                                opCODE = "9MTLXXX";
                                opDESC = "erererererererer";
                            }
                        }
                        catch (Exception ex)
                        {
                            opSUCCESS = "False";
                            opCODE = "9MTLXXX";
                            opDESC = ex.ToString();
                        }
                        break;
                    case "DataExchangeConfirm":
                        //ตรวจสอบข้อมูลในระบบ ผ่าน Web Services ภายใน
                        try
                        {

                            if (true)
                            {
                                opSUCCESS = "True";
                                opCODE = "100";
                                opDESC = "SUCCESS";
                                opAMOUNT_RECEIVED = "";
                            }
                            else
                            {
                                opSUCCESS = "False";
                                opCODE = "9MTLXXX";
                                opDESC = "erererererererer";
                            }
                        }
                        catch (Exception ex)
                        {
                            opSUCCESS = "False";
                            opCODE = "9MTLXXX";
                            opDESC = ex.ToString();
                        }
                        break;
                    case "DataExchangeCancel":
                        //ตรวจสอบข้อมูลในระบบ ผ่าน Web Services ภายใน
                        try
                        {

                            if (true)
                            {
                                opSUCCESS = "True";
                                opCODE = "100";
                                opDESC = "SUCCESS";
                                opAMOUNT_RECEIVED = "";
                            }
                            else
                            {
                                opSUCCESS = "False";
                                opCODE = "9MTLXXX";
                                opDESC = "erererererererer";
                            }
                        }
                        catch (Exception ex)
                        {
                            opSUCCESS = "False";
                            opCODE = "9MTLXXX";
                            opDESC = ex.ToString();
                        }
                        break;
                    case "OR":
                        //ตรวจสอบข้อมูลในระบบ ผ่าน Web Services ภายใน
                        try
                        {

                            if (true)
                            {
                                opSUCCESS = "True";
                                opCODE = "100";
                                opDESC = "SUCCESS";
                                opAMOUNT_RECEIVED = "";
                            }
                            else
                            {
                                opSUCCESS = "False";
                                opCODE = "9MTLXXX";
                                opDESC = "erererererererer";
                            }
                        }
                        catch (Exception ex)
                        {
                            opSUCCESS = "False";
                            opCODE = "9MTLXXX";
                            opDESC = ex.ToString();
                        }
                        break;
                    case "ORConfirm":
                        //ตรวจสอบข้อมูลในระบบ ผ่าน Web Services ภายใน
                        try
                        {

                            if (true)
                            {
                                opSUCCESS = "True";
                                opCODE = "100";
                                opDESC = "SUCCESS";
                                opAMOUNT_RECEIVED = "";
                            }
                            else
                            {
                                opSUCCESS = "False";
                                opCODE = "9MTLXXX";
                                opDESC = "erererererererer";
                            }
                        }
                        catch (Exception ex)
                        {
                            opSUCCESS = "False";
                            opCODE = "9MTLXXX";
                            opDESC = ex.ToString();
                        }
                        break;
                    case "ORCancel":
                        //ตรวจสอบข้อมูลในระบบ ผ่าน Web Services ภายใน
                        try
                        {

                            if (true)
                            {
                                opSUCCESS = "True";
                                opCODE = "100";
                                opDESC = "SUCCESS";
                                opAMOUNT_RECEIVED = "";
                            }
                            else
                            {
                                opSUCCESS = "False";
                                opCODE = "9MTLXXX";
                                opDESC = "erererererererer";
                            }
                        }
                        catch (Exception ex)
                        {
                            opSUCCESS = "False";
                            opCODE = "9MTLXXX";
                            opDESC = ex.ToString();
                        }
                        break;
                    default:
                        opSUCCESS = "False";
                        opCODE = "9MTLXXX";
                        opDESC = "ไม่ได้ส่ง Method ตามที่ตกลงมาให้";
                        break;
                }

                //กำหนดค่าให้ฟิลด์ต่างๆ ของ object สำหรับ RESPONSE class
                obj.TX_ID = opTX_ID;
                obj.LOG_ID = opLOG_ID;
                obj.VENDOR_ID = opVENDOR_ID;
                obj.SERVICE_ID = opSERVICE_ID;
                obj.METHOD = opMETHOD;
                obj.SUCCESS = opSUCCESS;
                obj.CODE = opCODE;
                obj.DESC = opDESC;
                obj.REFERENCE_1 = opREFERENCE_1;
                obj.REFERENCE_2 = opREFERENCE_2;
                obj.REFERENCE_3 = opREFERENCE_3;
                obj.REFERENCE_4 = opREFERENCE_4;
                obj.CUSTOMER_NAME = opCUSTOMER_NAME;
                obj.CUSTOMER_ADDR_1 = opCUSTOMER_ADDR_1;
                obj.CUSTOMER_ADDR_2 = opCUSTOMER_ADDR_2;
                obj.CUSTOMER_ADDR_3 = opCUSTOMER_ADDR_3;
                obj.CUSTOMER_TEL_NO = opCUSTOMER_TEL_NO;
                obj.RETURN1 = opRETURN1;
                obj.RETURN2 = opRETURN2;
                obj.RETURN3 = opRETURN3;
                obj.AMOUNT_RECEIVED = opAMOUNT_RECEIVED;
                obj.PRINT_SLIP = opPRINT_SLIP;

                //LogResponse: Insert Log Response - บันทึก Log Response ผลที่ได้จากการตรวจสอบข้อมูลในระบบ
                logobj.AddWSLogResponse(USERID, ipaddress, "Response", "service", METHOD, refnum, obj.TX_ID, obj.LOG_ID, obj.VENDOR_ID, obj.SERVICE_ID, obj.METHOD, obj.SUCCESS, obj.CODE, obj.DESC, obj.REFERENCE_1, obj.REFERENCE_2, obj.REFERENCE_3, obj.REFERENCE_4, obj.CUSTOMER_NAME, obj.CUSTOMER_ADDR_1, obj.CUSTOMER_ADDR_2, obj.CUSTOMER_ADDR_3, obj.CUSTOMER_TEL_NO, obj.RETURN1, obj.RETURN2, obj.RETURN3, obj.AMOUNT_RECEIVED, obj.PRINT_SLIP);

                return obj;
            }
            catch (Exception ex)
            {
                opSUCCESS = "False";
                opCODE = "9MTLXXX";
                opDESC = ex.ToString();

                //กำหนดค่าให้ฟิลด์ต่างๆ ของ object สำหรับ RESPONSE class
                obj.TX_ID = opTX_ID;
                obj.LOG_ID = opLOG_ID;
                obj.VENDOR_ID = opVENDOR_ID;
                obj.SERVICE_ID = opSERVICE_ID;
                obj.METHOD = opMETHOD;
                obj.SUCCESS = opSUCCESS;
                obj.CODE = opCODE;
                obj.DESC = opDESC;
                obj.REFERENCE_1 = opREFERENCE_1;
                obj.REFERENCE_2 = opREFERENCE_2;
                obj.REFERENCE_3 = opREFERENCE_3;
                obj.REFERENCE_4 = opREFERENCE_4;
                obj.CUSTOMER_NAME = opCUSTOMER_NAME;
                obj.CUSTOMER_ADDR_1 = opCUSTOMER_ADDR_1;
                obj.CUSTOMER_ADDR_2 = opCUSTOMER_ADDR_2;
                obj.CUSTOMER_ADDR_3 = opCUSTOMER_ADDR_3;
                obj.CUSTOMER_TEL_NO = opCUSTOMER_TEL_NO;
                obj.RETURN1 = opRETURN1;
                obj.RETURN2 = opRETURN2;
                obj.RETURN3 = opRETURN3;
                obj.AMOUNT_RECEIVED = opAMOUNT_RECEIVED;
                obj.PRINT_SLIP = opPRINT_SLIP;

                //LogResponse: Insert Log Response - บันทึก Log Response ว่าการเรียก Request เพื่อตรวจสอบข้อมูลในระบบไม่สำเร็จ
                logobj.AddWSLogResponse(USERID, ipaddress, "Response", "service", METHOD, refnum, obj.TX_ID, obj.LOG_ID, obj.VENDOR_ID, obj.SERVICE_ID, obj.METHOD, obj.SUCCESS, obj.CODE, obj.DESC, obj.REFERENCE_1, obj.REFERENCE_2, obj.REFERENCE_3, obj.REFERENCE_4, obj.CUSTOMER_NAME, obj.CUSTOMER_ADDR_1, obj.CUSTOMER_ADDR_2, obj.CUSTOMER_ADDR_3, obj.CUSTOMER_TEL_NO, obj.RETURN1, obj.RETURN2, obj.RETURN3, obj.AMOUNT_RECEIVED, obj.PRINT_SLIP);

                return obj;
            }
        }
    }
    #endregion

    #region Magic Web Services Method For Test
    [WebMethod(Description = "")]
    public GetTransactionByOTP_Result GetTxByOTP(string PIN, string OTP)
    {
        GetTransactionByOTP_Result obj = new GetTransactionByOTP_Result();
        obj = GetWSTransactionByOTP(PIN, OTP);
        return obj;
    }

    [WebMethod(Description = "")]
    public GetTransactionsList_Result GetTxList(string Status)
    {
        GetTransactionsList_Result obj = new GetTransactionsList_Result();
        obj = GetWSTransactionsList(Status);
        return obj;
    }
    #endregion

    #region Private Methods
    // MagicWS Method ใช้ภายใน
    private GetTransactionByOTP_Result GetWSTransactionByOTP(string PIN, string OTP)
    {
        GetTransactionByOTP_Result obj = new GetTransactionByOTP_Result();

        MagicWS_ForCounterService.MagicWS_ForCounterService wsobj = new MagicWS_ForCounterService.MagicWS_ForCounterService();
        wsobj.GetTransactionByOTP(PIN, OTP, out obj.Result, out obj.ClientNumber, out obj.PolicyNumber, out obj.VoucherNumber, out obj.ClaimNumber, out obj.ClaimDate, out obj.MobileNumber, out obj.Status);
        
        return obj;
    }

    private GetTransactionsList_Result GetWSTransactionsList(string Status)
    {
        GetTransactionsList_Result obj = new GetTransactionsList_Result();

        string str;

        MagicWS_ForCounterService.MagicWS_ForCounterService wsobj = new MagicWS_ForCounterService.MagicWS_ForCounterService();
        wsobj.GetTransactionsList(Status, out obj.Result, out str);

        if (str == "")
        {

        }
        else
        {
            string[] v_split_text;
            char[] delimeterChar = new char[] { '|' };
            v_split_text = str.Split(delimeterChar);

            int v_split_text_Length = v_split_text.Length;
            TransactionsList[] listobj = new TransactionsList[v_split_text_Length];
            for (int i = 0; i < v_split_text_Length; i++)
            {
                listobj[i] = new TransactionsList();
                listobj[i] = GetDelimetedByColonToObject(v_split_text[i]);
            }

            obj.transactionsList = listobj;
        }

        return obj;
    }

    // Common Method ใช้ภายใน
    private TransactionsList GetDelimetedByColonToObject(string str)
    {
        TransactionsList obj = new TransactionsList();

        string[] v_split_text;
        char[] delimeterChar = new char[] { ':' };
        v_split_text = str.Split(delimeterChar);
        obj.PIN = v_split_text[0];
        obj.MobileNumber = v_split_text[1];
        obj.ClientNumber = v_split_text[2];
        obj.PolicyNumber = v_split_text[3];
        obj.VoucherNumber = v_split_text[4];
        obj.ClaimNumber = v_split_text[5];
        obj.ClaimDate = v_split_text[6];
        obj.ClientName = v_split_text[7];
        obj.Amount = v_split_text[8];
        obj.Status = v_split_text[9];
        obj.OTPExpiryDate = v_split_text[10];

        return obj;
    }
    #endregion
}

