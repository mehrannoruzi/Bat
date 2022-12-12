For use Bat.Core just do it :

1- Install Bat.Core on your project

2- Use it in bussiness logic
for example :




    public class BaseService : IBaseService
    {
        public void UseBatCoreSample()
        {
            FileLoger.Info($"Method Started At {DateTime.Now}");
            FileLoger.CriticalInfo($"Method Started At {DateTime.Now}");

            string randomString = Randomizer.GetUniqueKey(length: 10);
            int randomInteger = Randomizer.GetRandomInteger(length: 6);

            string time = DateTime.Now.ToTime();
            DateTime miladiDate = "1400/12/15".ToDateTime();
            string persianDate = DateTime.Now.ToPersianDate();

            bool validation1 = "https://google.com".IsUrl();
            bool validation2 = "1400/12/15".IsPersianDate();
            bool validation3 = "0080799999".IsNationalCode();

            string encryptedString1 = AesEncryption.Encrypt(plainText: "Encrypt Me");
            string encryptedString2 = AesEncryption.Encrypt(plainText: "Encrypt Me", encryptKey: "EncryptionKey");
            string encryptedString3 = AesEncryption.Encrypt(plainText: "Encrypt Me", encryptKey: "EncryptionKey", salt: "Salt");
        
            string decryptedString1 = AesEncryption.Decrypt(cipherText: encryptedString1);
            string decryptedString2 = AesEncryption.Decrypt(cipherText: encryptedString2, encryptKey: "EncryptionKey");
            string decryptedString3 = AesEncryption.Decrypt(cipherText: encryptedString3, encryptKey: "EncryptionKey", salt: "Salt");

            var newObject = new
            {
                Id = 1,
                FirstName = "Mehran",
                LastName = "Norouzi"
            };
            string serialized = newObject.SerializeToJson();
            object deSerialized = serialized.DeSerializeJson();

        }
    }
