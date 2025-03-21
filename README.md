# Arcainer

<p align="center">
  <img src="https://github.com/user-attachments/assets/d0c3c800-97de-4bf1-b24e-a3e79bf46233" alt="Arcainer Logo" />
</p>

<p align="center">
  <a href="#features">Features</a> •
  <a href="#installation">Installation</a> •
  <a href="#usage">Usage</a> •
  <a href="#project-structure">Project Structure</a> •
  <a href="#contributing">Contributing</a>
</p>

Arcainer is a powerful web-based interface for managing and deploying containerized applications. It provides an intuitive way to manage your Docker stacks through a user-friendly web interface while maintaining compatibility with traditional Docker CLI operations.

## Features

- 🖥️ Web-based interface for Docker stack management
- 📦 Easy import and export of Docker stacks
- 🔄 Real-time stack status monitoring
- 📁 Organized stack storage structure
- 🛠️ Compatible with existing Docker CLI workflows

## Installation

The recommended way to deploy Arcainer is using Docker. Here's how to get started:

1. Create a `docker-compose.yml` file with the following configuration:

```yaml
services:
  arcainer:
    container_name: arcainer
    image: arcadell/arcainer:latest
    ports:
      - "8080:5000"
    volumes:
      - arcainer_storage:/storage
      - /var/run/docker.sock:/var/run/docker.sock
      
      # 1- NO RELATIVE PATH (./stacks:./stacks ❌) (/opt/stacks:/opt/stacks ✅)
      # 2- LEFT AND RIGHT PATH NEED TO BE THE SAME (/app/composes:/app/stacks ❌) (/app/stacks:/app/stacks ✅)
      - /opt/stacks:/opt/stacks
    environment:
      # STACKS_PATH NEED TO BE THE SAME AS ABOVE IF YOU CHANGE THIS CHANGE THE ABOVE TOO
      - STACKS_PATH=/opt/stacks

volumes:
  arcainer_storage:
    name: arcainer_storage
```

2. Run the stack:
```bash
docker-compose up -d
```

3. Access the web interface at `http://localhost:8080`

### Important Notes

- The `STACKS_PATH` environment variable must match the volume mount path
- Use absolute paths for volume mounts (e.g., `/opt/stacks:/opt/stacks`)
- Left and right paths in volume mounts must be identical
- Avoid using relative paths in volume mounts

## Usage

1. Access the web interface through your browser
2. Create, import, or manage your Docker stacks
3. Monitor stack status and logs
4. Deploy or update stacks as needed

## Project Structure

Arcainer organizes Docker stacks in a directory structure as follows:

```
.
├── ...
├── <stack-name>                    # Stack directory
│   ├── docker-compose.yml          # Docker compose configuration
│   └── ...                         # Additional stack files and volumes
└── <stack-name-2>
```

This structure allows for:
- Easy stack management through both web interface and CLI
- Simple stack import/export
- Organized storage of stack configurations
- Compatibility with existing Docker workflows

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.