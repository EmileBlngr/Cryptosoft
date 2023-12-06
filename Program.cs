using Cryptosoft;

Encrypt encrypt = new Encrypt();
bool run = true;
Console.WriteLine("Welcome to Cryptosoft !");
try
{
    if (args.Length == 2)
    {
        string action = args[0];
        encrypt.TargetDecryptPath = args[1];
        encrypt.TargetDecryptPath = encrypt.ConvertToLocalUNC(encrypt.TargetDecryptPath);
        switch (action)
        {
            case "-d":
                encrypt.DecryptFiles();
                break;
            default:
                ShowUsage();
                break;
        }
        
    }
    else if (args.Length == 3)
    {
        string action = args[0];
        encrypt.SourceEncryptPath = args[1];
        encrypt.TargetEncryptPath = args[2];
        encrypt.SourceEncryptPath = encrypt.ConvertToLocalUNC(encrypt.SourceEncryptPath);
        encrypt.TargetEncryptPath = encrypt.ConvertToLocalUNC(encrypt.TargetEncryptPath);
        switch (action)
        {
            case "-e":
                encrypt.EncryptData();
                break;
            default:
                ShowUsage();
                break;
        }      
    }
    else
    {
        ShowUsage();
    }
} 
catch (Exception ex)
{
    Console.WriteLine($"Error : {ex}");
}

void ShowUsage()
{
    Console.WriteLine("Cryptosoft.exe -e <source_path> <target_path> to encrypt");
    Console.WriteLine("Or Cryptosoft.exe -d <target_path> to decrypt");
}