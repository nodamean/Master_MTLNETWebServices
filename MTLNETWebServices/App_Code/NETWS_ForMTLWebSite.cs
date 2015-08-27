using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for NETWS_ForMTLWebSite
/// </summary>
[WebService(Namespace = "http://muangthai.co.th/MTLNETWebServices/NETWS_ForMTLWebSite/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class NETWS_ForMTLWebSite : System.Web.Services.WebService {

    protected string admin_username = "apluser";
    protected string admin_password = "rtyhgf";
    protected string ipaddress = HttpContext.Current.Request.UserHostAddress.ToString();
    protected int refnum;
    protected string webserviceName = "NETWS_ForMTLWebSite";
    protected string partnerName = "";

    public enum Gender { FEMALE, MALE }
    public Dictionary<int, char> GenderValues = new Dictionary<int, char>
    {
        {0, 'F'},
        {1, 'M'}
    };
    public enum PaymentMethod { ONE_MONTH, THREE_MONTHS, SIX_MONTHS, TWELVE_MONTHS }
    public Dictionary<int, int> PaymentMethodValues = new Dictionary<int, int>
    {
        {0, 1},
        {1, 3},
        {2, 6},
        {3, 12}
    };

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
        public string AgentPictureURL;
    }

    protected class ApplinXGetAgentNumberSearchByNameResult
    {
        public string fld_result;
        public string fld_sessionID;
        public string fld_agent_number;
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

    protected class MagicGetAgentNumberSearchByLicenseResult
    {
        public string p_result;
        public string p_agent_number;
    }

    public class CalculatePremium_Result
    {
        public string Result;
        public string Premium;
    }

    public class GetAgentStatus_Result
    {
        public string Result;
        public string LicenseNumber;
        public string Name;
        public string AgentNumber;
        public string Department;
        public string Position;
        public string Gender;
        public string Phone;
        public string DoB;
        public string AgentPictureURL;
    }

    public class GetPolicyDetailForPayment_Result
    {
        public string Result;
        public string SessionID;
        public string PaymentTypeToPay;
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

    public class SendSms_Result
    {
        public string Result { get; set; }
        public string NetInnovaMessageId { get; set; }
        public string NetInnovaTaskId { get; set; }
    }

    public class SendEmail_Result
    {
        public string Result { get; set; }
    }

    public NETWS_ForMTLWebSite()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(Description = "Method ใช้สำหรับคำนวณเบี้ยประกันภัย")]
    public CalculatePremium_Result CalculatePremium(string partnerUsername, string partnerPassword, string planCode, PaymentMethod paymentMethod, string age, Gender gender, string amount, string channel, string occupationGroup)
    {
        /*
        Call WS_SQM_CalculateService.CalculatePremium(Channel (int), SelectedPlanCode (string), PaymentMethod (int), CustomerAge (int), CustomerGender (char), MainInsured (decimal), CustomerOccupationGroup (strimg)); 
        ระบุ Channel เป็นรหัสช่องทางที่กำหนด เช่น 6, 
        ระบุ PaymentMethod เป็น 12/06/03/01
        ระบุ CustomerGender เป็น F/M , 
        ระบุ CustomerOccupationGroup เป็น 001
        */

        CalculatePremium_Result obj = new CalculatePremium_Result();
        RefRunningTBBLL runningobj = new RefRunningTBBLL();
        this.refnum = runningobj.AddRefRunningTBAndReturn();
        WSLogBLL logobj = new WSLogBLL();
        string methodName = "CalculatePremium";

        int vChannel = 0;
        int vPaymentMethod = 0;
        int vAge = 0;
        char vGender = 'F';
        decimal vMainInsured = 0;
        string vOccupationGroup = "";

        try
        {
            if (partnerUsername == "" || partnerPassword == "" || planCode == "" || paymentMethod.ToString() == "" || age == "" || gender.ToString() == "" || amount == "")
            {
                this.partnerName = partnerUsername;
                // LogRequest: Insert Log Request
                logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + planCode + "|" + gender.ToString() + "|" + age + "|" + amount + "|" + paymentMethod.ToString() + "|" + channel + "|" + occupationGroup, this.refnum);

                obj.Result = "notcomplete_กรุณาระบุข้อมูลให้ครบถ้วน";
            }
            else
            {
                // พิสูจน์ตัวตนของพันธมิตรก่อนที่จะให้ใช้งานจริง
                NETWS_ForPartnerAuthenticationChecking.CheckPartnerAuthentication_Result pacobj = CheckPartnerAuthenticationReturnDetail(partnerUsername, partnerPassword, this.ipaddress);
                if (pacobj.Result.Trim().ToLower() == "passed")
                {
                    //vChannel = 32;
                    // 20150325: แก้ให้ระบุช่องทางมาเลย ไม่ fix ให้
                    vChannel = Convert.ToInt32(channel);
                    //vPaymentMethod = Convert.ToInt32(paymentMethod);
                    vPaymentMethod = this.PaymentMethodValues[Convert.ToInt32(paymentMethod.ToString("D"))];
                    vAge = Convert.ToInt32(age);
                    //vGender = gender.ToString().ToUpper() == "FEMALE" ? 'F' : 'M';
                    vGender = this.GenderValues[Convert.ToInt32(gender.ToString("D"))];
                    vMainInsured = Convert.ToDecimal(amount);
                    //vOccupationGroup = "001";
                    // 20150325: แก้ให้ระบุช่องทางมาเลย ไม่ fix ให้
                    vOccupationGroup = occupationGroup;

                    this.partnerName = pacobj.PartnerName;
                    // LogRequest: Insert Log Request
                    logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + planCode + "|" + vGender.ToString() + "|" + vAge + "|" + vMainInsured + "|" + vPaymentMethod + "|" + vChannel + "|" + vOccupationGroup, this.refnum);

                    try
                    {
                        MTL.NETWS_ForSmartProposal.WS_SQM_CalculateService wsobj = new MTL.NETWS_ForSmartProposal.WS_SQM_CalculateService();
                        //string result = wsobj.CalculatePremium(vChannel, planCode, vPaymentMethod, vAge, vGender, vMainInsured, vOccupationGroup);
                        string result = wsobj.CompletePremium(vChannel, planCode, vPaymentMethod, vAge, vGender, vMainInsured, vOccupationGroup);
                        if (Convert.ToDecimal(result) >= 0)
                        {
                            // ช่องทาง 32 (For Online Sales) แบบประกัน EON08C ขาย + ส่วนควบ WP
                            if (channel.Trim() == "32" && planCode == "EON08C")
                            {
                                string resultWPRider = this.CalculateRiderWP(planCode, vPaymentMethod.ToString(), vAge.ToString(), gender.ToString() == "MALE" ? "M" : "F", vMainInsured.ToString(), result);
                                
                                result = (Convert.ToDecimal(result) + Convert.ToDecimal(resultWPRider)).ToString("N2");

                                obj.Result = "completed";
                                obj.Premium = result;
                            }
                            else
                            {
                                obj.Result = "completed";
                                obj.Premium = result;
                            }
                        }
                        else
                        {
                            if (result.Trim().Contains("ไม่พบข้อมูลเบี้ยประกัน (Premium)"))
                            {
                                obj.Result = "notcomplete_ไม่สามารถคำนวณเบี้ยประกันภัยได้ เนื่องจากอายุ หรือเพศ หรือจำนวนเงินเอาประกันภัยที่ท่านระบุไม่อยู่ในเงื่อนไขการรับประกัน";
                            }
                            else if (result.Trim().Contains("ไม่พบแบบประกัน"))
                            {
                                obj.Result = "notcomplete_ไม่สามารถคำนวณเบี้ยประกันภัยได้ เนื่องจากไม่พบข้อมูลของแบบประกันที่ท่านระบุในระบบ";
                            }
                            else
                            {
                                obj.Result = "notcomplete_" + result;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.ToString().Trim().Contains("ไม่พบข้อมูลเบี้ยประกัน (Premium)"))
                        {
                            obj.Result = "notcomplete_ไม่สามารถคำนวณเบี้ยประกันภัยได้ เนื่องจากอายุ หรือเพศ หรือจำนวนเงินเอาประกันภัยที่ท่านระบุไม่อยู่ในเงื่อนไขการรับประกัน";
                        }
                        else if (ex.Message.ToString().Trim().Contains("ไม่พบแบบประกัน"))
                        {
                            obj.Result = "notcomplete_ไม่สามารถคำนวณเบี้ยประกันภัยได้ เนื่องจากไม่พบข้อมูลของแบบประกันที่ท่านระบุในระบบ";
                        }
                        else if (ex.Message.ToString().Trim().Contains("The underlying connection was closed: A connection that was expected"))
                        {
                            obj.Result = "notcomplete_ไม่สามารถคำนวณเบี้ยประกันภัยได้ กรุณาลองใหม่อีกครั้ง";
                        }
                        else
                        {
                            obj.Result = "notcomplete_" + ex.Message.ToString();
                        }
                    }
                }
                else
                {
                    this.partnerName = pacobj.PartnerName;
                    // LogRequest: Insert Log Request
                    logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + planCode + "|" + vGender.ToString() + "|" + vAge + "|" + vMainInsured + "|" + vPaymentMethod + "|" + vChannel + "|" + vOccupationGroup, this.refnum);

                    obj.Result = pacobj.Result.Trim().Replace("notpass_", "notcomplete_");
                }
            }

            // LogResponse: Insert Log Response
            logobj.AddWSLog(this.partnerName, this.ipaddress, "Response", this.webserviceName, methodName, obj.Result + "|" + obj.Premium + "|" + planCode + "|" + vGender.ToString() + "|" + vAge + "|" + vMainInsured + "|" + vPaymentMethod + "|" + vChannel + "|" + vOccupationGroup, this.refnum);

            return obj;
        }
        catch (Exception ex)
        {
            obj.Result = "notcomplete_" + ex.Message.ToString();

            // LogResponse: Insert Log Response
            logobj.AddWSLog(this.partnerName, this.ipaddress, "Response", this.webserviceName, methodName, obj.Result, this.refnum);

            return obj;
        }
    }

    [WebMethod(Description = "Method ใช้สำหรับแสดงสถานะตัวแทน โดยระบุเลขที่ตัวแทน")]
    public GetAgentStatus_Result GetAgentStatusByAgentNumber(string partnerUsername, string partnerPassword, string agentNumber)
    {
        GetAgentStatus_Result obj = new GetAgentStatus_Result();
        RefRunningTBBLL runningobj = new RefRunningTBBLL();
        this.refnum = runningobj.AddRefRunningTBAndReturn();
        WSLogBLL logobj = new WSLogBLL();
        string methodName = "GetAgentStatusByAgentNumber";

        try
        {
            if (partnerUsername == "" || partnerPassword == "" || agentNumber == "")
            {
                this.partnerName = partnerUsername;
                // LogRequest: Insert Log Request
                logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + agentNumber, this.refnum);

                obj.Result = "notcomplete_กรุณาระบุข้อมูลให้ครบถ้วน";
            }
            else if (agentNumber.Length != 6 || !MTL.Utils.ThisWeb.CheckIsNumeric(agentNumber))
            {
                this.partnerName = partnerUsername;
                // LogRequest: Insert Log Request
                logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + agentNumber, this.refnum);

                obj.Result = "notcomplete_กรุณาระบุเลขที่ตัวแทนให้ถูกต้อง";
            }
            else
            {
                // พิสูจน์ตัวตนของพันธมิตรก่อนที่จะให้ใช้งานจริง
                NETWS_ForPartnerAuthenticationChecking.CheckPartnerAuthentication_Result pacobj = CheckPartnerAuthenticationReturnDetail(partnerUsername, partnerPassword, this.ipaddress);
                if (pacobj.Result.Trim().ToLower() == "passed")
                {
                    this.partnerName = pacobj.PartnerName;
                    // LogRequest: Insert Log Request
                    logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + agentNumber, this.refnum);
                
                    // ตรวจสอบสถานะตัวแทน
                    ApplinXGetAgentDetailResult resAgentDetailObj = new ApplinXGetAgentDetailResult();
                    resAgentDetailObj = this.GetAgentDetail(agentNumber);
                    if (resAgentDetailObj.fld_result.Trim().ToLower() == "found")
                    {
                        // เช็คสถานะตัวแทน
                        if (resAgentDetailObj.fld_agent_status_code.Trim() == "A")
                        {
                            obj.Result = "completed";
                            obj.AgentNumber = resAgentDetailObj.fld_agent_no.Trim();
                            obj.Name = resAgentDetailObj.fld_agent_title.Trim() + " " + resAgentDetailObj.fld_agent_name.Trim() + " " + resAgentDetailObj.fld_agent_surname.Trim();
                            obj.Gender = resAgentDetailObj.fld_agent_gender.Trim();
                            obj.DoB = resAgentDetailObj.fld_agent_dob.Trim();
                            obj.Phone = resAgentDetailObj.fld_agent_phone.Trim();
                            obj.LicenseNumber = resAgentDetailObj.fld_agent_license_number.Trim();
                            obj.Department = resAgentDetailObj.fld_agent_department.Trim();
                            obj.Position = resAgentDetailObj.fld_agent_position.Trim();
                            obj.AgentPictureURL = resAgentDetailObj.AgentPictureURL.Trim();
                        }
                        else
                        {
                            obj.Result = "notcomplete_ตัวแทนคนนี้ได้พ้นสภาพจากการเป็นตัวแทนกับบริษัทฯ แล้ว";
                        }
                    }
                    else
                    {
                        obj.Result = resAgentDetailObj.fld_result.Trim().Replace("notfound_", "notcomplete_");
                    }
                }
                else
                {
                    this.partnerName = pacobj.PartnerName;
                    // LogRequest: Insert Log Request
                    logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + agentNumber, this.refnum);

                    obj.Result = pacobj.Result.Trim().Replace("notpass_", "notcomplete_");
                }
            }

            // LogResponse: Insert Log Response
            logobj.AddWSLog(this.partnerName, this.ipaddress, "Response", this.webserviceName, methodName, obj.Result + "|" + obj.AgentNumber + "|" + obj.Name + "|" + obj.LicenseNumber, this.refnum);

            return obj;
        }
        catch (Exception ex)
        {
            obj.Result = "notcomplete_" + ex.Message.ToString();

            // LogResponse: Insert Log Response
            logobj.AddWSLog(this.partnerName, this.ipaddress, "Response", this.webserviceName, methodName, obj.Result, this.refnum);

            return obj;
        }
    }

    [WebMethod(Description = "Method ใช้สำหรับแสดงสถานะตัวแทน โดยระบุชื่อนามสกุลตัวแทน")]
    public GetAgentStatus_Result GetAgentStatusByName(string partnerUsername, string partnerPassword, string agentName, string agentSurname)
    {
        GetAgentStatus_Result obj = new GetAgentStatus_Result();
        RefRunningTBBLL runningobj = new RefRunningTBBLL();
        this.refnum = runningobj.AddRefRunningTBAndReturn();
        WSLogBLL logobj = new WSLogBLL();
        string methodName = "GetAgentStatusByName";

        try
        {
            if (partnerUsername == "" || partnerPassword == "" || agentName == "" || agentSurname == "")
            {
                this.partnerName = partnerUsername;
                // LogRequest: Insert Log Request
                logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + agentName + "|" + agentSurname, this.refnum);

                obj.Result = "notcomplete_กรุณาระบุข้อมูลให้ครบถ้วน";
            }
            else
            {
                // พิสูจน์ตัวตนของพันธมิตรก่อนที่จะให้ใช้งานจริง
                NETWS_ForPartnerAuthenticationChecking.CheckPartnerAuthentication_Result pacobj = CheckPartnerAuthenticationReturnDetail(partnerUsername, partnerPassword, this.ipaddress);
                if (pacobj.Result.Trim().ToLower() == "passed")
                {
                    this.partnerName = pacobj.PartnerName;
                    // LogRequest: Insert Log Request
                    logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + agentName + "|" + agentSurname, this.refnum);

                    // ค้นหาเลขที่ตัวแทนจากชื่อนามสกุล
                    ApplinXGetAgentNumberSearchByNameResult resAdminObj = new ApplinXGetAgentNumberSearchByNameResult();
                    MTL.WS_Admin.WS_Admin_ForCMS.WS_Admin_ForCMSService adminWSObj = new MTL.WS_Admin.WS_Admin_ForCMS.WS_Admin_ForCMSService();
                    resAdminObj.fld_result = adminWSObj.getAgentNumberSearchByName(this.admin_username, this.admin_password, agentName, agentSurname, out resAdminObj.fld_sessionID, out resAdminObj.fld_agent_number);
                    if (resAdminObj.fld_result.Trim() == "found")
                    {
                        // ตรวจสอบสถานะตัวแทน
                        ApplinXGetAgentDetailResult resAgentDetailObj = new ApplinXGetAgentDetailResult();
                        resAgentDetailObj = this.GetAgentDetail(resAdminObj.fld_agent_number.Trim());
                        if (resAgentDetailObj.fld_result.Trim().ToLower() == "found")
                        {
                            // เช็คสถานะตัวแทน
                            if (resAgentDetailObj.fld_agent_status_code.Trim() == "A")
                            {
                                obj.Result = "completed";
                                obj.AgentNumber = resAgentDetailObj.fld_agent_no.Trim();
                                obj.Name = resAgentDetailObj.fld_agent_title.Trim() + " " + resAgentDetailObj.fld_agent_name.Trim() + " " + resAgentDetailObj.fld_agent_surname.Trim();
                                obj.Gender = resAgentDetailObj.fld_agent_gender.Trim();
                                obj.DoB = resAgentDetailObj.fld_agent_dob.Trim();
                                obj.Phone = resAgentDetailObj.fld_agent_phone.Trim();
                                obj.LicenseNumber = resAgentDetailObj.fld_agent_license_number.Trim();
                                obj.Department = resAgentDetailObj.fld_agent_department.Trim();
                                obj.Position = resAgentDetailObj.fld_agent_position.Trim();
                                obj.AgentPictureURL = resAgentDetailObj.AgentPictureURL.Trim();
                            }
                            else
                            {
                                obj.Result = "notcomplete_ตัวแทนคนนี้ได้พ้นสภาพจากการเป็นตัวแทนกับบริษัทฯ แล้ว";
                            }
                        }
                        else
                        {
                            obj.Result = resAgentDetailObj.fld_result.Trim().Replace("notfound_", "notcomplete_");
                        }
                    }
                    else
                    {
                        obj.Result = "notcomplete_ไม่พบตัวแทนที่มีชื่อนามสกุลตามที่ท่านระบุ";
                    }
                }
                else
                {
                    this.partnerName = pacobj.PartnerName;
                    // LogRequest: Insert Log Request
                    logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + agentName + "|" + agentSurname, this.refnum);

                    obj.Result = pacobj.Result.Trim().Replace("notpass_", "notcomplete_");
                }
            }

            // LogResponse: Insert Log Response
            logobj.AddWSLog(this.partnerName, this.ipaddress, "Response", this.webserviceName, methodName, obj.Result + "|" + obj.AgentNumber + "|" + obj.Name + "|" + obj.LicenseNumber, this.refnum);

            return obj;
        }
        catch (Exception ex)
        {
            obj.Result = "notcomplete_" + ex.Message.ToString();

            // LogResponse: Insert Log Response
            logobj.AddWSLog(this.partnerName, this.ipaddress, "Response", this.webserviceName, methodName, obj.Result, this.refnum);

            return obj;
        }
    }

    [WebMethod(Description = "Method ใช้สำหรับแสดงสถานะตัวแทน โดยระบุเลขที่ใบอนุญาติตัวแทน")]
    public GetAgentStatus_Result GetAgentStatusByLicenseNumber(string partnerUsername, string partnerPassword, string agentLicenseNumber)
    {
        GetAgentStatus_Result obj = new GetAgentStatus_Result();
        RefRunningTBBLL runningobj = new RefRunningTBBLL();
        this.refnum = runningobj.AddRefRunningTBAndReturn();
        WSLogBLL logobj = new WSLogBLL();
        string methodName = "GetAgentStatusByLicenseNumber";

        try
        {
            if (partnerUsername == "" || partnerPassword == "" || agentLicenseNumber == "")
            {
                this.partnerName = partnerUsername;
                // LogRequest: Insert Log Request
                logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + agentLicenseNumber, this.refnum);

                obj.Result = "notcomplete_กรุณาระบุข้อมูลให้ครบถ้วน";
            }
            else if (agentLicenseNumber.Length != 10 || !MTL.Utils.ThisWeb.CheckIsNumeric(agentLicenseNumber))
            {
                this.partnerName = partnerUsername;
                // LogRequest: Insert Log Request
                logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + agentLicenseNumber, this.refnum);

                obj.Result = "notcomplete_กรุณาระบุเลขที่ใบอนุญาติตัวแทนให้ถูกต้อง";
            }
            else
            {
                // พิสูจน์ตัวตนของพันธมิตรก่อนที่จะให้ใช้งานจริง
                NETWS_ForPartnerAuthenticationChecking.CheckPartnerAuthentication_Result pacobj = CheckPartnerAuthenticationReturnDetail(partnerUsername, partnerPassword, this.ipaddress);
                if (pacobj.Result.Trim().ToLower() == "passed")
                {
                    this.partnerName = pacobj.PartnerName;
                    // LogRequest: Insert Log Request
                    logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + agentLicenseNumber, this.refnum);

                    // ค้นหาเลขที่ตัวแทนจากเลขที่ใบอนุญาติตัวแทน
                    MagicGetAgentNumberSearchByLicenseResult resMagicObj = new MagicGetAgentNumberSearchByLicenseResult();
                    MTL.MagicWS_ForCMS.MagicWS_ForCMS magicWSObj = new MTL.MagicWS_ForCMS.MagicWS_ForCMS();
                    magicWSObj.GetAgentNumberSearchByLicense(agentLicenseNumber, out resMagicObj.p_result, out resMagicObj.p_agent_number);
                    if (resMagicObj.p_result.Trim() == "found")
                    {
                        // ตรวจสอบสถานะตัวแทน
                        ApplinXGetAgentDetailResult resAgentDetailObj = new ApplinXGetAgentDetailResult();
                        resAgentDetailObj = this.GetAgentDetail(resMagicObj.p_agent_number.Trim());
                        if (resAgentDetailObj.fld_result.Trim().ToLower() == "found")
                        {
                            // เช็คสถานะตัวแทน
                            if (resAgentDetailObj.fld_agent_status_code.Trim() == "A")
                            {
                                obj.Result = "completed";
                                obj.AgentNumber = resAgentDetailObj.fld_agent_no.Trim();
                                obj.Name = resAgentDetailObj.fld_agent_title.Trim() + " " + resAgentDetailObj.fld_agent_name.Trim() + " " + resAgentDetailObj.fld_agent_surname.Trim();
                                obj.Gender = resAgentDetailObj.fld_agent_gender.Trim();
                                obj.DoB = resAgentDetailObj.fld_agent_dob.Trim();
                                obj.Phone = resAgentDetailObj.fld_agent_phone.Trim();
                                obj.LicenseNumber = resAgentDetailObj.fld_agent_license_number.Trim();
                                obj.Department = resAgentDetailObj.fld_agent_department.Trim();
                                obj.Position = resAgentDetailObj.fld_agent_position.Trim();
                                obj.AgentPictureURL = resAgentDetailObj.AgentPictureURL.Trim();
                            }
                            else
                            {
                                obj.Result = "notcomplete_ตัวแทนคนนี้ได้พ้นสภาพจากการเป็นตัวแทนกับบริษัทฯ แล้ว";
                            }
                        }
                        else
                        {
                            obj.Result = resAgentDetailObj.fld_result.Trim().Replace("notfound_", "notcomplete_");
                        }
                    }
                    else
                    {
                        obj.Result = resMagicObj.p_result.Trim().Replace("notfound","notcomplete_ไม่พบตัวแทนที่มีเลขที่ใบอนุญาติตัวแทนตามที่ท่านระบุ");
                    }
                }
                else
                {
                    this.partnerName = pacobj.PartnerName;
                    // LogRequest: Insert Log Request
                    logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + agentLicenseNumber, this.refnum);

                    obj.Result = pacobj.Result.Trim().Replace("notpass_", "notcomplete_");
                }
            }

            // LogResponse: Insert Log Response
            logobj.AddWSLog(this.partnerName, this.ipaddress, "Response", this.webserviceName, methodName, obj.Result + "|" + obj.AgentNumber + "|" + obj.Name + "|" + obj.LicenseNumber, this.refnum);

            return obj;
        }
        catch (Exception ex)
        {
            obj.Result = "notcomplete_" + ex.Message.ToString();

            // LogResponse: Insert Log Response
            logobj.AddWSLog(this.partnerName, this.ipaddress, "Response", this.webserviceName, methodName, obj.Result, this.refnum);

            return obj;
        }
    }

    [WebMethod(Description = "Method ใช้สำหรับแสดงรายละเอียดกรมธรรม์ที่พร้อมสำหรับการชำระเงินค่าเบี้ยประกันภัยต่ออายุ")]
    public GetPolicyDetailForPayment_Result GetPolicyDetailForPayment(string partnerUsername, string partnerPassword, string policyNumber)
    {
        GetPolicyDetailForPayment_Result obj = new GetPolicyDetailForPayment_Result();
        RefRunningTBBLL runningobj = new RefRunningTBBLL();
        this.refnum = runningobj.AddRefRunningTBAndReturn();
        WSLogBLL logobj = new WSLogBLL();
        string methodName = "GetPolicyDetailForPayment";

        try
        {
            if (partnerUsername == "" || partnerPassword == "" || policyNumber == "")
            {
                this.partnerName = partnerUsername;
                // LogRequest: Insert Log Request
                logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + policyNumber, this.refnum);

                obj.Result = "notcomplete_กรุณาระบุข้อมูลให้ครบถ้วน";
            }
            else if (policyNumber.Trim().Substring(0, 2) == "PA")
            {
                this.partnerName = partnerUsername;
                // LogRequest: Insert Log Request
                logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + policyNumber, this.refnum);

                obj.Result = "notcomplete_กรมธรรม์นี้ไม่รับชำระค่าเบี้ยประกันต่ออายุออนไลน์";
            }
            else if (policyNumber.Length != 10 || !MTL.Utils.ThisWeb.CheckIsNumeric(policyNumber))
            {
                this.partnerName = partnerUsername;
                // LogRequest: Insert Log Request
                logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + policyNumber, this.refnum);

                obj.Result = "notcomplete_กรุณาระบุเลขกรมธรรม์ให้ถูกต้อง";
            }
            else
            {
                // พิสูจน์ตัวตนของพันธมิตรก่อนที่จะให้ใช้งานจริง
                NETWS_ForPartnerAuthenticationChecking.CheckPartnerAuthentication_Result pacobj = CheckPartnerAuthenticationReturnDetail(partnerUsername, partnerPassword, this.ipaddress);
                if (pacobj.Result.Trim().ToLower() == "passed")
                {
                    this.partnerName = pacobj.PartnerName;
                    // LogRequest: Insert Log Request
                    logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + policyNumber, this.refnum);

                    // ดึงข้อมูลรายละเอียดของกรมธรรม์กับ ApplinX WS_Admin
                    ApplinXGetShortPolicyDetail2Result resAdminObj = new ApplinXGetShortPolicyDetail2Result();
                    MTL.WS_Admin.WS_Admin_ForMTLmPOS.WS_Admin_ForMPosService adminWSObj = new MTL.WS_Admin.WS_Admin_ForMTLmPOS.WS_Admin_ForMPosService();
                    resAdminObj.fld_result = adminWSObj.GetShortPolicyDetail2(this.admin_username, this.admin_password, policyNumber, out resAdminObj.fld_sessionID, out resAdminObj.fld_service_agent, out resAdminObj.fld_client_name, out resAdminObj.fld_face_amount, out resAdminObj.fld_paid_mode, out resAdminObj.fld_plan_code, out resAdminObj.fld_plan_name, out resAdminObj.fld_total_premium, out resAdminObj.fld_policy_status, out resAdminObj.fld_issue_date, out resAdminObj.fld_warning_message);
                    if (resAdminObj.fld_result.Trim().ToLower() == "found")
                    {
                        //// ตรวจสอบว่าสถานะกรมธรรม์เป็น 1/B/7/9 หรือไม่ ถ้าไม่เป็นจะไม่ยอมให้ชำระต่ออายุออนไลน์
                        //if (resAdminObj.fld_policy_status.Trim().ToUpper() != "1" && resAdminObj.fld_policy_status.Trim().ToUpper() != "B" && resAdminObj.fld_policy_status.Trim().ToUpper() != "7" && resAdminObj.fld_policy_status.Trim().ToUpper() != "9")
                        // 20150115 พี่ไร, พี่มด ให้รองรับเฉพาะสถานะกรมธรรม์เป็น 1 เท่านั้น
                        // ตรวจสอบว่าสถานะกรมธรรม์เป็น 1 หรือไม่ ถ้าไม่เป็นจะไม่ยอมให้ชำระต่ออายุออนไลน์
                        if (resAdminObj.fld_policy_status.Trim().ToUpper() != "1")
                        {
                            obj.Result = "notcomplete_กรมธรรม์นี้ไม่รับชำระค่าเบี้ยประกันต่ออายุออนไลน์";
                        }
                        else
                        {
                            // ตรวจสอบว่าเป็นกรมธรรม์ที่สามารถรับชำระด้วยวิธีการใดได้บ้าง
                            if (resAdminObj.fld_warning_message.Contains("อนุโลม"))
                            {
                                obj.PaymentTypeToPay = "CCP|CDC";
                            }
                            else if (resAdminObj.fld_warning_message.Contains("ไม่รับบัตรเครดิต"))
                            {
                                obj.PaymentTypeToPay = "CCP";
                            }
                            else
                            {
                                obj.PaymentTypeToPay = "CCP|CDC";
                            }

                            // ตรวจสอบว่าเป็นกรมธรรม์ประเภท PA หรือไม่ (เลขกรมธรรม์ขึ้นต้นด้วย 8) ถ้าใช่จะต้องไปเอาค่าเบี้ยประกันรวมจากหน้าจอใบเสร็จ
                            if (policyNumber.Trim().Substring(0, 1) == "8")
                            {
                                MTL.WS_Admin.WS_Admin_ForMTLmPOS.CPINQ03_ReceiptDetailList[] receiptList;
                                string receiptResult = adminWSObj.GetReceiptDetailList(this.admin_username, this.admin_password, policyNumber, "", "", out resAdminObj.fld_sessionID, out receiptList);
                                if (receiptResult.Trim().ToLower() == "completed")
                                {
                                    var lastListResult = receiptList.Last();

                                    obj.TotalPremium = lastListResult.Amount.Trim();
                                }
                                else
                                {
                                    obj.TotalPremium = resAdminObj.fld_total_premium.Trim();
                                }
                            }
                            else
                            {
                                obj.TotalPremium = resAdminObj.fld_total_premium.Trim();
                            }

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
                            obj.WarningMessage = resAdminObj.fld_warning_message.Trim();
                        }
                    }
                    else
                    {
                        obj.Result = resAdminObj.fld_result.Trim().Replace("notfound_", "notcomplete_");
                    }
                }
                else
                {
                    this.partnerName = pacobj.PartnerName;
                    // LogRequest: Insert Log Request
                    logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + policyNumber, this.refnum);

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
            logobj.AddWSLog(this.partnerName, this.ipaddress, "Response", this.webserviceName, methodName, obj.Result, this.refnum);

            return obj;
        }
    }

    [WebMethod(Description = "Method ใช้สำหรับส่ง SMS")]
    public SendSms_Result SendSms(string partnerUsername, string partnerPassword, string senderAccount, string receiverMobileNumber, string message)
    {
        SendSms_Result obj = new SendSms_Result();
        RefRunningTBBLL runningobj = new RefRunningTBBLL();
        this.refnum = runningobj.AddRefRunningTBAndReturn();
        WSLogBLL logobj = new WSLogBLL();
        string methodName = "SendSms";

        try
        {
            if (partnerUsername == "" || partnerPassword == "" || senderAccount == "" || receiverMobileNumber == "" || message == "")
            {
                this.partnerName = partnerUsername;
                // LogRequest: Insert Log Request
                logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + senderAccount + "|" + receiverMobileNumber + "|" + message, this.refnum);

                obj.Result = "notcomplete_กรุณาระบุข้อมูลให้ครบถ้วน";
            }
            else if (receiverMobileNumber.Length != 10 || !MTL.Utils.ThisWeb.CheckIsNumeric(receiverMobileNumber))
            {
                this.partnerName = partnerUsername;
                // LogRequest: Insert Log Request
                logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + senderAccount + "|" + receiverMobileNumber + "|" + message, this.refnum);

                obj.Result = "notcomplete_กรุณาระบุเบอร์โทรศัพท์ให้ถูกต้อง";
            }
            else
            {
                // พิสูจน์ตัวตนของพันธมิตรก่อนที่จะให้ใช้งานจริง
                NETWS_ForPartnerAuthenticationChecking.CheckPartnerAuthentication_Result pacObj = CheckPartnerAuthenticationReturnDetail(partnerUsername, partnerPassword, this.ipaddress);
                if (pacObj.Result.Trim().ToLower() == "passed")
                {
                    this.partnerName = pacObj.PartnerName;
                    // LogRequest: Insert Log Request
                    logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + senderAccount + "|" + receiverMobileNumber + "|" + message, this.refnum);

                    string smsSenderUser = senderAccount;
                    string smsSenderPassword = "";
                    switch (senderAccount.Trim())
                    {
                        case "postsmc@mtl":
                            smsSenderUser = "postsmc@mtl";
                            smsSenderPassword = "password";
                            break;
                        case "it@mtl":
                            smsSenderUser = "it@mtl";
                            smsSenderPassword = "mtl01";
                            break;
                        case "its@mtl":
                            smsSenderUser = "its@mtl";
                            smsSenderPassword = "mtl01";
                            break;
                        case "postit@mtl":
                            smsSenderUser = "postit@mtl";
                            smsSenderPassword = "izc7m6ic";
                            break;
                        case "postits@mtl":
                            smsSenderUser = "postits@mtl";
                            smsSenderPassword = "mtl2011";
                            break;
                        case "cashout@mtl":
                            smsSenderUser = "cashout@mtl";
                            smsSenderPassword = "cashout";
                            break;
                        default:
                            smsSenderUser = "postits@mtl";
                            smsSenderPassword = "mtl2011";
                            break;
                    }
                    MTL.NETWS_ForSendSMS.ForSendSMS wsobj = new MTL.NETWS_ForSendSMS.ForSendSMS();
                    MTL.NETWS_ForSendSMS.SMSSendNow_Result sendResult = wsobj.SendSMSNow(smsSenderUser, smsSenderPassword, receiverMobileNumber, message);

                    if (sendResult.Result.Trim() == "sent")
                    {
                        obj.Result = "completed";
                        obj.NetInnovaMessageId = sendResult.MessageId.Trim();
                        obj.NetInnovaTaskId = sendResult.TaskId.Trim();
                    }
                    else
                    {
                        obj.Result = "notcomplete_ไม่สามารถส่ง SMS ให้ได้";
                        obj.NetInnovaMessageId = sendResult.MessageId.Trim();
                        obj.NetInnovaTaskId = sendResult.TaskId.Trim();
                    }
                }
                else
                {
                    this.partnerName = pacObj.PartnerName;
                    // LogRequest: Insert Log Request
                    logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + senderAccount + "|" + receiverMobileNumber + "|" + message, this.refnum);

                    obj.Result = pacObj.Result.Trim();
                }
            }

            // LogResponse: Insert Log Response
            logobj.AddWSLog(this.partnerName, this.ipaddress, "Response", this.webserviceName, methodName, obj.Result + "|" + obj.NetInnovaMessageId + "|" + obj.NetInnovaTaskId, this.refnum);

            return obj;
        }
        catch (Exception ex)
        {
            obj.Result = "notcomplete_" + ex.Message.Trim();

            // LogResponse: Insert Log Response
            logobj.AddWSLog(this.partnerName, this.ipaddress, "Response", this.webserviceName, methodName, obj.Result, this.refnum);

            return obj;
        }
    }

    [WebMethod(Description = "Method ใช้สำหรับส่ง E-mail")]
    public SendEmail_Result SendEmail(string partnerUsername, string partnerPassword, string senderEmail, string receiverEmail, string subject, string message)
    {
        SendEmail_Result obj = new SendEmail_Result();
        RefRunningTBBLL runningobj = new RefRunningTBBLL();
        this.refnum = runningobj.AddRefRunningTBAndReturn();
        WSLogBLL logobj = new WSLogBLL();
        string methodName = "SendEmail";

        SendEmail sendEmailObj = new SendEmail();

        try
        {
            if (partnerUsername == "" || partnerPassword == "" || senderEmail == "" || receiverEmail == "" || subject == "" || message == "")
            {
                this.partnerName = partnerUsername;
                // LogRequest: Insert Log Request
                logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + senderEmail + "|" + receiverEmail + "|" + subject + "|" + message, this.refnum);

                obj.Result = "notcomplete_กรุณาระบุข้อมูลให้ครบถ้วน";
            }
            else if (!sendEmailObj.CheckIsValidEmail(senderEmail))
            {
                this.partnerName = partnerUsername;
                // LogRequest: Insert Log Request
                logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + senderEmail + "|" + receiverEmail + "|" + subject + "|" + message, this.refnum);

                obj.Result = "notcomplete_กรุณาระบุ e-mail address ของผู้ส่งให้ถูกต้อง";
            }
            else if (!sendEmailObj.CheckIsValidEmail(receiverEmail))
            {
                this.partnerName = partnerUsername;
                // LogRequest: Insert Log Request
                logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + senderEmail + "|" + receiverEmail + "|" + subject + "|" + message, this.refnum);

                obj.Result = "notcomplete_กรุณาระบุ e-mail address ของผู้รับให้ถูกต้อง";
            }
            else
            {
                // พิสูจน์ตัวตนของพันธมิตรก่อนที่จะให้ใช้งานจริง
                NETWS_ForPartnerAuthenticationChecking.CheckPartnerAuthentication_Result pacObj = CheckPartnerAuthenticationReturnDetail(partnerUsername, partnerPassword, this.ipaddress);
                if (pacObj.Result.Trim().ToLower() == "passed")
                {
                    this.partnerName = pacObj.PartnerName;
                    // LogRequest: Insert Log Request
                    logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + senderEmail + "|" + receiverEmail + "|" + subject + "|" + message, this.refnum);

                    sendEmailObj.EmailSender = senderEmail.Trim();
                    sendEmailObj.EmailRecipient = receiverEmail.Trim();
                    sendEmailObj.EmailRecipientBCC = "";
                    sendEmailObj.Subject = subject;
                    sendEmailObj.Content = message;

                    if (sendEmailObj.Send() == true)
                    {
                        obj.Result = "completed";
                    }
                    else
                    {
                        obj.Result = "notcomplete_ไม่สามารถส่ง E-mail ให้ได้";
                    }
                }
                else
                {
                    this.partnerName = pacObj.PartnerName;
                    // LogRequest: Insert Log Request
                    logobj.AddWSLog(this.partnerName, this.ipaddress, "Request", this.webserviceName, methodName, partnerUsername + "|" + senderEmail + "|" + receiverEmail + "|" + subject + "|" + message, this.refnum);

                    obj.Result = pacObj.Result.Trim();
                }
            }

            // LogResponse: Insert Log Response
            logobj.AddWSLog(this.partnerName, this.ipaddress, "Response", this.webserviceName, methodName, obj.Result, this.refnum);

            return obj;
        }
        catch (Exception ex)
        {
            obj.Result = "notcomplete_" + ex.Message.Trim();

            // LogResponse: Insert Log Response
            logobj.AddWSLog(this.partnerName, this.ipaddress, "Response", this.webserviceName, methodName, obj.Result, this.refnum);

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

    #region Method สำหรับแสดงรายละเอียดตัวแทน
    /// <summary>
    /// ใช้สำหรับแสดงรายละเอียดตัวแทน
    /// </summary>
    /// <param name="agentNumber">Agent Number</param>
    /// <returns>ผลลัพธ์ว่า completed พร้อมรายละเอียดอื่นๆ หรือ notcomplete_xxx</returns>
    private ApplinXGetAgentDetailResult GetAgentDetail(string agentNumber)
    {
        ApplinXGetAgentDetailResult resAdminObj = new ApplinXGetAgentDetailResult();
        MTL.WS_Admin.WS_Admin_ForMTLmPOS.WS_Admin_ForMPosService adminWSObj = new MTL.WS_Admin.WS_Admin_ForMTLmPOS.WS_Admin_ForMPosService();
        resAdminObj.fld_result = adminWSObj.getAgentDetail(this.admin_username, this.admin_password, agentNumber, out resAdminObj.fld_sessionID, out resAdminObj.fld_agent_addrdoc_line1, out resAdminObj.fld_agent_addrdoc_line2, out resAdminObj.fld_agent_addrdoc_phone, out resAdminObj.fld_agent_address_line1, out resAdminObj.fld_agent_address_line2, out resAdminObj.fld_agent_bank_account, out resAdminObj.fld_agent_client_number, out resAdminObj.fld_agent_department, out resAdminObj.fld_agent_dob, out resAdminObj.fld_agent_end_date, out resAdminObj.fld_agent_gender, out resAdminObj.fld_agent_license_expire_date, out resAdminObj.fld_agent_license_number, out resAdminObj.fld_agent_license_start_date, out resAdminObj.fld_agent_name, out resAdminObj.fld_agent_office, out resAdminObj.fld_agent_phone, out resAdminObj.fld_agent_pid, out resAdminObj.fld_agent_policy_out, out resAdminObj.fld_agent_position, out resAdminObj.fld_agent_remark_type, out resAdminObj.fld_agent_start_date, out resAdminObj.fld_agent_status_code, out resAdminObj.fld_agent_status_description, out resAdminObj.fld_agent_tax_number, out resAdminObj.fld_agent_type, out resAdminObj.fld_agent_under_to, out resAdminObj.fld_agent_surname, out resAdminObj.fld_agent_title, out resAdminObj.fld_agent_no);
        // TODO: ดึงรูปภาพตัวแทนจาก EDAS
        resAdminObj.AgentPictureURL = "";

        return resAdminObj;
    }
    #endregion

    #region Method สำหรับคำนวณ Rider
    private string CalculateRiderWP(string PlanCode, string PaymentMethod, string Age, string Gender, string Amount, string MainPremium)
    {
        //Call WS_SQM_CalculateService.CalculateWPRider(MainInsured (decimal), MainPremium (decimal), CustomerGender (char), CustomerAge (int), CustomerOccupationGroup (string), PaymentMethod (int), SelectedPlanCode (string), Channel (int)); 
        //ระบุ Channel เป็นรหัสช่องทางที่กำหนด เช่น 6, 32
        //ระบุ PaymentMethod เป็น 12 (รายปี และชำระครั้งเดียว)/06 (ราย 6 เดือน)/03 (ราย 3 เดือน)/01 (รายเดือน)
        //ระบุ CustomerGender เป็น F/M , 
        //ระบุ CustomerOccupationGroup เป็น 001

        string result = "";

        int vChannel = 32;
        int vPaymentMethod = PaymentMethod.Trim() == "OT" ? 12 : Convert.ToInt32(PaymentMethod);
        int vAge = Convert.ToInt32(Age);
        char vGender = Gender == "F" ? 'F' : 'M';
        decimal vMainInsured = Convert.ToDecimal(Amount);
        string vOccupationGroup = "001";

        //// คำนวณเบี้ยประกันของแบบประกันหลักก่อน เพื่อเอาไปใช้คำนวณสัญญาเพิ่มเติม WP
        //string mainPreium = this.CalculatePremium(PlanCode, PaymentMethod, Age, Gender, Amount);
        //decimal vMainPremium = Convert.ToDecimal(mainPreium);
        // หนึ่งบอกไม่ต้องคำนวณเบี้ยประกันของแบบประกันหลักก่อนก็ได้
        decimal vMainPremium = Convert.ToDecimal(MainPremium);

        try
        {
            MTL.NETWS_ForSmartProposal.WS_SQM_CalculateService wsobj = new MTL.NETWS_ForSmartProposal.WS_SQM_CalculateService();
            MTL.NETWS_ForSmartProposal.RiderValueElementMobile resp = new MTL.NETWS_ForSmartProposal.RiderValueElementMobile();
            resp = wsobj.CalculateWPRider(vMainInsured, vMainPremium, vGender, vAge, vOccupationGroup, vPaymentMethod, PlanCode, vChannel);
            result = resp.Rider_TotalPremium.Trim();
        }
        catch (Exception ex)
        {
            result = "notcomplete_" + ex.Message.ToString();
        }

        return result;
    }
    #endregion
}

