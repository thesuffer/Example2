using Microsoft.Bot.Schema;
using PersonalAssistantBot.Entities;
using PersonalAssistantBot.Models;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static QRCoder.PayloadGenerator;

namespace PersonalAssistantBot.QRCodeDataEmployee
{
    public class PayloadGenerator
    {

        public Attachment QRCodeGeneration(EmployeeInfoPayload employeeInfoPayload)
        {
            ContactData generator = new ContactData(ContactData.ContactOutputType.VCard3, firstname: employeeInfoPayload.FullName.ToString(), lastname: null, note: "Position:" + " " + employeeInfoPayload.PositionName.ToString() + " " + "Departament:" + employeeInfoPayload.Departament.ToString(), email: employeeInfoPayload.Email.ToString(), workPhone: employeeInfoPayload.Ext.ToString(), mobilePhone: employeeInfoPayload.MobilePhone.ToString(), city: employeeInfoPayload.City.ToString());
            string payload = generator.ToString();


            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            var qrCodeAsBitmap = qrCode.GetGraphic(12);

            qrCodeAsBitmap.Save("./wwwroot/images/QRCode.png");
            
            var imagePath = Path.Combine(Environment.CurrentDirectory, @"./wwwroot/images", "QRCode.png");
            var imageData = Convert.ToBase64String(File.ReadAllBytes(imagePath));
            return new Attachment
            {
                Name = @"./wwwroot/images/QRCode.png",
                ContentType = "image/png",
                ContentUrl = $"data:image/png;base64,{imageData}",
            };
        }
    }
}
