# RAG (Retrieval-Augmented Generation) - C# Implementation

A C# implementation of a Retrieval-Augmented Generation (RAG) system that processes PDF documents and enables intelligent question-answering using local AI models.

## 🎯 Overview

This RAG implementation allows you to ask questions about PDF documents by combining semantic search with large language models. The system extracts text from PDFs, converts it into semantic vectors, stores them in a vector database, and retrieves relevant context to generate accurate answers.

## 🏗️ Architecture

The system uses the following components:

- **Qdrant**: Vector database for storing and searching semantic embeddings
- **Ollama with Phi3 model**: Local LLM for generating embeddings and completions
- **PdfPig**: PDF text extraction library
- **.NET 9.0**: Runtime environment

### How It Works

1. **PDF Processing**: Extract text content from PDF documents
2. **Text Chunking**: Split the text into manageable chunks
3. **Overlay Chunking**: Create overlapping chunks to maintain context continuity
4. **Semantic Vectorization**: Convert each chunk into semantic vectors using Ollama's Phi3 model
5. **Vector Storage**: Store all vectors in Qdrant vector database
6. **Query Processing**: Convert user queries into semantic vectors
7. **Similarity Search**: Use Qdrant to find the most relevant chunks via semantic search
8. **Context Augmentation**: Compose a final prompt with retrieved context
9. **Answer Generation**: Generate accurate answers using the LLM with relevant context

## 📋 Prerequisites

- **Docker** with GPU passthrough support enabled
- **NVIDIA GPU** (required for Ollama to run efficiently)
- **.NET 9.0 SDK** or later

### Docker GPU Setup

Ensure Docker has access to your GPU. You need:
- NVIDIA Container Toolkit installed
- Docker configured with `--gpus=all` flag support

