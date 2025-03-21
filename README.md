## Arcainer

<p align="center">
  <img src="https://github.com/user-attachments/assets/d0c3c800-97de-4bf1-b24e-a3e79bf46233" />
</p>

**Arcainer** helps you to deploy your containerized applications through an intuitive web interface.

For now all the docker stacks are saved in a directory like this:

    .
    ├── ...
    ├── <stack-name>                    # Name of the stack
    │   ├── docker-compose.yml          # Docker compose file
    │   └── ...                         # Binded volumes
    └── <stack-name-2>

This allow you to import, export and manage your stack easy from the terminal too.

## Install

The best and easiest way to deploy Arcainer is currently by using Docker.

```
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