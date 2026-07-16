# CSS build stage — compiles Tailwind/daisyUI into a static stylesheet
FROM node:22-alpine AS css-build
WORKDIR /src
COPY package.json ./
RUN npm install
COPY . .
RUN npm run build:css

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY . .
COPY --from=css-build /src/wwwroot/css/app.tailwind.css ./wwwroot/css/app.tailwind.css


# Publish the app (verify your .csproj filename matches below)
RUN dotnet publish AOT.csproj -c Release -o /app

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "AOT.dll"]