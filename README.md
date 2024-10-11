Checkpoint realizado por Guilherme Miguel e Vicenzo Castelli


docker run --name database-mysql -e MYSQL_ROOT_PASSWORD=123 -p 3306:3306 -d mysql
//
docker run --name redis -p 6379:6379 -d redis


SCRIPT UTILIZADO:

CREATE TABLE sys.produtos ( id INT NOT NULL AUTO_INCREMENT, nome VARCHAR(45) NULL, preco DOUBLE NULL, quantidade_estoque INT NULL, data_criacao VARCHAR(45) NULL, PRIMARY KEY (id));
