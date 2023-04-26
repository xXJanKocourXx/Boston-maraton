using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;


using (Aes myAes = Aes.Create())
{
    foreach (var x in myAes.Key)
    {
        Console.Write(String.Format("{0:x2} ", x));
    }
    foreach (var x in myAes.IV)
    {
        Console.Write(String.Format("{0:x2} ", x));
    }
}
 /*
byte[] publicKey;
byte[] privateKey;
using (RSA rsa = RSA.Create())
{
    // Veřejný klíč
    File.WriteAllBytes("TestPublicKey.rsa", rsa.ExportRSAPublicKey());  // binární soubor
    string publicKeyXML = rsa.ToXmlString(false); // XML
    publicKey = rsa.ExportRSAPublicKey(); // pole bajtů v paměti​
    // Privátní klíč (obsahuje i veřejný klíč)
    File.WriteAllBytes("TestPrivateKey.rsa", rsa.ExportRSAPrivateKey()); // binární klíč
    string privateKeyXML = rsa.ToXmlString(true); // XML
    privateKey = rsa.ExportRSAPrivateKey(); // pole bajtů v paměti​

    RSAParameters rsaParams = rsa.ExportParameters(true); // získej parametry použité při výpočtu    
    Console.WriteLine("mezera pYčo " + privateKey);
}*/

string text1 = "Ahoj, světe";
string text2 = "Ahoj, květe";
byte[] hash1;
byte[] hash3;
using (SHA256 sha256Hash = SHA256.Create())
{
    hash1 = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(text1));
    hash3 = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(text2));

    WriteBytes(hash1); // b55460638c30fa90053090c95d40b67db7b37bfeb5d4fb2a7a07506edc2deb93
    WriteBytes(hash3); // a5163478bf307f1f84fcc62adf9ba84fba1ac975255e77e9f34e881e344aea4a​

    static void WriteBytes(byte[] data)
    {
        foreach (var x in data)
        {
            Console.Write(String.Format("{0:x2}", x));
        }
        Console.WriteLine();
        Console.WriteLine();
    } 
}



