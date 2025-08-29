using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Reflection;

public class Emails
{
    public static async Task<int> SendPasswordRestLink(string display_name, string name, string emails, string link, string mailSubject, string mailBody)
    {
        try
        {
            #region mailBody
            string mailBody1 = @"<!DOCTYPE HTML PUBLIC '-//W3C//DTD XHTML 1.0 Strict//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd'>
<html xmlns='http://www.w3.org/1999/xhtml'>
<head>
    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
    <meta name='viewport' content='width=device-width, initial-scale=1.0' />
    <meta http-equiv='X-UA-Compatible' content='IE=edge,chrome=1' />
    <meta name='format-detection' content='telephone=no' />
    <title>" + mailSubject + @"</title>
    <link href='https://fonts.googleapis.com/css?family=Lato:100,100i,300,300i,400,400i,700,700i,900,900i&display=swap' rel='stylesheet' />
    <style type='text/css'>
        html {
            background-color: #FFF;
            margin: 0;
            font-family: 'Lato', sans-serif;
            padding: 0;
        }

        /*@import url('https://fonts.googleapis.com/css?family=Roboto');*/

        body, #bodyTable, #bodyCell, #bodyCell {
            height: 100% !important;
            margin: 0;
            padding: 0;
            width: 100% !important;
        }

        table {
            border-collapse: collapse;
        }

            table[id=bodyTable] {
                width: 100% !important;
                margin: auto;
                max-width: 500px !important;
                color: #212121;
                font-weight: normal;
            }

        img, a img {
            border: 0;
            outline: none;
            text-decoration: none;
            height: auto;
            line-height: 100%;
        }

        /*a {
            text-decoration: none !important;
            border-bottom: 1px solid;
        }*/

        h1, h2, h3, h4, h5, h6 {
            color: #5F5F5F;
            font-weight: normal;
            font-size: 20px;
            line-height: 125%;
            text-align: Left;
            letter-spacing: normal;
            margin-top: 0;
            margin-right: 0;
            margin-bottom: 10px;
            margin-left: 0;
            padding-top: 0;
            padding-bottom: 0;
            padding-left: 0;
            padding-right: 0;
        }

        .ReadMsgBody {
            width: 100%;
        }

        .ExternalClass {
            width: 100%;
        }

            .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {
                line-height: 100%;
            }

        table, td {
            mso-table-lspace: 0pt;
            mso-table-rspace: 0pt;
        }

        #outlook a {
            padding: 0;
        }

        img {
            -ms-interpolation-mode: bicubic;
            display: block;
            outline: none;
            text-decoration: none;
        }

        body, table, td, p, a, li, blockquote {
            -ms-text-size-adjust: 100%;
            -webkit-text-size-adjust: 100%;
            font-weight: 500 !important;
        }

        .ExternalClass td[class='ecxflexibleContainerBox'] h3 {
            padding-top: 10px !important;
        }

        h1 {
            display: block;
            font-size: 26px;
            font-style: normal;
            font-weight: normal;
            line-height: 100%;
        }

        h2 {
            display: block;
            font-size: 20px;
            font-style: normal;
            font-weight: normal;
            line-height: 120%;
        }

        h3 {
            display: block;
            font-size: 17px;
            font-style: normal;
            font-weight: normal;
            line-height: 110%;
        }

        h4 {
            display: block;
            font-size: 18px;
            font-style: italic;
            font-weight: normal;
            line-height: 100%;
        }

        .flexibleImage {
            height: auto;
        }

        .linkRemoveBorder {
            border-bottom: 0 !important;
        }

        table[class=flexibleContainerCellDivider] {
            padding-bottom: 0 !important;
            padding-top: 0 !important;
        }

        body, #bodyTable {
            background-color: #E1E1E1;
        }

        #emailHeader {
            background-color: #fff;
        }

        #emailBody {
            background-color: #FFFFFF;
        }

        #emailFooter {
            background-color: #E1E1E1;
        }

        .nestedContainer {
            background-color: #F8F8F8;
            border: 1px solid #CCCCCC;
        }

        .emailButton {
            background-color: #205478;
            border-collapse: separate;
        }

        .buttonContent {
            color: #FFFFFF;
            font-family: Helvetica;
            font-size: 18px;
            font-weight: bold;
            line-height: 100%;
            padding: 15px;
            text-align: center;
        }

            .buttonContent a {
                color: #FFFFFF;
                display: block;
                text-decoration: none !important;
                border: 0 !important;
            }

        .emailCalendar {
            background-color: #FFFFFF;
            border: 1px solid #CCCCCC;
        }

        .emailCalendarMonth {
            background-color: #205478;
            color: #FFFFFF;
            font-size: 16px;
            font-weight: bold;
            padding-top: 10px;
            padding-bottom: 10px;
            text-align: center;
        }

        .emailCalendarDay {
            color: #205478;
            font-size: 60px;
            font-weight: bold;
            line-height: 100%;
            padding-top: 20px;
            padding-bottom: 20px;
            text-align: center;
        }

        .imageContentText {
            margin-top: 10px;
            line-height: 0;
        }

            .imageContentText a {
                line-height: 0;
            }

        #invisibleIntroduction {
            display: none !important;
        }

        span[class=ios-color-hack] a {
            color: #275100 !important;
            text-decoration: none !important;
        }

        span[class=ios-color-hack2] a {
            color: #205478 !important;
            text-decoration: none !important;
        }

        span[class=ios-color-hack3] a {
            color: #8B8B8B !important;
            text-decoration: none !important;
        }

        .a[href^='tel'], a[href^='sms'] {
            text-decoration: none !important;
            color: #606060 !important;
            pointer-events: none !important;
            cursor: default !important;
        }

        .mobile_link a[href^='tel'], .mobile_link a[href^='sms'] {
            text-decoration: none !important;
            color: #606060 !important;
            pointer-events: auto !important;
            cursor: default !important;
        }

        @media only screen and (max-width: 480px) {
            body {
                width: 100% !important;
                min-width: 100% !important;
            }

            table[id='emailHeader'],
            table[id='emailBody'],
            table[id='emailFooter'],
            table[class='flexibleContainer'],
            td[class='flexibleContainerCell'] {
                width: 100% !important;
            }

            td[class='flexibleContainerBox'], td[class='flexibleContainerBox'] table {
                display: block;
                width: 100%;
                text-align: left;
            }

            td[class='imageContent'] img {
                height: auto !important;
                width: 100% !important;
                max-width: 100% !important;
            }

            img[class='flexibleImage'] {
                height: auto !important;
                width: 100% !important;
                max-width: 100% !important;
            }

            img[class='flexibleImageSmall'] {
                height: auto !important;
                width: auto !important;
            }

            table[class='flexibleContainerBoxNext'] {
                padding-top: 10px !important;
            }

            table[class='emailButton'] {
                width: 100% !important;
            }

            td[class='buttonContent'] {
                padding: 0 !important;
            }

                td[class='buttonContent'] a {
                    padding: 15px !important;
                }
        }

        @media only screen and (-webkit-device-pixel-ratio:.75) {
        }

        @media only screen and (-webkit-device-pixel-ratio:1) {
        }

        @media only screen and (-webkit-device-pixel-ratio:1.5) {
        }

        @media only screen and (min-device-width : 320px) and (max-device-width:568px) {
        }

        .blink_text {
            -webkit-animation-name: blinker;
            -webkit-animation-duration: 2s;
            -webkit-animation-timing-function: linear;
            -webkit-animation-iteration-count: infinite;
            -moz-animation-name: blinker;
            -moz-animation-duration: 2s;
            -moz-animation-timing-function: linear;
            -moz-animation-iteration-count: infinite;
            animation-name: blinker;
            animation-duration: 2s;
            animation-timing-function: linear;
            animation-iteration-count: infinite;
            color: white;
        }

        @-moz-keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        @-webkit-keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        } 
        @keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }
    </style>
</head>
<body bgcolor='#E1E1E1' leftmargin='0' marginwidth='0' topmargin='0' marginheight='0' offset='0'>
    <center style='background-color:#E1E1E1;'>
        <table border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' id='bodyTable' style=' table-layout: fixed; max-width:100% !important;width: 100% !important;min-width: 100% !important;'>
            <tr>
                <td align='center' valign='top' id='bodyCell'>
                    
                    <table bgcolor='#FFFFFF' border='0' cellpadding='0' cellspacing='0' width='700' id='emailBody' style='margin-top:20px;margin-bottom:20px;'> 
                        <tr>
                            <td align='center' valign='top' style=''>
                                <table border='0' cellpadding='0' cellspacing='0' width='100%' bgcolor='#fff'>
                                    <tr>
                                        <td align='center' valign='top'>
                                            <table border='0' cellpadding='0' cellspacing='0' width='700' class='flexibleContainer'>
                                                <tr>
                                                    <td align='center' valign='top' width='700' class='flexibleContainerCell'>
                                                        <table border='0' cellpadding='30' cellspacing='0' width='100%'> 
                                                            <tr>
                                                                <td align='left' valign='top' style='padding:20px;background:#fff'>
                                                                    <table border='0' cellpadding='0' cellspacing='0' style='float:left;width: 100%;margin-right:5%'>
                                                                        <tr>
                                                                            <td align='left' valign='top' width='48%' style='vertical-align: middle;' class='flexibleContainerCell'>
                                                                                <img style='width:110px;margin-bottom:0px;' src='" + ConfigurationManager.AppSettings["domain"] + @"/assets/imgs/logo.png' />
                                                                            </td>
                                                                           
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align='left' valign='top' style='padding:0px;background:#fbd8a4de'>
                                                                    <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                                                        <tr>
                                                                            <td align='left' valign='top' width='100%' class='flexibleContainerCell'>
                                                                                <p style='font-size:22px;line-height:32px!important;text-align:center;color:#000000;margin-top:0px;background:#f9b654;padding:10px;'>" + mailSubject + @"</p>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr> 
                                                            <tr>
                                                                <td align='left' valign='top' style='padding-top:0px;'>
                                                                    <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                                                        <tr style='margin-bottom:15px;'>
                                                                            <td style='font-size:14px;' align='left' valign='top' width='100%' class='flexibleContainerCell'>
                                                                                " + mailBody + @"
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align='left' valign='top' style='padding:20px 20px;background:#fbd8a4de'>
                                                                   <br />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </center>
</body>
</html>";
            #endregion

            MailMessage mail = new MailMessage();
            mail.To.Add(emails);
            mail.From = new MailAddress(ConfigurationManager.AppSettings["from"], display_name);
            mail.Subject = mailSubject;
            mail.Body = mailBody1;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["host"];
            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            smtp.Credentials = new System.Net.NetworkCredential
                           (ConfigurationManager.AppSettings["userName"], ConfigurationManager.AppSettings["password"]);

            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSsl"]);
            await Task.Run(() => smtp.Send(mail));
            return 1;
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "SendPasswordRestLink", ex.Message);
            return 0;
        }
    }

    public static int SendEmailVerifyLink(string email, string name, string strlink)
    {
        try
        {
            #region mailBody
            string mailBody1 = @"<!DOCTYPE HTML PUBLIC '-//W3C//DTD XHTML 1.0 Strict//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd'>
<html xmlns='http://www.w3.org/1999/xhtml'>
<head>
    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
    <meta name='viewport' content='width=device-width, initial-scale=1.0' />
    <meta http-equiv='X-UA-Compatible' content='IE=edge,chrome=1' />
    <meta name='format-detection' content='telephone=no' />
    <title>Archidply Decor</title>
    <link rel='shortcut icon' href='https://www.Archidply Decor/images/Nextwebi-sq_Black_Logo2_1_140x.png' />
    <link href='https://fonts.googleapis.com/css?family=Lato:100,100i,300,300i,400,400i,700,700i,900,900i&display=swap' rel='stylesheet' />
    <style type='text/css'>
        html {
            background-color: #FFF;
            margin: 0;
            font-family: 'Lato', sans-serif;
            padding: 0;
        }

        /*@import url('https://fonts.googleapis.com/css?family=Roboto');*/

        body, #bodyTable, #bodyCell, #bodyCell {
            height: 100% !important;
            margin: 0;
            padding: 0;
            width: 100% !important;
        }

        table {
            border-collapse: collapse;
        }

            table[id=bodyTable] {
                width: 100% !important;
                margin: auto;
                max-width: 500px !important;
                color: #212121;
                font-weight: normal;
            }

        img, a img {
            border: 0;
            outline: none;
            text-decoration: none;
            height: auto;
            line-height: 100%;
        }

        /*a {
            text-decoration: none !important;
            border-bottom: 1px solid;
        }*/

        h1, h2, h3, h4, h5, h6 {
            color: #5F5F5F;
            font-weight: normal;
            font-size: 20px;
            line-height: 125%;
            text-align: Left;
            letter-spacing: normal;
            margin-top: 0;
            margin-right: 0;
            margin-bottom: 10px;
            margin-left: 0;
            padding-top: 0;
            padding-bottom: 0;
            padding-left: 0;
            padding-right: 0;
        }

        .ReadMsgBody {
            width: 100%;
        }

        .ExternalClass {
            width: 100%;
        }

            .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {
                line-height: 100%;
            }

        table, td {
            mso-table-lspace: 0pt;
            mso-table-rspace: 0pt;
        }

        #outlook a {
            padding: 0;
        }

        img {
            -ms-interpolation-mode: bicubic;
            display: block;
            outline: none;
            text-decoration: none;
        }

        body, table, td, p, a, li, blockquote {
            -ms-text-size-adjust: 100%;
            -webkit-text-size-adjust: 100%;
            font-weight: 500 !important;
        }

        .ExternalClass td[class='ecxflexibleContainerBox'] h3 {
            padding-top: 10px !important;
        }

        h1 {
            display: block;
            font-size: 26px;
            font-style: normal;
            font-weight: normal;
            line-height: 100%;
        }

        h2 {
            display: block;
            font-size: 20px;
            font-style: normal;
            font-weight: normal;
            line-height: 120%;
        }

        h3 {
            display: block;
            font-size: 17px;
            font-style: normal;
            font-weight: normal;
            line-height: 110%;
        }

        h4 {
            display: block;
            font-size: 18px;
            font-style: italic;
            font-weight: normal;
            line-height: 100%;
        }

        .flexibleImage {
            height: auto;
        }

        .linkRemoveBorder {
            border-bottom: 0 !important;
        }

        table[class=flexibleContainerCellDivider] {
            padding-bottom: 0 !important;
            padding-top: 0 !important;
        }

        body, #bodyTable {
            background-color: #E1E1E1;
        }

        #emailHeader {
            background-color: #fff;
        }

        #emailBody {
            background-color: #FFFFFF;
        }

        #emailFooter {
            background-color: #E1E1E1;
        }

        .nestedContainer {
            background-color: #F8F8F8;
            border: 1px solid #CCCCCC;
        }

        .emailButton {
            background-color: #205478;
            border-collapse: separate;
        }

        .buttonContent {
            color: #FFFFFF;
            font-family: Helvetica;
            font-size: 18px;
            font-weight: bold;
            line-height: 100%;
            padding: 15px;
            text-align: center;
        }

            .buttonContent a {
                color: #FFFFFF;
                display: block;
                text-decoration: none !important;
                border: 0 !important;
            }

        .emailCalendar {
            background-color: #FFFFFF;
            border: 1px solid #CCCCCC;
        }

        .emailCalendarMonth {
            background-color: #205478;
            color: #FFFFFF;
            font-size: 16px;
            font-weight: bold;
            padding-top: 10px;
            padding-bottom: 10px;
            text-align: center;
        }

        .emailCalendarDay {
            color: #205478;
            font-size: 60px;
            font-weight: bold;
            line-height: 100%;
            padding-top: 20px;
            padding-bottom: 20px;
            text-align: center;
        }

        .imageContentText {
            margin-top: 10px;
            line-height: 0;
        }

            .imageContentText a {
                line-height: 0;
            }

        #invisibleIntroduction {
            display: none !important;
        }

        span[class=ios-color-hack] a {
            color: #275100 !important;
            text-decoration: none !important;
        }

        span[class=ios-color-hack2] a {
            color: #205478 !important;
            text-decoration: none !important;
        }

        span[class=ios-color-hack3] a {
            color: #8B8B8B !important;
            text-decoration: none !important;
        }

        .a[href^='tel'], a[href^='sms'] {
            text-decoration: none !important;
            color: #606060 !important;
            pointer-events: none !important;
            cursor: default !important;
        }

        .mobile_link a[href^='tel'], .mobile_link a[href^='sms'] {
            text-decoration: none !important;
            color: #606060 !important;
            pointer-events: auto !important;
            cursor: default !important;
        }

        @media only screen and (max-width: 480px) {
            body {
                width: 100% !important;
                min-width: 100% !important;
            }

            table[id='emailHeader'],
            table[id='emailBody'],
            table[id='emailFooter'],
            table[class='flexibleContainer'],
            td[class='flexibleContainerCell'] {
                width: 100% !important;
            }

            td[class='flexibleContainerBox'], td[class='flexibleContainerBox'] table {
                display: block;
                width: 100%;
                text-align: left;
            }

            td[class='imageContent'] img {
                height: auto !important;
                width: 100% !important;
                max-width: 100% !important;
            }

            img[class='flexibleImage'] {
                height: auto !important;
                width: 100% !important;
                max-width: 100% !important;
            }

            img[class='flexibleImageSmall'] {
                height: auto !important;
                width: auto !important;
            }

            table[class='flexibleContainerBoxNext'] {
                padding-top: 10px !important;
            }

            table[class='emailButton'] {
                width: 100% !important;
            }

            td[class='buttonContent'] {
                padding: 0 !important;
            }

                td[class='buttonContent'] a {
                    padding: 15px !important;
                }
        }

        @media only screen and (-webkit-device-pixel-ratio:.75) {
        }

        @media only screen and (-webkit-device-pixel-ratio:1) {
        }

        @media only screen and (-webkit-device-pixel-ratio:1.5) {
        }

        @media only screen and (min-device-width : 320px) and (max-device-width:568px) {
        }

        .blink_text {
            -webkit-animation-name: blinker;
            -webkit-animation-duration: 2s;
            -webkit-animation-timing-function: linear;
            -webkit-animation-iteration-count: infinite;
            -moz-animation-name: blinker;
            -moz-animation-duration: 2s;
            -moz-animation-timing-function: linear;
            -moz-animation-iteration-count: infinite;
            animation-name: blinker;
            animation-duration: 2s;
            animation-timing-function: linear;
            animation-iteration-count: infinite;
            color: white;
        }

        @-moz-keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        @-webkit-keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        @keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }
    </style>
</head>
<body bgcolor='#E1E1E1' leftmargin='0' marginwidth='0' topmargin='0' marginheight='0' offset='0'>
    <center style='background-color:#E1E1E1;'>
        <table border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' id='bodyTable' style=' table-layout: fixed; max-width:100% !important;width: 100% !important;min-width: 100% !important;'>
            <tr>
                <td align='center' valign='top' id='bodyCell'>
                    
                    <table bgcolor='#FFFFFF' border='0' cellpadding='0' cellspacing='0' width='700' id='emailBody' style='margin-top:20px;margin-bottom:20px;'> 
                        <tr>
                            <td align='center' valign='top' style=''>
                                <table border='0' cellpadding='0' cellspacing='0' width='100%' bgcolor='#fff'>
                                    <tr>
                                        <td align='center' valign='top'>
                                            <table border='0' cellpadding='0' cellspacing='0' width='700' class='flexibleContainer'>
                                                <tr>
                                                    <td align='center' valign='top' width='700' class='flexibleContainerCell'>
                                                        <table border='0' cellpadding='30' cellspacing='0' width='100%'> 
                                                            <tr>
                                                                <td align='left' valign='top' style='padding:20px;background:#fff'>
                                                                    <table border='0' cellpadding='0' cellspacing='0' style='float:left;width: 100%;margin-right:5%'>
                                                                        <tr>
                                                                            <td align='left' valign='top' width='100%' style='vertical-align: middle;' class='flexibleContainerCell'>
                                                                                <center>
                                                                                    <img style='width:110px;margin-bottom:0px;' src='" + ConfigurationManager.AppSettings["domain"] + @"/assets/imgs/logo.png' />
                                                                                </center>
                                                                            </td>
                                                                        </tr>
                                                                    </table>

                                                                </td>
                                                            </tr>
<tr>
                                                                <td align='left' valign='top' style='padding:0px;background:#ffa8ae29'>
                                                                    <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                                                        <tr>
                                                                            <td align='left' valign='top' width='100%' class='flexibleContainerCell'>
                                                                                <p style='font-size:22px;line-height:32px!important;text-align:center;color:white;margin-top:0px;background:#cd6c73;padding:10px;'>VERIFY EMAIL</p>
                                                                                <p style='font-size:20px;line-height:28px!important;text-align:center;color:#000;font-weight:bold!important'>Request for account verification</p>
                                                                                <p style='font-size:15px;color:#000;line-height:22px!important;margin-bottom:5px;text-align:center;padding:0px 20px !important'>Hello " + name + @", You have successfully registered. Please click on the link below for verification.<br><br></p>


                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr> 
                                                            <tr>
                                                               <td align='left' valign='top' style='padding:20px 60px;background:#fff;text-align: center;'> 
