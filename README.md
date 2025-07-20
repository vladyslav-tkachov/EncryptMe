# Encrypt Me – WinForms AES String Encryptor

A simple, secure Windows Forms application for encrypting any string using AES-256 and saving the encrypted string, key, and IV as a JSON file.

---

## Features

- **AES-256 CBC Encryption:** Strong, industry-standard security.
- **User-friendly Interface:** Enter text, click Encrypt, and choose where to save.
- **Self-contained Single EXE:** No .NET installation required on target machine.
- **Portable Output:** Output includes encrypted string, encryption key, and IV in base64-encoded JSON.

---

## Publishing a Self-Contained, Single-File Executable

**Requirements:**
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) or later
- Windows OS (WinForms only runs on Windows)

**Recommended .csproj Settings:**
```xml
<Project Sdk="Microsoft.NET.Sdk.WindowsDesktop">
  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>net9.0-windows</TargetFramework>
    <UseWindowsForms>true</UseWindowsForms>
    <AssemblyName>Encrypt Me</AssemblyName>
    <RuntimeIdentifier>win-x64</RuntimeIdentifier>
    <SelfContained>true</SelfContained>
    <PublishSingleFile>true</PublishSingleFile>
    <PublishReadyToRun>true</PublishReadyToRun>
    <EnableCompressionInSingleFile>true</EnableCompressionInSingleFile>
  </PropertyGroup>
</Project>
````

**How to Publish:**

1. Open a terminal/command prompt in the project directory.
2. Run the following command:

   ```
   dotnet publish -c Release -r win-x64 --self-contained true
   ```
3. The executable will be in:

   ```
   ./bin/Release/net9.0-windows/win-x64/publish/
   ```

   * File will be named `Encrypt Me.exe` (as set in `.csproj`).

---

## How to Use

1. **Run** `Encrypt Me.exe`.
2. **Enter** the string you wish to encrypt into the text box.
3. **Click** the “Encrypt” button.
4. **Choose** the output folder when prompted.
5. The app saves a JSON file in the selected folder containing:

   * `encrypted` – the encrypted string (base64)
   * `key` – the encryption key (base64)
   * `iv` – the initialization vector (base64)

> **Keep your key and IV safe** – they are needed for decryption!

---

## Troubleshooting

* If the app opens slowly on first launch, this is normal for self-contained .NET WinForms apps due to runtime extraction.
* If you see many DLLs in `publish/`, ensure your `.csproj` has `PublishSingleFile` set to `true` and use the correct publish command above.
* Only Windows is supported.

---

## License

MIT License


---
