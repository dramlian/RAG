#!/bin/bash

# ---- Ollama setup ----
echo "Starting Ollama container..."
docker run -d --gpus=all \
  -v ollama:/root/.ollama \
  -p 11434:11434 \
  --name ollama \
  ollama/ollama

# Wait a few seconds for Ollama to start
echo "Waiting 5 seconds for Ollama to initialize..."
sleep 5

# Pull Phi3 model inside Ollama
echo "Pulling Phi3 model..."
docker exec -it ollama ollama pull phi3

# ---- Qdrant setup ----
echo "Starting Qdrant container..."
docker run -d \
  --name qdrant \
  -p 6333:6333 \
  -v qdrant_storage:/qdrant/storage \
  qdrant/qdrant

echo "Setup complete!"
echo "Ollama API: http://localhost:11434"
echo "Qdrant API: http://localhost:6333"
