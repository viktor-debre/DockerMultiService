FROM maven:3.8.4-openjdk-17

WORKDIR /app/app-web

COPY . .

RUN mvn clean install -DskipTests
RUN mvn dependency:copy-dependencies

RUN mkdir -p /app/app-web/classes
RUN mkdir -p /app/app-web/libs

RUN cp -R target/classes/* /app/app-web/classes/
RUN cp -R target/dependency/* /app/app-web/libs/

EXPOSE 6060

ENTRYPOINT ["java", "-cp", "/app/app-web/classes:/app/app-web/libs/*", "ukr.nure.itm.inf.dockerjavaservice.DockerJavaServiceApplication"]