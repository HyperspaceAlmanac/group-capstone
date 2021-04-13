using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace CarRentalService.TwilioSend
{
    public static class TwilioText
    {
        public static void SendTextToDriver(string customerNumber, string doorKey)
        {
            string accountSid = Secrets.TWILIO_ACCOUNT_SID;
            string authToken = Secrets.TWILIO_AUTH_TOKEN;

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: $"Your door code is {doorKey}",
                from: new Twilio.Types.PhoneNumber(Secrets.TWILIO_PHONE_NUMBER),
                to: new Twilio.Types.PhoneNumber(customerNumber)
            );

            Console.WriteLine(message.Sid);
        }
    }
}
