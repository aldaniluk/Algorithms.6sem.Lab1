using System.Collections.Generic;

namespace Lab1
{
    public static class GraphCounter
    {
        public static IndependentVerticesSetResult GetIndependentVerticesSetWithMaxWeight(List<Vertex> vertices)
        {
            List<IndependentVerticesSetResult> resultVertices = new List<IndependentVerticesSetResult>();

            for (int vertexPosition = 0; vertexPosition < vertices.Count; vertexPosition++)
            {
                int currentVertexWeight = vertices[vertexPosition].Weight;
                var currentVertices = new List<int>();

                int resultMaxWeight2VerticesBack = vertexPosition < 2 ? 0 : resultVertices[vertexPosition - 2].MaxWeight;
                int resultMaxWeight3VerticesBack = vertexPosition < 3 ? 0 : resultVertices[vertexPosition - 3].MaxWeight;

                var resultVertices2VerticesBack = vertexPosition < 2 ? currentVertices : resultVertices[vertexPosition - 2].Vertices;
                var resultVertices3VerticesBack = vertexPosition < 3 ? currentVertices : resultVertices[vertexPosition - 3].Vertices;

                if (resultMaxWeight2VerticesBack < resultMaxWeight3VerticesBack)
                {
                    currentVertexWeight += resultMaxWeight3VerticesBack;
                    currentVertices = resultVertices3VerticesBack.ConvertAll(i => i);
                }
                else
                {
                    currentVertexWeight += resultMaxWeight2VerticesBack;
                    currentVertices = resultVertices2VerticesBack.ConvertAll(i => i);
                }

                currentVertices.Add(vertices[vertexPosition].Number);
                resultVertices.Add(new IndependentVerticesSetResult(currentVertexWeight, currentVertices));
            }

            int lastWeight = resultVertices.Count < 1 ? 0 : resultVertices[resultVertices.Count - 1].MaxWeight;
            int lastSecondWeight = resultVertices.Count < 2 ? 0 : resultVertices[resultVertices.Count - 2].MaxWeight;
            return lastWeight < lastSecondWeight ? resultVertices[resultVertices.Count - 2] : resultVertices[resultVertices.Count - 1];
        }
    }

    public class IndependentVerticesSetResult
    {
        public int MaxWeight { get; set; }

        public List<int> Vertices { get; set; }

        public IndependentVerticesSetResult(int maxWeight, List<int> vertices)
        {
            MaxWeight = maxWeight;
            Vertices = vertices;
        }
    }

    public class Vertex
    {
        public int Number { get; set; }

        public int Weight { get; set; }

        public Vertex(int number, int weight)
        {
            Number = number;
            Weight = weight;
        }
    }
}