<a href='" + strlink + @"' style='padding: 10px 30px;background: #e7757f;text-decoration: none;color: #fff;font-size: 20px;'>Verify Email</a>
    <p style='font-size: 14px;line-height:22px!important;text-align:center;margin-top: 20px;color:#573e40;margin-bottom:30px;'>If clicking the button does not work, copy the URL below and paste it into your browser:<br>" + strlink + @"<br><br></p></td><tr>
                                                                <td align='left' valign='top' style='padding:20px 20px;background:#ffa8ae29'>
                                                                    <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                                                        <tr>
                                                                            <td align='center' valign='top' width='100%' class='flexibleContainerCell'>
                                                                                <ul style='list-style:none;width:400px;margin:0px auto;'>
                                                                                    <li style='display:inline-block;margin-right:10px;border-right:2px solid #ccc;padding-right:14px;'>
                                                                                        <a href='https://www.facebook.com/Archidplydecor.bonvivantpages/category/Brand/Archidply Decor-1404212866358535/' target='_blank'><img width='30' src='" + ConfigurationManager.AppSettings["domain"] + @"/images_/facebook.png' /></a>
                                                                                    </li>
                                                                                    <li style='display:inline-block;margin-right:10px;border-right:2px solid #ccc;padding-right:14px;'>
                                                                                        <a href='https://www.youtube.com/channel/UCs3BrrlAc5K93z3jrlYVJZw' target='_blank'> <img width='30' src='" + ConfigurationManager.AppSettings["domain"] + @"/images_/mail.png' /></a>
                                                                                    </li>
                                                                                    <li style='display:inline-block;margin-right:10px;border-right:2px solid #ccc;padding-right:14px;'>
                                                                                        <a href='https://www.instagram.com/archidplydecor_bonvivant/Archidply Decorlifestyles/?hl=en' target='_blank'><img width='30' src='" + ConfigurationManager.AppSettings["domain"] + @"/images_/instagram.png' /></a>
                                                                                    </li>
                                                                                    <li style='display:inline-block;margin-right:0px;'>
                                                                                        <a href='https://twitter.com/Archidply DecorRetail' target='_blank'><img width='30' src='" + ConfigurationManager.AppSettings["domain"] + @"/images_/twitter.png' /></a>
                                                                                    </li>
                                                                                </ul>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </center>
</body>
</html>";


            #endregion

            MailMessage mail = new MailMessage();
            mail.To.Add(email);
            mail.From = new MailAddress(ConfigurationManager.AppSettings["from"], ConfigurationManager.AppSettings["fromName"]);
            mail.Subject = "Archidply Decor Account verification";
            mail.Body = mailBody1;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["host"];
            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            smtp.Credentials = new System.Net.NetworkCredential
                           (ConfigurationManager.AppSettings["userName"], ConfigurationManager.AppSettings["password"]);
            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSsl"]);
            smtp.Send(mail);
            return 1;
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "SendEmailVerifyLink", ex.Message);
            return 0;
        }
    }
    public static int SendPasswordResetUser(string name, string email, string custId)
    {
        try
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(email);
            mail.From = new MailAddress(ConfigurationManager.AppSettings["from"], ConfigurationManager.AppSettings["fromName"]);
            mail.Subject = "Password Reset Request";
            #region mailBody

            string mailBody = @"<!DOCTYPE HTML PUBLIC '-//W3C//DTD XHTML 1.0 Strict//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd'>
<html xmlns='http://www.w3.org/1999/xhtml'>
<head>
    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
    <meta name='viewport' content='width=device-width, initial-scale=1.0' />
    <meta http-equiv='X-UA-Compatible' content='IE=edge,chrome=1' />
    <meta name='format-detection' content='telephone=no' />
    <title>ARCHIDPLY DECOR</title>
    <link href='https://fonts.googleapis.com/css?family=Lato:100,100i,300,300i,400,400i,700,700i,900,900i&display=swap' rel='stylesheet' />
    <style type='text/css'>
        html {
            background-color: #FFF;
            margin: 0;
            font-family: 'Lato', sans-serif;
            padding: 0;
        }

        /*@import url('https://fonts.googleapis.com/css?family=Roboto');*/

        body, #bodyTable, #bodyCell, #bodyCell {
            height: 100% !important;
            margin: 0;
            padding: 0;
            width: 100% !important;
        }

        table {
            border-collapse: collapse;
        }

            table[id=bodyTable] {
                width: 100% !important;
                margin: auto;
                max-width: 500px !important;
                color: #212121;
                font-weight: normal;
            }

        img, a img {
            border: 0;
            outline: none;
            text-decoration: none;
            height: auto;
            line-height: 100%;
        }

        /*a {
            text-decoration: none !important;
            border-bottom: 1px solid;
        }*/

        h1, h2, h3, h4, h5, h6 {
            color: #5F5F5F;
            font-weight: normal;
            font-size: 20px;
            line-height: 125%;
            text-align: Left;
            letter-spacing: normal;
            margin-top: 0;
            margin-right: 0;
            margin-bottom: 10px;
            margin-left: 0;
            padding-top: 0;
            padding-bottom: 0;
            padding-left: 0;
            padding-right: 0;
        }

        .ReadMsgBody {
            width: 100%;
        }

        .ExternalClass {
            width: 100%;
        }

            .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {
                line-height: 100%;
            }

        table, td {
            mso-table-lspace: 0pt;
            mso-table-rspace: 0pt;
        }

        #outlook a {
            padding: 0;
        }

        img {
            -ms-interpolation-mode: bicubic;
            display: block;
            outline: none;
            text-decoration: none;
        }

        body, table, td, p, a, li, blockquote {
            -ms-text-size-adjust: 100%;
            -webkit-text-size-adjust: 100%;
            font-weight: 500 !important;
        }

        .ExternalClass td[class='ecxflexibleContainerBox'] h3 {
            padding-top: 10px !important;
        }

        h1 {
            display: block;
            font-size: 26px;
            font-style: normal;
            font-weight: normal;
            line-height: 100%;
        }

        h2 {
            display: block;
            font-size: 20px;
            font-style: normal;
            font-weight: normal;
            line-height: 120%;
        }

        h3 {
            display: block;
            font-size: 17px;
            font-style: normal;
            font-weight: normal;
            line-height: 110%;
        }

        h4 {
            display: block;
            font-size: 18px;
            font-style: italic;
            font-weight: normal;
            line-height: 100%;
        }

        .flexibleImage {
            height: auto;
        }

        .linkRemoveBorder {
            border-bottom: 0 !important;
        }

        table[class=flexibleContainerCellDivider] {
            padding-bottom: 0 !important;
            padding-top: 0 !important;
        }

        body, #bodyTable {
            background-color: #E1E1E1;
        }

        #emailHeader {
            background-color: #fff;
        }

        #emailBody {
            background-color: #FFFFFF;
        }

        #emailFooter {
            background-color: #E1E1E1;
        }

        .nestedContainer {
            background-color: #F8F8F8;
            border: 1px solid #CCCCCC;
        }

        .emailButton {
            background-color: #205478;
            border-collapse: separate;
        }

        .buttonContent {
            color: #FFFFFF;
            font-family: Helvetica;
            font-size: 18px;
            font-weight: bold;
            line-height: 100%;
            padding: 15px;
            text-align: center;
        }

            .buttonContent a {
                color: #FFFFFF;
                display: block;
                text-decoration: none !important;
                border: 0 !important;
            }

        .emailCalendar {
            background-color: #FFFFFF;
            border: 1px solid #CCCCCC;
        }

        .emailCalendarMonth {
            background-color: #205478;
            color: #FFFFFF;
            font-size: 16px;
            font-weight: bold;
            padding-top: 10px;
            padding-bottom: 10px;
            text-align: center;
        }

        .emailCalendarDay {
            color: #205478;
            font-size: 60px;
            font-weight: bold;
            line-height: 100%;
            padding-top: 20px;
            padding-bottom: 20px;
            text-align: center;
        }

        .imageContentText {
            margin-top: 10px;
            line-height: 0;
        }

            .imageContentText a {
                line-height: 0;
            }

        #invisibleIntroduction {
            display: none !important;
        }

        span[class=ios-color-hack] a {
            color: #275100 !important;
            text-decoration: none !important;
        }

        span[class=ios-color-hack2] a {
            color: #205478 !important;
            text-decoration: none !important;
        }

        span[class=ios-color-hack3] a {
            color: #8B8B8B !important;
            text-decoration: none !important;
        }

        .a[href^='tel'], a[href^='sms'] {
            text-decoration: none !important;
            color: #606060 !important;
            pointer-events: none !important;
            cursor: default !important;
        }

        .mobile_link a[href^='tel'], .mobile_link a[href^='sms'] {
            text-decoration: none !important;
            color: #606060 !important;
            pointer-events: auto !important;
            cursor: default !important;
        }

        @media only screen and (max-width: 480px) {
            body {
                width: 100% !important;
                min-width: 100% !important;
            }

            table[id='emailHeader'],
            table[id='emailBody'],
            table[id='emailFooter'],
            table[class='flexibleContainer'],
            td[class='flexibleContainerCell'] {
                width: 100% !important;
            }

            td[class='flexibleContainerBox'], td[class='flexibleContainerBox'] table {
                display: block;
                width: 100%;
                text-align: left;
            }

            td[class='imageContent'] img {
                height: auto !important;
                width: 100% !important;
                max-width: 100% !important;
            }

            img[class='flexibleImage'] {
                height: auto !important;
                width: 100% !important;
                max-width: 100% !important;
            }

            img[class='flexibleImageSmall'] {
                height: auto !important;
                width: auto !important;
            }

            table[class='flexibleContainerBoxNext'] {
                padding-top: 10px !important;
            }

            table[class='emailButton'] {
                width: 100% !important;
            }

            td[class='buttonContent'] {
                padding: 0 !important;
            }

                td[class='buttonContent'] a {
                    padding: 15px !important;
                }
        }

        @media only screen and (-webkit-device-pixel-ratio:.75) {
        }

        @media only screen and (-webkit-device-pixel-ratio:1) {
        }

        @media only screen and (-webkit-device-pixel-ratio:1.5) {
        }

        @media only screen and (min-device-width : 320px) and (max-device-width:568px) {
        }

        .blink_text {
            -webkit-animation-name: blinker;
            -webkit-animation-duration: 2s;
            -webkit-animation-timing-function: linear;
            -webkit-animation-iteration-count: infinite;
            -moz-animation-name: blinker;
            -moz-animation-duration: 2s;
            -moz-animation-timing-function: linear;
            -moz-animation-iteration-count: infinite;
            animation-name: blinker;
            animation-duration: 2s;
            animation-timing-function: linear;
            animation-iteration-count: infinite;
            color: white;
        }

        @-moz-keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        @-webkit-keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        @keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }
    </style>
</head>
<body bgcolor='#E1E1E1' leftmargin='0' marginwidth='0' topmargin='0' marginheight='0' offset='0'>
    <center style='background-color:#E1E1E1;'>
        <table border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' id='bodyTable' style=' table-layout: fixed; max-width:100% !important;width: 100% !important;min-width: 100% !important;'>
            <tr>
                <td align='center' valign='top' id='bodyCell'>
                    <table bgcolor='#FFFFFF' border='0' cellpadding='0' cellspacing='0' width='700' id='emailBody' style='margin-top:20px;margin-bottom:20px;'>
                        <tr>
                            <td align='center' valign='top' style=''>
                                <table border='0' cellpadding='0' cellspacing='0' width='100%' bgcolor='#fff'>
                                    <tr>
                                        <td align='center' valign='top'>
                                            <table border='0' cellpadding='0' cellspacing='0' width='700' class='flexibleContainer'>
                                                <tr>
                                                    <td align='center' valign='top' width='700' class='flexibleContainerCell'>
                                                        <table border='0' cellpadding='30' cellspacing='0' width='100%'>
                                                            <tr>
                                                                <td align='left' valign='top' style='padding:10px;background:#fcfcfc'>
                                                                    <table border='0' cellpadding='0' cellspacing='0' style='float:left;float: left; margin-right: 0; width: 100%;'>
                                                                        <tr>
                                                                            <td align='left' valign='top' width='100%' class='flexibleContainerCell'>
                                                                                <center><img style='height:50px;margin-bottom:0px;' src='" + ConfigurationManager.AppSettings["domain"] + @"/assets/imgs/logo.png' /></center>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align='left' valign='top' style='padding:0px;'>
                                                                    <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                                                        <tr>
                                                                            <td align='left' valign='top' width='100%' class='flexibleContainerCell'>
                                                                                <p style='font-size:22px;line-height:28px!important;text-align:center;color:#111;font-weight:bold!important'>PASSWORD RESET</p>
                                                                                <p style='font-size:13px;color:#573e40;line-height:24px!important;margin-bottom:30px;text-align:center;padding:0px 50px;'>
                                                                                    Hello " + name + @"- We just received a request to reset your ARCHIDPLY DECOR account password
                                                                                </p>
                                                                                <p style='font-size:13px;color:#573e40;line-height:24px!important;margin-bottom:20px;padding:0px 50px;'>
                                                                                    Once you have made this request, please click the link below to reset your password
                                                                                </p>
                                                                                <br />
                                                                                <p style='margin-bottom:10px;'><center> <a href='" + ConfigurationManager.AppSettings["domain"] + @"/reset-password.aspx?c=" + custId + @"' target='_blank' style='font-size:26px;line-height:32px!important;text-decoration:none;color:white;margin-top:0px;background:#007FFF;padding:10px;width:50%;margin:0px auto'>Reset my account password</a></center></p>
                                                                                <br />
                                                                                <p style='font-size:13px;color:#573e40;line-height:24px!important;margin-bottom:10px;padding:0px 50px;'>
                                                                                    If clicking the button doesn’t work , copy paste the below url in your browser :
                                                                                </p>
                                                                                <p style='padding:0px 50px;margin-bottom:30px;'><a target='_blank' href='" + ConfigurationManager.AppSettings["domain"] + @"/reset-password.aspx?c=" + custId + @"'> " + ConfigurationManager.AppSettings["domain"] + @"/reset-password.aspx?c=" + custId + @"</a> </p>
                                                                                <p style='padding:0px 50px;margin-bottom:30px;'>If you did not request for password reset , please ignore this mail.<br />Your password will remain unchanged.</p>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align='left' valign='top' style='padding-top:0px;'>
                                                                    <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                                                        <tr style='margin-bottom:15px;'>
                                                                            <td align='left' valign='top' width='100%' class='flexibleContainerCell'>
                                                                                <p style='font-size:13px;line-height:24px!important;padding:0px 20px'>
                                                                                    If you have any queries about your account or any other matter, please feel free to contact us at   <a href='mailto:" + ConfigurationManager.AppSettings["from"] + "'>" + ConfigurationManager.AppSettings["from"] + "</a> or by visiting our <a href='" + ConfigurationManager.AppSettings["domain"] + @"' target='_blank'>Home</a> page.
                                                                                </p>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        <tr>
                                        <td align='left' valign='top' style='padding:10px 20px;background:#f2f2f2'>
                                            <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                                  <tr>
                                                                               <td align='center' valign='middle' width='100%' class='flexibleContainerCell'>
                                                                                    <ul style='list-style:none;width:390px;margin:0px auto;'>
                                                                                        <li style='display:none;margin-right:5px;padding-right:5px;'>
                                                                                            <a href='https://www.facebook.com/Archidplydecor.bonvivant' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/images_/facebook.png' /></a>
                                                                                        </li>
                                                                                        <li style='display:none;margin-right:5px;padding-right:5px;'>
                                                                                            <a href='https://www.instagram.com/archidplydecor_bonvivant/' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/images_/instagram.png' /></a>
                                                                                        </li>
                                                                                        <li style='display:none;margin-right:0px;padding-right:5px;'>
                                                                                            <a href='mailto:bangalore@archidply.com' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/images_/mail.png' /></a>
                                                                                        </li>

                                                                                    </ul>
                                                                                </td>

                                                                            </tr> 
                                            </table>
                                        </td>
                                    </tr> 
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>  
    </center>
</body>
</html>";

            #endregion
            mail.Body = mailBody;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["host"];
            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            smtp.Credentials = new System.Net.NetworkCredential
                   (ConfigurationManager.AppSettings["userName"], ConfigurationManager.AppSettings["password"]);
            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSsl"]);
            smtp.Send(mail);
            return 1;
        }
        catch (Exception exx)
        {
            return 0;
        }
    }


    public static int SendRegistered(string name, string emails)
    {
        try
        {

            #region mailBodys
            string mailBody = @"<!DOCTYPE HTML PUBLIC '-//W3C//DTD XHTML 1.0 Strict//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd'>
<html xmlns='http://www.w3.org/1999/xhtml'>
<head>
    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
    <meta name='viewport' content='width=device-width, initial-scale=1.0' />
    <meta http-equiv='X-UA-Compatible' content='IE=edge,chrome=1' />
    <meta name='format-detection' content='telephone=no' />
    <title>ARCHIDPLY DECOR</title>
    <link href='https://fonts.googleapis.com/css?family=Lato:100,100i,300,300i,400,400i,700,700i,900,900i&display=swap' rel='stylesheet' />
    <style type='text/css'>
        html {
            background-color: #FFF;
            margin: 0;
            font-family: 'Lato', sans-serif;
            padding: 0;
        }

        /*@import url('https://fonts.googleapis.com/css?family=Roboto');*/

        body, #bodyTable, #bodyCell, #bodyCell {
            height: 100% !important;
            margin: 0;
            padding: 0;
            width: 100% !important;
        }

        table {
            border-collapse: collapse;
        }

            table[id=bodyTable] {
                width: 100% !important;
                margin: auto;
                max-width: 500px !important;
                color: #212121;
                font-weight: normal;
            }

        img, a img {
            border: 0;
            outline: none;
            text-decoration: none;
            height: auto;
            line-height: 100%;
        }

        /*a {
            text-decoration: none !important;
            border-bottom: 1px solid;
        }*/

        h1, h2, h3, h4, h5, h6 {
            color: #5F5F5F;
            font-weight: normal;
            font-size: 20px;
            line-height: 125%;
            text-align: Left;
            letter-spacing: normal;
            margin-top: 0;
            margin-right: 0;
            margin-bottom: 10px;
            margin-left: 0;
            padding-top: 0;
            padding-bottom: 0;
            padding-left: 0;
            padding-right: 0;
        }

        .ReadMsgBody {
            width: 100%;
        }

        .ExternalClass {
            width: 100%;
        }

            .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {
                line-height: 100%;
            }

        table, td {
            mso-table-lspace: 0pt;
            mso-table-rspace: 0pt;
        }

        #outlook a {
            padding: 0;
        }

        img {
            -ms-interpolation-mode: bicubic;
            display: block;
            outline: none;
            text-decoration: none;
        }

        body, table, td, p, a, li, blockquote {
            -ms-text-size-adjust: 100%;
            -webkit-text-size-adjust: 100%;
            font-weight: 500 !important;
        }

        .ExternalClass td[class='ecxflexibleContainerBox'] h3 {
            padding-top: 10px !important;
        }

        h1 {
            display: block;
            font-size: 26px;
            font-style: normal;
            font-weight: normal;
            line-height: 100%;
        }

        h2 {
            display: block;
            font-size: 20px;
            font-style: normal;
            font-weight: normal;
            line-height: 120%;
        }

        h3 {
            display: block;
            font-size: 17px;
            font-style: normal;
            font-weight: normal;
            line-height: 110%;
        }

        h4 {
            display: block;
            font-size: 18px;
            font-style: italic;
            font-weight: normal;
            line-height: 100%;
        }

        .flexibleImage {
            height: auto;
        }

        .linkRemoveBorder {
            border-bottom: 0 !important;
        }

        table[class=flexibleContainerCellDivider] {
            padding-bottom: 0 !important;
            padding-top: 0 !important;
        }

        body, #bodyTable {
            background-color: #E1E1E1;
        }

        #emailHeader {
            background-color: #fff;
        }

        #emailBody {
            background-color: #FFFFFF;
        }

        #emailFooter {
            background-color: #E1E1E1;
        }

        .nestedContainer {
            background-color: #F8F8F8;
            border: 1px solid #CCCCCC;
        }

        .emailButton {
            background-color: #205478;
            border-collapse: separate;
        }

        .buttonContent {
            color: #FFFFFF;
            font-family: Helvetica;
            font-size: 18px;
            font-weight: bold;
            line-height: 100%;
            padding: 15px;
            text-align: center;
        }

            .buttonContent a {
                color: #FFFFFF;
                display: block;
                text-decoration: none !important;
                border: 0 !important;
            }

        .emailCalendar {
            background-color: #FFFFFF;
            border: 1px solid #CCCCCC;
        }

        .emailCalendarMonth {
            background-color: #205478;
            color: #FFFFFF;
            font-size: 16px;
            font-weight: bold;
            padding-top: 10px;
            padding-bottom: 10px;
            text-align: center;
        }

        .emailCalendarDay {
            color: #205478;
            font-size: 60px;
            font-weight: bold;
            line-height: 100%;
            padding-top: 20px;
            padding-bottom: 20px;
            text-align: center;
        }

        .imageContentText {
            margin-top: 10px;
            line-height: 0;
        }

            .imageContentText a {
                line-height: 0;
            }

        #invisibleIntroduction {
            display: none !important;
        }

        span[class=ios-color-hack] a {
            color: #275100 !important;
            text-decoration: none !important;
        }

        span[class=ios-color-hack2] a {
            color: #205478 !important;
            text-decoration: none !important;
        }

        span[class=ios-color-hack3] a {
            color: #8B8B8B !important;
            text-decoration: none !important;
        }

        .a[href^='tel'], a[href^='sms'] {
            text-decoration: none !important;
            color: #606060 !important;
            pointer-events: none !important;
            cursor: default !important;
        }

        .mobile_link a[href^='tel'], .mobile_link a[href^='sms'] {
            text-decoration: none !important;
            color: #606060 !important;
            pointer-events: auto !important;
            cursor: default !important;
        }

        @media only screen and (max-width: 480px) {
            body {
                width: 100% !important;
                min-width: 100% !important;
            }

            table[id='emailHeader'],
            table[id='emailBody'],
            table[id='emailFooter'],
            table[class='flexibleContainer'],
            td[class='flexibleContainerCell'] {
                width: 100% !important;
            }

            td[class='flexibleContainerBox'], td[class='flexibleContainerBox'] table {
                display: block;
                width: 100%;
                text-align: left;
            }

            td[class='imageContent'] img {
                height: auto !important;
                width: 100% !important;
                max-width: 100% !important;
            }

            img[class='flexibleImage'] {
                height: auto !important;
                width: 100% !important;
                max-width: 100% !important;
            }

            img[class='flexibleImageSmall'] {
                height: auto !important;
                width: auto !important;
            }

            table[class='flexibleContainerBoxNext'] {
                padding-top: 10px !important;
            }

            table[class='emailButton'] {
                width: 100% !important;
            }

            td[class='buttonContent'] {
                padding: 0 !important;
            }

                td[class='buttonContent'] a {
                    padding: 15px !important;
                }
        }

        @media only screen and (-webkit-device-pixel-ratio:.75) {
        }

        @media only screen and (-webkit-device-pixel-ratio:1) {
        }

        @media only screen and (-webkit-device-pixel-ratio:1.5) {
        }

        @media only screen and (min-device-width : 320px) and (max-device-width:568px) {
        }

        .blink_text {
            -webkit-animation-name: blinker;
            -webkit-animation-duration: 2s;
            -webkit-animation-timing-function: linear;
            -webkit-animation-iteration-count: infinite;
            -moz-animation-name: blinker;
            -moz-animation-duration: 2s;
            -moz-animation-timing-function: linear;
            -moz-animation-iteration-count: infinite;
            animation-name: blinker;
            animation-duration: 2s;
            animation-timing-function: linear;
            animation-iteration-count: infinite;
            color: white;
        }

        @-moz-keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        @-webkit-keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        @keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }
    </style>
