using Cryptosoft;

Encrypt encrypt = new Encrypt();
bool run = true;
Console.WriteLine("Welcome to Cryptosoft !");
while (run)
{
    try
    {
        Console.WriteLine("Choose an option :");
        Console.WriteLine("1. Encrypt files");
        Console.WriteLine("2. Decrypt files");
        string choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                Console.WriteLine("Encrypting files...");
                EncryptView();
                break;
            case "2":
                Console.WriteLine("Decrypting files...");
                DecryptView();
                break;
            default:
                Console.WriteLine("Invalid choice. Please enter 1 or 2.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error : {ex.Message}");
    }
}

void EncryptView()
{
    Console.WriteLine("Choose the path of the file or folder you want to encrypt :");
    encrypt.SourceEncryptPath = Console.ReadLine();
    encrypt.SourceEncryptPath = encrypt.ConvertToLocalUNC(encrypt.SourceEncryptPath);
    Console.WriteLine("Choose the path of the folder where you want to have the result :");
    encrypt.TargetEncryptPath = Console.ReadLine();
    encrypt.TargetEncryptPath = encrypt.ConvertToLocalUNC(encrypt.TargetEncryptPath);
    if (File.Exists(encrypt.SourceEncryptPath) || Directory.Exists(encrypt.SourceEncryptPath))
    {
        encrypt.EncryptData();
    }
    else
    {
        Console.WriteLine("Le chemin spécifié n'existe pas.");
    }
}
void DecryptView()
{
    Console.WriteLine("Choose the path of the file or folder you want to decrypt :");
    encrypt.TargetDecryptPath = Console.ReadLine();
    encrypt.TargetDecryptPath = encrypt.ConvertToLocalUNC(encrypt.TargetDecryptPath);
    if (Directory.Exists(encrypt.TargetDecryptPath))
    {
        encrypt.DecryptFiles();
    }
    else
    {
        Console.WriteLine("Le chemin spécifié n'existe pas.");
    }
}
