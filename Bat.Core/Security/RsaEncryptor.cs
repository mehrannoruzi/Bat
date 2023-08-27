using System.Text;
using System.Security.Cryptography;

namespace Bat.Core;

public class RsaEncryptor
{
	public static byte[] Sign(string privateKeyAddress, string privateKeyPassword, string data)
	{
		var keyContent = File.ReadAllText(privateKeyAddress);
		RSA rsa = RSA.Create();
		rsa.ImportFromEncryptedPem(keyContent, privateKeyPassword);
		var dataBytes = Encoding.ASCII.GetBytes(data);
		var signBytes = rsa.SignData(dataBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

		return signBytes;
	}

	public static bool Verify(string publicKeyAdress, byte[] data, byte[] signedData)
	{
		var keyContent = File.ReadAllText(publicKeyAdress);
		RSA rsa = RSA.Create();
		rsa.ImportFromPem(keyContent);
		var isVerified = rsa.VerifyData(data, signedData, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

		return isVerified;
	}
}