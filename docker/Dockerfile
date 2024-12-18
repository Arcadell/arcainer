# [BACKEND]
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS backend-build
WORKDIR /source
# [BACKEND] copy csproj and restore as distinct layers
COPY *.sln .
COPY src/Api/*.csproj ./src/Api/
COPY src/Application/*.csproj ./src/Application/
COPY src/Domain/*.csproj ./src/Domain/
COPY src/FluentDocker/*.csproj ./src/FluentDocker/
COPY src/Persistence/*.csproj ./src/Persistence/
RUN dotnet restore
# [BACKEND] copy everything else and build app
COPY src/. ./src/
WORKDIR /source/src/Api
RUN dotnet publish -c Release -o /app --no-restore --self-contained --runtime linux-x64

# [FRONTEND]
FROM node:23-alpine AS frontend-base
ENV PNPM_HOME="/pnpm"
ENV PATH="$PNPM_HOME:$PATH"
RUN corepack enable
COPY ./ui /app
WORKDIR /app

# [FRONTEND] build ui app
FROM frontend-base AS frontend-build
RUN --mount=type=cache,id=pnpm,target=/pnpm/store pnpm install --frozen-lockfile
RUN pnpm run build

# final stage/image
FROM docker.io/debian:bookworm
RUN apt update;                             \
    apt install -y --no-install-recommends  \
        curl                                \
        ca-certificates                     \
        libicu-dev;                         
        
# Let's install docker (only the CLI)
RUN \
install -m 0755 -d /etc/apt/keyrings ; \ 
curl -fsSL https://download.docker.com/linux/debian/gpg -o /etc/apt/keyrings/docker.asc ; \
chmod a+r /etc/apt/keyrings/docker.asc ; \
echo \
"deb [arch=$(dpkg --print-architecture) signed-by=/etc/apt/keyrings/docker.asc] https://download.docker.com/linux/debian \
$(. /etc/os-release && echo "$VERSION_CODENAME") stable" | \
tee /etc/apt/sources.list.d/docker.list > /dev/null ; \
apt update ;                                          \
apt install -y docker-ce-cli docker-compose-plugin ; \
echo '#!/bin/bash\n\ndocker compose "$@"' > /usr/bin/docker-compose ; \
chmod 755 /usr/bin/docker-compose ;


# Set the working directory
WORKDIR /app


# Copy frontend and backend builds, Nginx config, and start script
COPY --from=frontend-build /app/dist ./wwwroot
COPY /docker/start.sh /usr/bin/start.sh
COPY --from=backend-build /app ./


VOLUME [ "/app/storage" ]



RUN useradd -ms /bin/bash arcainer ; \
chown -R arcainer /app ; \
chmod +x ./Api /usr/bin/start.sh ;

USER root

EXPOSE 5000
# Entry point for the container
ENTRYPOINT ["./Api"]
