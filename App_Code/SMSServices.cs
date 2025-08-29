using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

/// <summary>
/// Summary description for SMSServices
/// </summary>
public class SMSServices
{
    public SMSServices()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //public static string SendOTPSMS(string strMobileno, string strotp)
    //{
    //    string strStreemResponse = string.Empty;
    //    string strResponseResult = string.Empty;
    //    try
    //    {
    //        //Your one time password(OTP) is 1234. Do not share this OTP with any one. Regards, FreeBroke.
    //        HttpWebRequest myHttpWebRequest = null;
    //        HttpWebResponse myHttpWebResponse = null;
    //        StreamReader strStreemReader = null;
    //        myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(@"http://sms.ciphertrivia.com//api/web2sms.php?workingkey=Ae56f654ce7fc02e880ad5ed5cadfc10a&to=" + strMobileno + "&sender=ARPHIB&entity_id=1501375090000035558&message=Hi there, We thank you for your registration on D2CCART.com, we are happy you are here in D2CCART family, we founded D2CCART in 2019 because, we wanted to create a trustworthy pocket friendly premium inspiring products you need specially for your sweet  dream home. OTP is to verify your account is " + strotp);
    //        myHttpWebRequest.Method = "GET";
    //        myHttpWebRequest.ContentType = "text/xml; encoding='utf-8'";
    //        myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
    //        strStreemReader = new StreamReader(myHttpWebResponse.GetResponseStream());
    //        strStreemResponse = strStreemReader.ReadToEnd();
    //        string[] strmsgid = strStreemResponse.Split('=');

    //    }
    //    catch (Exception ex)
    //    {
    //        strResponseResult = ex.Message;
    //        CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "SendOTPSMS", ex.Message);
    //    }
    //    return strResponseResult;
    //}

    public static string SendOTPSMS(string strMobileno, string strotp)
    {
        string strStreemResponse = string.Empty;
        string strResponseResult = string.Empty;
        try
        {
            //Your one time password(OTP) is 1234. Do not share this OTP with any one. Regards, FreeBroke.
            HttpWebRequest myHttpWebRequest = null;
            HttpWebResponse myHttpWebResponse = null;
            StreamReader strStreemReader = null;
            myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(@"http://sms.ciphertrivia.com//api/web2sms.php?workingkey=Ae56f654ce7fc02e880ad5ed5cadfc10a&to=" + strMobileno + "&sender=ARPHIB&entity_id=1501375090000035558&message=Use OTP " + strotp + " to login to your D2CCART account. D2CCART does not ask for OTP or contact number to be shared with anyone including D2CCART personnel.");
            myHttpWebRequest.Method = "GET";
            myHttpWebRequest.ContentType = "text/xml; encoding='utf-8'";
            myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
            strStreemReader = new StreamReader(myHttpWebResponse.GetResponseStream());
            strStreemResponse = strStreemReader.ReadToEnd();
            string[] strmsgid = strStreemResponse.Split('=');

        }
        catch (Exception ex)
        {
            strResponseResult = ex.Message;
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "SendOTPSMS", ex.Message);
        }
        return strResponseResult;
    }

    public static string SendOrderSuccess(string strMobileno, string oid)
    {
        string strStreemResponse = string.Empty;
        string strResponseResult = string.Empty;
        try
        {
            //Your one time password(OTP) is 1234. Do not share this OTP with any one. Regards, FreeBroke.
            HttpWebRequest myHttpWebRequest = null;
            HttpWebResponse myHttpWebResponse = null;
            StreamReader strStreemReader = null;
            myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(@"http://sms.ciphertrivia.com//api/web2sms.php?workingkey=Ae56f654ce7fc02e880ad5ed5cadfc10a&to=" + strMobileno + "&sender=ARPHIB&entity_id=1501375090000035558&message=Greetings from D2CCART, your order has been placed successfully on D2CCART.com, your order id is " + oid + ", thank you for shopping with trust.");
            myHttpWebRequest.Method = "GET";
            myHttpWebRequest.ContentType = "text/xml; encoding='utf-8'";
            myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
            strStreemReader = new StreamReader(myHttpWebResponse.GetResponseStream());
            strStreemResponse = strStreemReader.ReadToEnd();
            string[] strmsgid = strStreemResponse.Split('=');

        }
        catch (Exception ex)
        {
            strResponseResult = ex.Message;
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "SendOrderSuccess", ex.Message);
        }
        return strResponseResult;
    }

    public static string SendOrderOutForDelivery(string strMobileno, string oid, string trkURL, string trkCode)
    {
        string strStreemResponse = string.Empty;
        string strResponseResult = string.Empty;
        try
        {
            //Your one time password(OTP) is 1234. Do not share this OTP with any one. Regards, FreeBroke.
            HttpWebRequest myHttpWebRequest = null;
            HttpWebResponse myHttpWebResponse = null;
            StreamReader strStreemReader = null;
            myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(@"http://sms.ciphertrivia.com//api/web2sms.php?workingkey=Ae56f654ce7fc02e880ad5ed5cadfc10a&to=" + strMobileno + "&sender=ARPHIB&entity_id=1501375090000035558&message=Greetings from D2CCART, your order " + oid + " is out for delivery. tracking url " + trkURL + ", tracking code " + trkCode + ".");
            myHttpWebRequest.Method = "GET";
            myHttpWebRequest.ContentType = "text/xml; encoding='utf-8'";
            myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
            strStreemReader = new StreamReader(myHttpWebResponse.GetResponseStream());
            strStreemResponse = strStreemReader.ReadToEnd();
            string[] strmsgid = strStreemResponse.Split('=');

        }
        catch (Exception ex)
        {
            strResponseResult = ex.Message;
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "SendOrderOutForDelivery", ex.Message);
        }
        return strResponseResult;
    }
    public static string SendDelivered(string strMobileno, string oid, string oDate)
    {
        string strStreemResponse = string.Empty;
        string strResponseResult = string.Empty;
        try
        {
            //Your one time password(OTP) is 1234. Do not share this OTP with any one. Regards, FreeBroke.
            HttpWebRequest myHttpWebRequest = null;
            HttpWebResponse myHttpWebResponse = null;
            StreamReader strStreemReader = null;
            myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(@"http://sms.ciphertrivia.com//api/web2sms.php?workingkey=Ae56f654ce7fc02e880ad5ed5cadfc10a&to=" + strMobileno + "&sender=ARPHIB&entity_id=1501375090000035558&message=Greetings from D2CCART, your order " + oid + " is delivered successfully on " + oDate + ", thanks for shopping with us.");
            myHttpWebRequest.Method = "GET";
            myHttpWebRequest.ContentType = "text/xml; encoding='utf-8'";
            myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
            strStreemReader = new StreamReader(myHttpWebResponse.GetResponseStream());
            strStreemResponse = strStreemReader.ReadToEnd();
            string[] strmsgid = strStreemResponse.Split('=');

        }
        catch (Exception ex)
        {
            strResponseResult = ex.Message;
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "SendDelivered", ex.Message);
        }
        return strResponseResult;
    }
}