using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace EncryptMe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            txtInput.Focus();
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            string plaintext = txtInput.Text.Trim();
            if (string.IsNullOrWhiteSpace(plaintext))
            {
                MessageBox.Show("Input cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.KeySize = 256;
                    aes.Mode = CipherMode.CBC;
                    aes.GenerateKey();
                    aes.GenerateIV();

                    byte[] encrypted = EncryptString(plaintext, aes.Key, aes.IV, Encoding.UTF8);

                    string encB64 = Convert.ToBase64String(encrypted);
                    string keyB64 = Convert.ToBase64String(aes.Key);
                    string ivB64 = Convert.ToBase64String(aes.IV);

                    // Manual JSON output
                    string json = $"{{\n  \"encrypted\": \"{encB64}\",\n  \"key\": \"{keyB64}\",\n  \"iv\": \"{ivB64}\"\n}}";
                    string fileName = string.Empty;

                    using (var dialog = new FolderBrowserDialog())
                    {
                        dialog.Description = "Select a folder to save the encrypted JSON file.";
                        dialog.UseDescriptionForTitle = true;
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            string selectedPath = dialog.SelectedPath;
                            fileName = $"encrypted_{DateTime.Now:yyyy_dd_MM-HHmmss}.json";
                            string fullPath = Path.Combine(selectedPath, fileName);
                            File.WriteAllText(fullPath, json, Encoding.UTF8);

                            MessageBox.Show($"Encryption successful!\nFile written: {fullPath}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No folder selected. File not saved.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }


                    MessageBox.Show($"Encryption successful!\nFile written: {fileName}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"[ERROR] {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static byte[] EncryptString(string plainText, byte[] key, byte[] iv, Encoding encoding)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt, encoding))
                {
                    swEncrypt.Write(plainText);
                    swEncrypt.Flush();
                    csEncrypt.FlushFinalBlock();
                    return msEncrypt.ToArray();
                }
            }
        }
    }
}