</head>
<body bgcolor='#E1E1E1' leftmargin='0' marginwidth='0' topmargin='0' marginheight='0' offset='0'>
    <center style='background-color:#E1E1E1;'>
        <table border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' id='bodyTable' style=' table-layout: fixed; max-width:100% !important;width: 100% !important;min-width: 100% !important;'>
            <tr>
                <td align='center' valign='top' id='bodyCell'>
                    <table bgcolor='#FFFFFF' border='0' cellpadding='0' cellspacing='0' width='700' id='emailBody' style='margin-top:20px;margin-bottom:20px;'>
                        <tr>
                            <td align='center' valign='top' style=''>
                                <table border='0' cellpadding='0' cellspacing='0' width='100%' bgcolor='#fff'>
                                    <tr>
                                        <td align='center' valign='top'>
                                            <table border='0' cellpadding='0' cellspacing='0' width='700' class='flexibleContainer'>
                                                <tr>
                                                    <td align='center' valign='top' width='700' class='flexibleContainerCell'>
                                                        <table border='0' cellpadding='30' cellspacing='0' width='100%'>
                                                            <tr>
                                                                <td align='left' valign='top' style='padding:10px;background:#fcfcfc'>
                                                                    <table border='0' cellpadding='0' cellspacing='0' style='float:left;float: left; margin-right: 0; width: 100%;'>
                                                                        <tr>
                                                                            <td align='left' valign='top' width='100%' class='flexibleContainerCell'>
                                                                                <center><img style='height:50px;margin-bottom:0px;' src='" + ConfigurationManager.AppSettings["domain"] + @"/assets/imgs/logo.png' /></center>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align='left' valign='top' style='padding:0px;'>
                                                                    <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                                                        <tr>
                                                                            <td align='left' valign='top' width='100%' class='flexibleContainerCell'>
                                                                                <p style='font-size:22px;line-height:28px!important;text-align:center;color:#111;font-weight:bold!important'>YOUR ACCOUNT IS CREATED!</p>
                                                                                <p style='font-size:16px;color:#573e40;line-height:24px!important;margin-bottom:30px;text-align:center;padding:0px 50px;'>
                                                                                    Hi " + name + @"<br><br>We thank you for your registration on " + ConfigurationManager.AppSettings["domain"] + @", we are happy that you are here in ARCHIDPLY DECOR family.</p>
                                                                                <p style='font-size:16px;color:#573e40;line-height:24px!important;margin-bottom:30px;text-align:center;padding:0px 50px;'>
                                                                                    Please follow us on our social media page to check our products & new arrivals, special discount & offers for you, Stay connected & Warm Regards,
                                                                                </p>
                                                                                <p style='font-size:16px;color:#573e40;line-height:24px!important;margin-bottom:30px;text-align:center;padding:0px 50px;'>Team Archidply Decor<br>" + ConfigurationManager.AppSettings["domain"] + @"</p>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr> 
                                                            <tr>
                                        <td align='left' valign='top' style='padding:10px 20px;background:#f2f2f2'>
                                            <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                                  <tr>
                                                                               <td align='center' valign='middle' width='100%' class='flexibleContainerCell'>
                                                                                    <ul style='list-style:none;width:390px;margin:0px auto;'>
                                                                                        <li style='display:none;margin-right:5px;padding-right:5px;'>
                                                                                            <a href='https://www.facebook.com/Archidplydecor.bonvivant' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/img/email-icons/facebook.png' /></a>
                                                                                        </li>
                                                                                        <li style='display:none;margin-right:5px;padding-right:5px;'>
                                                                                            <a href='https://www.instagram.com/archidplydecor_bonvivant/' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/img/email-icons/instagram.png' /></a>
                                                                                        </li>
                                                                                        <li style='display:none;margin-right:0px;padding-right:5px;'>
                                                                                            <a href='mailto:bangalore@archidply.com' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/img/email-icons/mail.png' /></a>
                                                                                        </li>

                                                                                    </ul>
                                                                                </td>

                                                                            </tr> 
                                            </table>
                                        </td>
                                    </tr> 
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table> 
    </center>
</body>
</html>";
            #endregion
            MailMessage mail = new MailMessage();
            mail.To.Add(emails);
            mail.From = new MailAddress(ConfigurationManager.AppSettings["from"], ConfigurationManager.AppSettings["fromName"]);
            mail.Subject = "Welcome to ARCHIDPLY DECOR - " + name;
            mail.Body = mailBody;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["host"];
            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            smtp.Credentials = new System.Net.NetworkCredential
                           (ConfigurationManager.AppSettings["userName"], ConfigurationManager.AppSettings["password"]);
            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSsl"]);
            smtp.Send(mail);
            return 1;
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "SendRegistered", ex.Message);
            return 0;
        }
    }
    public static int ShiprocketOrderFailedRequest(string orderId)
    {
        try
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(ConfigurationManager.AppSettings["ToMail"]);
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["CCMail"]))
            {
                mail.CC.Add(ConfigurationManager.AppSettings["CCMail"]);
            }
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["BCCMail"]))
            {
                mail.Bcc.Add(ConfigurationManager.AppSettings["BCCMail"]);
            }
            mail.From = new MailAddress(ConfigurationManager.AppSettings["from"], ConfigurationManager.AppSettings["fromName"]);

            mail.Subject = "Shiprocket Order Failure - Archidply ( " + DateTime.Now + " )";
            mail.Body = "Hi Admin, <br><br>" + orderId + @" shiprocket order creation failed!<br>Regards,<br>Archidply";
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["host"];
            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            smtp.Credentials = new System.Net.NetworkCredential
                           (ConfigurationManager.AppSettings["userName"], ConfigurationManager.AppSettings["password"]);
            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSsl"]);
            smtp.Send(mail);
            return 1;
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "ShiprocketOrderFailedRequest", ex.Message);
            return 0;
        }
    }
    public static int ContactRequest(ContactUs con)
    {
        try
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(ConfigurationManager.AppSettings["Mail"]);
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["CCMail"]))
            {
                mail.CC.Add(ConfigurationManager.AppSettings["CCMail"]);
            }
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["BCCMail"]))
            {
                mail.Bcc.Add(ConfigurationManager.AppSettings["BCCMail"]);
            }
            mail.From = new MailAddress(ConfigurationManager.AppSettings["from"], ConfigurationManager.AppSettings["fromName"]);

            mail.Subject = "Contact Us Request - Archidply ( " + DateTime.Now + " )";
            mail.Body = "Hi Admin, <br><br>You have received a contactus request from " + con.UserName + ".<br><br><u><b><i>Details : </i></b></u><br>Name : " + con.UserName + "<br>Email-Id : " + con.EmailId + "<br>Mobile : " + con.ContactNo + "<br>Message : " + con.Message + "<br>Regards,<br>Archidply";
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["host"];
            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            smtp.Credentials = new System.Net.NetworkCredential
                           (ConfigurationManager.AppSettings["userName"], ConfigurationManager.AppSettings["password"]);
            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSsl"]);
            smtp.Send(mail);
            return 1;
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "ContactRequest", ex.Message);
            return 0;
        }
    }


    public static int BookingConfirmed(string oid, string productTable, string name, string email, string email1, string paidAmount, string pType, string address1, string address2)
    {
        try
        {
            #region mailBody
            string mailBody = @"<!DOCTYPE HTML PUBLIC '-//W3C//DTD XHTML 1.0 Strict//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd'>
<html xmlns='http://www.w3.org/1999/xhtml'>
<head>
    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
    <meta name='viewport' content='width=device-width, initial-scale=1.0' />
    <meta http-equiv='X-UA-Compatible' content='IE=edge,chrome=1' />
    <meta name='format-detection' content='telephone=no' />
    <title>Bogmalo Foods & Hospitality</title>
    <link rel='shortcut icon' href='http://nextwebi.com/emailtemplate/focuslay/logo.png' />
    <link href='https://fonts.googleapis.com/css?family=Lato:100,100i,300,300i,400,400i,700,700i,900,900i&display=swap' rel='stylesheet' />
    <style type='text/css'>
        html {
            background-color: #FFF;
            margin: 0;
            font-family: 'Lato', sans-serif;
            padding: 0;
        }

        /*@import url('https://fonts.googleapis.com/css?family=Roboto');*/

        body, #bodyTable, #bodyCell, #bodyCell {
            height: 100% !important;
            margin: 0;
            padding: 0;
            width: 100% !important;
        }

        table {
            border-collapse: collapse;
        }

            table[id=bodyTable] {
                width: 100% !important;
                margin: auto;
                max-width: 500px !important;
                color: #212121;
                font-weight: normal;
            }

        img, a img {
            border: 0;
            outline: none;
            text-decoration: none;
            height: auto;
            line-height: 100%;
        }

        /*a {
            text-decoration: none !important;
            border-bottom: 1px solid;
        }*/

        h1, h2, h3, h4, h5, h6 {
            color: #5F5F5F;
            font-weight: normal;
            font-size: 20px;
            line-height: 125%;
            text-align: Left;
            letter-spacing: normal;
            margin-top: 0;
            margin-right: 0;
            margin-bottom: 10px;
            margin-left: 0;
            padding-top: 0;
            padding-bottom: 0;
            padding-left: 0;
            padding-right: 0;
        }

        .ReadMsgBody {
            width: 100%;
        }

        .ExternalClass {
            width: 100%;
        }

            .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {
                line-height: 100%;
            }

        table, td {
            mso-table-lspace: 0pt;
            mso-table-rspace: 0pt;
        }

        #outlook a {
            padding: 0;
        }

        img {
            -ms-interpolation-mode: bicubic;
            display: block;
            outline: none;
            text-decoration: none;
        }

        body, table, td, p, a, li, blockquote {
            -ms-text-size-adjust: 100%;
            -webkit-text-size-adjust: 100%;
            font-weight: 500 !important;
        }

        .ExternalClass td[class='ecxflexibleContainerBox'] h3 {
            padding-top: 10px !important;
        }

        h1 {
            display: block;
            font-size: 26px;
            font-style: normal;
            font-weight: normal;
            line-height: 100%;
        }

        h2 {
            display: block;
            font-size: 20px;
            font-style: normal;
            font-weight: normal;
            line-height: 120%;
        }

        h3 {
            display: block;
            font-size: 17px;
            font-style: normal;
            font-weight: normal;
            line-height: 110%;
        }

        h4 {
            display: block;
            font-size: 18px;
            font-style: italic;
            font-weight: normal;
            line-height: 100%;
        }

        .flexibleImage {
            height: auto;
        }

        .linkRemoveBorder {
            border-bottom: 0 !important;
        }

        table[class=flexibleContainerCellDivider] {
            padding-bottom: 0 !important;
            padding-top: 0 !important;
        }

        body, #bodyTable {
            background-color: #E1E1E1;
        }

        #emailHeader {
            background-color: #fff;
        }

        #emailBody {
            background-color: #FFFFFF;
        }

        #emailFooter {
            background-color: #E1E1E1;
        }

        .nestedContainer {
            background-color: #F8F8F8;
            border: 1px solid #CCCCCC;
        }

        .emailButton {
            background-color: #205478;
            border-collapse: separate;
        }

        .buttonContent {
            color: #FFFFFF;
            font-family: Helvetica;
            font-size: 18px;
            font-weight: bold;
            line-height: 100%;
            padding: 15px;
            text-align: center;
        }

            .buttonContent a {
                color: #FFFFFF;
                display: block;
                text-decoration: none !important;
                border: 0 !important;
            }

        .emailCalendar {
            background-color: #FFFFFF;
            border: 1px solid #CCCCCC;
        }

        .emailCalendarMonth {
            background-color: #205478;
            color: #FFFFFF;
            font-size: 16px;
            font-weight: bold;
            padding-top: 10px;
            padding-bottom: 10px;
            text-align: center;
        }

        .emailCalendarDay {
            color: #205478;
            font-size: 60px;
            font-weight: bold;
            line-height: 100%;
            padding-top: 20px;
            padding-bottom: 20px;
            text-align: center;
        }

        .imageContentText {
            margin-top: 10px;
            line-height: 0;
        }

            .imageContentText a {
                line-height: 0;
            }

        #invisibleIntroduction {
            display: none !important;
        }

        span[class=ios-color-hack] a {
            color: #275100 !important;
            text-decoration: none !important;
        }

        span[class=ios-color-hack2] a {
            color: #205478 !important;
            text-decoration: none !important;
        }

        span[class=ios-color-hack3] a {
            color: #8B8B8B !important;
            text-decoration: none !important;
        }

        .a[href^='tel'], a[href^='sms'] {
            text-decoration: none !important;
            color: #606060 !important;
            pointer-events: none !important;
            cursor: default !important;
        }

        .mobile_link a[href^='tel'], .mobile_link a[href^='sms'] {
            text-decoration: none !important;
            color: #606060 !important;
            pointer-events: auto !important;
            cursor: default !important;
        }

        @media only screen and (max-width: 480px) {
            body {
                width: 100% !important;
                min-width: 100% !important;
            }

            table[id='emailHeader'],
            table[id='emailBody'],
            table[id='emailFooter'],
            table[class='flexibleContainer'],
            td[class='flexibleContainerCell'] {
                width: 100% !important;
            }

            td[class='flexibleContainerBox'], td[class='flexibleContainerBox'] table {
                display: block;
                width: 100%;
                text-align: left;
            }

            td[class='imageContent'] img {
                height: auto !important;
                width: 100% !important;
                max-width: 100% !important;
            }

            img[class='flexibleImage'] {
                height: auto !important;
                width: 100% !important;
                max-width: 100% !important;
            }

            img[class='flexibleImageSmall'] {
                height: auto !important;
                width: auto !important;
            }

            table[class='flexibleContainerBoxNext'] {
                padding-top: 10px !important;
            }

            table[class='emailButton'] {
                width: 100% !important;
            }

            td[class='buttonContent'] {
                padding: 0 !important;
            }

                td[class='buttonContent'] a {
                    padding: 15px !important;
                }
        }

        @media only screen and (-webkit-device-pixel-ratio:.75) {
        }

        @media only screen and (-webkit-device-pixel-ratio:1) {
        }

        @media only screen and (-webkit-device-pixel-ratio:1.5) {
        }

        @media only screen and (min-device-width : 320px) and (max-device-width:568px) {
        }

        .blink_text {
            -webkit-animation-name: blinker;
            -webkit-animation-duration: 2s;
            -webkit-animation-timing-function: linear;
            -webkit-animation-iteration-count: infinite;
            -moz-animation-name: blinker;
            -moz-animation-duration: 2s;
            -moz-animation-timing-function: linear;
            -moz-animation-iteration-count: infinite;
            animation-name: blinker;
            animation-duration: 2s;
            animation-timing-function: linear;
            animation-iteration-count: infinite;
            color: white;
        }

        @-moz-keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        @-webkit-keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        @keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }
    </style>
</head>
<body bgcolor='#E1E1E1' leftmargin='0' marginwidth='0' topmargin='0' marginheight='0' offset='0'>
    <center style='background-color:#E1E1E1;'>
        <table border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' id='bodyTable' style=' table-layout: fixed; max-width:100% !important;width: 100% !important;min-width: 100% !important;'>
            <tr>
                <td align='center' valign='top' id='bodyCell'>
 
                    <table bgcolor='#FFFFFF' border='0' cellpadding='0' cellspacing='0' width='700' id='emailBody' style='margin-top:20px;margin-bottom:20px;'>


                        <tr>
                            <td align='center' valign='top' style=''>
                                <table border='0' cellpadding='0' cellspacing='0' width='100%' bgcolor='#fff'>
                                    <tr>
                                        <td align='center' valign='top'>
                                            <table border='0' cellpadding='0' cellspacing='0' width='700' class='flexibleContainer'>
                                                <tr>
                                                    <td align='center' valign='top' width='700' class='flexibleContainerCell'>
                                                        <table border='0' cellpadding='30' cellspacing='0' width='100%'>



                                                            <tr>
                                                                <td align='left' valign='top' style='padding:10px;background:#fcfcfc'>
                                                                    <table border='0' cellpadding='0' cellspacing='0' style='float:left;float: left; margin-right: 0; width: 100%;'>
                                                                        <tr>
                                                                            <td align='left' valign='top' width='100%' class='flexibleContainerCell'>
                                                                                <center><img style='height:50px;margin-bottom:0px;' src='" + ConfigurationManager.AppSettings["domain"] + @"/assets/imgs/logo.png' /></center>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td align='left' valign='top' style='padding:0px;background:#fff'>
                                                        <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                                            <tr>
                                                                <td align='left' valign='top' width='100%' class='flexibleContainerCell'>
                                                                    <p style='font-size:22px;line-height:28px!important;text-align:center;color:#573e40;font-weight:bold!important'>
                                                                        Thank You for your order!
                                                                    </p>

                                                                    <p style='font-size:18px;color:#573e40;line-height:22px!important;margin-bottom:30px;text-align:left;padding:0px 50px;'>
                                                                        Hi " + name + @",
                                                                    </p>
                                                                    <p style='font-size:13px;line-height:25px!important;text-align:left;margin-top:0px;color:#573e40;margin-bottom:30px;padding:0px 50px;'>
                                                                        Thank you for shopping with us! We are here to provide you a great shopping experience with trust
                                                                        <br>we&#39;re getting your order ready to be shipped. We will notify you once dispatched.
                                                                    </p>
                                                                    <p style='font-size:26px;line-height:32px!important;text-align:center;color:white;margin-top:0px;background:#FF9212;padding:10px;width:50%;margin:0px auto;margin-bottom:0px'>Order Number: " + oid + @"</p>

                                                                </td> 
                                                            </tr> 
                                                        </table>
                                                    </td>
                                                </tr>  
                                                <tr>
                                                    <td align='left' valign='top' style='padding:0px 60px 20px 60px;background:#fff'>
                                                        <table border='0' cellpadding='0' cellspacing='0' width='100%'>


                                                            <tr>
                                                                <td align='left' valign='top' style='height:250px;float:left;width:46%;margin-bottom:10px;margin-top:20px;color:#fff;padding:10px; font-size:18px;margin-right:0.5%' bgcolor='#FF9212' class='flexibleContainerCell'>

                                                                   Billing Address
                                                                    <p style='color:#fff;text-align:left;font-size:12px;line-height:20px;'>  <br>
                                                                        " + address1 + @" 
                                                                    </p>
                                                                </td>
                                                                <td align='left' valign='top' style='height:250px;float:left;width:46%;margin-bottom:10px;margin-top:20px;text-align:left;padding:10px;color:#fff;font-size:18px;margin-left:0.5%;' bgcolor='#FF9212' class='flexibleContainerCell'>
                                                                    Delivery Address
                                                                    <p style='color:#fff;text-align:left;font-size:12px;line-height:20px;'> <br>
                                                                        " + address2 + @" 
                                                                    </p>
                                                                </td> 
                                                                <td align='left' valign='top' style='float:left;width:100%' class='flexibleContainerCell'>
                                                                    <p style='text-align:left;font-weight:600!important;margin-bottom:20px !important;font-size:16px;'>Stuff You Picked:</p>

                                                                </td>
                                                            </tr>
                                                              " + productTable + @"
                                                        </table>
                                                    </td>
                                                </tr> 
                                            </table>
                                        </td>
                                    </tr> 

                                  <tr>
                                        <td align='left' valign='top' style='padding:10px 20px;background:#f2f2f2'>
                                            <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                                  <tr>
                                                                               <td align='center' valign='middle' width='100%' class='flexibleContainerCell'>
                                                                                    <ul style='list-style:none;width:390px;margin:0px auto;'>
                                                                                        <li style='display:none;margin-right:5px;padding-right:5px;'>
                                                                                            <a href='https://www.facebook.com/Archidplydecor.bonvivant' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/img/email-icons/facebook.png' /></a>
                                                                                        </li>
                                                                                        <li style='display:none;margin-right:5px;padding-right:5px;'>
                                                                                            <a href='https://www.instagram.com/archidplydecor_bonvivant/' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/img/email-icons/instagram.png' /></a>
                                                                                        </li>
                                                                                        <li style='display:none;margin-right:0px;padding-right:5px;'>
                                                                                            <a href='mailto:bangalore@archidply.com' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/img/email-icons/mail.png' /></a>
                                                                                        </li>

                                                                                    </ul>
                                                                                </td>

                                                                            </tr> 
                                            </table>
                                        </td>
                                    </tr> 
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        
    </center>
