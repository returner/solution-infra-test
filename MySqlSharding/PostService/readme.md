##
https://www.thetechplatform.com/post/how-to-use-database-sharding-and-scale-an-asp-net-core-microservice-architecture
## docker create
docker run -p 3310:3306 --name=mysql1 -e MYSQL_ROOT_PASSWORD=pw -d mysql:5.6
docker run -p 3311:3306 --name=mysql2 -e MYSQL_ROOT_PASSWORD=pw -d mysql:5.6

## ���Ȯ��
docker container exec -it mysql1 /bin/sh
mysql -ppw
select * from post.Post;

docker container exec -it mysql2 /bin/sh
mysql -ppw
select * from post.Post;

## 참고자료
https://codeopinion.com/snapshots-in-event-sourcing-for-rehydrating-aggregates/
