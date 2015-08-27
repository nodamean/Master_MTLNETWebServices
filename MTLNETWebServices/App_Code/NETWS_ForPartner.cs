using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for NETWS_ForPartner
/// </summary>
[WebService(Namespace = "http://muangthai.co.th/MTLNETWebServices/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class NETWS_ForPartner : System.Web.Services.WebService {

    protected string admin_username = "apluser";
    protected string admin_password = "rtyhgf";
    protected string ipaddress = "";//HttpContext.Current.Request.UserHostAddress.ToString();
    protected int refnum;

    public class CheckSmilePassword_Result
    {
        public string fld_result;
        public string fld_sessionID;
        public string fld_last_changed_password_channel;
        public string fld_last_changed_password_date;
        public string fld_last_changed_password_time;
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
        public string fld_expiry_point_round1;
        public string fld_expiry_date_round1;
        public string fld_expiry_point_round2;
        public string fld_expiry_date_round2;
        public string fld_mobile_phone_number_SMC;
    }

    public class SetBookSmileActivity_NEW_Result
    {
        public string fld_result;
        public string fld_sessionID;
        public string fld_smile_point_before_booked;
        public string fld_smile_point_after_booked;
        public string fld_point;
        public string fld_booked_no;
        public string fld_get_discount_amount;
        public string fld_get_discount_percent;
        public string fld_get_discount_from;
        public string fld_receive_code;
        public string fld_comment;
    }

    public NETWS_ForPartner () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    #region Methods สำหรับ Partner
    [WebMethod(Description = "สำหรับ Model 1 ใช้รับสิทธิ์ โดยไม่ใช้คะแนนสะสม Smile Point แลกรับสิทธิ์")]
    public SetBookSmileActivity_NEW_Result PartnerSetBookSmileActivity(string fld_partner_username, string fld_partner_password, string fld_client_number, string fld_activity_id, string fld_book_no, string fld_mobile_phone_number, string fld_home_phone_number, string fld_home_phone_number_ext, string fld_office_phone_number, string fld_office_phone_number_ext, string fld_smile_branch_code, string fld_request_branch_code, string fld_request_branch_name, string fld_additional_amount)
    {
        SetBookSmileActivity_NEW_Result obj = new SetBookSmileActivity_NEW_Result();
        RefRunningTBBLL runningobj = new RefRunningTBBLL();
        refnum = runningobj.AddRefRunningTBAndReturn();
        WSLogBLL logobj = new WSLogBLL();

        try
        {
            //ตรวจสอบค่าที่จำเป็นต้องส่งมาให้ครบถ้วนก่อน
            if (fld_partner_username == "" || fld_partner_password == "" || fld_client_number == "" || fld_activity_id == "" || fld_book_no == "" || fld_smile_branch_code == "")
            {
                //LogRequest: Insert Log Request
                logobj.AddWSLog(fld_partner_username, ipaddress, "Request", "NETWS_ForPartner", "PartnerSetBookSmileActivity", fld_partner_username + "|" + fld_partner_password + "|" + fld_client_number + "|" + fld_activity_id + "|" + fld_book_no + "|" + fld_mobile_phone_number + "|" + fld_home_phone_number + "|" + fld_home_phone_number_ext + "|" + fld_office_phone_number + "|" + fld_office_phone_number_ext + "|" + fld_smile_branch_code + "|" + fld_request_branch_code + "|" + fld_request_branch_name, refnum);

                obj.fld_result = "notcomplete_กรุณาระบุ Partner Username หรือ Partner Password หรือ Client Number หรือ Activity ID หรือ Book No หรือ Smile Branch Code ให้ถูกต้อง";

                //LogResponse: Insert Log Response
                logobj.AddWSLog(fld_partner_username, ipaddress, "Response", "NETWS_ForPartner", "PartnerSetBookSmileActivity", obj.fld_result + "|" + fld_partner_username + "|" + fld_client_number + "|" + obj.fld_sessionID, refnum);

                return obj;
            }
            else
            {
                //พิสูจน์ตัวตนของพันธมิตรก่อนที่จะให้ใช้งานจริง
                NETWS_ForPartnerAuthenticationChecking.CheckPartnerAuthentication_Result pacobj = CheckPartnerAuthenticationReturnDetail(fld_partner_username, fld_partner_password, ipaddress);
                if (pacobj.Result.Trim().ToLower() == "passed")
                {
                    try
                    {
                        //ใช้เพื่อกำหนดค่าสำหรับ Input parameters ที่เฉพาะเจาะจงให้กับแต่ละพันมิตร
                        switch (pacobj.PartnerName.Trim())
                        {
                            case "i-wiz":
                                fld_book_no = "1";
                                break;
                        }

                        //LogRequest: Insert Log Request
                        logobj.AddWSLog(pacobj.PartnerName, ipaddress, "Request", "NETWS_ForPartner", "PartnerSetBookSmileActivity", fld_partner_username + "|" + fld_partner_password + "|" + fld_client_number + "|" + fld_activity_id + "|" + fld_book_no + "|" + fld_mobile_phone_number + "|" + fld_home_phone_number + "|" + fld_home_phone_number_ext + "|" + fld_office_phone_number + "|" + fld_office_phone_number_ext + "|" + fld_smile_branch_code + "|" + fld_request_branch_code + "|" + fld_request_branch_name, refnum);

                        //SetBookSmileActivity_NEW_Result mtlwsobj = MTLSetBookSmileActivity_NEW(fld_client_number, fld_activity_id, fld_book_no, fld_mobile_phone_number, fld_home_phone_number, fld_home_phone_number_ext, fld_office_phone_number, fld_office_phone_number_ext, fld_smile_branch_code, fld_request_branch_code, fld_request_branch_name, fld_additional_amount);
                        //obj.fld_result = mtlwsobj.fld_result;
                        //obj.fld_sessionID = mtlwsobj.fld_sessionID;

                        //ใช้เพื่อกำหนดค่าสำหรับ Output ที่เฉพาะเจาะจงให้กับแต่ละพันธมิตร
                        switch (pacobj.PartnerName.Trim())
                        {
                            case "i-wiz":
                                //ตรวจสอบเงื่อนไขต่างๆ ตามความต้องการ
                                #region i-wiz เงื่อนไขการตรวจสอบจาก CRM สำหรับโมเดล 1
                                /**************************************
                                Step1 ลูกค้ากดหมายเลข USSD แล้วตามด้วย Client Number เช่น *XXXX*1201404626# แล้วกดโทรออก
                                Step2 ระบบ i-wiz & MTL ตรวจสอบตามเงื่อนไข
                                Step3 แจ้งตอบกลับลูกค้าผ่าน USSD
                                **************************************/
                                //กรณีกด Code ผิด [i-wiz ต้องเช็ค]
                                //ขออภัยค่ะ รหัสไม่ถูกต้อง กรุณาตรวจสอบและส่งใหม่อีกครั้งค่ะ (58)
                                /*
                                 * i-wiz ต้องเช็คให้
                                 */

                                //กรณีเบอร์มือถือไม่ตรงกับฐานข้อมูลเบอร์ใน SMC
                                //ขออภัยค่ะ เบอร์โทรศัพท์ของท่านไม่ได้รับสิทธิ์ กรุณาติดต่อเบอร์ 1766 กด 4 ค่ะ (70)
                                /*
                                 * .NET ต้องเช็คกับข้อมูลที่ได้จาก MTLGetCustomerDetail() ให้ (ข้อมูลเบอร์โทรมือถือ SMC ที่ต๋องเพิ่มใหม่)
                                 */

                                //กรณี Client Number ไม่มีในระบบ [ยกเลิก]
                                //ขออภัยค่ะ เลขที่ประจำตัวไม่ถูกต้อง กรุณาตรวจสอบและส่งใหม่อีกครั้งค่ะ (68)
                                /*
                                 * ยกเลิกแล้ว ไม่ต้องเช็ค แต่ถ้าต้องเช็ค .NET ต้องเช็คกับ fld_result จาก MTLGetCustomerDetail() ให้ (notfound)
                                 */

                                //กรณีเบอร์มือถือและ Client Number ไม่ match กันกับฐานข้อมูลใน SMC
                                //เบอร์มือถือและเลขประจำตัวของท่านไม่ตรงกับในระบบ กรุณาติดต่อ 1766 กด 4 ค่ะ (69)
                                /*
                                 * ??? เช็คยังไง ??? .NET ต้องเช็คกับข้อมูลที่ได้จาก MTLGetCustomerDetail() ให้
                                 */

                                //กรณีกดใช้ในช่วงเวลาที่ไม่อยู่ในระยะเวลากิจกรรม
                                //ขออภัยค่ะ คุณส่งรหัสในช่วงเวลานอกเหนือจากการได้รับสิทธิ์ ขอบคุณค่ะ (66)
                                /*
                                 * .NET ต้องเช็คกับ fld_result จาก MTLSetBookSmileActivity_NEW() ให้ (notcomplete_ไม่ได้อยู่ในระยะเวลาการแลกคะแนน)
                                 */

                                //กรณีกดใช้หลังจากสิ้นสุดระยะเวลาการจัดกิจกรรม [ยกเลิก]
                                //ขออภัยค่ะ สิทธิพิเศษนี้หมดเขตการรับสิทธิ์แล้ว ขอบคุณที่ให้ความสนใจค่ะ (69)
                                /*
                                 * ยกเลิกแล้ว ไม่ต้องเช็ค
                                 */

                                //กรณีสิทธิประโยชน์มีผู้ใช้สิทธิ์เต็มจำนวนแล้ว
                                //ขออภัยค่ะ มีผู้รับสิทธิ์เต็มจำนวนแล้ว ขอบคุณที่ให้ความสนใจค่ะ (61)
                                /*
                                 * .NET ต้องเช็คกับ fld_result จาก MTLSetBookSmileActivity_NEW() ให้ (notcomplete_จำนวนรางวัลไม่พอสำหรับการแลก)
                                 */

                                //กรณีได้รับสิทธิ์ครบตามเงื่อนไข แต่ส่งมาขอใหม่
                                //ขออภัยค่ะ คุณได้รับสิทธิ์ครบตามเงื่อนไขแล้ว ขอบคุณที่ให้ความสนใจค่ะ (68)
                                /*
                                 * .NET ต้องเช็คกับ fld_result จาก MTLSetBookSmileActivity_NEW() ให้ (notcomplete_แลกเกินจำนวน , notcomplete_แลกเกินจำนวนต่อวัน , notcomplete_แลกเกินจำนวนต่อสัปดาห์ , notcomplete_แลกเกินจำนวนต่อเดือน , notcomplete_แลกเกินจำนวนต่อปี , notcomplete_แลกเกินจำนวนต่อกิจกรรม)
                                 */

                                //กรณีระบบขัดข้อง เช่น ส่งแล้วไม่ได้ข้อความตอบกลับ [i-wiz ต้องเช็ค]
                                //ขออภัยค่ะ ระบบขัดข้องชั่วคราว กรุณาติดต่อ 1766 กด 4 ค่ะ (55)
                                /*
                                 * i-wiz ต้องเช็คให้
                                 */

                                //กรณีตรวจสอบสิทธิ์แล้วลูกค้าได้รับสิทธิ์
                                //โปรดแสดงข้อความและรหัส MTL xxxx ที่จุดบริการเพื่อรับสิทธิ์ค่ะ (61)
                                /*
                                 * .NET ต้องเช็คกับ fld_result จาก MTLSetBookSmileActivity_NEW() ให้ (completed)
                                 */
                                #endregion

                                GetCustomerDetail_Result customerwsobj = MTLGetCustomerDetail(fld_client_number);
                                if (customerwsobj.fld_result.Trim() == "notfound")
                                {
                                    //obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ เลขที่ประจำตัวของท่านไม่ได้รับสิทธิ์ กรุณาติดต่อ 1766 กด 4 ค่ะ";
                                    obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ เลขที่ประจำตัวของท่านไม่ได้รับสิทธิ์ กรุณาติดต่อ 1766 กด4ค่ะ";
                                }
                                else if (customerwsobj.fld_result.Trim() == "notfound_ข้อมูลยังรันไม่เสร็จ")
                                {
                                    obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ ระบบยังไม่สามารถให้บริการได้ในขณะนี้ กรุณาติดต่อ 1766 กด 4 ค่ะ";
                                }
                                else if (customerwsobj.fld_client_isSmileClubMember.Trim() != "Y" && customerwsobj.fld_client_isSmileClubMember.Trim() != "S")
                                {
                                    obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ เลขที่ประจำตัวของท่านไม่ได้รับสิทธิ์ กรุณาติดต่อ 1766 กด4ค่ะ";
                                }
                                else if (customerwsobj.fld_result.Trim() == "found" && (customerwsobj.fld_mobile_phone_number_SMC.Trim() != fld_mobile_phone_number.Trim()))
                                {
                                    //obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ เบอร์โทรศัพท์ของท่านไม่ได้รับสิทธิ์ กรุณาติดต่อเบอร์ 1766 กด 4 ค่ะ";
                                    obj.fld_result = "notcomplete_MSG_เบอร์มือถือไม่ตรงกับเลขที่ประจำตัวของท่านในระบบ กรุณาติดต่อ1766กด4ค่ะ";
                                }
                                else
                                {
                                    SetBookSmileActivity_NEW_Result mtlwsobj = MTLSetBookSmileActivity_NEW(fld_client_number, fld_activity_id, fld_book_no, fld_mobile_phone_number, fld_home_phone_number, fld_home_phone_number_ext, fld_office_phone_number, fld_office_phone_number_ext, fld_smile_branch_code, fld_request_branch_code, fld_request_branch_name, fld_additional_amount);
                                    obj.fld_result = mtlwsobj.fld_result;
                                    obj.fld_sessionID = mtlwsobj.fld_sessionID;

                                    switch (mtlwsobj.fld_result.Trim())
                                    {
                                        case "notcomplete_ไม่มีข้อมูลสาขานี้":
                                            obj.fld_result = "";
                                            break;
                                        case "notcomplete_กรุณาระบุรหัสกิจกรรม":
                                            obj.fld_result = "";
                                            break;
                                        case "notcomplete_รหัสกิจกรรมไม่ถูกต้อง":
                                            obj.fld_result = "";
                                            break;
                                        case "notcomplete_ไม่พบกิจกรรมในสาขานี้":
                                            obj.fld_result = "";
                                            break;
                                        case "notcomplete_จำนวนรางวัลไม่พอสำหรับการแลก":
                                            obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ มีผู้รับสิทธิ์เต็มจำนวนแล้ว ขอบคุณที่ให้ความสนใจค่ะ";
                                            break;
                                        case "notpass_มี SUB กิจกรรม":
                                            obj.fld_result = "";
                                            break;
                                        case "notcomplete_ไม่ได้เป็นสมาชิกSmileClub":
                                            //obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ เลขที่ประจำตัวของท่านไม่ได้รับสิทธิ์ กรุณาติดต่อ 1766 กด 4 ค่ะ";
                                            obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ เลขที่ประจำตัวของท่านไม่ได้รับสิทธิ์ กรุณาติดต่อ 1766 กด4ค่ะ";
                                            break;
                                        case "notcomplete_ยังไม่เปิดสิทธิในการแลก":
                                            obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ คุณส่งรหัสในช่วงเวลานอกเหนือจากการได้รับสิทธิ์ ขอบคุณค่ะ";
                                            break;
                                        case "notcomplete_ไม่มีข้อมูลลูกค้า":
                                            //obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ เลขที่ประจำตัวของท่านไม่ได้รับสิทธิ์ กรุณาติดต่อ 1766 กด 4 ค่ะ";
                                            obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ เลขที่ประจำตัวของท่านไม่ได้รับสิทธิ์ กรุณาติดต่อ 1766 กด4ค่ะ";
                                            break;
                                        case "notcomplete_ไม่มีข้อมูลคะแนนสะสม":
                                            obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ เลขที่ประจำตัวของท่านไม่ได้รับสิทธิ์ กรุณาติดต่อ 1766 กด4ค่ะ";
                                            break;
                                        case "notpass_กรุณาระบุจำนวนเงิน":
                                            obj.fld_result = "";
                                            break;
                                        case "notcomplete_กรุณาระบุผู้มาแลกรับ":
                                            obj.fld_result = "";
                                            break;
                                        case "notcomplete_ไม่ได้อยู่ในระยะเวลาการแลกคะแนน":
                                            obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ คุณส่งรหัสในช่วงเวลานอกเหนือจากการได้รับสิทธิ์ ขอบคุณค่ะ";
                                            break;
                                        case "notcomplete_จำนวนรางวัลเกินกว่าที่กำหนด":
                                            obj.fld_result = "";
                                            break;
                                        case "notcomplete_กรุณาใส่จำนวนรางวัล":
                                            obj.fld_result = "";
                                            break;
                                        case "notcomplete_พนักงานหรือตัวแทนไม่สามารถแลกได้ในขณะนี้":
                                            //obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ พนักงานหรือตัวแทนไม่สามารถรับสิทธิ์ได้ในขณะนี้ค่ะ";
                                            obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ พนักงานหรือตัวแทนไม่สามารถรับสิทธิ์นี้ได้ค่ะ";
                                            break;
                                        case "notcomplete_จำนวนคะแนนไม่เพียงพอ":
                                            obj.fld_result = "";
                                            break;
                                        case "notcomplete_แลกเกินจำนวน":
                                            obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ คุณได้รับสิทธิ์ครบตามเงื่อนไขแล้ว ขอบคุณที่ให้ความสนใจค่ะ";
                                            break;
                                        case "notcomplete_แลกเกินจำนวนต่อวัน":
                                            obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ คุณได้รับสิทธิ์ครบตามเงื่อนไขแล้ว ขอบคุณที่ให้ความสนใจค่ะ";
                                            break;
                                        case "notcomplete_แลกเกินจำนวนต่อสัปดาห์":
                                            obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ คุณได้รับสิทธิ์ครบตามเงื่อนไขแล้ว ขอบคุณที่ให้ความสนใจค่ะ";
                                            break;
                                        case "notcomplete_แลกเกินจำนวนต่อเดือน":
                                            obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ คุณได้รับสิทธิ์ครบตามเงื่อนไขแล้ว ขอบคุณที่ให้ความสนใจค่ะ";
                                            break;
                                        case "notcomplete_แลกเกินจำนวนต่อปี":
                                            obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ คุณได้รับสิทธิ์ครบตามเงื่อนไขแล้ว ขอบคุณที่ให้ความสนใจค่ะ";
                                            break;
                                        case "notcomplete_แลกเกินจำนวนต่อกิจกรรม":
                                            obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ คุณได้รับสิทธิ์ครบตามเงื่อนไขแล้ว ขอบคุณที่ให้ความสนใจค่ะ";
                                            break;
                                        case "notpass_จำนวน POINT ไม่พอสำหรับการแลก":
                                            obj.fld_result = "";
                                            break;
                                        case "notpass_ยอดเงินไม่พอสำหรับการแลก":
                                            obj.fld_result = "";
                                            break;
                                        case "notpass_จำนวนรางวัลไม่พอสำหรับการแลก":
                                            obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ มีผู้รับสิทธิ์เต็มจำนวนแล้ว ขอบคุณที่ให้ความสนใจค่ะ";
                                            break;
                                        case "completed":
                                            obj.fld_smile_point_before_booked = mtlwsobj.fld_smile_point_before_booked;
                                            obj.fld_smile_point_after_booked = mtlwsobj.fld_smile_point_after_booked;
                                            obj.fld_point = mtlwsobj.fld_point;
                                            obj.fld_booked_no = mtlwsobj.fld_booked_no;
                                            obj.fld_get_discount_amount = mtlwsobj.fld_get_discount_amount;
                                            obj.fld_get_discount_percent = mtlwsobj.fld_get_discount_percent;
                                            obj.fld_get_discount_from = mtlwsobj.fld_get_discount_from;
                                            obj.fld_receive_code = mtlwsobj.fld_receive_code;

                                            //obj.fld_result = "completed_MSG_โปรดแสดงข้อความและรหัส MTL " + obj.fld_receive_code.Trim() + " ที่จุดบริการเพื่อรับสิทธิ์ค่ะ";
                                            //obj.fld_result = "completed_MSG_โปรดแสดงรหัส MTL " + obj.fld_receive_code.Trim() + " ที่จุดบริการเพื่อรับสิทธิ์ค่ะ " + GetCurrentDateTime();
                                            //20130909: นิวแจ้งแก้ไข 09/09/2013
                                            obj.fld_result = "completed_MSG_โปรดแสดงรหัส Mc " + obj.fld_receive_code.Trim() + " ที่จุดบริการเพื่อรับสิทธิ์ค่ะ " + GetCurrentDateTime();
                                            break;
                                        default:
                                            obj.fld_result = "notcomplete";
                                            break;
                                    }
                                }
                                break;
                            default:
                                //obj.fld_smile_point_before_booked = mtlwsobj.fld_smile_point_before_booked;
                                //obj.fld_smile_point_after_booked = mtlwsobj.fld_smile_point_after_booked;
                                //obj.fld_point = mtlwsobj.fld_point;
                                //obj.fld_booked_no = mtlwsobj.fld_booked_no;
                                //obj.fld_get_discount_amount = mtlwsobj.fld_get_discount_amount;
                                //obj.fld_get_discount_percent = mtlwsobj.fld_get_discount_percent;
                                //obj.fld_get_discount_from = mtlwsobj.fld_get_discount_from;
                                //obj.fld_receive_code = mtlwsobj.fld_receive_code;
                                break;
                        }

                        //LogResponse: Insert Log Response
                        logobj.AddWSLog(pacobj.PartnerName, ipaddress, "Response", "NETWS_ForPartner", "PartnerSetBookSmileActivity", obj.fld_result + "|" + pacobj.PartnerName + "|" + fld_client_number + "|" + obj.fld_sessionID, refnum);

                        //20130909: CRM ให้เพิ่มส่ง SMS เอง
                        bool sendResult = SendSMS(fld_mobile_phone_number, obj.fld_result.Replace("completed_MSG_", "").Replace("notcomplete_MSG_", ""));
                        return obj;
                    }
                    catch (Exception ex)
                    {
                        //LogRequest: Insert Log Request
                        logobj.AddWSLog(pacobj.PartnerName, ipaddress, "Request", "NETWS_ForPartner", "PartnerSetBookSmileActivity", fld_partner_username + "|" + fld_partner_password + "|" + fld_client_number + "|" + fld_activity_id + "|" + fld_book_no + "|" + fld_mobile_phone_number + "|" + fld_home_phone_number + "|" + fld_home_phone_number_ext + "|" + fld_office_phone_number + "|" + fld_office_phone_number_ext + "|" + fld_smile_branch_code + "|" + fld_request_branch_code + "|" + fld_request_branch_name, refnum);

                        obj.fld_result = "notcomplete_" + ex.ToString();

                        //LogResponse: Insert Log Response
                        logobj.AddWSLog(pacobj.PartnerName, ipaddress, "Response", "NETWS_ForPartner", "PartnerSetBookSmileActivity", obj.fld_result + "|" + pacobj.PartnerName + "|" + fld_client_number + "|" + obj.fld_sessionID, refnum);

                        return obj;
                    }
                }
                else
                {
                    //LogRequest: Insert Log Request
                    logobj.AddWSLog(pacobj.PartnerName, ipaddress, "Request", "NETWS_ForPartner", "PartnerSetBookSmileActivity", fld_partner_username + "|" + fld_partner_password + "|" + fld_client_number + "|" + fld_activity_id + "|" + fld_book_no + "|" + fld_mobile_phone_number + "|" + fld_home_phone_number + "|" + fld_home_phone_number_ext + "|" + fld_office_phone_number + "|" + fld_office_phone_number_ext + "|" + fld_smile_branch_code + "|" + fld_request_branch_code + "|" + fld_request_branch_name, refnum);

                    obj.fld_result = pacobj.Result.Trim().Replace("notpass_", "notcomplete_");

                    //LogResponse: Insert Log Response
                    logobj.AddWSLog(pacobj.PartnerName, ipaddress, "Response", "NETWS_ForPartner", "PartnerSetBookSmileActivity", obj.fld_result + "|" + pacobj.PartnerName + "|" + fld_client_number + "|" + obj.fld_sessionID, refnum);

                    return obj;
                }
            }
        }
        catch (Exception ex)
        {
            //LogRequest: Insert Log Request
            logobj.AddWSLog(fld_partner_username, ipaddress, "Request", "NETWS_ForPartner", "PartnerSetBookSmileActivity", fld_partner_username + "|" + fld_partner_password + "|" + fld_client_number + "|" + fld_activity_id + "|" + fld_book_no + "|" + fld_mobile_phone_number + "|" + fld_home_phone_number + "|" + fld_home_phone_number_ext + "|" + fld_office_phone_number + "|" + fld_office_phone_number_ext + "|" + fld_smile_branch_code + "|" + fld_request_branch_code + "|" + fld_request_branch_name, refnum);

            obj.fld_result = "notcomplete_" + ex.ToString();

            //LogResponse: Insert Log Response
            logobj.AddWSLog(fld_partner_username, ipaddress, "Response", "NETWS_ForPartner", "PartnerSetBookSmileActivity", obj.fld_result + "|" + obj.fld_sessionID, refnum);

            return obj;
        }
    }

    [WebMethod(Description = "สำหรับ Model 2 ใช้แสดงคะแนนสะสม Smile Point และรายละเอียดลูกค้า")]
    public GetCustomerDetail_Result PartnerGetCustomerDetail(string fld_partner_username, string fld_partner_password, string fld_client_number, string fld_mobile_number)
    {
        GetCustomerDetail_Result obj = new GetCustomerDetail_Result();
        RefRunningTBBLL runningobj = new RefRunningTBBLL();
        refnum = runningobj.AddRefRunningTBAndReturn();
        WSLogBLL logobj = new WSLogBLL();

        try
        {
            //ตรวจสอบค่าที่จำเป็นต้องส่งมาให้ครบถ้วนก่อน
            if (fld_partner_username == "" || fld_partner_password == "" || fld_client_number == "")
            {
                //LogRequest: Insert Log Request
                logobj.AddWSLog(fld_partner_username, ipaddress, "Request", "NETWS_ForPartner", "PartnerGetCustomerDetail", fld_partner_username + "|" + fld_partner_password + "|" + fld_client_number + "|" + fld_mobile_number, refnum);
    
                obj.fld_result = "notcomplete_กรุณาระบุ Partner Username หรือ Partner Password หรือ Client Number ให้ถูกต้อง";

                //LogResponse: Insert Log Response
                logobj.AddWSLog(fld_partner_username, ipaddress, "Response", "NETWS_ForPartner", "PartnerGetCustomerDetail", obj.fld_result + "|" + fld_partner_username + "|" + fld_client_number + "|" + obj.fld_sessionID, refnum);

                return obj;
            }
            else
            {
                //พิสูจน์ตัวตนของพันธมิตรก่อนที่จะให้ใช้งานจริง
                NETWS_ForPartnerAuthenticationChecking.CheckPartnerAuthentication_Result pacobj = CheckPartnerAuthenticationReturnDetail(fld_partner_username, fld_partner_password, ipaddress);
                if (pacobj.Result.Trim().ToLower() == "passed")
                {
                    try
                    {
                        //LogRequest: Insert Log Request
                        logobj.AddWSLog(pacobj.PartnerName, ipaddress, "Request", "NETWS_ForPartner", "PartnerGetCustomerDetail", fld_partner_username + "|" + fld_partner_password + "|" + fld_client_number + "|" + fld_mobile_number, refnum);

                        GetCustomerDetail_Result mtlwsobj = MTLGetCustomerDetail(fld_client_number);
                        obj.fld_result = mtlwsobj.fld_result;
                        obj.fld_sessionID = mtlwsobj.fld_sessionID;

                        //ใช้เพื่อกำหนดค่าสำหรับ Output ที่เฉพาะเจาะจงให้กับแต่ละพันธมิตร
                        switch (pacobj.PartnerName.Trim())
                        {
                            case "i-wiz":
                                //ตรวจสอบเงื่อนไขต่างๆ ตามความต้องการ
                                #region i-wiz เงื่อนไขการตรวจสอบจาก CRM สำหรับโมเดล 2
                                /**************************************
                                Step1 ลูกค้าพิมพ์ SMS ข้อความ Client Number เช่น 1201404626 แล้วส่งมาที่เบอร์ 4839009
                                Step2 ระบบ i-wiz & MTL ตรวจสอบตามเงื่อนไข
                                Step3 แจ้งตอบกลับลูกค้าผ่าน SMS
                                **************************************/
                                //กรณีส่ง message ผิดเบอร์ [ควบคุมไม่ได้]
                                //ไม่แสดงข้อความ
                                /*
                                 * ไม่ต้องทำอะไร
                                 */

                                //กรณีเบอร์มือถือไม่ตรงกับฐานข้อมูลเบอร์ใน SMC
                                //ขออภัยค่ะ เบอร์โทรศัพท์ของท่านไม่ได้รับสิทธิ์ กรุณาติดต่อ 1766 กด 4 ค่ะ (70)
                                /*
                                 * .NET ต้องเช็คกับข้อมูลที่ได้จาก MTLGetCustomerDetail() ให้ (ข้อมูลเบอร์โทรมือถือ SMC ที่ต๋องเพิ่มใหม่)
                                 */

                                //กรณี Client Number ไม่มีในระบบ
                                //ขออภัยค่ะ เลขที่ประจำตัวไม่ถูกต้อง กรุณาตรวจสอบและส่งใหม่อีกครั้งค่ะ (68)
                                /*
                                 * .NET ต้องเช็คกับ fld_result จาก MTLGetCustomerDetail() ให้ (notfound)
                                 */

                                //กรณีเบอร์มือถือและ Client Number ไม่ match กันกับฐานข้อมูลใน SMC
                                //เบอร์มือถือและเลขประจำตัวของท่านไม่ตรงกับในระบบ กรุณาติดต่อ 1766 กด 4 ค่ะ (69)
                                /*
                                 * ??? เช็คยังไง ??? .NET ต้องเช็คกับข้อมูลที่ได้จาก MTLGetCustomerDetail() ให้
                                 */

                                //กรณีระบบขัดข้อง เช่น ส่งถูกเบอร์แล้วไม่ได้รับข้อความตอบกลับ
                                //ขออภัยค่ะ ระบบขัดข้องชั่วคราว กรุณาติดต่อ 1766 กด 4 ค่ะ (55)
                                /*
                                 * i-wiz ต้องเช็คให้
                                 */

                                //กรณีตรวจสอบคะแนนสะสมได้
                                //คะแนนสะสม Smile Point คงเหลือของคุณคือ xxxx.xx คะแนน โดยมีคะแนนที่จะหมดอายุในวันที่ 30/06/xx จำนวน xxxx.xx คะแนน และ 31/12/xx xxxx.xx คะแนนค่ะ (140)
                                /*
                                 * .NET ต้องเช็คกับ fld_result จาก MTLGetCustomerDetail() ให้ (found)
                                 */
                                #endregion

                                if (mtlwsobj.fld_result.Trim() == "notfound")
                                {
                                    //obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ เลขที่ประจำตัวของท่านไม่ได้รับสิทธิ์ กรุณาติดต่อ 1766 กด 4 ค่ะ";
                                    obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ เลขที่ประจำตัวของท่านไม่ได้รับสิทธิ์ กรุณาติดต่อ 1766 กด4ค่ะ";
                                }
                                else if (mtlwsobj.fld_result.Trim() == "notfound_ข้อมูลยังรันไม่เสร็จ")
                                {
                                    //obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ ระบบยังไม่สามารถให้บริการได้ในขณะนี้ กรุณาติดต่อ 1766 กด 4 ค่ะ";
                                    //20130908: นิวแจ้งแก้ไข 26/08/2013
                                    obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ ระบบไม่สามารถให้บริการได้ในขณะนี้ กรุณาติดต่อ 1766 กด 4 ค่ะ";
                                }
                                else if (mtlwsobj.fld_client_isSmileClubMember.Trim() != "Y" && mtlwsobj.fld_client_isSmileClubMember != "S")
                                {
                                    obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ เลขที่ประจำตัวของท่านไม่ได้รับสิทธิ์ กรุณาติดต่อ 1766 กด4ค่ะ";
                                }
                                else if (mtlwsobj.fld_result.Trim() == "found" && (mtlwsobj.fld_mobile_phone_number_SMC.Trim() != fld_mobile_number.Trim()))
                                {
                                    //obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ เบอร์โทรศัพท์ของท่านไม่ได้รับสิทธิ์ กรุณาติดต่อ 1766 กด 4 ค่ะ";
                                    obj.fld_result = "notcomplete_MSG_เบอร์มือถือไม่ตรงกับเลขที่ประจำตัวของท่านในระบบ กรุณาติดต่อ1766กด4ค่ะ";
                                }
                                else
                                {
                                    switch (obj.fld_result.Trim())
                                    {
                                        case "notfound_ข้อมูลยังรันไม่เสร็จ":
                                            //obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ ระบบยังไม่สามารถให้บริการได้ในขณะนี้ กรุณาติดต่อ 1766 กด 4 ค่ะ";
                                            //20130908: นิวแจ้งแก้ไข 26/08/2013
                                            obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ ระบบไม่สามารถให้บริการได้ในขณะนี้ กรุณาติดต่อ 1766 กด 4 ค่ะ";
                                            break;
                                        case "notfound":
                                            //obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ เลขที่ประจำตัวของท่านไม่ได้รับสิทธิ์ กรุณาติดต่อ 1766 กด 4 ค่ะ";
                                            obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ เลขที่ประจำตัวของท่านไม่ได้รับสิทธิ์ กรุณาติดต่อ 1766 กด4ค่ะ";
                                            break;
                                        case "found":
                                            obj.fld_customer_name = mtlwsobj.fld_customer_name;
                                            obj.fld_customer_surname = mtlwsobj.fld_customer_surname;
                                            obj.fld_customer_dob = mtlwsobj.fld_customer_dob;
                                            obj.fld_customer_age = mtlwsobj.fld_customer_age;
                                            obj.fld_customer_idcard = mtlwsobj.fld_customer_idcard;
                                            obj.fld_smile_point = mtlwsobj.fld_smile_point;
                                            obj.fld_card_type = mtlwsobj.fld_card_type;
                                            obj.fld_email = mtlwsobj.fld_email;
                                            obj.fld_address_line1 = mtlwsobj.fld_address_line1;
                                            obj.fld_address_line2 = mtlwsobj.fld_address_line2;
                                            obj.fld_address_line3 = mtlwsobj.fld_address_line3;
                                            obj.fld_mobile_phone_number = mtlwsobj.fld_mobile_phone_number;
                                            obj.fld_home_phone_number = mtlwsobj.fld_home_phone_number;
                                            obj.fld_office_phone_number = mtlwsobj.fld_office_phone_number;
                                            obj.fld_client_isAgent = mtlwsobj.fld_client_isAgent;
                                            obj.fld_client_isSmileClubMember = mtlwsobj.fld_client_isSmileClubMember;
                                            obj.fld_expiry_point_round1 = mtlwsobj.fld_expiry_point_round1;
                                            obj.fld_expiry_date_round1 = mtlwsobj.fld_expiry_date_round1;
                                            obj.fld_expiry_point_round2 = mtlwsobj.fld_expiry_point_round2;
                                            obj.fld_expiry_date_round2 = mtlwsobj.fld_expiry_date_round2;
                                            obj.fld_mobile_phone_number_SMC = mtlwsobj.fld_mobile_phone_number_SMC;

                                            //obj.fld_result = "completed_MSG_คะแนนสะสม Smile Point คงเหลือของคุณคือ " + obj.fld_smile_point.Trim() + " คะแนน โดยมีคะแนนที่จะหมดอายุในวันที่ " + obj.fld_expiry_date_round1.Trim() + " จำนวน " + obj.fld_expiry_point_round1.Trim() + " คะแนน และ " + obj.fld_expiry_date_round2.Trim() + " " + obj.fld_expiry_point_round2.Trim() + " คะแนนค่ะ";
                                            obj.fld_result = "completed_MSG_คะแนนสะสมคงเหลือของคุณคือ " + obj.fld_smile_point.Trim() + " คะแนนค่ะ แจ้งเวลา " + GetCurrentDateTime();
                                            break;
                                        default:
                                            obj.fld_result = "notcomplete";
                                            break;
                                    }
                                }
                                break;
                            default:
                                obj.fld_customer_name = mtlwsobj.fld_customer_name;
                                obj.fld_customer_surname = mtlwsobj.fld_customer_surname;
                                obj.fld_customer_dob = mtlwsobj.fld_customer_dob;
                                obj.fld_customer_age = mtlwsobj.fld_customer_age;
                                obj.fld_customer_idcard = mtlwsobj.fld_customer_idcard;
                                obj.fld_smile_point = mtlwsobj.fld_smile_point;
                                obj.fld_card_type = mtlwsobj.fld_card_type;
                                obj.fld_email = mtlwsobj.fld_email;
                                obj.fld_address_line1 = mtlwsobj.fld_address_line1;
                                obj.fld_address_line2 = mtlwsobj.fld_address_line2;
                                obj.fld_address_line3 = mtlwsobj.fld_address_line3;
                                obj.fld_mobile_phone_number = mtlwsobj.fld_mobile_phone_number;
                                obj.fld_home_phone_number = mtlwsobj.fld_home_phone_number;
                                obj.fld_office_phone_number = mtlwsobj.fld_office_phone_number;
                                obj.fld_client_isAgent = mtlwsobj.fld_client_isAgent;
                                obj.fld_client_isSmileClubMember = mtlwsobj.fld_client_isSmileClubMember;
                                obj.fld_expiry_point_round1 = mtlwsobj.fld_expiry_point_round1;
                                obj.fld_expiry_date_round1 = mtlwsobj.fld_expiry_date_round1;
                                obj.fld_expiry_point_round2 = mtlwsobj.fld_expiry_point_round2;
                                obj.fld_expiry_date_round2 = mtlwsobj.fld_expiry_date_round2;
                                obj.fld_mobile_phone_number_SMC = mtlwsobj.fld_mobile_phone_number_SMC;
                                break;
                        }

                        //LogResponse: Insert Log Response
                        logobj.AddWSLog(pacobj.PartnerName, ipaddress, "Response", "NETWS_ForPartner", "PartnerGetCustomerDetail", obj.fld_result + "|" + pacobj.PartnerName + "|" + fld_client_number + "|" + obj.fld_sessionID, refnum);

                        //20130909: CRM ให้เพิ่มส่ง SMS เอง
                        bool sendResult = SendSMS(fld_mobile_number, obj.fld_result.Replace("completed_MSG_", "").Replace("notcomplete_MSG_", ""));
                        return obj;
                    }
                    catch (Exception ex)
                    {
                        //LogRequest: Insert Log Request
                        logobj.AddWSLog(pacobj.PartnerName, ipaddress, "Request", "NETWS_ForPartner", "PartnerGetCustomerDetail", fld_partner_username + "|" + fld_partner_password + "|" + fld_client_number + "|" + fld_mobile_number, refnum);
                        
                        obj.fld_result = "notcomplete_" + ex.ToString();

                        //LogResponse: Insert Log Response
                        logobj.AddWSLog(pacobj.PartnerName, ipaddress, "Response", "NETWS_ForPartner", "PartnerGetCustomerDetail", obj.fld_result + "|" + pacobj.PartnerName + "|" + fld_client_number + "|" + obj.fld_sessionID, refnum);

                        return obj;
                    }
                }
                else
                {
                    //LogRequest: Insert Log Request
                    logobj.AddWSLog(pacobj.PartnerName, ipaddress, "Request", "NETWS_ForPartner", "PartnerGetCustomerDetail", fld_partner_username + "|" + fld_partner_password + "|" + fld_client_number + "|" + fld_mobile_number, refnum);
                    
                    obj.fld_result = pacobj.Result.Trim().Replace("notpass_", "notcomplete_");

                    //LogResponse: Insert Log Response
                    logobj.AddWSLog(pacobj.PartnerName, ipaddress, "Response", "NETWS_ForPartner", "PartnerGetCustomerDetail", obj.fld_result + "|" + pacobj.PartnerName + "|" + fld_client_number + "|" + obj.fld_sessionID, refnum);

                    return obj;
                }
            }
        }
        catch (Exception ex)
        {
            //LogRequest: Insert Log Request
            logobj.AddWSLog(fld_partner_username, ipaddress, "Request", "NETWS_ForPartner", "PartnerGetCustomerDetail", fld_partner_username + "|" + fld_partner_password + "|" + fld_client_number + "|" + fld_mobile_number, refnum);

            obj.fld_result = "notcomplete_" + ex.ToString();

            //LogResponse: Insert Log Response
            logobj.AddWSLog(fld_partner_username, ipaddress, "Response", "NETWS_ForPartner", "PartnerGetCustomerDetail", obj.fld_result + "|" + obj.fld_sessionID, refnum);

            return obj;
        }
    }

    [WebMethod(Description = "สำหรับ Model 3 ใช้รับสิทธิ์ โดยใช้คะแนนสะสม Smile Point แลกรับสิทธิ์")]
    public SetBookSmileActivity_NEW_Result PartnerSetBookSmileActivityWithSmilePassword(string fld_partner_username, string fld_partner_password, string fld_client_number, string fld_smile_password, string fld_activity_id, string fld_book_no, string fld_mobile_phone_number, string fld_home_phone_number, string fld_home_phone_number_ext, string fld_office_phone_number, string fld_office_phone_number_ext, string fld_smile_branch_code, string fld_request_branch_code, string fld_request_branch_name, string fld_additional_amount)
    {
        SetBookSmileActivity_NEW_Result obj = new SetBookSmileActivity_NEW_Result();
        RefRunningTBBLL runningobj = new RefRunningTBBLL();
        refnum = runningobj.AddRefRunningTBAndReturn();
        WSLogBLL logobj = new WSLogBLL();

        try
        {
            //ตรวจสอบค่าที่จำเป็นต้องส่งมาให้ครบถ้วนก่อน
            if (fld_partner_username == "" || fld_partner_password == "" || fld_client_number == "" || fld_smile_password == "" || fld_activity_id == "" || fld_book_no == "" || fld_smile_branch_code == "")
            {
                //LogRequest: Insert Log Request
                logobj.AddWSLog(fld_partner_username, ipaddress, "Request", "NETWS_ForPartner", "PartnerSetBookSmileActivityWithSmilePassword", fld_partner_username + "|" + fld_partner_password + "|" + fld_client_number + "|" + fld_smile_password + "|" + fld_activity_id + "|" + fld_book_no + "|" + fld_mobile_phone_number + "|" + fld_home_phone_number + "|" + fld_home_phone_number_ext + "|" + fld_office_phone_number + "|" + fld_office_phone_number_ext + "|" + fld_smile_branch_code + "|" + fld_request_branch_code + "|" + fld_request_branch_name, refnum);

                obj.fld_result = "notcomplete_กรุณาระบุ Partner Username หรือ Partner Password หรือ Client Number หรือ Smile Password หรือ Activity ID หรือ Book No หรือ Smile Branch Code ให้ถูกต้อง";

                //LogResponse: Insert Log Response
                logobj.AddWSLog(fld_partner_username, ipaddress, "Response", "NETWS_ForPartner", "PartnerSetBookSmileActivityWithSmilePassword", obj.fld_result + "|" + fld_partner_username + "|" + fld_client_number + "|" + obj.fld_sessionID, refnum);

                return obj;
            }
            else
            {
                //พิสูจน์ตัวตนของพันธมิตรก่อนที่จะให้ใช้งานจริง
                NETWS_ForPartnerAuthenticationChecking.CheckPartnerAuthentication_Result pacobj = CheckPartnerAuthenticationReturnDetail(fld_partner_username, fld_partner_password, ipaddress);
                if (pacobj.Result.Trim().ToLower() == "passed")
                {
                    try
                    {
                        //LogRequest: Insert Log Request
                        logobj.AddWSLog(pacobj.PartnerName, ipaddress, "Request", "NETWS_ForPartner", "PartnerSetBookSmileActivityWithSmilePassword", fld_partner_username + "|" + fld_partner_password + "|" + fld_client_number + "|" + fld_smile_password + "|" + fld_activity_id + "|" + fld_book_no + "|" + fld_mobile_phone_number + "|" + fld_home_phone_number + "|" + fld_home_phone_number_ext + "|" + fld_office_phone_number + "|" + fld_office_phone_number_ext + "|" + fld_smile_branch_code + "|" + fld_request_branch_code + "|" + fld_request_branch_name, refnum);

                        //SetBookSmileActivity_NEW_Result mtlwsobj = MTLSetBookSmileActivity_NEW(fld_client_number, fld_activity_id, fld_book_no, fld_mobile_phone_number, fld_home_phone_number, fld_home_phone_number_ext, fld_office_phone_number, fld_office_phone_number_ext, fld_smile_branch_code, fld_request_branch_code, fld_request_branch_name, fld_additional_amount);
                        //obj.fld_result = mtlwsobj.fld_result;
                        //obj.fld_sessionID = mtlwsobj.fld_sessionID;
                        
                        //ใช้เพื่อกำหนดค่าสำหรับ Output ที่เฉพาะเจาะจงให้กับแต่ละพันธมิตร
                        switch (pacobj.PartnerName.Trim())
                        {
                            case "i-wiz":
                                //ตรวจสอบเงื่อนไขต่างๆ ตามความต้องการ
                                #region i-wiz เงื่อนไขการตรวจสอบจาก CRM สำหรับโมเดล 3
                                /**************************************
                                Step1 ลูกค้าพิมพ์ SMS ข้อความ รหัสกิจกรรม * Client Number * รหัสผ่านส่วนตัว Smile Password * จำนวนที่ต้องการแลก เช่น AAAAAAAA*1201404626*1234*2 แล้วส่งมาที่เบอร์ 4839009
                                Step2 ระบบ i-wiz & MTL ตรวจสอบตามเงื่อนไข
                                Step3 แจ้งตอบกลับลูกค้าผ่าน SMS
                                **************************************/
                                //กรณีส่ง message ผิดเบอร์ [ควบคุมไม่ได้]
                                //ไม่แสดงข้อความ
                                /*
                                 * ไม่ต้องทำอะไร
                                 */

                                //กรณีเบอร์มือถือไม่ตรงกับฐานข้อมูลเบอร์ใน SMC
                                //ขออภัยค่ะ เบอร์โทรศัพท์ของท่านไม่ได้รับสิทธิ์ กรุณาติดต่อ 1766 กด 4 ค่ะ (70)
                                /*
                                 * .NET ต้องเช็คกับข้อมูลที่ได้จาก MTLGetCustomerDetail() ให้ (ข้อมูลเบอร์โทรมือถือ SMC ที่ต๋องเพิ่มใหม่)
                                 */

                                //กรณีรหัสกิจกรรมผิด
                                //ขออภัยค่ะ รหัสกิจกรรมไม่ถูกต้อง กรุณาตรวจสอบและส่งใหม่อีกครั้งค่ะ (65)
                                /*
                                 * .NET ต้องเช็คกับ fld_result จาก MTLSetBookSmileActivity_NEW() ใหม่ (notcomplete_รหัสกิจกรรมไม่ถูกต้อง)
                                 */

                                //กรณี Client Number ไม่มีในระบบ
                                //ขออภัยค่ะ เลขที่ประจำตัวไม่ถูกต้อง กรุณาตรวจสอบและส่งใหม่อีกครั้งค่ะ (68)
                                /*
                                 * .NET ต้องเช็คกับ fld_result จาก MTLCheckSmilePassword() ใหม่ (notpass_01_เลขประจำตัวไม่ถูกต้อง)
                                 */

                                //กรณีเบอร์มือถือและ Client Number ไม่ match กันกับฐานข้อมูลใน SMC
                                //เบอร์มือถือและเลขประจำตัวของท่านไม่ตรงกับในระบบ กรุณาติดต่อ 1766 กด 4 ค่ะ (69)
                                /*
                                 * ??? เช็คยังไง ??? .NET ต้องเช็คกับข้อมูลที่ได้จาก MTLGetCustomerDetail() ให้
                                 */

                                //กรณี Client Number และรหัสผ่านไม่ match กันกับฐานข้อมูลใน SMC
                                //เลขประจำตัวและรหัสผ่านของท่านไม่ตรงกับในระบบ กรุณาติดต่อ 1766 กด 4 ค่ะ (70)
                                /*
                                 * .NET ต้องเช็คกับ fld_result จาก MTLCheckSmilePassword() ใหม่ (notpass_02_รหัสผ่านส่วนตัวไม่ถูกต้อง)
                                 */

                                //กรณีกดใช้ในช่วงเวลาที่ไม่อยู่ในระยะเวลากิจกรรม
                                //ขออภัยค่ะ คุณส่งรหัสในช่วงเวลานอกเหนือจากการได้สิทธิ์ ขอบคุณค่ะ (66)
                                /*
                                 * .NET ต้องเช็คกับ fld_result จาก MTLSetBookSmileActivity_NEW() ให้ (notcomplete_ไม่ได้อยู่ในระยะเวลาการแลกคะแนน)
                                 */

                                //กรณีกดใช้หลังจากสิ้นสุดระยะเวลาการจัดกิจกรรม
                                //ขออภัยค่ะ สิทธิพิเศษนี้หมดเขตการรับสิทธิ์แล้ว ขอบคุณที่ให้ความสนใจค่ะ (69)
                                /*
                                 * ??? ไม่ยกเลิกเหมือนในโมเดล 1 เหรอ ??? ยกเลิกแล้ว ไม่ต้องเช็ค
                                 */

                                //กรณีสิทธิประโยชน์มีผู้ใช้สิทธิ์เต็มจำนวนแล้ว
                                //ขออภัยค่ะ มีผู้รับสิทธิ์เต็มจำนวนแล้ว ขอบคุณที่ให้ความสนใจค่ะ (61)
                                /*
                                 * .NET ต้องเช็คกับ fld_result จาก MTLSetBookSmileActivity_NEW() ให้ (notcomplete_จำนวนรางวัลไม่พอสำหรับการแลก)
                                 */

                                //กรณีได้รับสิทธิ์ครบตามเงื่อนไข แต่ส่งมาขอใหม่
                                //ขออภัยค่ะ คุณได้รับสิทธิ์ครบตามเงื่อนไขแล้ว ขอบคุณที่ให้ความสนใจค่ะ (68)
                                /*
                                 * .NET ต้องเช็คกับ fld_result จาก MTLSetBookSmileActivity_NEW() ให้ (notcomplete_แลกเกินจำนวน , notcomplete_แลกเกินจำนวนต่อวัน , notcomplete_แลกเกินจำนวนต่อสัปดาห์ , notcomplete_แลกเกินจำนวนต่อเดือน , notcomplete_แลกเกินจำนวนต่อปี , notcomplete_แลกเกินจำนวนต่อกิจกรรม)
                                 */

                                //กรณีลูกค้ามีคะแนนสะสมไม่เพียงพอที่จะได้รับสิทธิ์
                                //ขออภัยค่ะ คะแนนสะสม Smile Point คงเหลือของคุณมีไม่พอสำหรับแลกรับสิทธิ์ ขณะนี้คุณมีคะแนนสะสมคงเหลือ xxxx.xx คะแนนค่ะ กรุณาติดต่อ 1766 กด 4 ค่ะ (140)
                                /*
                                 * .NET ต้องเช็คกับ fld_result จาก MTLSetBookSmileActivity_NEW() ให้ (notcomplete_จำนวน POINT ไม่พอสำหรับการแลก)
                                 */

                                //กรณีระบบขัดข้อง เช่น ส่งแล้วไม่ได้ข้อความตอบกลับ [i-wiz ต้องเช็ค]
                                //ขออภัยค่ะ ระบบขัดข้องชั่วคราว กรุณาติดต่อ 1766 กด 4 ค่ะ (55)
                                /*
                                 * i-wiz ต้องเช็คให้
                                 */

                                //กรณีตรวจสอบสิทธิ์แล้วลูกค้าได้รับสิทธิ์
                                //โปรดแสดงรหัส MTL xxxx และจำนวน x สิทธิ์ที่จุดบริการเพื่อรับสิทธิ์ค่ะ (68)
                                /*
                                 * .NET ต้องเช็คกับ fld_result จาก MTLSetBookSmileActivity_NEW() ให้ (completed)
                                 */
                                #endregion

                                GetCustomerDetail_Result customerwsobj = MTLGetCustomerDetail(fld_client_number);
                                if (customerwsobj.fld_result.Trim() == "notfound")
                                {
                                    //obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ เลขที่ประจำตัวของท่านไม่ได้รับสิทธิ์ กรุณาติดต่อ 1766 กด 4 ค่ะ";
                                    obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ เลขที่ประจำตัวของท่านไม่ได้รับสิทธิ์ กรุณาติดต่อ 1766 กด4ค่ะ";
                                }
                                else if (customerwsobj.fld_result.Trim() == "notfound_ข้อมูลยังรันไม่เสร็จ")
                                {
                                    //obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ ระบบยังไม่สามารถให้บริการได้ในขณะนี้ กรุณาติดต่อ 1766 กด 4 ค่ะ";
                                    //20130908: นิวแจ้งแก้ไข 26/08/2013
                                    obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ ระบบไม่สามารถให้บริการได้ในขณะนี้ กรุณาติดต่อ 1766 กด 4 ค่ะ";
                                }
                                else if (customerwsobj.fld_client_isSmileClubMember.Trim() != "Y" && customerwsobj.fld_client_isSmileClubMember != "S")
                                {
                                    obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ เลขที่ประจำตัวของท่านไม่ได้รับสิทธิ์ กรุณาติดต่อ 1766 กด4ค่ะ";
                                }
                                //20130917: นิวแจ้งแก้ไข เฉพาะ Model 3 ไม่ต้องตรวจเช็คเบอร์โทรว่าตรงกันหรือไม่ 17/09/2013
                                //else if (customerwsobj.fld_result.Trim() == "found" && (customerwsobj.fld_mobile_phone_number_SMC.Trim() != fld_mobile_phone_number.Trim()))
                                //{
                                //    //obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ เบอร์โทรศัพท์ของท่านไม่ได้รับสิทธิ์ กรุณาติดต่อ 1766 กด 4 ค่ะ";
                                //    obj.fld_result = "notcomplete_MSG_เบอร์มือถือไม่ตรงกับเลขที่ประจำตัวของท่านในระบบ กรุณาติดต่อ1766กด4ค่ะ";
                                //}
                                else
                                {
                                    CheckSmilePassword_Result smilepasswordwsobj = MTLCheckSmilePassword(fld_client_number, fld_smile_password, "");
                                    if (smilepasswordwsobj.fld_result.Trim() == "notpass_01_เลขประจำตัวไม่ถูกต้อง")
                                    {
                                        //obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ เลขที่ประจำตัวของท่านไม่ได้รับสิทธิ์ กรุณาติดต่อ 1766 กด 4 ค่ะ";
                                        obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ เลขที่ประจำตัวของท่านไม่ได้รับสิทธิ์ กรุณาติดต่อ 1766 กด4ค่ะ";
                                    }
                                    else if (smilepasswordwsobj.fld_result.Trim() == "notpass_02_รหัสผ่านส่วนตัวไม่ถูกต้อง")
                                    {
                                        obj.fld_result = "notcomplete_MSG_เลขประจำตัวและรหัสผ่านของท่านไม่ตรงกับในระบบ กรุณาติดต่อ 1766 กด 4 ค่ะ";
                                    }
                                    else if (smilepasswordwsobj.fld_result.Trim() == "notpass_06_เลขประจำตัวนี้ยังไม่ได้เป็นสมาชิก")
                                    {
                                        //obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ เลขที่ประจำตัวของท่านไม่ได้รับสิทธิ์ กรุณาติดต่อ 1766 กด 4 ค่ะ";
                                        obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ เลขที่ประจำตัวของท่านไม่ได้รับสิทธิ์ กรุณาติดต่อ 1766 กด4ค่ะ";
                                    }
                                    else if (smilepasswordwsobj.fld_result.Trim() == "notpass_04_ยังไม่ได้ทำการActivation")
                                    {
                                        //obj.fld_result = "notcomplete_MSG_เลขประจำตัวและรหัสผ่านของท่านไม่ตรงกับในระบบ กรุณาติดต่อ 1766 กด 4 ค่ะ";
                                        //20130908: นิวแจ้งแก้ไข 26/08/2013
                                        obj.fld_result = "notcomplete_MSG_ท่านยังไม่ได้ลงทะเบียนเปิดบัตร Smile Club กรุณาติดต่อ 1766 กด 4 ค่ะ";
                                    }
                                    //20131014: เสมแจ้งแก้ไข 14/10/2556 เฉพาะ Model 3 ไม่ต้องเช็ค error message notpass_03_บัตรประจำตัวนี้ถูกระงับการใช้งาน
                                    //else if (smilepasswordwsobj.fld_result.Trim() == "notpass_03_บัตรประจำตัวนี้ถูกระงับการใช้งาน")
                                    //{
                                    //    obj.fld_result = "notcomplete_MSG_เลขประจำตัวและรหัสผ่านของท่านไม่ตรงกับในระบบ กรุณาติดต่อ 1766 กด 4 ค่ะ";
                                    //}
                                    else
                                    {
                                        SetBookSmileActivity_NEW_Result mtlwsobj = MTLSetBookSmileActivity_NEW(fld_client_number, fld_activity_id, fld_book_no, fld_mobile_phone_number, fld_home_phone_number, fld_home_phone_number_ext, fld_office_phone_number, fld_office_phone_number_ext, fld_smile_branch_code, fld_request_branch_code, fld_request_branch_name, fld_additional_amount);
                                        obj.fld_result = mtlwsobj.fld_result;
                                        obj.fld_sessionID = mtlwsobj.fld_sessionID;

                                        switch (mtlwsobj.fld_result.Trim())
                                        {
                                            case "notcomplete_ไม่มีข้อมูลสาขานี้":
                                                obj.fld_result = "";
                                                break;
                                            case "notcomplete_กรุณาระบุรหัสกิจกรรม":
                                                obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ รหัสกิจกรรมไม่ถูกต้อง กรุณาตรวจสอบและส่งใหม่อีกครั้งค่ะ";
                                                break;
                                            case "notcomplete_รหัสกิจกรรมไม่ถูกต้อง":
                                                obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ รหัสกิจกรรมไม่ถูกต้อง กรุณาตรวจสอบและส่งใหม่อีกครั้งค่ะ";
                                                break;
                                            case "notcomplete_ไม่พบกิจกรรมในสาขานี้":
                                                obj.fld_result = "";
                                                break;
                                            case "notcomplete_จำนวนรางวัลไม่พอสำหรับการแลก":
                                                obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ มีผู้รับสิทธิ์เต็มจำนวนแล้ว ขอบคุณที่ให้ความสนใจค่ะ";
                                                break;
                                            case "notpass_มี SUB กิจกรรม":
                                                obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ รหัสกิจกรรมไม่ถูกต้อง กรุณาตรวจสอบและส่งใหม่อีกครั้งค่ะ";
                                                break;
                                            case "notcomplete_ไม่ได้เป็นสมาชิกSmileClub":
                                                //obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ เลขที่ประจำตัวของท่านไม่ได้รับสิทธิ์ กรุณาติดต่อ 1766 กด 4 ค่ะ";
                                                obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ เลขที่ประจำตัวของท่านไม่ได้รับสิทธิ์ กรุณาติดต่อ 1766 กด4ค่ะ";
                                                break;
                                            case "notcomplete_ยังไม่เปิดสิทธิในการแลก":
                                                obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ คุณส่งรหัสในช่วงเวลานอกเหนือจากการได้รับสิทธิ์ ขอบคุณค่ะ";
                                                break;
                                            case "notcomplete_ไม่มีข้อมูลลูกค้า":
                                                //obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ เลขที่ประจำตัวของท่านไม่ได้รับสิทธิ์ กรุณาติดต่อ 1766 กด 4 ค่ะ";
                                                obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ เลขที่ประจำตัวของท่านไม่ได้รับสิทธิ์ กรุณาติดต่อ 1766 กด4ค่ะ";
                                                break;
                                            case "notcomplete_ไม่มีข้อมูลคะแนนสะสม":
                                                //obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ คะแนนสะสม Smile Point คงเหลือของคุณมีไม่พอสำหรับแลกสิทธิ์ ขณะนี้คุณมีคะแนนสะสมคงเหลือ 0 คะแนนค่ะ สอบถามติดต่อ 1766 กด 4 ค่ะ";
                                                //20130908: นิวแจ้งแก้ไข 26/08/2013
                                                obj.fld_result = "notcomplete_MSG_คะแนนสะสมคงเหลือของคุณคือ 0 คะแนนค่ะ แจ้งเวลา " + GetCurrentDateTime();
                                                break;
                                            case "notpass_กรุณาระบุจำนวนเงิน":
                                                obj.fld_result = "";
                                                break;
                                            case "notcomplete_กรุณาระบุผู้มาแลกรับ":
                                                obj.fld_result = "";
                                                break;
                                            case "notcomplete_ไม่ได้อยู่ในระยะเวลาการแลกคะแนน":
                                                //obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ คุณส่งรหัสในช่วงเวลานอกเหนือจากการได้สิทธิ์ ขอบคุณค่ะ";
                                                //20130908: นิวแจ้งแก้ไข 26/08/2013
                                                obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ คุณส่งรหัสในช่วงเวลานอกเหนือจากการได้รับสิทธิ์ ขอบคุณค่ะ";
                                                break;
                                            case "notcomplete_จำนวนรางวัลเกินกว่าที่กำหนด":
                                                obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ จำนวนสิทธิ์ที่คุณต้องการแลกเกินกว่าเงื่อนไขที่กำหนดค่ะ";
                                                break;
                                            case "notcomplete_กรุณาใส่จำนวนรางวัล":
                                                //obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ จำนวนสิทธิ์ที่คุณต้องการแลกเกินกว่าเงื่อนไขที่กำหนดค่ะ";
                                                obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ กรุณาระบุจำนวนสิทธิ์ที่คุณต้องการแลกด้วยค่ะ";
                                                break;
                                            case "notcomplete_พนักงานหรือตัวแทนไม่สามารถแลกได้ในขณะนี้":
                                                //obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ พนักงานหรือตัวแทนไม่สามารถรับสิทธิ์ได้ในขณะนี้ค่ะ";
                                                obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ พนักงานหรือตัวแทนไม่สามารถรับสิทธิ์นี้ได้ค่ะ";
                                                break;
                                            case "notcomplete_จำนวนคะแนนไม่เพียงพอ":
                                                //obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ คะแนนสะสม Smile Point คงเหลือของคุณมีไม่พอสำหรับแลกรับสิทธิ์ ขณะนี้คุณมีคะแนนสะสมคงเหลือ " + customerwsobj.fld_smile_point + " คะแนนค่ะ สอบถามติดต่อ 1766 กด 4 ค่ะ";
                                                //obj.fld_result = "notcomplete_MSG_คุณมีคะแนนสะสมไม่เพียงพอ คงเหลือ " + customerwsobj.fld_smile_point + " คะแนน แจ้ง " + GetCurrentDateTime();
                                                //20131014: เสมแจ้งแก้ไข 14/10/2556
                                                obj.fld_result = "notcomplete_MSG_คุณมีคะแนนสะสมไม่เพียงพอ คงเหลือ " + customerwsobj.fld_smile_point.Trim() + " คะแนน แจ้ง " + GetCurrentDateTime();
                                                break;
                                            case "notcomplete_แลกเกินจำนวน":
                                                //obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ คุณได้รับสิทธิ์ครบตามเงื่อนไขแล้ว ขอบคุณที่ให้ความสนใจค่ะ";
                                                //20130909: นิวแจ้งแก้ไข 09/09/2013
                                                obj.fld_result = "notcomplete_MSG_กิจกรรมนี้สามารถแลกได้ 1 สิทธิ์ต่อสัปดาห์ (จันทร์-อาทิตย์) เท่านั้นค่ะ";
                                                break;
                                            case "notcomplete_แลกเกินจำนวนต่อวัน":
                                                //obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ คุณได้รับสิทธิ์ครบตามเงื่อนไขแล้ว ขอบคุณที่ให้ความสนใจค่ะ";
                                                //20130909: นิวแจ้งแก้ไข 09/09/2013
                                                //obj.fld_result = "notcomplete_MSG_กิจกรรมนี้สามารถแลกได้ 1 สิทธิ์ต่อสัปดาห์ (จันทร์-อาทิตย์) เท่านั้นค่ะ";
                                                //20140228: นิวแจ้งแก้ไข 27/02/2014
                                                //obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ คุณขอใช้สิทธิ์เกินกว่าจำนวนสิทธิ์ต่อวันที่เงื่อนไขกำหนดค่ะ";
                                                //20140314: นิวแจ้งแก้ไข 14/03/2014 รองรับ B-Quik (แบบรับสิทธิ์ฟรี 55990000, แบบแลกคะแนนรับสิทธิ์ 56000000) ใช้พร้อมกับ Siam Future
                                                if (fld_activity_id == "55990000")
                                                {
                                                    obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ คุณขอใช้สิทธิ์เกินกว่าจำนวนสิทธิ์ที่เงื่อนไขกำหนดค่ะ";
                                                }
                                                //20140724: เสมแจ้งแก้ไข 21/07/2014 รองรับแคมเปญ 10th Year Muang Thai Smile Give Double McDonald's (ชุดอร่อยสุดคุ้ม 56630000, ชุดเครื่องดื่มแมคคาเฟ่ 56640000)
                                                else if (fld_activity_id == "56630000" || fld_activity_id == "56640000")
                                                {
                                                    obj.fld_result = "notcomplete_MSG_กิจกรรมนี้แต่ละแคมเปญสามารถแลกได้ 1 สิทธิ์ต่อวัน เท่านั้นค่ะ";
                                                }
                                                //20140805: เสมแจ้งแก้ไข 21/07/2014 รองรับแคมเปญ Smile Shopping ข้อปสนุกลดสนั่นทั่วไทย (สินค้าเครือ CMG) (56660000)
                                                else if (fld_activity_id == "56660000")
                                                {
                                                    obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ คุณขอใช้สิทธิ์เกินกว่าจำนวนสิทธิ์ต่อวันที่เงื่อนไขกำหนดค่ะ";
                                                }
                                                //20140805: เสมแจ้งแก้ไข 21/07/2014 รองรับแคมเปญ King Power Lounge (56040000)
                                                else if (fld_activity_id == "56040000")
                                                {
                                                    obj.fld_result = "notcomplete_MSG_กิจกรรมนี้สามารถแลกได้ 1 สิทธิ์ต่อวัน เท่านั้นค่ะ";
                                                }
                                                //20141104: เสมแจ้งแก้ไข 31/10/2014 รองรับแคมเปญ Lazada (57330000)
                                                else if (fld_activity_id == "57330000")
                                                {
                                                    obj.fld_result = "notcomplete_MSG_กิจกรรมนี้ตลอดทั้งโครงการสามารถแลกได้ 1 สิทธิ์ต่อท่านเท่านั้นค่ะ";
                                                }
                                                //20141203: เสมแจ้งแก้ไข 03/12/2014 รองรับแคมเปญ Lazada ส่วนลด 300 บาท 12/12 (57490000)
                                                else if (fld_activity_id == "57490000")
                                                {
                                                    obj.fld_result = "notcomplete_MSG_กิจกรรมนี้ตลอดทั้งโครงการสามารถแลกได้ 1 สิทธิ์ต่อท่านเท่านั้นค่ะ";
                                                }
                                                //20150106: เสมแจ้งแก้ไข 05/01/2015 รองรับแคมเปญลดสุด คุ้มช้อปกับ ZALORA (57840000)
                                                else if (fld_activity_id == "57840000")
                                                {
                                                    obj.fld_result = "notcomplete_MSG_กิจกรรมนี้ตลอดทั้งโครงการสามารถแลกได้ 1 สิทธิ์ต่อท่านเท่านั้นค่ะ";
                                                }
                                                //20150131: เสมแจ้งแก้ไข 27/01/2015 รองรับแคมเปญ Smile Movie Day9 - ป๊อบคอร์น (58130000)
                                                else if (fld_activity_id == "58130000")
                                                {
                                                    obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ คุณขอใช้สิทธิ์เกินกว่าจำนวนสิทธิ์ต่อวันที่เงื่อนไขกำหนดค่ะ";
                                                }
                                                //20150704: เสมแจ้งแก้ไข 25/06/2015 รองรับแคมเปญ Smile HomeService กับ HomePro (58640000)
                                                else if (fld_activity_id == "58640000")
                                                {
                                                    obj.fld_result = "notcomplete_MSG_กิจกรรมนี้ตลอดทั้งโครงการสามารถแลกได้ 4 สิทธิ์ต่อท่านเท่านั้นค่ะ";
                                                }
                                                //20150704: เสมแจ้งแก้ไข 02/07/2015 รองรับแคมเปญ เที่ยวเมืองไทย I LOVE U - ส่วนลด 500 บาท (59950000)
                                                else if (fld_activity_id == "59950000")
                                                {
                                                    obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ คุณขอใช้สิทธิ์เกินกว่าจำนวนสิทธิ์ต่อวันที่เงื่อนไขกำหนดค่ะ";
                                                }
                                                //20150704: เสมแจ้งแก้ไข 02/07/2015 รองรับแคมเปญ เที่ยวเมืองไทย I LOVE U - ส่วนลด 1000 บาท (59960000)
                                                else if (fld_activity_id == "59960000")
                                                {
                                                    obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ คุณขอใช้สิทธิ์เกินกว่าจำนวนสิทธิ์ต่อวันที่เงื่อนไขกำหนดค่ะ";
                                                }
                                                //20150704: เสมแจ้งแก้ไข 02/07/2015 รองรับแคมเปญ เที่ยวเมืองไทย I LOVE U - ห้อง BRC (59970000)
                                                else if (fld_activity_id == "59970000")
                                                {
                                                    obj.fld_result = "notcomplete_MSG_คุณอยู่นอกเหนือเงื่อนไขการให้บริการค่ะ สอบถามโทร 1766 กด 4";
                                                }
                                                else
                                                {
                                                    obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ คุณขอใช้สิทธิ์เกินกว่าจำนวนสิทธิ์ต่อวันที่เงื่อนไขกำหนดค่ะ";
                                                }
                                                break;
                                            case "notcomplete_แลกเกินจำนวนต่อสัปดาห์":
                                                //obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ คุณได้รับสิทธิ์ครบตามเงื่อนไขแล้ว ขอบคุณที่ให้ความสนใจค่ะ";
                                                //20130909: นิวแจ้งแก้ไข 09/09/2013
                                                obj.fld_result = "notcomplete_MSG_กิจกรรมนี้สามารถแลกได้ 1 สิทธิ์ต่อสัปดาห์ (จันทร์-อาทิตย์) เท่านั้นค่ะ";
                                                break;
                                            case "notcomplete_แลกเกินจำนวนต่อเดือน":
                                                //obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ คุณได้รับสิทธิ์ครบตามเงื่อนไขแล้ว ขอบคุณที่ให้ความสนใจค่ะ";
                                                //20130909: นิวแจ้งแก้ไข 09/09/2013
                                                obj.fld_result = "notcomplete_MSG_กิจกรรมนี้สามารถแลกได้ 1 สิทธิ์ต่อสัปดาห์ (จันทร์-อาทิตย์) เท่านั้นค่ะ";
                                                break;
                                            case "notcomplete_แลกเกินจำนวนต่อปี":
                                                //obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ คุณได้รับสิทธิ์ครบตามเงื่อนไขแล้ว ขอบคุณที่ให้ความสนใจค่ะ";
                                                //20130909: นิวแจ้งแก้ไข 09/09/2013
                                                obj.fld_result = "notcomplete_MSG_กิจกรรมนี้สามารถแลกได้ 1 สิทธิ์ต่อสัปดาห์ (จันทร์-อาทิตย์) เท่านั้นค่ะ";
                                                break;
                                            case "notcomplete_แลกเกินจำนวนต่อกิจกรรม":
                                                //obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ คุณได้รับสิทธิ์ครบตามเงื่อนไขแล้ว ขอบคุณที่ให้ความสนใจค่ะ";
                                                //20130909: นิวแจ้งแก้ไข 09/09/2013
                                                //obj.fld_result = "notcomplete_MSG_กิจกรรมนี้สามารถแลกได้ 1 สิทธิ์ต่อสัปดาห์ (จันทร์-อาทิตย์) เท่านั้นค่ะ";
                                                //20140314: นิวแจ้งแก้ไข 14/03/2014 รองรับ B-Quik (แบบรับสิทธิ์ฟรี 55990000, แบบแลกคะแนนรับสิทธิ์ 56000000) ใช้พร้อมกับ Siam Future
                                                if (fld_activity_id == "55990000")
                                                {
                                                    obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ คุณขอใช้สิทธิ์เกินกว่าจำนวนสิทธิ์ที่เงื่อนไขกำหนดค่ะ";
                                                }
                                                //20140724: เสมแจ้งแก้ไข 21/07/2014 รองรับแคมเปญ 10th Year Muang Thai Smile Give Double McDonald's (ชุดอร่อยสุดคุ้ม 56630000, ชุดเครื่องดื่มแมคคาเฟ่ 56640000)
                                                else if (fld_activity_id == "56630000" || fld_activity_id == "56640000")
                                                {
                                                    obj.fld_result = "notcomplete_MSG_กิจกรรมนี้แต่ละแคมเปญสามารถแลกได้ 1 สิทธิ์ต่อวัน เท่านั้นค่ะ";
                                                }
                                                //20140805: เสมแจ้งแก้ไข 21/07/2014 รองรับแคมเปญ Smile Shopping ข้อปสนุกลดสนั่นทั่วไทย (สินค้าเครือ CMG) (56660000)
                                                else if (fld_activity_id == "56660000")
                                                {
                                                    obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ คุณขอใช้สิทธิ์เกินกว่าจำนวนสิทธิ์ต่อวันที่เงื่อนไขกำหนดค่ะ";
                                                }
                                                //20140805: เสมแจ้งแก้ไข 21/07/2014 รองรับแคมเปญ King Power Lounge (56040000)
                                                else if (fld_activity_id == "56040000")
                                                {
                                                    obj.fld_result = "notcomplete_MSG_กิจกรรมนี้สามารถแลกได้ 1 สิทธิ์ต่อวัน เท่านั้นค่ะ";
                                                }
                                                //20141104: เสมแจ้งแก้ไข 31/10/2014 รองรับแคมเปญ Lazada (57330000)
                                                else if (fld_activity_id == "57330000")
                                                {
                                                    obj.fld_result = "notcomplete_MSG_กิจกรรมนี้ตลอดทั้งโครงการสามารถแลกได้ 1 สิทธิ์ต่อท่านเท่านั้นค่ะ";
                                                }
                                                //20141203: เสมแจ้งแก้ไข 03/12/2014 รองรับแคมเปญ Lazada ส่วนลด 300 บาท 12/12 (57490000)
                                                else if (fld_activity_id == "57490000")
                                                {
                                                    obj.fld_result = "notcomplete_MSG_กิจกรรมนี้ตลอดทั้งโครงการสามารถแลกได้ 1 สิทธิ์ต่อท่านเท่านั้นค่ะ";
                                                }
                                                //20150106: เสมแจ้งแก้ไข 05/01/2015 รองรับแคมเปญลดสุด คุ้มช้อปกับ ZALORA (57840000)
                                                else if (fld_activity_id == "57840000")
                                                {
                                                    obj.fld_result = "notcomplete_MSG_กิจกรรมนี้ตลอดทั้งโครงการสามารถแลกได้ 1 สิทธิ์ต่อท่านเท่านั้นค่ะ";
                                                }
                                                //20150131: เสมแจ้งแก้ไข 27/01/2015 รองรับแคมเปญ Smile Movie Day9 - ป๊อบคอร์น (58130000)
                                                else if (fld_activity_id == "58130000")
                                                {
                                                    obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ คุณขอใช้สิทธิ์เกินกว่าจำนวนสิทธิ์ต่อวันที่เงื่อนไขกำหนดค่ะ";
                                                }
                                                //20150704: เสมแจ้งแก้ไข 25/06/2015 รองรับแคมเปญ Smile HomeService กับ HomePro (58640000)
                                                else if (fld_activity_id == "58640000")
                                                {
                                                    obj.fld_result = "notcomplete_MSG_กิจกรรมนี้ตลอดทั้งโครงการสามารถแลกได้ 4 สิทธิ์ต่อท่านเท่านั้นค่ะ";
                                                }
                                                //20150704: เสมแจ้งแก้ไข 02/07/2015 รองรับแคมเปญ เที่ยวเมืองไทย I LOVE U - ส่วนลด 500 บาท (59950000)
                                                else if (fld_activity_id == "59950000")
                                                {
                                                    obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ คุณขอใช้สิทธิ์เกินกว่าจำนวนสิทธิ์ต่อวันที่เงื่อนไขกำหนดค่ะ";
                                                }
                                                //20150704: เสมแจ้งแก้ไข 02/07/2015 รองรับแคมเปญ เที่ยวเมืองไทย I LOVE U - ส่วนลด 1000 บาท (59960000)
                                                else if (fld_activity_id == "59960000")
                                                {
                                                    obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ คุณขอใช้สิทธิ์เกินกว่าจำนวนสิทธิ์ต่อวันที่เงื่อนไขกำหนดค่ะ";
                                                }
                                                //20150704: เสมแจ้งแก้ไข 02/07/2015 รองรับแคมเปญ เที่ยวเมืองไทย I LOVE U - ห้อง BRC (59970000)
                                                else if (fld_activity_id == "59970000")
                                                {
                                                    obj.fld_result = "notcomplete_MSG_คุณอยู่นอกเหนือเงื่อนไขการให้บริการค่ะ สอบถามโทร 1766 กด 4";
                                                }
                                                else
                                                {
                                                    obj.fld_result = "notcomplete_MSG_กิจกรรมนี้สามารถแลกได้ 1 สิทธิ์ต่อสัปดาห์ (จันทร์-อาทิตย์) เท่านั้นค่ะ"; //Siam Future ไม่มีตกเงื่อนไขนี้ ข้อความนี้จึงเป็นข้อความเก่าของ McDonald's
                                                }
                                                break;
                                            case "notpass_จำนวน POINT ไม่พอสำหรับการแลก":
                                                //obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ คะแนนสะสม Smile Point คงเหลือของคุณมีไม่พอสำหรับแลกรับสิทธิ์ ขณะนี้คุณมีคะแนนสะสมคงเหลือ " + customerwsobj.fld_smile_point + " คะแนนค่ะ สอบถามติดต่อ 1766 กด 4 ค่ะ";
                                                //obj.fld_result = "notcomplete_MSG_คุณมีคะแนนสะสมไม่เพียงพอ คงเหลือ " + customerwsobj.fld_smile_point + " คะแนน แจ้ง " + GetCurrentDateTime();
                                                //20131014: เสมแจ้งแก้ไข 14/10/2556
                                                obj.fld_result = "notcomplete_MSG_คุณมีคะแนนสะสมไม่เพียงพอ คงเหลือ " + customerwsobj.fld_smile_point.Trim() + " คะแนน แจ้ง " + GetCurrentDateTime();
                                                break;
                                            case "notpass_ยอดเงินไม่พอสำหรับการแลก":
                                                obj.fld_result = "";
                                                break;
                                            case "notpass_จำนวนรางวัลไม่พอสำหรับการแลก":
                                                obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ มีผู้รับสิทธิ์เต็มจำนวนแล้ว ขอบคุณที่ให้ความสนใจค่ะ";
                                                break;
                                            case "completed":
                                                obj.fld_smile_point_before_booked = mtlwsobj.fld_smile_point_before_booked;
                                                obj.fld_smile_point_after_booked = mtlwsobj.fld_smile_point_after_booked;
                                                obj.fld_point = mtlwsobj.fld_point;
                                                obj.fld_booked_no = mtlwsobj.fld_booked_no;
                                                obj.fld_get_discount_amount = mtlwsobj.fld_get_discount_amount;
                                                obj.fld_get_discount_percent = mtlwsobj.fld_get_discount_percent;
                                                obj.fld_get_discount_from = mtlwsobj.fld_get_discount_from;
                                                obj.fld_receive_code = mtlwsobj.fld_receive_code;
                                                obj.fld_comment = mtlwsobj.fld_comment;

                                                //obj.fld_result = "completed_MSG_โปรดแสดงรหัส MTL " + obj.fld_receive_code.Trim() + " และจำนวน " + obj.fld_booked_no.Trim() + " สิทธิ์ที่จุดบริการเพื่อรับสิทธิ์ค่ะ";
                                                //obj.fld_result = "completed_MSG_โปรดแสดงรหัส MTL " + obj.fld_receive_code.Trim() + " จำนวน " + obj.fld_booked_no.Trim() + " สิทธิ์ที่จุดบริการค่ะ " + GetCurrentDateTime();
                                                //20130909: นิวแจ้งแก้ไข 09/09/2013
                                                //obj.fld_result = "completed_MSG_โปรดแสดงรหัส Mc " + obj.fld_receive_code.Trim() + " ที่จุดบริการเพื่อรับสิทธิ์ค่ะ " + GetCurrentDateTime();
                                                //20140131: นิวแจ้งแก้ไข 31/01/2014 สำหรับแคมเปญร่วมกับ Siam Future
                                                //obj.fld_result = "completed_MSG_" + obj.fld_comment.Trim() + " " + obj.fld_receive_code.Trim() + " จำนวน " + obj.fld_booked_no.Trim() + " สิทธิ์ค่ะ " + GetCurrentDateTime() + ", " + fld_client_number.Trim();
                                                //20140314: นิวแจ้งแก้ไข 14/03/2014 รองรับ B-Quik (แบบรับสิทธิ์ฟรี 55990000, แบบแลกคะแนนรับสิทธิ์ 56000000) ใช้พร้อมกับ Siam Future
                                                if (fld_activity_id == "55990000")
                                                {
                                                    obj.fld_result = "completed_MSG_" + "บีควิก ฟรี รหัส " + obj.fld_receive_code.Trim() + " จำนวน " + obj.fld_booked_no.Trim() + " สิทธิ์ค่ะ " + GetCurrentDateTime() + ", " + fld_client_number.Trim();
                                                }
                                                else if (fld_activity_id == "56000000")
                                                {
                                                    obj.fld_result = "completed_MSG_" + "บีควิก ส่วนลดรหัส " + obj.fld_receive_code.Trim() + " จำนวน " + obj.fld_booked_no.Trim() + " สิทธิ์ค่ะ " + GetCurrentDateTime() + ", " + fld_client_number.Trim();
                                                }
                                                //20140724: เสมแจ้งแก้ไข 21/07/2014 รองรับแคมเปญ 10th Year Muang Thai Smile Give Double McDonald's (ชุดอร่อยสุดคุ้ม 56630000, ชุดเครื่องดื่มแมคคาเฟ่ 56640000)
                                                else if (fld_activity_id == "56630000")
                                                {
                                                    obj.fld_result = "completed_MSG_" + "ชุดอร่อยสุดคุ้ม รหัสรับสิทธิ์" + obj.fld_receive_code.Trim() + " ค่ะ " + GetCurrentDateTime();
                                                }
                                                else if (fld_activity_id == "56640000")
                                                {
                                                    obj.fld_result = "completed_MSG_" + "เครื่องดื่ม McCafe 12 ออนซ์ รหัสรับสิทธิ์" + obj.fld_receive_code.Trim() + " ค่ะ " + GetCurrentDateTime();
                                                }
                                                //20140805: เสมแจ้งแก้ไข 21/07/2014 รองรับแคมเปญ Smile Shopping ข้อปสนุกลดสนั่นทั่วไทย (สินค้าเครือ CMG) (56660000)
                                                else if (fld_activity_id == "56660000")
                                                {
                                                    obj.fld_result = "completed_MSG_" + obj.fld_comment.Trim() + " " + obj.fld_receive_code.Trim() + " จำนวน " + obj.fld_booked_no.Trim() + "สิทธิ์ค่ะ " + GetCurrentDateTime() + ", " + fld_client_number.Trim();
                                                }
                                                //20140805: เสมแจ้งแก้ไข 21/07/2014 รองรับแคมเปญ King Power Lounge (56040000)
                                                else if (fld_activity_id == "56040000")
                                                {
                                                    obj.fld_result = "completed_MSG_" + "King Power Lounge สุวรรณภูมิ รหัสรับสิทธิ์" + obj.fld_receive_code.Trim() + " ค่ะ " + GetCurrentDateTime();
                                                }
                                                //20141104: เสมแจ้งแก้ไข 31/10/2014 รองรับแคมเปญ Lazada (57330000)
                                                else if (fld_activity_id == "57330000")
                                                {
                                                    obj.fld_result = "completed_MSG_" + "LAZADA ส่วนลด 250 บาท รหัสรับสิทธิ์ " + obj.fld_receive_code.Trim() + " ใช้ได้ถึง 31/12/2557";
                                                }
                                                //20141203: เสมแจ้งแก้ไข 03/12/2014 รองรับแคมเปญ Lazada ส่วนลด 300 บาท 12/12 (57490000)
                                                else if (fld_activity_id == "57490000")
                                                {
                                                    obj.fld_result = "completed_MSG_" + "LAZADA ส่วนลด 300 บาท รหัสรับสิทธิ์ " + obj.fld_receive_code.Trim() + " ใช้ได้เฉพาะวันที่ 12/12/2557";
                                                }
                                                //20150106: เสมแจ้งแก้ไข 05/01/2015 รองรับแคมเปญลดสุด คุ้มช้อปกับ ZALORA (57840000)
                                                else if (fld_activity_id == "57840000")
                                                {
                                                    //20150706: เสมแจ้งแก้ไข 06/07/2015 เปลี่ยนจาก "ใช้ได้ถึง 31/03/2558" เป็น "30/09/2558" แทน
                                                    //obj.fld_result = "completed_MSG_" + "ZALORA ส่วนลด 300 บาท รหัสรับสิทธิ์ " + obj.fld_receive_code.Trim() + " ใช้ได้ถึง 31/03/2558";
                                                    obj.fld_result = "completed_MSG_" + "ZALORA ส่วนลด 300 บาท รหัสรับสิทธิ์ " + obj.fld_receive_code.Trim() + " ใช้ได้ถึง 30/09/2558";
                                                }
                                                //20150131: เสมแจ้งแก้ไข 27/01/2015 รองรับแคมเปญ Smile Movie Day9 - ป๊อบคอร์น (58130000)
                                                else if (fld_activity_id == "58130000")
                                                {
                                                    obj.fld_result = "completed_MSG_" + "ชุดป๊อบคอร์น รหัสรับสิทธิ์" + obj.fld_receive_code.Trim() + " ค่ะ " + GetCurrentDateTime();
                                                }
                                                //20150704: เสมแจ้งแก้ไข 25/06/2015 รองรับแคมเปญ Smile HomeService กับ HomePro (58640000)
                                                else if (fld_activity_id == "58640000")
                                                {
                                                    obj.fld_result = "completed_MSG_" + "บริการ Home Service รหัสรับสิทธิ์" + obj.fld_receive_code.Trim() + " ค่ะ " + GetCurrentDateTime();
                                                }
                                                //20150704: เสมแจ้งแก้ไข 02/07/2015 รองรับแคมเปญ เที่ยวเมืองไทย I LOVE U - ส่วนลด 500 บาท (59950000)
                                                else if (fld_activity_id == "59950000")
                                                {
                                                    //20150722: เสมแจ้งแก้ไข 21/07/2015 เพิ่มเว้นวรรค และเปลี่ยนจาก "ส่วนลด500บาท รหัสรับสิทธิ์" เป็น "ส่วนลด600บาท รหัสส่วนลด " แทน
                                                    //obj.fld_result = "completed_MSG_" + "Bangkok Airwaysส่วนลด500บาท รหัสรับสิทธิ์" + obj.fld_receive_code.Trim() + " ค่ะ " + GetCurrentDateTime();
                                                    obj.fld_result = "completed_MSG_" + "Bangkok Airways ส่วนลด600บาท รหัสส่วนลด " + obj.fld_receive_code.Trim() + " ค่ะ " + GetCurrentDateTime();
                                                }
                                                //20150704: เสมแจ้งแก้ไข 02/07/2015 รองรับแคมเปญ เที่ยวเมืองไทย I LOVE U - ส่วนลด 1000 บาท (59960000)
                                                else if (fld_activity_id == "59960000")
                                                {
                                                    //20150722: เสมแจ้งแก้ไข 21/07/2015 เพิ่มเว้นวรรค และเปลี่ยนจาก "ส่วนลด1000บาท รหัสรับสิทธิ์" เป็น "ส่วนลด1200บาท รหัสส่วนลด " แทน
                                                    //obj.fld_result = "completed_MSG_" + "Bangkok Airwaysส่วนลด1000บาท รหัสรับสิทธิ์" + obj.fld_receive_code.Trim() + " ค่ะ " + GetCurrentDateTime();
                                                    obj.fld_result = "completed_MSG_" + "Bangkok Airways ส่วนลด1200บาท รหัสส่วนลด " + obj.fld_receive_code.Trim() + " ค่ะ " + GetCurrentDateTime();
                                                }
                                                //20150704: เสมแจ้งแก้ไข 02/07/2015 รองรับแคมเปญ เที่ยวเมืองไทย I LOVE U - ห้อง BRC (59970000)
                                                else if (fld_activity_id == "59970000")
                                                {
                                                    obj.fld_result = "completed_MSG_" + "คุณได้รับสิทธิ์ใช้ห้องรับรอง Blue Ribbon Club ค่ะ " + GetCurrentDateTime();
                                                }
                                                else
                                                {
                                                    obj.fld_result = "completed_MSG_" + obj.fld_comment.Trim() + " " + obj.fld_receive_code.Trim() + " จำนวน " + obj.fld_booked_no.Trim() + " สิทธิ์ค่ะ " + GetCurrentDateTime() + ", " + fld_client_number.Trim();
                                                }
                                                break;
                                            default:
                                                obj.fld_result = "notcomplete";
                                                break;
                                        }
                                    }
                                }
                                break;
                            default:
                                //obj.fld_smile_point_before_booked = mtlwsobj.fld_smile_point_before_booked;
                                //obj.fld_smile_point_after_booked = mtlwsobj.fld_smile_point_after_booked;
                                //obj.fld_point = mtlwsobj.fld_point;
                                //obj.fld_booked_no = mtlwsobj.fld_booked_no;
                                //obj.fld_get_discount_amount = mtlwsobj.fld_get_discount_amount;
                                //obj.fld_get_discount_from = mtlwsobj.fld_get_discount_from;
                                //obj.fld_get_discount_percent = mtlwsobj.fld_get_discount_percent;
                                //obj.fld_receive_code = mtlwsobj.fld_receive_code;
                                break;
                        }

                        //LogResponse: Insert Log Response
                        logobj.AddWSLog(pacobj.PartnerName, ipaddress, "Response", "NETWS_ForPartner", "PartnerSetBookSmileActivityWithSmilePassword", obj.fld_result + "|" + pacobj.PartnerName + "|" + fld_client_number + "|" + obj.fld_sessionID, refnum);

                        //20130909: CRM ให้เพิ่มส่ง SMS เอง
                        bool sendResult = SendSMS(fld_mobile_phone_number, obj.fld_result.Replace("completed_MSG_", "").Replace("notcomplete_MSG_", ""));
                        return obj;
                    }
                    catch (Exception ex)
                    {
                        //LogRequest: Insert Log Request
                        logobj.AddWSLog(pacobj.PartnerName, ipaddress, "Request", "NETWS_ForPartner", "PartnerSetBookSmileActivityWithSmilePassword", fld_partner_username + "|" + fld_partner_password + "|" + fld_client_number + "|" + fld_smile_password + "|" + fld_activity_id + "|" + fld_book_no + "|" + fld_mobile_phone_number + "|" + fld_home_phone_number + "|" + fld_home_phone_number_ext + "|" + fld_office_phone_number + "|" + fld_office_phone_number_ext + "|" + fld_smile_branch_code + "|" + fld_request_branch_code + "|" + fld_request_branch_name, refnum);

                        obj.fld_result = "notcomplete_" + ex.ToString();

                        //LogResponse: Insert Log Response
                        logobj.AddWSLog(pacobj.PartnerName, ipaddress, "Response", "NETWS_ForPartner", "PartnerSetBookSmileActivityWithSmilePassword", obj.fld_result + "|" + pacobj.PartnerName + "|" + fld_client_number + "|" + obj.fld_sessionID, refnum);

                        return obj;
                    }
                }
                else
                {
                    //LogRequest: Insert Log Request
                    logobj.AddWSLog(pacobj.PartnerName, ipaddress, "Request", "NETWS_ForPartner", "PartnerSetBookSmileActivityWithSmilePassword", fld_partner_username + "|" + fld_partner_password + "|" + fld_client_number + "|" + fld_smile_password + "|" + fld_activity_id + "|" + fld_book_no + "|" + fld_mobile_phone_number + "|" + fld_home_phone_number + "|" + fld_home_phone_number_ext + "|" + fld_office_phone_number + "|" + fld_office_phone_number_ext + "|" + fld_smile_branch_code + "|" + fld_request_branch_code + "|" + fld_request_branch_name, refnum);

                    obj.fld_result = pacobj.Result.Trim().Replace("notpass_", "notcomplete_");

                    //LogResponse: Insert Log Response
                    logobj.AddWSLog(pacobj.PartnerName, ipaddress, "Response", "NETWS_ForPartner", "PartnerSetBookSmileActivityWithSmilePassword", obj.fld_result + "|" + pacobj.PartnerName + "|" + fld_client_number + "|" + obj.fld_sessionID, refnum);

                    return obj;
                }
            }
        }
        catch (Exception ex)
        {
            //LogRequest: Insert Log Request
            logobj.AddWSLog(fld_partner_username, ipaddress, "Request", "NETWS_ForPartner", "PartnerSetBookSmileActivityWithSmilePassword", fld_partner_username + "|" + fld_partner_password + "|" + fld_client_number + "|" + fld_smile_password + "|" + fld_activity_id + "|" + fld_book_no + "|" + fld_mobile_phone_number + "|" + fld_home_phone_number + "|" + fld_home_phone_number_ext + "|" + fld_office_phone_number + "|" + fld_office_phone_number_ext + "|" + fld_smile_branch_code + "|" + fld_request_branch_code + "|" + fld_request_branch_name, refnum);

            obj.fld_result = "notcomplete_" + ex.ToString();

            //LogResponse: Insert Log Response
            logobj.AddWSLog(fld_partner_username, ipaddress, "Response", "NETWS_ForPartner", "PartnerSetBookSmileActivityWithSmilePassword", obj.fld_result + "|" + obj.fld_sessionID, refnum);

            return obj;
        }
    }

    [WebMethod(Description = "สำหรับ Model 4 ใช้รับสิทธิ์ โดยใช้คะแนนสะสม Smile Point แลกรับสิทธิ์ One Request Per Many Receive Codes")]
    public SetBookSmileActivity_NEW_Result PartnerSetBookSmileActivityWithSmilePasswordReturnManyReceiveCode(string fld_partner_username, string fld_partner_password, string fld_client_number, string fld_smile_password, string fld_activity_id, string fld_book_no, string fld_mobile_phone_number, string fld_home_phone_number, string fld_home_phone_number_ext, string fld_office_phone_number, string fld_office_phone_number_ext, string fld_smile_branch_code, string fld_request_branch_code, string fld_request_branch_name, string fld_additional_amount)
    {
        SetBookSmileActivity_NEW_Result obj = new SetBookSmileActivity_NEW_Result();
        RefRunningTBBLL runningobj = new RefRunningTBBLL();
        refnum = runningobj.AddRefRunningTBAndReturn();
        WSLogBLL logobj = new WSLogBLL();

        try
        {
            //ตรวจสอบค่าที่จำเป็นต้องส่งมาให้ครบถ้วนก่อน
            if (fld_partner_username == "" || fld_partner_password == "" || fld_client_number == "" || fld_smile_password == "" || fld_activity_id == "" || fld_book_no == "" || fld_smile_branch_code == "")
            {
                //LogRequest: Insert Log Request
                logobj.AddWSLog(fld_partner_username, ipaddress, "Request", "NETWS_ForPartner", "PartnerSetBookSmileActivityWithSmilePasswordReturnManyReceiveCode", fld_partner_username + "|" + fld_partner_password + "|" + fld_client_number + "|" + fld_smile_password + "|" + fld_activity_id + "|" + fld_book_no + "|" + fld_mobile_phone_number + "|" + fld_home_phone_number + "|" + fld_home_phone_number_ext + "|" + fld_office_phone_number + "|" + fld_office_phone_number_ext + "|" + fld_smile_branch_code + "|" + fld_request_branch_code + "|" + fld_request_branch_name, refnum);

                obj.fld_result = "notcomplete_กรุณาระบุ Partner Username หรือ Partner Password หรือ Client Number หรือ Smile Password หรือ Activity ID หรือ Book No หรือ Smile Branch Code ให้ถูกต้อง";

                //LogResponse: Insert Log Response
                logobj.AddWSLog(fld_partner_username, ipaddress, "Response", "NETWS_ForPartner", "PartnerSetBookSmileActivityWithSmilePasswordReturnManyReceiveCode", obj.fld_result + "|" + fld_partner_username + "|" + fld_client_number + "|" + obj.fld_sessionID, refnum);

                return obj;
            }
            else
            {
                //พิสูจน์ตัวตนของพันธมิตรก่อนที่จะให้ใช้งานจริง
                NETWS_ForPartnerAuthenticationChecking.CheckPartnerAuthentication_Result pacobj = CheckPartnerAuthenticationReturnDetail(fld_partner_username, fld_partner_password, ipaddress);
                if (pacobj.Result.Trim().ToLower() == "passed")
                {
                    try
                    {
                        //LogRequest: Insert Log Request
                        logobj.AddWSLog(pacobj.PartnerName, ipaddress, "Request", "NETWS_ForPartner", "PartnerSetBookSmileActivityWithSmilePasswordReturnManyReceiveCode", fld_partner_username + "|" + fld_partner_password + "|" + fld_client_number + "|" + fld_smile_password + "|" + fld_activity_id + "|" + fld_book_no + "|" + fld_mobile_phone_number + "|" + fld_home_phone_number + "|" + fld_home_phone_number_ext + "|" + fld_office_phone_number + "|" + fld_office_phone_number_ext + "|" + fld_smile_branch_code + "|" + fld_request_branch_code + "|" + fld_request_branch_name, refnum);

                        //SetBookSmileActivity_NEW_Result mtlwsobj = MTLSetBookSmileActivity_NEW(fld_client_number, fld_activity_id, fld_book_no, fld_mobile_phone_number, fld_home_phone_number, fld_home_phone_number_ext, fld_office_phone_number, fld_office_phone_number_ext, fld_smile_branch_code, fld_request_branch_code, fld_request_branch_name, fld_additional_amount);
                        //obj.fld_result = mtlwsobj.fld_result;
                        //obj.fld_sessionID = mtlwsobj.fld_sessionID;

                        //ใช้เพื่อกำหนดค่าสำหรับ Output ที่เฉพาะเจาะจงให้กับแต่ละพันธมิตร
                        switch (pacobj.PartnerName.Trim())
                        {
                            case "i-wiz":
                                //ตรวจสอบเงื่อนไขต่างๆ ตามความต้องการ
                                GetCustomerDetail_Result customerwsobj = MTLGetCustomerDetail(fld_client_number);
                                if (customerwsobj.fld_result.Trim() == "notfound")
                                {
                                    //obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ เลขที่ประจำตัวของท่านไม่ได้รับสิทธิ์ กรุณาติดต่อ 1766 กด 4 ค่ะ";
                                    obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ เลขที่ประจำตัวของท่านไม่ได้รับสิทธิ์ กรุณาติดต่อ 1766 กด4ค่ะ";
                                }
                                else if (customerwsobj.fld_result.Trim() == "notfound_ข้อมูลยังรันไม่เสร็จ")
                                {
                                    //obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ ระบบยังไม่สามารถให้บริการได้ในขณะนี้ กรุณาติดต่อ 1766 กด 4 ค่ะ";
                                    //20130908: นิวแจ้งแก้ไข 26/08/2013
                                    obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ ระบบไม่สามารถให้บริการได้ในขณะนี้ กรุณาติดต่อ 1766 กด 4 ค่ะ";
                                }
                                else if (customerwsobj.fld_client_isSmileClubMember.Trim() != "Y" && customerwsobj.fld_client_isSmileClubMember != "S")
                                {
                                    obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ เลขที่ประจำตัวของท่านไม่ได้รับสิทธิ์ กรุณาติดต่อ 1766 กด4ค่ะ";
                                }
                                //20130917: นิวแจ้งแก้ไข เฉพาะ Model 3 ไม่ต้องตรวจเช็คเบอร์โทรว่าตรงกันหรือไม่ 17/09/2013
                                //else if (customerwsobj.fld_result.Trim() == "found" && (customerwsobj.fld_mobile_phone_number_SMC.Trim() != fld_mobile_phone_number.Trim()))
                                //{
                                //    //obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ เบอร์โทรศัพท์ของท่านไม่ได้รับสิทธิ์ กรุณาติดต่อ 1766 กด 4 ค่ะ";
                                //    obj.fld_result = "notcomplete_MSG_เบอร์มือถือไม่ตรงกับเลขที่ประจำตัวของท่านในระบบ กรุณาติดต่อ1766กด4ค่ะ";
                                //}
                                else
                                {
                                    CheckSmilePassword_Result smilepasswordwsobj = MTLCheckSmilePassword(fld_client_number, fld_smile_password, "");
                                    if (smilepasswordwsobj.fld_result.Trim() == "notpass_01_เลขประจำตัวไม่ถูกต้อง")
                                    {
                                        //obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ เลขที่ประจำตัวของท่านไม่ได้รับสิทธิ์ กรุณาติดต่อ 1766 กด 4 ค่ะ";
                                        obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ เลขที่ประจำตัวของท่านไม่ได้รับสิทธิ์ กรุณาติดต่อ 1766 กด4ค่ะ";
                                    }
                                    else if (smilepasswordwsobj.fld_result.Trim() == "notpass_02_รหัสผ่านส่วนตัวไม่ถูกต้อง")
                                    {
                                        obj.fld_result = "notcomplete_MSG_เลขประจำตัวและรหัสผ่านของท่านไม่ตรงกับในระบบ กรุณาติดต่อ 1766 กด 4 ค่ะ";
                                    }
                                    else if (smilepasswordwsobj.fld_result.Trim() == "notpass_06_เลขประจำตัวนี้ยังไม่ได้เป็นสมาชิก")
                                    {
                                        //obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ เลขที่ประจำตัวของท่านไม่ได้รับสิทธิ์ กรุณาติดต่อ 1766 กด 4 ค่ะ";
                                        obj.fld_result = "notcomplete_MSG_ขออภัยค่ะ เลขที่ประจำตัวของท่านไม่ได้รับสิทธิ์ กรุณาติดต่อ 1766 กด4ค่ะ";
                                    }
                                    else if (smilepasswordwsobj.fld_result.Trim() == "notpass_04_ยังไม่ได้ทำการActivation")
                                    {
                                        //obj.fld_result = "notcomplete_MSG_เลขประจำตัวและรหัสผ่านของท่านไม่ตรงกับในระบบ กรุณาติดต่อ 1766 กด 4 ค่ะ";
                                        //20130908: นิวแจ้งแก้ไข 26/08/2013
                                        obj.fld_result = "notcomplete_MSG_ท่านยังไม่ได้ลงทะเบียนเปิดบัตร Smile Club กรุณาติดต่อ 1766 กด 4 ค่ะ";
                                    }
                                    //20131014: เสมแจ้งแก้ไข 14/10/2556 เฉพาะ Model 3 ไม่ต้องเช็ค error message notpass_03_บัตรประจำตัวนี้ถูกระงับการใช้งาน
                                    //else if (smilepasswordwsobj.fld_result.Trim() == "notpass_03_บัตรประจำตัวนี้ถูกระงับการใช้งาน")
                                    //{
                                    //    obj.fld_result = "notcomplete_MSG_เลขประจำตัวและรหัสผ่านของท่านไม่ตรงกับในระบบ กรุณาติดต่อ 1766 กด 4 ค่ะ";
                                    //}
                                    else
                                    {
                                        /* ===== เช็คจำนวนว่าต้องการกี่สิทธิ์แล้วจึงส่งไป AS400 เท่านั้น และนำ Output(s) มาแสดงใน 1 ข้อความ ===== */
                                        int vBookNo = Convert.ToInt32(fld_book_no);

                                        string v_result = "";
                                        string v_sessionID = "";
                                        string v_smile_point_before_booked = "";
                                        string v_smile_point_after_booked = "";
                                        string v_point = "";
                                        string v_booked_no = "";
                                        string v_get_discount_amount = "";
                                        string v_get_discount_percent = "";
                                        string v_get_discount_from = "";
                                        string v_receive_code = "";
                                        string v_comment = "";

                                        // ทำการแลกสิทธิ์ตามจำนวนที่ต้องการแลก (fld_book_no) ทีละ 1 สิทธิ์
                                        List<SetBookSmileActivity_NEW_Result> oplist = new List<SetBookSmileActivity_NEW_Result>();
                                        for (int i = 0; i < vBookNo; i++)
                                        {
                                            SetBookSmileActivity_NEW_Result mtlwsobj = MTLSetBookSmileActivity_NEW(fld_client_number, fld_activity_id, "1", fld_mobile_phone_number, fld_home_phone_number, fld_home_phone_number_ext, fld_office_phone_number, fld_office_phone_number_ext, fld_smile_branch_code, fld_request_branch_code, fld_request_branch_name, fld_additional_amount);
                                            //v_result = mtlwsobj.fld_result;
                                            //v_sessionID = mtlwsobj.fld_sessionID;

                                            oplist.Add(mtlwsobj);
                                        }

                                        // เอาผลการแลกสิทธิ์มาตรวจสอบ แล้วเลือกเฉพาะที่ได้รับสิทธิ์ และเอารหัสรับสิทธิ์มา concate กัน
                                        bool vPreviousResultIsCompleted = false;
                                        foreach (SetBookSmileActivity_NEW_Result item in oplist)
                                        {
                                            if (item.fld_result == "completed")
                                            {
                                                v_result = item.fld_result;
                                                v_sessionID = item.fld_sessionID;

                                                v_receive_code += item.fld_receive_code + ",";

                                                v_smile_point_before_booked = item.fld_smile_point_before_booked;
                                                v_smile_point_after_booked = item.fld_smile_point_after_booked;
                                                v_point = item.fld_point;
                                                v_booked_no = item.fld_booked_no;
                                                v_get_discount_amount = item.fld_get_discount_amount;
                                                v_get_discount_percent = item.fld_get_discount_percent;
                                                v_get_discount_from = item.fld_get_discount_from;
                                                v_comment = item.fld_comment;

                                                vPreviousResultIsCompleted = true;
                                            }
                                            else
                                            {
                                                if (vPreviousResultIsCompleted == true)
                                                {

                                                }
                                                else
                                                {
                                                    v_result = item.fld_result;
                                                    v_sessionID = item.fld_sessionID;

                                                    vPreviousResultIsCompleted = false;
                                                }
                                            }
                                        }

                                        // เอารหัสรับสิทธิ์ที่ได้ concate กัน มาตรวจสอบ และประกอบกับ wording ที่กำหนด
                                        if (!String.IsNullOrEmpty(v_receive_code.Trim()))
                                        {
                                            v_receive_code = v_receive_code.Substring(0, v_receive_code.Length - 1);

                                            if (Convert.ToInt32(fld_book_no.Trim()) >= 2) // ขอมาตั้งแต่ 2 สิทธิ์ขึ้นไป
                                            {
                                                if ((v_receive_code.Split(',').Length - 1) > 0)
                                                {
                                                    //20141001: เสมแจ้งแก้ไข 24/09/2014 รองรับแคมเปญ Belle Villa Resort เบลล์วิลล่า เชียงใหม่ (57000100)
                                                    if (fld_activity_id == "57000100")
                                                    {
                                                        //v_receive_code = "2 สิทธิ์ " + v_receive_code.Substring(0, v_receive_code.LastIndexOf(",")) + " และ" + v_receive_code.Substring(v_receive_code.LastIndexOf(",") + 1, v_receive_code.Length - v_receive_code.LastIndexOf(",") - 1) + " ใช้ได้ถึง 01/10/2558";
                                                        //20150313: เสมแจ้งแก้ไข 13/03/2015 เปลี่ยนเป็น 30/11/2558
                                                        v_receive_code = "2 สิทธิ์ " + v_receive_code.Substring(0, v_receive_code.LastIndexOf(",")) + " และ" + v_receive_code.Substring(v_receive_code.LastIndexOf(",") + 1, v_receive_code.Length - v_receive_code.LastIndexOf(",") - 1) + " ใช้ได้ถึง 30/11/2558";
                                                    }
                                                    //20141001: เสมแจ้งแก้ไข 24/09/2014 รองรับแคมเปญ Belle Villa Resort เบลล์วิลล่า ปาย (57000200)
                                                    else if (fld_activity_id == "57000200")
                                                    {
                                                        //v_receive_code = "2 สิทธิ์ " + v_receive_code.Substring(0, v_receive_code.LastIndexOf(",")) + " และ" + v_receive_code.Substring(v_receive_code.LastIndexOf(",") + 1, v_receive_code.Length - v_receive_code.LastIndexOf(",") - 1) + " ใช้ได้ถึง 01/10/2558";
                                                        //20150313: เสมแจ้งแก้ไข 13/03/2015 เปลี่ยนเป็น 30/11/2558
                                                        v_receive_code = "2 สิทธิ์ " + v_receive_code.Substring(0, v_receive_code.LastIndexOf(",")) + " และ" + v_receive_code.Substring(v_receive_code.LastIndexOf(",") + 1, v_receive_code.Length - v_receive_code.LastIndexOf(",") - 1) + " ใช้ได้ถึง 30/11/2558";
                                                    }
                                                    //20141001: เสมแจ้งแก้ไข 24/09/2014 รองรับแคมเปญ Belle Villa Resort เบลล์วิลล่า เขาใหญ่ (57000300)
                                                    else if (fld_activity_id == "57000300")
                                                    {
                                                        //v_receive_code = "2 สิทธิ์ " + v_receive_code.Substring(0, v_receive_code.LastIndexOf(",")) + " และ" + v_receive_code.Substring(v_receive_code.LastIndexOf(",") + 1, v_receive_code.Length - v_receive_code.LastIndexOf(",") - 1) + " ใช้ได้ถึง 01/10/2558";
                                                        //20150313: เสมแจ้งแก้ไข 13/03/2015 เปลี่ยนเป็น 30/11/2558
                                                        v_receive_code = "2 สิทธิ์ " + v_receive_code.Substring(0, v_receive_code.LastIndexOf(",")) + " และ" + v_receive_code.Substring(v_receive_code.LastIndexOf(",") + 1, v_receive_code.Length - v_receive_code.LastIndexOf(",") - 1) + " ใช้ได้ถึง 30/11/2558";
                                                    }
                                                    //20141001: เสมแจ้งแก้ไข 24/09/2014 รองรับแคมเปญ CP Fresh Mart ส่วนลด 30 บาท (57050000)
                                                    else if (fld_activity_id == "57050000")
                                                    {
                                                        v_receive_code = "2 สิทธิ์ " + v_receive_code.Substring(0, v_receive_code.LastIndexOf(",")) + " และ" + v_receive_code.Substring(v_receive_code.LastIndexOf(",") + 1, v_receive_code.Length - v_receive_code.LastIndexOf(",") - 1) + "ค่ะ";
                                                    }
                                                    //20141001: เสมแจ้งแก้ไข 24/09/2014 รองรับแคมเปญ CP Fresh Mart ส่วนลด 100 บาท (57060000)
                                                    else if (fld_activity_id == "57060000")
                                                    {
                                                        v_receive_code = "2 สิทธิ์ " + v_receive_code.Substring(0, v_receive_code.LastIndexOf(",")) + " และ" + v_receive_code.Substring(v_receive_code.LastIndexOf(",") + 1, v_receive_code.Length - v_receive_code.LastIndexOf(",") - 1) + "ค่ะ";
                                                    }
                                                    //20150131: เสมแจ้งแก้ไข 27/01/2015 รองรับแคมเปญ Smile Movie Day9 - ตั๋วหนัง SF (58120000)
                                                    else if (fld_activity_id == "58120000")
                                                    {
                                                        v_receive_code = v_receive_code.Substring(0, v_receive_code.LastIndexOf(",")) + " และ" + v_receive_code.Substring(v_receive_code.LastIndexOf(",") + 1, v_receive_code.Length - v_receive_code.LastIndexOf(",") - 1) + "ค่ะ";
                                                    }
                                                    //20150313: เสมแจ้งแก้ไข 13/03/2015 รองรับแคมเปญ Happy Dee Happy Set (McDonald's) - Mc ชุดอร่อยสุดคุ้ม (58600000)
                                                    else if (fld_activity_id == "58600000")
                                                    {
                                                        v_receive_code = v_receive_code.Substring(0, v_receive_code.LastIndexOf(",")) + " และ" + v_receive_code.Substring(v_receive_code.LastIndexOf(",") + 1, v_receive_code.Length - v_receive_code.LastIndexOf(",") - 1) + "ค่ะ";
                                                    }
                                                    //20150313: เสมแจ้งแก้ไข 13/03/2015 รองรับแคมเปญ Happy Dee Happy Set (McDonald's) - Mc ชุด Happy Meal (58610000)
                                                    else if (fld_activity_id == "58610000")
                                                    {
                                                        v_receive_code = v_receive_code.Substring(0, v_receive_code.LastIndexOf(",")) + " และ" + v_receive_code.Substring(v_receive_code.LastIndexOf(",") + 1, v_receive_code.Length - v_receive_code.LastIndexOf(",") - 1) + "ค่ะ";
                                                    }
                                                    else
                                                    {
                                                        v_receive_code = v_receive_code.Substring(0, v_receive_code.LastIndexOf(",")) + " และ" + v_receive_code.Substring(v_receive_code.LastIndexOf(",") + 1, v_receive_code.Length - v_receive_code.LastIndexOf(",") - 1) + "ค่ะ";
                                                    }
                                                }
                                                else
                                                {
                                                    //20141001: เสมแจ้งแก้ไข 24/09/2014 รองรับแคมเปญ Belle Villa Resort เบลล์วิลล่า เชียงใหม่ (57000100)
                                                    if (fld_activity_id == "57000100")
                                                    {
                                                        //v_receive_code = "ได้ 1 สิทธิ์เท่านั้นค่ะ " + v_receive_code + " ใช้ได้ถึง 01/10/2558";
                                                        //20150313: เสมแจ้งแก้ไข 13/03/2015 เปลี่ยนเป็น 30/11/2558
                                                        v_receive_code = "ได้ 1 สิทธิ์เท่านั้นค่ะ " + v_receive_code + " ใช้ได้ถึง 30/11/2558";
                                                    }
                                                    //20141001: เสมแจ้งแก้ไข 24/09/2014 รองรับแคมเปญ Belle Villa Resort เบลล์วิลล่า ปาย (57000200)
                                                    else if (fld_activity_id == "57000200")
                                                    {
                                                        //v_receive_code = "ได้ 1 สิทธิ์เท่านั้นค่ะ " + v_receive_code + " ใช้ได้ถึง 01/10/2558";
                                                        //20150313: เสมแจ้งแก้ไข 13/03/2015 เปลี่ยนเป็น 30/11/2558
                                                        v_receive_code = "ได้ 1 สิทธิ์เท่านั้นค่ะ " + v_receive_code + " ใช้ได้ถึง 30/11/2558";
                                                    }
                                                    //20141001: เสมแจ้งแก้ไข 24/09/2014 รองรับแคมเปญ Belle Villa Resort เบลล์วิลล่า เขาใหญ่ (57000300)
                                                    else if (fld_activity_id == "57000300")
                                                    {
                                                        //v_receive_code = "ได้ 1 สิทธิ์เท่านั้นค่ะ " + v_receive_code + " ใช้ได้ถึง 01/10/2558";
                                                        //20150313: เสมแจ้งแก้ไข 13/03/2015 เปลี่ยนเป็น 30/11/2558
                                                        v_receive_code = "ได้ 1 สิทธิ์เท่านั้นค่ะ " + v_receive_code + " ใช้ได้ถึง 30/11/2558";
                                                    }
                                                    //20141001: เสมแจ้งแก้ไข 24/09/2014 รองรับแคมเปญ CP Fresh Mart ส่วนลด 30 บาท (57050000)
                                                    else if (fld_activity_id == "57050000")
                                                    {
                                                        v_receive_code = "ได้ 1 สิทธิ์เท่านั้นค่ะ " + v_receive_code;
                                                    }
                                                    //20141001: เสมแจ้งแก้ไข 24/09/2014 รองรับแคมเปญ CP Fresh Mart ส่วนลด 100 บาท (57060000)
                                                    else if (fld_activity_id == "57060000")
                                                    {
                                                        v_receive_code = "ได้ 1 สิทธิ์เท่านั้นค่ะ " + v_receive_code;
                                                    }
                                                    //20150131: เสมแจ้งแก้ไข 27/01/2015 รองรับแคมเปญ Smile Movie Day9 - ตั๋วหนัง SF (58120000)
                                                    else if (fld_activity_id == "58120000")
                                                    {
                                                        v_receive_code = v_receive_code + "ได้ 1 สิทธิ์เท่านั้นค่ะ";
                                                    }
                                                    //20150313: เสมแจ้งแก้ไข 13/03/2015 รองรับแคมเปญ Happy Dee Happy Set (McDonald's) - Mc ชุดอร่อยสุดคุ้ม (58600000)
                                                    else if (fld_activity_id == "58600000")
                                                    {
                                                        v_receive_code = v_receive_code + "ได้ 1 สิทธิ์เท่านั้นค่ะ";
                                                    }
                                                    //20150313: เสมแจ้งแก้ไข 13/03/2015 รองรับแคมเปญ Happy Dee Happy Set (McDonald's) - Mc ชุด Happy Meal (58610000)
                                                    else if (fld_activity_id == "58610000")
                                                    {
                                                        v_receive_code = v_receive_code + "ได้ 1 สิทธิ์เท่านั้นค่ะ";
                                                    }
                                                    else
                                                    {
                                                        v_receive_code = v_receive_code + "ได้ 1 สิทธิ์เท่านั้นค่ะ";
                                                    }
                                                }
                                            }
                                            else if (Convert.ToInt32(fld_book_no.Trim()) == 1) // ขอมาแค่ 1 สิทธิ์เท่านั้น
                                            {
                                                //20141001: เสมแจ้งแก้ไข 24/09/2014 รองรับแคมเปญ Belle Villa Resort เบลล์วิลล่า เชียงใหม่ (57000100)
                                                if (fld_activity_id == "57000100")
                                                {
                                                    //v_receive_code = "1 สิทธิ์ " + v_receive_code + " ใช้ได้ถึง 01/10/2558";
                                                    //20150313: เสมแจ้งแก้ไข 13/03/2015 เปลี่ยนเป็น 30/11/2558
                                                    v_receive_code = "1 สิทธิ์ " + v_receive_code + " ใช้ได้ถึง 30/11/2558";
                                                }
                                                //20141001: เสมแจ้งแก้ไข 24/09/2014 รองรับแคมเปญ Belle Villa Resort เบลล์วิลล่า ปาย (57000200)
                                                else if (fld_activity_id == "57000200")
                                                {
                                                    //v_receive_code = "1 สิทธิ์ " + v_receive_code + " ใช้ได้ถึง 01/10/2558";
                                                    //20150313: เสมแจ้งแก้ไข 13/03/2015 เปลี่ยนเป็น 30/11/2558
                                                    v_receive_code = "1 สิทธิ์ " + v_receive_code + " ใช้ได้ถึง 30/11/2558";
                                                }
                                                //20141001: เสมแจ้งแก้ไข 24/09/2014 รองรับแคมเปญ Belle Villa Resort เบลล์วิลล่า เขาใหญ่ (57000300)
                                                else if (fld_activity_id == "57000300")
                                                {
                                                    //v_receive_code = "1 สิทธิ์ " + v_receive_code + " ใช้ได้ถึง 01/10/2558";
                                                    //20150313: เสมแจ้งแก้ไข 13/03/2015 เปลี่ยนเป็น 30/11/2558
                                                    v_receive_code = "1 สิทธิ์ " + v_receive_code + " ใช้ได้ถึง 30/11/2558";
                                                }
                                                //20141001: เสมแจ้งแก้ไข 24/09/2014 รองรับแคมเปญ CP Fresh Mart ส่วนลด 30 บาท (57050000)
                                                else if (fld_activity_id == "57050000")
                                                {
                                                    v_receive_code = "1 สิทธิ์ " + v_receive_code + "ค่ะ";
                                                }
                                                //20141001: เสมแจ้งแก้ไข 24/09/2014 รองรับแคมเปญ CP Fresh Mart ส่วนลด 100 บาท (57060000)
                                                else if (fld_activity_id == "57060000")
                                                {
                                                    v_receive_code = "1 สิทธิ์ " + v_receive_code + "ค่ะ";
                                                }
                                                //20150131: เสมแจ้งแก้ไข 27/01/2015 รองรับแคมเปญ Smile Movie Day9 - ตั๋วหนัง SF (58120000)
                                                else if (fld_activity_id == "58120000")
                                                {
                                                    v_receive_code = v_receive_code + "ค่ะ";
                                                }
                                                //20150313: เสมแจ้งแก้ไข 13/03/2015 รองรับแคมเปญ Happy Dee Happy Set (McDonald's) - Mc ชุดอร่อยสุดคุ้ม (58600000)
                                                else if (fld_activity_id == "58600000")
                                                {
                                                    v_receive_code = v_receive_code + "ค่ะ";
                                                }
                                                //20150313: เสมแจ้งแก้ไข 13/03/2015 รองรับแคมเปญ Happy Dee Happy Set (McDonald's) - Mc ชุด Happy Meal (58610000)
                                                else if (fld_activity_id == "58610000")
                                                {
                                                    v_receive_code = v_receive_code + "ค่ะ";
                                                }
                                                else
                                                {
                                                    v_receive_code = v_receive_code + "ค่ะ";
                                                }
                                            }
                                            else // TODO: Default
                                            {
                                                v_receive_code = v_receive_code + "ค่ะ";
                                            }
                                        }

                                        switch (v_result)
                                        {
                                            case "notcomplete_ไม่มีข้อมูลสาขานี้":
                                                v_result = "";
                                                break;
                                            case "notcomplete_กรุณาระบุรหัสกิจกรรม":
                                                v_result = "notcomplete_MSG_ขออภัยค่ะ รหัสกิจกรรมไม่ถูกต้อง กรุณาตรวจสอบและส่งใหม่อีกครั้งค่ะ";
                                                break;
                                            case "notcomplete_รหัสกิจกรรมไม่ถูกต้อง":
                                                v_result = "notcomplete_MSG_ขออภัยค่ะ รหัสกิจกรรมไม่ถูกต้อง กรุณาตรวจสอบและส่งใหม่อีกครั้งค่ะ";
                                                break;
                                            case "notcomplete_ไม่พบกิจกรรมในสาขานี้":
                                                v_result = "";
                                                break;
                                            case "notcomplete_จำนวนรางวัลไม่พอสำหรับการแลก":
                                                v_result = "notcomplete_MSG_ขออภัยค่ะ มีผู้รับสิทธิ์เต็มจำนวนแล้ว ขอบคุณที่ให้ความสนใจค่ะ";
                                                break;
                                            case "notpass_มี SUB กิจกรรม":
                                                v_result = "notcomplete_MSG_ขออภัยค่ะ รหัสกิจกรรมไม่ถูกต้อง กรุณาตรวจสอบและส่งใหม่อีกครั้งค่ะ";
                                                break;
                                            case "notcomplete_ไม่ได้เป็นสมาชิกSmileClub":
                                                //v_result = "notcomplete_MSG_ขออภัยค่ะ เลขที่ประจำตัวของท่านไม่ได้รับสิทธิ์ กรุณาติดต่อ 1766 กด 4 ค่ะ";
                                                v_result = "notcomplete_MSG_ขออภัยค่ะ เลขที่ประจำตัวของท่านไม่ได้รับสิทธิ์ กรุณาติดต่อ 1766 กด4ค่ะ";
                                                break;
                                            case "notcomplete_ยังไม่เปิดสิทธิในการแลก":
                                                v_result = "notcomplete_MSG_ขออภัยค่ะ คุณส่งรหัสในช่วงเวลานอกเหนือจากการได้รับสิทธิ์ ขอบคุณค่ะ";
                                                break;
                                            case "notcomplete_ไม่มีข้อมูลลูกค้า":
                                                //v_result = "notcomplete_MSG_ขออภัยค่ะ เลขที่ประจำตัวของท่านไม่ได้รับสิทธิ์ กรุณาติดต่อ 1766 กด 4 ค่ะ";
                                                v_result = "notcomplete_MSG_ขออภัยค่ะ เลขที่ประจำตัวของท่านไม่ได้รับสิทธิ์ กรุณาติดต่อ 1766 กด4ค่ะ";
                                                break;
                                            case "notcomplete_ไม่มีข้อมูลคะแนนสะสม":
                                                //v_result = "notcomplete_MSG_ขออภัยค่ะ คะแนนสะสม Smile Point คงเหลือของคุณมีไม่พอสำหรับแลกสิทธิ์ ขณะนี้คุณมีคะแนนสะสมคงเหลือ 0 คะแนนค่ะ สอบถามติดต่อ 1766 กด 4 ค่ะ";
                                                //20130908: นิวแจ้งแก้ไข 26/08/2013
                                                v_result = "notcomplete_MSG_คะแนนสะสมคงเหลือของคุณคือ 0 คะแนนค่ะ แจ้งเวลา " + GetCurrentDateTime();
                                                break;
                                            case "notpass_กรุณาระบุจำนวนเงิน":
                                                v_result = "";
                                                break;
                                            case "notcomplete_กรุณาระบุผู้มาแลกรับ":
                                                v_result = "";
                                                break;
                                            case "notcomplete_ไม่ได้อยู่ในระยะเวลาการแลกคะแนน":
                                                //v_result = "notcomplete_MSG_ขออภัยค่ะ คุณส่งรหัสในช่วงเวลานอกเหนือจากการได้สิทธิ์ ขอบคุณค่ะ";
                                                //20130908: นิวแจ้งแก้ไข 26/08/2013
                                                v_result = "notcomplete_MSG_ขออภัยค่ะ คุณส่งรหัสในช่วงเวลานอกเหนือจากการได้รับสิทธิ์ ขอบคุณค่ะ";
                                                break;
                                            case "notcomplete_จำนวนรางวัลเกินกว่าที่กำหนด":
                                                v_result = "notcomplete_MSG_ขออภัยค่ะ จำนวนสิทธิ์ที่คุณต้องการแลกเกินกว่าเงื่อนไขที่กำหนดค่ะ";
                                                break;
                                            case "notcomplete_กรุณาใส่จำนวนรางวัล":
                                                //v_result = "notcomplete_MSG_ขออภัยค่ะ จำนวนสิทธิ์ที่คุณต้องการแลกเกินกว่าเงื่อนไขที่กำหนดค่ะ";
                                                v_result = "notcomplete_MSG_ขออภัยค่ะ กรุณาระบุจำนวนสิทธิ์ที่คุณต้องการแลกด้วยค่ะ";
                                                break;
                                            case "notcomplete_พนักงานหรือตัวแทนไม่สามารถแลกได้ในขณะนี้":
                                                //v_result = "notcomplete_MSG_ขออภัยค่ะ พนักงานหรือตัวแทนไม่สามารถรับสิทธิ์ได้ในขณะนี้ค่ะ";
                                                v_result = "notcomplete_MSG_ขออภัยค่ะ พนักงานหรือตัวแทนไม่สามารถรับสิทธิ์นี้ได้ค่ะ";
                                                break;
                                            case "notcomplete_จำนวนคะแนนไม่เพียงพอ":
                                                //v_result = "notcomplete_MSG_ขออภัยค่ะ คะแนนสะสม Smile Point คงเหลือของคุณมีไม่พอสำหรับแลกรับสิทธิ์ ขณะนี้คุณมีคะแนนสะสมคงเหลือ " + customerwsobj.fld_smile_point + " คะแนนค่ะ สอบถามติดต่อ 1766 กด 4 ค่ะ";
                                                //v_result = "notcomplete_MSG_คุณมีคะแนนสะสมไม่เพียงพอ คงเหลือ " + customerwsobj.fld_smile_point + " คะแนน แจ้ง " + GetCurrentDateTime();
                                                //20131014: เสมแจ้งแก้ไข 14/10/2556
                                                v_result = "notcomplete_MSG_คุณมีคะแนนสะสมไม่เพียงพอ คงเหลือ " + customerwsobj.fld_smile_point.Trim() + " คะแนน แจ้ง " + GetCurrentDateTime();
                                                break;
                                            case "notcomplete_แลกเกินจำนวน":
                                                //v_result = "notcomplete_MSG_ขออภัยค่ะ คุณได้รับสิทธิ์ครบตามเงื่อนไขแล้ว ขอบคุณที่ให้ความสนใจค่ะ";
                                                //20130909: นิวแจ้งแก้ไข 09/09/2013
                                                v_result = "notcomplete_MSG_กิจกรรมนี้สามารถแลกได้ 1 สิทธิ์ต่อสัปดาห์ (จันทร์-อาทิตย์) เท่านั้นค่ะ";
                                                break;
                                            case "notcomplete_แลกเกินจำนวนต่อวัน":
                                                //v_result = "notcomplete_MSG_ขออภัยค่ะ คุณได้รับสิทธิ์ครบตามเงื่อนไขแล้ว ขอบคุณที่ให้ความสนใจค่ะ";
                                                //20130909: นิวแจ้งแก้ไข 09/09/2013
                                                //v_result = "notcomplete_MSG_กิจกรรมนี้สามารถแลกได้ 1 สิทธิ์ต่อสัปดาห์ (จันทร์-อาทิตย์) เท่านั้นค่ะ";
                                                //20140228: นิวแจ้งแก้ไข 27/02/2014
                                                //v_result = "notcomplete_MSG_ขออภัยค่ะ คุณขอใช้สิทธิ์เกินกว่าจำนวนสิทธิ์ต่อวันที่เงื่อนไขกำหนดค่ะ";
                                                //20140314: นิวแจ้งแก้ไข 14/03/2014 รองรับ B-Quik (แบบรับสิทธิ์ฟรี 55990000, แบบแลกคะแนนรับสิทธิ์ 56000000) ใช้พร้อมกับ Siam Future
                                                //if (fld_activity_id == "55990000")
                                                //{
                                                //    v_result = "notcomplete_MSG_ขออภัยค่ะ คุณขอใช้สิทธิ์เกินกว่าจำนวนสิทธิ์ที่เงื่อนไขกำหนดค่ะ";
                                                //}
                                                //else
                                                //{
                                                //    v_result = "notcomplete_MSG_ขออภัยค่ะ คุณขอใช้สิทธิ์เกินกว่าจำนวนสิทธิ์ต่อวันที่เงื่อนไขกำหนดค่ะ";
                                                //}
                                                //20140502: นิวแจ้งแก้ไข 30/04/2014 สำหรับแคมเปญ Major + SF (ตั๋วดูหนังและป๊อปคอร์น)
                                                //20140502: เสมแจ้งแก้ไข (โทรคุยกับเสม 02/05/2014) เสมบอกว่าจริงๆ i-wiz จะดักตั้งแต่ตอนแรกอยู่แล้ว ดังนั้นให้ใช้ข้อความดิม
                                                //v_result = "notcomplete_MSG_คุณพิมพ์ไม่ถูกต้องตามรูปแบบที่กำหนด กรุณาตรวจสอบและส่งใหม่อีกครั้งค่ะ";

                                                //20141001: เสมแจ้งแก้ไข 24/09/2014 รองรับแคมเปญ Belle Villa Resort (57000100,57000200,57000300)
                                                if (fld_activity_id == "57000100" || fld_activity_id == "57000200" || fld_activity_id == "57000300")
                                                {
                                                    v_result = "notcomplete_MSG_กิจกรรมนี้ตลอดทั้งโครงการสามารถแลกได้สูงสุด 2 สิทธิ์ต่อท่านเท่านั้นค่ะ";
                                                }
                                                //20141001: เสมแจ้งแก้ไข 24/09/2014 รองรับแคมเปญ CP Fresh Mart (57050000,57060000)
                                                else if (fld_activity_id == "57050000" || fld_activity_id == "57060000")
                                                {
                                                    v_result = "notcomplete_MSG_กิจกรรมนี้แต่ละแคมเปญแลกได้สูงสุด 2 สิทธิ์ต่อวันต่อท่านเท่านั้นค่ะ";
                                                }
                                                //20150131: เสมแจ้งแก้ไข 27/01/2015 รองรับแคมเปญ Smile Movie Day9 - ตั๋วหนัง SF (58120000)
                                                else if (fld_activity_id == "58120000")
                                                {
                                                    v_result = "notcomplete_MSG_ขออภัยค่ะ คุณขอใช้สิทธิ์เกินกว่าจำนวนสิทธิ์ต่อวันที่เงื่อนไขกำหนดค่ะ";
                                                }
                                                //20150313: เสมแจ้งแก้ไข 13/03/2015 รองรับแคมเปญ Happy Dee Happy Set (McDonald's) - Mc ชุดอร่อยสุดคุ้ม (58600000)
                                                else if (fld_activity_id == "58600000")
                                                {
                                                    v_result = "notcomplete_MSG_ขออภัยค่ะ คุณขอใช้สิทธิ์เกินกว่าจำนวนสิทธิ์ต่อวันที่เงื่อนไขกำหนดค่ะ";
                                                }
                                                //20150313: เสมแจ้งแก้ไข 13/03/2015 รองรับแคมเปญ Happy Dee Happy Set (McDonald's) - Mc ชุด Happy Meal (58610000)
                                                else if (fld_activity_id == "58610000")
                                                {
                                                    v_result = "notcomplete_MSG_ขออภัยค่ะ คุณขอใช้สิทธิ์เกินกว่าจำนวนสิทธิ์ต่อวันที่เงื่อนไขกำหนดค่ะ";
                                                }
                                                else
                                                {
                                                    v_result = "notcomplete_MSG_ขออภัยค่ะ คุณขอใช้สิทธิ์เกินกว่าจำนวนสิทธิ์ต่อวันที่เงื่อนไขกำหนดค่ะ";
                                                }
                                                break;
                                            case "notcomplete_แลกเกินจำนวนต่อสัปดาห์":
                                                //v_result = "notcomplete_MSG_ขออภัยค่ะ คุณได้รับสิทธิ์ครบตามเงื่อนไขแล้ว ขอบคุณที่ให้ความสนใจค่ะ";
                                                //20130909: นิวแจ้งแก้ไข 09/09/2013
                                                v_result = "notcomplete_MSG_กิจกรรมนี้สามารถแลกได้ 1 สิทธิ์ต่อสัปดาห์ (จันทร์-อาทิตย์) เท่านั้นค่ะ";
                                                break;
                                            case "notcomplete_แลกเกินจำนวนต่อเดือน":
                                                //v_result = "notcomplete_MSG_ขออภัยค่ะ คุณได้รับสิทธิ์ครบตามเงื่อนไขแล้ว ขอบคุณที่ให้ความสนใจค่ะ";
                                                //20130909: นิวแจ้งแก้ไข 09/09/2013
                                                v_result = "notcomplete_MSG_กิจกรรมนี้สามารถแลกได้ 1 สิทธิ์ต่อสัปดาห์ (จันทร์-อาทิตย์) เท่านั้นค่ะ";
                                                break;
                                            case "notcomplete_แลกเกินจำนวนต่อปี":
                                                //v_result = "notcomplete_MSG_ขออภัยค่ะ คุณได้รับสิทธิ์ครบตามเงื่อนไขแล้ว ขอบคุณที่ให้ความสนใจค่ะ";
                                                //20130909: นิวแจ้งแก้ไข 09/09/2013
                                                v_result = "notcomplete_MSG_กิจกรรมนี้สามารถแลกได้ 1 สิทธิ์ต่อสัปดาห์ (จันทร์-อาทิตย์) เท่านั้นค่ะ";
                                                break;
                                            case "notcomplete_แลกเกินจำนวนต่อกิจกรรม":
                                                //v_result = "notcomplete_MSG_ขออภัยค่ะ คุณได้รับสิทธิ์ครบตามเงื่อนไขแล้ว ขอบคุณที่ให้ความสนใจค่ะ";
                                                //20130909: นิวแจ้งแก้ไข 09/09/2013
                                                //v_result = "notcomplete_MSG_กิจกรรมนี้สามารถแลกได้ 1 สิทธิ์ต่อสัปดาห์ (จันทร์-อาทิตย์) เท่านั้นค่ะ";
                                                //20140508: ลองทดสอบเองพบว่ากรณีที่แลกเกินจำนวนไปแล้ว มันตกกรณีนี้ (AS400 น่าจะเช็คอันนี้ก่อน) เลยเปลี่ยนมาให้ข้อความแบบที่เสมต้องการให้แทน (ไม่ได้บอกเสม)
                                                //v_result = "notcomplete_MSG_ขออภัยค่ะ คุณขอใช้สิทธิ์เกินกว่าจำนวนสิทธิ์ต่อวันที่เงื่อนไขกำหนดค่ะ";

                                                //20141001: เสมแจ้งแก้ไข 24/09/2014 รองรับแคมเปญ Belle Villa Resort (57000100,57000200,57000300)
                                                if (fld_activity_id == "57000100" || fld_activity_id == "57000200" || fld_activity_id == "57000300")
                                                {
                                                    v_result = "notcomplete_MSG_กิจกรรมนี้ตลอดทั้งโครงการสามารถแลกได้สูงสุด 2 สิทธิ์ต่อท่านเท่านั้นค่ะ";
                                                }
                                                //20141001: เสมแจ้งแก้ไข 24/09/2014 รองรับแคมเปญ CP Fresh Mart (57050000,57060000)
                                                else if (fld_activity_id == "57050000" || fld_activity_id == "57060000")
                                                {
                                                    v_result = "notcomplete_MSG_กิจกรรมนี้แต่ละแคมเปญแลกได้สูงสุด 2 สิทธิ์ต่อวันต่อท่านเท่านั้นค่ะ";
                                                }
                                                //20150131: เสมแจ้งแก้ไข 27/01/2015 รองรับแคมเปญ Smile Movie Day9 - ตั๋วหนัง SF (58120000)
                                                else if (fld_activity_id == "58120000")
                                                {
                                                    v_result = "notcomplete_MSG_ขออภัยค่ะ คุณขอใช้สิทธิ์เกินกว่าจำนวนสิทธิ์ต่อวันที่เงื่อนไขกำหนดค่ะ";
                                                }
                                                //20150313: เสมแจ้งแก้ไข 13/03/2015 รองรับแคมเปญ Happy Dee Happy Set (McDonald's) - Mc ชุดอร่อยสุดคุ้ม (58600000)
                                                else if (fld_activity_id == "58600000")
                                                {
                                                    v_result = "notcomplete_MSG_ขออภัยค่ะ คุณขอใช้สิทธิ์เกินกว่าจำนวนสิทธิ์ต่อวันที่เงื่อนไขกำหนดค่ะ";
                                                }
                                                //20150313: เสมแจ้งแก้ไข 13/03/2015 รองรับแคมเปญ Happy Dee Happy Set (McDonald's) - Mc ชุด Happy Meal (58610000)
                                                else if (fld_activity_id == "58610000")
                                                {
                                                    v_result = "notcomplete_MSG_ขออภัยค่ะ คุณขอใช้สิทธิ์เกินกว่าจำนวนสิทธิ์ต่อวันที่เงื่อนไขกำหนดค่ะ";
                                                }
                                                else
                                                {
                                                    v_result = "notcomplete_MSG_ขออภัยค่ะ คุณขอใช้สิทธิ์เกินกว่าจำนวนสิทธิ์ต่อวันที่เงื่อนไขกำหนดค่ะ";
                                                }
                                                break;
                                            case "notpass_จำนวน POINT ไม่พอสำหรับการแลก":
                                                //v_result = "notcomplete_MSG_ขออภัยค่ะ คะแนนสะสม Smile Point คงเหลือของคุณมีไม่พอสำหรับแลกรับสิทธิ์ ขณะนี้คุณมีคะแนนสะสมคงเหลือ " + customerwsobj.fld_smile_point + " คะแนนค่ะ สอบถามติดต่อ 1766 กด 4 ค่ะ";
                                                //v_result = "notcomplete_MSG_คุณมีคะแนนสะสมไม่เพียงพอ คงเหลือ " + customerwsobj.fld_smile_point + " คะแนน แจ้ง " + GetCurrentDateTime();
                                                //20131014: เสมแจ้งแก้ไข 14/10/2556
                                                v_result = "notcomplete_MSG_คุณมีคะแนนสะสมไม่เพียงพอ คงเหลือ " + customerwsobj.fld_smile_point.Trim() + " คะแนน แจ้ง " + GetCurrentDateTime();
                                                break;
                                            case "notpass_ยอดเงินไม่พอสำหรับการแลก":
                                                v_result = "";
                                                break;
                                            case "notpass_จำนวนรางวัลไม่พอสำหรับการแลก":
                                                v_result = "notcomplete_MSG_ขออภัยค่ะ มีผู้รับสิทธิ์เต็มจำนวนแล้ว ขอบคุณที่ให้ความสนใจค่ะ";
                                                break;
                                            case "completed":
                                                //20141001: เสมแจ้งแก้ไข 24/09/2014 รองรับแคมเปญ Belle Villa Resort เบลล์วิลล่า เชียงใหม่ (57000100)
                                                if (fld_activity_id == "57000100")
                                                {
                                                    v_result = "เบลล์วิลล่า เชียงใหม่ " + v_receive_code;
                                                }
                                                //20141001: เสมแจ้งแก้ไข 24/09/2014 รองรับแคมเปญ Belle Villa Resort เบลล์วิลล่า ปาย (57000200)
                                                else if (fld_activity_id == "57000200")
                                                {
                                                    v_result = "เบลล์วิลล่า ปาย " + v_receive_code;
                                                }
                                                //20141001: เสมแจ้งแก้ไข 24/09/2014 รองรับแคมเปญ Belle Villa Resort เบลล์วิลล่า เขาใหญ่ (57000300)
                                                else if (fld_activity_id == "57000300")
                                                {
                                                    v_result = "เบลล์วิลล่า เขาใหญ่ " + v_receive_code;
                                                }
                                                //20141001: เสมแจ้งแก้ไข 24/09/2014 รองรับแคมเปญ CP Fresh Mart ส่วนลด 30 บาท (57050000)
                                                else if (fld_activity_id == "57050000")
                                                {
                                                    v_result = "CP ส่วนลด 30 บาท " + v_receive_code + " " + GetCurrentDateTime();
                                                }
                                                //20141001: เสมแจ้งแก้ไข 24/09/2014 รองรับแคมเปญ CP Fresh Mart ส่วนลด 100 บาท (57060000)
                                                else if (fld_activity_id == "57060000")
                                                {
                                                    v_result = "CP ส่วนลด 100 บาท " + v_receive_code + " " + GetCurrentDateTime();
                                                }
                                                //20150131: เสมแจ้งแก้ไข 27/01/2015 รองรับแคมเปญ Smile Movie Day9 - ตั๋วหนัง SF (58120000)
                                                else if (fld_activity_id == "58120000")
                                                {
                                                    v_result = "ตั๋วหนัง รหัสรับสิทธิ์ " + v_receive_code + " " + GetCurrentDateTime();
                                                }
                                                //20150313: เสมแจ้งแก้ไข 13/03/2015 รองรับแคมเปญ Happy Dee Happy Set (McDonald's) - Mc ชุดอร่อยสุดคุ้ม (58600000)
                                                else if (fld_activity_id == "58600000")
                                                {
                                                    v_result = "ชุดอร่อยสุดคุ้ม รหัสรับสิทธิ์ " + v_receive_code + " " + GetCurrentDateTime();
                                                }
                                                //20150313: เสมแจ้งแก้ไข 13/03/2015 รองรับแคมเปญ Happy Dee Happy Set (McDonald's) - Mc ชุด Happy Meal (58610000)
                                                else if (fld_activity_id == "58610000")
                                                {
                                                    v_result = "ชุดHappy Meal รหัสรับสิทธิ์ " + v_receive_code + " " + GetCurrentDateTime();
                                                }
                                                else
                                                {
                                                    v_result = "รหัสรับสิทธิ์" + v_receive_code + " " + GetCurrentDateTime();
                                                }
                                                break;
                                            default:
                                                v_result = "notcomplete";
                                                break;
                                        }

                                        /* Assign ค่าให้ Return object*/
                                        obj.fld_smile_point_before_booked = v_smile_point_before_booked;
                                        obj.fld_smile_point_after_booked = v_smile_point_after_booked;
                                        obj.fld_point = v_point;
                                        obj.fld_booked_no = v_booked_no;
                                        obj.fld_get_discount_amount = v_get_discount_amount;
                                        obj.fld_get_discount_percent = v_get_discount_percent;
                                        obj.fld_get_discount_from = v_get_discount_from;
                                        obj.fld_receive_code = v_receive_code;
                                        obj.fld_comment = v_comment;
                                        obj.fld_result = v_result;
                                        obj.fld_sessionID = v_sessionID;
                                    }
                                }
                                break;
                            default:
                                //obj.fld_smile_point_before_booked = mtlwsobj.fld_smile_point_before_booked;
                                //obj.fld_smile_point_after_booked = mtlwsobj.fld_smile_point_after_booked;
                                //obj.fld_point = mtlwsobj.fld_point;
                                //obj.fld_booked_no = mtlwsobj.fld_booked_no;
                                //obj.fld_get_discount_amount = mtlwsobj.fld_get_discount_amount;
                                //obj.fld_get_discount_from = mtlwsobj.fld_get_discount_from;
                                //obj.fld_get_discount_percent = mtlwsobj.fld_get_discount_percent;
                                //obj.fld_receive_code = mtlwsobj.fld_receive_code;
                                break;
                        }

                        //LogResponse: Insert Log Response
                        logobj.AddWSLog(pacobj.PartnerName, ipaddress, "Response", "NETWS_ForPartner", "PartnerSetBookSmileActivityWithSmilePasswordReturnManyReceiveCode", obj.fld_result + "|" + pacobj.PartnerName + "|" + fld_client_number + "|" + obj.fld_sessionID, refnum);

                        //20130909: CRM ให้เพิ่มส่ง SMS เอง
                        bool sendResult = SendSMS(fld_mobile_phone_number, obj.fld_result.Replace("completed_MSG_", "").Replace("notcomplete_MSG_", ""));
                        return obj;
                    }
                    catch (Exception ex)
                    {
                        //LogRequest: Insert Log Request
                        logobj.AddWSLog(pacobj.PartnerName, ipaddress, "Request", "NETWS_ForPartner", "PartnerSetBookSmileActivityWithSmilePasswordReturnManyReceiveCode", fld_partner_username + "|" + fld_partner_password + "|" + fld_client_number + "|" + fld_smile_password + "|" + fld_activity_id + "|" + fld_book_no + "|" + fld_mobile_phone_number + "|" + fld_home_phone_number + "|" + fld_home_phone_number_ext + "|" + fld_office_phone_number + "|" + fld_office_phone_number_ext + "|" + fld_smile_branch_code + "|" + fld_request_branch_code + "|" + fld_request_branch_name, refnum);

                        obj.fld_result = "notcomplete_" + ex.ToString();

                        //LogResponse: Insert Log Response
                        logobj.AddWSLog(pacobj.PartnerName, ipaddress, "Response", "NETWS_ForPartner", "PartnerSetBookSmileActivityWithSmilePasswordReturnManyReceiveCode", obj.fld_result + "|" + pacobj.PartnerName + "|" + fld_client_number + "|" + obj.fld_sessionID, refnum);

                        return obj;
                    }
                }
                else
                {
                    //LogRequest: Insert Log Request
                    logobj.AddWSLog(pacobj.PartnerName, ipaddress, "Request", "NETWS_ForPartner", "PartnerSetBookSmileActivityWithSmilePasswordReturnManyReceiveCode", fld_partner_username + "|" + fld_partner_password + "|" + fld_client_number + "|" + fld_smile_password + "|" + fld_activity_id + "|" + fld_book_no + "|" + fld_mobile_phone_number + "|" + fld_home_phone_number + "|" + fld_home_phone_number_ext + "|" + fld_office_phone_number + "|" + fld_office_phone_number_ext + "|" + fld_smile_branch_code + "|" + fld_request_branch_code + "|" + fld_request_branch_name, refnum);

                    obj.fld_result = pacobj.Result.Trim().Replace("notpass_", "notcomplete_");

                    //LogResponse: Insert Log Response
                    logobj.AddWSLog(pacobj.PartnerName, ipaddress, "Response", "NETWS_ForPartner", "PartnerSetBookSmileActivityWithSmilePasswordReturnManyReceiveCode", obj.fld_result + "|" + pacobj.PartnerName + "|" + fld_client_number + "|" + obj.fld_sessionID, refnum);

                    return obj;
                }
            }
        }
        catch (Exception ex)
        {
            //LogRequest: Insert Log Request
            logobj.AddWSLog(fld_partner_username, ipaddress, "Request", "NETWS_ForPartner", "PartnerSetBookSmileActivityWithSmilePasswordReturnManyReceiveCode", fld_partner_username + "|" + fld_partner_password + "|" + fld_client_number + "|" + fld_smile_password + "|" + fld_activity_id + "|" + fld_book_no + "|" + fld_mobile_phone_number + "|" + fld_home_phone_number + "|" + fld_home_phone_number_ext + "|" + fld_office_phone_number + "|" + fld_office_phone_number_ext + "|" + fld_smile_branch_code + "|" + fld_request_branch_code + "|" + fld_request_branch_name, refnum);

            obj.fld_result = "notcomplete_" + ex.ToString();

            //LogResponse: Insert Log Response
            logobj.AddWSLog(fld_partner_username, ipaddress, "Response", "NETWS_ForPartner", "PartnerSetBookSmileActivityWithSmilePasswordReturnManyReceiveCode", obj.fld_result + "|" + obj.fld_sessionID, refnum);

            return obj;
        }
    }
    #endregion

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

    #region Methods สำหรับเรียก ApplinX Web Services ภายใน
    private GetCustomerDetail_Result MTLGetCustomerDetail(string fld_client_number)
    {
        GetCustomerDetail_Result obj = new GetCustomerDetail_Result();

        //1. Call ApplinX Web Services GetCustomerDetail()
        MTL.WS_Admin.WS_Admin_ForSmartCard.WS_Admin_ForSmartCardService wsobj = new MTL.WS_Admin.WS_Admin_ForSmartCard.WS_Admin_ForSmartCardService();
        obj.fld_result = wsobj.GetCustomerDetail(admin_username, admin_password, fld_client_number, out obj.fld_sessionID, out obj.fld_customer_name, out obj.fld_customer_surname, out obj.fld_customer_dob, out obj.fld_customer_age, out obj.fld_customer_idcard, out obj.fld_smile_point, out obj.fld_card_type, out obj.fld_email, out obj.fld_address_line1, out obj.fld_address_line2, out obj.fld_address_line3, out obj.fld_mobile_phone_number, out obj.fld_home_phone_number, out obj.fld_office_phone_number, out obj.fld_client_isAgent, out obj.fld_client_isSmileClubMember, out obj.fld_expiry_point_round1, out obj.fld_expiry_date_round1, out obj.fld_expiry_point_round2, out obj.fld_expiry_date_round2, out obj.fld_mobile_phone_number_SMC);

        /*
         * ตัวอย่างข้อมูลสำหรับทดสอบ
         */
        //GetCustomerDetail_Result obj = new GetCustomerDetail_Result
        //{
        //    fld_result = "found",
        //    fld_sessionID = "U012341",
        //    fld_customer_name = "สุนทร",
        //    fld_customer_surname = "ธนาประเสริฐสุข",
        //    fld_customer_dob = "17/05/2521",
        //    fld_customer_age = "35",
        //    fld_customer_idcard = "3-7605-00235-26-9",
        //    fld_smile_point = "1200.50",
        //    fld_card_type = "Smile",
        //    fld_email = "soonthana@gmail.com",
        //    fld_address_line1 = "250 ถ.รัชดาภิเษก ห้วยขวาง กทม. 10310",
        //    fld_address_line2 = "",
        //    fld_address_line3 = "",
        //    fld_mobile_phone_number = "0868212217",
        //    fld_home_phone_number = "032461573",
        //    fld_office_phone_number = "022902098",
        //    fld_client_isAgent = "N",
        //    fld_client_isSmileClubMember = "Y",
        //    fld_expiry_point_round1 = "1000.00",
        //    fld_expiry_date_round1 = "30/06/56",
        //    fld_expiry_point_round2 = "200.50",
        //    fld_expiry_date_round2 = "31/12/56",
        //    fld_mobile_phone_number_SMC = "0868212217"
        //};

        return obj;
    }

    private CheckSmilePassword_Result MTLCheckSmilePassword(string fld_client_number, string fld_smile_password, string fld_card_seq_number)
    {
        CheckSmilePassword_Result obj = new CheckSmilePassword_Result();

        //1. Call ApplinX Web Services CheckSmilePassword()
        WS_Admin_SmileServices.WS_Admin_ForSmartCard.WS_Admin_ForSmartCardService wsobj = new WS_Admin_SmileServices.WS_Admin_ForSmartCard.WS_Admin_ForSmartCardService();
        obj.fld_result = wsobj.CheckSmilePassword(admin_username, admin_password, fld_client_number, fld_smile_password, fld_card_seq_number, out obj.fld_sessionID, out obj.fld_last_changed_password_channel, out obj.fld_last_changed_password_date, out obj.fld_last_changed_password_time);

        /*
         * ตัวอย่างข้อมูลสำหรับทดสอบ
         */
        //CheckSmilePassword_Result obj = new CheckSmilePassword_Result
        //{
        //    fld_result = "passed",
        //    fld_sessionID = "U012342",
        //    fld_last_changed_password_channel = "",
        //    fld_last_changed_password_date = "",
        //    fld_last_changed_password_time = ""
        //};

        return obj;
    }

    private SetBookSmileActivity_NEW_Result MTLSetBookSmileActivity_NEW(string fld_client_number, string fld_activity_id, string fld_book_no, string fld_mobile_phone_number, string fld_home_phone_number, string fld_home_phone_number_ext, string fld_office_phone_number, string fld_office_phone_number_ext, string fld_smile_branch_code, string fld_request_branch_code, string fld_request_branch_name, string fld_additional_amount)
    {
        SetBookSmileActivity_NEW_Result obj = new SetBookSmileActivity_NEW_Result();

        //1. Call ApplinX Web Services SetBookSmileActivity()
        WS_Admin_SmileServices.WS_Admin_ForSmartCard.WS_Admin_ForSmartCardService wsobj = new WS_Admin_SmileServices.WS_Admin_ForSmartCard.WS_Admin_ForSmartCardService();
        obj.fld_result = wsobj.SetBookSmileActivity_NEW(admin_username, admin_password, fld_client_number, fld_activity_id, ref fld_book_no, fld_mobile_phone_number, fld_home_phone_number, fld_home_phone_number_ext, fld_office_phone_number, fld_office_phone_number_ext, fld_smile_branch_code, fld_request_branch_code, fld_request_branch_name, fld_additional_amount, out obj.fld_sessionID, out obj.fld_smile_point_before_booked, out obj.fld_smile_point_after_booked, out obj.fld_point, out obj.fld_get_discount_amount, out obj.fld_get_discount_percent, out obj.fld_get_discount_from, out obj.fld_receive_code, out obj.fld_comment);
        obj.fld_booked_no = fld_book_no;
        
        /*
         * ตัวอย่างข้อมูลสำหรับทดสอบ
         */
        //SetBookSmileActivity_NEW_Result obj = new SetBookSmileActivity_NEW_Result
        //{
        //    fld_result = "completed",
        //    fld_sessionID = "U012343",
        //    fld_smile_point_before_booked = "1200.50",
        //    fld_smile_point_after_booked = "1000.50",
        //    fld_point = "100.00",
        //    fld_booked_no = "2",
        //    fld_get_discount_amount = ".00",
        //    fld_get_discount_percent = "",
        //    fld_get_discount_from = ".00",
        //    fld_receive_code = "7392"
        //};

        return obj;
    }
    #endregion

    #region Method สำหรับส่ง SMS ผ่าน Net-Innova API
    private bool SendSMS(string MobileNumber, string Message)
    {
        bool result = false;
        MTL.NETWS_ForSendSMS.ForSendSMS wsobj = new MTL.NETWS_ForSendSMS.ForSendSMS();
        MTL.NETWS_ForSendSMS.SMSSendNow_Result sendResult = wsobj.SendSMSNow("postsmc@mtl", "password", MobileNumber, Message);

        if (sendResult.Result.Trim() == "sent")
        {
            result = true;
        }
        else
        {
            result = false;
        }

        return result;
    }
    #endregion

    #region Utilities Methods
    private string GetCurrentDateTime()
    {
        string vHH = MTL.Utils.ThisWeb.GetCurrentDateTimeByFormat("HH");
        string vMI = MTL.Utils.ThisWeb.GetCurrentDateTimeByFormat("MI");
        string vDD = MTL.Utils.ThisWeb.GetCurrentDateTimeByFormat("DD");
        string vMM = MTL.Utils.ThisWeb.GetCurrentDateTimeByFormat("MM");
        string vYYYY = (Convert.ToInt32(MTL.Utils.ThisWeb.GetCurrentDateTimeByFormat("YYYY")) + 543).ToString();

        return vHH + ":" + vMI + ", " + vDD + "/" + vMM + "/" + vYYYY;
    }
    #endregion
}