</body>
</html>";
            #endregion
            MailMessage mail = new MailMessage();
            mail.To.Add(email);
            mail.CC.Add(email1);
            mail.From = new MailAddress(ConfigurationManager.AppSettings["from"], ConfigurationManager.AppSettings["fromName"]);
            mail.Subject = "Archidply Order Confirmation";
            mail.Body = mailBody;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["host"];
            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            smtp.Credentials = new System.Net.NetworkCredential
                   (ConfigurationManager.AppSettings["userName"], ConfigurationManager.AppSettings["password"]);
            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSsl"]);
            smtp.Send(mail);
            return 1;
        }
        catch (Exception exx)
        {
            return 0;
        }
    }
    public static int BookingConfirmedAdmin(string oid, string productTable, string name, string email, string paidAmount, string pType, string address1, string address2)
    {
        try
        {
            string amt = pType.ToLower() == "cod" ? "Payable:<br/>₹" + paidAmount + @"" : " Amount Paid <br/></b>₹" + paidAmount + @"";
            #region mailBody
            string mailBody = @"<!DOCTYPE HTML PUBLIC '-//W3C//DTD XHTML 1.0 Strict//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd'>
<html xmlns='http://www.w3.org/1999/xhtml'>
<head>
    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
    <meta name='viewport' content='width=device-width, initial-scale=1.0' />
    <meta http-equiv='X-UA-Compatible' content='IE=edge,chrome=1' />
    <meta name='format-detection' content='telephone=no' />
    <title>Archidply</title>
    <link rel='shortcut icon' href='http://nextwebi.com/emailtemplate/focuslay/logo.png' />
    <link href='https://fonts.googleapis.com/css?family=Lato:100,100i,300,300i,400,400i,700,700i,900,900i&display=swap' rel='stylesheet' />
    <style type='text/css'>
        html {
            background-color: #FFF;
            margin: 0;
            font-family: 'Lato', sans-serif;
            padding: 0;
        }

        /*@import url('https://fonts.googleapis.com/css?family=Roboto');*/

        body, #bodyTable, #bodyCell, #bodyCell {
            height: 100% !important;
            margin: 0;
            padding: 0;
            width: 100% !important;
        }

        table {
            border-collapse: collapse;
        }

            table[id=bodyTable] {
                width: 100% !important;
                margin: auto;
                max-width: 500px !important;
                color: #212121;
                font-weight: normal;
            }

        img, a img {
            border: 0;
            outline: none;
            text-decoration: none;
            height: auto;
            line-height: 100%;
        }

        /*a {
            text-decoration: none !important;
            border-bottom: 1px solid;
        }*/

        h1, h2, h3, h4, h5, h6 {
            color: #5F5F5F;
            font-weight: normal;
            font-size: 20px;
            line-height: 125%;
            text-align: Left;
            letter-spacing: normal;
            margin-top: 0;
            margin-right: 0;
            margin-bottom: 10px;
            margin-left: 0;
            padding-top: 0;
            padding-bottom: 0;
            padding-left: 0;
            padding-right: 0;
        }

        .ReadMsgBody {
            width: 100%;
        }

        .ExternalClass {
            width: 100%;
        }

            .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {
                line-height: 100%;
            }

        table, td {
            mso-table-lspace: 0pt;
            mso-table-rspace: 0pt;
        }

        #outlook a {
            padding: 0;
        }

        img {
            -ms-interpolation-mode: bicubic;
            display: block;
            outline: none;
            text-decoration: none;
        }

        body, table, td, p, a, li, blockquote {
            -ms-text-size-adjust: 100%;
            -webkit-text-size-adjust: 100%;
            font-weight: 500 !important;
        }

        .ExternalClass td[class='ecxflexibleContainerBox'] h3 {
            padding-top: 10px !important;
        }

        h1 {
            display: block;
            font-size: 26px;
            font-style: normal;
            font-weight: normal;
            line-height: 100%;
        }

        h2 {
            display: block;
            font-size: 20px;
            font-style: normal;
            font-weight: normal;
            line-height: 120%;
        }

        h3 {
            display: block;
            font-size: 17px;
            font-style: normal;
            font-weight: normal;
            line-height: 110%;
        }

        h4 {
            display: block;
            font-size: 18px;
            font-style: italic;
            font-weight: normal;
            line-height: 100%;
        }

        .flexibleImage {
            height: auto;
        }

        .linkRemoveBorder {
            border-bottom: 0 !important;
        }

        table[class=flexibleContainerCellDivider] {
            padding-bottom: 0 !important;
            padding-top: 0 !important;
        }

        body, #bodyTable {
            background-color: #E1E1E1;
        }

        #emailHeader {
            background-color: #fff;
        }

        #emailBody {
            background-color: #FFFFFF;
        }

        #emailFooter {
            background-color: #E1E1E1;
        }

        .nestedContainer {
            background-color: #F8F8F8;
            border: 1px solid #CCCCCC;
        }

        .emailButton {
            background-color: #205478;
            border-collapse: separate;
        }

        .buttonContent {
            color: #FFFFFF;
            font-family: Helvetica;
            font-size: 18px;
            font-weight: bold;
            line-height: 100%;
            padding: 15px;
            text-align: center;
        }

            .buttonContent a {
                color: #FFFFFF;
                display: block;
                text-decoration: none !important;
                border: 0 !important;
            }

        .emailCalendar {
            background-color: #FFFFFF;
            border: 1px solid #CCCCCC;
        }

        .emailCalendarMonth {
            background-color: #205478;
            color: #FFFFFF;
            font-size: 16px;
            font-weight: bold;
            padding-top: 10px;
            padding-bottom: 10px;
            text-align: center;
        }

        .emailCalendarDay {
            color: #205478;
            font-size: 60px;
            font-weight: bold;
            line-height: 100%;
            padding-top: 20px;
            padding-bottom: 20px;
            text-align: center;
        }

        .imageContentText {
            margin-top: 10px;
            line-height: 0;
        }

            .imageContentText a {
                line-height: 0;
            }

        #invisibleIntroduction {
            display: none !important;
        }

        span[class=ios-color-hack] a {
            color: #275100 !important;
            text-decoration: none !important;
        }

        span[class=ios-color-hack2] a {
            color: #205478 !important;
            text-decoration: none !important;
        }

        span[class=ios-color-hack3] a {
            color: #8B8B8B !important;
            text-decoration: none !important;
        }

        .a[href^='tel'], a[href^='sms'] {
            text-decoration: none !important;
            color: #606060 !important;
            pointer-events: none !important;
            cursor: default !important;
        }

        .mobile_link a[href^='tel'], .mobile_link a[href^='sms'] {
            text-decoration: none !important;
            color: #606060 !important;
            pointer-events: auto !important;
            cursor: default !important;
        }

        @media only screen and (max-width: 480px) {
            body {
                width: 100% !important;
                min-width: 100% !important;
            }

            table[id='emailHeader'],
            table[id='emailBody'],
            table[id='emailFooter'],
            table[class='flexibleContainer'],
            td[class='flexibleContainerCell'] {
                width: 100% !important;
            }

            td[class='flexibleContainerBox'], td[class='flexibleContainerBox'] table {
                display: block;
                width: 100%;
                text-align: left;
            }

            td[class='imageContent'] img {
                height: auto !important;
                width: 100% !important;
                max-width: 100% !important;
            }

            img[class='flexibleImage'] {
                height: auto !important;
                width: 100% !important;
                max-width: 100% !important;
            }

            img[class='flexibleImageSmall'] {
                height: auto !important;
                width: auto !important;
            }

            table[class='flexibleContainerBoxNext'] {
                padding-top: 10px !important;
            }

            table[class='emailButton'] {
                width: 100% !important;
            }

            td[class='buttonContent'] {
                padding: 0 !important;
            }

                td[class='buttonContent'] a {
                    padding: 15px !important;
                }
        }

        @media only screen and (-webkit-device-pixel-ratio:.75) {
        }

        @media only screen and (-webkit-device-pixel-ratio:1) {
        }

        @media only screen and (-webkit-device-pixel-ratio:1.5) {
        }

        @media only screen and (min-device-width : 320px) and (max-device-width:568px) {
        }

        .blink_text {
            -webkit-animation-name: blinker;
            -webkit-animation-duration: 2s;
            -webkit-animation-timing-function: linear;
            -webkit-animation-iteration-count: infinite;
            -moz-animation-name: blinker;
            -moz-animation-duration: 2s;
            -moz-animation-timing-function: linear;
            -moz-animation-iteration-count: infinite;
            animation-name: blinker;
            animation-duration: 2s;
            animation-timing-function: linear;
            animation-iteration-count: infinite;
            color: white;
        }

        @-moz-keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        @-webkit-keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        @keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }
    </style>
</head>
<body bgcolor='#E1E1E1' leftmargin='0' marginwidth='0' topmargin='0' marginheight='0' offset='0'>
    <center style='background-color:#E1E1E1;'>
        <table border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' id='bodyTable' style=' table-layout: fixed; max-width:100% !important;width: 100% !important;min-width: 100% !important;'>
            <tr>
                <td align='center' valign='top' id='bodyCell'>
 
                    <table bgcolor='#FFFFFF' border='0' cellpadding='0' cellspacing='0' width='700' id='emailBody' style='margin-top:20px;margin-bottom:20px;'>


                        <tr>
                            <td align='center' valign='top' style=''>
                                <table border='0' cellpadding='0' cellspacing='0' width='100%' bgcolor='#fff'>
                                    <tr>
                                        <td align='center' valign='top'>
                                            <table border='0' cellpadding='0' cellspacing='0' width='700' class='flexibleContainer'>
                                                <tr>
                                                    <td align='center' valign='top' width='700' class='flexibleContainerCell'>
                                                        <table border='0' cellpadding='30' cellspacing='0' width='100%'>



                                                            <tr>
                                                                <td align='left' valign='top' style='padding:10px;background:#fcfcfc'>
                                                                    <table border='0' cellpadding='0' cellspacing='0' style='float:left;float: left; margin-right: 0; width: 100%;'>
                                                                        <tr>
                                                                            <td align='left' valign='top' width='100%' class='flexibleContainerCell'>
                                                                                <center><img style='height:50px;margin-bottom:0px;' src='" + ConfigurationManager.AppSettings["domain"] + @"/assets/imgs/logo.png' /></center>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>

                                                        </table>




                                                    </td>
                                                </tr>


                                                <tr>
                                                    <td align='left' valign='top' style='padding:0px;background:#fff'>
                                                        <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                                            <tr>
                                                                <td align='left' valign='top' width='100%' class='flexibleContainerCell'>
                                                                    <p style='font-size:22px;line-height:28px!important;text-align:center;color:#573e40;font-weight:bold!important'>
                                                                        Hi Admin, You have received on order, Below are the details
                                                                    </p>

                                                                   
                                                                    <p style='font-size:26px;line-height:32px!important;text-align:center;color:white;margin-top:0px;background:#FF9212;padding:10px;width:50%;margin:0px auto;margin-bottom:0px'>Order Number: " + oid + @"</p>

                                                                </td> 
                                                            </tr> 
                                                        </table>
                                                    </td>
                                                </tr>  
                                                <tr>
                                                    <td align='left' valign='top' style='padding:0px 60px 20px 60px;background:#fff'>
                                                        <table border='0' cellpadding='0' cellspacing='0' width='100%'>


                                                            <tr>
                                                                <td align='left' valign='top' style='height:250px;float:left;width:46%;margin-bottom:10px;margin-top:20px;color:#fff;padding:10px; font-size:18px;margin-right:0.5%' bgcolor='#FF9212' class='flexibleContainerCell'>

                                                                   Billing Address
                                                                    <p style='color:#fff;text-align:left;font-size:12px;line-height:20px;'>
                                                                        " + address1 + @" 
                                                                    </p> 
                                                                </td>
                                                                <td align='left' valign='top' style='height:250px;float:left;width:46%;margin-bottom:10px;margin-top:20px;text-align:left;padding:10px;color:#fff;font-size:18px;margin-left:0.5%;' bgcolor='#FF9212' class='flexibleContainerCell'>
                                                                    Delivery Address
                                                                    <p style='color:#fff;text-align:left;font-size:12px;line-height:20px;'>
                                                                        " + address2 + @" 
                                                                    </p> 
                                                                </td> 
                                                                <td align='left' valign='top' style='float:left;width:100%' class='flexibleContainerCell'>
                                                                    <p style='text-align:left;font-weight:600!important;margin-bottom:20px !important;font-size:16px;'>Stuff You Picked:</p>

                                                                </td>
                                                            </tr>
                                                              " + productTable + @"
                                                        </table>
                                                    </td>
                                                </tr> 
                                            </table>
                                        </td>
                                    </tr> 

                                     <tr>
                                        <td align='left' valign='top' style='padding:10px 20px;background:#f2f2f2'>
                                            <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                                  <tr>
                                                                               <td align='center' valign='middle' width='100%' class='flexibleContainerCell'>
                                                                                    <ul style='list-style:none;width:390px;margin:0px auto;'>
                                                                                        <li style='display:none;margin-right:5px;padding-right:5px;'>
                                                                                            <a href='https://www.facebook.com/Archidplydecor.bonvivant' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/img/email-icons/facebook.png' /></a>
                                                                                        </li>
                                                                                        <li style='display:none;margin-right:5px;padding-right:5px;'>
                                                                                            <a href='https://www.instagram.com/archidplydecor_bonvivant/' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/img/email-icons/instagram.png' /></a>
                                                                                        </li>
                                                                                        <li style='display:none;margin-right:0px;padding-right:5px;'>
                                                                                            <a href='mailto:bangalore@archidply.com' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/img/email-icons/mail.png' /></a>
                                                                                        </li>

                                                                                    </ul>
                                                                                </td>

                                                                            </tr> 
                                            </table>
                                        </td>
                                    </tr> 
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        
    </center>
</body>
</html>";
            #endregion
            MailMessage mail = new MailMessage();
            //mail.To.Add(ConfigurationManager.AppSettings["ToMail"]);
            mail.To.Add("adltrading@archidply.com");
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["CCMail"]))
            {
                mail.CC.Add(ConfigurationManager.AppSettings["CCMail"]);
            }
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["BCCMail"]))
            {
                mail.Bcc.Add(ConfigurationManager.AppSettings["BCCMail"]);
            }
            mail.From = new MailAddress(ConfigurationManager.AppSettings["from"], ConfigurationManager.AppSettings["fromName"]);
            mail.Subject = "Archidply Order Confirmation ( " + DateTime.Now + " )";
            mail.Body = mailBody;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["host"];
            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            smtp.Credentials = new System.Net.NetworkCredential
                   (ConfigurationManager.AppSettings["userName"], ConfigurationManager.AppSettings["password"]);
            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSsl"]);
            smtp.Send(mail);
            return 1;
        }
        catch (Exception exx)
        {
            return 0;
        }
    }
    public static int OrderDispatched(string oid, string productTable, string name, string email, string mobile, string paidAmount, string pType, string delprname, string delprnumber, string link, string address)
    {
        try
        {
            string amt = pType.ToLower() == "cod20" ? "Payable:<br />₹" + paidAmount + @"" : " Amount Paid <br /> </b>₹" + paidAmount + @"";
            #region mailbody
            string mailbody = @"<!DOCTYPE HTML PUBLIC '-//W3C//DTD XHTML 1.0 Strict//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd'>
<html xmlns='http://www.w3.org/1999/xhtml'>
<head>
    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
    <meta name='viewport' content='width=device-width, initial-scale=1.0' />
    <meta http-equiv='X-UA-Compatible' content='IE=edge,chrome=1' />
    <meta name='format-detection' content='telephone=no' />
    <title>Archidply</title>
    <link href='https://fonts.googleapis.com/css?family=Lato:100,100i,300,300i,400,400i,700,700i,900,900i&display=swap' rel='stylesheet' />
    <style type='text/css'>
        html {
            background-color: #FFF;
            margin: 0;
            font-family: 'Lato', sans-serif;
            padding: 0;
        }

        /*@import url('https://fonts.googleapis.com/css?family=Roboto');*/

        body, #bodyTable, #bodyCell, #bodyCell {
            height: 100% !important;
            margin: 0;
            padding: 0;
            width: 100% !important;
        }

        table {
            border-collapse: collapse;
        }

            table[id=bodyTable] {
                width: 100% !important;
                margin: auto;
                max-width: 500px !important;
                color: #212121;
                font-weight: normal;
            }

        img, a img {
            border: 0;
            outline: none;
            text-decoration: none;
            height: auto;
            line-height: 100%;
        }

        /*a {
            text-decoration: none !important;
            border-bottom: 1px solid;
        }*/

        h1, h2, h3, h4, h5, h6 {
            color: #5F5F5F;
            font-weight: normal;
            font-size: 20px;
            line-height: 125%;
            text-align: Left;
            letter-spacing: normal;
            margin-top: 0;
            margin-right: 0;
            margin-bottom: 10px;
            margin-left: 0;
            padding-top: 0;
            padding-bottom: 0;
            padding-left: 0;
            padding-right: 0;
        }

        .ReadMsgBody {
            width: 100%;
        }

        .ExternalClass {
            width: 100%;
        }

            .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {
                line-height: 100%;
            }

        table, td {
            mso-table-lspace: 0pt;
            mso-table-rspace: 0pt;
        }

        #outlook a {
            padding: 0;
        }

        img {
            -ms-interpolation-mode: bicubic;
            display: block;
            outline: none;
            text-decoration: none;
        }

        body, table, td, p, a, li, blockquote {
            -ms-text-size-adjust: 100%;
            -webkit-text-size-adjust: 100%;
            font-weight: 500 !important;
        }

        .ExternalClass td[class='ecxflexibleContainerBox'] h3 {
            padding-top: 10px !important;
        }

        h1 {
            display: block;
            font-size: 26px;
            font-style: normal;
            font-weight: normal;
            line-height: 100%;
        }

        h2 {
            display: block;
            font-size: 20px;
            font-style: normal;
            font-weight: normal;
            line-height: 120%;
        }

        h3 {
            display: block;
            font-size: 17px;
            font-style: normal;
            font-weight: normal;
            line-height: 110%;
        }

        h4 {
            display: block;
            font-size: 18px;
            font-style: italic;
            font-weight: normal;
            line-height: 100%;
        }

        .flexibleImage {
            height: auto;
        }

        .linkRemoveBorder {
            border-bottom: 0 !important;
        }

        table[class=flexibleContainerCellDivider] {
            padding-bottom: 0 !important;
            padding-top: 0 !important;
        }

        body, #bodyTable {
            background-color: #E1E1E1;
        }

        #emailHeader {
            background-color: #fff;
        }

        #emailBody {
            background-color: #FFFFFF;
        }

        #emailFooter {
            background-color: #E1E1E1;
        }

        .nestedContainer {
            background-color: #F8F8F8;
            border: 1px solid #CCCCCC;
        }

        .emailButton {
            background-color: #205478;
            border-collapse: separate;
        }

        .buttonContent {
            color: #FFFFFF;
            font-family: Helvetica;
            font-size: 18px;
            font-weight: bold;
            line-height: 100%;
            padding: 15px;
            text-align: center;
        }

            .buttonContent a {
                color: #FFFFFF;
                display: block;
                text-decoration: none !important;
                border: 0 !important;
            }

        .emailCalendar {
            background-color: #FFFFFF;
            border: 1px solid #CCCCCC;
        }

        .emailCalendarMonth {
            background-color: #205478;
            color: #FFFFFF;
            font-size: 16px;
            font-weight: bold;
            padding-top: 10px;
            padding-bottom: 10px;
            text-align: center;
        }

        .emailCalendarDay {
            color: #205478;
            font-size: 60px;
            font-weight: bold;
            line-height: 100%;
            padding-top: 20px;
            padding-bottom: 20px;
            text-align: center;
        }

        .imageContentText {
            margin-top: 10px;
            line-height: 0;
        }

            .imageContentText a {
                line-height: 0;
            }

        #invisibleIntroduction {
            display: none !important;
        }

        span[class=ios-color-hack] a {
            color: #275100 !important;
            text-decoration: none !important;
        }

        span[class=ios-color-hack2] a {
            color: #205478 !important;
            text-decoration: none !important;
        }

        span[class=ios-color-hack3] a {
            color: #8B8B8B !important;
            text-decoration: none !important;
        }

        .a[href^='tel'], a[href^='sms'] {
            text-decoration: none !important;
            color: #606060 !important;
            pointer-events: none !important;
            cursor: default !important;
        }

        .mobile_link a[href^='tel'], .mobile_link a[href^='sms'] {
            text-decoration: none !important;
            color: #606060 !important;
            pointer-events: auto !important;
            cursor: default !important;
        }

        @media only screen and (max-width: 480px) {
            body {
                width: 100% !important;
                min-width: 100% !important;
            }

            table[id='emailHeader'],
            table[id='emailBody'],
            table[id='emailFooter'],
            table[class='flexibleContainer'],
            td[class='flexibleContainerCell'] {
                width: 100% !important;
            }

            td[class='flexibleContainerBox'], td[class='flexibleContainerBox'] table {
                display: block;
                width: 100%;
                text-align: left;
            }

            td[class='imageContent'] img {
                height: auto !important;
                width: 100% !important;
                max-width: 100% !important;
            }

            img[class='flexibleImage'] {
                height: auto !important;
                width: 100% !important;
                max-width: 100% !important;
            }

            img[class='flexibleImageSmall'] {
                height: auto !important;
                width: auto !important;
            }

            table[class='flexibleContainerBoxNext'] {
                padding-top: 10px !important;
            }

            table[class='emailButton'] {
                width: 100% !important;
            }

            td[class='buttonContent'] {
                padding: 0 !important;
            }

                td[class='buttonContent'] a {
                    padding: 15px !important;
                }
        }

        @media only screen and (-webkit-device-pixel-ratio:.75) {
        }

        @media only screen and (-webkit-device-pixel-ratio:1) {
        }

        @media only screen and (-webkit-device-pixel-ratio:1.5) {
        }

        @media only screen and (min-device-width : 320px) and (max-device-width:568px) {
        }

        .blink_text {
            -webkit-animation-name: blinker;
            -webkit-animation-duration: 2s;
            -webkit-animation-timing-function: linear;
            -webkit-animation-iteration-count: infinite;
            -moz-animation-name: blinker;
            -moz-animation-duration: 2s;
            -moz-animation-timing-function: linear;
            -moz-animation-iteration-count: infinite;
            animation-name: blinker;
            animation-duration: 2s;
            animation-timing-function: linear;
            animation-iteration-count: infinite;
            color: white;
        }

        @-moz-keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        @-webkit-keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        @keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }
    </style>
