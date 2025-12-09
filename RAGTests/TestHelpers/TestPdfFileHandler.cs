using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

public class TestPdfFileHandler
{
    private readonly string _dummyPdfPath;
    private readonly string _dummyTextContent;

    public TestPdfFileHandler(string dummyPdfPath, string dummyTextContent)
    {
        _dummyPdfPath = dummyPdfPath;
        _dummyTextContent = dummyTextContent;
    }
    public void CreatePdfWithText()
    { 
        DeleteDummyPdfIfExists();
        using (var writer = new PdfWriter(_dummyPdfPath))
        {
            using (var pdf = new PdfDocument(writer))
            {
                var document = new Document(pdf);
                document.Add(new Paragraph(_dummyTextContent));
                document.Close();
            }
        }
    }

    public void DeleteDummyPdfIfExists()
    {
        if (DoesThePdfExist())
        {
            File.Delete(_dummyPdfPath);
        }
    }

    public bool DoesThePdfExist()
    {
        return File.Exists(_dummyPdfPath);
    }   
}