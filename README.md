# API-For-Logging
Небольшая программа для работы с БД  

База данных: PostgreSQL  
Создание БД:  
CREATE DATEBASE ApiForLoggingDB  
CREATE TABLE Records (id INTEGER SERIAL primary key, date DATE, text VARCHAR, logLevel VARCHAR, source VARCHAR);
