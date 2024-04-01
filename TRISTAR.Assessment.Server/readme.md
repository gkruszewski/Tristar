# TRISTAR.Assessment.Server

This project contains the web server that runs the http API.

* The `TRISTAR.Assessment.Infrastructure` namespace contains all concerns that would cut across features.
* The `TRISTAR.Assessment.People` namespace contains all code that has to do the person feature. In this namespace, you will find the `PersonController` which is responsible for routing http traffic to the `PersonServerRepository`.