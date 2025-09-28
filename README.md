# 🔍 Semantic Text Similarity with Azure OpenAI

This project demonstrates how to calculate semantic similarity between two pieces of text using Azure OpenAI's `text-embedding-3-large` model. Built as a C# .NET 8 console application(same code can be applied on api's, httpfuncions, etc...), it leverages the Embeddings API via raw HTTP calls to generate high-dimensional vector representations of input texts and computes their cosine similarity.

## 🚀 Features
- Uses Azure OpenAI Embeddings API (via REST)
- Calculates cosine similarity between two texts
- Interactive console loop for repeated comparisons
- Supports real-time input and batch testing
- Ideal for NLP research, semantic search, and clustering

## 🧠 Technologies
- .NET 8
- C#
- Azure OpenAI (Embeddings API)
- RESTful HTTP with `HttpClient`

## 📦 How to Run
1. Clone the repository
2. Replace your Azure OpenAI endpoint, deployment name, and API key in `Program.cs`
3. Run the app:
   ```bash
   dotnet run