</head>
<body bgcolor='#E1E1E1' leftmargin='0' marginwidth='0' topmargin='0' marginheight='0' offset='0'>
    <center style='background-color:#E1E1E1;'>
        <table border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' id='bodyTable' style=' table-layout: fixed; max-width:100% !important;width: 100% !important;min-width: 100% !important;'>
            <tr>
                <td align='center' valign='top' id='bodyCell'>
                    <!--<table bgcolor='#fff' border='0' cellpadding='0' cellspacing='0' width='600' id='emailHeader' style='margin-top:20px;'>
                        <tr>
                            <td align='center' valign='top'>
                                <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                    <tr>
                                        <td align='center' valign='top' style='padding:0px'>
                                            <table border='0' cellpadding='10' cellspacing='0' width='500' class='flexibleContainer'>
                                                <tr>
                                                    <td valign='top' width='500' class='flexibleContainerCell' style='padding:0px;'>
                                                        <table align='left' border='0' cellpadding='0' cellspacing='0' width='100%'>
                                                            <tr>
                                                                <td align='center' valign='top' style='padding:0px 0px;'>
                                                                    <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                                                        <tr>
                                                                            <td align='left' valign='top' width='100%' class='flexibleContainerCell'>

                                                                            </td>
                                                                        </tr>

                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>-->
                    <table bgcolor='#FFFFFF' border='0' cellpadding='0' cellspacing='0' width='700' id='emailBody' style='margin-top:20px;margin-bottom:20px;'>


                        <tr>
                            <td align='center' valign='top' style=''>
                                <table border='0' cellpadding='0' cellspacing='0' width='100%' bgcolor='#fff'>
                                    <tr>
                                        <td align='center' valign='top'>
                                            <table border='0' cellpadding='0' cellspacing='0' width='700' class='flexibleContainer'>
                                                <tr>
                                                    <td align='center' valign='top' width='700' class='flexibleContainerCell'>
                                                        <table border='0' cellpadding='30' cellspacing='0' width='100%'>



                                                            <tr>
                                                                <td align='left' valign='top' style='padding:10px;background:#fcfcfc'>
                                                                    <table border='0' cellpadding='0' cellspacing='0' style='float:left;float: left; margin-right: 0; width: 100%;'>
                                                                        <tr>
                                                                            <td align='left' valign='top' width='100%' class='flexibleContainerCell'>
                                                                                <center><img style='height:50px;margin-bottom:0px;' src='" + ConfigurationManager.AppSettings["domain"] + @"/assets/imgs/logo.png' /></center>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>

                                                        </table>




                                                    </td>
                                                </tr>


                                                <tr>
                                                    <td align='left' valign='top' style='padding:0px;background:#fff'>
                                                        <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                                            <tr>
                                                                <td align='left' valign='top' width='100%' class='flexibleContainerCell'>
                                                                    <p style='font-size:22px;line-height:28px!important;text-align:center;color:#573e40;font-weight:bold!important'>
                                                                        Your order is on the way! 
                                                                    </p>

                                                                    <p style='font-size:18px;color:#573e40;line-height:22px!important;margin-bottom:30px;text-align:left;padding:0px 50px;'>
                                                                        Hi " + name + @",
                                                                    </p>
                                                                    <p style='line-height:22px!important;text-align:left;margin-top:0px;color:#573e40;margin-bottom:30px;padding:0px 50px;'>
                                                                       Your order is on the way. Track your shipment to see the delivery status.
                                                                       Your tracking details are mentioned below.
                                                                    </p>
                                                                    <p style='line-height:22px!important;text-align:left;margin-top:0px;color:#573e40;margin-bottom:30px;padding:0px 50px;'>
                                                                        Courier Name : " + delprname + @" <br>Tracking Code : " + delprnumber + @" <br>Tracking Link : " + link + @"
                                                                    </p>
                                                                  
                                                                </td>
                                                            </tr>

                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align='left' valign='top' style='padding:0px 50px 20px 50px;background:#fff'>
                                                        <table border='0' cellpadding='0' cellspacing='0' width='100%'>


                                                            <tr>
                                                                <td align='left' valign='top' style='height:200px;float:left;width:46%;margin-bottom:10px;margin-top:20px;color:#fff;padding:10px; font-size:18px;margin-right:0.5%' bgcolor='#8cc540' class='flexibleContainerCell'>
                                                                    <p style='color:#fff;text-align:left;font-size:16px;margin-bottom:5px;line-height:20px;'>
                                                                        Order Number: " + oid + @"
                                                                    </p>
                                                                    Payment Method
                                                                    <p style='color:#fff;text-align:left;font-size:12px;margin-bottom:25px;line-height:20px;'>
                                                                        " + pType + @"
                                                                    </p>
                                                                    <p style='color:#fff;text-align:left;font-size:12px;line-height:20px;margin-bottom:5px;'>
                                                                       " + amt + @"
                                                                    </p> 
                                                                </td>
                                                                <td align='left' valign='top' style='height:200px;float:left;width:46%;margin-bottom:10px;margin-top:20px;text-align:left;padding:10px;color:#fff;font-size:18px;margin-left:0.5%;' bgcolor='#8cc540' class='flexibleContainerCell'>
                                                                    Shipped To:
                                                                    <p style='color:#fff;text-align:left;font-size:12px;line-height:20px;'>
                                                                       " + address + @"
                                                                    </p>

                                                                </td>

                                                                <td align='left' valign='top' style='float:left;width:100%' class='flexibleContainerCell'>
                                                                    <p style='text-align:left;font-weight:600!important;margin-bottom:20px !important;font-size:16px;'>Stuff You Picked:</p>

                                                                </td>
                                                            </tr>
                                                            " + productTable + @"
                                                            
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                        <td align='left' valign='top' style='padding:10px 20px;background:#f2f2f2'>
                                            <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                                  <tr>
                                                                               <td align='center' valign='middle' width='100%' class='flexibleContainerCell'>
                                                                                    <ul style='list-style:none;width:390px;margin:0px auto;'>
                                                                                        <li style='display:none;margin-right:5px;padding-right:5px;'>
                                                                                            <a href='https://www.facebook.com/Archidplydecor.bonvivant' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/img/email-icons/facebook.png' /></a>
                                                                                        </li>
                                                                                        <li style='display:none;margin-right:5px;padding-right:5px;'>
                                                                                            <a href='https://www.instagram.com/archidplydecor_bonvivant/' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/img/email-icons/instagram.png' /></a>
                                                                                        </li>
                                                                                        <li style='display:none;margin-right:5px;padding-right:5px;'>
                                                                                            <a href='https://twitter.com/' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/img/email-icons/twitter.png' /></a>
                                                                                        </li>
                                                                                        <li style='display:none;margin-right:5px;padding-right:5px;'>
                                                                                            <a href='https://www.youtube.com/' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/img/email-icons/youtube.png' /></a>
                                                                                        </li>
                                                                                        <li style='display:none;margin-right:0px;padding-right:5px;'>
                                                                                            <a href='mailto:bangalore@archidply.com' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/img/email-icons/mail.png' /></a>
                                                                                        </li>

                                                                                    </ul>
                                                                                </td>

                                                                            </tr> 
                                            </table>
                                        </td>
                                    </tr> 
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

    </center>
</body>
</html>";
            #endregion
            MailMessage mail = new MailMessage();
            mail.To.Add(email);
            mail.From = new MailAddress(ConfigurationManager.AppSettings["from"], ConfigurationManager.AppSettings["fromName"]);
            mail.Subject = "Tracking Information for Order#" + oid + "";
            mail.Body = mailbody;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["host"];
            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            smtp.Credentials = new System.Net.NetworkCredential
                   (ConfigurationManager.AppSettings["userName"], ConfigurationManager.AppSettings["password"]);
            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSsl"]);
            smtp.Send(mail);
            return 0;
        }
        catch (Exception exx)
        {
            return 0;
        }
    }
    public static int OrderDelivered(string oid, string productTable, string name, string email, string mobile, string pType, string address, string paidAmount)
    {
        try
        {
            #region mailbody
            string mailbody = @"<!DOCTYPE HTML PUBLIC '-//W3C//DTD XHTML 1.0 Strict//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd'>
<html xmlns='http://www.w3.org/1999/xhtml'>
<head>
    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
    <meta name='viewport' content='width=device-width, initial-scale=1.0' />
    <meta http-equiv='X-UA-Compatible' content='IE=edge,chrome=1' />
    <meta name='format-detection' content='telephone=no' />
    <title>Archidply</title>
    <link rel='shortcut icon' href='" + ConfigurationManager.AppSettings["domain"] + @"/emailtemplate/focuslay/logo.png' />
    <link href='https://fonts.googleapis.com/css?family=Lato:100,100i,300,300i,400,400i,700,700i,900,900i&display=swap' rel='stylesheet' />
    <style type='text/css'>
        html {
            background-color: #FFF;
            margin: 0;
            font-family: 'Lato', sans-serif;
            padding: 0;
        }

        /*@import url('https://fonts.googleapis.com/css?family=Roboto');*/

        body, #bodyTable, #bodyCell, #bodyCell {
            height: 100% !important;
            margin: 0;
            padding: 0;
            width: 100% !important;
        }

        table {
            border-collapse: collapse;
        }

            table[id=bodyTable] {
                width: 100% !important;
                margin: auto;
                max-width: 500px !important;
                color: #212121;
                font-weight: normal;
            }

        img, a img {
            border: 0;
            outline: none;
            text-decoration: none;
            height: auto;
            line-height: 100%;
        }

        /*a {
            text-decoration: none !important;
            border-bottom: 1px solid;
        }*/

        h1, h2, h3, h4, h5, h6 {
            color: #5F5F5F;
            font-weight: normal;
            font-size: 20px;
            line-height: 125%;
            text-align: Left;
            letter-spacing: normal;
            margin-top: 0;
            margin-right: 0;
            margin-bottom: 10px;
            margin-left: 0;
            padding-top: 0;
            padding-bottom: 0;
            padding-left: 0;
            padding-right: 0;
        }

        .ReadMsgBody {
            width: 100%;
        }

        .ExternalClass {
            width: 100%;
        }

            .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {
                line-height: 100%;
            }

        table, td {
            mso-table-lspace: 0pt;
            mso-table-rspace: 0pt;
        }

        #outlook a {
            padding: 0;
        }

        img {
            -ms-interpolation-mode: bicubic;
            display: block;
            outline: none;
            text-decoration: none;
        }

        body, table, td, p, a, li, blockquote {
            -ms-text-size-adjust: 100%;
            -webkit-text-size-adjust: 100%;
            font-weight: 500 !important;
        }

        .ExternalClass td[class='ecxflexibleContainerBox'] h3 {
            padding-top: 10px !important;
        }

        h1 {
            display: block;
            font-size: 26px;
            font-style: normal;
            font-weight: normal;
            line-height: 100%;
        }

        h2 {
            display: block;
            font-size: 20px;
            font-style: normal;
            font-weight: normal;
            line-height: 120%;
        }

        h3 {
            display: block;
            font-size: 17px;
            font-style: normal;
            font-weight: normal;
            line-height: 110%;
        }

        h4 {
            display: block;
            font-size: 18px;
            font-style: italic;
            font-weight: normal;
            line-height: 100%;
        }

        .flexibleImage {
            height: auto;
        }

        .linkRemoveBorder {
            border-bottom: 0 !important;
        }

        table[class=flexibleContainerCellDivider] {
            padding-bottom: 0 !important;
            padding-top: 0 !important;
        }

        body, #bodyTable {
            background-color: #E1E1E1;
        }

        #emailHeader {
            background-color: #fff;
        }

        #emailBody {
            background-color: #FFFFFF;
        }

        #emailFooter {
            background-color: #E1E1E1;
        }

        .nestedContainer {
            background-color: #F8F8F8;
            border: 1px solid #CCCCCC;
        }

        .emailButton {
            background-color: #205478;
            border-collapse: separate;
        }

        .buttonContent {
            color: #FFFFFF;
            font-family: Helvetica;
            font-size: 18px;
            font-weight: bold;
            line-height: 100%;
            padding: 15px;
            text-align: center;
        }

            .buttonContent a {
                color: #FFFFFF;
                display: block;
                text-decoration: none !important;
                border: 0 !important;
            }

        .emailCalendar {
            background-color: #FFFFFF;
            border: 1px solid #CCCCCC;
        }

        .emailCalendarMonth {
            background-color: #205478;
            color: #FFFFFF;
            font-size: 16px;
            font-weight: bold;
            padding-top: 10px;
            padding-bottom: 10px;
            text-align: center;
        }

        .emailCalendarDay {
            color: #205478;
            font-size: 60px;
            font-weight: bold;
            line-height: 100%;
            padding-top: 20px;
            padding-bottom: 20px;
            text-align: center;
        }

        .imageContentText {
            margin-top: 10px;
            line-height: 0;
        }

            .imageContentText a {
                line-height: 0;
            }

        #invisibleIntroduction {
            display: none !important;
        }

        span[class=ios-color-hack] a {
            color: #275100 !important;
            text-decoration: none !important;
        }

        span[class=ios-color-hack2] a {
            color: #205478 !important;
            text-decoration: none !important;
        }

        span[class=ios-color-hack3] a {
            color: #8B8B8B !important;
            text-decoration: none !important;
        }

        .a[href^='tel'], a[href^='sms'] {
            text-decoration: none !important;
            color: #606060 !important;
            pointer-events: none !important;
            cursor: default !important;
        }

        .mobile_link a[href^='tel'], .mobile_link a[href^='sms'] {
            text-decoration: none !important;
            color: #606060 !important;
            pointer-events: auto !important;
            cursor: default !important;
        }

        @media only screen and (max-width: 480px) {
            body {
                width: 100% !important;
                min-width: 100% !important;
            }

            table[id='emailHeader'],
            table[id='emailBody'],
            table[id='emailFooter'],
            table[class='flexibleContainer'],
            td[class='flexibleContainerCell'] {
                width: 100% !important;
            }

            td[class='flexibleContainerBox'], td[class='flexibleContainerBox'] table {
                display: block;
                width: 100%;
                text-align: left;
            }

            td[class='imageContent'] img {
                height: auto !important;
                width: 100% !important;
                max-width: 100% !important;
            }

            img[class='flexibleImage'] {
                height: auto !important;
                width: 100% !important;
                max-width: 100% !important;
            }

            img[class='flexibleImageSmall'] {
                height: auto !important;
                width: auto !important;
            }

            table[class='flexibleContainerBoxNext'] {
                padding-top: 10px !important;
            }

            table[class='emailButton'] {
                width: 100% !important;
            }

            td[class='buttonContent'] {
                padding: 0 !important;
            }

                td[class='buttonContent'] a {
                    padding: 15px !important;
                }
        }

        @media only screen and (-webkit-device-pixel-ratio:.75) {
        }

        @media only screen and (-webkit-device-pixel-ratio:1) {
        }

        @media only screen and (-webkit-device-pixel-ratio:1.5) {
        }

        @media only screen and (min-device-width : 320px) and (max-device-width:568px) {
        }

        .blink_text {
            -webkit-animation-name: blinker;
            -webkit-animation-duration: 2s;
            -webkit-animation-timing-function: linear;
            -webkit-animation-iteration-count: infinite;
            -moz-animation-name: blinker;
            -moz-animation-duration: 2s;
            -moz-animation-timing-function: linear;
            -moz-animation-iteration-count: infinite;
            animation-name: blinker;
            animation-duration: 2s;
            animation-timing-function: linear;
            animation-iteration-count: infinite;
            color: white;
        }

        @-moz-keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        @-webkit-keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        @keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }
    </style>
