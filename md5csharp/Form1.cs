using System.Security.Cryptography;
using System.Text;

namespace md5csharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //md5 þifreleme metodu
        string hash = "";
        string Encrypt(string text)
        {
            byte[] data = Encoding.Default.GetBytes(text);

            using (MD5CryptoServiceProvider md5csharp = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5csharp.ComputeHash(Encoding.Default.GetBytes(hash));

                using (TripleDESCryptoServiceProvider tripleDES=new TripleDESCryptoServiceProvider() 
                { Key=keys, Mode=CipherMode.ECB,Padding=PaddingMode.PKCS7})
                {
                    ICryptoTransform transform = tripleDES.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    return Convert.ToBase64String(results); 
                }
            }
        }
        //þifre çözümü
      
        string Descrypt(string text)
        {
            byte[] data = Convert.FromBase64String(text);

            using (MD5CryptoServiceProvider md5csharp = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5csharp.ComputeHash(Encoding.Default.GetBytes(hash));

                using (TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider()
                { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripleDES.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    return Encoding.Default.GetString(results); 
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            txtSifrelenen.Text = Encrypt(txtMetin.Text); 
            txtCozulen.Text = Descrypt(txtSifrelenen.Text);
        }
    }
}