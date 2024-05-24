using CommonLibrary;

namespace EncryptionService
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
          txtEncryptedText.Text =   EncryptionHelper.EncryptString(txtPlainText.Text);
                
        }
    }
}
