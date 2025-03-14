One of the services in an online game needs to handle a request to exchange gold for resources. 
The operation involves deducting gold from a player’s account and crediting resources to the same player’s account.


The system is designed as follows:
 - A dedicated microservice manages the gold balance, with its own data storage.
 - Another dedicated microservice manages the resources, with its own separate data storage.
 - Both microservices are independent and can fail at any time.


Your task is to design a solution that ensures the following:
 - Either both the gold deduction and resource credit succeed, or neither occurs.
 - The gold balance must never become negative.
 - Once the operation is completed, the changes must persist even if any microservice fails