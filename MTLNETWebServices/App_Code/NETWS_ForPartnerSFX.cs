using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for NETWS_ForPartnerSFX
/// </summary>
[WebService(Namespace = "http://muangthai.co.th/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class NETWS_ForPartnerSFX : System.Web.Services.WebService {

    protected string admin_username = "apluser";
    protected string admin_password = "rtyhgf";
    protected string ipaddress = HttpContext.Current.Request.UserHostAddress.ToString();
    protected int refnum;

    public class CheckSmilePassword_Result
    {
        public string fld_result;
        public string fld_sessionID;
        public string fld_last_changed_password_channel;
        public string fld_last_changed_password_date;
        public string fld_last_changed_password_time;
        //Extension
        public string fld_customer_name;
        public string fld_customer_surname;
    }

    public class GetCustomerDetail_Result
    {
        public string fld_result;
        public string fld_sessionID;
        public string fld_customer_name;
        public string fld_customer_surname;
        public string fld_customer_dob;
        public string fld_customer_age;
        public string fld_customer_idcard;
        public string fld_smile_point;
        public string fld_card_type;
        public string fld_email;
        public string fld_address_line1;
        public string fld_address_line2;
        public string fld_address_line3;
        public string fld_mobile_phone_number;
        public string fld_home_phone_number;
        public string fld_office_phone_number;
        public string fld_client_isAgent;
        public string fld_client_isSmileClubMember;
    }

    public class GetAllSmileActivityList_Result
    {
        public string fld_result;
        public string fld_sessionID;
        public string fld_smile_branch_name;
        public SMCINQ06D_1_AllSmileActivityList[] fld_all_smile_activity_list;
    }
    public class SMCINQ06D_1_AllSmileActivityList
    {
        public string id;
        public string name;
        public string place;
        public string type;
        public string status;
        public string seat;
        public string seat_each;
        public string seat_each_unit;
        public string max_book_seat;
        public string point;
        public string activity_startdate;
        public string activity_enddate;
        public string book_startdate;
        public string book_enddate;
        public string officer_book_startdate;
        public string officer_book_enddate;
        public string book_by_type;
        public string remain_seat;
        public string type_desc;
        public string max_book_seat_pstptn;
    }

    public class SetBookSmileActivity_Result
    {
        public string fld_result;
        public string fld_sessionID;
        public string fld_smile_point_befor_booked;
        public string fld_smile_point_after_booked;
        public string fld_point;
        public string fld_booked_no;
    }

    public NETWS_ForPartnerSFX () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    protected GetCustomerDetail_Result GetCustomerDetail(string client_number)
    {
        GetCustomerDetail_Result obj = new GetCustomerDetail_Result();
        WS_Admin_ForSmartCard.WS_Admin_ForSmartCardService wsobj = new WS_Admin_ForSmartCard.WS_Admin_ForSmartCardService();
        obj.fld_result = wsobj.GetCustomerDetail(admin_username, admin_password, client_number, out obj.fld_sessionID, out obj.fld_customer_name, out obj.fld_customer_surname, out obj.fld_customer_dob, out obj.fld_customer_age, out obj.fld_customer_idcard, out obj.fld_smile_point, out obj.fld_card_type, out obj.fld_email, out obj.fld_address_line1, out obj.fld_address_line2, out obj.fld_address_line3, out obj.fld_mobile_phone_number, out obj.fld_home_phone_number, out obj.fld_office_phone_number, out obj.fld_client_isAgent, out obj.fld_client_isSmileClubMember); 
        return obj;
    }

    protected string[] GetSmileActivityListByBranch(string smile_branch_code)
    {
        GetAllSmileActivityList_Result obj = new GetAllSmileActivityList_Result();
        // 20111117 เปลี่ยน ApplinX Application Service เพื่อ 24 ชม.
        //WS_Admin_ForSmartCard.SMCINQ06D_1_AllSmileActivityList[] v_list = new WS_Admin_ForSmartCard.SMCINQ06D_1_AllSmileActivityList[0];
        //WS_Admin_ForSmartCard.WS_Admin_ForSmartCardService wsobj = new WS_Admin_ForSmartCard.WS_Admin_ForSmartCardService();
        WS_Admin_SmileService_ForSmartCard.SMCINQ06D_1_AllSmileActivityList[] v_list = new WS_Admin_SmileService_ForSmartCard.SMCINQ06D_1_AllSmileActivityList[0];
        WS_Admin_SmileService_ForSmartCard.WS_Admin_ForSmartCardService wsobj = new WS_Admin_SmileService_ForSmartCard.WS_Admin_ForSmartCardService();
        obj.fld_result = wsobj.GetAllSmileActivityList(admin_username, admin_password, smile_branch_code, out obj.fld_sessionID, out obj.fld_smile_branch_name, out v_list);

        int v_list_Length = v_list.Length;
        string[] xx = new string[v_list_Length];
        for (int i = 0; i < v_list_Length; i++)
        {
            xx[i] = v_list[i].id;
        }
        return xx;
    }

    [WebMethod (Description = "สำหรับตรวจสอบรหัสผ่าน")]
    public CheckSmilePassword_Result CheckSmilePassword(string fld_channel, string fld_partner_branch_code, string fld_partner_branch_name, string fld_client_number, string fld_smile_password, string fld_card_seq_number)
    {
        CheckSmilePassword_Result obj = new CheckSmilePassword_Result();
        RefRunningTBBLL runningobj = new RefRunningTBBLL();
        refnum = runningobj.AddRefRunningTBAndReturn();
        WSLogBLL logobj = new WSLogBLL();
        try
        {
            if (fld_channel == "" || fld_channel.ToUpper() != "SFX" || fld_client_number == "")
            {
                //LogRequest: Insert Log Request
                logobj.AddWSLog(fld_channel, ipaddress, "Request", "NETWS_ForPartner", "CheckSmilePassword", fld_channel + "|" + fld_partner_branch_code + "|" + fld_partner_branch_name + "|" + fld_client_number + "|" + fld_smile_password + "|" + fld_card_seq_number, refnum);

                obj.fld_result = "notpass_กรุณาระบุ Channel หรือ Client Number ให้ถูกต้อง";
                //LogResponse: Insert Log Response
                logobj.AddWSLog(fld_channel, ipaddress, "Response", "NETWS_ForPartner", "CheckSmilePassword", obj.fld_result + "|" + fld_channel + "|" + fld_partner_branch_code + "|" + fld_partner_branch_name + "|" + obj.fld_sessionID, refnum);

                return obj;
            }
            else
            {
                switch (fld_channel.ToUpper())
                {
                    case "SFX":
                        try
                        {
                            //LogRequest: Insert Log Request
                            logobj.AddWSLog(fld_channel, ipaddress, "Request", "NETWS_ForPartner", "CheckSmilePassword", fld_channel + "|" + fld_partner_branch_code + "|" + fld_partner_branch_name + "|" + fld_client_number + "|" + fld_smile_password + "|" + fld_card_seq_number, refnum);

                            // 20111117 เปลี่ยน ApplinX Application Service เพื่อ 24 ชม.
                            //WS_Admin_ForSmartCard.WS_Admin_ForSmartCardService wsobj = new WS_Admin_ForSmartCard.WS_Admin_ForSmartCardService();
                            WS_Admin_SmileService_ForSmartCard.WS_Admin_ForSmartCardService wsobj = new WS_Admin_SmileService_ForSmartCard.WS_Admin_ForSmartCardService();
                            obj.fld_result = wsobj.CheckSmilePassword(admin_username, admin_password, fld_client_number, fld_smile_password, fld_card_seq_number, out obj.fld_sessionID, out obj.fld_last_changed_password_channel, out obj.fld_last_changed_password_date, out obj.fld_last_changed_password_time);
                            
                            //เอาชื่อ-นามสกุลลูกค้าจาก Method GetCustomerDetail มาแสดงด้วย
                            GetCustomerDetail_Result obj2 = GetCustomerDetail(fld_client_number);
                            obj.fld_customer_name = obj2.fld_customer_name;
                            obj.fld_customer_surname = obj2.fld_customer_surname;
                            //LogResponse: Insert Log Response
                            logobj.AddWSLog(fld_channel, ipaddress, "Response", "NETWS_ForPartner", "CheckSmilePassword", obj.fld_result + "|" + fld_channel + "|" + fld_partner_branch_code + "|" + fld_partner_branch_name + "|" + obj.fld_sessionID, refnum);

                            return obj;
                        }
                        catch (Exception ex)
                        {
                            obj.fld_result = ex.ToString();
                            //LogResponse: Insert Log Response
                            logobj.AddWSLog(fld_channel, ipaddress, "Response", "NETWS_ForPartner", "CheckSmilePassword", obj.fld_result + "|" + fld_channel + "|" + fld_partner_branch_code + "|" + fld_partner_branch_name + "|" + obj.fld_sessionID, refnum);

                            return obj;
                        }
                        break;
                    default:
                        obj.fld_result = "notpass_ระบบของท่านไม่มีสิทธิ์ตรวจสอบรหัสผ่านของสมาชิกเมืองไทย Smile Club";
                        //LogResponse: Insert Log Response
                        logobj.AddWSLog(fld_channel, ipaddress, "Response", "NETWS_ForPartner", "CheckSmilePassword", obj.fld_result + "|" + fld_channel + "|" + fld_partner_branch_code + "|" + fld_partner_branch_name + "|" + obj.fld_sessionID, refnum);

                        return obj;
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            obj.fld_result = ex.ToString();
            //LogResponse: Insert Log Response
            logobj.AddWSLog(fld_channel, ipaddress, "Response", "NETWS_ForPartner", "CheckSmilePassword", obj.fld_result + "|" + fld_channel + "|" + fld_partner_branch_code + "|" + fld_partner_branch_name + "|" + obj.fld_sessionID, refnum);

            return obj;
        }
    }

    [WebMethod(Description = "สำหรับแสดงรายการกิจกรรม")]
    public GetAllSmileActivityList_Result GetAllSmileActivityList(string fld_channel, string fld_partner_branch_code, string fld_partner_branch_name, string fld_smile_branch_code)
    {
        GetAllSmileActivityList_Result obj = new GetAllSmileActivityList_Result();
        RefRunningTBBLL runningobj = new RefRunningTBBLL();
        refnum = runningobj.AddRefRunningTBAndReturn();
        WSLogBLL logobj = new WSLogBLL();
        try
        {
            if (fld_channel == "" || fld_channel.ToUpper() != "SFX")
            {
                //LogRequest: Insert Log Request
                logobj.AddWSLog(fld_channel, ipaddress, "Request", "NETWS_ForPartner", "GetAllSmileActivityList", fld_channel + "|" + fld_partner_branch_code + "|" + fld_partner_branch_name + "|" + fld_smile_branch_code, refnum);
                
                obj.fld_result = "notfound_กรุณาระบุ Channel ให้ถูกต้อง";
                //LogResponse: Insert Log Response
                logobj.AddWSLog(fld_channel, ipaddress, "Response", "NETWS_ForPartner", "GetAllSmileActivityList", obj.fld_result + "|" + fld_channel + "|" + fld_partner_branch_code + "|" + fld_partner_branch_name + "|" + obj.fld_sessionID, refnum);

                return obj;
            }
            else
            {
                switch (fld_channel.ToUpper())
                {
                    case "SFX":
                        if (fld_smile_branch_code.ToUpper() != "B0Z01")
                        {
                            //LogRequest: Insert Log Request
                            logobj.AddWSLog(fld_channel, ipaddress, "Request", "NETWS_ForPartner", "GetAllSmileActivityList", fld_channel + "|" + fld_partner_branch_code + "|" + fld_partner_branch_name + "|" + fld_smile_branch_code, refnum);

                            obj.fld_result = "notfound_ระบบของท่านไม่มีสิทธิ์ดูรายการกิจกรรม";
                            //LogResponse: Insert Log Response
                            logobj.AddWSLog(fld_channel, ipaddress, "Response", "NETWS_ForPartner", "GetAllSmileActivityList", obj.fld_result + "|" + fld_channel + "|" + fld_partner_branch_code + "|" + fld_partner_branch_name + "|" + obj.fld_sessionID, refnum);

                            return obj;
                        }
                        else
                        {
                            try
                            {
                                //LogRequest: Insert Log Request
                                logobj.AddWSLog(fld_channel, ipaddress, "Request", "NETWS_ForPartner", "GetAllSmileActivityList", fld_channel + "|" + fld_partner_branch_code + "|" + fld_partner_branch_name + "|" + fld_smile_branch_code, refnum);

                                // 20111117 เปลี่ยน ApplinX Application Service เพื่อ 24 ชม.
                                //WS_Admin_ForSmartCard.SMCINQ06D_1_AllSmileActivityList[] v_list = new WS_Admin_ForSmartCard.SMCINQ06D_1_AllSmileActivityList[0];
                                //WS_Admin_ForSmartCard.WS_Admin_ForSmartCardService wsobj = new WS_Admin_ForSmartCard.WS_Admin_ForSmartCardService();
                                WS_Admin_SmileService_ForSmartCard.SMCINQ06D_1_AllSmileActivityList[] v_list = new WS_Admin_SmileService_ForSmartCard.SMCINQ06D_1_AllSmileActivityList[0];
                                WS_Admin_SmileService_ForSmartCard.WS_Admin_ForSmartCardService wsobj = new WS_Admin_SmileService_ForSmartCard.WS_Admin_ForSmartCardService();
                                obj.fld_result = wsobj.GetAllSmileActivityList(admin_username, admin_password, fld_smile_branch_code, out obj.fld_sessionID, out obj.fld_smile_branch_name, out v_list);

                                int v_list_Length = v_list.Length;
                                SMCINQ06D_1_AllSmileActivityList[] listobj = new SMCINQ06D_1_AllSmileActivityList[v_list_Length];
                                for (int i = 0; i < v_list_Length; i++)
                                {
                                    listobj[i] = new SMCINQ06D_1_AllSmileActivityList();
                                    listobj[i].id = v_list[i].id.ToString();
                                    listobj[i].name = v_list[i].name.ToString();
                                    listobj[i].place = v_list[i].place.ToString();
                                    listobj[i].type = v_list[i].type.ToString();
                                    listobj[i].status = v_list[i].status.ToString();
                                    if (fld_smile_branch_code == "")
                                    {
                                    }
                                    else
                                    {
                                        listobj[i].seat = v_list[i].seat.ToString();
                                        listobj[i].seat_each = v_list[i].seat_each.ToString();
                                        listobj[i].seat_each_unit = v_list[i].seat_each_unit.ToString();
                                        listobj[i].remain_seat = v_list[i].remain_seat.ToString();
                                    }
                                    listobj[i].max_book_seat = v_list[i].max_book_seat.ToString();
                                    listobj[i].point = v_list[i].point.ToString();
                                    listobj[i].activity_startdate = v_list[i].activity_startdate.ToString();
                                    listobj[i].activity_enddate = v_list[i].activity_enddate.ToString();
                                    listobj[i].book_startdate = v_list[i].book_startdate.ToString();
                                    listobj[i].book_enddate = v_list[i].book_enddate.ToString();
                                    listobj[i].officer_book_startdate = v_list[i].officer_book_startdate.ToString();
                                    listobj[i].officer_book_enddate = v_list[i].officer_book_enddate.ToString();
                                    listobj[i].book_by_type = v_list[i].book_by_type.ToString();
                                    listobj[i].type_desc = v_list[i].type_desc.ToString();
                                    listobj[i].max_book_seat_pstptn = v_list[i].max_book_seat_pstptn.ToString();
                                }
                                obj.fld_all_smile_activity_list = listobj;
                                //LogResponse: Insert Log Response
                                logobj.AddWSLog(fld_channel, ipaddress, "Response", "NETWS_ForPartner", "GetAllSmileActivityList", obj.fld_result + "|" + fld_channel + "|" + fld_partner_branch_code + "|" + fld_partner_branch_name + "|" + obj.fld_sessionID, refnum);

                                return obj;
                            }
                            catch (Exception ex)
                            {
                                obj.fld_result = ex.ToString();
                                //LogResponse: Insert Log Response
                                logobj.AddWSLog(fld_channel, ipaddress, "Response", "NETWS_ForPartner", "GetAllSmileActivityList", obj.fld_result + "|" + fld_channel + "|" + fld_partner_branch_code + "|" + fld_partner_branch_name + "|" + obj.fld_sessionID, refnum);

                                return obj;
                            }
                        }
                        break;
                    default:
                        obj.fld_result = "notfound_ระบบของท่านไม่มีสิทธิ์ดูรายการกิจกรรม";
                        //LogResponse: Insert Log Response
                        logobj.AddWSLog(fld_channel, ipaddress, "Response", "NETWS_ForPartner", "GetAllSmileActivityList", obj.fld_result + "|" + fld_channel + "|" + fld_partner_branch_code + "|" + fld_partner_branch_name + "|" + obj.fld_sessionID, refnum);

                        return obj;
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            obj.fld_result = ex.ToString();
            //LogResponse: Insert Log Response
            logobj.AddWSLog(fld_channel, ipaddress, "Response", "NETWS_ForPartner", "GetAllSmileActivityList", obj.fld_result + "|" + fld_channel + "|" + fld_partner_branch_code + "|" + fld_partner_branch_name + "|" + obj.fld_sessionID, refnum);

            return obj;
        }
    }

    [WebMethod(Description = "สำหรับใช้คะแนนสะสม Smile Point แลกกิจกรรม")]
    public SetBookSmileActivity_Result SetBookSmileActivity(string fld_channel, string fld_partner_branch_code, string fld_partner_branch_name, string fld_client_number, string fld_activity_id, string fld_book_no, string fld_mobile_phone_number, string fld_home_phone_number, string fld_home_phone_number_ext, string fld_office_phone_number, string fld_office_phone_number_ext, string fld_smile_branch_code)
    {
        SetBookSmileActivity_Result obj = new SetBookSmileActivity_Result();
        RefRunningTBBLL runningobj = new RefRunningTBBLL();
        refnum = runningobj.AddRefRunningTBAndReturn();
        WSLogBLL logobj = new WSLogBLL();
        try
        {
            if (fld_channel == "" || fld_channel.ToUpper() != "SFX" || fld_client_number == "" || fld_activity_id == "" || fld_book_no == "" || fld_smile_branch_code == "")
            {
                //LogRequest: Insert Log Request
                logobj.AddWSLog(fld_channel, ipaddress, "Request", "NETWS_ForPartner", "SetBookSmileActivity", fld_channel + "|" + fld_partner_branch_code + "|" + fld_partner_branch_name + "|" + fld_client_number + "|" + fld_activity_id + "|" + fld_book_no + "|" + fld_mobile_phone_number + "|" + fld_home_phone_number + "|" + fld_home_phone_number_ext + "|" + fld_office_phone_number + "|" + fld_office_phone_number_ext + "|" + fld_smile_branch_code, refnum);

                obj.fld_result = "notcomplete_กรุณาระบุ Channel หรือ Client Number หรือ Activity ID หรือ Book No หรือ Smile Branch Code ให้ถูกต้อง";
                //LogResponse: Insert Log Response
                logobj.AddWSLog(fld_channel, ipaddress, "Response", "NETWS_ForPartner", "SetBookSmileActivity", obj.fld_result + "|" + fld_channel + "|" + fld_partner_branch_code + "|" + fld_partner_branch_name + "|" + obj.fld_sessionID, refnum);

                return obj;
            }
            else
            {
                switch (fld_channel.ToUpper())
                {
                    case "SFX":
                        string[] xx = GetSmileActivityListByBranch(fld_smile_branch_code);
                        int found = Array.BinarySearch(xx, fld_activity_id);

                        bool activityIsFound;
                        if (found <= -1)
                        {
                            activityIsFound = false;
                        }
                        else
                        {
                            activityIsFound = true;
                        }

                        //if (fld_activity_id != "4713" || fld_smile_branch_code.ToUpper() != "B0Z01")
                        if (!activityIsFound || fld_smile_branch_code.ToUpper() != "B0Z01")
                            {
                            //LogRequest: Insert Log Request
                                logobj.AddWSLog(fld_channel, ipaddress, "Request", "NETWS_ForPartner", "SetBookSmileActivity", fld_channel + "|" + fld_partner_branch_code + "|" + fld_partner_branch_name + "|" + fld_client_number + "|" + fld_activity_id + "|" + fld_book_no + "|" + fld_mobile_phone_number + "|" + fld_home_phone_number + "|" + fld_home_phone_number_ext + "|" + fld_office_phone_number + "|" + fld_office_phone_number_ext + "|" + fld_smile_branch_code, refnum);

                            obj.fld_result = "notcomplete_ระบบของท่านไม่มีสิทธิ์แลกคะแนนให้สมาชิกเมืองไทย Smile Club";
                            //LogResponse: Insert Log Response
                            logobj.AddWSLog(fld_channel, ipaddress, "Response", "NETWS_ForPartner", "SetBookSmileActivity", obj.fld_result + "|" + fld_channel + "|" + fld_partner_branch_code + "|" + fld_partner_branch_name + "|" + obj.fld_sessionID, refnum);

                            return obj;
                        }
                        else
                        {
                            try
                            {
                                //LogRequest: Insert Log Request
                                logobj.AddWSLog(fld_channel, ipaddress, "Request", "NETWS_ForPartner", "SetBookSmileActivity", fld_channel + "|" + fld_partner_branch_code + "|" + fld_partner_branch_name + "|" + fld_client_number + "|" + fld_activity_id + "|" + fld_book_no + "|" + fld_mobile_phone_number + "|" + fld_home_phone_number + "|" + fld_home_phone_number_ext + "|" + fld_office_phone_number + "|" + fld_office_phone_number_ext + "|" + fld_smile_branch_code, refnum);

                                // 20111117 เปลี่ยน ApplinX Application Service เพื่อ 24 ชม.
                                //WS_Admin_ForSmartCard.WS_Admin_ForSmartCardService wsobj = new WS_Admin_ForSmartCard.WS_Admin_ForSmartCardService();
                                WS_Admin_SmileService_ForSmartCard.WS_Admin_ForSmartCardService wsobj = new WS_Admin_SmileService_ForSmartCard.WS_Admin_ForSmartCardService();
                                obj.fld_result = wsobj.SetBookSmileActivity(admin_username, admin_password, fld_client_number, fld_activity_id, ref fld_book_no, fld_mobile_phone_number, fld_home_phone_number, fld_home_phone_number_ext, fld_office_phone_number, fld_office_phone_number_ext, fld_smile_branch_code, fld_partner_branch_code, fld_partner_branch_name, out obj.fld_sessionID, out obj.fld_smile_point_befor_booked, out obj.fld_smile_point_after_booked, out obj.fld_point);
                                obj.fld_booked_no = fld_book_no;
                                //LogResponse: Insert Log Response
                                logobj.AddWSLog(fld_channel, ipaddress, "Response", "NETWS_ForPartner", "SetBookSmileActivity", obj.fld_result + "|" + fld_channel + "|" + fld_partner_branch_code + "|" + fld_partner_branch_name + "|" + obj.fld_sessionID, refnum);

                                return obj;
                            }
                            catch (Exception ex)
                            {
                                obj.fld_result = ex.ToString();
                                //LogResponse: Insert Log Response
                                logobj.AddWSLog(fld_channel, ipaddress, "Response", "NETWS_ForPartner", "SetBookSmileActivity", obj.fld_result + "|" + fld_channel + "|" + fld_partner_branch_code + "|" + fld_partner_branch_name + "|" + obj.fld_sessionID, refnum);

                                return obj;
                            }
                        }
                        break;
                    default:
                        obj.fld_result = "notcomplete_ระบบของท่านไม่มีสิทธิ์แลกคะแนนให้สมาชิกเมืองไทย Smile Club";
                        //LogResponse: Insert Log Response
                        logobj.AddWSLog(fld_channel, ipaddress, "Response", "NETWS_ForPartner", "SetBookSmileActivity", obj.fld_result + "|" + fld_channel + "|" + fld_partner_branch_code + "|" + fld_partner_branch_name + "|" + obj.fld_sessionID, refnum);

                        return obj;
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            obj.fld_result = ex.ToString();
            //LogResponse: Insert Log Response
            logobj.AddWSLog(fld_channel, ipaddress, "Response", "NETWS_ForPartner", "SetBookSmileActivity", obj.fld_result + "|" + fld_channel + "|" + fld_partner_branch_code + "|" + fld_partner_branch_name + "|" + obj.fld_sessionID, refnum);

            return obj;
        }
    }
}

