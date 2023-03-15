FROM openjdk:17

RUN mkdir -p /app/app-web

COPY target/DockerJavaService-0.0.1-SNAPSHOT.jar /app/app-web/api-admin.jar

EXPOSE 6060

ENTRYPOINT ["java", "-jar", "/app/app-web/api-admin.jar"]