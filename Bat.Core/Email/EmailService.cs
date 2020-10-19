using System;
using System.Net;
using System.Text;
using System.Linq;
using System.Net.Mail;
using System.Collections.Generic;

namespace Bat.Core
{
    public class EmailService : IEmailService
    {
        private readonly int _port;
        private readonly string _host;
        private readonly bool _enableSsl;
        private readonly string _username;
        private readonly string _password;
        private readonly SmtpClient _emailService;
        private readonly NetworkCredential _userCredential;

        /// <summary>
        /// ctor for EmailService
        /// </summary>
        /// <param name="host">host address of email server</param>
        /// <param name="username">email sender username</param>
        /// <param name="password">email sender password</param>
        public EmailService(string host, string username, string password, bool enableSsl = false)
        {
            //smtp.gmail.com
            //Google Port 465 (SSL required)
            //Google Port 587 (TLS required)
            //Port : 252

            //webmail.hillavas.com
            //Hillavas Port : 587

            _port = 25;
            _host = host;
            _username = username;
            _password = password;
            _enableSsl = enableSsl;
            _emailService = new SmtpClient(_host, _port);
            _userCredential = new NetworkCredential { UserName = _username, Password = _password };
        }

        /// <summary>
        /// ctor for EmailService
        /// </summary>
        /// <param name="host">host address of email server</param>
        /// <param name="port">port of email server</param>
        /// <param name="username">email sender username</param>
        /// <param name="password">email sender password</param>
        public EmailService(string host, int port, string username, string password, bool enableSsl = false)
        {
            //smtp.gmail.com
            //Google Port 465 (SSL required)
            //Google Port 587 (TLS required)
            //Port : 252

            //webmail.hillavas.com
            //Hillavas Port : 587

            //Port : 25

            _host = host;
            _port = port;
            _username = username;
            _password = password;
            _enableSsl = enableSsl;
            _emailService = new SmtpClient(_host, _port);
            _userCredential = new NetworkCredential { UserName = _username, Password = _password };
        }




        /// <summary>
        /// send 1 email to many receiver from 1 sender
        /// replace *** with EmailMessage.htmlTemplate
        /// replace ### with EmailMessage.AtachmentsLink
        /// </summary>
        /// <param name="from">sender address</param>
        /// <param name="to">receiver address</param>
        /// <param name="email">email message</param>
        /// <returns>sended emails status</returns>
        public IResponse<List<bool>> Send(string from, List<string> to, EmailMessage email)
        {
            var response = new Response<List<bool>>();
            var sendStatus = new List<bool>();

            try
            {
                #region Error Handeling
                if (to == null || to.Count() == 0)
                {
                    response.IsSuccessful = false;
                    response.Message = "لیست گیرنده ها خالی است";
                    return response;
                }
                #endregion

                #region Send Email
                _emailService.Credentials = _userCredential;
                _emailService.EnableSsl = _enableSsl;

                foreach (var address in to)
                {
                    try
                    {
                        var body = string.IsNullOrEmpty(email.HtmlTemplate) ? email.Body : email.HtmlTemplate.Replace("***", email.Body);
                        if (email.AtachmentsLink != null && email.AtachmentsLink.Any())
                        {
                            var links = string.Empty;
                            foreach (var item in email.AtachmentsLink)
                            {
                                links = item + "</br>";
                            }
                            body = body.Replace("###", links);
                        }
                        var message = new MailMessage(from: from, to: address)
                        {
                            Subject = email.Subject,
                            SubjectEncoding = Encoding.UTF8,
                            IsBodyHtml = true,
                            Body = body,
                            BodyEncoding = Encoding.UTF8,
                            //BodyTransferEncoding = System.Net.Mime.TransferEncoding.Base64,
                            DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure,
                        };
                        _emailService.Send(message);
                        sendStatus.Add(true);
                    }
                    catch (Exception e)
                    {
                        sendStatus.Add(false);

                        response.Result = sendStatus;
                        response.Message = e.Message;
                        return response;
                    }
                }
                #endregion

                response.IsSuccessful = true;
                response.Message = "ایمیل های مورد نظر با موفقیت ارسال شد";
                response.Result = sendStatus;
                return response;
            }
            catch
            {
                response.IsSuccessful = false;
                response.Message = "ارسال ایمیل های مورد نظر با با خطا رو به رو شده است";
                return response;
            }
        }