</head>
<body bgcolor='#E1E1E1' leftmargin='0' marginwidth='0' topmargin='0' marginheight='0' offset='0'>
    <center style='background-color:#E1E1E1;'>
        <table border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' id='bodyTable' style=' table-layout: fixed; max-width:100% !important;width: 100% !important;min-width: 100% !important;'>
            <tr>
                <td align='center' valign='top' id='bodyCell'>
                    <table bgcolor='#FFFFFF' border='0' cellpadding='0' cellspacing='0' width='700' id='emailBody' style='margin-top:20px;margin-bottom:20px;'>

                        <tr>
                            <td align='center' valign='top' style=''>
                                <table border='0' cellpadding='0' cellspacing='0' width='100%' bgcolor='#fff'>
                                    <tr>
                                        <td align='center' valign='top'>
                                            <table border='0' cellpadding='0' cellspacing='0' width='700' class='flexibleContainer'>
                                                <tr>
                                                    <td align='center' valign='top' width='700' class='flexibleContainerCell'>
                                                        <table border='0' cellpadding='30' cellspacing='0' width='100%'>

                                                            <tr>
                                                                <td align='left' valign='top' style='padding:10px;background:#fcfcfc'>
                                                                    <table border='0' cellpadding='0' cellspacing='0' style='float:left;float: left; margin-right: 0; width: 100%;'>
                                                                        <tr>
                                                                            <td align='left' valign='top' width='100%' class='flexibleContainerCell'>
                                                                                <center><img style='height:50px;margin-bottom:0px;' src='" + ConfigurationManager.AppSettings["domain"] + @"/assets/imgs/logo.png' /></center>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>

                                                        </table>

                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td align='left' valign='top' style='padding:0px;background:#fff'>
                                                        <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                                            <tr>
                                                                <td align='left' valign='top' width='100%' class='flexibleContainerCell'>
                                                                    <p style='font-size:22px;line-height:28px!important;text-align:center;color:#573e40;font-weight:bold!important'>
                                                                        Your Order has been delivered! 
                                                                    </p>

                                                                    <p style='font-size:18px;color:#573e40;line-height:22px!important;margin-bottom:30px;text-align:left;padding:0px 50px;'>
                                                                        Hi " + name + @",
                                                                    </p>
                                                                    <p style='font-size:13px;line-height:23px!important;text-align:left;margin-top:0px;color:#573e40;margin-bottom:30px;padding:0px 50px;'>
                                                                        Greetings from Archidply, your order " + oid + @" is delivered successfully on " + CommonModel.UTCTime().ToString("dd-MMM-yyyy") + @", thank you for shopping with us.
                                                                    </p>
                                                                   <p style='font-size:13px;line-height:23px!important;text-align:left;margin-top:0px;color:#573e40;margin-bottom:30px;padding:0px 50px;'>Your feedback is important for us, Kindly write to feedback " + ConfigurationManager.AppSettings["from"] + @"<br><br>Team Archidply<br>" + ConfigurationManager.AppSettings["domain"] + @"</p>
                                                                </td>
                                                            </tr>

                                                        </table>
                                                    </td>
                                                </tr>
                                               
                                                 <tr>
                                        <td align='left' valign='top' style='padding:10px 20px;background:#f2f2f2'>
                                            <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                                  <tr>
                                                                               <td align='center' valign='middle' width='100%' class='flexibleContainerCell'>
                                                                                    <ul style='list-style:none;width:390px;margin:0px auto;'>
                                                                                        <li style='display:none;margin-right:5px;padding-right:5px;'>
                                                                                            <a href='https://www.facebook.com/Archidplydecor.bonvivant' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/images_/facebook.png' /></a>
                                                                                        </li>
                                                                                        <li style='display:none;margin-right:5px;padding-right:5px;'>
                                                                                            <a href='https://www.instagram.com/archidplydecor_bonvivant/' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/images_/instagram.png' /></a>
                                                                                        </li>
                                                                                        <li style='display:none;margin-right:5px;padding-right:5px;'>
                                                                                            <a href='https://twitter.com/' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/images_/twitter.png' /></a>
                                                                                        </li>
                                                                                        <li style='display:none;margin-right:5px;padding-right:5px;'>
                                                                                            <a href='https://www.youtube.com/' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/images_/youtube.png' /></a>
                                                                                        </li>
                                                                                        <li style='display:none;margin-right:0px;padding-right:5px;'>
                                                                                            <a href='mailto:bangalore@archidply.com' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/images_/mail.png' /></a>
                                                                                        </li>

                                                                                    </ul>
                                                                                </td>

                                                                            </tr> 
                                            </table>
                                        </td>
                                    </tr> 
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

    </center>
</body>
</html>";
            #endregion
            MailMessage mail = new MailMessage();
            mail.To.Add(email);
            mail.From = new MailAddress(ConfigurationManager.AppSettings["from"], ConfigurationManager.AppSettings["fromName"]);
            mail.Subject = "Archidply - " + oid + " Delivered";
            mail.Body = mailbody;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["host"];
            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            smtp.Credentials = new System.Net.NetworkCredential
                   (ConfigurationManager.AppSettings["userName"], ConfigurationManager.AppSettings["password"]);

            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSsl"]);
            smtp.Send(mail);
            return 0;
        }
        catch (Exception exx)
        {
            return 0;
        }
    }
    public static int CancellationMail(string oid, string productTable, string name, string email, string mobile, string pType, string address, string orderedOn)
    {
        try
        {
            #region mailbody
            string mailbody = @" <!DOCTYPE HTML PUBLIC '-//W3C//DTD XHTML 1.0 Strict//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd'>
<html xmlns='http://www.w3.org/1999/xhtml'>
<head>
    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
    <meta name='viewport' content='width=device-width, initial-scale=1.0' />
    <meta http-equiv='X-UA-Compatible' content='IE=edge,chrome=1' />
    <meta name='format-detection' content='telephone=no' />
    <title>Archidply</title>
    <link href='https://fonts.googleapis.com/css?family=Lato:100,100i,300,300i,400,400i,700,700i,900,900i&display=swap' rel='stylesheet' />
    <style type='text/css'>
        html {
            background-color: #FFF;
            margin: 0;
            font-family: 'Lato', sans-serif;
            padding: 0;
        }

        /*@import url('https://fonts.googleapis.com/css?family=Roboto');*/

        body, #bodyTable, #bodyCell, #bodyCell {
            height: 100% !important;
            margin: 0;
            padding: 0;
            width: 100% !important;
        }

        table {
            border-collapse: collapse;
        }

            table[id=bodyTable] {
                width: 100% !important;
                margin: auto;
                max-width: 500px !important;
                color: #212121;
                font-weight: normal;
            }

        img, a img {
            border: 0;
            outline: none;
            text-decoration: none;
            height: auto;
            line-height: 100%;
        }

        /*a {
            text-decoration: none !important;
            border-bottom: 1px solid;
        }*/

        h1, h2, h3, h4, h5, h6 {
            color: #5F5F5F;
            font-weight: normal;
            font-size: 20px;
            line-height: 125%;
            text-align: Left;
            letter-spacing: normal;
            margin-top: 0;
            margin-right: 0;
            margin-bottom: 10px;
            margin-left: 0;
            padding-top: 0;
            padding-bottom: 0;
            padding-left: 0;
            padding-right: 0;
        }

        .ReadMsgBody {
            width: 100%;
        }

        .ExternalClass {
            width: 100%;
        }

            .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {
                line-height: 100%;
            }

        table, td {
            mso-table-lspace: 0pt;
            mso-table-rspace: 0pt;
        }

        #outlook a {
            padding: 0;
        }

        img {
            -ms-interpolation-mode: bicubic;
            display: block;
            outline: none;
            text-decoration: none;
        }

        body, table, td, p, a, li, blockquote {
            -ms-text-size-adjust: 100%;
            -webkit-text-size-adjust: 100%;
            font-weight: 500 !important;
        }

        .ExternalClass td[class='ecxflexibleContainerBox'] h3 {
            padding-top: 10px !important;
        }

        h1 {
            display: block;
            font-size: 26px;
            font-style: normal;
            font-weight: normal;
            line-height: 100%;
        }

        h2 {
            display: block;
            font-size: 20px;
            font-style: normal;
            font-weight: normal;
            line-height: 120%;
        }

        h3 {
            display: block;
            font-size: 17px;
            font-style: normal;
            font-weight: normal;
            line-height: 110%;
        }

        h4 {
            display: block;
            font-size: 18px;
            font-style: italic;
            font-weight: normal;
            line-height: 100%;
        }

        .flexibleImage {
            height: auto;
        }

        .linkRemoveBorder {
            border-bottom: 0 !important;
        }

        table[class=flexibleContainerCellDivider] {
            padding-bottom: 0 !important;
            padding-top: 0 !important;
        }

        body, #bodyTable {
            background-color: #E1E1E1;
        }

        #emailHeader {
            background-color: #fff;
        }

        #emailBody {
            background-color: #FFFFFF;
        }

        #emailFooter {
            background-color: #E1E1E1;
        }

        .nestedContainer {
            background-color: #F8F8F8;
            border: 1px solid #CCCCCC;
        }

        .emailButton {
            background-color: #205478;
            border-collapse: separate;
        }

        .buttonContent {
            color: #FFFFFF;
            font-family: Helvetica;
            font-size: 18px;
            font-weight: bold;
            line-height: 100%;
            padding: 15px;
            text-align: center;
        }

            .buttonContent a {
                color: #FFFFFF;
                display: block;
                text-decoration: none !important;
                border: 0 !important;
            }

        .emailCalendar {
            background-color: #FFFFFF;
            border: 1px solid #CCCCCC;
        }

        .emailCalendarMonth {
            background-color: #205478;
            color: #FFFFFF;
            font-size: 16px;
            font-weight: bold;
            padding-top: 10px;
            padding-bottom: 10px;
            text-align: center;
        }

        .emailCalendarDay {
            color: #205478;
            font-size: 60px;
            font-weight: bold;
            line-height: 100%;
            padding-top: 20px;
            padding-bottom: 20px;
            text-align: center;
        }

        .imageContentText {
            margin-top: 10px;
            line-height: 0;
        }

            .imageContentText a {
                line-height: 0;
            }

        #invisibleIntroduction {
            display: none !important;
        }

        span[class=ios-color-hack] a {
            color: #275100 !important;
            text-decoration: none !important;
        }

        span[class=ios-color-hack2] a {
            color: #205478 !important;
            text-decoration: none !important;
        }

        span[class=ios-color-hack3] a {
            color: #8B8B8B !important;
            text-decoration: none !important;
        }

        .a[href^='tel'], a[href^='sms'] {
            text-decoration: none !important;
            color: #606060 !important;
            pointer-events: none !important;
            cursor: default !important;
        }

        .mobile_link a[href^='tel'], .mobile_link a[href^='sms'] {
            text-decoration: none !important;
            color: #606060 !important;
            pointer-events: auto !important;
            cursor: default !important;
        }

        @media only screen and (max-width: 480px) {
            body {
                width: 100% !important;
                min-width: 100% !important;
            }

            table[id='emailHeader'],
            table[id='emailBody'],
            table[id='emailFooter'],
            table[class='flexibleContainer'],
            td[class='flexibleContainerCell'] {
                width: 100% !important;
            }

            td[class='flexibleContainerBox'], td[class='flexibleContainerBox'] table {
                display: block;
                width: 100%;
                text-align: left;
            }

            td[class='imageContent'] img {
                height: auto !important;
                width: 100% !important;
                max-width: 100% !important;
            }

            img[class='flexibleImage'] {
                height: auto !important;
                width: 100% !important;
                max-width: 100% !important;
            }

            img[class='flexibleImageSmall'] {
                height: auto !important;
                width: auto !important;
            }

            table[class='flexibleContainerBoxNext'] {
                padding-top: 10px !important;
            }

            table[class='emailButton'] {
                width: 100% !important;
            }

            td[class='buttonContent'] {
                padding: 0 !important;
            }

                td[class='buttonContent'] a {
                    padding: 15px !important;
                }
        }

        @media only screen and (-webkit-device-pixel-ratio:.75) {
        }

        @media only screen and (-webkit-device-pixel-ratio:1) {
        }

        @media only screen and (-webkit-device-pixel-ratio:1.5) {
        }

        @media only screen and (min-device-width : 320px) and (max-device-width:568px) {
        }

        .blink_text {
            -webkit-animation-name: blinker;
            -webkit-animation-duration: 2s;
            -webkit-animation-timing-function: linear;
            -webkit-animation-iteration-count: infinite;
            -moz-animation-name: blinker;
            -moz-animation-duration: 2s;
            -moz-animation-timing-function: linear;
            -moz-animation-iteration-count: infinite;
            animation-name: blinker;
            animation-duration: 2s;
            animation-timing-function: linear;
            animation-iteration-count: infinite;
            color: white;
        }

        @-moz-keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        @-webkit-keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        @keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }
    </style>
</head>
<body bgcolor='#E1E1E1' leftmargin='0' marginwidth='0' topmargin='0' marginheight='0' offset='0'>
    <center style='background-color:#E1E1E1;'>
        <table border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' id='bodyTable' style=' table-layout: fixed; max-width:100% !important;width: 100% !important;min-width: 100% !important;'>
            <tr>
                <td align='center' valign='top' id='bodyCell'>

                    <table bgcolor='#FFFFFF' border='0' cellpadding='0' cellspacing='0' width='700' id='emailBody' style='margin-top:20px;margin-bottom:20px;'>


                        <tr>
                            <td align='center' valign='top' style=''>
                                <table border='0' cellpadding='0' cellspacing='0' width='100%' bgcolor='#fff'>
                                    <tr>
                                        <td align='center' valign='top'>
                                            <table border='0' cellpadding='0' cellspacing='0' width='700' class='flexibleContainer'>
                                                <tr>
                                                    <td align='center' valign='top' width='700' class='flexibleContainerCell'>
                                                        <table border='0' cellpadding='30' cellspacing='0' width='100%'>



                                                            <tr>
                                                                <td align='left' valign='top' style='padding:10px;background:#fcfcfc'>
                                                                    <table border='0' cellpadding='0' cellspacing='0' style='float:left;float: left; margin-right: 0; width: 100%;'>
                                                                        <tr>
                                                                            <td align='left' valign='top' width='100%' class='flexibleContainerCell'>
                                                                                <center><img style='height:50px;margin-bottom:0px;' src='" + ConfigurationManager.AppSettings["domain"] + @"/assets/imgs/logo.png' /></center>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>


                                                            <tr>
                                                                <td align='left' valign='top' style='padding:0px;'>
                                                                    <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                                                        <tr>
                                                                            <td align='left' valign='top' width='100%' class='flexibleContainerCell'>

                                                                                <p style='font-size:22px;line-height:28px!important;text-align:center;color:#111;font-weight:bold!important'>
                                                                                    Order cancelled!
                                                                                </p>

                                                                                <p style='font-size:16px;color:#573e40;line-height:24px!important;margin-bottom:30px;text-align:left;padding:0px 50px;'>
                                                                                    Hi " + name + @"
                                                                                </p>
                                                                                <p style='font-size:13px;color:#573e40;line-height:24px!important;margin-bottom:30px;text-align:left;padding:0px 50px;'>
                                                                                    Greetings from Archidply, we have received your request for order cancellation, please note we are checking the best possibility as per cancellation terms & will intimate you.                                                                                 </p>
                                                                                <p style='font-size:13px;color:#573e40;line-height:24px!important;margin-bottom:30px;text-align:center;padding:0px 50px;'>
                                                                                    Thank you for shopping with us.<br>Your feedback is important for us, Kindly write to feedback " + ConfigurationManager.AppSettings["from"] + @"
                                                                                </p>

                                                                               
                                                                            </td>


                                                                        </tr>

                                                                    </table>
                                                                </td>
                                                            </tr>  
                                                             <tr>
                                        <td align='left' valign='top' style='padding:10px 20px;background:#f2f2f2'>
                                            <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                                  <tr>
                                                                               <td align='center' valign='middle' width='100%' class='flexibleContainerCell'>
                                                                                    <ul style='list-style:none;width:390px;margin:0px auto;'>
                                                                                        <li style='display:none;margin-right:5px;padding-right:5px;'>
                                                                                            <a href='https://www.facebook.com/Archidplydecor.bonvivant' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/img/email-icons/facebook.png' /></a>
                                                                                        </li>
                                                                                        <li style='display:none;margin-right:5px;padding-right:5px;'>
                                                                                            <a href='https://www.instagram.com/archidplydecor_bonvivant/' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/img/email-icons/instagram.png' /></a>
                                                                                        </li>
                                                                                        <li style='display:none;margin-right:0px;padding-right:5px;'>
                                                                                            <a href='mailto:bangalore@archidply.com' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/img/email-icons/mail.png' /></a>
                                                                                        </li>

                                                                                    </ul>
                                                                                </td>

                                                                            </tr> 
                                            </table>
                                        </td>
                                    </tr> 
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

    </center>
</body>
</html>";
            #endregion
            MailMessage mail = new MailMessage();
            mail.To.Add(email);
            mail.From = new MailAddress(ConfigurationManager.AppSettings["from"], ConfigurationManager.AppSettings["fromName"]);
            mail.Subject = "Your " + ConfigurationManager.AppSettings["domain"] + @" order has been cancelled";
            mail.Body = mailbody;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["host"];
            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            smtp.Credentials = new System.Net.NetworkCredential
                   (ConfigurationManager.AppSettings["userName"], ConfigurationManager.AppSettings["password"]);
            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSsl"]);
            smtp.Send(mail);
            return 0;
        }
        catch (Exception exx)
        {
            return 0;
        }
    }
    public static int ReturnMail(string oid, string productTable, string name, string email, string mobile, string pType, string address, string orderedOn)
    {
        try
        {
            #region mailbody
            string mailbody = @" <!DOCTYPE HTML PUBLIC '-//W3C//DTD XHTML 1.0 Strict//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd'>
<html xmlns='http://www.w3.org/1999/xhtml'>
<head>
    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
    <meta name='viewport' content='width=device-width, initial-scale=1.0' />
    <meta http-equiv='X-UA-Compatible' content='IE=edge,chrome=1' />
    <meta name='format-detection' content='telephone=no' />
    <title>Archidply</title>
    <link href='https://fonts.googleapis.com/css?family=Lato:100,100i,300,300i,400,400i,700,700i,900,900i&display=swap' rel='stylesheet' />
    <style type='text/css'>
        html {
            background-color: #FFF;
            margin: 0;
            font-family: 'Lato', sans-serif;
            padding: 0;
        }

        /*@import url('https://fonts.googleapis.com/css?family=Roboto');*/

        body, #bodyTable, #bodyCell, #bodyCell {
            height: 100% !important;
            margin: 0;
            padding: 0;
            width: 100% !important;
        }

        table {
            border-collapse: collapse;
        }

            table[id=bodyTable] {
                width: 100% !important;
                margin: auto;
                max-width: 500px !important;
                color: #212121;
                font-weight: normal;
            }

        img, a img {
            border: 0;
            outline: none;
            text-decoration: none;
            height: auto;
            line-height: 100%;
        }

        /*a {
            text-decoration: none !important;
            border-bottom: 1px solid;
        }*/

        h1, h2, h3, h4, h5, h6 {
            color: #5F5F5F;
            font-weight: normal;
            font-size: 20px;
            line-height: 125%;
            text-align: Left;
            letter-spacing: normal;
            margin-top: 0;
            margin-right: 0;
            margin-bottom: 10px;
            margin-left: 0;
            padding-top: 0;
            padding-bottom: 0;
            padding-left: 0;
            padding-right: 0;
        }

        .ReadMsgBody {
            width: 100%;
        }

        .ExternalClass {
            width: 100%;
        }

            .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {
                line-height: 100%;
            }

        table, td {
            mso-table-lspace: 0pt;
            mso-table-rspace: 0pt;
        }

        #outlook a {
            padding: 0;
        }

        img {
            -ms-interpolation-mode: bicubic;
            display: block;
            outline: none;
            text-decoration: none;
        }

        body, table, td, p, a, li, blockquote {
            -ms-text-size-adjust: 100%;
            -webkit-text-size-adjust: 100%;
            font-weight: 500 !important;
        }

        .ExternalClass td[class='ecxflexibleContainerBox'] h3 {
            padding-top: 10px !important;
        }

        h1 {
            display: block;
            font-size: 26px;
            font-style: normal;
            font-weight: normal;
            line-height: 100%;
        }

        h2 {
            display: block;
            font-size: 20px;
            font-style: normal;
            font-weight: normal;
            line-height: 120%;
        }

        h3 {
            display: block;
            font-size: 17px;
            font-style: normal;
            font-weight: normal;
            line-height: 110%;
        }

        h4 {
            display: block;
            font-size: 18px;
            font-style: italic;
            font-weight: normal;
            line-height: 100%;
        }

        .flexibleImage {
            height: auto;
        }

        .linkRemoveBorder {
            border-bottom: 0 !important;
        }

        table[class=flexibleContainerCellDivider] {
            padding-bottom: 0 !important;
            padding-top: 0 !important;
        }

        body, #bodyTable {
            background-color: #E1E1E1;
        }

        #emailHeader {
            background-color: #fff;
        }

        #emailBody {
            background-color: #FFFFFF;
        }

        #emailFooter {
            background-color: #E1E1E1;
        }

        .nestedContainer {
            background-color: #F8F8F8;
            border: 1px solid #CCCCCC;
        }

        .emailButton {
            background-color: #205478;
            border-collapse: separate;
        }

        .buttonContent {
            color: #FFFFFF;
            font-family: Helvetica;
            font-size: 18px;
            font-weight: bold;
            line-height: 100%;
            padding: 15px;
            text-align: center;
        }

            .buttonContent a {
                color: #FFFFFF;
                display: block;
                text-decoration: none !important;
                border: 0 !important;
            }

        .emailCalendar {
            background-color: #FFFFFF;
            border: 1px solid #CCCCCC;
        }

        .emailCalendarMonth {
            background-color: #205478;
            color: #FFFFFF;
            font-size: 16px;
            font-weight: bold;
            padding-top: 10px;
            padding-bottom: 10px;
            text-align: center;
        }

        .emailCalendarDay {
            color: #205478;
            font-size: 60px;
            font-weight: bold;
            line-height: 100%;
            padding-top: 20px;
            padding-bottom: 20px;
            text-align: center;
        }

        .imageContentText {
            margin-top: 10px;
            line-height: 0;
        }

            .imageContentText a {
                line-height: 0;
            }

        #invisibleIntroduction {
            display: none !important;
        }

        span[class=ios-color-hack] a {
            color: #275100 !important;
            text-decoration: none !important;
        }

        span[class=ios-color-hack2] a {
            color: #205478 !important;
            text-decoration: none !important;
        }

        span[class=ios-color-hack3] a {
            color: #8B8B8B !important;
            text-decoration: none !important;
        }

        .a[href^='tel'], a[href^='sms'] {
            text-decoration: none !important;
            color: #606060 !important;
            pointer-events: none !important;
            cursor: default !important;
        }

        .mobile_link a[href^='tel'], .mobile_link a[href^='sms'] {
            text-decoration: none !important;
            color: #606060 !important;
            pointer-events: auto !important;
            cursor: default !important;
        }

        @media only screen and (max-width: 480px) {
            body {
                width: 100% !important;
                min-width: 100% !important;
            }

            table[id='emailHeader'],
            table[id='emailBody'],
            table[id='emailFooter'],
            table[class='flexibleContainer'],
            td[class='flexibleContainerCell'] {
                width: 100% !important;
            }

            td[class='flexibleContainerBox'], td[class='flexibleContainerBox'] table {
                display: block;
                width: 100%;
                text-align: left;
            }

            td[class='imageContent'] img {
                height: auto !important;
                width: 100% !important;
                max-width: 100% !important;
            }

            img[class='flexibleImage'] {
                height: auto !important;
                width: 100% !important;
                max-width: 100% !important;
            }

            img[class='flexibleImageSmall'] {
                height: auto !important;
                width: auto !important;
            }

            table[class='flexibleContainerBoxNext'] {
                padding-top: 10px !important;
            }

            table[class='emailButton'] {
                width: 100% !important;
            }

            td[class='buttonContent'] {
                padding: 0 !important;
            }

                td[class='buttonContent'] a {
                    padding: 15px !important;
                }
        }

        @media only screen and (-webkit-device-pixel-ratio:.75) {
        }

        @media only screen and (-webkit-device-pixel-ratio:1) {
        }

        @media only screen and (-webkit-device-pixel-ratio:1.5) {
        }

        @media only screen and (min-device-width : 320px) and (max-device-width:568px) {
        }

        .blink_text {
            -webkit-animation-name: blinker;
            -webkit-animation-duration: 2s;
            -webkit-animation-timing-function: linear;
            -webkit-animation-iteration-count: infinite;
            -moz-animation-name: blinker;
            -moz-animation-duration: 2s;
            -moz-animation-timing-function: linear;
            -moz-animation-iteration-count: infinite;
            animation-name: blinker;
            animation-duration: 2s;
            animation-timing-function: linear;
            animation-iteration-count: infinite;
            color: white;
        }

        @-moz-keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        @-webkit-keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        @keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }
    </style>
