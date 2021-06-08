using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using Core_SatelliteBlogSitesi.Models.DAL;
using Core_SatelliteBlogSitesi.Models.DATA;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Text.RegularExpressions;

namespace Core_SatelliteBlogSitesi.Models.Methods
{
    public class GeneralMethods
    {
        public static void SendActivationMail(User user)
        {
            using (MailMessage mm = new MailMessage("satellitebloginfo@gmail.com", user.Email))
            {
                mm.Subject = "Aktivasyon Maili";
                string body = "Merhaba " + user.FirstName + ",";
                body += "<br /><br />Lütfen aşağıdaki aktivasyon linkine tıklayarak üyeliğinizi tamamlayınız.";
                body += "<br /><a href = '" + string.Format("https://localhost:44304/User/Activation/{0}", user.ActivationCode) + "'>Buraya Tıklayarak Üyeliğinizi Aktive Edebilirsiniz.</a>";
                body += "<br /><br />Teşekkürler.";
                mm.Body = body;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                NetworkCredential NetworkCred = new NetworkCredential("satellitebloginfo@gmail.com", "123456789BaBoost");
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
                mm.Dispose();
            }
        }

        public static void GirisEmail(User user)
        {
            using (MailMessage mm = new MailMessage("satellitebloginfo@gmail.com", user.Email))
            {
                mm.Subject = "Giriş Maili";
                string body = "Merhaba " + user.FirstName + ",";
                body += "<br /><br />Lütfen aşağıdaki Giriş linkine tıklayarak devam ediniz.";
                body += "<br /><a href = '" + string.Format("https://localhost:44304/Anasayfa") + "'>Buraya Tıklayarak Siteye Giriş Yapabilirsiniz.</a>";
                body += "<br /><br />Teşekkürler.";
                mm.Body = body;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                NetworkCredential NetworkCred = new NetworkCredential("satellitebloginfo@gmail.com", "123456789BaBoost");
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
                mm.Dispose();
            }
        }

        [Obsolete]
        public static string UploadImage(IFormFile file, IHostingEnvironment environment)
        {
            string uniqueFileName = string.Empty;

            string uploadsFolder = Path.Combine(environment.WebRootPath, "postedImage");
            uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;

            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return uniqueFileName;
        }


        public static void DeleteImage(string imageName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\image\\postedImage", imageName);

            if (File.Exists(path)) File.Delete(path);
        }



        public static short CalculateReadTime(string articleContent)
        {
            Regex rx = new Regex("<[^>]*>");

            string contentWithoutTags = rx.Replace(articleContent, "");
            contentWithoutTags = contentWithoutTags.Replace("&nbsp", " ");

            short wordCount = GeneralMethods.GetWordCount(contentWithoutTags);

            short readTime = (short)(Math.Round((decimal)(wordCount / 250)));

            if (readTime == 0) readTime = 1;

            return readTime;
        }


        public static short GetWordCount(string text)
        {
            short wordCount = 0, index = 0;

            // skip whitespace until first word
            while (index < text.Length && char.IsWhiteSpace(text[index]))
                index++;

            while (index < text.Length)
            {
                // check if current char is part of a word
                while (index < text.Length && !char.IsWhiteSpace(text[index]))
                    index++;

                wordCount++;

                // skip whitespace until next word
                while (index < text.Length && char.IsWhiteSpace(text[index]))
                    index++;
            }

            return wordCount;
        }
    }
}

