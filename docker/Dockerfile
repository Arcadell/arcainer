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
FROM ubuntu/nginx
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
        curl \
        ca-certificates \
        libicu-dev && \
        curl -sSL https://get.docker.com/ | sh && \
        curl -SL https://github.com/docker/compose/releases/download/v2.30.3/docker-compose-linux-x86_64 -o /usr/local/bin/docker-compose && \
        chmod +x /usr/local/bin/docker-compose

COPY --from=frontend-build /app/dist /usr/share/nginx/html
COPY /docker/nginx.conf /etc/nginx/nginx.conf
COPY /docker/start.sh /app/start.sh

WORKDIR /app
COPY --from=backend-build /app ./
EXPOSE 80
EXPOSE 5000

RUN chmod +x ./Api ./start.sh
ENTRYPOINT ["bash", "./start.sh"]