</head>
<body bgcolor='#E1E1E1' leftmargin='0' marginwidth='0' topmargin='0' marginheight='0' offset='0'>
    <center style='background-color:#E1E1E1;'>
        <table border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' id='bodyTable' style=' table-layout: fixed; max-width:100% !important;width: 100% !important;min-width: 100% !important;'>
            <tr>
                <td align='center' valign='top' id='bodyCell'>

                    <table bgcolor='#FFFFFF' border='0' cellpadding='0' cellspacing='0' width='700' id='emailBody' style='margin-top:20px;margin-bottom:20px;'>


                        <tr>
                            <td align='center' valign='top' style=''>
                                <table border='0' cellpadding='0' cellspacing='0' width='100%' bgcolor='#fff'>
                                    <tr>
                                        <td align='center' valign='top'>
                                            <table border='0' cellpadding='0' cellspacing='0' width='700' class='flexibleContainer'>
                                                <tr>
                                                    <td align='center' valign='top' width='700' class='flexibleContainerCell'>
                                                        <table border='0' cellpadding='30' cellspacing='0' width='100%'>



                                                            <tr>
                                                                <td align='left' valign='top' style='padding:10px;background:#fcfcfc'>
                                                                    <table border='0' cellpadding='0' cellspacing='0' style='float:left;float: left; margin-right: 0; width: 100%;'>
                                                                        <tr>
                                                                            <td align='left' valign='top' width='100%' class='flexibleContainerCell'>
                                                                                <center><img style='height:50px;margin-bottom:0px;' src='" + ConfigurationManager.AppSettings["domain"] + @"/assets/imgs/logo.png' /></center>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>


                                                            <tr>
                                                                <td align='left' valign='top' style='padding:0px;'>
                                                                    <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                                                        <tr>
                                                                            <td align='left' valign='top' width='100%' class='flexibleContainerCell'>

                                                                                <p style='font-size:22px;line-height:28px!important;text-align:center;color:#111;font-weight:bold!important'>
                                                                                    Order cancelled!
                                                                                </p>

                                                                                <p style='font-size:16px;color:#573e40;line-height:24px!important;margin-bottom:30px;text-align:left;padding:0px 50px;'>
                                                                                    Hi " + name + @",
                                                                                </p>
                                                                                <p style='font-size:13px;color:#573e40;line-height:24px!important;margin-bottom:30px;text-align:left;padding:0px 50px;'>
Greetings from Archidply, we have successfully created return of
your order, Please keep the products in its original shape with
its Price tag, original packing, our logistics team with shortly
contact with you to take return. Once we receive the products,
will initiate refunds to your original payment mode &amp; will be
intimated.
                                                                                 </p>
                                                                                <p style='font-size:13px;color:#573e40;line-height:24px!important;margin-bottom:30px;text-align:center;padding:0px 50px;'>
                                                                                      Your feedback is important for us, Kindly write to feedback " + ConfigurationManager.AppSettings["from"] + @"
                                                                                </p><p style='font-size:13px;color:#573e40;line-height:24px!important;margin-bottom:30px;padding:0px 50px;'>
                                                                                      Team Archidply<br>" + ConfigurationManager.AppSettings["domain"] + @"
                                                                                </p>

                                                                               
                                                                            </td>


                                                                        </tr>

                                                                    </table>
                                                                </td>
                                                            </tr>  
                                                             <tr>
                                        <td align='left' valign='top' style='padding:10px 20px;background:#f2f2f2'>
                                            <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                                  <tr>
                                                                               <td align='center' valign='middle' width='100%' class='flexibleContainerCell'>
                                                                                    <ul style='list-style:none;width:390px;margin:0px auto;'>
                                                                                        <li style='display:none;margin-right:5px;padding-right:5px;'>
                                                                                            <a href='https://www.facebook.com/Archidplydecor.bonvivant' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/img/email-icons/facebook.png' /></a>
                                                                                        </li>
                                                                                        <li style='display:none;margin-right:5px;padding-right:5px;'>
                                                                                            <a href='https://www.instagram.com/archidplydecor_bonvivant/' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/img/email-icons/instagram.png' /></a>
                                                                                        </li>
                                                                                        <li style='display:none;margin-right:0px;padding-right:5px;'>
                                                                                            <a href='mailto:bangalore@archidply.com' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/img/email-icons/mail.png' /></a>
                                                                                        </li>

                                                                                    </ul>
                                                                                </td>

                                                                            </tr> 
                                            </table>
                                        </td>
                                    </tr> 
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

    </center>
</body>
</html>";
            #endregion
            MailMessage mail = new MailMessage();
            mail.To.Add(email);
            mail.From = new MailAddress(ConfigurationManager.AppSettings["from"], ConfigurationManager.AppSettings["fromName"]);
            mail.Subject = "Your " + ConfigurationManager.AppSettings["domain"] + @" order has been cancelled";
            mail.Body = mailbody;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["host"];
            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            smtp.Credentials = new System.Net.NetworkCredential
                   (ConfigurationManager.AppSettings["userName"], ConfigurationManager.AppSettings["password"]);
            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSsl"]);
            smtp.Send(mail);
            return 0;
        }
        catch (Exception exx)
        {
            return 0;
        }
    }
    public static int BookingCancelledAdmin(string oid, string productTable, string name, string email, string paidAmount, string pType, string address1, string address2)
    {
        try
        {
            string amt = pType.ToLower() == "cod" ? "Payable:<br />₹" + paidAmount + @"" : " Amount Paid <br /> </b>₹" + paidAmount + @"";
            #region mailBody
            string mailBody = @"<!DOCTYPE HTML PUBLIC '-//W3C//DTD XHTML 1.0 Strict//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd'>
<html xmlns='http://www.w3.org/1999/xhtml'>
<head>
    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
    <meta name='viewport' content='width=device-width, initial-scale=1.0' />
    <meta http-equiv='X-UA-Compatible' content='IE=edge,chrome=1' />
    <meta name='format-detection' content='telephone=no' />
    <title>Archidply</title>
    <link rel='shortcut icon' href='http://nextwebi.com/emailtemplate/focuslay/logo.png' />
    <link href='https://fonts.googleapis.com/css?family=Lato:100,100i,300,300i,400,400i,700,700i,900,900i&display=swap' rel='stylesheet' />
    <style type='text/css'>
        html {
            background-color: #FFF;
            margin: 0;
            font-family: 'Lato', sans-serif;
            padding: 0;
        }

        /*@import url('https://fonts.googleapis.com/css?family=Roboto');*/

        body, #bodyTable, #bodyCell, #bodyCell {
            height: 100% !important;
            margin: 0;
            padding: 0;
            width: 100% !important;
        }

        table {
            border-collapse: collapse;
        }

            table[id=bodyTable] {
                width: 100% !important;
                margin: auto;
                max-width: 500px !important;
                color: #212121;
                font-weight: normal;
            }

        img, a img {
            border: 0;
            outline: none;
            text-decoration: none;
            height: auto;
            line-height: 100%;
        }

        /*a {
            text-decoration: none !important;
            border-bottom: 1px solid;
        }*/

        h1, h2, h3, h4, h5, h6 {
            color: #5F5F5F;
            font-weight: normal;
            font-size: 20px;
            line-height: 125%;
            text-align: Left;
            letter-spacing: normal;
            margin-top: 0;
            margin-right: 0;
            margin-bottom: 10px;
            margin-left: 0;
            padding-top: 0;
            padding-bottom: 0;
            padding-left: 0;
            padding-right: 0;
        }

        .ReadMsgBody {
            width: 100%;
        }

        .ExternalClass {
            width: 100%;
        }

            .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {
                line-height: 100%;
            }

        table, td {
            mso-table-lspace: 0pt;
            mso-table-rspace: 0pt;
        }

        #outlook a {
            padding: 0;
        }

        img {
            -ms-interpolation-mode: bicubic;
            display: block;
            outline: none;
            text-decoration: none;
        }

        body, table, td, p, a, li, blockquote {
            -ms-text-size-adjust: 100%;
            -webkit-text-size-adjust: 100%;
            font-weight: 500 !important;
        }

        .ExternalClass td[class='ecxflexibleContainerBox'] h3 {
            padding-top: 10px !important;
        }

        h1 {
            display: block;
            font-size: 26px;
            font-style: normal;
            font-weight: normal;
            line-height: 100%;
        }

        h2 {
            display: block;
            font-size: 20px;
            font-style: normal;
            font-weight: normal;
            line-height: 120%;
        }

        h3 {
            display: block;
            font-size: 17px;
            font-style: normal;
            font-weight: normal;
            line-height: 110%;
        }

        h4 {
            display: block;
            font-size: 18px;
            font-style: italic;
            font-weight: normal;
            line-height: 100%;
        }

        .flexibleImage {
            height: auto;
        }

        .linkRemoveBorder {
            border-bottom: 0 !important;
        }

        table[class=flexibleContainerCellDivider] {
            padding-bottom: 0 !important;
            padding-top: 0 !important;
        }

        body, #bodyTable {
            background-color: #E1E1E1;
        }

        #emailHeader {
            background-color: #fff;
        }

        #emailBody {
            background-color: #FFFFFF;
        }

        #emailFooter {
            background-color: #E1E1E1;
        }

        .nestedContainer {
            background-color: #F8F8F8;
            border: 1px solid #CCCCCC;
        }

        .emailButton {
            background-color: #205478;
            border-collapse: separate;
        }

        .buttonContent {
            color: #FFFFFF;
            font-family: Helvetica;
            font-size: 18px;
            font-weight: bold;
            line-height: 100%;
            padding: 15px;
            text-align: center;
        }

            .buttonContent a {
                color: #FFFFFF;
                display: block;
                text-decoration: none !important;
                border: 0 !important;
            }

        .emailCalendar {
            background-color: #FFFFFF;
            border: 1px solid #CCCCCC;
        }

        .emailCalendarMonth {
            background-color: #205478;
            color: #FFFFFF;
            font-size: 16px;
            font-weight: bold;
            padding-top: 10px;
            padding-bottom: 10px;
            text-align: center;
        }

        .emailCalendarDay {
            color: #205478;
            font-size: 60px;
            font-weight: bold;
            line-height: 100%;
            padding-top: 20px;
            padding-bottom: 20px;
            text-align: center;
        }

        .imageContentText {
            margin-top: 10px;
            line-height: 0;
        }

            .imageContentText a {
                line-height: 0;
            }

        #invisibleIntroduction {
            display: none !important;
        }

        span[class=ios-color-hack] a {
            color: #275100 !important;
            text-decoration: none !important;
        }

        span[class=ios-color-hack2] a {
            color: #205478 !important;
            text-decoration: none !important;
        }

        span[class=ios-color-hack3] a {
            color: #8B8B8B !important;
            text-decoration: none !important;
        }

        .a[href^='tel'], a[href^='sms'] {
            text-decoration: none !important;
            color: #606060 !important;
            pointer-events: none !important;
            cursor: default !important;
        }

        .mobile_link a[href^='tel'], .mobile_link a[href^='sms'] {
            text-decoration: none !important;
            color: #606060 !important;
            pointer-events: auto !important;
            cursor: default !important;
        }

        @media only screen and (max-width: 480px) {
            body {
                width: 100% !important;
                min-width: 100% !important;
            }

            table[id='emailHeader'],
            table[id='emailBody'],
            table[id='emailFooter'],
            table[class='flexibleContainer'],
            td[class='flexibleContainerCell'] {
                width: 100% !important;
            }

            td[class='flexibleContainerBox'], td[class='flexibleContainerBox'] table {
                display: block;
                width: 100%;
                text-align: left;
            }

            td[class='imageContent'] img {
                height: auto !important;
                width: 100% !important;
                max-width: 100% !important;
            }

            img[class='flexibleImage'] {
                height: auto !important;
                width: 100% !important;
                max-width: 100% !important;
            }

            img[class='flexibleImageSmall'] {
                height: auto !important;
                width: auto !important;
            }

            table[class='flexibleContainerBoxNext'] {
                padding-top: 10px !important;
            }

            table[class='emailButton'] {
                width: 100% !important;
            }

            td[class='buttonContent'] {
                padding: 0 !important;
            }

                td[class='buttonContent'] a {
                    padding: 15px !important;
                }
        }

        @media only screen and (-webkit-device-pixel-ratio:.75) {
        }

        @media only screen and (-webkit-device-pixel-ratio:1) {
        }

        @media only screen and (-webkit-device-pixel-ratio:1.5) {
        }

        @media only screen and (min-device-width : 320px) and (max-device-width:568px) {
        }

        .blink_text {
            -webkit-animation-name: blinker;
            -webkit-animation-duration: 2s;
            -webkit-animation-timing-function: linear;
            -webkit-animation-iteration-count: infinite;
            -moz-animation-name: blinker;
            -moz-animation-duration: 2s;
            -moz-animation-timing-function: linear;
            -moz-animation-iteration-count: infinite;
            animation-name: blinker;
            animation-duration: 2s;
            animation-timing-function: linear;
            animation-iteration-count: infinite;
            color: white;
        }

        @-moz-keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        @-webkit-keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        @keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }
    </style>
</head>
<body bgcolor='#E1E1E1' leftmargin='0' marginwidth='0' topmargin='0' marginheight='0' offset='0'>
    <center style='background-color:#E1E1E1;'>
        <table border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' id='bodyTable' style=' table-layout: fixed; max-width:100% !important;width: 100% !important;min-width: 100% !important;'>
            <tr>
                <td align='center' valign='top' id='bodyCell'>
 
                    <table bgcolor='#FFFFFF' border='0' cellpadding='0' cellspacing='0' width='700' id='emailBody' style='margin-top:20px;margin-bottom:20px;'>


                        <tr>
                            <td align='center' valign='top' style=''>
                                <table border='0' cellpadding='0' cellspacing='0' width='100%' bgcolor='#fff'>
                                    <tr>
                                        <td align='center' valign='top'>
                                            <table border='0' cellpadding='0' cellspacing='0' width='700' class='flexibleContainer'>
                                                <tr>
                                                    <td align='center' valign='top' width='700' class='flexibleContainerCell'>
                                                        <table border='0' cellpadding='30' cellspacing='0' width='100%'>



                                                            <tr>
                                                                <td align='left' valign='top' style='padding:10px;background:#fcfcfc'>
                                                                    <table border='0' cellpadding='0' cellspacing='0' style='float:left;float: left; margin-right: 0; width: 100%;'>
                                                                        <tr>
                                                                            <td align='left' valign='top' width='100%' class='flexibleContainerCell'>
                                                                                <center><img style='height:50px;margin-bottom:0px;' src='" + ConfigurationManager.AppSettings["domain"] + @"/assets/imgs/logo.png' /></center>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>

                                                        </table>




                                                    </td>
                                                </tr>


                                                <tr>
                                                    <td align='left' valign='top' style='padding:0px;background:#fff'>
                                                        <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                                            <tr>
                                                                <td align='left' valign='top' width='100%' class='flexibleContainerCell'>
                                                                    <p style='font-size:22px;line-height:28px!important;text-align:center;color:#573e40;font-weight:bold!important'>
                                                                        Hi Admin, You have received an order cancel request, Below are the details
                                                                    </p>

                                                                   
                                                                    <p style='font-size:26px;line-height:32px!important;text-align:center;color:white;margin-top:0px;background:#FF9212;padding:10px;width:50%;margin:0px auto;margin-bottom:0px'>Order Number: " + oid + @"</p>

                                                                </td> 
                                                            </tr> 
                                                        </table>
                                                    </td>
                                                </tr>  
                                                <tr>
                                                    <td align='left' valign='top' style='padding:0px 60px 20px 60px;background:#fff'>
                                                        <table border='0' cellpadding='0' cellspacing='0' width='100%'>


                                                            <tr>
                                                                <td align='left' valign='top' style='height:250px;float:left;width:46%;margin-bottom:10px;margin-top:20px;color:#fff;padding:10px; font-size:18px;margin-right:0.5%' bgcolor='#FF9212' class='flexibleContainerCell'>

                                                                   Billing Address
                                                                    <p style='color:#fff;text-align:left;font-size:12px;line-height:20px;'>" + name + @"<br>
                                                                        " + address1 + @" 
                                                                    </p> 
                                                                </td>
                                                                <td align='left' valign='top' style='height:250px;float:left;width:46%;margin-bottom:10px;margin-top:20px;text-align:left;padding:10px;color:#fff;font-size:18px;margin-left:0.5%;' bgcolor='#FF9212' class='flexibleContainerCell'>
                                                                    Delivery Address
                                                                    <p style='color:#fff;text-align:left;font-size:12px;line-height:20px;'>" + name + @"<br>
                                                                        " + address2 + @" 
                                                                    </p> 
                                                                </td> 
                                                                <td align='left' valign='top' style='float:left;width:100%' class='flexibleContainerCell'>
                                                                    <p style='text-align:left;font-weight:600!important;margin-bottom:20px !important;font-size:16px;'>Stuff You Picked:</p>

                                                                </td>
                                                            </tr>
                                                              " + productTable + @"
                                                        </table>
                                                    </td>
                                                </tr> 
                                            </table>
                                        </td>
                                    </tr> 

                                     <tr>
                                        <td align='left' valign='top' style='padding:10px 20px;background:#f2f2f2'>
                                            <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                                  <tr>
                                                                               <td align='center' valign='middle' width='100%' class='flexibleContainerCell'>
                                                                                    <ul style='list-style:none;width:390px;margin:0px auto;'>
                                                                                        <li style='display:none;margin-right:5px;padding-right:5px;'>
                                                                                            <a href='https://www.facebook.com/Archidplydecor.bonvivant' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/img/email-icons/facebook.png' /></a>
                                                                                        </li>
                                                                                        <li style='display:none;margin-right:5px;padding-right:5px;'>
                                                                                            <a href='https://www.instagram.com/archidplydecor_bonvivant/' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/img/email-icons/instagram.png' /></a>
                                                                                        </li>
                                                                                        <li style='display:none;margin-right:0px;padding-right:5px;'>
                                                                                            <a href='mailto:bangalore@archidply.com' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/img/email-icons/mail.png' /></a>
                                                                                        </li>

                                                                                    </ul>
                                                                                </td>

                                                                            </tr> 
                                            </table>
                                        </td>
                                    </tr> 
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        
    </center>
