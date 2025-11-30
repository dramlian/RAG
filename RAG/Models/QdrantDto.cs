public class QdrantDto
{
    public QdrantDto(IEnumerable<(string, IEnumerable<double>)> input)
    {
        points = input.Select((item, index) => new Point
        {
            id = index,
            vector = item.Item2.ToList(),
            payload = new Payload
            {
                text = item.Item1,
                category = "default"
            }
        }).ToList();
    }

    public IEnumerable<Point> points;

    public class Payload
    {
        public string? text;
        public string? category;
    }

    public class Point
    {
        public int id;
        public IReadOnlyCollection<double>? vector;
        public Payload? payload;
    }
}