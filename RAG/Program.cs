PdfService pdfService = new PdfService("/home/damian/Documents/workfolder/RAG/RAG/monopoly instructions.pdf");
var text = pdfService.ConvertTheDocumentToText();

OverLayChunksService overLayChunksService = new OverLayChunksService(text);
var chunks = overLayChunksService.GetOverLayedChunks(5);


HttpClientService httpClientService = new HttpClientService(new HttpClient());
SemanticVectorService semanticVectorService = new SemanticVectorService(httpClientService);
var QdrantDto = await semanticVectorService.GetSemanticVectors(chunks);

IQdrantService qdrantService = new QdrantService("module_collection", 2048);
await qdrantService.DeleteCollection();
await qdrantService.CreateCollection();
await qdrantService.UpdateInsertPoints(QdrantDto.points);

IPromptService promptService = new PromptService(qdrantService, semanticVectorService);
var finalPrompt = await promptService.CreateCustomQuery("What happens to a bankrupt player’s properties?");

IOllamaService ollamaService = new OllamaService(httpClientService);
Console.WriteLine(await ollamaService.GetAnswer(finalPrompt));