</body>
</html>";
            #endregion
            MailMessage mail = new MailMessage();
            mail.To.Add(ConfigurationManager.AppSettings["Mail"]);
            mail.From = new MailAddress(ConfigurationManager.AppSettings["from"], ConfigurationManager.AppSettings["fromName"]);
            mail.Subject = "Archidply Order Cancel Request";
            mail.Body = mailBody;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["host"];
            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            smtp.Credentials = new System.Net.NetworkCredential
                   (ConfigurationManager.AppSettings["userName"], ConfigurationManager.AppSettings["password"]);
            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSsl"]);
            smtp.Send(mail);
            return 1;
        }
        catch (Exception exx)
        {
            return 0;
        }
    }
    public static int BookingRetrurnAdmin(string oid, string productTable, string name, string email, string paidAmount, string pType, string address1, string address2)
    {
        try
        {
            string amt = pType.ToLower() == "cod" ? "Payable:<br />₹" + paidAmount + @"" : " Amount Paid <br /> </b>₹" + paidAmount + @"";
            #region mailBody
            string mailBody = @"<!DOCTYPE HTML PUBLIC '-//W3C//DTD XHTML 1.0 Strict//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd'>
<html xmlns='http://www.w3.org/1999/xhtml'>
<head>
    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
    <meta name='viewport' content='width=device-width, initial-scale=1.0' />
    <meta http-equiv='X-UA-Compatible' content='IE=edge,chrome=1' />
    <meta name='format-detection' content='telephone=no' />
    <title>Archidply</title>
    <link rel='shortcut icon' href='http://nextwebi.com/emailtemplate/focuslay/logo.png' />
    <link href='https://fonts.googleapis.com/css?family=Lato:100,100i,300,300i,400,400i,700,700i,900,900i&display=swap' rel='stylesheet' />
    <style type='text/css'>
        html {
            background-color: #FFF;
            margin: 0;
            font-family: 'Lato', sans-serif;
            padding: 0;
        }

        /*@import url('https://fonts.googleapis.com/css?family=Roboto');*/

        body, #bodyTable, #bodyCell, #bodyCell {
            height: 100% !important;
            margin: 0;
            padding: 0;
            width: 100% !important;
        }

        table {
            border-collapse: collapse;
        }

            table[id=bodyTable] {
                width: 100% !important;
                margin: auto;
                max-width: 500px !important;
                color: #212121;
                font-weight: normal;
            }

        img, a img {
            border: 0;
            outline: none;
            text-decoration: none;
            height: auto;
            line-height: 100%;
        }

        /*a {
            text-decoration: none !important;
            border-bottom: 1px solid;
        }*/

        h1, h2, h3, h4, h5, h6 {
            color: #5F5F5F;
            font-weight: normal;
            font-size: 20px;
            line-height: 125%;
            text-align: Left;
            letter-spacing: normal;
            margin-top: 0;
            margin-right: 0;
            margin-bottom: 10px;
            margin-left: 0;
            padding-top: 0;
            padding-bottom: 0;
            padding-left: 0;
            padding-right: 0;
        }

        .ReadMsgBody {
            width: 100%;
        }

        .ExternalClass {
            width: 100%;
        }

            .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {
                line-height: 100%;
            }

        table, td {
            mso-table-lspace: 0pt;
            mso-table-rspace: 0pt;
        }

        #outlook a {
            padding: 0;
        }

        img {
            -ms-interpolation-mode: bicubic;
            display: block;
            outline: none;
            text-decoration: none;
        }

        body, table, td, p, a, li, blockquote {
            -ms-text-size-adjust: 100%;
            -webkit-text-size-adjust: 100%;
            font-weight: 500 !important;
        }

        .ExternalClass td[class='ecxflexibleContainerBox'] h3 {
            padding-top: 10px !important;
        }

        h1 {
            display: block;
            font-size: 26px;
            font-style: normal;
            font-weight: normal;
            line-height: 100%;
        }

        h2 {
            display: block;
            font-size: 20px;
            font-style: normal;
            font-weight: normal;
            line-height: 120%;
        }

        h3 {
            display: block;
            font-size: 17px;
            font-style: normal;
            font-weight: normal;
            line-height: 110%;
        }

        h4 {
            display: block;
            font-size: 18px;
            font-style: italic;
            font-weight: normal;
            line-height: 100%;
        }

        .flexibleImage {
            height: auto;
        }

        .linkRemoveBorder {
            border-bottom: 0 !important;
        }

        table[class=flexibleContainerCellDivider] {
            padding-bottom: 0 !important;
            padding-top: 0 !important;
        }

        body, #bodyTable {
            background-color: #E1E1E1;
        }

        #emailHeader {
            background-color: #fff;
        }

        #emailBody {
            background-color: #FFFFFF;
        }

        #emailFooter {
            background-color: #E1E1E1;
        }

        .nestedContainer {
            background-color: #F8F8F8;
            border: 1px solid #CCCCCC;
        }

        .emailButton {
            background-color: #205478;
            border-collapse: separate;
        }

        .buttonContent {
            color: #FFFFFF;
            font-family: Helvetica;
            font-size: 18px;
            font-weight: bold;
            line-height: 100%;
            padding: 15px;
            text-align: center;
        }

            .buttonContent a {
                color: #FFFFFF;
                display: block;
                text-decoration: none !important;
                border: 0 !important;
            }

        .emailCalendar {
            background-color: #FFFFFF;
            border: 1px solid #CCCCCC;
        }

        .emailCalendarMonth {
            background-color: #205478;
            color: #FFFFFF;
            font-size: 16px;
            font-weight: bold;
            padding-top: 10px;
            padding-bottom: 10px;
            text-align: center;
        }

        .emailCalendarDay {
            color: #205478;
            font-size: 60px;
            font-weight: bold;
            line-height: 100%;
            padding-top: 20px;
            padding-bottom: 20px;
            text-align: center;
        }

        .imageContentText {
            margin-top: 10px;
            line-height: 0;
        }

            .imageContentText a {
                line-height: 0;
            }

        #invisibleIntroduction {
            display: none !important;
        }

        span[class=ios-color-hack] a {
            color: #275100 !important;
            text-decoration: none !important;
        }

        span[class=ios-color-hack2] a {
            color: #205478 !important;
            text-decoration: none !important;
        }

        span[class=ios-color-hack3] a {
            color: #8B8B8B !important;
            text-decoration: none !important;
        }

        .a[href^='tel'], a[href^='sms'] {
            text-decoration: none !important;
            color: #606060 !important;
            pointer-events: none !important;
            cursor: default !important;
        }

        .mobile_link a[href^='tel'], .mobile_link a[href^='sms'] {
            text-decoration: none !important;
            color: #606060 !important;
            pointer-events: auto !important;
            cursor: default !important;
        }

        @media only screen and (max-width: 480px) {
            body {
                width: 100% !important;
                min-width: 100% !important;
            }

            table[id='emailHeader'],
            table[id='emailBody'],
            table[id='emailFooter'],
            table[class='flexibleContainer'],
            td[class='flexibleContainerCell'] {
                width: 100% !important;
            }

            td[class='flexibleContainerBox'], td[class='flexibleContainerBox'] table {
                display: block;
                width: 100%;
                text-align: left;
            }

            td[class='imageContent'] img {
                height: auto !important;
                width: 100% !important;
                max-width: 100% !important;
            }

            img[class='flexibleImage'] {
                height: auto !important;
                width: 100% !important;
                max-width: 100% !important;
            }

            img[class='flexibleImageSmall'] {
                height: auto !important;
                width: auto !important;
            }

            table[class='flexibleContainerBoxNext'] {
                padding-top: 10px !important;
            }

            table[class='emailButton'] {
                width: 100% !important;
            }

            td[class='buttonContent'] {
                padding: 0 !important;
            }

                td[class='buttonContent'] a {
                    padding: 15px !important;
                }
        }

        @media only screen and (-webkit-device-pixel-ratio:.75) {
        }

        @media only screen and (-webkit-device-pixel-ratio:1) {
        }

        @media only screen and (-webkit-device-pixel-ratio:1.5) {
        }

        @media only screen and (min-device-width : 320px) and (max-device-width:568px) {
        }

        .blink_text {
            -webkit-animation-name: blinker;
            -webkit-animation-duration: 2s;
            -webkit-animation-timing-function: linear;
            -webkit-animation-iteration-count: infinite;
            -moz-animation-name: blinker;
            -moz-animation-duration: 2s;
            -moz-animation-timing-function: linear;
            -moz-animation-iteration-count: infinite;
            animation-name: blinker;
            animation-duration: 2s;
            animation-timing-function: linear;
            animation-iteration-count: infinite;
            color: white;
        }

        @-moz-keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        @-webkit-keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        @keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }
    </style>
</head>
<body bgcolor='#E1E1E1' leftmargin='0' marginwidth='0' topmargin='0' marginheight='0' offset='0'>
    <center style='background-color:#E1E1E1;'>
        <table border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' id='bodyTable' style=' table-layout: fixed; max-width:100% !important;width: 100% !important;min-width: 100% !important;'>
            <tr>
                <td align='center' valign='top' id='bodyCell'>
 
                    <table bgcolor='#FFFFFF' border='0' cellpadding='0' cellspacing='0' width='700' id='emailBody' style='margin-top:20px;margin-bottom:20px;'>


                        <tr>
                            <td align='center' valign='top' style=''>
                                <table border='0' cellpadding='0' cellspacing='0' width='100%' bgcolor='#fff'>
                                    <tr>
                                        <td align='center' valign='top'>
                                            <table border='0' cellpadding='0' cellspacing='0' width='700' class='flexibleContainer'>
                                                <tr>
                                                    <td align='center' valign='top' width='700' class='flexibleContainerCell'>
                                                        <table border='0' cellpadding='30' cellspacing='0' width='100%'>



                                                            <tr>
                                                                <td align='left' valign='top' style='padding:10px;background:#fcfcfc'>
                                                                    <table border='0' cellpadding='0' cellspacing='0' style='float:left;float: left; margin-right: 0; width: 100%;'>
                                                                        <tr>
                                                                            <td align='left' valign='top' width='100%' class='flexibleContainerCell'>
                                                                                <center><img style='height:50px;margin-bottom:0px;' src='" + ConfigurationManager.AppSettings["domain"] + @"/assets/imgs/logo.png' /></center>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>

                                                        </table>




                                                    </td>
                                                </tr>


                                                <tr>
                                                    <td align='left' valign='top' style='padding:0px;background:#fff'>
                                                        <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                                            <tr>
                                                                <td align='left' valign='top' width='100%' class='flexibleContainerCell'>
                                                                    <p style='font-size:22px;line-height:28px!important;text-align:center;color:#573e40;font-weight:bold!important'>
                                                                        Hi Admin, You have received an order return request, Below are the details
                                                                    </p>

                                                                   
                                                                    <p style='font-size:26px;line-height:32px!important;text-align:center;color:white;margin-top:0px;background:#FF9212;padding:10px;width:50%;margin:0px auto;margin-bottom:0px'>Order Number: " + oid + @"</p>

                                                                </td> 
                                                            </tr> 
                                                        </table>
                                                    </td>
                                                </tr>  
                                                <tr>
                                                    <td align='left' valign='top' style='padding:0px 60px 20px 60px;background:#fff'>
                                                        <table border='0' cellpadding='0' cellspacing='0' width='100%'>


                                                            <tr>
                                                                <td align='left' valign='top' style='height:250px;float:left;width:46%;margin-bottom:10px;margin-top:20px;color:#fff;padding:10px; font-size:18px;margin-right:0.5%' bgcolor='#FF9212' class='flexibleContainerCell'>

                                                                   Billing Address
                                                                    <p style='color:#fff;text-align:left;font-size:12px;line-height:20px;'>" + name + @"<br>
                                                                        " + address1 + @" 
                                                                    </p> 
                                                                </td>
                                                                <td align='left' valign='top' style='height:250px;float:left;width:46%;margin-bottom:10px;margin-top:20px;text-align:left;padding:10px;color:#fff;font-size:18px;margin-left:0.5%;' bgcolor='#FF9212' class='flexibleContainerCell'>
                                                                    Delivery Address
                                                                    <p style='color:#fff;text-align:left;font-size:12px;line-height:20px;'>" + name + @"<br>
                                                                        " + address2 + @" 
                                                                    </p> 
                                                                </td> 
                                                                <td align='left' valign='top' style='float:left;width:100%' class='flexibleContainerCell'>
                                                                    <p style='text-align:left;font-weight:600!important;margin-bottom:20px !important;font-size:16px;'>Stuff You Picked:</p>

                                                                </td>
                                                            </tr>
                                                              " + productTable + @"
                                                        </table>
                                                    </td>
                                                </tr> 
                                            </table>
                                        </td>
                                    </tr> 

                                    <tr>
                                        <td align='left' valign='top' style='padding:10px 20px;background:#f2f2f2'>
                                            <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                                  <tr>
                                                                               <td align='center' valign='middle' width='100%' class='flexibleContainerCell'>
                                                                                    <ul style='list-style:none;width:390px;margin:0px auto;'>
                                                                                        <li style='display:none;margin-right:5px;padding-right:5px;'>
                                                                                            <a href='https://www.facebook.com/Archidplydecor.bonvivant' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/img/email-icons/facebook.png' /></a>
                                                                                        </li>
                                                                                        <li style='display:none;margin-right:5px;padding-right:5px;'>
                                                                                            <a href='https://www.instagram.com/archidplydecor_bonvivant/' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/img/email-icons/instagram.png' /></a>
                                                                                        </li>
                                                                                        <li style='display:none;margin-right:0px;padding-right:5px;'>
                                                                                            <a href='mailto:bangalore@archidply.com' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/img/email-icons/mail.png' /></a>
                                                                                        </li>

                                                                                    </ul>
                                                                                </td>

                                                                            </tr> 
                                            </table>
                                        </td>
                                    </tr>  
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        
    </center>
</body>
</html>";
            #endregion
            MailMessage mail = new MailMessage();
            mail.To.Add(ConfigurationManager.AppSettings["Mail"]);
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["CCMail"]))
            {
                mail.CC.Add(ConfigurationManager.AppSettings["CCMail"]);
            }
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["BCCMail"]))
            {
                mail.Bcc.Add(ConfigurationManager.AppSettings["BCCMail"]);
            }
            mail.From = new MailAddress(ConfigurationManager.AppSettings["from"], ConfigurationManager.AppSettings["fromName"]);
            mail.Subject = "Archidply Order Return Request ( " + DateTime.Now + " )";
            mail.Body = mailBody;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["host"];
            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            smtp.Credentials = new System.Net.NetworkCredential
                   (ConfigurationManager.AppSettings["userName"], ConfigurationManager.AppSettings["password"]);
            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSsl"]);
            smtp.Send(mail);
            return 1;
        }
        catch (Exception exx)
        {
            return 0;
        }
    }

    public static int SendEnquiryRequestToAdmin(string name, string MobileNo, string email, string products, string message,string Size,string Thickness,string city)
    {
        try
        {


            MailMessage mail = new MailMessage();
            mail.To.Add(ConfigurationManager.AppSettings["ToMail"]);
            if (ConfigurationManager.AppSettings["CCMail"] != "")
            {
                mail.CC.Add(ConfigurationManager.AppSettings["CCMail"]);
            }
            if (ConfigurationManager.AppSettings["BCCMail"] != "")
            {
                mail.Bcc.Add(ConfigurationManager.AppSettings["BCCMail"]);
            }
            mail.From = new MailAddress(ConfigurationManager.AppSettings["from"], ConfigurationManager.AppSettings["fromName"]);
            mail.Subject = "Archidply Decor";

            string Body = "Hello Admin," + "<br/>" + "You have received a Product Enquiry from " + name + " " + "<br/>" + "<br/>" + "<strong><u>Details:</u></strong>" + "<br/>" + "Name:" + name + "<br/>" + "Email Address :" + email + "<br/>" + "city :" + city + "<br/>" + "Mobile :" + MobileNo + "<br />" + "ProductName :" + products + "<br />" + "ProductSize :" + Size + "<br />" + "ProductThickness :" + Thickness + "<br/>" + "Message : " + message + "<br/><br/><br/>" + "Regards" + "<br/><br/>" + "Archidply Decor" + "<br/>" + "";

            mail.Body = Body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["host"];
            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            smtp.Credentials = new System.Net.NetworkCredential
                   (ConfigurationManager.AppSettings["userName"], ConfigurationManager.AppSettings["password"]);
            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSsl"]);
            smtp.Send(mail);
            return 1;
        }
        catch (Exception exx)
        {

            return 0;

        }
    }
    public static int ContactUSRequestToCustomer(string name, string email)
    {
        try
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(email);

            mail.From = new MailAddress(ConfigurationManager.AppSettings["from"], ConfigurationManager.AppSettings["fromName"]);
            mail.Subject = "Archidply Decor";
            string Body = "Dear " + name + ",<br/><br/>We have received your request, our team will get back to you soon.  <br/><br/>Regards,<br/>Archidply Decor<br/>";
            mail.Body = Body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["host"];
            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            smtp.Credentials = new System.Net.NetworkCredential
                   (ConfigurationManager.AppSettings["userName"], ConfigurationManager.AppSettings["password"]);
            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSsl"]);
            smtp.Send(mail);
            return 1;
        }
        catch (Exception exx)
        {

            return 0;

        }
    }
    public static int sendEnquiryToCustomer(string name, string email)
    {
        try
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(email);

            mail.From = new MailAddress(ConfigurationManager.AppSettings["from"], ConfigurationManager.AppSettings["fromName"]);
            mail.Subject = "Archidply Decor";
            string Body = "Dear " + name + ",<br/><br/>We have received your Product enquiry our team will get back to you soon.  <br/><br/>Regards,<br/>Archidply Decor<br/>";
            mail.Body = Body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["host"];
            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            smtp.Credentials = new System.Net.NetworkCredential
                   (ConfigurationManager.AppSettings["userName"], ConfigurationManager.AppSettings["password"]);
            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSsl"]);
            smtp.Send(mail);
            return 1;
        }
        catch (Exception exx)
        {

            return 0;

        }
    }

    public static int SendJobApplyToAdmin(string name, string Email, string InterestedField, string Exp, string Phone, string city, string CoverLetter)
    {
        try
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(ConfigurationManager.AppSettings["ToMail"]);
            if (ConfigurationManager.AppSettings["CCMail"] != "")
            {
                mail.CC.Add(ConfigurationManager.AppSettings["CCMail"]);
            }
            if (ConfigurationManager.AppSettings["BCCMail"] != "")
            {
                mail.Bcc.Add(ConfigurationManager.AppSettings["BCCMail"]);
            }

            mail.From = new MailAddress(ConfigurationManager.AppSettings["from"], ConfigurationManager.AppSettings["fromName"]);
            mail.Subject = "Archidply Decor Job Apply Mail";
            string Body = "Hello Admin," + "<br/>" + "<br/>You have received a Job Application from " + name + " " + "<br/>" + "<br/>" + "<br/>" + "Details:" + "<br/>" + "Name:" + name + "<br/>" + "Email Address :" + Email + "<br/>" + "InterestedField :" + InterestedField + "<br />" + "Experience : " + Exp + "<br/>" + "Phone : " + Phone + "<br/>" + "City : " + city + "<br/>" + "CoverLetter : <a href='" + ConfigurationManager.AppSettings["domain"] + "" + CoverLetter + "'>click to view</a><br/><br/><br/>" + "Best regards" + "<br/><br/>" + "Archidply Decor" + "<br/>" + "";

            mail.Body = Body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["host"];
            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            smtp.Credentials = new System.Net.NetworkCredential
                   (ConfigurationManager.AppSettings["userName"], ConfigurationManager.AppSettings["password"]);
            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSsl"]);
            smtp.Send(mail);
            return 1;
        }

        catch (Exception exx)
        {

            return 0;

        }
    }
    public static int SendJobApply(string name, string Email)
    {
        try
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(Email);

            mail.From = new MailAddress(ConfigurationManager.AppSettings["from"], ConfigurationManager.AppSettings["fromName"]);
            mail.Subject = "Thank You for Applying to Archidply Decor";

            string Body = "Dear <strong>" + name + "</strong>,<br/><br/>Thank you for your interest in joining Archidply Decor team. We have received your application and our recruitment team is currently reviewing your submission.<br/><br/>We are thrilled to see your interest in becoming a part of our team. If your qualifications match our requirements, we will contact you soon to discuss the next steps. <br/><br/><br/> If you have any questions in the meantime, feel free to reach out to us at Mail: bangalore@archidply.com.<br/><br/><br/>Thanks & Regards<br/>Archidply Decor<br/>";

            mail.Body = Body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["host"];
            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            smtp.Credentials = new System.Net.NetworkCredential
                   (ConfigurationManager.AppSettings["userName"], ConfigurationManager.AppSettings["password"]);
            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSsl"]);
            smtp.Send(mail);
            return 1;
        }

        catch (Exception exx)
        {

            return 0;

        }
    }
    public static int SendReviewReply(string name, string Email, string ProdName)
    {
        try
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(Email);

            mail.From = new MailAddress(ConfigurationManager.AppSettings["from"], ConfigurationManager.AppSettings["fromName"]);
            mail.Subject = "Thank You for Your Review!";

            string Body = "Dear <strong>" + name + "</strong>,<br/><br/>Thank you for taking the time to share your thoughts about our product," + ProdName + " . We truly value your feedback and are thrilled to know about your experience..<br/><br/>Your insights help us improve and continue delivering the best experience possible. If you have any further comments or suggestions, feel free to reach out to us at any time. <br/><br/><br/>We hope to serve you again soon!.<br/><br/><br/>Thanks & Regards<br/>Archidply Decor<br/>";

            mail.Body = Body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["host"];
            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            smtp.Credentials = new System.Net.NetworkCredential
                   (ConfigurationManager.AppSettings["userName"], ConfigurationManager.AppSettings["password"]);
            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSsl"]);
            smtp.Send(mail);
            return 1;
        }

        catch (Exception exx)
        {

            return 0;

        }
    }
}
