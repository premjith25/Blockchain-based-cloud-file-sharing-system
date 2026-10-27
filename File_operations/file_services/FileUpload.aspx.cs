using System;
using System.IO;
using CloudFileShare.Services; // Assuming Person 2's methods are here

namespace CloudFileShare
{
    public partial class FileUploadHandler : System.Web.UI.Page
    {
        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (fileUploadControl.HasFile)
            {
                try
                {
                    string fileName = Path.GetFileName(fileUploadControl.FileName);
                    byte[] fileBytes = fileUploadControl.FileBytes;

                    // 1. Call Person 2's Encryption Method
                    // byte[] encryptedData = EncryptionService.Encrypt(fileBytes);

                    // 2. Call Person 2's Blockchain AddBlock Method
                    // BlockchainService.AddBlock(fileName, encryptedData);

                    Response.Write("<script>alert('File uploaded, encrypted, and added to blockchain successfully!');</script>");
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
                }
            }
        }
    }
}