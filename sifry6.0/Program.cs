﻿using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;


using (Aes myAes = Aes.Create())
{
    foreach (var x in myAes.Key)
    {
        Console.Write(String.Format("{0:x2} ", x, "\n"));
    }
    foreach (var x in myAes.IV)
    {
        Console.Write(String.Format("{0:x2} ", x, "\n"));
    }
}



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
   Console.WriteLine("\nJara je homosexual " + privateKey);
}



Console.WriteLine("\n Zadejte 1. string na převedení pomocí HASH");
string text1 = Console.ReadLine();
Console.WriteLine("\n Zadejte 2. string na převedení pomocí HASH");
string text2 = Console.ReadLine();


byte[] hash1;
byte[] hash2;

using (SHA256 sha256Hash = SHA256.Create())
{
    hash1 = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(text1));
    hash2 = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(text2));

    WriteBytes(hash1); // b55460638c30fa90053090c95d40b67db7b37bfeb5d4fb2a7a07506edc2deb93
    WriteBytes(hash2); // a5163478bf307f1f84fcc62adf9ba84fba1ac975255e77e9f34e881e344aea4a​

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

//verejny klic dekrypce
string text = "Secret message.";
byte[] original = Encoding.ASCII.GetBytes(text);
byte[] encrypted;
using (RSA rsa = RSA.Create())
{
    int amount;
    rsa.ImportRSAPublicKey(publicKey, out amount);
    encrypted = rsa.Encrypt(original, RSAEncryptionPadding.Pkcs1);
}


//privatni klic dekrypce
byte[] received;
using (RSA rsa = RSA.Create())
{
    int amount;
    rsa.ImportRSAPrivateKey(privateKey, out amount);
    received = rsa.Decrypt(encrypted, RSAEncryptionPadding.Pkcs1);
}

