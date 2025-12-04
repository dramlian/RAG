using Qdrant.Client.Grpc;

public class QdrantDto
{
    public List<PointStruct> points;
    public QdrantDto(List<(string, float[])> inputs)
    {
        points = new List<PointStruct>();
        for (int i = 0; i < inputs.Count(); i++)
        {
            points.Add(new PointStruct
            {
                Id = (ulong)i,
                Vectors = inputs[i].Item2,
                Payload =
                {
                ["text"] = inputs[i].Item1,
            }
            });
        }
    }
}