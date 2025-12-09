public class PdfServiceTests
{
    private IPdfService _pdfService;
    private TestPdfFileHandler _pdfFileHandler;
    private readonly string _dummyTextContent;

    public PdfServiceTests()
    {
        string pdfName="sample.pdf";
        _dummyTextContent="This is a sample PDF content for testing.";
        _pdfFileHandler = new TestPdfFileHandler(pdfName, _dummyTextContent);
        _pdfFileHandler.CreatePdfWithText();
        _pdfService = new PdfService(pdfName);
    }

    [Fact]
    public void TextExtractionTest()
    {
       Assert.Equal(_dummyTextContent, _pdfService.ConvertTheDocumentToText().Replace("\n", "").Trim());
       _pdfFileHandler.DeleteDummyPdfIfExists();
    }

    [Fact]
    public void FileNotFoundExceptionTest()
    {
       _pdfFileHandler.DeleteDummyPdfIfExists();
       Assert.Throws<FileNotFoundException>(() => _pdfService.ConvertTheDocumentToText());
    }

}
