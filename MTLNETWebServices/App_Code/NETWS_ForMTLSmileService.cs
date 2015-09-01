using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for NETWS_ForMTLSmileService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class NETWS_ForMTLSmileService : System.Web.Services.WebService {
    protected string admin_username = "apluser";
    protected string admin_password = "rtyhgf";
    protected string ipaddress = "";//HttpContext.Current.Request.UserHostAddress.ToString();
    protected int refnum;
    protected string webserviceName = "NETWS_ForMTLSmileService";
    protected string partnerName = "";
    public NETWS_ForMTLSmileService () {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
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

    protected class ApplinXGetPolicyDetail2Result
    {
        public string fld_result;
        public string fld_sessionID;
        public string fld_approve_date;
        public string fld_attained_date;
        public string fld_belong_to_agent;
        public string fld_billed_to_date;
        public string fld_client_address;
        public string fld_client_dob;
        public string fld_client_name;
        public string fld_coverage_period;
        public string fld_disability_premium;
        public string fld_dividend_method;
        public string fld_extra_paid_period;
        public string fld_extra_premium;
        public string fld_face_amount;
        public string fld_issue_date;
        public string fld_last_paid_by;
        public string fld_last_paid_date;
        public string fld_luanch_date;
        public string fld_maturity_date;
        public string fld_next_paid_age;
        public string fld_owner_agent;
        public string fld_paid_date;
        public string fld_paid_mode;
        public string fld_paid_period;
        public string fld_payor;
        public string fld_plan_code;
        public string fld_plan_name;
        public string fld_rate_age;
        public string fld_rider_premium;
        public string fld_sale_department;
        public string fld_service_agent;
        public string fld_total_premium;
        public string fld_tranche_billed_to;
        public string fld_tranche_paid;
        public string fld_year_billed_to;
        public string fld_year_paid;
        public string fld_f2_mode_01;
        public string fld_f2_mode_03;
        public string fld_f2_mode_06;
        public string fld_f2_mode_12;
        public string fld_f4_address1;
        public string fld_f4_address2;
        public string fld_f4_business_phone;
        public string fld_f4_client_number;
        public string fld_f4_face_amount;
        public string fld_f4_mobile_phone;
        public string fld_f4_policy_status;
        public string fld_f4_resident_phone;
        public string fld_f4_responsible_team;
        public string fld_f4_sum_insured;
        public string fld_f5_misc_susp_date;
        public string fld_f5_misc_susp_value;
        public string fld_f5_prem_susp_date;
        public string fld_f5_prem_susp_value;
        public string fld_f6_policy_list;
        public string option;
        public string policy_number;
        public string policy_status;
        public string client_name;
        public string client_surname;
        public string client_number;
        public string fld_f7_assurance_code;
        public string fld_f7_hazard_health;
        public string fld_f7_hazard_occupation;
        public string fld_f7_health_check_code;
        public string fld_f8_disability_extra_premium;
        public string fld_f8_disability_premium;
        public string fld_f8_disability_total_premium;
        public string fld_f8_life_extra_premium;
        public string fld_f8_life_premium;
        public string fld_f8_life_total_premium;
        public string fld_f8_sum_extra_premium;
        public string fld_f8_sum_premium;
        public string fld_f8_sum_total_premium;
        public string fld_f8_tranche;
        public string fld_f8_year;
        public string fld_f8_policy_rider_list;
        public string plan_name;
        public string premium;
        public string extra_premium;
        public string total_premium;
        public string fld_warning_message;
        public string fld_f4_paid_by;
        public string fld_f4_paid_by_text;
        public string fld_f4_paid_by_create_date;
        public string fld_f4_paid_by_cancel_date;
        public string fld_f4_paid_by_account_number;
        public string fld_f4_MDC_bank;
        public string fld_f4_MDC_create_date;
        public string fld_f4_MDC_cancel_date;
        public string fld_f4_MDC_account_number;
        public string fld_f7_preserve_code;
        public string fld_f7_reinsurance;
        public string fld_f7_fpo_at;
        public string fld_f7_message;
        public string fld_f7_f11_message;
        public string fld_last_paid_by_text;
        public string fld_policyIsTakaful;
        public string fld_policy_status_code;
        public string fld_policy_status_subcode;
        public string fld_smile_club;
        public string fld_topup_loan;


       
    }
    public class GetPolicyDetailForPayment_Result
    {
        public string Result;
        public string SessionID;
        public string PolicyNumber;
        public string PlanCode;
        public string PlanName;
        //----ชำระเบี้ยประกันภัย----
        public string TypePaid;
        public string IssueBilledDate;
        public string BilledToDate;
        public string ClientName;
        public string FaceAmount;
        public string PaidMode;
        public string TotalPremium;
        public string PaymentTypeToPay;
        public string PolicyStatus;
        //public string IssueDate;
        public string WarningMessage;
    }
    protected class ApplinXgetPolicyCashValueResult
    {
        public string fld_result;
        public string fld_errmsg;
        public string fld_sessionID;
        public string fld_client_name;
        public string fld_plan_name;
        public string fld_contract_start_date;
        public string fld_apl;
        public string fld_apl_interest;
        public string fld_apl_interest_2;
        public string fld_cash_value_present;
        public string fld_date;
        public string fld_dividend;
        public string fld_loan_interest;
        public string fld_loan_interest_2;
        public string fld_loan_value;
        public string fld_loan_value_net;
        public string fld_policy_number;
        public string fld_premium_outof_payment;
        public string fld_surrender_value_net;
        public string fld_year;

    }
    public class GetPolicyAPLForPayment_Result
    {
        public string Result;
        public string SessionID;
        public string PolicyNumber;
        public string PlanName;
        public string APLAmount;
        public string PaymentTypeToPay;
    }

    public class GetPolicyLoanForPayment_Result
    {
        public string Result;
        public string SessionID;
        public string PolicyNumber;
        public string PlanName;
        public string LoanAmount;
        public string PaymentTypeToPay;
    }
    public enum DateInterval
    {
        Day,
        DayOfYear,
        Hour,
        Minute,
        Month,
        Quarter,
        Second,
        Weekday,
        WeekOfYear,
        Year
    }
    protected class PremiumDetail_Result
    {
       
    }
    #region รายละเอียดกรมธรรม์
    [WebMethod(Description = "Method ใช้สำหรับแสดงรายละเอียดกรมธรรม์ที่พร้อมสำหรับการชำระเงินค่าเบี้ยประกันภัยต่ออายุ")]
    public GetPolicyDetailForPayment_Result GetPolicyDetailForPayment(string partnerUsername, string partnerPassword, string policyNumber)
    {
        GetPolicyDetailForPayment_Result obj = new GetPolicyDetailForPayment_Result();
        PremiumDetail_Result pmobj = new PremiumDetail_Result();
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
                    //ApplinXGetShortPolicyDetail2Result resAdminObj = new ApplinXGetShortPolicyDetail2Result();
                    //MTL.WS_Admin.WS_Admin_ForMTLmPOS.WS_Admin_ForMPosService adminWSObj = new MTL.WS_Admin.WS_Admin_ForMTLmPOS.WS_Admin_ForMPosService();
                    //resAdminObj.fld_result = adminWSObj.GetpolicyDetail(this.admin_username, this.admin_password, policyNumber, out resAdminObj.fld_sessionID, out resAdminObj.fld_service_agent, out resAdminObj.fld_client_name, out resAdminObj.fld_face_amount, out resAdminObj.fld_paid_mode, out resAdminObj.fld_plan_code, out resAdminObj.fld_plan_name, out resAdminObj.fld_total_premium, out resAdminObj.fld_policy_status, out resAdminObj.fld_issue_date, out resAdminObj.fld_warning_message);
                    ApplinXGetPolicyDetail2Result resAdminObj = new ApplinXGetPolicyDetail2Result();

                    MTL.WS_Admin.WS_Admin_SmileServices.WS_Admin_ForSmartCardService adminWSObj = new MTL.WS_Admin.WS_Admin_SmileServices.WS_Admin_ForSmartCardService();
                    MTL.WS_Admin.WS_Admin_SmileServices.CPINQ02C_F6_PolicyList[] fld_f6_policy_list;
                    MTL.WS_Admin.WS_Admin_SmileServices.CPINQ02_PolicyRiderList[] fld_f8_policy_rider_list;

                    resAdminObj.fld_result = adminWSObj.GetPolicyDetail2(this.admin_username, this.admin_password, policyNumber, out resAdminObj.fld_sessionID,
                   out resAdminObj.fld_approve_date, out resAdminObj.fld_attained_date, out resAdminObj.fld_belong_to_agent, out resAdminObj.fld_billed_to_date, out resAdminObj.fld_client_address, out resAdminObj.fld_client_dob, out resAdminObj.fld_client_name, out resAdminObj.fld_coverage_period, out resAdminObj.fld_disability_premium, out resAdminObj.fld_dividend_method,
                   out resAdminObj.fld_extra_paid_period, out resAdminObj.fld_extra_premium, out resAdminObj.fld_face_amount, out resAdminObj.fld_issue_date, out resAdminObj.fld_last_paid_by, out resAdminObj.fld_last_paid_date, out resAdminObj.fld_luanch_date, out resAdminObj.fld_maturity_date, out resAdminObj.fld_next_paid_age, out resAdminObj.fld_owner_agent,
                   out resAdminObj.fld_paid_date, out resAdminObj.fld_paid_mode, out resAdminObj.fld_paid_period, out resAdminObj.fld_payor, out resAdminObj.fld_plan_code, out resAdminObj.fld_plan_name, out resAdminObj.fld_rate_age, out resAdminObj.fld_rider_premium, out  resAdminObj.fld_sale_department, out resAdminObj.fld_service_agent,
                   out resAdminObj.fld_total_premium, out resAdminObj.fld_tranche_billed_to, out resAdminObj.fld_tranche_paid, out resAdminObj.fld_year_billed_to, out resAdminObj.fld_year_paid, out resAdminObj.fld_f2_mode_01, out resAdminObj.fld_f2_mode_03, out resAdminObj.fld_f2_mode_06, out resAdminObj.fld_f2_mode_12, out resAdminObj.fld_f4_address1,
                   out resAdminObj.fld_f4_address2, out resAdminObj.fld_f4_business_phone, out resAdminObj.fld_f4_client_number, out resAdminObj.fld_f4_face_amount, out resAdminObj.fld_f4_mobile_phone, out resAdminObj.fld_f4_policy_status, out resAdminObj.fld_f4_resident_phone, out resAdminObj.fld_f4_responsible_team, out resAdminObj.fld_f4_sum_insured, out resAdminObj.fld_f5_misc_susp_date,
                   out resAdminObj.fld_f5_misc_susp_value, out resAdminObj.fld_f5_prem_susp_date, out resAdminObj.fld_f5_prem_susp_value, out  fld_f6_policy_list, out resAdminObj.fld_f7_assurance_code, out resAdminObj.fld_f7_hazard_health, out resAdminObj.fld_f7_hazard_occupation, out resAdminObj.fld_f7_health_check_code, out resAdminObj.fld_f8_disability_extra_premium, out resAdminObj.fld_f8_disability_premium,
                   out resAdminObj.fld_f8_disability_total_premium, out resAdminObj.fld_f8_life_extra_premium, out resAdminObj.fld_f8_life_premium, out resAdminObj.fld_f8_life_total_premium, out resAdminObj.fld_f8_sum_extra_premium, out resAdminObj.fld_f8_sum_premium, out resAdminObj.fld_f8_sum_total_premium, out resAdminObj.fld_f8_tranche, out resAdminObj.fld_f8_year, out fld_f8_policy_rider_list,
                   out resAdminObj.fld_warning_message, out resAdminObj.fld_f4_paid_by, out resAdminObj.fld_f4_paid_by_text, out resAdminObj.fld_f4_paid_by_create_date, out resAdminObj.fld_f4_paid_by_cancel_date, out resAdminObj.fld_f4_paid_by_account_number, out resAdminObj.fld_f4_MDC_bank, out resAdminObj.fld_f4_MDC_create_date, out resAdminObj.fld_f4_MDC_cancel_date, out resAdminObj.fld_f4_MDC_account_number,
                   out resAdminObj.fld_f7_preserve_code, out resAdminObj.fld_f7_reinsurance, out resAdminObj.fld_f7_fpo_at, out resAdminObj.fld_f7_message, out resAdminObj.fld_f7_f11_message, out resAdminObj.fld_last_paid_by_text, out resAdminObj.fld_policyIsTakaful, out resAdminObj.fld_policy_status_code, out resAdminObj.fld_policy_status_subcode);
                    //, out resAdminObj.fld_smile_club, out resAdminObj.fld_topup_loan);

                    
                    if (resAdminObj.fld_result.Trim().ToLower() == "found")
                    {
                        //// ตรวจสอบว่าสถานะกรมธรรม์เป็น 1/B/7/9 หรือไม่ ถ้าไม่เป็นจะไม่ยอมให้ชำระต่ออายุออนไลน์
                        //if (resAdminObj.fld_policy_status.Trim().ToUpper() != "1" && resAdminObj.fld_policy_status.Trim().ToUpper() != "B" && resAdminObj.fld_policy_status.Trim().ToUpper() != "7" && resAdminObj.fld_policy_status.Trim().ToUpper() != "9")
                        // 20150115 พี่ไร, พี่มด ให้รองรับเฉพาะสถานะกรมธรรม์เป็น 1 เท่านั้น
                        // ตรวจสอบว่าสถานะกรมธรรม์เป็น 1 หรือไม่ ถ้าไม่เป็นจะไม่ยอมให้ชำระต่ออายุออนไลน์
                        // ตรวจสอบfld_billed_to_date (Next due date กำหนดชำระครั้งต่อไป) ต้องมีค่า 
                        // ตรวจสอบ ไม่รับกรมธรรม์ที่เป็นกองทุน IL  จะใช้กรมธรรม์ที่ขึ้นด้วย 9 ใน 10 หลัก(ของเดิม) และ เป็นกรมธรรม์ที่เป็น 8 หลัก จะไม่รับชำระทุกช่องทาง
                        if ((resAdminObj.fld_policy_status_code.Trim().ToUpper() != "1" || resAdminObj.fld_billed_to_date.Trim() == "" || (policyNumber.Trim().Substring(0, 1) == "9" && policyNumber.Trim().Length == 10) || (policyNumber.Trim().Length == 8)))
                        {
                                obj.Result = "notcomplete_กรมธรรม์นี้ไม่รับชำระค่าเบี้ยประกันต่ออายุออนไลน์";
                        }
                        else
                        { // กรณีค้างจ่ายตรวจสอบเงื่อนไขวันกำหนดชำระ ไม่เกิน 31 วัน  
                            DateTime nextDue = Convert.ToDateTime(resAdminObj.fld_billed_to_date);
                            DateTime nextDue_2 = Convert.ToDateTime(resAdminObj.fld_billed_to_date).AddDays(31);
                            DateTime paid_date = Convert.ToDateTime(resAdminObj.fld_paid_date);

                            //กรณีจ่ายก่อนล่วงหน้างวด รับเงื่อนไขnextduedate -วันที่จ่าย <= ตามงวด(1ปี ,รายเดือน ,3เดือน ,6เดือน)
                            long diff = DateDiff(DateInterval.Month, DateTime.Today, nextDue);
                            long period = DateDiff(DateInterval.Month, paid_date, nextDue);


                            int compareValue = nextDue.CompareTo(DateTime.Today);
                            if (compareValue < 0 )
                            {
                                obj.Result = "notcomplete_กรมธรรม์นี้ไม่รับชำระค่าเบี้ยประกันต่ออายุออนไลน์ เนื่องจากเกินกำหนดที่รับชำระ กรุณาชำระช่องทางอื่นๆ";

                            }
                            else if (diff > period)
                            {
                                obj.Result = "notcomplete_กรมธรรม์นี้ไม่รับชำระค่าเบี้ยประกันต่ออายุออนไลน์ เนื่องจากยังไม่ถึงกำหนดชำระ";
                            }
                            else 
                            {
                                obj.TypePaid = "ชำระเบี้ยประกันภัย";
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

                                // ตรวจสอบว่าเป็นกรมธรรม์ประเภท PA แบบรายเดือนหรือไม่ (เลขกรมธรรม์ขึ้นต้นด้วย 8) ถ้าใช่จะต้องไปเอาค่าเบี้ยประกันรวมจากหน้าจอใบเสร็จ
                                if (policyNumber.Trim().Substring(0, 1) == "8" )
                                {
                                    MTL.WS_Admin.WS_Admin_ForMTLmPOS.WS_Admin_ForMPosService adminWSObj_mPos = new MTL.WS_Admin.WS_Admin_ForMTLmPOS.WS_Admin_ForMPosService();

                                    MTL.WS_Admin.WS_Admin_ForMTLmPOS.CPINQ03_ReceiptDetailList[] receiptList;
                                    string receiptResult = adminWSObj_mPos.GetReceiptDetailList(this.admin_username, this.admin_password, policyNumber, "", "", out resAdminObj.fld_sessionID, out receiptList);
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
                                obj.PolicyNumber = policyNumber;
                                obj.PlanCode = resAdminObj.fld_plan_code.Trim();
                                obj.PlanName = resAdminObj.fld_plan_name.Trim();
                                obj.IssueBilledDate = resAdminObj.fld_year_billed_to.Trim() + "/" + resAdminObj.fld_tranche_billed_to.Trim();
                                obj.BilledToDate = resAdminObj.fld_billed_to_date.Trim();
                                obj.FaceAmount = resAdminObj.fld_face_amount.Trim();
                                obj.PaidMode = resAdminObj.fld_paid_mode.Trim();
                                obj.PolicyStatus = resAdminObj.fld_policy_status_code.Trim();


                                //obj.WarningMessage = resAdminObj.fld_warning_message.Trim();
                                //obj.SessionID = resAdminObj.fld_sessionID.Trim();
                                //obj.ClientName = resAdminObj.fld_client_name.Trim();
                                //obj.IssueDate = resAdminObj.fld_issue_date.Trim();
                                //obj.ServiceAgent = resAdminObj.fld_service_agent.Trim();

                            }
                            
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
    #endregion 
    #region APL
    [WebMethod(Description = "Method ใช้สำหรับใช้แสดงข้อมูลสิทธิ์ตามกรมธรรม์เงินกู้อัตโนมัติ(APL)พร้อมสำหรับการชำระเงินค่าเบี้ยประกันภัยต่ออายุ")]
    public GetPolicyAPLForPayment_Result GetPolicyAPLForPayment(string partnerUsername, string partnerPassword, string policyNumber)
    {
        GetPolicyAPLForPayment_Result obj = new GetPolicyAPLForPayment_Result();
        RefRunningTBBLL runningobj = new RefRunningTBBLL();
        this.refnum = runningobj.AddRefRunningTBAndReturn();
        WSLogBLL logobj = new WSLogBLL();
        string methodName = "GetPolicyAPLForPayment";

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

                obj.Result = "notcomplete_กรมธรรม์นี้ไม่มียอดเงินกู้อัตโนมัติ(APL)ให้ชำระ";
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
                    ApplinXgetPolicyCashValueResult resAdminObj = new ApplinXgetPolicyCashValueResult();
                    MTL.WS_Admin.WS_Admin_SmileServices.WS_Admin_ForSmartCardService adminWSObj = new MTL.WS_Admin.WS_Admin_SmileServices.WS_Admin_ForSmartCardService();
                    resAdminObj.fld_errmsg = adminWSObj.getPolicyCashValue(this.admin_username, this.admin_password, policyNumber, "", out  resAdminObj.fld_sessionID, out resAdminObj.fld_client_name, out resAdminObj.fld_plan_name, out resAdminObj.fld_contract_start_date, out resAdminObj.fld_apl, out resAdminObj.fld_apl_interest, out resAdminObj.fld_apl_interest_2, out resAdminObj.fld_cash_value_present, out resAdminObj.fld_date, out resAdminObj.fld_dividend, out resAdminObj.fld_loan_interest, out resAdminObj.fld_loan_interest_2, out resAdminObj.fld_loan_value, out resAdminObj.fld_loan_value_net, out resAdminObj.fld_policy_number, out resAdminObj.fld_premium_outof_payment, out resAdminObj.fld_surrender_value_net, out resAdminObj.fld_year);

                    if (resAdminObj.fld_errmsg.Trim().ToLower() == "หมายเลขกรมธรรม์ถูกต้อง")
                    {
                        if (resAdminObj.fld_apl_interest_2.Trim().ToLower() != ".00")
                        {
                            obj.Result = "completed";
                            obj.SessionID = resAdminObj.fld_sessionID.Trim();
                            obj.PolicyNumber = policyNumber;
                            obj.PlanName = resAdminObj.fld_plan_name.Trim();
                            obj.APLAmount = resAdminObj.fld_apl_interest_2.Trim();
                            obj.PaymentTypeToPay    ="CCP"; //รับบัตรเดบิตเท่านั้น
                            
                        }
                        else
                        {
                            obj.Result = "notcomplete_กรมธรรม์นี้ไม่มียอดเงินกู้อัตโนมัติ(APL)ให้ชำระ";

                        }
                    }
                    else
                    {
                        obj.Result = "notcomplete_กรุณาระบุเลขกรมธรรม์ให้ถูกต้อง";
                        
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
            logobj.AddWSLog(this.partnerName, this.ipaddress, "Response", this.webserviceName, methodName, obj.Result + "|"  + obj.PolicyNumber + "|" + "|" + obj.PlanName + "|" + obj.APLAmount + "|" + obj.PlanName + "|" + obj.PaymentTypeToPay + "|" + obj.SessionID, this.refnum);

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
    #endregion

    #region Loan
    [WebMethod(Description = "Method ใช้สำหรับใช้แสดงข้อมูลเงินกู้ตามสิทธิ์กรมธรรม์(Loan)พร้อมสำหรับการชำระเงินค่าเบี้ยประกันภัยต่ออายุ")]
    public GetPolicyLoanForPayment_Result GetPolicyLoanForPayment(string partnerUsername, string partnerPassword, string policyNumber)
    {
        GetPolicyLoanForPayment_Result obj = new GetPolicyLoanForPayment_Result();

        RefRunningTBBLL runningobj = new RefRunningTBBLL();
        this.refnum = runningobj.AddRefRunningTBAndReturn();
        WSLogBLL logobj = new WSLogBLL();
        string methodName = "GetPolicyLoanForPayment";

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

                obj.Result = "notcomplete_กรมธรรม์นี้ไม่มีเงินกู้ตามสิทธิ์(Loan)ที่ต้องชำระ";
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
                    ApplinXgetPolicyCashValueResult resAdminObj = new ApplinXgetPolicyCashValueResult();
                    MTL.WS_Admin.WS_Admin_SmileServices.WS_Admin_ForSmartCardService adminWSObj = new MTL.WS_Admin.WS_Admin_SmileServices.WS_Admin_ForSmartCardService();
                    resAdminObj.fld_errmsg = adminWSObj.getPolicyCashValue(this.admin_username, this.admin_password, policyNumber, "", out  resAdminObj.fld_sessionID, out resAdminObj.fld_client_name, out resAdminObj.fld_plan_name, out resAdminObj.fld_contract_start_date, out resAdminObj.fld_apl, out resAdminObj.fld_apl_interest, out resAdminObj.fld_apl_interest_2, out resAdminObj.fld_cash_value_present, out resAdminObj.fld_date, out resAdminObj.fld_dividend, out resAdminObj.fld_loan_interest, out resAdminObj.fld_loan_interest_2, out resAdminObj.fld_loan_value, out resAdminObj.fld_loan_value_net, out resAdminObj.fld_policy_number, out resAdminObj.fld_premium_outof_payment, out resAdminObj.fld_surrender_value_net, out resAdminObj.fld_year);

                    if (resAdminObj.fld_errmsg.Trim().ToLower() == "หมายเลขกรมธรรม์ถูกต้อง")
                    {
                        if (resAdminObj.fld_loan_interest_2.Trim().ToLower() != ".00")
                        {
                            obj.Result = "completed";
                            obj.SessionID = resAdminObj.fld_sessionID.Trim();
                            obj.PolicyNumber = policyNumber;
                            obj.PlanName = resAdminObj.fld_plan_name.Trim();
                            obj.LoanAmount = resAdminObj.fld_loan_interest_2.Trim();
                            obj.PaymentTypeToPay = "CCP"; //รับบัตรเดบิตเท่านั้น

                        }
                        else
                        {
                            obj.Result = "notcomplete_กรมธรรม์นี้ไม่มีเงินกู้ตามสิทธิ์(Loan)ที่ต้องชำระ";

                        }
                    }
                    else
                    {
                        obj.Result = "notcomplete_กรุณาระบุหมายเลขกรมธรรม์ให้ถูกต้อง";

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
            logobj.AddWSLog(this.partnerName, this.ipaddress, "Response", this.webserviceName, methodName, obj.Result + "|" + obj.PolicyNumber + "|" + "|" + obj.PlanName + "|" + obj.LoanAmount + "|" + obj.PlanName + "|" + obj.PaymentTypeToPay + "|" + obj.SessionID, this.refnum);

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
    #endregion

    public static long DateDiff(DateInterval intervalType, System.DateTime dateOne, System.DateTime dateTwo)
    {
        switch (intervalType)
        {
            case DateInterval.Day:
            case DateInterval.DayOfYear:
                System.TimeSpan spanForDays = dateTwo - dateOne;
                return (long)spanForDays.TotalDays;
            case DateInterval.Hour:
                System.TimeSpan spanForHours = dateTwo - dateOne;
                return (long)spanForHours.TotalHours;
            case DateInterval.Minute:
                System.TimeSpan spanForMinutes = dateTwo - dateOne;
                return (long)spanForMinutes.TotalMinutes;
            case DateInterval.Month:
                return ((dateTwo.Year - dateOne.Year) * 12) + (dateTwo.Month - dateOne.Month);
            case DateInterval.Quarter:
                long dateOneQuarter = (long)System.Math.Ceiling(dateOne.Month / 3.0);
                long dateTwoQuarter = (long)System.Math.Ceiling(dateTwo.Month / 3.0);
                return (4 * (dateTwo.Year - dateOne.Year)) + dateTwoQuarter - dateOneQuarter;
            case DateInterval.Second:
                System.TimeSpan spanForSeconds = dateTwo - dateOne;
                return (long)spanForSeconds.TotalSeconds;
            case DateInterval.Weekday:
                System.TimeSpan spanForWeekdays = dateTwo - dateOne;
                return (long)(spanForWeekdays.TotalDays / 7.0);
            case DateInterval.WeekOfYear:
                System.DateTime dateOneModified = dateOne;
                System.DateTime dateTwoModified = dateTwo;
                while (dateTwoModified.DayOfWeek != System.Globalization.DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek)
                {
                    dateTwoModified = dateTwoModified.AddDays(-1);
                }
                while (dateOneModified.DayOfWeek != System.Globalization.DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek)
                {
                    dateOneModified = dateOneModified.AddDays(-1);
                }
                System.TimeSpan spanForWeekOfYear = dateTwoModified - dateOneModified;
                return (long)(spanForWeekOfYear.TotalDays / 7.0);
            case DateInterval.Year:
                return dateTwo.Year - dateOne.Year;
            default:
                return 0;
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
}
