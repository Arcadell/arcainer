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
FROM ubuntu:rolling
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
        libicu-dev \
    && rm -rf /var/lib/apt/lists/*

COPY --from=frontend-build /app/dist /usr/share/nginx/html
COPY /nginx.conf /etc/nginx/conf.d/default.conf
WORKDIR /app
COPY --from=backend-build /app ./
EXPOSE 80
ENTRYPOINT ["./Api"]