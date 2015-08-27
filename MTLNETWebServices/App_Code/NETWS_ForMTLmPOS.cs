using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for NETWS_ForMTLmPOS
/// </summary>
[WebService(Namespace = "http://muangthai.co.th/MTLNETWebServices/NETWS_ForMTLmPOS/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class NETWS_ForMTLmPOS : System.Web.Services.WebService {

    protected string admin_username = "apluser";
    protected string admin_password = "rtyhgf";
    protected string ipaddress = HttpContext.Current.Request.UserHostAddress.ToString();
    protected int refnum;
    protected string webserviceName = "NETWS_ForMTLmPOS";
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

    protected class ApplinXCheckAgentOwnerPolicyResult
    {
        public string fld_result;
        public string fld_sessionID;
        public string fld_belong_to_agent;
        public string fld_client_name;
        public string fld_face_amount;
        public string fld_plan_code;
        public string fld_plan_name;
        public string fld_total_premium;
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

    protected class ApplinXGetShortPolicyDetail2Result
    {
        public string fld_result;
        public string fld_sessionID;
        public string fld_service_agent;
        public string fld_client_name;
        public string fld_face_amount;
        public string fld_paid_mode;
        public string fld_plan_code;
        public string fld_plan_name;
        public string fld_total_premium;
        public string fld_policy_status;
        public string fld_issue_date;
        public string fld_warning_message;
    }

    protected class OSBEAppValidateCCPPayment
    {
        public string result;
        public string reasonCode;
        public string reasonText;
        public OSBEAppValidationCCPPaymntApplicationDetail[] applicationDetail;
    }

    protected class OSBEAppValidationCCPPaymntApplicationDetail
    {
        public string temporaryReceiptNumber;
        public string applicationNumber;
        public string assureName;
        public string assureLastname;
        public decimal insured;
        public decimal premium;
        public string planCode;
        public string planName;
    }

    protected class OSBEAppValidationRecipeNumber
    {
        public string result;
        public string responseCode;
        public bool isValid;
        public string reasonCode;
        public string description;
        // ว่านส่งให้เมื่อ 06/07/2558 ส่งรายละเอียดเมื่อ 09/07/2558
        public string resultCase;
        public bool eappFoundCurrentYear;
        public string applicationStatus;
        public string eSignatureState;
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

    public class GetAgentPolicyDetailForPayment_Result
    {
        public string Result;
        public string SessionID;
        public string ServiceAgent;
        public string ClientName;
        public string FaceAmount;
        public string PaidMode;
        public string PlanCode;
        public string PlanName;
        public string TotalPremium;
        public string PolicyStatus;
        public string IssueDate;
        public string WarningMessage;
    }

    public class GetApplicationFromEAPPByTemporaryReceiptNumber_Result
    {
        public string Result;
        public string TemporaryReceiptNumber;
        public string ApplicationNumber;
        public string ClientName;
        public string SumInsured;
        public string Premium;
        public string PlanCode;
        public string PlanName;
    }

    public class CheckTemporaryReceiptNumberInAs400IsCanUse_Result
    {
        public string Result;
        public string ErrorMessage;
    }

    public class CheckTemporaryReceiptNumberAndNumberToPayInSqlIsUsedExceedLimit_Result
    {
        public bool Result { get; set; }
        public string Message { get; set; }
    }

    protected class CovertToDateResult
    {
        public int Year;
        public int Month;
        public int Day;
    }

    public NETWS_ForMTLmPOS () {

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

    [WebMethod(Description = "Method ใช้สำหรับแสดงรายละเอียดกรมธรรม์ของตัวแทนที่พร้อมสำหรับการชำระเงินค่าเบี้ยประกันภัยงานต่ออายุ")]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
    public GetAgentPolicyDetailForPayment_Result GetAgentPolicyDetailForPayment(string partnerUsername, string partnerPassword, string agentNumber, string agentPassword, string policyNumber)
    {
        GetAgentPolicyDetailForPayment_Result obj = new GetAgentPolicyDetailForPayment_Result();
        RefRunningTBBLL runningobj = new RefRunningTBBLL();
        this.refnum = runningobj.AddRefRunningTBAndReturn();
        WSLogBLL logobj = new WSLogBLL();
        string methodName = "GetAgentPolicyByPolicyNumber";

        try
        {
            if (partnerUsername == "" || partnerPassword == "" || policyNumber == "" || agentNumber == "" || agentPassword == "")
            {
                this.partnerName = partnerUsername;
                // LogRequest: Insert Log Request
                logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + agentNumber + "|" + agentPassword + "|" + policyNumber, this.refnum);

                obj.Result = "notpass_กรุณาระบุข้อมูลให้ครบถ้วน";
            }
            else if (policyNumber.Length != 10 || !MTL.Utils.ThisWeb.CheckIsNumeric(policyNumber))
            {
                this.partnerName = partnerUsername;
                // LogRequest: Insert Log Request
                logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + agentNumber + "|" + agentPassword + "|" + policyNumber, this.refnum);

                obj.Result = "notcomplete_กรุณาระบุเลขกรมธรรม์ให้ถูกต้อง";
            }
            else if (agentNumber.Length != 6 || !MTL.Utils.ThisWeb.CheckIsNumeric(agentNumber))
            {
                this.partnerName = partnerUsername;
                // LogRequest: Insert Log Request
                logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + agentNumber + "|" + agentPassword + "|" + policyNumber, this.refnum);

                obj.Result = "notcomplete_กรุณาระบุเลขที่ตัวแทนให้ถูกต้อง";
            }
            else if (agentPassword.Length != 6 || !MTL.Utils.ThisWeb.CheckIsNumeric(agentPassword))
            {
                this.partnerName = partnerUsername;
                // LogRequest: Insert Log Request
                logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + agentNumber + "|" + agentPassword + "|" + policyNumber, this.refnum);

                obj.Result = "notcomplete_กรุณาระบุรหัสผ่านตัวแทนให้ถูกต้อง";
            }
            else
            {
                // พิสูจน์ตัวตนของพันธมิตรก่อนที่จะให้ใช้งานจริง
                NETWS_ForPartnerAuthenticationChecking.CheckPartnerAuthentication_Result pacobj = CheckPartnerAuthenticationReturnDetail(partnerUsername, partnerPassword, this.ipaddress);
                if (pacobj.Result.Trim().ToLower() == "passed")
                {
                    this.partnerName = pacobj.PartnerName;
                    // LogRequest: Insert Log Request
                    logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + agentNumber + "|" + agentPassword + "|" + policyNumber, this.refnum);

                    //// ตรวจสอบว่าเลขกรมธรรม์ที่ระบุเป็นกรมธรรม์ของตัวแทนจริงกับ ApplinX WS_Agent
                    //ApplinXCheckAgentOwnerPolicyResult resAgentObj = new ApplinXCheckAgentOwnerPolicyResult();
                    //MTL.WS_Agent.WS_Agent_ForMTLmPOS.WS_Agent_ForMPosService agentWSObj = new MTL.WS_Agent.WS_Agent_ForMTLmPOS.WS_Agent_ForMPosService();
                    //resAgentObj.fld_result = agentWSObj.CheckAgentOwnerPolicy(agentNumber, agentPassword, policyNumber, out resAgentObj.fld_sessionID, out resAgentObj.fld_belong_to_agent, out resAgentObj.fld_client_name, out resAgentObj.fld_face_amount, out resAgentObj.fld_plan_code, out resAgentObj.fld_plan_name, out resAgentObj.fld_total_premium);
                    //if (resAgentObj.fld_result.Trim().ToLower() == "หมายเลขกรมธรรม์ถูกต้อง")
                    //{
                        // ดึงข้อมูลรายละเอียดของกรมธรรม์กับ ApplinX WS_Admin
                        ApplinXGetShortPolicyDetail2Result resAdminObj = new ApplinXGetShortPolicyDetail2Result();
                        MTL.WS_Admin.WS_Admin_ForMTLmPOS.WS_Admin_ForMPosService adminWSObj = new MTL.WS_Admin.WS_Admin_ForMTLmPOS.WS_Admin_ForMPosService();
                        resAdminObj.fld_result = adminWSObj.GetShortPolicyDetail2(this.admin_username, this.admin_password, policyNumber, out resAdminObj.fld_sessionID, out resAdminObj.fld_service_agent, out resAdminObj.fld_client_name, out resAdminObj.fld_face_amount, out resAdminObj.fld_paid_mode, out resAdminObj.fld_plan_code, out resAdminObj.fld_plan_name, out resAdminObj.fld_total_premium, out resAdminObj.fld_policy_status, out resAdminObj.fld_issue_date, out resAdminObj.fld_warning_message);
                        if (resAdminObj.fld_result.Trim().ToLower() == "found")
                        {
                            if (resAdminObj.fld_policy_status.Trim().ToUpper() != "1" && resAdminObj.fld_policy_status.Trim().ToUpper() != "B" && resAdminObj.fld_policy_status.Trim().ToUpper() != "7" && resAdminObj.fld_policy_status.Trim().ToUpper() != "8" && resAdminObj.fld_policy_status.Trim().ToUpper() != "9")
                            {
                                obj.Result = "notcomplete_กรมธรรม์นี้ไม่สามารถรับชำระผ่านช่องทาง mPOS ได้ กรุณาชำระผ่านช่องทางอื่นๆ ของบริษัทฯ";
                            }
                            else if (resAdminObj.fld_warning_message.Contains("ไม่รับบัตรเครดิต"))
                            {
                                obj.Result = "notcomplete_กรมธรรม์นี้ไม่รับชำระด้วยบัตรเครดิต";
                                obj.WarningMessage = "";
                            }
                            else
                            {
                                obj.Result = "completed";
                                obj.SessionID = resAdminObj.fld_sessionID.Trim();
                                obj.ClientName = resAdminObj.fld_client_name.Trim();
                                obj.FaceAmount = resAdminObj.fld_face_amount.Trim();
                                obj.IssueDate = resAdminObj.fld_issue_date.Trim();
                                obj.PaidMode = resAdminObj.fld_paid_mode.Trim();
                                obj.PlanCode = resAdminObj.fld_plan_code.Trim();
                                obj.PlanName = resAdminObj.fld_plan_name.Trim();
                                obj.PolicyStatus = resAdminObj.fld_policy_status.Trim();
                                obj.ServiceAgent = resAdminObj.fld_service_agent.Trim();
                                obj.TotalPremium = resAdminObj.fld_total_premium.Trim();
                                obj.WarningMessage = resAdminObj.fld_warning_message.Trim();
                            }

                            if (resAdminObj.fld_policy_status.Trim().ToUpper() == "8")
                            {
                                obj.TotalPremium = "";
                            }
                        }
                        else
                        {
                            obj.Result = resAdminObj.fld_result.Trim().Replace("notfound_", "notcomplete_");
                        }
                    //}
                    //else
                    //{
                    //    obj.Result = "notcomplete_" + resAgentObj.fld_result.Trim();
                    //}
                }
                else
                {
                    this.partnerName = pacobj.PartnerName;
                    // LogRequest: Insert Log Request
                    logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + agentNumber + "|" + agentPassword + "|" + policyNumber, this.refnum);

                    obj.Result = pacobj.Result.Trim().Replace("notpass_", "notcomplete_");
                }
            }

            // LogResponse: Insert Log Response
            logobj.AddWSLog(this.partnerName, this.ipaddress, "Response", this.webserviceName, methodName, obj.Result + "|" + obj.PolicyStatus + "|" + obj.TotalPremium + "|" + "|" + obj.WarningMessage + "|" + obj.PlanCode + "|" + obj.PlanName + "|" + obj.ClientName + "|" + obj.SessionID, this.refnum);

            return obj;
        }
        catch (Exception ex)
        {
            obj.Result = "notcomplete_" + ex.Message.ToString();

            // LogResponse: Insert Log Response
            logobj.AddWSLog(this.partnerName, this.ipaddress, "Response", webserviceName, methodName, obj.Result + "|" + obj.SessionID, this.refnum);

            return obj;
        }
    }

    [WebMethod(Description = "Method ใช้สำหรับตรวจสอบเลขที่ใบรับเงินชั่วคราวว่าถูกต้องหรือไม่")]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
    public string CheckIsValidTemporaryReceiptNumber(string partnerUsername, string partnerPassword, string temporaryReceiptNumber)
    {
        string result = "";
        RefRunningTBBLL runningobj = new RefRunningTBBLL();
        this.refnum = runningobj.AddRefRunningTBAndReturn();
        WSLogBLL logobj = new WSLogBLL();
        string methodName = "CheckIsValidTemporaryReceiptNumber";

        try
        {
            if (partnerUsername == "" || partnerPassword == "" || temporaryReceiptNumber == "")
            {
                this.partnerName = partnerUsername;
                // LogRequest: Insert Log Request
                logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + temporaryReceiptNumber, this.refnum);

                result = "notvalid_กรุณาระบุข้อมูลให้ครบถ้วน";
            }
            else if (temporaryReceiptNumber.Length != 12 || !MTL.Utils.ThisWeb.CheckIsNumeric(temporaryReceiptNumber))
            {
                this.partnerName = partnerUsername;
                // LogRequest: Insert Log Request
                logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + temporaryReceiptNumber, this.refnum);

                result = "notvalid_กรุณาระบุเลขที่ใบรับเงินชั่วคราวให้ถูกต้อง";
            }
            else
            {
                // พิสูจน์ตัวตนของพันธมิตรก่อนที่จะให้ใช้งานจริง
                NETWS_ForPartnerAuthenticationChecking.CheckPartnerAuthentication_Result pacobj = CheckPartnerAuthenticationReturnDetail(partnerUsername, partnerPassword, this.ipaddress);
                if (pacobj.Result.Trim().ToLower() == "passed")
                {
                    this.partnerName = pacobj.PartnerName;
                    // LogRequest: Insert Log Request
                    logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + temporaryReceiptNumber, this.refnum);

                    // ตรวจสอบเลขที่ใบรับเงินชั่วคราว
                    MTL.Utils.ThisWeb thisweb = new MTL.Utils.ThisWeb();
                    if (thisweb.CheckIsValidTemporaryBillNumber(temporaryReceiptNumber))
                    {
                        // ตรวจสอบเลขที่ใบรับเงินชั่วคราวว่าได้ถูกบันทึกว่าตัวแทนส่งงานเข้ามายังบริษัทฯ (เก็บอยู่ใน AS400) แล้วหรือยัง หรือสามารถนำมาใช้ได้หรือไม่ ผ่าน OSB EAppService
                        CheckTemporaryReceiptNumberInAs400IsCanUse_Result checkResult = new CheckTemporaryReceiptNumberInAs400IsCanUse_Result();
                        // 20150129: Comment ไว้เพื่อทดสอบไม่ต้องใช้ค่าจาก Web Services ว่าน
                        checkResult = this.CheckTemporaryReceiptNumberInAs400IsCanUse(temporaryReceiptNumber);
                        // 20150129: สำหรับทดสอบโดยไม่ต้องใช้ค่าจาก Web Services ว่าน
                        //checkResult.Result = "testja";
                        if (checkResult.Result == "false")
                        {
                            result = "notvalid_" + checkResult.ErrorMessage.Trim();
                        }
                        else
                        {
                            result = "valid_" + checkResult.ErrorMessage.Trim();
                        }
                    }
                    else
                    {
                        result = "notvalid_เลขที่ใบรับเงินชั่วคราวไม่ถูกต้อง";
                    }
                }
                else
                {
                    this.partnerName = pacobj.PartnerName;
                    // LogRequest: Insert Log Request
                    logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + temporaryReceiptNumber, this.refnum);

                    result = pacobj.Result.Trim().Replace("notpass_", "notvalid_");
                }
            }

            // LogResponse: Insert Log Response
            logobj.AddWSLog(this.partnerName, this.ipaddress, "Response", this.webserviceName, methodName, result + "|" + temporaryReceiptNumber, this.refnum);

            return result;
        }
        catch (Exception ex)
        {
            result = "notvalid_" + ex.Message.ToString();

            // LogResponse: Insert Log Response
            logobj.AddWSLog(this.partnerName, this.ipaddress, "Response", webserviceName, methodName, result + "|", this.refnum);

            return result;
        }
    }

    [WebMethod(Description = "Method ใช้สำหรับแสดงข้อมูลใบคำขอเอาประกันที่พร้อมสำหรับการชำระเงินค่าเบี้ยประกันภัยงานใหม่")]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
    public GetApplicationFromEAPPByTemporaryReceiptNumber_Result GetApplicationFromEAPPByTemporaryReceiptNumber(string partnerUsername, string partnerPassword, string temporaryReceiptNumber)
    {
        GetApplicationFromEAPPByTemporaryReceiptNumber_Result obj = new GetApplicationFromEAPPByTemporaryReceiptNumber_Result();
        RefRunningTBBLL runningobj = new RefRunningTBBLL();
        this.refnum = runningobj.AddRefRunningTBAndReturn();
        WSLogBLL logobj = new WSLogBLL();
        string methodName = "GetApplicationFromEAPPByTemporaryReceiptNumber";
        
        try
        {
            if (partnerUsername == "" || partnerPassword == "" || temporaryReceiptNumber == "")
            {
                this.partnerName = partnerUsername;
                // LogRequest: Insert Log Request
                logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + temporaryReceiptNumber, this.refnum);

                obj.Result = "notcomplete_กรุณาระบุข้อมูลให้ครบถ้วน";
            }
            else if (temporaryReceiptNumber.Length != 12 || !MTL.Utils.ThisWeb.CheckIsNumeric(temporaryReceiptNumber))
            {
                this.partnerName = partnerUsername;
                // LogRequest: Insert Log Request
                logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + temporaryReceiptNumber, this.refnum);

                obj.Result = "notcomplete_กรุณาระบุเลขที่ใบรับเงินชั่วคราวให้ถูกต้อง";
            }
            else
            {
                // พิสูจน์ตัวตนของพันธมิตรก่อนที่จะให้ใช้งานจริง
                NETWS_ForPartnerAuthenticationChecking.CheckPartnerAuthentication_Result pacobj = CheckPartnerAuthenticationReturnDetail(partnerUsername, partnerPassword, this.ipaddress);
                if (pacobj.Result.Trim().ToLower() == "passed")
                {
                    this.partnerName = pacobj.PartnerName;
                    // LogRequest: Insert Log Request
                    logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + temporaryReceiptNumber, this.refnum);

                    // ดึงข้อมูลรายละเอียดของใบคำขอเอาประกันกับ OSB EAppService
                    MTL.OSBWS_EAppService.EApp_ForValidateCCPPayment_PS.ValidateCCPPayment wsObj = new MTL.OSBWS_EAppService.EApp_ForValidateCCPPayment_PS.ValidateCCPPayment();
                    MTL.OSBWS_EAppService.EApp_ForValidateCCPPayment_PS.ResponseApplicationDetail[] respAppDetail;
                    OSBEAppValidateCCPPayment validateCCPObj = new OSBEAppValidateCCPPayment();
                    validateCCPObj.result = wsObj.CallValidateCCPPayment(temporaryReceiptNumber, out validateCCPObj.reasonCode, out validateCCPObj.reasonText, out respAppDetail);
                    if (validateCCPObj.result.Trim() != "00" && validateCCPObj.reasonCode.Trim() != "00")
                    {
                        obj.Result = "notcomplete_" + validateCCPObj.reasonText.Trim();

                        // กรณีที่เช็คแล้วว่าไม่สามารถชำระด้วยบัครเครดิตได้ แต่หากตัวแทนต้องการจะชำระจริงๆ ก็ต้องเสียค่าธรรมเนียมเอง
                        if (validateCCPObj.result.Trim() == "99" && validateCCPObj.reasonCode.Trim() == "92")
                        {
                            obj.ApplicationNumber = respAppDetail[0].applicationNumber.ToString().Trim();
                            obj.ClientName = respAppDetail[0].assureName.ToString().Trim() + " " + respAppDetail[0].assureLastname.ToString().Trim();
                            obj.PlanCode = respAppDetail[0].planCode.ToString().Trim();
                            obj.PlanName = respAppDetail[0].planName.ToString().Trim();
                            obj.Premium = respAppDetail[0].premium.ToString().Trim();
                            obj.SumInsured = respAppDetail[0].insured.ToString().Trim();
                            obj.TemporaryReceiptNumber = respAppDetail[0].tempolaryReceiptNumber.ToString().Trim();
                        }
                    }
                    else
                    {
                        obj.Result = "completed";
                        obj.ApplicationNumber = respAppDetail[0].applicationNumber.ToString().Trim();
                        obj.ClientName = respAppDetail[0].assureName.ToString().Trim() + " " + respAppDetail[0].assureLastname.ToString().Trim();
                        obj.PlanCode = respAppDetail[0].planCode.ToString().Trim();
                        obj.PlanName = respAppDetail[0].planName.ToString().Trim();
                        obj.Premium = respAppDetail[0].premium.ToString().Trim();
                        obj.SumInsured = respAppDetail[0].insured.ToString().Trim();
                        obj.TemporaryReceiptNumber = respAppDetail[0].tempolaryReceiptNumber.ToString().Trim();
                    }
                }
                else
                {
                    this.partnerName = pacobj.PartnerName;
                    // LogRequest: Insert Log Request
                    logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + temporaryReceiptNumber, this.refnum);

                    obj.Result = pacobj.Result.Trim().Replace("notpass_", "notcomplete_");
                }
            }

            // LogResponse: Insert Log Response
            logobj.AddWSLog(this.partnerName, this.ipaddress, "Response", this.webserviceName, methodName, obj.Result + "|" + obj.ApplicationNumber + "|" + obj.ClientName + "|" + "|" + obj.Premium + "|" + obj.PlanCode + "|" + obj.PlanName + "|" + obj.SumInsured, this.refnum);

            return obj;
        }
        catch (Exception ex)
        {
            obj.Result = "notcomplete_" + ex.Message.ToString();

            // LogResponse: Insert Log Response
            logobj.AddWSLog(this.partnerName, this.ipaddress, "Response", webserviceName, methodName, obj.Result + "|", this.refnum);

            return obj;
        }
    }

    [WebMethod(Description = "Method ใช้สำหรับเพิ่มเลขที่ใบรับเงินชั่วคราวที่ยังไม่เคยถูกนำไปใช้งานไว้ในฐานข้อมูลการใช้งาน")]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
    public string AddTemporaryReceiptNumberToUsingLog(string partnerUsername, string partnerPassword, string temporaryReceiptNumber, string temporaryReceiptDate, string payFor, string payForNumber, string payAmount, string agentNumber)
    {
        string result = "";
        RefRunningTBBLL runningobj = new RefRunningTBBLL();
        this.refnum = runningobj.AddRefRunningTBAndReturn();
        WSLogBLL logobj = new WSLogBLL();
        string methodName = "AddTemporaryReceiptNumberToUsingLog";

        try
        {
            if (partnerUsername == "" || partnerPassword == "" || temporaryReceiptNumber == "" || temporaryReceiptDate == "" || payFor == "" || payForNumber == "" || payAmount == "" || agentNumber == "")
            {
                this.partnerName = partnerUsername;
                // LogRequest: Insert Log Request
                logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + temporaryReceiptNumber + "|" + temporaryReceiptDate + "|" + payFor + "|" + payForNumber + "|" + payAmount + "|" + agentNumber, this.refnum);

                result = "notcomplete_กรุณาระบุข้อมูลให้ครบถ้วน";
            }
            else if (temporaryReceiptNumber.Length != 12 || !MTL.Utils.ThisWeb.CheckIsNumeric(temporaryReceiptNumber))
            {
                this.partnerName = partnerUsername;
                // LogRequest: Insert Log Request
                logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + temporaryReceiptNumber + "|" + temporaryReceiptDate + "|" + payFor + "|" + payForNumber + "|" + payAmount + "|" + agentNumber, this.refnum);

                result = "notconmplete_กรุณาระบุเลขที่ใบรับเงินชั่วคราวให้ถูกต้อง";
            }
            else if (payForNumber.Length < 10 || payForNumber.Length > 11 || !MTL.Utils.ThisWeb.CheckIsNumeric(payForNumber))
            {
                this.partnerName = partnerUsername;
                // LogRequest: Insert Log Request
                logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + temporaryReceiptNumber + "|" + temporaryReceiptDate + "|" + payFor + "|" + payForNumber + "|" + payAmount + "|" + agentNumber, this.refnum);

                result = "notcomplete_กรุณาระบุเลขที่กรมธรรม์ หรือเลขที่ใบคำขอเอาประกันภัยให้ถูกต้อง";
            }
            else if (agentNumber.Length != 6 || !MTL.Utils.ThisWeb.CheckIsNumeric(agentNumber))
            {
                this.partnerName = partnerUsername;
                // LogRequest: Insert Log Request
                logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + temporaryReceiptNumber + "|" + temporaryReceiptDate + "|" + payFor + "|" + payForNumber + "|" + payAmount + "|" + agentNumber, this.refnum);

                result = "notcomplete_กรุณาระบุเลขที่ตัวแทนให้ถูกต้อง";
            }
            else
            {
                // พิสูจน์ตัวตนของพันธมิตรก่อนที่จะให้ใช้งานจริง
                NETWS_ForPartnerAuthenticationChecking.CheckPartnerAuthentication_Result pacobj = CheckPartnerAuthenticationReturnDetail(partnerUsername, partnerPassword, this.ipaddress);
                if (pacobj.Result.Trim().ToLower() == "passed")
                {
                    this.partnerName = pacobj.PartnerName;
                    // LogRequest: Insert Log Request
                    logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + temporaryReceiptNumber + "|" + temporaryReceiptDate + "|" + payFor + "|" + payForNumber + "|" + payAmount + "|" + agentNumber, this.refnum);

                    // ตรวจสอบเลขที่ใบรับเงินชั่วคราว
                    MTL.Utils.ThisWeb thisweb = new MTL.Utils.ThisWeb();
                    if (thisweb.CheckIsValidTemporaryBillNumber(temporaryReceiptNumber))
                    {
                        // ตรวจสอบเลขที่ใบรับเงินชั่วคราวว่าได้ถูกบันทึกว่าตัวแทนส่งงานเข้ามายังบริษัทฯ (เก็บอยู่ใน AS400) แล้วหรือยัง หรือสามารถนำมาใช้ได้หรือไม่ ผ่าน OSB EAppService
                        CheckTemporaryReceiptNumberInAs400IsCanUse_Result checkResult = new CheckTemporaryReceiptNumberInAs400IsCanUse_Result();
                        // 20150129: Comment ไว้เพื่อทดสอบไม่ต้องใช้ค่าจาก Web Services ว่าน
                        checkResult = this.CheckTemporaryReceiptNumberInAs400IsCanUse(temporaryReceiptNumber);
                        // 20150129: สำหรับทดสอบโดยไม่ต้องใช้ค่าจาก Web Services ว่าน
                        //checkResult.Result = "testja";

                        if (checkResult.Result == "false")
                        {
                            result = "notcomplete_" + checkResult.ErrorMessage.Trim();
                        }
                        else
                        {
                            // 20150129: ตรวจสอบเลขที่ใบรับเงินชั่วคราว กับเลขที่ใบคำขอเอาประกันหรือเลขที่กรมธรรม์ว่าเคยถูกใช้ไปแล้วครบจำนวนครั้งที่กำหนดหรือยัง ถ้าพบว่าเคยใช้ไปแล้วจะไม่สามารถนำไปใช้งานได้อีก
                            // 20150527: ปรับใช้ class รองรับการตรวจสอบว่าใช้เลขที่ใบรับเงินชั่วคราวนี้ได้อีกกี่ครั้ง
                            CheckTemporaryReceiptNumberAndNumberToPayInSqlIsUsedExceedLimit_Result checkTempAndNumberInSqlObj = new CheckTemporaryReceiptNumberAndNumberToPayInSqlIsUsedExceedLimit_Result();
                            checkTempAndNumberInSqlObj = this.CheckTemporaryReceiptNumberAndNumberToPayInSqlIsUsedExceedLimit(temporaryReceiptNumber, payForNumber);
                            if (checkTempAndNumberInSqlObj.Result == true)
                            {
                                result = "notcomplete_เลขที่ใบรับเงินชั่วคราวนี้ถูกนำไปใช้แล้ว";
                            }
                            else
                            {
                                using (var dbContext = new DAL.MTL_mPOSEntities())
                                {
                                    DAL.TemporaryReceiptUsingLog tempReceipt = new DAL.TemporaryReceiptUsingLog();
                                    tempReceipt.TemporaryReceiptNumber = temporaryReceiptNumber.Trim();

                                    CovertToDateResult dateObj = new CovertToDateResult();
                                    dateObj = ConvertToDate(temporaryReceiptDate);
                                    DateTime tempReceiptDate = new DateTime(dateObj.Year, dateObj.Month, dateObj.Day);
                                    tempReceipt.TemporaryReceiptDate = tempReceiptDate;

                                    tempReceipt.PayFor = payFor.Trim();
                                    tempReceipt.PayForNumber = payForNumber.Trim();
                                    tempReceipt.PayAmount = payAmount.Trim();
                                    tempReceipt.AgentNumber = agentNumber.Trim();
                                    tempReceipt.LogDateTime = DateTime.Now;

                                    dbContext.AddToTemporaryReceiptUsingLog(tempReceipt);
                                    dbContext.SaveChanges();

                                    result = "completed";
                                }
                            }
                        }
                    }
                    else
                    {
                        result = "notcomplete_เลขที่ใบรับเงินชั่วคราวไม่ถูกต้อง";
                    }
                }
                else
                {
                    this.partnerName = pacobj.PartnerName;
                    // LogRequest: Insert Log Request
                    logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + temporaryReceiptNumber + "|" + temporaryReceiptDate + "|" + payFor + "|" + payForNumber + "|" + payAmount + "|" + agentNumber, this.refnum);

                    result = pacobj.Result.Trim().Replace("notpass_", "notcomplete_");
                }
            }

            // LogResponse: Insert Log Response
            logobj.AddWSLog(this.partnerName, this.ipaddress, "Response", this.webserviceName, methodName, result + "|" + temporaryReceiptNumber + "|" + temporaryReceiptDate + "|" + payFor + "|" + payForNumber + "|" + payAmount + "|" + agentNumber, this.refnum);

            return result;
        }
        catch (Exception ex)
        {
            result = "notcomplete_" + ex.Message.ToString();

            // LogResponse: Insert Log Response
            logobj.AddWSLog(this.partnerName, this.ipaddress, "Response", webserviceName, methodName, result + "|", this.refnum);

            return result;
        }
    }

    [WebMethod(Description = "Method ใช้สำหรับตรวจสอบเลขที่ใบรับเงินชั่วคราว กับเลขที่ใบคำขอเอาประกันหรือเลขที่กรมธรรม์ว่าเคยถูกใช้ไปแล้วครบจำนวนครั้งที่กำหนดหรือยัง")]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
    public string CheckTemporaryReceiptNumberAndNumberToPayIsUsedExceedLimit(string partnerUsername, string partnerPassword, string temporaryReceiptNumber, string payForNumber)
    {
        string result = "";
        RefRunningTBBLL runningobj = new RefRunningTBBLL();
        this.refnum = runningobj.AddRefRunningTBAndReturn();
        WSLogBLL logobj = new WSLogBLL();
        string methodName = "CheckTemporaryReceiptNumberAndNumberToPayIsUsedExceedLimit";

        try
        {
            if (partnerUsername == "" || partnerPassword == "" || temporaryReceiptNumber == "")
            {
                this.partnerName = partnerUsername;
                // LogRequest: Insert Log Request
                logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + temporaryReceiptNumber + "|" + payForNumber, this.refnum);

                result = "notvalid_กรุณาระบุข้อมูลให้ครบถ้วน";
            }
            else if (temporaryReceiptNumber.Length != 12 || !MTL.Utils.ThisWeb.CheckIsNumeric(temporaryReceiptNumber))
            {
                this.partnerName = partnerUsername;
                // LogRequest: Insert Log Request
                logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + temporaryReceiptNumber + "|" + payForNumber, this.refnum);

                result = "notvalid_กรุณาระบุเลขที่ใบรับเงินชั่วคราวให้ถูกต้อง";
            }
            else if (payForNumber.Length < 10 || payForNumber.Length > 11 || !MTL.Utils.ThisWeb.CheckIsNumeric(payForNumber))
            {
                this.partnerName = partnerUsername;
                // LogRequest: Insert Log Request
                logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + temporaryReceiptNumber + "|" + payForNumber, this.refnum);

                result = "notvalid_กรุณาระบุเลขที่กรมธรรม์ หรือเลขที่ใบคำขอเอาประกันภัยให้ถูกต้อง";
            }
            else
            {
                // พิสูจน์ตัวตนของพันธมิตรก่อนที่จะให้ใช้งานจริง
                NETWS_ForPartnerAuthenticationChecking.CheckPartnerAuthentication_Result pacobj = CheckPartnerAuthenticationReturnDetail(partnerUsername, partnerPassword, this.ipaddress);
                if (pacobj.Result.Trim().ToLower() == "passed")
                {
                    this.partnerName = pacobj.PartnerName;
                    // LogRequest: Insert Log Request
                    logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + temporaryReceiptNumber + "|" + payForNumber, this.refnum);

                    // ตรวจสอบเลขที่ใบรับเงินชั่วคราว
                    MTL.Utils.ThisWeb thisweb = new MTL.Utils.ThisWeb();
                    if (thisweb.CheckIsValidTemporaryBillNumber(temporaryReceiptNumber))
                    {
                        // ตรวจสอบเลขที่ใบรับเงินชั่วคราวว่าได้ถูกบันทึกว่าตัวแทนส่งงานเข้ามายังบริษัทฯ (เก็บอยู่ใน AS400) แล้วหรือยัง หรือสามารถนำมาใช้ได้หรือไม่ ผ่าน OSB EAppService
                        CheckTemporaryReceiptNumberInAs400IsCanUse_Result checkResult = new CheckTemporaryReceiptNumberInAs400IsCanUse_Result();
                        // 20150129: Comment ไว้เพื่อทดสอบไม่ต้องใช้ค่าจาก Web Services ว่าน
                        checkResult = this.CheckTemporaryReceiptNumberInAs400IsCanUse(temporaryReceiptNumber);
                        // 20150129: สำหรับทดสอบโดยไม่ต้องใช้ค่าจาก Web Services ว่าน
                        //checkResult.Result = "testja";

                        if (checkResult.Result == "false")
                        {
                            result = "notvalid_" + checkResult.ErrorMessage.Trim();
                        }
                        else
                        {
                            // ตรวจสอบเลขที่ใบรับเงินชั่วคราว กับเลขที่ใบคำขอเอาประกันหรือเลขที่กรมธรรม์ว่าเคยถูกใช้ไปแล้วครบจำนวนครั้งที่กำหนดหรือยัง ถ้าพบว่าเคยใช้ไปแล้วจะไม่สามารถนำไปใช้งานได้อีก
                            // 20150527: ปรับใช้ class รองรับการตรวจสอบว่าใช้เลขที่ใบรับเงินชั่วคราวนี้ได้อีกกี่ครั้ง
                            CheckTemporaryReceiptNumberAndNumberToPayInSqlIsUsedExceedLimit_Result checkTempAndNumberInSqlObj = new CheckTemporaryReceiptNumberAndNumberToPayInSqlIsUsedExceedLimit_Result();
                            checkTempAndNumberInSqlObj = this.CheckTemporaryReceiptNumberAndNumberToPayInSqlIsUsedExceedLimit(temporaryReceiptNumber, payForNumber);
                            if (checkTempAndNumberInSqlObj.Result == true)
                            {
                                result = "notvalid_เลขที่ใบรับเงินชั่วคราวนี้ถูกนำไปใช้ครบจำนวนครั้งที่กำหนดแล้ว";
                            }
                            else
                            {
                                //result = "valid";
                                if (checkTempAndNumberInSqlObj.Message == "1")
                                {
                                    result = "valid_ใช้ได้อีก 1 ครั้ง";
                                }
                                else
                                {
                                    result = "valid";
                                }
                            }
                        }
                    }
                    else
                    {
                        result = "notvalid_เลขที่ใบรับเงินชั่วคราวไม่ถูกต้อง";
                    }
                }
                else
                {
                    this.partnerName = pacobj.PartnerName;
                    // LogRequest: Insert Log Request
                    logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + temporaryReceiptNumber + "|" + payForNumber, this.refnum);

                    result = pacobj.Result.Trim().Replace("notpass_", "notvalid_");
                }
            }

            // LogResponse: Insert Log Response
            logobj.AddWSLog(this.partnerName, this.ipaddress, "Response", this.webserviceName, methodName, result + "|" + temporaryReceiptNumber + "|" + payForNumber, this.refnum);

            return result;
        }
        catch (Exception ex)
        {
            result = "notvalid_" + ex.Message.ToString();

            // LogResponse: Insert Log Response
            logobj.AddWSLog(this.partnerName, this.ipaddress, "Response", webserviceName, methodName, result + "|", this.refnum);

            return result;
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

    // ใช้สำหรับตรวจสอบเลขที่ใบรับเงินชั่วคราวว่าเคยถูกใช้ไปแล้วหรือยัง ถ้าพบว่าเคยใช้ไปแล้วจะไม่สามารถนำไปใช้งานได้อีก
    private bool CheckTemporaryReceiptNumberInSqlIsUsed(string temporaryReceiptNumber)
    {
        using (var dbContext = new DAL.MTL_mPOSEntities())
        {
            var tr = from t in dbContext.TemporaryReceiptUsingLog
                     where t.TemporaryReceiptNumber == temporaryReceiptNumber.Trim()
                     select t;

            if (tr.Count() >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    // ใช้สำหรับตรวจสอบเลขที่ใบรับเงินชั่วคราว กับเลขที่ใบคำขอเอาประกันหรือเลขที่กรมธรรม์ว่าเคยถูกใช้ไปแล้วครบจำนวนครั้งที่กำหนดหรือยัง ถ้าพบว่าเคยใช้ไปแล้วจะไม่สามารถนำไปใช้งานได้อีก
    // 20150527: ปรับใช้ class รองรับการตรวจสอบว่าใช้เลขที่ใบรับเงินชั่วคราวนี้ได้อีกกี่ครั้ง
    private CheckTemporaryReceiptNumberAndNumberToPayInSqlIsUsedExceedLimit_Result CheckTemporaryReceiptNumberAndNumberToPayInSqlIsUsedExceedLimit(string temporaryReceiptNumber, string numberToPay)
    {
        CheckTemporaryReceiptNumberAndNumberToPayInSqlIsUsedExceedLimit_Result obj = new CheckTemporaryReceiptNumberAndNumberToPayInSqlIsUsedExceedLimit_Result();

        int limit = 2;

        using (var dbContext = new DAL.MTL_mPOSEntities())
        {
            var tr = from t in dbContext.TemporaryReceiptUsingLog
                     where t.TemporaryReceiptNumber == temporaryReceiptNumber.Trim()
                     select t;

            bool temporaryReceiptNumberHasEqualOrGreaterThanLimit = false;
            bool numberToPayIsEqualToPayToNumber = false;

            if (tr.Count() >= limit)
            {
                temporaryReceiptNumberHasEqualOrGreaterThanLimit = true;
            }

            foreach (var item in tr)
            {
                numberToPayIsEqualToPayToNumber = item.PayForNumber.Trim() == numberToPay.Trim() ? true : false;

                if (numberToPayIsEqualToPayToNumber == false)
                {
                    break;
                }
            }

            // ตรวจสอบว่าถูกต้อง หรือเกินจำนวนครั้งที่กำหนดหรือยัง
            if (tr.Count() == null || tr.Count() == 0)
            {
                //return false; // กรณีเลขที่ใบรับเงินชั่วคราวนี้ยังไม่เคยถูกใช้เลย จะ return ให้เป็นว่ายังไม่เกินจำนวนที่กำหนด
                obj.Result = false; // กรณีเลขที่ใบรับเงินชั่วคราวนี้ยังไม่เคยถูกใช้เลย จะ return ให้เป็นว่ายังไม่เกินจำนวนที่กำหนด
                return obj;
            }
            else if (temporaryReceiptNumberHasEqualOrGreaterThanLimit == true) // กรณีเลขที่ใบรับเงินชั่วคราวมีมากกว่าหรือเท่ากับจำนวนที่กำหนดไว้แล้ว จะ return ให้เป็นว่าเกินจำนวนที่กำหนดแล้ว
            {
                //return true;
                obj.Result = true;
                return obj;
            }
            else if (numberToPayIsEqualToPayToNumber == false) // กรณีเลขที่ใบคำขอเอาประกันหรือเลขที่กรมธรรม์ที่เคยใช้คู่กับเลขที่ใบรับเงินชั่วคราวนี้ไปแล้ว ไม่ตรงกับเลขที่ใบคำขอเอาประกันหรือเลขที่กรมธรรม์ที่ส่งร้องขอเข้ามาใหม่ จะ return ให้เป็นว่าเกินจำนวนที่กำหนดแล้ว
            {
                //return true;
                obj.Result = true;
                return obj;
            }
            else
            {
                var trn = from t in dbContext.TemporaryReceiptUsingLog
                          where t.TemporaryReceiptNumber == temporaryReceiptNumber.Trim() && t.PayForNumber == numberToPay.Trim()
                          select t;

                DateTime trnLogDateTime = System.DateTime.Now;

                // วนลูปเอา LogDateTime ของรายการสุดท้าย (เลขที่ใบรับเงินชั่วคราวที่ถูกใช้ครั้งล่าสุด) มาเก็บไว้ที่ตัวแปร trnLogDateTime
                foreach (var item in trn)
                {
                    trnLogDateTime = (DateTime)item.LogDateTime;
                }

                if (trn.Count() >= limit)
                {
                    //return true; // กรณีเลขที่ใบรับเงินชั่วคราว กับเลขที่ใบคำขอเอาประกันหรือเลขที่กรมธรรม์ (เลขใดเลขหนึ่ง) ถูกใช้ครบจำนวนครั้งที่กำหนด จะ return ให้เป็นว่าเกินจำนวนที่กำหนดแล้ว
                    obj.Result = true; // กรณีเลขที่ใบรับเงินชั่วคราว กับเลขที่ใบคำขอเอาประกันหรือเลขที่กรมธรรม์ (เลขใดเลขหนึ่ง) ถูกใช้ครบจำนวนครั้งที่กำหนด จะ return ให้เป็นว่าเกินจำนวนที่กำหนดแล้ว
                    return obj;
                }
                else if (trnLogDateTime.Date != System.DateTime.Now.Date)
                {
                    //return true; // กรณีเลขที่ใบรับเงินชั่วคราว กับเลขที่ใบคำขอเอาประกันหรือเลขที่กรมธรรม์ (เลขใดเลขหนึ่ง) ถูกใช้ยังไม่ครบจำนวนครั้งที่กำหนด และครั้งที่ถูกใช้ล่าสุดไม่ใช่วันเดียวกับวันปัจจุบัน จะ return ให้เป็นว่าเกินจำนวนที่กำหนดแล้ว
                    obj.Result = true; // กรณีเลขที่ใบรับเงินชั่วคราว กับเลขที่ใบคำขอเอาประกันหรือเลขที่กรมธรรม์ (เลขใดเลขหนึ่ง) ถูกใช้ยังไม่ครบจำนวนครั้งที่กำหนด และครั้งที่ถูกใช้ล่าสุดไม่ใช่วันเดียวกับวันปัจจุบัน จะ return ให้เป็นว่าเกินจำนวนที่กำหนดแล้ว
                    return obj;
                }
                else
                {
                    //return false; // กรณีเลขที่ใบรับเงินชั่วคราว กับเลขที่ใบคำขอเอาประกันหรือเลขที่กรมธรรม์ (เลขใดเลขหนึ่ง) ถูกใช้ยังไม่ครบจำนวนครั้งที่กำหนด จะ return ให้เป็นว่ายังไม่เกินจำนวนที่กำหนด
                    obj.Result = false; // กรณีเลขที่ใบรับเงินชั่วคราว กับเลขที่ใบคำขอเอาประกันหรือเลขที่กรมธรรม์ (เลขใดเลขหนึ่ง) ถูกใช้ยังไม่ครบจำนวนครั้งที่กำหนด จะ return ให้เป็นว่ายังไม่เกินจำนวนที่กำหนด
                    obj.Message = "1";
                    return obj;
                }
            }
        }
    }

    // ใช้สำหรับตรวจสอบเลขที่ใบรับเงินชั่วคราวว่าได้ถูกบันทึกว่าตัวแทนส่งงานเข้ามายังบริษัทฯ (เก็บอยู่ใน AS400) แล้วหรือยัง หรือสามารถนำมาใช้ได้หรือไม่ ผ่าน OSB EAppService
    private CheckTemporaryReceiptNumberInAs400IsCanUse_Result CheckTemporaryReceiptNumberInAs400IsCanUse(string temporaryReceiptNumber)
    {
        CheckTemporaryReceiptNumberInAs400IsCanUse_Result obj = new CheckTemporaryReceiptNumberInAs400IsCanUse_Result();
        // 20150327: Wanpaya แจ้งเมื่อ 23/03/2558 สำหรับ MTLmPOS ให้ใช้ Service ในการ Validate ใหม่ โดยตัดเรื่องการ Validate เลขที่ใบรับเงินชั่วคราวซ้ำใน 1 ปี ออก
        //MTL.OSBWS_EAppService.EApp_ForValidateRecipeNumber_PS.ValidateRecipeNumber wsObj = new MTL.OSBWS_EAppService.EApp_ForValidateRecipeNumber_PS.ValidateRecipeNumber();
        //OSBEAppValidationRecipeNumber validateRecipeObj = new OSBEAppValidationRecipeNumber();
        //validateRecipeObj.result = wsObj.CallValidateRecipeNumber(System.DateTime.Now, temporaryReceiptNumber, "", "", out validateRecipeObj.responseCode, out validateRecipeObj.isValid, out validateRecipeObj.reasonCode, out validateRecipeObj.description);

        //MTL.OSBWS_EAppService.EApp_ForValidateRecipeNumberWithoutDuplicate_PS.ValidateRecipeNumber wsObj = new MTL.OSBWS_EAppService.EApp_ForValidateRecipeNumberWithoutDuplicate_PS.ValidateRecipeNumber();
        //OSBEAppValidationRecipeNumber validateRecipeObj = new OSBEAppValidationRecipeNumber();
        //validateRecipeObj.result = wsObj.CallValidateRecipeNumber(System.DateTime.Now, temporaryReceiptNumber, "", "", out validateRecipeObj.responseCode, out validateRecipeObj.isValid, out validateRecipeObj.reasonCode, out validateRecipeObj.description);

        //if (validateRecipeObj.responseCode.Trim() != "00")
        //{
        //    if (validateRecipeObj.isValid == false)
        //    {
        //        obj.Result = "false";
        //        switch (validateRecipeObj.reasonCode.Trim())
        //        {
        //            case "01":
        //                obj.ErrorMessage = "เล่มที่ใบรับเงินชั่วคราวยังไม่ได้ลงทะเบียน - W01";
        //                break;
        //            case "02":
        //                obj.ErrorMessage = "เลขที่ใบรับเงินชั่วคราวใช้ซ้ำอยู่ในระยะเวลา 1 ปี - W02";
        //                break;
        //            case "03":
        //                obj.ErrorMessage = "เลขที่ใบรับเงินชั่วคราวไม่ถูกต้อง - W03";
        //                break;
        //            case "04":
        //                // 20150604: ว่านแจ้งแก้ไขเมื่อ 28/05/2558
        //                //obj.ErrorMessage = "ใบคำขอเอาประกันมีการบันทึกเลขที่ใบรับเงินชั่วคราวอยู่แล้ว - W04";
        //                obj.ErrorMessage = "เลขที่ใบรับเงินชั่วคราวยังไม่ถูกบันทึกที่ระบบ E-Application - W04";
        //                break;
        //            default:
        //                obj.ErrorMessage = "ไม่สามารถใช้เลขที่ใบรับเงินชั่วคราวนี้ได้ - W99";
        //                break;
        //        }
        //    }
        //    else
        //    {
        //        obj.Result = "true";
        //    }
        //}
        //else
        //{
        //    obj.Result = "true";
        //}

        // ว่านส่งให้เมื่อ 06/07/2558 ส่งรายละเอียดเมื่อ 09/07/2558
        MTL.OSBWS_EAppService.EApp_ForValidateRecipeNumberForAll_PS.ValidateRecipeNumberForAll wsObj = new MTL.OSBWS_EAppService.EApp_ForValidateRecipeNumberForAll_PS.ValidateRecipeNumberForAll();
        OSBEAppValidationRecipeNumber validateRecipeObj = new OSBEAppValidationRecipeNumber();
        validateRecipeObj.result = wsObj.CallValidateRecipeNumberForAll(System.DateTime.Now, temporaryReceiptNumber, "", "", out validateRecipeObj.isValid, out validateRecipeObj.reasonCode, out validateRecipeObj.resultCase, out validateRecipeObj.eappFoundCurrentYear, out validateRecipeObj.applicationStatus, out validateRecipeObj.eSignatureState, out validateRecipeObj.description);

        switch (validateRecipeObj.reasonCode.Trim())
        {
            case "01":
                obj.Result = "true";
                obj.ErrorMessage = "งาน Non E-App หรือ งานต่ออายุ - W01";
                break;
            case "02":
                obj.Result = "true";
                obj.ErrorMessage = "งานใหม่ E-App สามารถตัดเงินได้ - W02";
                break;
            case "91":
                obj.Result = "false";
                obj.ErrorMessage = "เล่มใบรับเงินยังไม่ได้ลงทะเบียน - W91";
                break;
            case "93":
                obj.Result = "false";
                //obj.ErrorMessage = "เลขที่ใบรับเงินไม่ถูกฟอร์แมต - W93";
                // ErrorMessage จากอ้อ ITS เมื่อ 09/07/2558
                obj.ErrorMessage = "เลขที่ใบรับเงินชั่วคราวไม่ถูกฟอร์แมต - W93";
                break;
            case "94":
                obj.Result = "false";
                obj.ErrorMessage = "ใบคำขอมีการบันทึกเลขที่ใบเสร็จอยู่แล้ว - W94";
                break;
            case "95":
                obj.Result = "false";
                //obj.ErrorMessage = "งานใหม่ที่ยังไม่ Submit หรือยังไม่ตัดสินใจเซ็นต์ E-Signature - W95";
                // ErrorMessage จากอ้อ ITS เมื่อ 09/07/2558
                obj.ErrorMessage = "ข้อมูลใบคำขอในระบบ E-Application ยังไม่เรียบร้อย กรุณาตรวจสอบใบคำขออีกครั้ง - W95";
                break;
            case "92":
                obj.Result = "false";
                //obj.ErrorMessage = "เลขซ้ำอยู่ในช่วงระยะเวลา 1 ปี - W92";
                // ErrorMessage จากอ้อ ITS เมื่อ 09/07/2558
                obj.ErrorMessage = "เลขที่ใบรับเงินชั่วคราวมีการใช้ซ้ำภายใน 1 ปี - W92";
                break;
            case "99":
                obj.Result = "false";
                obj.ErrorMessage = "Error อื่นๆ - W99";
                break;
            default:
                obj.Result = "false";
                obj.ErrorMessage = "";
                break;
        }

        return obj;
    }
    #endregion
}

