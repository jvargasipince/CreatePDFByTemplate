using CreatePDFfromTemplate.Model;
using iTextSharp.text.pdf;
using System;
using System.Globalization;
using System.IO;

namespace CreatePDFfromTemplate.Util
{
    public class GeneratePDF
    {
        public GeneratePDF()
        {

        }
        public string GenerateInvestorDocument(ContractInfo contractInfo)
        {
            string fullName = string.Concat(contractInfo.FirstName, " ", contractInfo.LastName);

            string filePath = @"Template\";

            string fileNameExisting = @"TemplateContract_KaizenForce.pdf";
            string fileNameNew = @"KaizenForce_" + fullName.Replace(" ", "").Trim() + ".pdf";

            string fullNewPath = filePath + fileNameNew;
            string fullExistingPath = filePath + fileNameExisting;

            using (var existingFileStream = new FileStream(fullExistingPath, FileMode.Open))

            using (var newFileStream = new FileStream(fullNewPath, FileMode.Create))
            {
                // Open existing PDF
                var pdfReader = new PdfReader(existingFileStream);

                // PdfStamper, which will create
                var stamper = new PdfStamper(pdfReader, newFileStream);

                AcroFields fields = stamper.AcroFields;
                fields.SetField("FullName", fullName);
                fields.SetField("DocumentNumber", contractInfo.DocumentNumber);
                fields.SetField("Date", DateTime.Now.ToString("D", CultureInfo.CreateSpecificCulture("es-ES")));

                // "Flatten" the form so it wont be editable/usable anymore
                stamper.FormFlattening = true;

                stamper.Close();
                pdfReader.Close();
                
                return fileNameNew;
            }
        }
    }
}
