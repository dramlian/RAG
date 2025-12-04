PdfService pdfService = new PdfService("/home/damian/Documents/workfolder/RAG/RAG/monopoly instructions.pdf");
var text = pdfService.ConvertTheDocumentToText();

OverLayChunksService overLayChunksService = new OverLayChunksService(text);

var chunks = overLayChunksService.GetOverLayedChunks(5);

HttpClient client = new HttpClient();

HttpClientService httpClientService = new HttpClientService(client);

SemanticVectorService semanticVectorService = new SemanticVectorService(httpClientService, chunks);
var QdrantDto = await semanticVectorService.GetSemanticVectors();



IQdrantService service = new QdrantService("module_collection", 2048);

await service.DeleteCollection();
await service.CreateCollection();


await service.UpdateInsertPoints(QdrantDto.points);

var queryVector = Enumerable.Range(1, 2048).Select(_ => (float)new Random().NextDouble()).ToArray();
var returned = await service.SearchForPoints(2, queryVector);

foreach (var point in returned)
{
  Console.WriteLine(point);
}


