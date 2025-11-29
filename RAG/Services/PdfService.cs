using System.Text;
using UglyToad.PdfPig;

public class PdfService
{
    private readonly string _pdfPath;

    public PdfService(string pdfPath)
    {
        _pdfPath = pdfPath;
    }

    public string ConvertTheDocumentToText()
    {
        if (!File.Exists(_pdfPath))
            throw new FileNotFoundException("PDF not found: " + _pdfPath);

        var sb = new StringBuilder();

        using (var pdf = PdfDocument.Open(_pdfPath))
        {
            foreach (var page in pdf.GetPages())
            {
                sb.AppendLine(page.Text);
            }
        }

        return sb.ToString();
    }
}
