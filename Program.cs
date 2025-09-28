using System;
using System.Linq;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Azure.AI.OpenAI;
using OpenAI;

public class FindCosineSimilarity
{
    static async Task Main(string[] args)
    {


        try
        {
            string endpoint = "<Open AI Url>"; // ex https://abc.openai.azure.com/
            string deployment = "text-embedding-3-large";
            string apiKey = "<Open AI key>";
            string apiVersion = "2023-05-15";



            while (true)
            {
                Console.WriteLine("\n New Comparison");
                Console.WriteLine("Enter text 1:");
                string text1 = Console.ReadLine();

                Console.WriteLine("Enter text 2:");
                string text2 = Console.ReadLine();

                var vectorA = await GetEmbeddingAsync(endpoint, deployment, apiKey, apiVersion, text1);
                var vectorB = await GetEmbeddingAsync(endpoint, deployment, apiKey, apiVersion, text2);

                double dotProduct = vectorA.Zip(vectorB, (a, b) => a * b).Sum();
                double magnitudeA = Math.Sqrt(vectorA.Select(x => x * x).Sum());
                double magnitudeB = Math.Sqrt(vectorB.Select(x => x * x).Sum());
                double cosineSimilarity = dotProduct / (magnitudeA * magnitudeB);

                Console.WriteLine($"\n Cosine Similarity: {cosineSimilarity:F4}");

                Console.WriteLine("\n Do you want to compare another pair? (y/n)");
                string answer = Console.ReadLine()?.Trim().ToLower();
                if (answer != "y") break;
            }

            Console.WriteLine("\n Exiting. Thanks for comparing!");

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    static async Task<float[]> GetEmbeddingAsync(string endpoint, string deployment, string apiKey, string apiVersion, string inputText)
    {
        try
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("api-key", apiKey);

            string url = $"{endpoint}openai/deployments/{deployment}/embeddings?api-version={apiVersion}";

            var requestBody = new
            {
                input = inputText
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseJson);
            var embedding = doc.RootElement
                .GetProperty("data")[0]
                .GetProperty("embedding")
                .EnumerateArray()
                .Select(x => x.GetSingle())
                .ToArray();

            return embedding;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
    }
}