        /// <summary>
        /// send 1 email to many receiver from many sender
        /// replace *** with EmailMessage.htmlTemplate
        /// replace ### with EmailMessage.AtachmentsLink
        /// </summary>
        /// <param name="from">sender address</param>
        /// <param name="to">receiver address</param>
        /// <param name="email">email message</param>
        /// <returns>sended emails status</returns>
        public IResponse<List<bool>> Send(List<string> from, List<string> to, EmailMessage email)
        {
            var response = new Response<List<bool>>();
            var sendStatus = new List<bool>();

            try
            {
                #region Error Handeling
                if (from == null || from.Count() == 0)
                {
                    response.IsSuccessful = false;
                    response.Message = "لیست فرستنده ها خالی است";
                    return response;
                }
                if (to == null || to.Count() == 0)
                {
                    response.IsSuccessful = false;
                    response.Message = "لیست گیرنده ها خالی است";
                    return response;
                }
                if (from.Count() != to.Count())
                {
                    response.IsSuccessful = false;
                    response.Message = "تعداد آدرس های فرستنده با آدرس های گیرنده یکسان نمی باشد";
                    return response;
                }
                #endregion

                #region Send Email
                _emailService.Credentials = _userCredential;
                _emailService.EnableSsl = _enableSsl;

                for (int i = 0; i < from.Count(); i++)
                {
                    try
                    {
                        var body = string.IsNullOrEmpty(email.HtmlTemplate) ? email.Body : email.HtmlTemplate.Replace("***", email.Body);
                        if (email.AtachmentsLink != null && email.AtachmentsLink.Any())
                        {
                            var links = string.Empty;
                            foreach (var item in email.AtachmentsLink)
                            {
                                links = item + "</br>";
                            }
                            body = body.Replace("###", links);
                        }
                        var message = new MailMessage(from: from[i], to: to[i])
                        {
                            Subject = email.Subject,
                            SubjectEncoding = Encoding.Unicode,
                            IsBodyHtml = true,
                            Body = body,
                            BodyEncoding = Encoding.Unicode,
                            //BodyTransferEncoding = System.Net.Mime.TransferEncoding.Base64,
                            DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure,
                        };
                        _emailService.Send(message);
                        sendStatus.Add(true);
                    }
                    catch
                    {
                        sendStatus.Add(false);
                    }
                }
                #endregion

                response.IsSuccessful = true;
                response.Message = "ایمیل های مورد نظر با موفقیت ارسال شد";
                response.Result = sendStatus;
                return response;
            }
            catch
            {
                response.IsSuccessful = false;
                response.Message = "ارسال ایمیل های مورد نظر با با خطا رو به رو شده است";
                return response;
            }
        }

        /// <summary>
        /// send many email to many receiver from many sender
        /// replace *** with EmailMessage.htmlTemplate
        /// replace ### with EmailMessage.AtachmentsLink
        /// </summary>
        /// <param name="from">sender address</param>
        /// <param name="to">receiver address</param>
        /// <param name="email">email message</param>
        /// <returns>sended emails status</returns>
        public IResponse<List<bool>> Send(List<string> from, List<string> to, List<EmailMessage> email)
        {
            var response = new Response<List<bool>>();
            var sendStatus = new List<bool>();

            try
            {
                #region Error Handeling
                if (from == null || from.Count() == 0)
                {
                    response.IsSuccessful = false;
                    response.Message = "لیست فرستنده ها خالی است";
                    return response;
                }
                if (to == null || to.Count() == 0)
                {
                    response.IsSuccessful = false;
                    response.Message = "لیست گیرنده ها خالی است";
                    return response;
                }
                if (email == null || email.Count() == 0)
                {
                    response.IsSuccessful = false;
                    response.Message = "لیست ایمیل ها خالی است";
                    return response;
                }
                if (from.Count() != to.Count())
                {
                    response.IsSuccessful = false;
                    response.Message = "تعداد آدرس های فرستنده با آدرس های گیرنده یکسان نمی باشد";
                    return response;
                }
                if (from.Count() != email.Count())
                {
                    response.IsSuccessful = false;
                    response.Message = "تعداد آدرس های فرستنده با تعداد ایمیل ها یکسان نمی باشد";
                    return response;
                }
                #endregion

                #region Send Email
                _emailService.Credentials = _userCredential;
                _emailService.EnableSsl = _enableSsl;

                for (int i = 0; i < from.Count(); i++)
                {
                    try
                    {
                        var body = string.IsNullOrEmpty(email[i].HtmlTemplate) ? email[i].Body : email[i].HtmlTemplate.Replace("***", email[i].Body);
                        if (email[i].AtachmentsLink != null && email[i].AtachmentsLink.Any())
                        {
                            var links = string.Empty;
                            foreach (var item in email[i].AtachmentsLink)
                            {
                                links = item + "</br>";
                            }
                            body = body.Replace("###", links);
                        }
                        var message = new MailMessage(from: from[i], to: to[i])
                        {
                            Subject = email[i].Subject,
                            SubjectEncoding = Encoding.Unicode,
                            IsBodyHtml = true,
                            Body = body,
                            BodyEncoding = Encoding.Unicode,
                            //BodyTransferEncoding = System.Net.Mime.TransferEncoding.Base64,
                            DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure,
                        };
                        _emailService.Send(message);
                        sendStatus.Add(true);
                    }
                    catch
                    {
                        sendStatus.Add(false);
                    }

                }
                #endregion

                response.IsSuccessful = true;
                response.Message = "ایمیل های مورد نظر با موفقیت ارسال شد";
                response.Result = sendStatus;
                return response;
            }
            catch
            {
                response.IsSuccessful = false;
                response.Message = "ارسال ایمیل های مورد نظر با با خطا رو به رو شده است";
                return response;
            }
        }

    }
}
