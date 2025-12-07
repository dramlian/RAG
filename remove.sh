echo "Stopping and removing Ollama container..."
docker stop ollama
docker rm ollama

echo "Stopping and removing Qdrant container..."
docker stop qdrant
docker rm qdrant

echo "Cleanup complete!" 
