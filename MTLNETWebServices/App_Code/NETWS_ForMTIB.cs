using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for NETWS_ForMTIB
/// </summary>
[WebService(Namespace = "http://muangthai.co.th/MTLNETWebServices/NETWS_ForMTIB/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class NETWS_ForMTIB : System.Web.Services.WebService {

    protected string admin_username = "apluser";
    protected string admin_password = "rtyhgf";
    protected string ipaddress = HttpContext.Current.Request.UserHostAddress.ToString();
    protected int refnum;
    protected string webserviceName = "NETWS_ForMTIB";
    protected string partnerName = "";

    protected class ApplinXCheckAgentAuthenticationResult
    {
        public string fld_result;
        public string fld_sessionID;
        public string fld_agent_number;
        public string fld_agent_name;
        public string fld_agent_department;
        public string fld_agent_position;
        public string fld_agent_type;
    }

    protected class ApplinXGetAgentDetailResult
    {
        public string fld_result;
        public string fld_sessionID;
        public string fld_agent_addrdoc_line1;
        public string fld_agent_addrdoc_line2;
        public string fld_agent_addrdoc_phone;
        public string fld_agent_address_line1;
        public string fld_agent_address_line2;
        public string fld_agent_bank_account;
        public string fld_agent_client_number;
        public string fld_agent_department;
        public string fld_agent_dob;
        public string fld_agent_end_date;
        public string fld_agent_gender;
        public string fld_agent_license_expire_date;
        public string fld_agent_license_number;
        public string fld_agent_license_start_date;
        public string fld_agent_name;
        public string fld_agent_office;
        public string fld_agent_phone;
        public string fld_agent_pid;
        public string fld_agent_policy_out;
        public string fld_agent_position;
        public string fld_agent_remark_type;
        public string fld_agent_start_date;
        public string fld_agent_status_code;
        public string fld_agent_status_description;
        public string fld_agent_tax_number;
        public string fld_agent_type;
        public string fld_agent_under_to;
        public string fld_agent_surname;
        public string fld_agent_title;
        public string fld_agent_no;
    }

    public class CheckAgentAuthentication_Result
    {
        public string Result;
        public string SessionID;
        public string AgentName;
        public string AgentDepartment;
        public string AgentPosition;
        public string AgentType;
        public string AgentStatus;
    }

    protected class CovertToDateResult
    {
        public int Year;
        public int Month;
        public int Day;
    }

    public NETWS_ForMTIB () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }


    [WebMethod(Description = "Method ใช้สำหรับพิสูจน์ตัวตนของตัวแทนว่ามีสิทธิ์ใช้งานหรือไม่")]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
    public CheckAgentAuthentication_Result CheckAgentAuthentication(string partnerUsername, string partnerPassword, string agentNumber, string agentPassword)
    {
        CheckAgentAuthentication_Result obj = new CheckAgentAuthentication_Result();
        RefRunningTBBLL runningobj = new RefRunningTBBLL();
        this.refnum = runningobj.AddRefRunningTBAndReturn();
        WSLogBLL logobj = new WSLogBLL();
        string methodName = "CheckAgentAuthentication";

        try
        {
            if (partnerUsername == "" || partnerPassword == "" || agentNumber == "" || agentPassword == "")
            {
                this.partnerName = partnerUsername;
                // LogRequest: Insert Log Request
                logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + agentNumber + "|" + agentPassword, this.refnum);

                obj.Result = "notpass_กรุณาระบุข้อมูลให้ครบถ้วน";
            }
            else if (agentNumber.Length != 6 || !MTL.Utils.ThisWeb.CheckIsNumeric(agentNumber))
            {
                this.partnerName = partnerUsername;
                // LogRequest: Insert Log Request
                logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + agentNumber + "|" + agentPassword, this.refnum);

                obj.Result = "notpass_กรุณาระบุเลขที่ตัวแทนให้ถูกต้อง";
            }
            else if (agentPassword.Length != 6 || !MTL.Utils.ThisWeb.CheckIsNumeric(agentPassword))
            {
                this.partnerName = partnerUsername;
                // LogRequest: Insert Log Request
                logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + agentNumber + "|" + agentPassword, this.refnum);

                obj.Result = "notpass_กรุณาระบุรหัสผ่านตัวแทนให้ถูกต้อง";
            }
            else
            {
                // พิสูจน์ตัวตนของพันธมิตรก่อนที่จะให้ใช้งานจริง
                NETWS_ForPartnerAuthenticationChecking.CheckPartnerAuthentication_Result pacobj = CheckPartnerAuthenticationReturnDetail(partnerUsername, partnerPassword, this.ipaddress);
                if (pacobj.Result.Trim().ToLower() == "passed")
                {
                    this.partnerName = pacobj.PartnerName;
                    // LogRequest: Insert Log Request
                    logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + agentNumber + "|" + agentPassword, this.refnum);

                    // ตรวจสอบสิทธิ์ของตัวแทนกับ ApplinX WS_Agent
                    ApplinXCheckAgentAuthenticationResult resAgentObj = new ApplinXCheckAgentAuthenticationResult();
                    MTL.WS_Agent.WS_Agent_ForMTLmPOS.WS_Agent_ForMPosService agentWSObj = new MTL.WS_Agent.WS_Agent_ForMTLmPOS.WS_Agent_ForMPosService();
                    resAgentObj.fld_result = agentWSObj.CheckAgentAuthentication(ref agentNumber, agentPassword, out resAgentObj.fld_sessionID, out resAgentObj.fld_agent_name, out resAgentObj.fld_agent_department, out resAgentObj.fld_agent_position, out resAgentObj.fld_agent_type);
                    if (resAgentObj.fld_result.Trim().ToLower() == "passed")
                    {
                        // ดึงข้อมูลรายละเอียดของตัวแทนกับ ApplinX WS_Admin
                        ApplinXGetAgentDetailResult resAdminObj = new ApplinXGetAgentDetailResult();
                        MTL.WS_Admin.WS_Admin_ForMTLmPOS.WS_Admin_ForMPosService adminWSObj = new MTL.WS_Admin.WS_Admin_ForMTLmPOS.WS_Admin_ForMPosService();
                        resAdminObj.fld_result = adminWSObj.getAgentDetail(this.admin_username, this.admin_password, agentNumber, out resAdminObj.fld_sessionID, out resAdminObj.fld_agent_addrdoc_line1, out resAdminObj.fld_agent_addrdoc_line2, out resAdminObj.fld_agent_addrdoc_phone, out resAdminObj.fld_agent_address_line1, out resAdminObj.fld_agent_address_line2, out resAdminObj.fld_agent_bank_account, out resAdminObj.fld_agent_client_number, out resAdminObj.fld_agent_department, out resAdminObj.fld_agent_dob, out resAdminObj.fld_agent_end_date, out resAdminObj.fld_agent_gender, out resAdminObj.fld_agent_license_expire_date, out resAdminObj.fld_agent_license_number, out resAdminObj.fld_agent_license_start_date, out resAdminObj.fld_agent_name, out resAdminObj.fld_agent_office, out resAdminObj.fld_agent_phone, out resAdminObj.fld_agent_pid, out resAdminObj.fld_agent_policy_out, out resAdminObj.fld_agent_position, out resAdminObj.fld_agent_remark_type, out resAdminObj.fld_agent_start_date, out resAdminObj.fld_agent_status_code, out resAdminObj.fld_agent_status_description, out resAdminObj.fld_agent_tax_number, out resAdminObj.fld_agent_type, out resAdminObj.fld_agent_under_to, out resAdminObj.fld_agent_surname, out resAdminObj.fld_agent_title, out resAdminObj.fld_agent_no);
                        if (resAdminObj.fld_result.Trim().ToLower() == "found")
                        {
                            // เช็คใบอนุญาติตัวแทนว่าหมดอายุหรือยัง?
                            CovertToDateResult dateObj = new CovertToDateResult();
                            dateObj = ConvertToDate(resAdminObj.fld_agent_license_expire_date.Trim());

                            DateTime agentLicenseExpiryDate = new DateTime(dateObj.Year, dateObj.Month, dateObj.Day);

                            if (agentLicenseExpiryDate > DateTime.Now)
                            {
                                obj.Result = "passed";
                                obj.SessionID = resAdminObj.fld_sessionID.Trim();
                                obj.AgentName = resAdminObj.fld_agent_name.Trim() + " " + resAdminObj.fld_agent_surname.Trim();
                                obj.AgentStatus = resAdminObj.fld_agent_status_code.Trim();
                                obj.AgentType = resAdminObj.fld_agent_type.Trim();
                                obj.AgentPosition = resAdminObj.fld_agent_position.Trim();
                                obj.AgentDepartment = resAdminObj.fld_agent_department.Trim();
                            }
                            else
                            {
                                obj.Result = "notpass_ท่านไม่สามารถใช้งานได้ เนื่องจากเลขที่ตัวแทนที่ท่านระบุไม่มีผลบังคับแล้ว";
                            }
                        }
                        else
                        {
                            obj.Result = resAdminObj.fld_result.Trim().Replace("notfound_", "notpass_");
                        }
                    }
                    else
                    {
                        obj.Result = resAgentObj.fld_result.Trim();
                    }
                }
                else
                {
                    this.partnerName = pacobj.PartnerName;
                    // LogRequest: Insert Log Request
                    logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + agentNumber + "|" + agentPassword, this.refnum);

                    obj.Result = pacobj.Result.Trim();
                }
            }

            // LogResponse: Insert Log Response
            logobj.AddWSLog(this.partnerName, this.ipaddress, "Response", this.webserviceName, methodName, obj.Result + "|" + obj.AgentName + "|" + obj.AgentStatus + "|" + obj.AgentType + "|" + obj.AgentPosition + "|" + obj.AgentDepartment + "|" + obj.SessionID, this.refnum);

            return obj;
        }
        catch (Exception ex)
        {
            obj.Result = "notpass_" + ex.Message.ToString();

            // LogResponse: Insert Log Response
            logobj.AddWSLog(this.partnerName, this.ipaddress, "Response", this.webserviceName, methodName, obj.Result + "|" + obj.SessionID, this.refnum);

            return obj;
        }
    }


    #region Method สำหรับพิสูจน์ตัวตนพันธมิตร
    /// <summary>
    /// ใช้สำหรับพิสูจน์ตัวตนพันธมิตรว่ามีสิทธิ์ใช้ Web Services หรือไม่
    /// </summary>
    /// <param name="PartnerUsername">Partner Username</param>
    /// <param name="PartnerPassword">Partner Password</param>
    /// <returns>ผลลัพธ์ว่า passed พร้อมกับรายละเอียดอื่นๆ หรือ notpass_xxxx</returns>
    private NETWS_ForPartnerAuthenticationChecking.CheckPartnerAuthentication_Result CheckPartnerAuthenticationReturnDetail(string PartnerUsername, string PartnerPassword, string IPAddress)
    {
        NETWS_ForPartnerAuthenticationChecking.CheckPartnerAuthentication_Result obj = new NETWS_ForPartnerAuthenticationChecking.CheckPartnerAuthentication_Result();

        NETWS_ForPartnerAuthenticationChecking.ForPartnerAuthenticationChecking wsobj = new NETWS_ForPartnerAuthenticationChecking.ForPartnerAuthenticationChecking();
        obj = wsobj.CheckPartnerAuthenticationReturnDetail(PartnerUsername, PartnerPassword, IPAddress);

        return obj;
    }
    #endregion

    #region Utilities Methods
    // ใช้สำหรับแปลง Date ในรูปแบบ String ไปเป็น Object
    private CovertToDateResult ConvertToDate(string dateString)
    {
        CovertToDateResult obj = new CovertToDateResult();

        string[] delimeterString = new string[] { "/" };
        string[] splitedString = dateString.Split(delimeterString, StringSplitOptions.RemoveEmptyEntries);

        obj.Day = Convert.ToInt32(splitedString[0].ToString());
        obj.Month = Convert.ToInt32(splitedString[1].ToString());
        obj.Year = Convert.ToInt32(splitedString[2].ToString()) - 543;

        return obj;
    }
    #endregion
}

