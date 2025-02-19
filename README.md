## Arcainer
> [!WARNING]
> This project is still under development and not yet production-ready

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
git clone https://github.com/Arcadell/arcainer.git
cd arcainer
docker compose up -d
```