public class PdfService
{
    private readonly string _pdfPath;

    public PdfService(string pdfPath)
    {
        _pdfPath = pdfPath;
    }

    public string ConvertTheDocumentToText()
    {
        string folderPath = System.IO.Path.GetDirectoryName(_pdfPath)
            ?? throw new Exception("No path for document found");
        /*
        the logic
        */
        return folderPath;

    }

}