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
        nginx                               \
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




COPY --from=frontend-build /app/dist /usr/share/nginx/html
COPY /docker/nginx.conf /etc/nginx/nginx.conf
COPY /docker/start.sh /app/start.sh

WORKDIR /app
COPY --from=backend-build /app ./
EXPOSE 80
EXPOSE 5000

RUN chmod +x ./Api ./start.sh
ENTRYPOINT ["bash", "./start.sh"]